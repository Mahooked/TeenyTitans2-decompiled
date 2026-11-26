package com.google.android.gms.internal;

import android.content.Intent;
import android.os.Bundle;
import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzwz extends zzed implements zzwx {
    zzwz(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.overlay.client.IAdOverlay");
    }

    @Override // com.google.android.gms.internal.zzwx
    public void onActivityResult(int i, int i2, Intent intent) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeInt(i);
        parcelZzZ.writeInt(i2);
        zzef.zza(parcelZzZ, intent);
        zzb(12, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzwx
    public final void onBackPressed() throws RemoteException {
        zzb(10, zzZ());
    }

    @Override // com.google.android.gms.internal.zzwx
    public void onCreate(Bundle bundle) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, bundle);
        zzb(1, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzwx
    public void onDestroy() throws RemoteException {
        zzb(8, zzZ());
    }

    @Override // com.google.android.gms.internal.zzwx
    public void onPause() throws RemoteException {
        zzb(5, zzZ());
    }

    @Override // com.google.android.gms.internal.zzwx
    public final void onRestart() throws RemoteException {
        zzb(2, zzZ());
    }

    @Override // com.google.android.gms.internal.zzwx
    public void onResume() throws RemoteException {
        zzb(4, zzZ());
    }

    @Override // com.google.android.gms.internal.zzwx
    public final void onSaveInstanceState(Bundle bundle) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, bundle);
        Parcel parcelZza = zza(6, parcelZzZ);
        if (parcelZza.readInt() != 0) {
            bundle.readFromParcel(parcelZza);
        }
        parcelZza.recycle();
    }

    @Override // com.google.android.gms.internal.zzwx
    public void onStart() throws RemoteException {
        zzb(3, zzZ());
    }

    @Override // com.google.android.gms.internal.zzwx
    public void onStop() throws RemoteException {
        zzb(7, zzZ());
    }

    @Override // com.google.android.gms.internal.zzwx
    public final void zzaa() throws RemoteException {
        zzb(9, zzZ());
    }

    @Override // com.google.android.gms.internal.zzwx
    public final boolean zzfK() throws RemoteException {
        Parcel parcelZza = zza(11, zzZ());
        boolean zZza = zzef.zza(parcelZza);
        parcelZza.recycle();
        return zZza;
    }

    @Override // com.google.android.gms.internal.zzwx
    public final void zzo(IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, iObjectWrapper);
        zzb(13, parcelZzZ);
    }
}
