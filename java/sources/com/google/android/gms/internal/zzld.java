package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzld extends zzjk {
    private /* synthetic */ zzlc zzBk;

    zzld(zzlc zzlcVar) {
        this.zzBk = zzlcVar;
    }

    @Override // com.google.android.gms.internal.zzjk, com.google.android.gms.ads.AdListener
    public final void onAdFailedToLoad(int i) {
        this.zzBk.zzBd.zza(this.zzBk.zzae());
        super.onAdFailedToLoad(i);
    }

    @Override // com.google.android.gms.internal.zzjk, com.google.android.gms.ads.AdListener
    public final void onAdLoaded() {
        this.zzBk.zzBd.zza(this.zzBk.zzae());
        super.onAdLoaded();
    }
}
