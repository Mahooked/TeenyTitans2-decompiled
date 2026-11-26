package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzku extends zzed implements zzks {
    zzku(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.client.IVideoController");
    }

    @Override // com.google.android.gms.internal.zzks
    public final float getAspectRatio() throws RemoteException {
        Parcel parcelZza = zza(9, zzZ());
        float f = parcelZza.readFloat();
        parcelZza.recycle();
        return f;
    }

    @Override // com.google.android.gms.internal.zzks
    public final int getPlaybackState() throws RemoteException {
        Parcel parcelZza = zza(5, zzZ());
        int i = parcelZza.readInt();
        parcelZza.recycle();
        return i;
    }

    @Override // com.google.android.gms.internal.zzks
    public final boolean isCustomControlsEnabled() throws RemoteException {
        Parcel parcelZza = zza(10, zzZ());
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }

    @Override // com.google.android.gms.internal.zzks
    public final boolean isMuted() throws RemoteException {
        Parcel parcelZza = zza(4, zzZ());
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }

    @Override // com.google.android.gms.internal.zzks
    public final void mute(boolean z) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, z);
        zzb(3, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzks
    public final void pause() throws RemoteException {
        zzb(2, zzZ());
    }

    @Override // com.google.android.gms.internal.zzks
    public final void play() throws RemoteException {
        zzb(1, zzZ());
    }

    @Override // com.google.android.gms.internal.zzks
    public final void zza(zzkv zzkvVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzkvVar);
        zzb(8, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzks
    public final float zzdv() throws RemoteException {
        Parcel parcelZza = zza(6, zzZ());
        float f = parcelZza.readFloat();
        parcelZza.recycle();
        return f;
    }

    @Override // com.google.android.gms.internal.zzks
    public final float zzdw() throws RemoteException {
        Parcel parcelZza = zza(7, zzZ());
        float f = parcelZza.readFloat();
        parcelZza.recycle();
        return f;
    }
}
