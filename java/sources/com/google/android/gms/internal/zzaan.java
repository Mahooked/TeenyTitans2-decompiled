package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzaan extends zzee implements zzaam {
    public zzaan() {
        attachInterface(this, "com.google.android.gms.ads.internal.request.IAdRequestService");
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        zzaas zzaatVar = null;
        zzaap zzaarVar = null;
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 1:
                zzaai zzaaiVarZzc = zzc((zzaae) zzef.zza(parcel, zzaae.CREATOR));
                parcel2.writeNoException();
                zzef.zzb(parcel2, zzaaiVarZzc);
                break;
            case 2:
                zzaae zzaaeVar = (zzaae) zzef.zza(parcel, zzaae.CREATOR);
                IBinder strongBinder = parcel.readStrongBinder();
                if (strongBinder != null) {
                    IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.request.IAdResponseListener");
                    zzaarVar = iInterfaceQueryLocalInterface instanceof zzaap ? (zzaap) iInterfaceQueryLocalInterface : new zzaar(strongBinder);
                }
                zza(zzaaeVar, zzaarVar);
                parcel2.writeNoException();
                break;
            case 3:
                zzaax zzaaxVar = (zzaax) zzef.zza(parcel, zzaax.CREATOR);
                IBinder strongBinder2 = parcel.readStrongBinder();
                if (strongBinder2 != null) {
                    IInterface iInterfaceQueryLocalInterface2 = strongBinder2.queryLocalInterface("com.google.android.gms.ads.internal.request.INonagonResponseListener");
                    zzaatVar = iInterfaceQueryLocalInterface2 instanceof zzaas ? (zzaas) iInterfaceQueryLocalInterface2 : new zzaat(strongBinder2);
                }
                zza(zzaaxVar, zzaatVar);
                parcel2.writeNoException();
                break;
            default:
                return false;
        }
        return true;
    }
}
