package com.google.android.gms.internal;

import com.google.android.gms.ads.doubleclick.OnCustomRenderedAdLoadedListener;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zznk extends zzni {
    private final OnCustomRenderedAdLoadedListener zzBh;

    public zznk(OnCustomRenderedAdLoadedListener onCustomRenderedAdLoadedListener) {
        this.zzBh = onCustomRenderedAdLoadedListener;
    }

    @Override // com.google.android.gms.internal.zznh
    public final void zza(zzne zzneVar) {
        this.zzBh.onCustomRenderedAdLoaded(new zznd(zzneVar));
    }
}
