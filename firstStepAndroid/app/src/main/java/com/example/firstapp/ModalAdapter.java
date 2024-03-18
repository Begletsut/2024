package com.example.firstapp;

import android.annotation.SuppressLint;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import java.util.List;

public class ModalAdapter extends BaseAdapter {
    private final Context context;
    private final List<String> modalItems;
    public ModalAdapter(Context context, List<String> items) {
        this.context = context;
        this.modalItems = items;
    }
    @Override
    public int getCount() {
        return modalItems.size();
    }

    @Override
    public Object getItem(int position) {
        return modalItems.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        LayoutInflater inflater = LayoutInflater.from(context);
        @SuppressLint("ViewHolder") View view = inflater.inflate(R.layout.contact_item, parent, false);
        TextView itemName = view.findViewById(R.id.contactItemName);

        String item = getItem(position).toString();
        itemName.setText(item);

        return view;
    }
}
