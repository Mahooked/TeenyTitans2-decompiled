package com.google.firebase.iid;

import android.util.Log;

/* JADX INFO: loaded from: classes.dex */
final class zzg implements Runnable {
    private /* synthetic */ zzd zzckn;
    private /* synthetic */ zzf zzcko;

    zzg(zzf zzfVar, zzd zzdVar) {
        this.zzcko = zzfVar;
        this.zzckn = zzdVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (Log.isLoggable("EnhancedIntentService", 3)) {
            Log.d("EnhancedIntentService", "bg processing of the intent starting now");
        }
        this.zzcko.zzckm.handleIntent(this.zzckn.intent);
        this.zzckn.finish();
    }
}
