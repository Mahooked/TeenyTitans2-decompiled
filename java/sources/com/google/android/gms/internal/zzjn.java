package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzjn extends zzed implements zzjl {
    zzjn(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.client.IAdClickListener");
    }

    @Override // com.google.android.gms.internal.zzjl
    public final void onAdClicked() throws RemoteException {
        zzb(1, zzZ());
    }
}
