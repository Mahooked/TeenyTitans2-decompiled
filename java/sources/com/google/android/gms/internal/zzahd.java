package com.google.android.gms.internal;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

/* JADX INFO: loaded from: classes.dex */
final class zzahd extends BroadcastReceiver {
    private /* synthetic */ zzagz zzZy;

    private zzahd(zzagz zzagzVar) {
        this.zzZy = zzagzVar;
    }

    /* synthetic */ zzahd(zzagz zzagzVar, zzaha zzahaVar) {
        this(zzagzVar);
    }

    @Override // android.content.BroadcastReceiver
    public final void onReceive(Context context, Intent intent) {
        if ("android.intent.action.USER_PRESENT".equals(intent.getAction())) {
            this.zzZy.zzZu = true;
        } else if ("android.intent.action.SCREEN_OFF".equals(intent.getAction())) {
            this.zzZy.zzZu = false;
        }
    }
}
