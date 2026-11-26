package com.google.android.gms.internal;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

/* JADX INFO: loaded from: classes.dex */
final class zzgg extends BroadcastReceiver {
    private /* synthetic */ zzge zzxG;

    zzgg(zzge zzgeVar) {
        this.zzxG = zzgeVar;
    }

    @Override // android.content.BroadcastReceiver
    public final void onReceive(Context context, Intent intent) {
        this.zzxG.zzh(3);
    }
}
