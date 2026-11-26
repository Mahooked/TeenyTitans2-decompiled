package com.google.android.gms.internal;

import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import android.support.v4.view.MotionEventCompat;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzuu extends zzee implements zzut {
    public zzuu() {
        attachInterface(this, "com.google.android.gms.ads.internal.mediation.client.IMediationAdapter");
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        zzuw zzuyVar;
        zzuw zzuyVar2;
        zzuw zzuyVar3;
        zzuw zzuyVar4 = null;
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 1:
                IObjectWrapper iObjectWrapperZzM = IObjectWrapper.zza.zzM(parcel.readStrongBinder());
                zziv zzivVar = (zziv) zzef.zza(parcel, zziv.CREATOR);
                zzir zzirVar = (zzir) zzef.zza(parcel, zzir.CREATOR);
                String string = parcel.readString();
                IBinder strongBinder = parcel.readStrongBinder();
                if (strongBinder == null) {
                    zzuyVar3 = null;
                } else {
                    IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.mediation.client.IMediationAdapterListener");
                    zzuyVar3 = iInterfaceQueryLocalInterface instanceof zzuw ? (zzuw) iInterfaceQueryLocalInterface : new zzuy(strongBinder);
                }
                zza(iObjectWrapperZzM, zzivVar, zzirVar, string, zzuyVar3);
                parcel2.writeNoException();
                break;
            case 2:
                IObjectWrapper view = getView();
                parcel2.writeNoException();
                zzef.zza(parcel2, view);
                break;
            case 3:
                IObjectWrapper iObjectWrapperZzM2 = IObjectWrapper.zza.zzM(parcel.readStrongBinder());
                zzir zzirVar2 = (zzir) zzef.zza(parcel, zzir.CREATOR);
                String string2 = parcel.readString();
                IBinder strongBinder2 = parcel.readStrongBinder();
                if (strongBinder2 != null) {
                    IInterface iInterfaceQueryLocalInterface2 = strongBinder2.queryLocalInterface("com.google.android.gms.ads.internal.mediation.client.IMediationAdapterListener");
                    zzuyVar4 = iInterfaceQueryLocalInterface2 instanceof zzuw ? (zzuw) iInterfaceQueryLocalInterface2 : new zzuy(strongBinder2);
                }
                zza(iObjectWrapperZzM2, zzirVar2, string2, zzuyVar4);
                parcel2.writeNoException();
                break;
            case 4:
                showInterstitial();
                parcel2.writeNoException();
                break;
            case 5:
                destroy();
                parcel2.writeNoException();
                break;
            case 6:
                IObjectWrapper iObjectWrapperZzM3 = IObjectWrapper.zza.zzM(parcel.readStrongBinder());
                zziv zzivVar2 = (zziv) zzef.zza(parcel, zziv.CREATOR);
                zzir zzirVar3 = (zzir) zzef.zza(parcel, zzir.CREATOR);
                String string3 = parcel.readString();
                String string4 = parcel.readString();
                IBinder strongBinder3 = parcel.readStrongBinder();
                if (strongBinder3 != null) {
                    IInterface iInterfaceQueryLocalInterface3 = strongBinder3.queryLocalInterface("com.google.android.gms.ads.internal.mediation.client.IMediationAdapterListener");
                    zzuyVar4 = iInterfaceQueryLocalInterface3 instanceof zzuw ? (zzuw) iInterfaceQueryLocalInterface3 : new zzuy(strongBinder3);
                }
                zza(iObjectWrapperZzM3, zzivVar2, zzirVar3, string3, string4, zzuyVar4);
                parcel2.writeNoException();
                break;
            case 7:
                IObjectWrapper iObjectWrapperZzM4 = IObjectWrapper.zza.zzM(parcel.readStrongBinder());
                zzir zzirVar4 = (zzir) zzef.zza(parcel, zzir.CREATOR);
                String string5 = parcel.readString();
                String string6 = parcel.readString();
                IBinder strongBinder4 = parcel.readStrongBinder();
                if (strongBinder4 == null) {
                    zzuyVar2 = null;
                } else {
                    IInterface iInterfaceQueryLocalInterface4 = strongBinder4.queryLocalInterface("com.google.android.gms.ads.internal.mediation.client.IMediationAdapterListener");
                    zzuyVar2 = iInterfaceQueryLocalInterface4 instanceof zzuw ? (zzuw) iInterfaceQueryLocalInterface4 : new zzuy(strongBinder4);
                }
                zza(iObjectWrapperZzM4, zzirVar4, string5, string6, zzuyVar2);
                parcel2.writeNoException();
                break;
            case 8:
                pause();
                parcel2.writeNoException();
                break;
            case 9:
                resume();
                parcel2.writeNoException();
                break;
            case 10:
                zza(IObjectWrapper.zza.zzM(parcel.readStrongBinder()), (zzir) zzef.zza(parcel, zzir.CREATOR), parcel.readString(), zzaeb.zzx(parcel.readStrongBinder()), parcel.readString());
                parcel2.writeNoException();
                break;
            case 11:
                zzc((zzir) zzef.zza(parcel, zzir.CREATOR), parcel.readString());
                parcel2.writeNoException();
                break;
            case 12:
                showVideo();
                parcel2.writeNoException();
                break;
            case 13:
                boolean zIsInitialized = isInitialized();
                parcel2.writeNoException();
                zzef.zza(parcel2, zIsInitialized);
                break;
            case 14:
                IObjectWrapper iObjectWrapperZzM5 = IObjectWrapper.zza.zzM(parcel.readStrongBinder());
                zzir zzirVar5 = (zzir) zzef.zza(parcel, zzir.CREATOR);
                String string7 = parcel.readString();
                String string8 = parcel.readString();
                IBinder strongBinder5 = parcel.readStrongBinder();
                if (strongBinder5 == null) {
                    zzuyVar = null;
                } else {
                    IInterface iInterfaceQueryLocalInterface5 = strongBinder5.queryLocalInterface("com.google.android.gms.ads.internal.mediation.client.IMediationAdapterListener");
                    zzuyVar = iInterfaceQueryLocalInterface5 instanceof zzuw ? (zzuw) iInterfaceQueryLocalInterface5 : new zzuy(strongBinder5);
                }
                zza(iObjectWrapperZzM5, zzirVar5, string7, string8, zzuyVar, (zzon) zzef.zza(parcel, zzon.CREATOR), parcel.createStringArrayList());
                parcel2.writeNoException();
                break;
            case 15:
                zzvc zzvcVarZzfq = zzfq();
                parcel2.writeNoException();
                zzef.zza(parcel2, zzvcVarZzfq);
                break;
            case 16:
                zzvf zzvfVarZzfr = zzfr();
                parcel2.writeNoException();
                zzef.zza(parcel2, zzvfVarZzfr);
                break;
            case 17:
                Bundle bundleZzfs = zzfs();
                parcel2.writeNoException();
                zzef.zzb(parcel2, bundleZzfs);
                break;
            case 18:
                Bundle interstitialAdapterInfo = getInterstitialAdapterInfo();
                parcel2.writeNoException();
                zzef.zzb(parcel2, interstitialAdapterInfo);
                break;
            case 19:
                Bundle bundleZzft = zzft();
                parcel2.writeNoException();
                zzef.zzb(parcel2, bundleZzft);
                break;
            case 20:
                zza((zzir) zzef.zza(parcel, zzir.CREATOR), parcel.readString(), parcel.readString());
                parcel2.writeNoException();
                break;
            case 21:
                zzk(IObjectWrapper.zza.zzM(parcel.readStrongBinder()));
                parcel2.writeNoException();
                break;
            case MotionEventCompat.AXIS_GAS /* 22 */:
                boolean zZzfu = zzfu();
                parcel2.writeNoException();
                zzef.zza(parcel2, zZzfu);
                break;
            case MotionEventCompat.AXIS_BRAKE /* 23 */:
                zza(IObjectWrapper.zza.zzM(parcel.readStrongBinder()), zzaeb.zzx(parcel.readStrongBinder()), parcel.createStringArrayList());
                parcel2.writeNoException();
                break;
            case MotionEventCompat.AXIS_DISTANCE /* 24 */:
                zzpj zzpjVarZzfv = zzfv();
                parcel2.writeNoException();
                zzef.zza(parcel2, zzpjVarZzfv);
                break;
            case 25:
                setImmersiveMode(zzef.zza(parcel));
                parcel2.writeNoException();
                break;
            case 26:
                zzks videoController = getVideoController();
                parcel2.writeNoException();
                zzef.zza(parcel2, videoController);
                break;
            default:
                return false;
        }
        return true;
    }
}
