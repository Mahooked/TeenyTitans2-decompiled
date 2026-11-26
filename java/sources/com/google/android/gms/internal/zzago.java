package com.google.android.gms.internal;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;

/* JADX INFO: loaded from: classes.dex */
final class zzago extends zzagr {
    private /* synthetic */ zzags zzZb;
    private /* synthetic */ Context zztF;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzago(Context context, zzags zzagsVar) {
        super(null);
        this.zztF = context;
        this.zzZb = zzagsVar;
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        SharedPreferences sharedPreferences = this.zztF.getSharedPreferences("admob", 0);
        Bundle bundle = new Bundle();
        bundle.putBoolean("content_url_opted_out", sharedPreferences.getBoolean("content_url_opted_out", true));
        if (this.zzZb != null) {
            this.zzZb.zzf(bundle);
        }
    }
}
