package com.google.android.gms.internal;

import android.content.DialogInterface;
import android.content.Intent;

/* JADX INFO: loaded from: classes.dex */
final class zzahs implements DialogInterface.OnClickListener {
    private /* synthetic */ zzahq zzZI;
    private /* synthetic */ String zzZJ;

    zzahs(zzahq zzahqVar, String str) {
        this.zzZI = zzahqVar;
        this.zzZJ = str;
    }

    @Override // android.content.DialogInterface.OnClickListener
    public final void onClick(DialogInterface dialogInterface, int i) {
        com.google.android.gms.ads.internal.zzbs.zzbz();
        zzagz.zzb(this.zzZI.mContext, Intent.createChooser(new Intent("android.intent.action.SEND").setType("text/plain").putExtra("android.intent.extra.TEXT", this.zzZJ), "Share via"));
    }
}
