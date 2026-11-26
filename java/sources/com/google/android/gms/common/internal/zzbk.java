package com.google.android.gms.common.internal;

import com.google.android.gms.common.api.Response;
import com.google.android.gms.common.api.Result;

/* JADX INFO: Add missing generic type declarations: [R, T] */
/* JADX INFO: loaded from: classes.dex */
final class zzbk<R, T> implements zzbm<R, T> {
    private /* synthetic */ Response zzaIn;

    zzbk(Response response) {
        this.zzaIn = response;
    }

    @Override // com.google.android.gms.common.internal.zzbm
    public final /* synthetic */ Object zzd(Result result) {
        this.zzaIn.setResult(result);
        return this.zzaIn;
    }
}
