package com.google.android.gms.internal;

import android.content.Context;

/* JADX INFO: loaded from: classes.dex */
final class zzahc implements zzajb {
    private /* synthetic */ String zzZz;
    private /* synthetic */ Context zztF;

    zzahc(zzagz zzagzVar, Context context, String str) {
        this.zztF = context;
        this.zzZz = str;
    }

    @Override // com.google.android.gms.internal.zzajb
    public final void zzaN(String str) {
        com.google.android.gms.ads.internal.zzbs.zzbz();
        zzagz.zzd(this.zztF, this.zzZz, str);
    }
}
