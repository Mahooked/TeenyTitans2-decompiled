package com.google.android.gms.internal;

import android.content.Context;
import android.content.SharedPreferences;

/* JADX INFO: loaded from: classes.dex */
final class zzagg extends zzagr {
    private /* synthetic */ long zzZg;
    private /* synthetic */ Context zztF;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzagg(Context context, long j) {
        super(null);
        this.zztF = context;
        this.zzZg = j;
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        SharedPreferences.Editor editorEdit = this.zztF.getSharedPreferences("admob", 0).edit();
        editorEdit.putLong("app_last_background_time_ms", this.zzZg);
        editorEdit.apply();
    }
}
