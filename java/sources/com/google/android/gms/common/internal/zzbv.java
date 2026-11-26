package com.google.android.gms.common.internal;

import android.content.Context;
import android.os.IBinder;
import android.os.IInterface;
import android.view.View;

/* JADX INFO: loaded from: classes.dex */
public final class zzbv extends com.google.android.gms.dynamic.zzp<zzbb> {
    private static final zzbv zzaIv = new zzbv();

    private zzbv() {
        super("com.google.android.gms.common.ui.SignInButtonCreatorImpl");
    }

    public static View zzc(Context context, int i, int i2) throws com.google.android.gms.dynamic.zzq {
        return zzaIv.zzd(context, i, i2);
    }

    private final View zzd(Context context, int i, int i2) throws com.google.android.gms.dynamic.zzq {
        try {
            zzbt zzbtVar = new zzbt(i, i2, null);
            return (View) com.google.android.gms.dynamic.zzn.zzE(zzaS(context).zza(com.google.android.gms.dynamic.zzn.zzw(context), zzbtVar));
        } catch (Exception e) {
            throw new com.google.android.gms.dynamic.zzq(new StringBuilder(64).append("Could not get button with size ").append(i).append(" and color ").append(i2).toString(), e);
        }
    }

    @Override // com.google.android.gms.dynamic.zzp
    public final /* synthetic */ zzbb zzb(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.common.internal.ISignInButtonCreator");
        return iInterfaceQueryLocalInterface instanceof zzbb ? (zzbb) iInterfaceQueryLocalInterface : new zzbc(iBinder);
    }
}
