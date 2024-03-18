package com.example.firstapp;


import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Base64;

import java.io.ByteArrayOutputStream;
import java.util.ArrayList;
import java.util.List;

public class ContactManager {
//    private static final String PREF_NAME = "contact_prefs";
//    private static final String KEY_CONTACTS = "contacts";
//    public static final Map<String, CountDownTimer> timers = new HashMap<>();
    public static UpdateTimers listener;
    public static List<Contact> contacts = new ArrayList<>();
    public static Contact User;
    public static int userChatting = 0;
    public static boolean isTimerRunning = false;

    public static Contact getContact(String name){
        for (Contact contact : contacts){
            if (contact.getName().equals(name)){
                return contact;
            }
        }
        return null;
    }

    public static void removeContact(String name){contacts.remove(getContact(name));}
    public static void startTimersLoop(){
        Thread thread = new Thread(() -> {
            isTimerRunning = true;
            while (true) {
                if (listener != null){listener.onTickUpdate();}
                try {
                    Thread.sleep(1000);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
            }
        });
        thread.start();
    }

//    public static void saveContacts(Context context, List<Contact> contacts) {
//        SharedPreferences prefs = context.getSharedPreferences(PREF_NAME, Context.MODE_PRIVATE);
//        SharedPreferences.Editor editor = prefs.edit();
//
//        Gson gson = new Gson();
//        String jsonContacts = gson.toJson(contacts);
//        editor.putString(KEY_CONTACTS, jsonContacts);
//
//        editor.apply();
//    }
//
//    public static List<Contact> loadContacts(Context context) {
//        SharedPreferences prefs = context.getSharedPreferences(PREF_NAME, Context.MODE_PRIVATE);
//        String jsonContacts = prefs.getString(KEY_CONTACTS, "");
//
//        Gson gson = new Gson();
//        Type type = new TypeToken<List<Contact>>() {}.getType();
//        return gson.fromJson(jsonContacts, type);
//    }
    public static String bitmapToBase64(Bitmap bitmap) {
    ByteArrayOutputStream bytes = new ByteArrayOutputStream();
    bitmap.compress(Bitmap.CompressFormat.PNG, 100, bytes);
    byte[] byteArray = bytes.toByteArray();
    return Base64.encodeToString(byteArray, Base64.DEFAULT);
    }

    public static Bitmap base64ToBitmap(String base64) {
        byte[] decodedByteArray = Base64.decode(base64, Base64.DEFAULT);
        return BitmapFactory.decodeByteArray(decodedByteArray, 0, decodedByteArray.length);
    }

}
