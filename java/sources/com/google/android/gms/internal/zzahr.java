package com.google.android.gms.internal;

import android.content.DialogInterface;

/* JADX INFO: loaded from: classes.dex */
final class zzahr implements DialogInterface.OnClickListener {
    private /* synthetic */ int zzZF;
    private /* synthetic */ int zzZG;
    private /* synthetic */ int zzZH;
    private /* synthetic */ zzahq zzZI;

    zzahr(zzahq zzahqVar, int i, int i2, int i3) {
        this.zzZI = zzahqVar;
        this.zzZF = i;
        this.zzZG = i2;
        this.zzZH = i3;
    }

    @Override // android.content.DialogInterface.OnClickListener
    public final void onClick(DialogInterface dialogInterface, int i) {
        if (i == this.zzZF) {
            this.zzZI.zzhY();
            return;
        }
        if (i == this.zzZG) {
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGr)).booleanValue()) {
                this.zzZI.zzhZ();
                return;
            }
        }
        if (i == this.zzZH) {
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGs)).booleanValue()) {
                this.zzZI.zzia();
            }
        }
    }
}
