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

import androidx.core.content.ContextCompat;
import androidx.fragment.app.FragmentManager;

import java.util.ArrayList;
import java.util.List;

public class StateReceiver extends BroadcastReceiver {
    private static Context appContext;
    public static StateReceivers stateReceiver;
    private static boolean isRegistered = false;
    public static FragmentManager fragmentManager;
    public static StateModalFragment stateModalFragment = new StateModalFragment();


    @Override
    public void onReceive(Context context, Intent intent) {
        checkInternetLocationPermissions();
    }

    public static void startUp(Context context){
        appContext = context;
        if (!isRegistered) {
            IntentFilter connectivityFilter = new IntentFilter(ConnectivityManager.CONNECTIVITY_ACTION);
            IntentFilter locationFilter = new IntentFilter(LocationManager.MODE_CHANGED_ACTION);
            appContext.registerReceiver(new StateReceiver(), connectivityFilter);
            appContext.registerReceiver(new StateReceiver(), locationFilter);
            isRegistered = true;
        }

        StateReceiver.stateModalFragment.setCancelable(false);
    }
    public static void checkInternetLocationPermissions(){
        RepositoryList.modalItems.clear();
        if (!isNetworkAvailable()) {
            RepositoryList.modalItems.add("Internet");
        }

        LocationManager locationManager = (LocationManager) appContext.getSystemService(Context.LOCATION_SERVICE);

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.P) {
            if (locationManager == null || !locationManager.isLocationEnabled()) {
                RepositoryList.modalItems.add("Location");
            }
        }
        if (ContextCompat.checkSelfPermission(appContext, android.Manifest.permission.ACCESS_FINE_LOCATION)
                != PackageManager.PERMISSION_GRANTED) {
            RepositoryList.modalItems.add("PermissionsLocation");
        }
        if (ContextCompat.checkSelfPermission(appContext, Manifest.permission.READ_EXTERNAL_STORAGE)
                != PackageManager.PERMISSION_GRANTED) {
            RepositoryList.modalItems.add("PermissionsFiles");
        }
        if (RepositoryList.modalItems.size() > 0 && stateReceiver != null){
            if (stateModalFragment.isAdded()){
                stateModalFragment.updateItems(RepositoryList.modalItems);
            }
            else{
                stateModalFragment.setItems(RepositoryList.modalItems);
                stateReceiver.ShowModal();
            }
        }
        else {
            if (stateReceiver != null){
                stateReceiver.HideModal();
            }
        }
    }
    public static void showDialogFragment(FragmentManager fragmentManager){
        stateModalFragment.show(fragmentManager, "StateModalFragment");
    }

    private static boolean isNetworkAvailable() {
        ConnectivityManager connectivityManager = (ConnectivityManager) appContext.getSystemService(Context.CONNECTIVITY_SERVICE);
        if (connectivityManager != null) {
            NetworkCapabilities capabilities = connectivityManager.getNetworkCapabilities(connectivityManager.getActiveNetwork());
            return capabilities != null && (capabilities.hasTransport(NetworkCapabilities.TRANSPORT_CELLULAR) ||
                    capabilities.hasTransport(NetworkCapabilities.TRANSPORT_WIFI));
        }
        return false;
    }

}// class
