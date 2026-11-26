package com.google.android.gms.internal;

import android.content.Context;
import android.os.IBinder;
import android.os.IInterface;
import android.os.RemoteException;
import android.widget.FrameLayout;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzqc extends com.google.android.gms.dynamic.zzp<zzoz> {
    public zzqc() {
        super("com.google.android.gms.ads.NativeAdViewDelegateCreatorImpl");
    }

    public final zzow zzb(Context context, FrameLayout frameLayout, FrameLayout frameLayout2) {
        zzow zzoyVar;
        try {
            IBinder iBinderZza = zzaS(context).zza(com.google.android.gms.dynamic.zzn.zzw(context), com.google.android.gms.dynamic.zzn.zzw(frameLayout), com.google.android.gms.dynamic.zzn.zzw(frameLayout2), 11020000);
            if (iBinderZza == null) {
                zzoyVar = null;
            } else {
                IInterface iInterfaceQueryLocalInterface = iBinderZza.queryLocalInterface("com.google.android.gms.ads.internal.formats.client.INativeAdViewDelegate");
                zzoyVar = iInterfaceQueryLocalInterface instanceof zzow ? (zzow) iInterfaceQueryLocalInterface : new zzoy(iBinderZza);
            }
            return zzoyVar;
        } catch (RemoteException | com.google.android.gms.dynamic.zzq e) {
            zzajc.zzc("Could not create remote NativeAdViewDelegate.", e);
            return null;
        }
    }

    @Override // com.google.android.gms.dynamic.zzp
    protected final /* synthetic */ zzoz zzb(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.formats.client.INativeAdViewDelegateCreator");
        return iInterfaceQueryLocalInterface instanceof zzoz ? (zzoz) iInterfaceQueryLocalInterface : new zzpa(iBinder);
    }
}
