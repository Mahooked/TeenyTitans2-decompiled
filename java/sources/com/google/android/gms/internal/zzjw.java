package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.ads.formats.PublisherAdViewOptions;

/* JADX INFO: loaded from: classes.dex */
public final class zzjw extends zzed implements zzju {
    zzjw(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.ads.internal.client.IAdLoaderBuilder");
    }

    @Override // com.google.android.gms.internal.zzju
    public final void zza(PublisherAdViewOptions publisherAdViewOptions) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, publisherAdViewOptions);
        zzb(9, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzju
    public final void zza(zzon zzonVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzonVar);
        zzb(6, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzju
    public final void zza(zzpn zzpnVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzpnVar);
        zzb(3, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzju
    public final void zza(zzpq zzpqVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzpqVar);
        zzb(4, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzju
    public final void zza(zzpz zzpzVar, zziv zzivVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzpzVar);
        zzef.zza(parcelZzZ, zzivVar);
        zzb(8, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzju
    public final void zza(String str, zzpw zzpwVar, zzpt zzptVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        parcelZzZ.writeString(str);
        zzef.zza(parcelZzZ, zzpwVar);
        zzef.zza(parcelZzZ, zzptVar);
        zzb(5, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzju
    public final zzjr zzaZ() throws RemoteException {
        zzjr zzjtVar;
        Parcel parcelZza = zza(1, zzZ());
        IBinder strongBinder = parcelZza.readStrongBinder();
        if (strongBinder == null) {
            zzjtVar = null;
        } else {
            IInterface iInterfaceQueryLocalInterface = strongBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IAdLoader");
            zzjtVar = iInterfaceQueryLocalInterface instanceof zzjr ? (zzjr) iInterfaceQueryLocalInterface : new zzjt(strongBinder);
        }
        parcelZza.recycle();
        return zzjtVar;
    }

    @Override // com.google.android.gms.internal.zzju
    public final void zzb(zzjo zzjoVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzjoVar);
        zzb(2, parcelZzZ);
    }

    @Override // com.google.android.gms.internal.zzju
    public final void zzb(zzkk zzkkVar) throws RemoteException {
        Parcel parcelZzZ = zzZ();
        zzef.zza(parcelZzZ, zzkkVar);
        zzb(7, parcelZzZ);
    }
}
