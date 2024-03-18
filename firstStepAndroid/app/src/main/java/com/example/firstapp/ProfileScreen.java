package com.example.firstapp;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Bundle;
import android.os.ParcelFileDescriptor;
import android.provider.MediaStore;
import android.text.TextUtils;
import android.view.KeyEvent;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.activity.result.ActivityResultLauncher;
import androidx.activity.result.contract.ActivityResultContracts;
import androidx.appcompat.app.AppCompatActivity;

import com.bumptech.glide.Glide;

public class ProfileScreen extends AppCompatActivity implements UIUpdateListener, StateReceivers {
    private SharedPreferences sharedPreferences;
    private Bitmap photoBitmap;
    private ImageView ivPhoto;
    private ActivityResultLauncher<String> imagePickerLauncher;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.profile_screen);

        mainMethod();
    }
    private void mainMethod(){
        StateReceiver.stateReceiver = this;
        sharedPreferences = getSharedPreferences("my_preferences", Context.MODE_PRIVATE);
        Button btnSelectPhoto = findViewById(R.id.btnSelectPhoto);
        Button btnNext = findViewById(R.id.PSBtnNext);
        TextView tvVersion = findViewById(R.id.tvVersion);
        EditText etName = findViewById(R.id.etName);
        EditText etAge = findViewById(R.id.etAge);
        ivPhoto = findViewById(R.id.ivPhoto);
        btnSelectPhoto.setOnClickListener(view -> openImage());
        btnNext.setOnClickListener(view -> goToMain());
        tvVersion.setText(BuildConfig.BUILD_DATE);
        imagePickerLauncher = registerForActivityResult(new ActivityResultContracts.GetContent(), result -> {
            if (result != null) {
                photoBitmap = uriToBitmap(this, result);
                Glide.with(this).load(photoBitmap).into(ivPhoto);
            }
        });
        if (!PreCheck.isFirstTimeStart){
            Glide.with(this).load(ContactManager.User.getPhoto()).into(ivPhoto);
            etName.setText(ContactManager.User.getName());
            etAge.setText(String.valueOf(ContactManager.User.getAge()));
        }
    }
    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if (keyCode == KeyEvent.KEYCODE_BACK) {
            Intent intent = new Intent(this,MainScreen.class);
            if(PreCheck.isFirstTimeStart){
                intent = new Intent(this,WelcomeScreen.class);
            }
            startActivity(intent);
            return true;
        }
        return super.onKeyDown(keyCode, event);
    }

    private void openImage() {imagePickerLauncher.launch("image/*");}
    public Bitmap uriToBitmap(Context context, Uri uri) {
        if (uri == null) {
            return null;
        }
        if ("content".equalsIgnoreCase(uri.getScheme())) {
            try (Cursor cursor = context.getContentResolver().query(uri, new String[]{MediaStore.Images.ImageColumns.DATA}, null, null, null)) {
                if (cursor != null && cursor.moveToFirst()) {
                    @SuppressLint("Range") String imagePath = cursor.getString(cursor.getColumnIndex(MediaStore.Images.ImageColumns.DATA));
                    if (imagePath != null) {
                        return BitmapFactory.decodeFile(imagePath);
                    }
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
            try (ParcelFileDescriptor pfd = context.getContentResolver().openFileDescriptor(uri, "r")) {
                if (pfd != null) {
                    return BitmapFactory.decodeFileDescriptor(pfd.getFileDescriptor());
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        } else if ("file".equalsIgnoreCase(uri.getScheme())) {
            return BitmapFactory.decodeFile(uri.getPath());
        }
        return null;
    }
    private void goToMain(){
        EditText etName = findViewById(R.id.etName);
        EditText etAge = findViewById(R.id.etAge);
        if (TextUtils.isEmpty(etName.getText().toString()) || TextUtils.isEmpty(etAge.getText().toString())){
            Toast.makeText(this, "Fill all fields", Toast.LENGTH_SHORT).show();
            return;
        }
        ContactManager.User = new Contact(etName.getText().toString(),
                Integer.parseInt(String.valueOf(etAge.getText())),
                        photoBitmap,
                        null);
        PreCheck.isFirstTimeStart = false;
        String imageInString = ContactManager.bitmapToBase64(photoBitmap);
        SharedPreferences.Editor editor = sharedPreferences.edit();
        editor.putBoolean("firstStart", false);
        editor.putString("name",etName.getText().toString());
        editor.putInt("age", Integer.parseInt(etAge.getText().toString()));
        editor.putString("photoBitmap", imageInString);

        editor.apply();

        Intent intent = new Intent(this,MainScreen.class);
        startActivity(intent);
    }
    private void showToast(String message) {Toast.makeText(this, message, Toast.LENGTH_SHORT).show();}

    @Override
    public void ShowModal() {StateReceiver.showDialogFragment(getSupportFragmentManager());}

    @Override
    public void HideModal() {
        if (StateReceiver.stateModalFragment.isAdded()){
            StateReceiver.stateModalFragment.dismiss();
        }
    }

    @Override
    public void onUIUpdate(String message) {}

    @Override
    public void onDisconnect() {runOnUiThread(() -> showToast("Disconnected from server"));}
} // class
