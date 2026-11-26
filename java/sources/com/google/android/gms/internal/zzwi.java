package com.google.android.gms.internal;

import android.content.DialogInterface;
import android.content.Intent;

/* JADX INFO: loaded from: classes.dex */
final class zzwi implements DialogInterface.OnClickListener {
    private /* synthetic */ zzwh zzNx;

    zzwi(zzwh zzwhVar) {
        this.zzNx = zzwhVar;
    }

    @Override // android.content.DialogInterface.OnClickListener
    public final void onClick(DialogInterface dialogInterface, int i) {
        Intent intentCreateIntent = this.zzNx.createIntent();
        com.google.android.gms.ads.internal.zzbs.zzbz();
        zzagz.zzb(this.zzNx.mContext, intentCreateIntent);
    }
}
