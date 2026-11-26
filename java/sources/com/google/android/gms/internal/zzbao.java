package com.google.android.gms.internal;

import android.os.DeadObjectException;
import android.support.annotation.NonNull;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.internal.zzbay;

/* JADX INFO: loaded from: classes.dex */
public final class zzbao<A extends zzbay<? extends Result, Api.zzb>> extends zzbam {
    private A zzaBt;

    public zzbao(int i, A a) {
        super(i);
        this.zzaBt = a;
    }

    @Override // com.google.android.gms.internal.zzbam
    public final void zza(@NonNull zzbbt zzbbtVar, boolean z) {
        zzbbtVar.zza(this.zzaBt, z);
    }

    @Override // com.google.android.gms.internal.zzbam
    public final void zza(zzbdd<?> zzbddVar) throws DeadObjectException {
        this.zzaBt.zzb(zzbddVar.zzpJ());
    }

    @Override // com.google.android.gms.internal.zzbam
    public final void zzp(@NonNull Status status) {
        this.zzaBt.zzr(status);
    }
}
