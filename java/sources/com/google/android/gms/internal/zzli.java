package com.google.android.gms.internal;

import android.content.Context;
import android.os.IBinder;
import android.os.IInterface;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzli extends com.google.android.gms.dynamic.zzp<zzkq> {
    public zzli() {
        super("com.google.android.gms.ads.MobileAdsSettingManagerCreatorImpl");
    }

    @Override // com.google.android.gms.dynamic.zzp
    protected final /* synthetic */ zzkq zzb(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IMobileAdsSettingManagerCreator");
        return iInterfaceQueryLocalInterface instanceof zzkq ? (zzkq) iInterfaceQueryLocalInterface : new zzkr(iBinder);
    }

    public final zzkn zzh(Context context) {
        zzkn zzkpVar;
        try {
            IBinder iBinderZza = zzaS(context).zza(com.google.android.gms.dynamic.zzn.zzw(context), 11020000);
            if (iBinderZza == null) {
                zzkpVar = null;
            } else {
                IInterface iInterfaceQueryLocalInterface = iBinderZza.queryLocalInterface("com.google.android.gms.ads.internal.client.IMobileAdsSettingManager");
                zzkpVar = iInterfaceQueryLocalInterface instanceof zzkn ? (zzkn) iInterfaceQueryLocalInterface : new zzkp(iBinderZza);
            }
            return zzkpVar;
        } catch (RemoteException e) {
            zzajc.zzc("Could not get remote MobileAdsSettingManager.", e);
            return null;
        } catch (com.google.android.gms.dynamic.zzq e2) {
            zzajc.zzc("Could not get remote MobileAdsSettingManager.", e2);
            return null;
        }
    }
}
