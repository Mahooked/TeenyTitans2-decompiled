package com.google.android.gms.internal;

import android.content.Context;
import android.content.SharedPreferences;

/* JADX INFO: loaded from: classes.dex */
final class zzafy extends zzagr {
    private /* synthetic */ int zzZc;
    private /* synthetic */ Context zztF;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzafy(Context context, int i) {
        super(null);
        this.zztF = context;
        this.zzZc = i;
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        SharedPreferences.Editor editorEdit = this.zztF.getSharedPreferences("admob", 0).edit();
        editorEdit.putInt("version_code", this.zzZc);
        editorEdit.apply();
    }
}
