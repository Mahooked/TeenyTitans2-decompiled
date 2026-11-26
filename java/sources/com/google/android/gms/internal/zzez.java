package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzez extends zzee implements zzey {
    public zzez() {
        attachInterface(this, "com.google.android.gms.ads.adshield.internal.IAdShieldClient");
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 1:
                String strZzaf = zzaf();
                parcel2.writeNoException();
                parcel2.writeString(strZzaf);
                return true;
            case 2:
                zzb(parcel.readString(), parcel.readString());
                parcel2.writeNoException();
                return true;
            case 3:
                boolean zZza = zza(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                parcel2.writeNoException();
                zzef.zza(parcel2, zZza);
                return true;
            case 4:
                boolean zZzb = zzb(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                parcel2.writeNoException();
                zzef.zza(parcel2, zZzb);
                return true;
            case 5:
                zzk(parcel.readString());
                parcel2.writeNoException();
                return true;
            case 6:
                IObjectWrapper iObjectWrapperZza = zza(IObjectWrapper.zza.zzM(parcel.readStrongBinder()), IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                parcel2.writeNoException();
                zzef.zza(parcel2, iObjectWrapperZza);
                return true;
            case 7:
                String strZzc = zzc(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                parcel2.writeNoException();
                parcel2.writeString(strZzc);
                return true;
            case 8:
                String strZza = zza(IObjectWrapper.zza.zzM(parcel.readStrongBinder()), parcel.readString());
                parcel2.writeNoException();
                parcel2.writeString(strZza);
                return true;
            case 9:
                zzd(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                parcel2.writeNoException();
                return true;
            case 10:
                IObjectWrapper iObjectWrapperZzb = zzb(IObjectWrapper.zza.zzM(parcel.readStrongBinder()), IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                parcel2.writeNoException();
                zzef.zza(parcel2, iObjectWrapperZzb);
                return true;
            case 11:
                boolean zZzb2 = zzb(parcel.readString(), zzef.zza(parcel));
                parcel2.writeNoException();
                zzef.zza(parcel2, zZzb2);
                return true;
            case 12:
                String strZza2 = zza(IObjectWrapper.zza.zzM(parcel.readStrongBinder()), parcel.createByteArray());
                parcel2.writeNoException();
                parcel2.writeString(strZza2);
                return true;
            default:
                return false;
        }
    }
}
