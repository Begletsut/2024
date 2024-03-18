package com.example.firstapp;

import android.animation.ObjectAnimator;
import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.text.TextUtils;
import android.view.Gravity;
import android.view.KeyEvent;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;

import java.util.Locale;

public class SingleContactScreen extends AppCompatActivity implements UIUpdateListener, StateReceivers, UpdateTimers {
    private Contact currentContact;
    private ChatMessageAdapter messagesAdapter;
    private CountDownTimer countDownTimer;
    private EditText etMessage;
    private RecyclerView gridMessages;
    private ImageView ivEmoji;
    private TextView tvTimer;
    private TextView tvCDTimer;
    private Button btnSend;
    private Button btnBlockUnblock;
    private final long timerDialog = 15000;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.single_contact_screen);
        mainMethod();
    }

    private void mainMethod() {
        StateReceiver.stateReceiver = this;
        ContactManager.listener = this;
        ServerCommunication.ConnectToServerTask.addUIUpdateListener(this);
        Intent intent = getIntent();
        currentContact = ContactManager.getContact((String) intent.getSerializableExtra("contact"));

        Button btnAdd5min = findViewById(R.id.SingleBtnAdd5min);
        View footer = findViewById(R.id.SingleFooter);
        ImageView photo = findViewById(R.id.scPhoto);
        TextView TVName = findViewById(R.id.sctvName);
        TextView TVAge = findViewById(R.id.sctvAge);
        EmojiPopupWindow emojiPopupWindow = new EmojiPopupWindow(this, etMessage);

        ivEmoji = findViewById(R.id.ivEmoji);
        etMessage = findViewById(R.id.etMessage);
        tvTimer = findViewById(R.id.SingleTVTimer);
        tvCDTimer = findViewById(R.id.SingleTVCDTimer);
        btnAdd5min.setOnClickListener(view -> add5min());
        btnBlockUnblock = findViewById(R.id.SingleBtnBlockUnblock);
        btnBlockUnblock.setOnClickListener(view -> blockUnblockContact());
        btnSend = findViewById(R.id.btnSend);
        btnSend.setOnClickListener(view -> sendMsgServer());
        TVName.setText(currentContact.getName());
        Glide.with(this).load(currentContact.getPhoto()).into(photo);
        String ageYearsOld = String.format("%s years old", currentContact.getAge());
        TVAge.setText(ageYearsOld);
        messagesAdapter = new ChatMessageAdapter(currentContact.Chat);
        gridMessages = findViewById(R.id.gridMessages);
        gridMessages.setLayoutManager(new LinearLayoutManager(this));
        gridMessages.setAdapter(messagesAdapter);

        if (RepositoryList.blackList.contains(currentContact.getName())){
            btnBlockUnblock.setText("Unblock");
        }
        else {
            btnBlockUnblock.setText("Block");
        }
        ivEmoji.setOnClickListener(v -> {
            if (emojiPopupWindow.isShowing()) {
                emojiPopupWindow.dismiss();
                ObjectAnimator footerAnimator = ObjectAnimator.ofFloat(footer, "translationY", -300, 0);
                footerAnimator.setDuration(500);
                footerAnimator.start();
            } else {
                ObjectAnimator footerAnimator = ObjectAnimator.ofFloat(footer, "translationY", 0, -300);
                footerAnimator.setDuration(500);
                footerAnimator.start();

                InputMethodManager imm = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
                imm.hideSoftInputFromWindow(etMessage.getWindowToken(), 0);
                emojiPopupWindow.showAtLocation(v, Gravity.BOTTOM, 0, 0);
            }
        });

        if (currentContact.timeCD != 0){
            disableControls();
        }
        else {
            startTimerDialog();
        }
    }

    private void blockUnblockContact() {
        if (RepositoryList.blackList.contains(currentContact.getName())){
            RepositoryList.blackList.remove(currentContact.getName());
            btnBlockUnblock.setText("Block");
        }
        else{
            RepositoryList.blackList.add(currentContact.getName());
            btnBlockUnblock.setText("Unblock");
        }
    }


    private void showToast(String message) {Toast.makeText(this, message, Toast.LENGTH_SHORT).show();}

    private void sendMsgServer(){
        if (etMessage.getText().toString().isEmpty()){
            return;
        }
        addChatMessageContact(new ChatMessage(false, "You:\n" + etMessage.getText().toString().trim()));
        String message = etMessage.getText().toString();
        String completeMessage = "P2PMessage&" + ContactManager.User.getName() + "&" + currentContact.getName() + "&" + message;
        gridMessages.smoothScrollToPosition(messagesAdapter.getItemCount() - 1);
        etMessage.setText("");
        ServerCommunication.SendMessageTask sendTask = new ServerCommunication.SendMessageTask(completeMessage);
        new Thread(sendTask).start();
    }
    private void add5min() {
        if (MainScreen.UserMinutes >= 5){
            MainScreen.UserMinutes -= 5;
            enableControls();
            countDownTimer.cancel();
            startTimerDialog();
        }
        else {showToast("Not enough ?minutes?");}
    }
    private void enableControls(){
        etMessage.setEnabled(true);
        btnSend.setEnabled(true);
        ivEmoji.setEnabled(true);
    }
    private void disableControls(){
        etMessage.setEnabled(false);
        btnSend.setEnabled(false);
        ivEmoji.setEnabled(false);
    }
    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if (keyCode == KeyEvent.KEYCODE_BACK) {
                MyDialog.showYesNoDialog(this, String.format("Do you want to end conversation with %s?", currentContact.getName()), (dialog, which) -> {
                    if (currentContact.timeCD == 0){
                        currentContact.startCDTimer();
                    }
                    if (countDownTimer != null){countDownTimer.cancel();}
                    Intent intent = new Intent(this,MainScreen.class);
                    startActivity(intent);
                }, (dialog, which) -> {});
                return true;
        }
        return super.onKeyDown(keyCode, event);
    }
    private void startTimerDialog() {
        countDownTimer = new CountDownTimer(timerDialog, 1000) {
            public void onTick(long millisUntilFinished) {
                long minutes = millisUntilFinished / (60 * 1000);
                long seconds = (millisUntilFinished % (60 * 1000)) / 1000;

                String timeLeftFormatted = String.format(Locale.getDefault(), "%02d:%02d", minutes, seconds);
                tvTimer.setText(timeLeftFormatted);
            }

            public void onFinish() {
                currentContact.startCDTimer();
                disableControls();
            }
        }.start();
    }

    @SuppressLint("NotifyDataSetChanged")
    private void addChatMessageContact(ChatMessage message) {// add message to List<String> chat
        if (TextUtils.isEmpty(message.getText())){
            return;
        }
        currentContact.Chat.add(message);
        messagesAdapter.notifyDataSetChanged();
    }
    @SuppressLint("NotifyDataSetChanged")
    @Override
    public void onUIUpdate(String message) {runOnUiThread(() -> messagesAdapter.notifyDataSetChanged());}

    @Override
    public void onDisconnect() {runOnUiThread(() -> {
        showToast("disconnected from server");
        disableControls();});
    }

    @Override
    public void ShowModal() {if (!StateReceiver.stateModalFragment.isAdded()) StateReceiver.showDialogFragment(getSupportFragmentManager());}

    @Override
    public void HideModal() {if (StateReceiver.stateModalFragment.isAdded()) StateReceiver.stateModalFragment.dismiss();}

    @Override
    public void onTickUpdate() {
        runOnUiThread(() -> {
            long minutes = currentContact.timeCD / (60 * 1000);
            long seconds = (currentContact.timeCD % (60 * 1000)) / 1000;

            String timeLeftFormatted = String.format(Locale.getDefault(), "%02d:%02d", minutes, seconds);
            tvCDTimer.setText(timeLeftFormatted);});
    }

    @Override
    public void onFinishTextView() {tvCDTimer.setText("00:00");}
}