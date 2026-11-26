package com.google.android.gms.internal;

import android.content.Context;
import android.content.SharedPreferences;

/* JADX INFO: loaded from: classes.dex */
final class zzagk extends zzagr {
    private /* synthetic */ long zzZi;
    private /* synthetic */ Context zztF;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzagk(Context context, long j) {
        super(null);
        this.zztF = context;
        this.zzZi = j;
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        SharedPreferences.Editor editorEdit = this.zztF.getSharedPreferences("admob", 0).edit();
        editorEdit.putLong("first_ad_req_time_ms", this.zzZi);
        editorEdit.apply();
    }
}
