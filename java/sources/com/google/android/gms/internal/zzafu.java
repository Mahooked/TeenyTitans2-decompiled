package com.google.android.gms.internal;

import android.content.Context;
import android.content.SharedPreferences;

/* JADX INFO: loaded from: classes.dex */
final class zzafu extends zzagr {
    private /* synthetic */ boolean zzYY;
    private /* synthetic */ Context zztF;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzafu(Context context, boolean z) {
        super(null);
        this.zztF = context;
        this.zzYY = z;
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        SharedPreferences.Editor editorEdit = this.zztF.getSharedPreferences("admob", 0).edit();
        editorEdit.putBoolean("use_https", this.zzYY);
        editorEdit.apply();
    }
}
