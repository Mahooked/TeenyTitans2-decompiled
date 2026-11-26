package com.google.android.gms.internal;

import android.content.Context;
import android.content.SharedPreferences;

/* JADX INFO: loaded from: classes.dex */
final class zzafw extends zzagr {
    private /* synthetic */ boolean zzZa;
    private /* synthetic */ Context zztF;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzafw(Context context, boolean z) {
        super(null);
        this.zztF = context;
        this.zzZa = z;
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        SharedPreferences.Editor editorEdit = this.zztF.getSharedPreferences("admob", 0).edit();
        editorEdit.putBoolean("auto_collect_location", this.zzZa);
        editorEdit.apply();
    }
}
