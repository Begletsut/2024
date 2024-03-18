package com.example.firstapp;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;

import androidx.fragment.app.DialogFragment;

import java.util.ArrayList;
import java.util.List;

public class StateModalFragment extends DialogFragment {
    private List<String> items = new ArrayList<>();
    ModalAdapter adapter;


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_modal, container, false);
        ListView listView = view.findViewById(R.id.fragmentModal);
        adapter = new ModalAdapter(getActivity(), items);
        listView.setAdapter(adapter);
        return view;
    }
    public void setItems(List<String> modalItems){
        items = modalItems;

    }
    public void updateItems(List<String> modalItems){
        items = modalItems;
        adapter.notifyDataSetChanged();
    }
}
