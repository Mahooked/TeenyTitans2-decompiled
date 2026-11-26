package com.google.android.gms.flags.impl;

import android.content.SharedPreferences;
import java.util.concurrent.Callable;

/* JADX INFO: loaded from: classes.dex */
final class zzg implements Callable<Long> {
    private /* synthetic */ SharedPreferences zzaXK;
    private /* synthetic */ String zzaXL;
    private /* synthetic */ Long zzaXO;

    zzg(SharedPreferences sharedPreferences, String str, Long l) {
        this.zzaXK = sharedPreferences;
        this.zzaXL = str;
        this.zzaXO = l;
    }

    @Override // java.util.concurrent.Callable
    public final /* synthetic */ Long call() throws Exception {
        return Long.valueOf(this.zzaXK.getLong(this.zzaXL, this.zzaXO.longValue()));
    }
}
