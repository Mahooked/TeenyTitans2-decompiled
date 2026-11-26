package com.google.android.gms.flags.impl;

import android.content.SharedPreferences;
import java.util.concurrent.Callable;

/* JADX INFO: loaded from: classes.dex */
final class zzi implements Callable<String> {
    private /* synthetic */ SharedPreferences zzaXK;
    private /* synthetic */ String zzaXL;
    private /* synthetic */ String zzaXP;

    zzi(SharedPreferences sharedPreferences, String str, String str2) {
        this.zzaXK = sharedPreferences;
        this.zzaXL = str;
        this.zzaXP = str2;
    }

    @Override // java.util.concurrent.Callable
    public final /* synthetic */ String call() throws Exception {
        return this.zzaXK.getString(this.zzaXL, this.zzaXP);
    }
}
