package com.google.android.gms.internal;

import android.os.RemoteException;
import android.view.View;
import com.google.android.gms.ads.doubleclick.CustomRenderedAd;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zznd implements CustomRenderedAd {
    private final zzne zzHh;

    public zznd(zzne zzneVar) {
        this.zzHh = zzneVar;
    }

    @Override // com.google.android.gms.ads.doubleclick.CustomRenderedAd
    public final String getBaseUrl() {
        try {
            return this.zzHh.zzdX();
        } catch (RemoteException e) {
            zzajc.zzc("Could not delegate getBaseURL to CustomRenderedAd", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.doubleclick.CustomRenderedAd
    public final String getContent() {
        try {
            return this.zzHh.getContent();
        } catch (RemoteException e) {
            zzajc.zzc("Could not delegate getContent to CustomRenderedAd", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.doubleclick.CustomRenderedAd
    public final void onAdRendered(View view) {
        try {
            this.zzHh.zzi(view != null ? com.google.android.gms.dynamic.zzn.zzw(view) : null);
        } catch (RemoteException e) {
            zzajc.zzc("Could not delegate onAdRendered to CustomRenderedAd", e);
        }
    }

    @Override // com.google.android.gms.ads.doubleclick.CustomRenderedAd
    public final void recordClick() {
        try {
            this.zzHh.recordClick();
        } catch (RemoteException e) {
            zzajc.zzc("Could not delegate recordClick to CustomRenderedAd", e);
        }
    }

    @Override // com.google.android.gms.ads.doubleclick.CustomRenderedAd
    public final void recordImpression() {
        try {
            this.zzHh.recordImpression();
        } catch (RemoteException e) {
            zzajc.zzc("Could not delegate recordImpression to CustomRenderedAd", e);
        }
    }
}
