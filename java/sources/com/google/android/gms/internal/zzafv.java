package com.google.android.gms.internal;

import android.content.Context;
import android.content.SharedPreferences;

/* JADX INFO: loaded from: classes.dex */
final class zzafv extends zzagr {
    private /* synthetic */ String zzYZ;
    private /* synthetic */ Context zztF;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzafv(Context context, String str) {
        super(null);
        this.zztF = context;
        this.zzYZ = str;
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        SharedPreferences.Editor editorEdit = this.zztF.getSharedPreferences("admob", 0).edit();
        editorEdit.putString("content_vertical_hashes", this.zzYZ);
        editorEdit.apply();
    }
}
