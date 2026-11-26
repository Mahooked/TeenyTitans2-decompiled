package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzpk extends zzee implements zzpj {
    public zzpk() {
        attachInterface(this, "com.google.android.gms.ads.internal.formats.client.INativeCustomTemplateAd");
    }

    public static zzpj zzk(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.formats.client.INativeCustomTemplateAd");
        return iInterfaceQueryLocalInterface instanceof zzpj ? (zzpj) iInterfaceQueryLocalInterface : new zzpl(iBinder);
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 1:
                String strZzP = zzP(parcel.readString());
                parcel2.writeNoException();
                parcel2.writeString(strZzP);
                return true;
            case 2:
                zzos zzosVarZzQ = zzQ(parcel.readString());
                parcel2.writeNoException();
                zzef.zza(parcel2, zzosVarZzQ);
                return true;
            case 3:
                List<String> availableAssetNames = getAvailableAssetNames();
                parcel2.writeNoException();
                parcel2.writeStringList(availableAssetNames);
                return true;
            case 4:
                String customTemplateId = getCustomTemplateId();
                parcel2.writeNoException();
                parcel2.writeString(customTemplateId);
                return true;
            case 5:
                performClick(parcel.readString());
                parcel2.writeNoException();
                return true;
            case 6:
                recordImpression();
                parcel2.writeNoException();
                return true;
            case 7:
                zzks videoController = getVideoController();
                parcel2.writeNoException();
                zzef.zza(parcel2, videoController);
                return true;
            case 8:
                destroy();
                parcel2.writeNoException();
                return true;
            case 9:
                IObjectWrapper iObjectWrapperZzen = zzen();
                parcel2.writeNoException();
                zzef.zza(parcel2, iObjectWrapperZzen);
                return true;
            case 10:
                boolean zZzj = zzj(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                parcel2.writeNoException();
                zzef.zza(parcel2, zZzj);
                return true;
            case 11:
                IObjectWrapper iObjectWrapperZzei = zzei();
                parcel2.writeNoException();
                zzef.zza(parcel2, iObjectWrapperZzei);
                return true;
            default:
                return false;
        }
    }
}
