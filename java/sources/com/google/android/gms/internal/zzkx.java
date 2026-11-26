package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzkx extends zzed implements zzkv {
    zzkx(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.client.IVideoLifecycleCallbacks");
    }

    @Override // com.google.android.gms.internal.zzkv
    public final void onVideoEnd() throws RemoteException {
        zzb(4, zzZ());
    }

    @Override // com.google.android.gms.internal.zzkv
    public final void onVideoMute(boolean z) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, z);
        zzb(5, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzkv
    public final void onVideoPause() throws RemoteException {
        zzb(3, zzZ());
    }

    @Override // com.google.android.gms.internal.zzkv
    public final void onVideoPlay() throws RemoteException {
        zzb(2, zzZ());
    }

    @Override // com.google.android.gms.internal.zzkv
    public final void onVideoStart() throws RemoteException {
        zzb(1, zzZ());
    }
}
