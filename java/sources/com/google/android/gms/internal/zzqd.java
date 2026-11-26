package com.google.android.gms.internal;

import com.google.android.gms.ads.formats.NativeAppInstallAd;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzqd extends zzpo {
    private final NativeAppInstallAd.OnAppInstallAdLoadedListener zzIL;

    public zzqd(NativeAppInstallAd.OnAppInstallAdLoadedListener onAppInstallAdLoadedListener) {
        this.zzIL = onAppInstallAdLoadedListener;
    }

    @Override // com.google.android.gms.internal.zzpn
    public final void zza(zzpb zzpbVar) {
        this.zzIL.onAppInstallAdLoaded(new zzpe(zzpbVar));
    }
}
