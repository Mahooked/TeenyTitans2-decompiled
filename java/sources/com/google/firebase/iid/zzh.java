package com.google.firebase.iid;

import android.content.BroadcastReceiver;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.IBinder;
import android.support.annotation.VisibleForTesting;
import android.util.Log;
import com.google.android.gms.common.stats.zza;
import java.util.LinkedList;
import java.util.Queue;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledThreadPoolExecutor;

/* JADX INFO: loaded from: classes.dex */
public final class zzh implements ServiceConnection {
    private final Intent zzckp;
    private final ScheduledExecutorService zzckq;
    private final Queue<zzd> zzckr;
    private zzf zzcks;
    private boolean zzckt;
    private final Context zzqD;

    public zzh(Context context, String str) {
        this(context, str, new ScheduledThreadPoolExecutor(0));
    }

    @VisibleForTesting
    private zzh(Context context, String str, ScheduledExecutorService scheduledExecutorService) {
        this.zzckr = new LinkedList();
        this.zzckt = false;
        this.zzqD = context.getApplicationContext();
        this.zzckp = new Intent(str).setPackage(this.zzqD.getPackageName());
        this.zzckq = scheduledExecutorService;
    }

    private final synchronized void zzJO() {
        if (Log.isLoggable("EnhancedIntentService", 3)) {
            Log.d("EnhancedIntentService", "flush queue called");
        }
        while (!this.zzckr.isEmpty()) {
            if (Log.isLoggable("EnhancedIntentService", 3)) {
                Log.d("EnhancedIntentService", "found intent to be delivered");
            }
            if (this.zzcks == null || !this.zzcks.isBinderAlive()) {
                if (Log.isLoggable("EnhancedIntentService", 3)) {
                    Log.d("EnhancedIntentService", new StringBuilder(39).append("binder is dead. start connection? ").append(!this.zzckt).toString());
                }
                if (this.zzckt) {
                    break;
                }
                this.zzckt = true;
                try {
                    if (!zza.zzrU().zza(this.zzqD, this.zzckp, this, 65)) {
                        Log.e("EnhancedIntentService", "binding to the service failed");
                        while (!this.zzckr.isEmpty()) {
                            this.zzckr.poll().finish();
                        }
                        break;
                    }
                    break;
                } catch (SecurityException e) {
                    Log.e("EnhancedIntentService", "Exception while binding the service", e);
                }
            } else {
                if (Log.isLoggable("EnhancedIntentService", 3)) {
                    Log.d("EnhancedIntentService", "binder is alive, sending the intent.");
                }
                this.zzcks.zza(this.zzckr.poll());
            }
        }
    }

    @Override // android.content.ServiceConnection
    public final void onServiceConnected(ComponentName componentName, IBinder iBinder) {
        synchronized (this) {
            this.zzckt = false;
            this.zzcks = (zzf) iBinder;
            if (Log.isLoggable("EnhancedIntentService", 3)) {
                String strValueOf = String.valueOf(componentName);
                Log.d("EnhancedIntentService", new StringBuilder(String.valueOf(strValueOf).length() + 20).append("onServiceConnected: ").append(strValueOf).toString());
            }
            zzJO();
        }
    }

    @Override // android.content.ServiceConnection
    public final void onServiceDisconnected(ComponentName componentName) {
        if (Log.isLoggable("EnhancedIntentService", 3)) {
            String strValueOf = String.valueOf(componentName);
            Log.d("EnhancedIntentService", new StringBuilder(String.valueOf(strValueOf).length() + 23).append("onServiceDisconnected: ").append(strValueOf).toString());
        }
        zzJO();
    }

    public final synchronized void zza(Intent intent, BroadcastReceiver.PendingResult pendingResult) {
        if (Log.isLoggable("EnhancedIntentService", 3)) {
            Log.d("EnhancedIntentService", "new intent queued in the bind-strategy delivery");
        }
        this.zzckr.add(new zzd(intent, pendingResult, this.zzckq));
        zzJO();
    }
}
