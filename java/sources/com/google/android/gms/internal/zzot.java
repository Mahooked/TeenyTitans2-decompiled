package com.google.android.gms.internal;

import android.net.Uri;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzot extends zzee implements zzos {
    public zzot() {
        attachInterface(this, "com.google.android.gms.ads.internal.formats.client.INativeAdImage");
    }

    public static zzos zzi(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.formats.client.INativeAdImage");
        return iInterfaceQueryLocalInterface instanceof zzos ? (zzos) iInterfaceQueryLocalInterface : new zzou(iBinder);
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 1:
                IObjectWrapper iObjectWrapperZzeg = zzeg();
                parcel2.writeNoException();
                zzef.zza(parcel2, iObjectWrapperZzeg);
                return true;
            case 2:
                Uri uri = getUri();
                parcel2.writeNoException();
                zzef.zzb(parcel2, uri);
                return true;
            case 3:
                double scale = getScale();
                parcel2.writeNoException();
                parcel2.writeDouble(scale);
                return true;
            default:
                return false;
        }
    }
}
