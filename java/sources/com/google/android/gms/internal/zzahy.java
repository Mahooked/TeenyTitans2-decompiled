package com.google.android.gms.internal;

import android.content.DialogInterface;
import android.net.Uri;

/* JADX INFO: loaded from: classes.dex */
final class zzahy implements DialogInterface.OnClickListener {
    private /* synthetic */ zzahx zzZR;

    zzahy(zzahx zzahxVar) {
        this.zzZR = zzahxVar;
    }

    @Override // android.content.DialogInterface.OnClickListener
    public final void onClick(DialogInterface dialogInterface, int i) {
        com.google.android.gms.ads.internal.zzbs.zzbz();
        zzagz.zza(this.zzZR.zztF, Uri.parse("https://support.google.com/dfp_premium/answer/7160685#push"));
    }
}
