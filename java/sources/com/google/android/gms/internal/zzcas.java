package com.google.android.gms.internal;

import android.content.Context;
import android.os.DeadObjectException;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;

/* JADX INFO: loaded from: classes.dex */
public final class zzcas extends com.google.android.gms.common.internal.zzd<zzcax> {
    public zzcas(Context context, Looper looper, com.google.android.gms.common.internal.zzf zzfVar, com.google.android.gms.common.internal.zzg zzgVar) {
        super(context, looper, 116, zzfVar, zzgVar, null);
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final /* synthetic */ IInterface zzd(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.gass.internal.IGassService");
        return iInterfaceQueryLocalInterface instanceof zzcax ? (zzcax) iInterfaceQueryLocalInterface : new zzcay(iBinder);
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final String zzdb() {
        return "com.google.android.gms.gass.START";
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final String zzdc() {
        return "com.google.android.gms.gass.internal.IGassService";
    }

    public final zzcax zzvz() throws DeadObjectException {
        return (zzcax) super.zzrf();
    }
}
