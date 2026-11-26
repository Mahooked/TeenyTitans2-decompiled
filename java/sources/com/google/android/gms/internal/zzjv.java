package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.ads.formats.PublisherAdViewOptions;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzjv extends zzee implements zzju {
    public zzjv() {
        attachInterface(this, "com.google.android.gms.ads.internal.client.IAdLoaderBuilder");
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        zzkk zzkmVar = null;
        zzjo zzjqVar = null;
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 1:
                zzjr zzjrVarZzaZ = zzaZ();
                parcel2.writeNoException();
                zzef.zza(parcel2, zzjrVarZzaZ);
                break;
            case 2:
                IBinder strongBinder = parcel.readStrongBinder();
                if (strongBinder != null) {
                    IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IAdListener");
                    zzjqVar = iInterfaceQueryLocalInterface instanceof zzjo ? (zzjo) iInterfaceQueryLocalInterface : new zzjq(strongBinder);
                }
                zzb(zzjqVar);
                parcel2.writeNoException();
                break;
            case 3:
                zza(zzpo.zzl(parcel.readStrongBinder()));
                parcel2.writeNoException();
                break;
            case 4:
                zza(zzpr.zzm(parcel.readStrongBinder()));
                parcel2.writeNoException();
                break;
            case 5:
                zza(parcel.readString(), zzpx.zzo(parcel.readStrongBinder()), zzpu.zzn(parcel.readStrongBinder()));
                parcel2.writeNoException();
                break;
            case 6:
                zza((zzon) zzef.zza(parcel, zzon.CREATOR));
                parcel2.writeNoException();
                break;
            case 7:
                IBinder strongBinder2 = parcel.readStrongBinder();
                if (strongBinder2 != null) {
                    IInterface iInterfaceQueryLocalInterface2 = strongBinder2.queryLocalInterface("com.google.android.gms.ads.internal.client.ICorrelationIdProvider");
                    zzkmVar = iInterfaceQueryLocalInterface2 instanceof zzkk ? (zzkk) iInterfaceQueryLocalInterface2 : new zzkm(strongBinder2);
                }
                zzb(zzkmVar);
                parcel2.writeNoException();
                break;
            case 8:
                zza(zzqa.zzp(parcel.readStrongBinder()), (zziv) zzef.zza(parcel, zziv.CREATOR));
                parcel2.writeNoException();
                break;
            case 9:
                zza((PublisherAdViewOptions) zzef.zza(parcel, PublisherAdViewOptions.CREATOR));
                parcel2.writeNoException();
                break;
            default:
                return false;
        }
        return true;
    }
}
