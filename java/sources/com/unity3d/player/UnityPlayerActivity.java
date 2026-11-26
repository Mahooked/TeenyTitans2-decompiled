package com.unity3d.player;

import android.app.Activity;
import android.app.Dialog;
import android.content.Intent;
import android.content.res.Configuration;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.MotionEvent;
import com.amazon.android.Kiwi;

/* JADX INFO: loaded from: classes.dex */
public class UnityPlayerActivity extends Activity {
    protected UnityPlayer mUnityPlayer;

    private void onCreateUnityPlayerActivity(Bundle bundle) {
        requestWindowFeature(1);
        super.onCreate(bundle);
        getWindow().setFormat(2);
        this.mUnityPlayer = new UnityPlayer(this);
        setContentView(this.mUnityPlayer);
        this.mUnityPlayer.requestFocus();
    }

    private void onDestroyUnityPlayerActivity() {
        this.mUnityPlayer.quit();
        super.onDestroy();
    }

    private void onPauseUnityPlayerActivity() {
        super.onPause();
        this.mUnityPlayer.pause();
    }

    private void onResumeUnityPlayerActivity() {
        super.onResume();
        this.mUnityPlayer.resume();
    }

    private void onStartUnityPlayerActivity() {
        super.onStart();
        this.mUnityPlayer.start();
    }

    private void onStopUnityPlayerActivity() {
        super.onStop();
        this.mUnityPlayer.stop();
    }

    @Override // android.app.Activity, android.view.Window.Callback
    public boolean dispatchKeyEvent(KeyEvent keyEvent) {
        return keyEvent.getAction() == 2 ? this.mUnityPlayer.injectEvent(keyEvent) : super.dispatchKeyEvent(keyEvent);
    }

    @Override // android.app.Activity
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (Kiwi.onActivityResult(this, requestCode, resultCode, data)) {
            return;
        }
        super.onActivityResult(requestCode, resultCode, data);
    }

    @Override // android.app.Activity, android.content.ComponentCallbacks
    public void onConfigurationChanged(Configuration configuration) {
        super.onConfigurationChanged(configuration);
        this.mUnityPlayer.configurationChanged(configuration);
    }

    @Override // android.app.Activity
    public void onCreate(Bundle bundle) {
        onCreateUnityPlayerActivity(bundle);
        Kiwi.onCreate((Activity) this, true);
    }

    @Override // android.app.Activity
    public Dialog onCreateDialog(int i) {
        Dialog dialog = Kiwi.onCreateDialog(this, i);
        return dialog != null ? dialog : super.onCreateDialog(i);
    }

    @Override // android.app.Activity
    public Dialog onCreateDialog(int i, Bundle bundle) {
        Dialog dialog = Kiwi.onCreateDialog(this, i);
        return dialog != null ? dialog : super.onCreateDialog(i, bundle);
    }

    @Override // android.app.Activity
    public void onDestroy() {
        onDestroyUnityPlayerActivity();
        Kiwi.onDestroy(this);
    }

    @Override // android.app.Activity
    public boolean onGenericMotionEvent(MotionEvent motionEvent) {
        return this.mUnityPlayer.injectEvent(motionEvent);
    }

    @Override // android.app.Activity, android.view.KeyEvent.Callback
    public boolean onKeyDown(int i, KeyEvent keyEvent) {
        return this.mUnityPlayer.injectEvent(keyEvent);
    }

    @Override // android.app.Activity, android.view.KeyEvent.Callback
    public boolean onKeyUp(int i, KeyEvent keyEvent) {
        return this.mUnityPlayer.injectEvent(keyEvent);
    }

    @Override // android.app.Activity, android.content.ComponentCallbacks
    public void onLowMemory() {
        super.onLowMemory();
        this.mUnityPlayer.lowMemory();
    }

    @Override // android.app.Activity
    protected void onNewIntent(Intent intent) {
        setIntent(intent);
    }

    @Override // android.app.Activity
    public void onPause() {
        onPauseUnityPlayerActivity();
        Kiwi.onPause(this);
    }

    @Override // android.app.Activity
    public void onResume() {
        onResumeUnityPlayerActivity();
        Kiwi.onResume(this);
    }

    @Override // android.app.Activity
    public void onStart() {
        onStartUnityPlayerActivity();
        Kiwi.onStart(this);
    }

    @Override // android.app.Activity
    public void onStop() {
        onStopUnityPlayerActivity();
        Kiwi.onStop(this);
    }

    @Override // android.app.Activity
    public boolean onTouchEvent(MotionEvent motionEvent) {
        return this.mUnityPlayer.injectEvent(motionEvent);
    }

    @Override // android.app.Activity, android.content.ComponentCallbacks2
    public void onTrimMemory(int i) {
        super.onTrimMemory(i);
        if (i == 15) {
            this.mUnityPlayer.lowMemory();
        }
    }

    @Override // android.app.Activity, android.view.Window.Callback
    public void onWindowFocusChanged(boolean z) {
        super.onWindowFocusChanged(z);
        this.mUnityPlayer.windowFocusChanged(z);
    }
}
