package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.safetynet.SafeBrowsingData;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzcrx extends zzee implements zzcrw {
    public zzcrx() {
        attachInterface(this, "com.google.android.gms.safetynet.internal.ISafetyNetCallbacks");
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 1:
                zza((Status) zzef.zza(parcel, Status.CREATOR), (com.google.android.gms.safetynet.zza) zzef.zza(parcel, com.google.android.gms.safetynet.zza.CREATOR));
                break;
            case 2:
                zzeG(parcel.readString());
                break;
            case 3:
                zza((Status) zzef.zza(parcel, Status.CREATOR), (SafeBrowsingData) zzef.zza(parcel, SafeBrowsingData.CREATOR));
                break;
            case 4:
                zza((Status) zzef.zza(parcel, Status.CREATOR), zzef.zza(parcel));
                break;
            case 5:
            case 9:
            default:
                return false;
            case 6:
                zza((Status) zzef.zza(parcel, Status.CREATOR), (com.google.android.gms.safetynet.zzf) zzef.zza(parcel, com.google.android.gms.safetynet.zzf.CREATOR));
                break;
            case 7:
                zzb((Status) zzef.zza(parcel, Status.CREATOR), zzef.zza(parcel));
                break;
            case 8:
                zza((Status) zzef.zza(parcel, Status.CREATOR), (com.google.android.gms.safetynet.zzd) zzef.zza(parcel, com.google.android.gms.safetynet.zzd.CREATOR));
                break;
            case 10:
                zzc((Status) zzef.zza(parcel, Status.CREATOR), zzef.zza(parcel));
                break;
            case 11:
                zzF((Status) zzef.zza(parcel, Status.CREATOR));
                break;
            case 12:
                zzd((Status) zzef.zza(parcel, Status.CREATOR), zzef.zza(parcel));
                break;
        }
        return true;
    }
}
