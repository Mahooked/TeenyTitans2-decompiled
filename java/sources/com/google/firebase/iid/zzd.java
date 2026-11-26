package com.google.firebase.iid;

import android.content.BroadcastReceiver;
import android.content.Intent;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;

/* JADX INFO: loaded from: classes.dex */
final class zzd {
    final Intent intent;
    private final BroadcastReceiver.PendingResult zzcki;
    private boolean zzckj = false;
    private final ScheduledFuture<?> zzckk;

    zzd(Intent intent, BroadcastReceiver.PendingResult pendingResult, ScheduledExecutorService scheduledExecutorService) {
        this.intent = intent;
        this.zzcki = pendingResult;
        this.zzckk = scheduledExecutorService.schedule(new zze(this, intent), 9500L, TimeUnit.MILLISECONDS);
    }

    final synchronized void finish() {
        if (!this.zzckj) {
            this.zzcki.finish();
            this.zzckk.cancel(false);
            this.zzckj = true;
        }
    }
}
