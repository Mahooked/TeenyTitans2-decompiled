package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzaeb extends zzee implements zzaea {
    public zzaeb() {
        attachInterface(this, "com.google.android.gms.ads.internal.reward.mediation.client.IMediationRewardedVideoAdListener");
    }

    public static zzaea zzx(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.reward.mediation.client.IMediationRewardedVideoAdListener");
        return iInterfaceQueryLocalInterface instanceof zzaea ? (zzaea) iInterfaceQueryLocalInterface : new zzaec(iBinder);
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 1:
                zzq(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                break;
            case 2:
                zzc(IObjectWrapper.zza.zzM(parcel.readStrongBinder()), parcel.readInt());
                break;
            case 3:
                zzr(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                break;
            case 4:
                zzs(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                break;
            case 5:
                zzt(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                break;
            case 6:
                zzu(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                break;
            case 7:
                zza(IObjectWrapper.zza.zzM(parcel.readStrongBinder()), (zzaee) zzef.zza(parcel, zzaee.CREATOR));
                break;
            case 8:
                zzv(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                break;
            case 9:
                zzd(IObjectWrapper.zza.zzM(parcel.readStrongBinder()), parcel.readInt());
                break;
            case 10:
                zzw(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                break;
            default:
                return false;
        }
        parcel2.writeNoException();
        return true;
    }
}
