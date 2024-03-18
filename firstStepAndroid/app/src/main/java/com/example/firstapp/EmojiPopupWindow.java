package com.example.firstapp;

import android.content.Context;
import android.util.TypedValue;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.PopupWindow;
import android.widget.TextView;

public class EmojiPopupWindow {
    private final Context context;
    private final PopupWindow popupWindow;
    private final EditText editText;

    public EmojiPopupWindow(Context context, EditText editText) {
        this.context = context;
        this.popupWindow = new PopupWindow(context);
        setupPopupWindow();
        this.editText = editText;
    }

    private void setupPopupWindow() {
        View contentView = LayoutInflater.from(context).inflate(R.layout.emoji_popup_layout, null);
        popupWindow.setContentView(contentView);
        popupWindow.setWidth(LinearLayout.LayoutParams.WRAP_CONTENT);
        popupWindow.setHeight(LinearLayout.LayoutParams.WRAP_CONTENT);
        popupWindow.setOutsideTouchable(false);

        LinearLayout emojiContainer = contentView.findViewById(R.id.emojiContainer);

        int[] emojiCodes = {0x1F600, 0x1F601, 0x1F602};

        for (int emojiCode : emojiCodes) {
            String emoji = getEmojiByUnicode(emojiCode);
            TextView emojiTextView = new TextView(context);
            emojiTextView.setText(emoji);
            emojiTextView.setTextSize(TypedValue.COMPLEX_UNIT_SP, 24);
            emojiTextView.setOnClickListener(v -> handleEmojiClick(emoji));

            emojiContainer.addView(emojiTextView);
        }
    }
    private String getEmojiByUnicode(int unicode) {
        return new String(Character.toChars(unicode));
    }
    private void handleEmojiClick(String emoji) {
        editText.append(emoji);
    }
    public boolean isShowing() {
        return popupWindow.isShowing();
    }

    public void showAtLocation(View parent, int gravity, int x, int y) {
        popupWindow.showAtLocation(parent, gravity, x, y);
    }
    public void dismiss() {
        popupWindow.dismiss();
    }
}
