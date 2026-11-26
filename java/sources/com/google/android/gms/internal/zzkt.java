package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzkt extends zzee implements zzks {
    public zzkt() {
        attachInterface(this, "com.google.android.gms.ads.internal.client.IVideoController");
    }

    public static zzks zzg(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IVideoController");
        return iInterfaceQueryLocalInterface instanceof zzks ? (zzks) iInterfaceQueryLocalInterface : new zzku(iBinder);
    }

    @Override // android.os.Binder
    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        zzkv zzkxVar;
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 1:
                play();
                parcel2.writeNoException();
                break;
            case 2:
                pause();
                parcel2.writeNoException();
                break;
            case 3:
                mute(zzef.zza(parcel));
                parcel2.writeNoException();
                break;
            case 4:
                boolean zIsMuted = isMuted();
                parcel2.writeNoException();
                zzef.zza(parcel2, zIsMuted);
                break;
            case 5:
                int playbackState = getPlaybackState();
                parcel2.writeNoException();
                parcel2.writeInt(playbackState);
                break;
            case 6:
                float fZzdv = zzdv();
                parcel2.writeNoException();
                parcel2.writeFloat(fZzdv);
                break;
            case 7:
                float fZzdw = zzdw();
                parcel2.writeNoException();
                parcel2.writeFloat(fZzdw);
                break;
            case 8:
                IBinder strongBinder = parcel.readStrongBinder();
                if (strongBinder == null) {
                    zzkxVar = null;
                } else {
                    IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IVideoLifecycleCallbacks");
                    zzkxVar = iInterfaceQueryLocalInterface instanceof zzkv ? (zzkv) iInterfaceQueryLocalInterface : new zzkx(strongBinder);
                }
                zza(zzkxVar);
                parcel2.writeNoException();
                break;
            case 9:
                float aspectRatio = getAspectRatio();
                parcel2.writeNoException();
                parcel2.writeFloat(aspectRatio);
                break;
            case 10:
                boolean zIsCustomControlsEnabled = isCustomControlsEnabled();
                parcel2.writeNoException();
                zzef.zza(parcel2, zIsCustomControlsEnabled);
                break;
            default:
                return false;
        }
        return true;
    }
}
