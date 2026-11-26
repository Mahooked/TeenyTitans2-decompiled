package com.google.android.gms.internal;

import com.google.android.gms.ads.formats.NativeContentAd;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzqe extends zzpr {
    private final NativeContentAd.OnContentAdLoadedListener zzIM;

    public zzqe(NativeContentAd.OnContentAdLoadedListener onContentAdLoadedListener) {
        this.zzIM = onContentAdLoadedListener;
    }

    @Override // com.google.android.gms.internal.zzpq
    public final void zza(zzpf zzpfVar) {
        this.zzIM.onContentAdLoaded(new zzpi(zzpfVar));
    }
}
