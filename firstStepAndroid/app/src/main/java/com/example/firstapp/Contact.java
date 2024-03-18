package com.example.firstapp;

import android.graphics.Bitmap;
import android.location.Location;
import android.os.CountDownTimer;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class Contact implements Serializable {
    private int id;
    private final String name;
    private final int age;
    private final Bitmap photo;
    public List<ChatMessage> Chat;
    private Location location;
    public long timeCD = 0;
    private CountDownTimer countDownTimer;
    private static final long CD = 20 * 1000;

    public Contact(String name, int age, Bitmap photo, Location location) {
        this.name = name;
        this.age = age;
        this.photo = photo;
        Chat = new ArrayList<>();
        this.location = location;
    }

    public String getName(){
        return name;
    }
    public int getAge(){
        return age;
    }
    public Bitmap getPhoto(){
        return photo;
    }
    public Location getLocation(){
        return this.location;
    }

    public void startCDTimer() {
        countDownTimer = new CountDownTimer(CD, 1000) {
            public void onTick(long millisUntilFinished) {
                timeCD = millisUntilFinished;
            }

            public void onFinish() {
                cancelTimer();
            }
        }.start();
    }
    public void cancelTimer() {
        if (countDownTimer != null) {
            countDownTimer.cancel();
            timeCD = 0;
        }
    }
}
