package com.google.android.gms.internal;

import com.google.android.gms.ads.formats.NativeCustomTemplateAd;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzqf extends zzpu {
    private final NativeCustomTemplateAd.OnCustomClickListener zzIN;

    public zzqf(NativeCustomTemplateAd.OnCustomClickListener onCustomClickListener) {
        this.zzIN = onCustomClickListener;
    }

    @Override // com.google.android.gms.internal.zzpt
    public final void zzb(zzpj zzpjVar, String str) {
        this.zzIN.onCustomClick(new zzpm(zzpjVar), str);
    }
}
