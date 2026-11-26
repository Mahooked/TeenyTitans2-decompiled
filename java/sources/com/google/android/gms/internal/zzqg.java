package com.google.android.gms.internal;

import com.google.android.gms.ads.formats.NativeCustomTemplateAd;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzqg extends zzpx {
    private final NativeCustomTemplateAd.OnCustomTemplateAdLoadedListener zzIO;

    public zzqg(NativeCustomTemplateAd.OnCustomTemplateAdLoadedListener onCustomTemplateAdLoadedListener) {
        this.zzIO = onCustomTemplateAdLoadedListener;
    }

    @Override // com.google.android.gms.internal.zzpw
    public final void zza(zzpj zzpjVar) {
        this.zzIO.onCustomTemplateAdLoaded(new zzpm(zzpjVar));
    }
}
