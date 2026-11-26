package com.google.android.gms.common.internal;

import android.os.IBinder;
import android.os.IInterface;
import com.google.android.gms.common.api.Api;

/* JADX INFO: loaded from: classes.dex */
public final class zzbx<T extends IInterface> extends zzz<T> {
    @Override // com.google.android.gms.common.internal.zzd
    protected final T zzd(IBinder iBinder) {
        Api.zzg zzgVar = null;
        return (T) zzgVar.zzd(iBinder);
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final String zzdb() {
        Api.zzg zzgVar = null;
        return zzgVar.zzdb();
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final String zzdc() {
        Api.zzg zzgVar = null;
        return zzgVar.zzdc();
    }
}
