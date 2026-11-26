package com.google.android.gms.internal;

import android.os.RemoteException;
import java.util.Random;

/* JADX INFO: loaded from: classes.dex */
final class zztj extends zzjp {
    private final zzjo zzKm;

    zztj(zzjo zzjoVar) {
        this.zzKm = zzjoVar;
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdClicked() throws RemoteException {
        this.zzKm.onAdClicked();
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdClosed() throws RemoteException {
        if (zzts.zzeX()) {
            int iIntValue = ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEg)).intValue();
            int iIntValue2 = ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEh)).intValue();
            if (iIntValue <= 0 || iIntValue2 < 0) {
                com.google.android.gms.ads.internal.zzbs.zzbN().zzeG();
            } else {
                zzagz.zzZr.postDelayed(zztk.zzKn, new Random().nextInt(iIntValue2 + 1) + iIntValue);
            }
        }
        this.zzKm.onAdClosed();
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdFailedToLoad(int i) throws RemoteException {
        this.zzKm.onAdFailedToLoad(i);
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdImpression() throws RemoteException {
        this.zzKm.onAdImpression();
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdLeftApplication() throws RemoteException {
        this.zzKm.onAdLeftApplication();
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdLoaded() throws RemoteException {
        this.zzKm.onAdLoaded();
    }

    @Override // com.google.android.gms.internal.zzjo
    public final void onAdOpened() throws RemoteException {
        this.zzKm.onAdOpened();
    }
}
