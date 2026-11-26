package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzade extends zzee implements zzadd {
    public zzade() {
        attachInterface(this, "com.google.android.gms.ads.internal.reward.client.IRewardedVideoAdListener");
    }

    public static zzadd zzw(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.reward.client.IRewardedVideoAdListener");
        return iInterfaceQueryLocalInterface instanceof zzadd ? (zzadd) iInterfaceQueryLocalInterface : new zzadf(iBinder);
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        zzacv zzacxVar;
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 1:
                onRewardedVideoAdLoaded();
                break;
            case 2:
                onRewardedVideoAdOpened();
                break;
            case 3:
                onRewardedVideoStarted();
                break;
            case 4:
                onRewardedVideoAdClosed();
                break;
            case 5:
                IBinder strongBinder = parcel.readStrongBinder();
                if (strongBinder == null) {
                    zzacxVar = null;
                } else {
                    IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.reward.client.IRewardItem");
                    zzacxVar = iInterfaceQueryLocalInterface instanceof zzacv ? (zzacv) iInterfaceQueryLocalInterface : new zzacx(strongBinder);
                }
                zza(zzacxVar);
                break;
            case 6:
                onRewardedVideoAdLeftApplication();
                break;
            case 7:
                onRewardedVideoAdFailedToLoad(parcel.readInt());
                break;
            default:
                return false;
        }
        parcel2.writeNoException();
        return true;
    }
}
