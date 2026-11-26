package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzpu extends zzee implements zzpt {
    public zzpu() {
        attachInterface(this, "com.google.android.gms.ads.internal.formats.client.IOnCustomClickListener");
    }

    public static zzpt zzn(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.formats.client.IOnCustomClickListener");
        return iInterfaceQueryLocalInterface instanceof zzpt ? (zzpt) iInterfaceQueryLocalInterface : new zzpv(iBinder);
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        zzpj zzplVar;
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        if (i != 1) {
            return false;
        }
        IBinder strongBinder = parcel.readStrongBinder();
        if (strongBinder == null) {
            zzplVar = null;
        } else {
            IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.formats.client.INativeCustomTemplateAd");
            zzplVar = iInterfaceQueryLocalInterface instanceof zzpj ? (zzpj) iInterfaceQueryLocalInterface : new zzpl(strongBinder);
        }
        zzb(zzplVar, parcel.readString());
        parcel2.writeNoException();
        return true;
    }
}
