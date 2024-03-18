package com.example.firstapp;

import androidx.appcompat.app.AppCompatActivity;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;

public class ServerCommunication extends AppCompatActivity {

    private static final String SERVER_IP = "XXX.XXX.XX.XXX";

    private static final int SERVER_PORT = 0000;
    private static Socket socket;
    private static BufferedReader input;
    private static OutputStream output;
    private static final List<UIUpdateListener> uiUpdateListeners = new ArrayList<>();
    public static void closeConnection() {
        try {
            if (output != null) {
                output.close();
            }
            if (input != null) {
                input.close();
            }
            if (socket != null) {
                socket.close();
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public static class ConnectToServerTask implements Runnable{

        public static void addUIUpdateListener(UIUpdateListener listener) {
            if (!uiUpdateListeners.contains(listener)){
                uiUpdateListeners.add(listener);
            }
        }

        public static void removeUIUpdateListener(UIUpdateListener listener) {
            uiUpdateListeners.remove(listener);
        }


        @Override
        public void run() {
            try {
                socket = new Socket(SERVER_IP, SERVER_PORT);
                input = new BufferedReader(new InputStreamReader(socket.getInputStream()));
                output = socket.getOutputStream();

                String welcomeMessage = input.readLine();
                if (welcomeMessage != null && !welcomeMessage.isEmpty()){
                    notifyUIUpdateListeners(welcomeMessage);
                }
                while (socket.isConnected() && !socket.isClosed()) {
                    String receivedMessage = input.readLine();
                    notifyUIUpdateListeners(receivedMessage);
                }
            }  catch (Exception e) {
                for (UIUpdateListener listener : uiUpdateListeners) {
                    listener.onDisconnect();
                }
                e.printStackTrace();
            }
        }
        private void notifyUIUpdateListeners(String message) {
            for (UIUpdateListener listener : uiUpdateListeners) {
                listener.onUIUpdate(message);
            }
        }
    } // class

    public static class SendMessageTask implements Runnable {

        private final String message;

        SendMessageTask(String message) {
            this.message = message;
        }

        @Override
        public void run() {
            try {
                output.write((message + "\n").getBytes());
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    } // class
}
