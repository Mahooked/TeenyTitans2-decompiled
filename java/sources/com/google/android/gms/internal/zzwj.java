package com.google.android.gms.internal;

import android.content.DialogInterface;

/* JADX INFO: loaded from: classes.dex */
final class zzwj implements DialogInterface.OnClickListener {
    private /* synthetic */ zzwh zzNx;

    zzwj(zzwh zzwhVar) {
        this.zzNx = zzwhVar;
    }

    @Override // android.content.DialogInterface.OnClickListener
    public final void onClick(DialogInterface dialogInterface, int i) {
        this.zzNx.zzan("Operation denied by user.");
    }
}
