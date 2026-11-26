package com.google.firebase.iid;

import android.app.Service;
import android.content.Intent;
import android.os.Binder;
import android.os.IBinder;
import android.support.annotation.VisibleForTesting;
import android.support.v4.content.WakefulBroadcastReceiver;
import android.util.Log;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzb extends Service {
    private Binder zzckd;
    private int zzcke;

    @VisibleForTesting
    final ExecutorService zzbrV = Executors.newSingleThreadExecutor();
    private final Object mLock = new Object();
    private int zzckf = 0;

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzm(Intent intent) {
        if (intent != null) {
            WakefulBroadcastReceiver.completeWakefulIntent(intent);
        }
        synchronized (this.mLock) {
            this.zzckf--;
            if (this.zzckf == 0) {
                stopSelfResult(this.zzcke);
            }
        }
    }

    public abstract void handleIntent(Intent intent);

    @Override // android.app.Service
    public final synchronized IBinder onBind(Intent intent) {
        if (Log.isLoggable("EnhancedIntentService", 3)) {
            Log.d("EnhancedIntentService", "Service received bind request");
        }
        if (this.zzckd == null) {
            this.zzckd = new zzf(this);
        }
        return this.zzckd;
    }

    @Override // android.app.Service
    public final int onStartCommand(Intent intent, int i, int i2) {
        synchronized (this.mLock) {
            this.zzcke = i2;
            this.zzckf++;
        }
        Intent intentZzn = zzn(intent);
        if (intentZzn == null) {
            zzm(intent);
            return 2;
        }
        if (zzo(intentZzn)) {
            zzm(intent);
            return 2;
        }
        this.zzbrV.execute(new zzc(this, intentZzn, intent));
        return 3;
    }

    protected Intent zzn(Intent intent) {
        return intent;
    }

    public boolean zzo(Intent intent) {
        return false;
    }
}
