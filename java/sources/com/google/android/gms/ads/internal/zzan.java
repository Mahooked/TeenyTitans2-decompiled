package com.google.android.gms.ads.internal;

import com.google.android.gms.ads.internal.overlay.AdOverlayInfoParcel;
import com.google.android.gms.internal.zzafp;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@zzzn
final class zzan extends zzafp {
    final /* synthetic */ zzal zzun;
    private final int zzuo;

    public zzan(zzal zzalVar, int i) {
        this.zzun = zzalVar;
        this.zzuo = i;
    }

    @Override // com.google.android.gms.internal.zzafp
    public void onStop() {
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        zzap zzapVar = new zzap(this.zzun.zzsP.zzur, this.zzun.zzba(), this.zzun.zzuh, this.zzun.zzui, this.zzun.zzsP.zzur ? this.zzuo : -1, this.zzun.zzuj);
        int requestedOrientation = this.zzun.zzsP.zzvY.zzPg.getRequestedOrientation();
        if (requestedOrientation == -1) {
            requestedOrientation = this.zzun.zzsP.zzvY.orientation;
        }
        zzagz.zzZr.post(new zzao(this, new AdOverlayInfoParcel(this.zzun, this.zzun, this.zzun, this.zzun.zzsP.zzvY.zzPg, requestedOrientation, this.zzun.zzsP.zzvT, this.zzun.zzsP.zzvY.zzTt, zzapVar)));
    }
}
