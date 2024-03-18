package com.example.firstapp;

import android.content.Context;
import android.content.DialogInterface;

import androidx.appcompat.app.AlertDialog;

public class MyDialog {
    public static void showYesNoDialog(Context context, String message, DialogInterface.OnClickListener yesListener, DialogInterface.OnClickListener noListener) {
        AlertDialog.Builder builder = new AlertDialog.Builder(context);
        builder.setMessage(message)
                .setPositiveButton("Yes", yesListener)
                .setNegativeButton("No", noListener)
                .show();
    }
}
