package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzpr extends zzee implements zzpq {
    public zzpr() {
        attachInterface(this, "com.google.android.gms.ads.internal.formats.client.IOnContentAdLoadedListener");
    }

    public static zzpq zzm(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.formats.client.IOnContentAdLoadedListener");
        return iInterfaceQueryLocalInterface instanceof zzpq ? (zzpq) iInterfaceQueryLocalInterface : new zzps(iBinder);
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        zzpf zzphVar;
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        if (i != 1) {
            return false;
        }
        IBinder strongBinder = parcel.readStrongBinder();
        if (strongBinder == null) {
            zzphVar = null;
        } else {
            IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.formats.client.INativeContentAd");
            zzphVar = iInterfaceQueryLocalInterface instanceof zzpf ? (zzpf) iInterfaceQueryLocalInterface : new zzph(strongBinder);
        }
        zza(zzphVar);
        parcel2.writeNoException();
        return true;
    }
}
