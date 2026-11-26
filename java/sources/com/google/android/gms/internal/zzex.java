package com.google.android.gms.internal;

import android.content.Context;
import android.os.IBinder;
import android.os.IInterface;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzex extends com.google.android.gms.dynamic.zzp<zzfb> {
    private static final zzex zzsm = new zzex();

    private zzex() {
        super("com.google.android.gms.ads.adshield.AdShieldCreatorImpl");
    }

    public static zzey zzb(String str, Context context, boolean z) {
        zzey zzeyVarZzc;
        return (com.google.android.gms.common.zze.zzoW().isGooglePlayServicesAvailable(context) != 0 || (zzeyVarZzc = zzsm.zzc(str, context, false)) == null) ? new zzew(str, context, false) : zzeyVarZzc;
    }

    private final zzey zzc(String str, Context context, boolean z) {
        IObjectWrapper iObjectWrapperZzw = com.google.android.gms.dynamic.zzn.zzw(context);
        try {
            IBinder iBinderZza = z ? zzaS(context).zza(str, iObjectWrapperZzw) : zzaS(context).zzb(str, iObjectWrapperZzw);
            if (iBinderZza == null) {
                return null;
            }
            IInterface iInterfaceQueryLocalInterface = iBinderZza.queryLocalInterface("com.google.android.gms.ads.adshield.internal.IAdShieldClient");
            return iInterfaceQueryLocalInterface instanceof zzey ? (zzey) iInterfaceQueryLocalInterface : new zzfa(iBinderZza);
        } catch (RemoteException | com.google.android.gms.dynamic.zzq e) {
            return null;
        }
    }

    @Override // com.google.android.gms.dynamic.zzp
    protected final /* synthetic */ zzfb zzb(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.adshield.internal.IAdShieldCreator");
        return iInterfaceQueryLocalInterface instanceof zzfb ? (zzfb) iInterfaceQueryLocalInterface : new zzfc(iBinder);
    }
}
