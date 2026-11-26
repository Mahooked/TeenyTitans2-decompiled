package com.google.android.gms.internal;

import android.app.Activity;
import android.os.IBinder;
import android.os.IInterface;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzww extends com.google.android.gms.dynamic.zzp<zzxa> {
    public zzww() {
        super("com.google.android.gms.ads.AdOverlayCreatorImpl");
    }

    @Override // com.google.android.gms.dynamic.zzp
    protected final /* synthetic */ zzxa zzb(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.overlay.client.IAdOverlayCreator");
        return iInterfaceQueryLocalInterface instanceof zzxa ? (zzxa) iInterfaceQueryLocalInterface : new zzxb(iBinder);
    }

    public final zzwx zze(Activity activity) {
        zzwx zzwzVar;
        try {
            IBinder iBinderZzp = zzaS(activity).zzp(com.google.android.gms.dynamic.zzn.zzw(activity));
            if (iBinderZzp == null) {
                zzwzVar = null;
            } else {
                IInterface iInterfaceQueryLocalInterface = iBinderZzp.queryLocalInterface("com.google.android.gms.ads.internal.overlay.client.IAdOverlay");
                zzwzVar = iInterfaceQueryLocalInterface instanceof zzwx ? (zzwx) iInterfaceQueryLocalInterface : new zzwz(iBinderZzp);
            }
            return zzwzVar;
        } catch (RemoteException e) {
            zzajc.zzc("Could not create remote AdOverlay.", e);
            return null;
        } catch (com.google.android.gms.dynamic.zzq e2) {
            zzajc.zzc("Could not create remote AdOverlay.", e2);
            return null;
        }
    }
}
