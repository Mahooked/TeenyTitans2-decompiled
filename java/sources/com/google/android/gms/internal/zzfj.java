package com.google.android.gms.internal;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

/* JADX INFO: loaded from: classes.dex */
final class zzfj extends BroadcastReceiver {
    private /* synthetic */ zzfi zzxh;

    zzfj(zzfi zzfiVar) {
        this.zzxh = zzfiVar;
    }

    @Override // android.content.BroadcastReceiver
    public final void onReceive(Context context, Intent intent) {
        this.zzxh.zzg(3);
    }
}
