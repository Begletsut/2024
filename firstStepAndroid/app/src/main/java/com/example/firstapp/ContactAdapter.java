package com.example.firstapp;

import android.annotation.SuppressLint;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.bumptech.glide.Glide;

import java.util.List;
import java.util.Locale;

public class ContactAdapter extends BaseAdapter {
    private final Context context;
    private final List<Contact> contacts;
    public ContactAdapter(Context context, List<Contact> contacts) {
        this.context = context;
        this.contacts = contacts;
    }
    @Override
    public int getCount() {
        return contacts.size();
    }

    @Override
    public Object getItem(int position) {
        return contacts.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        LayoutInflater inflater = LayoutInflater.from(context);
        @SuppressLint("ViewHolder") View view = inflater.inflate(R.layout.contact_item, parent, false);

        ImageView contactItemPhoto = view.findViewById(R.id.contactItemPhoto);
        TextView contactItemName = view.findViewById(R.id.contactItemName);

        Contact contact = (Contact) getItem(position);
        Glide.with(context).load(contact.getPhoto()).into(contactItemPhoto);
        long minutes = contact.timeCD / (60 * 1000);
        long seconds = (contact.timeCD % (60 * 1000)) / 1000;

        String timeLeftFormatted = String.format(Locale.getDefault(), "%02d:%02d", minutes, seconds);
        String showText = contact.getName() + " " + contact.getAge() + " years " + timeLeftFormatted;
        contactItemName.setText(showText);

        return view;
    }
}
