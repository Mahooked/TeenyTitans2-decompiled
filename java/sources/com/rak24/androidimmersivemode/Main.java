package com.rak24.androidimmersivemode;

import android.annotation.TargetApi;
import android.app.Activity;
import android.app.Application;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.view.View;
import com.unity3d.player.UnityPlayer;

/* JADX INFO: loaded from: classes.dex */
@TargetApi(21)
public class Main {
    static BroadcastReceiver br;
    public static boolean gamePaused;
    static Application unityApp;
    static Main _INSTANCE = null;
    static int lastHeightDiff = 0;

    public Main() {
        _INSTANCE = this;
    }

    public static Main instance() {
        return _INSTANCE == null ? new Main() : _INSTANCE;
    }

    public void EnableImmersiveMode(Context currentContext) {
        if (Build.VERSION.SDK_INT >= 19) {
            Activity activity = (Activity) currentContext;
            ImmersiveMode(activity.getWindow().getDecorView());
            SetUIChangeListener(activity.getWindow().getDecorView());
            unityApp = UnityPlayer.currentActivity.getApplication();
            unityApp.registerActivityLifecycleCallbacks(new Application.ActivityLifecycleCallbacks() { // from class: com.rak24.androidimmersivemode.Main.1
                @Override // android.app.Application.ActivityLifecycleCallbacks
                public void onActivityResumed(Activity activity2) {
                    Main.ImmersiveMode(activity2.getWindow().getDecorView());
                }

                @Override // android.app.Application.ActivityLifecycleCallbacks
                public void onActivityCreated(Activity activity2, Bundle savedInstanceState) {
                }

                @Override // android.app.Application.ActivityLifecycleCallbacks
                public void onActivityDestroyed(Activity activity2) {
                }

                @Override // android.app.Application.ActivityLifecycleCallbacks
                public void onActivityPaused(Activity activity2) {
                }

                @Override // android.app.Application.ActivityLifecycleCallbacks
                public void onActivitySaveInstanceState(Activity activity2, Bundle outState) {
                }

                @Override // android.app.Application.ActivityLifecycleCallbacks
                public void onActivityStarted(Activity activity2) {
                }

                @Override // android.app.Application.ActivityLifecycleCallbacks
                public void onActivityStopped(Activity activity2) {
                }
            });
        }
    }

    public static void ImmersiveMode(View v) {
        if (Build.VERSION.SDK_INT >= 19) {
            v.setSystemUiVisibility(7686);
        }
    }

    public static void ImmersiveModeFromCache(final Activity activity) {
        if (Build.VERSION.SDK_INT >= 19) {
            Handler h = new Handler();
            h.postDelayed(new Runnable() { // from class: com.rak24.androidimmersivemode.Main.2
                @Override // java.lang.Runnable
                public void run() {
                    Main.ImmersiveMode(activity.getWindow().getDecorView());
                }
            }, 700L);
        }
    }

    public static void EnableAppPin(Activity unityActivity) {
        if (Build.VERSION.SDK_INT >= 21) {
            unityActivity.startLockTask();
        }
    }

    public static void DisableAppPin(Activity unityActivity) {
        if (Build.VERSION.SDK_INT >= 21) {
            unityActivity.stopLockTask();
        }
    }

    void SetUIChangeListener(final View v) {
        v.setOnSystemUiVisibilityChangeListener(new View.OnSystemUiVisibilityChangeListener() { // from class: com.rak24.androidimmersivemode.Main.3
            @Override // android.view.View.OnSystemUiVisibilityChangeListener
            public void onSystemUiVisibilityChange(int visibility) {
                if ((visibility & 4) == 0) {
                    Main.ImmersiveMode(v);
                }
            }
        });
    }
}
