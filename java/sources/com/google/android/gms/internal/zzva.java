package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzva extends zzee implements zzuz {
    public zzva() {
        attachInterface(this, "com.google.android.gms.ads.internal.mediation.client.IMediationResponseMetadata");
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        if (i != 1) {
            return false;
        }
        int iZzfo = zzfo();
        parcel2.writeNoException();
        parcel2.writeInt(iZzfo);
        return true;
    }
}
