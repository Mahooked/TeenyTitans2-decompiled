package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import android.support.v4.view.MotionEventCompat;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzacz extends zzee implements zzacy {
    public zzacz() {
        attachInterface(this, "com.google.android.gms.ads.internal.reward.client.IRewardedVideoAd");
    }

    public static zzacy zzv(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.reward.client.IRewardedVideoAd");
        return iInterfaceQueryLocalInterface instanceof zzacy ? (zzacy) iInterfaceQueryLocalInterface : new zzada(iBinder);
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        zzadd zzadfVar;
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 1:
                zza((zzadj) zzef.zza(parcel, zzadj.CREATOR));
                parcel2.writeNoException();
                break;
            case 2:
                show();
                parcel2.writeNoException();
                break;
            case 3:
                IBinder strongBinder = parcel.readStrongBinder();
                if (strongBinder == null) {
                    zzadfVar = null;
                } else {
                    IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.reward.client.IRewardedVideoAdListener");
                    zzadfVar = iInterfaceQueryLocalInterface instanceof zzadd ? (zzadd) iInterfaceQueryLocalInterface : new zzadf(strongBinder);
                }
                zza(zzadfVar);
                parcel2.writeNoException();
                break;
            case 4:
                setUserId(parcel.readString());
                parcel2.writeNoException();
                break;
            case 5:
                boolean zIsLoaded = isLoaded();
                parcel2.writeNoException();
                zzef.zza(parcel2, zIsLoaded);
                break;
            case 6:
                pause();
                parcel2.writeNoException();
                break;
            case 7:
                resume();
                parcel2.writeNoException();
                break;
            case 8:
                destroy();
                parcel2.writeNoException();
                break;
            case 9:
                zzf(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                parcel2.writeNoException();
                break;
            case 10:
                zzg(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                parcel2.writeNoException();
                break;
            case 11:
                zzh(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                parcel2.writeNoException();
                break;
            case 12:
                String mediationAdapterClassName = getMediationAdapterClassName();
                parcel2.writeNoException();
                parcel2.writeString(mediationAdapterClassName);
                break;
            case MotionEventCompat.AXIS_GENERIC_3 /* 34 */:
                setImmersiveMode(zzef.zza(parcel));
                parcel2.writeNoException();
                break;
            default:
                return false;
        }
        return true;
    }
}
