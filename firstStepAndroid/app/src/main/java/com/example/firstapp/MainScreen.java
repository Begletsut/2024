package com.example.firstapp;

import android.Manifest;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.location.Location;
import android.location.LocationManager;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.view.KeyEvent;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.ActivityCompat;

import com.bumptech.glide.Glide;
import com.google.android.gms.location.FusedLocationProviderClient;
import com.google.android.gms.location.LocationServices;
import com.google.android.gms.tasks.Task;

import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.CompletableFuture;


public class MainScreen extends AppCompatActivity implements UIUpdateListener, StateReceivers, UpdateTimers {
    private ContactAdapter contactAdapter;
    private LocationManager locationManager;
    CompletableFuture<Void> future;
    private FusedLocationProviderClient fusedLocationProviderClient;
    private double latitude = 0;
    private double longitude = 0;
    private boolean doubleBackToExitPressedOnce = false;
    public static int UserMinutes = 15;
    private TextView tvMinutes;
    private ListView gridContacts;
    ImageView ivPhoto;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main_screen);

        mainMethod();
    }

    private void mainMethod() {
        StateReceiver.stateReceiver = this;
        ContactManager.listener = this;
        ServerCommunication.ConnectToServerTask.addUIUpdateListener(this);

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.N) {
            future = new CompletableFuture<>();
        }
        StateReceiver.fragmentManager = getSupportFragmentManager();
        fusedLocationProviderClient = LocationServices.getFusedLocationProviderClient(this);

        gridContacts = findViewById(R.id.MainGridContacts);
        locationManager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);

        TextView tvVersion = findViewById(R.id.MainTvVersion);
        ivPhoto = findViewById(R.id.MainIvPhoto);
        Button btnScan = findViewById(R.id.MainBtnScan);
        Button btnClose = findViewById(R.id.btnClose);

        tvVersion.setText(BuildConfig.BUILD_DATE);
        ivPhoto.setOnClickListener(view -> goToProfile());
        tvMinutes = findViewById(R.id.MainTvMinutes);
        tvMinutes.setText(String.valueOf(UserMinutes));
        btnScan.setOnClickListener(view -> {
            try {
                connectToServer();
            } catch (InterruptedException | FileNotFoundException e) {
                throw new RuntimeException(e);
            }
        });
        btnClose.setOnClickListener(view -> {
            ServerCommunication.closeConnection();
            ContactManager.contacts.clear();
            contactAdapter.notifyDataSetChanged();
        });
        loadData();
        if (!ContactManager.isTimerRunning){
            ContactManager.startTimersLoop();
        }

        /////////////////////////<TEMP
        if (ContactManager.contacts.isEmpty()){
            ContactManager.contacts.add(new Contact("contactExample1", 123, null, null));
            ContactManager.contacts.add(new Contact("contactExample2", 321, null, null));
            ContactManager.contacts.add(new Contact("contactExample3", 321, null, null));

        }
        contactAdapter = new ContactAdapter(this, ContactManager.contacts);
        gridContacts.setAdapter(contactAdapter);
        gridContacts.setOnItemClickListener((parent, view, position, id) -> {
            Contact selectedContact = (Contact) contactAdapter.getItem(position);
            goToSingleScreen(selectedContact);
        });
        contactAdapter.notifyDataSetChanged();
        ListView gridMap = findViewById(R.id.lwMap);
        List<Contact> contactsMap = new ArrayList<>();
        ContactAdapter adapter2 = new ContactAdapter(this, contactsMap);
        contactsMap.add(new Contact("MAP", 102, null, null));
        gridMap.setAdapter(adapter2);
        ///////////////////////// TEMP/>

    }

    private void sendMsgServer(String message) {
        ServerCommunication.SendMessageTask sendTask = new ServerCommunication.SendMessageTask(message);
        new Thread(sendTask).start();
    }

    private void connectToServer() throws InterruptedException, FileNotFoundException {
        new Handler(msg -> {
            String receivedMessage = (String) msg.obj;
            showToast(receivedMessage);
            return true;
        });

        ServerCommunication.ConnectToServerTask.addUIUpdateListener(this);
        ServerCommunication.ConnectToServerTask connectTask = new ServerCommunication.ConnectToServerTask();
        new Thread(connectTask).start();
        Thread.sleep(1000);

        getLocation();
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.N) {
            future.thenRun(() -> sendMsgServer("FirstStep#" + ContactManager.User.getName() + "#" + ContactManager.User.getAge() + "#" + latitude + "#" + longitude + "#" + "image"));
        }

        //String MyPhotoString = ContactManager.bitmapToBase64(MyPhoto); TODO: image android-tcp-android

    }



    private void showToast(String message) {
        Toast.makeText(this, message, Toast.LENGTH_SHORT).show();
    }
    private void getLocation(){
        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            return;
        }
        Task<Location> locationTask = fusedLocationProviderClient.getLastLocation();
        locationTask.addOnCompleteListener(this, task -> {
            if (task.isSuccessful() && task.getResult() != null) {
                Location location = task.getResult();
                latitude = location.getLatitude();
                longitude = location.getLongitude();
            } else {
                showToast("Error getting location");
            }
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.N) {
                future.complete(null);
            }
        });
    }

    private void loadData() {
        SharedPreferences sharedPreferences = getSharedPreferences("my_preferences", Context.MODE_PRIVATE);
        String name = sharedPreferences.getString("name", "");
        int age = sharedPreferences.getInt("age", 0);
        Bitmap photo = ContactManager.base64ToBitmap(sharedPreferences.getString("photoBitmap", ""));
        ContactManager.User = new Contact(name, age, photo, null);
        TextView tvName = findViewById(R.id.MainTvName);
        String textToShow = ContactManager.User.getName() + " " + ContactManager.User.getAge() + " years";
        tvName.setText(textToShow);
        ivPhoto = findViewById(R.id.MainIvPhoto);
        Glide.with(this).load(ContactManager.User.getPhoto()).into(ivPhoto);
    }

    private void goToSingleScreen(Contact contact) {
        MyDialog.showYesNoDialog(this, String.format("Do you want to start conversation with %s ?", contact.getName()), (dialog, which) -> {
            Intent intent = new Intent(this, SingleContactScreen.class);
            intent.putExtra("contact", contact.getName());
            intent.putExtra("myName", ContactManager.User.getName());
            startActivity(intent);
        }, (dialog, which) -> {});
    }

    private void goToProfile() {
        Intent intent = new Intent(this, ProfileScreen.class);
        startActivity(intent);
    }
    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if (keyCode == KeyEvent.KEYCODE_BACK) {
            if (doubleBackToExitPressedOnce) {
                finishAffinity();
                return false;
            }
            this.doubleBackToExitPressedOnce = true;
            showToast("Press 2 times to exit");

            new Handler().postDelayed(() -> doubleBackToExitPressedOnce = false, 1000);
            return true;
        }
        return super.onKeyDown(keyCode, event);
    }

    @Override
    public void onUIUpdate(String message) {
        runOnUiThread(() -> {
            String[] splitMessage = message.split("&");
            if (splitMessage[0].equals("Users")) {
                ContactManager.contacts.clear();
                Location userLocation = new Location("");
                for (int i = 1; i < splitMessage.length; i++) {
                    String[] splitUser = splitMessage[i].split("#");
                    String userName = splitUser[0];
                    int userAge = Integer.parseInt(splitUser[1]);
                    userLocation.setLatitude(Double.parseDouble(splitUser[2]));
                    userLocation.setLatitude(Double.parseDouble(splitUser[3]));
                    //Bitmap userPhoto = ContactManager.base64ToBitmap(splitUser[4]);
                    ContactManager.contacts.add(new Contact(userName, userAge, null, userLocation));
                }
                contactAdapter.notifyDataSetChanged();
            } else if (splitMessage[0].equals("P2PMessage")) {
                String sender = splitMessage[1];
                if (RepositoryList.blackList.contains(sender)){return;}
                String text = splitMessage[3];
                Contact currentContact = ContactManager.getContact(sender);
                assert currentContact != null;
                currentContact.Chat.add(new ChatMessage(true, text));
                NotificationHelper.showNotification(this, "FirstStep", "Message from: " + sender);
            } else if (splitMessage[0].equals("NewUser")){
                String[] decodedMessage = splitMessage[1].split("#");
                String newUserName = decodedMessage[0];
                int newUserAge = Integer.parseInt(decodedMessage[1]);
                Location newUserLocation = new Location("");
                newUserLocation.setLatitude(Double.parseDouble(decodedMessage[2]));
                newUserLocation.setAltitude(Double.parseDouble(decodedMessage[3]));
//                String newUserPhoto = decodedMessage[4];
                ContactManager.contacts.add(new Contact(newUserName, newUserAge, null, newUserLocation));
                contactAdapter.notifyDataSetChanged();
            }
            else if (splitMessage[0].equals("RemovedUser")){
                ContactManager.removeContact(splitMessage[1]);
                contactAdapter.notifyDataSetChanged();
            }
        });
    }

    @Override
    public void onDisconnect() {
        runOnUiThread(() ->
                showToast("disconnected from server"));
                ContactManager.contacts.clear();
                contactAdapter.notifyDataSetChanged();
    }

    @Override
    public void ShowModal() {
        if (!StateReceiver.stateModalFragment.isAdded()){
            StateReceiver.showDialogFragment(getSupportFragmentManager());
        }
    }

    @Override
    public void HideModal() {
        if (StateReceiver.stateModalFragment.isAdded()){
            StateReceiver.stateModalFragment.dismiss();
        }
    }
    @Override
    protected void onResume() {
        super.onResume();
        tvMinutes.setText(String.valueOf(UserMinutes) + " minutes");
    }

    @Override
    public void onTickUpdate() {runOnUiThread(() -> {contactAdapter.notifyDataSetChanged();});}

    @Override
    public void onFinishTextView() {}
} // class
