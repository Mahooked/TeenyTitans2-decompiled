package com.google.android.gms.internal;

import android.content.Context;
import android.os.IBinder;
import android.os.IInterface;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzip extends com.google.android.gms.dynamic.zzp<zzjx> {
    public zzip() {
        super("com.google.android.gms.ads.AdLoaderBuilderCreatorImpl");
    }

    public final zzju zza(Context context, String str, zzuq zzuqVar) {
        zzju zzjwVar;
        try {
            IBinder iBinderZza = zzaS(context).zza(com.google.android.gms.dynamic.zzn.zzw(context), str, zzuqVar, 11020000);
            if (iBinderZza == null) {
                zzjwVar = null;
            } else {
                IInterface iInterfaceQueryLocalInterface = iBinderZza.queryLocalInterface("com.google.android.gms.ads.internal.client.IAdLoaderBuilder");
                zzjwVar = iInterfaceQueryLocalInterface instanceof zzju ? (zzju) iInterfaceQueryLocalInterface : new zzjw(iBinderZza);
            }
            return zzjwVar;
        } catch (RemoteException e) {
            zzajc.zzc("Could not create remote builder for AdLoader.", e);
            return null;
        } catch (com.google.android.gms.dynamic.zzq e2) {
            zzajc.zzc("Could not create remote builder for AdLoader.", e2);
            return null;
        }
    }

    @Override // com.google.android.gms.dynamic.zzp
    protected final /* synthetic */ zzjx zzb(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.client.IAdLoaderBuilderCreator");
        return iInterfaceQueryLocalInterface instanceof zzjx ? (zzjx) iInterfaceQueryLocalInterface : new zzjy(iBinder);
    }
}
