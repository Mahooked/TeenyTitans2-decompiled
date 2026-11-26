package com.google.android.gms.internal;

import android.os.Bundle;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzpc extends zzee implements zzpb {
    public zzpc() {
        attachInterface(this, "com.google.android.gms.ads.internal.formats.client.INativeAppInstallAd");
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 2:
                IObjectWrapper iObjectWrapperZzei = zzei();
                parcel2.writeNoException();
                zzef.zza(parcel2, iObjectWrapperZzei);
                break;
            case 3:
                String headline = getHeadline();
                parcel2.writeNoException();
                parcel2.writeString(headline);
                break;
            case 4:
                List images = getImages();
                parcel2.writeNoException();
                parcel2.writeList(images);
                break;
            case 5:
                String body = getBody();
                parcel2.writeNoException();
                parcel2.writeString(body);
                break;
            case 6:
                zzos zzosVarZzeh = zzeh();
                parcel2.writeNoException();
                zzef.zza(parcel2, zzosVarZzeh);
                break;
            case 7:
                String callToAction = getCallToAction();
                parcel2.writeNoException();
                parcel2.writeString(callToAction);
                break;
            case 8:
                double starRating = getStarRating();
                parcel2.writeNoException();
                parcel2.writeDouble(starRating);
                break;
            case 9:
                String store = getStore();
                parcel2.writeNoException();
                parcel2.writeString(store);
                break;
            case 10:
                String price = getPrice();
                parcel2.writeNoException();
                parcel2.writeString(price);
                break;
            case 11:
                Bundle extras = getExtras();
                parcel2.writeNoException();
                zzef.zzb(parcel2, extras);
                break;
            case 12:
                destroy();
                parcel2.writeNoException();
                break;
            case 13:
                zzks videoController = getVideoController();
                parcel2.writeNoException();
                zzef.zza(parcel2, videoController);
                break;
            case 14:
                zzc((Bundle) zzef.zza(parcel, Bundle.CREATOR));
                parcel2.writeNoException();
                break;
            case 15:
                boolean zZzd = zzd((Bundle) zzef.zza(parcel, Bundle.CREATOR));
                parcel2.writeNoException();
                zzef.zza(parcel2, zZzd);
                break;
            case 16:
                zze((Bundle) zzef.zza(parcel, Bundle.CREATOR));
                parcel2.writeNoException();
                break;
            default:
                return false;
        }
        return true;
    }
}
