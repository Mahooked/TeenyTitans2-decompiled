package jp.ne.donuts.uniclipboard;

import android.app.Activity;
import android.content.ClipData;
import android.content.ClipDescription;
import android.content.ClipboardManager;
import com.unity3d.player.UnityPlayer;
import java.util.concurrent.Callable;
import java.util.concurrent.FutureTask;

/* JADX INFO: loaded from: classes.dex */
public class Clipboard {
    public static void setText(final String text) {
        final Activity activity = UnityPlayer.currentActivity;
        activity.runOnUiThread(new Runnable() { // from class: jp.ne.donuts.uniclipboard.Clipboard.1
            @Override // java.lang.Runnable
            public void run() {
                ClipData.Item item = new ClipData.Item(text);
                String[] mimeType = {"text/uri-list"};
                ClipData cd = new ClipData(new ClipDescription("text_data", mimeType), item);
                ClipboardManager cm = (ClipboardManager) activity.getSystemService("clipboard");
                cm.setPrimaryClip(cd);
            }
        });
    }

    public static String getText() {
        final Activity activity = UnityPlayer.currentActivity;
        FutureTask<String> futureResult = new FutureTask<>(new Callable<String>() { // from class: jp.ne.donuts.uniclipboard.Clipboard.2
            @Override // java.util.concurrent.Callable
            public String call() throws Exception {
                ClipboardManager cm = (ClipboardManager) activity.getSystemService("clipboard");
                ClipData cd = cm.getPrimaryClip();
                return cd != null ? cd.getItemAt(0).getText().toString() : "";
            }
        });
        activity.runOnUiThread(futureResult);
        try {
            return futureResult.get();
        } catch (Exception e) {
            return "";
        }
    }
}
