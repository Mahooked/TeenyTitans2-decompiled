package com.google.android.gms.internal;

import android.content.Context;
import android.os.IBinder;
import android.os.IInterface;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zziq extends com.google.android.gms.dynamic.zzp<zzkc> {
    public zziq() {
        super("com.google.android.gms.ads.AdManagerCreatorImpl");
    }

    public final zzjz zza(Context context, zziv zzivVar, String str, zzuq zzuqVar, int i) {
        zzjz zzkbVar;
        try {
            IBinder iBinderZza = zzaS(context).zza(com.google.android.gms.dynamic.zzn.zzw(context), zzivVar, str, zzuqVar, 11020000, i);
            if (iBinderZza == null) {
                zzkbVar = null;
            } else {
                IInterface iInterfaceQueryLocalInterface = iBinderZza.queryLocalInterface("com.google.android.gms.ads.internal.client.IAdManager");
                zzkbVar = iInterfaceQueryLocalInterface instanceof zzjz ? (zzjz) iInterfaceQueryLocalInterface : new zzkb(iBinderZza);
            }
            return zzkbVar;
        } catch (RemoteException | com.google.android.gms.dynamic.zzq e) {
            zzajc.zza("Could not create remote AdManager.", e);
            return null;
        }
    }

    @Override // com.google.android.gms.dynamic.zzp
    protected final /* synthetic */ zzkc zzb(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IAdManagerCreator");
        return iInterfaceQueryLocalInterface instanceof zzkc ? (zzkc) iInterfaceQueryLocalInterface : new zzkd(iBinder);
    }
}
