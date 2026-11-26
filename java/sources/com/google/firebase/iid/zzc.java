package com.google.firebase.iid;

import android.content.Intent;

/* JADX INFO: loaded from: classes.dex */
final class zzc implements Runnable {
    private /* synthetic */ Intent val$intent;
    private /* synthetic */ Intent zzckg;
    private /* synthetic */ zzb zzckh;

    zzc(zzb zzbVar, Intent intent, Intent intent2) {
        this.zzckh = zzbVar;
        this.val$intent = intent;
        this.zzckg = intent2;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzckh.handleIntent(this.val$intent);
        this.zzckh.zzm(this.zzckg);
    }
}
