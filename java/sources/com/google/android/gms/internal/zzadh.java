package com.google.android.gms.internal;

import android.content.Context;
import android.os.IBinder;
import android.os.IInterface;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzadh extends com.google.android.gms.dynamic.zzp<zzadb> {
    public zzadh() {
        super("com.google.android.gms.ads.reward.RewardedVideoAdCreatorImpl");
    }

    public final zzacy zza(Context context, zzuq zzuqVar) {
        zzacy zzadaVar;
        try {
            IBinder iBinderZza = zzaS(context).zza(com.google.android.gms.dynamic.zzn.zzw(context), zzuqVar, 11020000);
            if (iBinderZza == null) {
                zzadaVar = null;
            } else {
                IInterface iInterfaceQueryLocalInterface = iBinderZza.queryLocalInterface("com.google.android.gms.ads.internal.reward.client.IRewardedVideoAd");
                zzadaVar = iInterfaceQueryLocalInterface instanceof zzacy ? (zzacy) iInterfaceQueryLocalInterface : new zzada(iBinderZza);
            }
            return zzadaVar;
        } catch (RemoteException | com.google.android.gms.dynamic.zzq e) {
            zzajc.zzc("Could not get remote RewardedVideoAd.", e);
            return null;
        }
    }

    @Override // com.google.android.gms.dynamic.zzp
    protected final /* synthetic */ zzadb zzb(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.reward.client.IRewardedVideoAdCreator");
        return iInterfaceQueryLocalInterface instanceof zzadb ? (zzadb) iInterfaceQueryLocalInterface : new zzadc(iBinder);
    }
}
