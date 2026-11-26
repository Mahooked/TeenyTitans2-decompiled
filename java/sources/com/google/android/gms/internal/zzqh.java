package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;
import com.google.android.gms.ads.doubleclick.PublisherAdView;
import com.google.android.gms.ads.formats.OnPublisherAdViewLoadedListener;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzqh extends zzqa {
    private final OnPublisherAdViewLoadedListener zzIP;

    public zzqh(OnPublisherAdViewLoadedListener onPublisherAdViewLoadedListener) {
        this.zzIP = onPublisherAdViewLoadedListener;
    }

    @Override // com.google.android.gms.internal.zzpz
    public final void zza(zzjz zzjzVar, IObjectWrapper iObjectWrapper) {
        if (zzjzVar == null || iObjectWrapper == null) {
            return;
        }
        PublisherAdView publisherAdView = new PublisherAdView((Context) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper));
        try {
            if (zzjzVar.zzay() instanceof zzio) {
                zzio zzioVar = (zzio) zzjzVar.zzay();
                publisherAdView.setAdListener(zzioVar != null ? zzioVar.getAdListener() : null);
            }
        } catch (RemoteException e) {
            zzajc.zzc("Failed to get ad listener.", e);
        }
        try {
            if (zzjzVar.zzax() instanceof zzix) {
                zzix zzixVar = (zzix) zzjzVar.zzax();
                publisherAdView.setAppEventListener(zzixVar != null ? zzixVar.getAppEventListener() : null);
            }
        } catch (RemoteException e2) {
            zzajc.zzc("Failed to get app event listener.", e2);
        }
        zzaiy.zzaaH.post(new zzqi(this, publisherAdView, zzjzVar));
    }
}
