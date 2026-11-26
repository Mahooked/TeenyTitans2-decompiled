package com.google.android.gms.internal;

import android.content.Context;
import android.content.SharedPreferences;

/* JADX INFO: loaded from: classes.dex */
final class zzaga extends zzagr {
    private /* synthetic */ String zzZd;
    private /* synthetic */ long zzZe;
    private /* synthetic */ Context zztF;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzaga(Context context, String str, long j) {
        super(null);
        this.zztF = context;
        this.zzZd = str;
        this.zzZe = j;
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        SharedPreferences.Editor editorEdit = this.zztF.getSharedPreferences("admob", 0).edit();
        editorEdit.putString("app_settings_json", this.zzZd);
        editorEdit.putLong("app_settings_last_update_ms", this.zzZe);
        editorEdit.apply();
    }
}
