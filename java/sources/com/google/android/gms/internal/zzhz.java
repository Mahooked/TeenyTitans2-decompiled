package com.google.android.gms.internal;

import android.content.Context;
import android.os.DeadObjectException;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzhz extends com.google.android.gms.common.internal.zzd<zzid> {
    zzhz(Context context, Looper looper, com.google.android.gms.common.internal.zzf zzfVar, com.google.android.gms.common.internal.zzg zzgVar) {
        super(context, looper, 123, zzfVar, zzgVar, null);
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final /* synthetic */ IInterface zzd(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.cache.ICacheService");
        return iInterfaceQueryLocalInterface instanceof zzid ? (zzid) iInterfaceQueryLocalInterface : new zzie(iBinder);
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final String zzdb() {
        return "com.google.android.gms.ads.service.CACHE";
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final String zzdc() {
        return "com.google.android.gms.ads.internal.cache.ICacheService";
    }

    public final zzid zzdd() throws DeadObjectException {
        return (zzid) super.zzrf();
    }
}
