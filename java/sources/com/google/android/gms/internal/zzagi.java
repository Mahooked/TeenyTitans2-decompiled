package com.google.android.gms.internal;

import android.content.Context;
import android.content.SharedPreferences;

/* JADX INFO: loaded from: classes.dex */
final class zzagi extends zzagr {
    private /* synthetic */ int zzZh;
    private /* synthetic */ Context zztF;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzagi(Context context, int i) {
        super(null);
        this.zztF = context;
        this.zzZh = i;
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        SharedPreferences.Editor editorEdit = this.zztF.getSharedPreferences("admob", 0).edit();
        editorEdit.putInt("request_in_session_count", this.zzZh);
        editorEdit.apply();
    }
}
