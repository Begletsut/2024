package com.example.firstapp;

public class ChatMessage {
    private Boolean isIncoming;
    private String text;

    public ChatMessage(Boolean isIncoming, String text) {
        this.isIncoming = isIncoming;
        this.text = text;
    }

    public String getText() {return text;}
    public boolean isIncoming() {return isIncoming;}
}
