package com.google.android.gms.internal;

import android.content.Context;
import android.content.SharedPreferences;

/* JADX INFO: loaded from: classes.dex */
final class zzagf extends zzagr {
    private /* synthetic */ Context zztF;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzagf(Context context) {
        super(null);
        this.zztF = context;
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        SharedPreferences.Editor editorEdit = this.zztF.getSharedPreferences("admob", 0).edit();
        editorEdit.remove("native_advanced_settings");
        editorEdit.apply();
    }
}
