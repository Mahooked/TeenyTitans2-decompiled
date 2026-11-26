package com.google.android.gms.internal;

import android.content.Context;
import android.content.SharedPreferences;

/* JADX INFO: loaded from: classes.dex */
final class zzagc extends zzagr {
    private /* synthetic */ String zzZf;
    private /* synthetic */ Context zztF;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzagc(Context context, String str) {
        super(null);
        this.zztF = context;
        this.zzZf = str;
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        SharedPreferences.Editor editorEdit = this.zztF.getSharedPreferences("admob", 0).edit();
        editorEdit.putString("native_advanced_settings", this.zzZf);
        editorEdit.apply();
    }
}
