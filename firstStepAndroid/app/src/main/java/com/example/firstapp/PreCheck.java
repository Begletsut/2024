package com.example.firstapp;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

public class PreCheck extends AppCompatActivity {
    public static boolean isFirstTimeStart;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.fragment_modal);
        StateReceiver.startUp(getApplicationContext());
        isFirstTimeStart();
        Intent intent;
        if(isFirstTimeStart){
            intent = new Intent(this, WelcomeScreen.class);
        }
        else {
            intent = new Intent(this, MainScreen.class);
        }
        startActivity(intent);
    }
    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        StateReceiver.checkInternetLocationPermissions();
    }
    public void isFirstTimeStart(){
        SharedPreferences sharedPreferences = getSharedPreferences("my_preferences", Context.MODE_PRIVATE);
        isFirstTimeStart = sharedPreferences.getBoolean("firstStart", true);
    }
} // class
