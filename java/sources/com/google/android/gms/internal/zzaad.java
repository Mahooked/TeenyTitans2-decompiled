package com.google.android.gms.internal;

import android.content.Context;
import android.os.DeadObjectException;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaad extends com.google.android.gms.common.internal.zzd<zzaam> {
    private int zzSx;

    public zzaad(Context context, Looper looper, com.google.android.gms.common.internal.zzf zzfVar, com.google.android.gms.common.internal.zzg zzgVar, int i) {
        super(context, looper, 8, zzfVar, zzgVar, null);
        this.zzSx = i;
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final /* synthetic */ IInterface zzd(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.request.IAdRequestService");
        return iInterfaceQueryLocalInterface instanceof zzaam ? (zzaam) iInterfaceQueryLocalInterface : new zzaao(iBinder);
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final String zzdb() {
        return "com.google.android.gms.ads.service.START";
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final String zzdc() {
        return "com.google.android.gms.ads.internal.request.IAdRequestService";
    }

    public final zzaam zzgC() throws DeadObjectException {
        return (zzaam) super.zzrf();
    }
}
