package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import android.support.v4.view.MotionEventCompat;
import com.comscore.utils.Constants;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzka extends zzee implements zzjz {
    public zzka() {
        attachInterface(this, "com.google.android.gms.ads.internal.client.IAdManager");
    }

    public static zzjz zze(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IAdManager");
        return iInterfaceQueryLocalInterface instanceof zzjz ? (zzjz) iInterfaceQueryLocalInterface : new zzkb(iBinder);
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        zzkk zzkmVar = null;
        zzjo zzjqVar = null;
        zzke zzkgVar = null;
        zzjl zzjnVar = null;
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 1:
                IObjectWrapper iObjectWrapperZzal = zzal();
                parcel2.writeNoException();
                zzef.zza(parcel2, iObjectWrapperZzal);
                break;
            case 2:
                destroy();
                parcel2.writeNoException();
                break;
            case 3:
                boolean zIsReady = isReady();
                parcel2.writeNoException();
                zzef.zza(parcel2, zIsReady);
                break;
            case 4:
                boolean zZza = zza((zzir) zzef.zza(parcel, zzir.CREATOR));
                parcel2.writeNoException();
                zzef.zza(parcel2, zZza);
                break;
            case 5:
                pause();
                parcel2.writeNoException();
                break;
            case 6:
                resume();
                parcel2.writeNoException();
                break;
            case 7:
                IBinder strongBinder = parcel.readStrongBinder();
                if (strongBinder != null) {
                    IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IAdListener");
                    zzjqVar = iInterfaceQueryLocalInterface instanceof zzjo ? (zzjo) iInterfaceQueryLocalInterface : new zzjq(strongBinder);
                }
                zza(zzjqVar);
                parcel2.writeNoException();
                break;
            case 8:
                IBinder strongBinder2 = parcel.readStrongBinder();
                if (strongBinder2 != null) {
                    IInterface iInterfaceQueryLocalInterface2 = strongBinder2.queryLocalInterface("com.google.android.gms.ads.internal.client.IAppEventListener");
                    zzkgVar = iInterfaceQueryLocalInterface2 instanceof zzke ? (zzke) iInterfaceQueryLocalInterface2 : new zzkg(strongBinder2);
                }
                zza(zzkgVar);
                parcel2.writeNoException();
                break;
            case 9:
                showInterstitial();
                parcel2.writeNoException();
                break;
            case 10:
                stopLoading();
                parcel2.writeNoException();
                break;
            case 11:
                zzao();
                parcel2.writeNoException();
                break;
            case 12:
                zziv zzivVarZzam = zzam();
                parcel2.writeNoException();
                zzef.zzb(parcel2, zzivVarZzam);
                break;
            case 13:
                zza((zziv) zzef.zza(parcel, zziv.CREATOR));
                parcel2.writeNoException();
                break;
            case 14:
                zza(zzxh.zzs(parcel.readStrongBinder()));
                parcel2.writeNoException();
                break;
            case 15:
                zza(zzxp.zzu(parcel.readStrongBinder()), parcel.readString());
                parcel2.writeNoException();
                break;
            case 16:
            case 17:
            case MotionEventCompat.AXIS_RELATIVE_X /* 27 */:
            case MotionEventCompat.AXIS_RELATIVE_Y /* 28 */:
            default:
                return false;
            case 18:
                String mediationAdapterClassName = getMediationAdapterClassName();
                parcel2.writeNoException();
                parcel2.writeString(mediationAdapterClassName);
                break;
            case 19:
                zza(zzni.zzh(parcel.readStrongBinder()));
                parcel2.writeNoException();
                break;
            case 20:
                IBinder strongBinder3 = parcel.readStrongBinder();
                if (strongBinder3 != null) {
                    IInterface iInterfaceQueryLocalInterface3 = strongBinder3.queryLocalInterface("com.google.android.gms.ads.internal.client.IAdClickListener");
                    zzjnVar = iInterfaceQueryLocalInterface3 instanceof zzjl ? (zzjl) iInterfaceQueryLocalInterface3 : new zzjn(strongBinder3);
                }
                zza(zzjnVar);
                parcel2.writeNoException();
                break;
            case 21:
                IBinder strongBinder4 = parcel.readStrongBinder();
                if (strongBinder4 != null) {
                    IInterface iInterfaceQueryLocalInterface4 = strongBinder4.queryLocalInterface("com.google.android.gms.ads.internal.client.ICorrelationIdProvider");
                    zzkmVar = iInterfaceQueryLocalInterface4 instanceof zzkk ? (zzkk) iInterfaceQueryLocalInterface4 : new zzkm(strongBinder4);
                }
                zza(zzkmVar);
                parcel2.writeNoException();
                break;
            case MotionEventCompat.AXIS_GAS /* 22 */:
                setManualImpressionsEnabled(zzef.zza(parcel));
                parcel2.writeNoException();
                break;
            case MotionEventCompat.AXIS_BRAKE /* 23 */:
                boolean zIsLoading = isLoading();
                parcel2.writeNoException();
                zzef.zza(parcel2, zIsLoading);
                break;
            case MotionEventCompat.AXIS_DISTANCE /* 24 */:
                zza(zzade.zzw(parcel.readStrongBinder()));
                parcel2.writeNoException();
                break;
            case 25:
                setUserId(parcel.readString());
                parcel2.writeNoException();
                break;
            case 26:
                zzks videoController = getVideoController();
                parcel2.writeNoException();
                zzef.zza(parcel2, videoController);
                break;
            case 29:
                zza((zzlx) zzef.zza(parcel, zzlx.CREATOR));
                parcel2.writeNoException();
                break;
            case 30:
                zza((zzky) zzef.zza(parcel, zzky.CREATOR));
                parcel2.writeNoException();
                break;
            case Constants.CACHE_EXPIRY_DAYS /* 31 */:
                String adUnitId = getAdUnitId();
                parcel2.writeNoException();
                parcel2.writeString(adUnitId);
                break;
            case 32:
                zzke zzkeVarZzax = zzax();
                parcel2.writeNoException();
                zzef.zza(parcel2, zzkeVarZzax);
                break;
            case 33:
                zzjo zzjoVarZzay = zzay();
                parcel2.writeNoException();
                zzef.zza(parcel2, zzjoVarZzay);
                break;
            case MotionEventCompat.AXIS_GENERIC_3 /* 34 */:
                setImmersiveMode(zzef.zza(parcel));
                parcel2.writeNoException();
                break;
            case MotionEventCompat.AXIS_GENERIC_4 /* 35 */:
                String strZzaI = zzaI();
                parcel2.writeNoException();
                parcel2.writeString(strZzaI);
                break;
        }
        return true;
    }
}
