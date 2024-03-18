package com.example.firstapp;

import android.Manifest;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.pm.PackageManager;
import android.location.LocationManager;
import android.net.ConnectivityManager;
import android.net.NetworkCapabilities;
import android.os.Build;
import android.os.Bundle;
import android.provider.Settings;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.ActivityCompat;
import androidx.core.content.ContextCompat;


public class WelcomeScreen extends AppCompatActivity {
    private boolean arePermissionsGranted = false;
    private boolean areAccessFilesGranted = false;
    private boolean isWifiEnabled = false;
    private boolean isLocationEnabled = false;
    @RequiresApi(api = Build.VERSION_CODES.TIRAMISU)
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.welcome_screen);

        Button btnLocationEnable = findViewById(R.id.btnLocationON);
        btnLocationEnable.setOnClickListener(view -> enableLocation());
        Button btnGrantPermissions = findViewById(R.id.btnGrantPermissions);
        btnGrantPermissions.setOnClickListener(view -> grantPermissions());
        Button btnNext = findViewById(R.id.btnNext);
        btnNext.setOnClickListener(view -> goToProfileScreen());
        Button btnAccessFiles = findViewById(R.id.btnGrantFiles);
        btnAccessFiles.setOnClickListener(view -> grantFilesPermission());

        registerReceiver(new StateReceiver_test(), new IntentFilter(ConnectivityManager.CONNECTIVITY_ACTION));
        registerReceiver(new StateReceiver_test(), new IntentFilter(LocationManager.MODE_CHANGED_ACTION));

        checkInternetLocationPermissions();
    }
    protected void onDestroy() {
        unregisterReceiver(new StateReceiver_test());
        super.onDestroy();
    }
    private void grantFilesPermission(){
        if (ActivityCompat.checkSelfPermission(this, android.Manifest.permission.READ_EXTERNAL_STORAGE) != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(this, new String[]{android.Manifest.permission.READ_EXTERNAL_STORAGE}, 1);
        }
        else {
            showToast("Permission already granted.");
        }
    }

    private void showToast(String message) {
        Toast.makeText(this, message, Toast.LENGTH_SHORT).show();
    }
    private void goToProfileScreen(){
        Intent intent = new Intent(this, ProfileScreen.class);
        startActivity(intent);
    }
    private void grantPermissions(){
        if (ContextCompat.checkSelfPermission(this, android.Manifest.permission.ACCESS_FINE_LOCATION)
                != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(this,
                    new String[]{android.Manifest.permission.ACCESS_FINE_LOCATION},
                    1);
        }
        else {
            showToast("Permission already granted.");
        }
    }
    private void checkInternetLocationPermissions(){

        ImageView iconWifi = findViewById(R.id.ivInternet);
        ImageView iconLocation = findViewById(R.id.ivLocation);
        ImageView iconPermissions = findViewById(R.id.ivPermissions);
        ImageView iconAccessFiles = findViewById(R.id.ivAccessFiles);
        Button btnLocation = findViewById(R.id.btnLocationON);

        if (isNetworkAvailable()) {
            iconWifi.setImageDrawable(ContextCompat.getDrawable(this, android.R.drawable.checkbox_on_background));
            isWifiEnabled = true;
        }
        else{
            iconWifi.setImageDrawable(ContextCompat.getDrawable(this, android.R.drawable.btn_dialog));
            isWifiEnabled = false;
        }

        LocationManager locationManager = (LocationManager) getApplicationContext().getSystemService(Context.LOCATION_SERVICE);

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.P) {
            if (locationManager != null && locationManager.isLocationEnabled()) {
                iconLocation.setImageDrawable(ContextCompat.getDrawable(this, android.R.drawable.checkbox_on_background));
                isLocationEnabled = true;
            } else {
                iconLocation.setImageDrawable(ContextCompat.getDrawable(this, android.R.drawable.btn_dialog));
                isLocationEnabled = false;
            }
        }
        if (ContextCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION)
                == PackageManager.PERMISSION_GRANTED) {
            iconPermissions.setImageDrawable(ContextCompat.getDrawable(this, android.R.drawable.checkbox_on_background));
            arePermissionsGranted = true;
            btnLocation.setEnabled(true);
        }
        else {
            iconPermissions.setImageDrawable(ContextCompat.getDrawable(this, android.R.drawable.btn_dialog));
            arePermissionsGranted = false;
            btnLocation.setEnabled(false);
        }
        if (ContextCompat.checkSelfPermission(this, Manifest.permission.READ_EXTERNAL_STORAGE)
                == PackageManager.PERMISSION_GRANTED) {
            iconAccessFiles.setImageDrawable(ContextCompat.getDrawable(this, android.R.drawable.checkbox_on_background));
            areAccessFilesGranted = true;
        }
        else {
            iconAccessFiles.setImageDrawable(ContextCompat.getDrawable(this, android.R.drawable.btn_dialog));
            areAccessFilesGranted = false;
        }
        checkAndEnableButton();
    }
    private void enableLocation() {
        if (!isLocationEnabled) {
            startActivity(new Intent(Settings.ACTION_LOCATION_SOURCE_SETTINGS));
        } else {
            showToast("Location is already ON");
        }
    }
    private boolean isNetworkAvailable() {
        ConnectivityManager connectivityManager = (ConnectivityManager) this.getSystemService(Context.CONNECTIVITY_SERVICE);
        if (connectivityManager != null) {
            NetworkCapabilities capabilities = connectivityManager.getNetworkCapabilities(connectivityManager.getActiveNetwork());
            return capabilities != null && (capabilities.hasTransport(NetworkCapabilities.TRANSPORT_CELLULAR) ||
                    capabilities.hasTransport(NetworkCapabilities.TRANSPORT_WIFI));
        }
        return false;
    }

    private void checkAndEnableButton() {
        Button btnNext = findViewById(R.id.btnNext);
        btnNext.setEnabled(isWifiEnabled && isLocationEnabled && arePermissionsGranted && areAccessFilesGranted);
    }
    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        checkInternetLocationPermissions();
    }
    public class StateReceiver_test extends BroadcastReceiver {
        @Override
        public void onReceive(Context context, Intent intent) {
            checkInternetLocationPermissions();
        }
    }// class
} // class

