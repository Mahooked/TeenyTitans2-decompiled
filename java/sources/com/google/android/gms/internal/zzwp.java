package com.google.android.gms.internal;

import android.content.DialogInterface;

/* JADX INFO: loaded from: classes.dex */
final class zzwp implements DialogInterface.OnClickListener {
    private /* synthetic */ zzwn zzNQ;

    zzwp(zzwn zzwnVar) {
        this.zzNQ = zzwnVar;
    }

    @Override // android.content.DialogInterface.OnClickListener
    public final void onClick(DialogInterface dialogInterface, int i) {
        this.zzNQ.zzan("User canceled the download.");
    }
}
