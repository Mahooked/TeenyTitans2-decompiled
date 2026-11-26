package com.google.android.gms.internal;

import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.safetynet.SafetyNet;

/* JADX INFO: loaded from: classes.dex */
abstract class zzcrv<R extends Result> extends zzbay<R, zzcsn> {
    public zzcrv(GoogleApiClient googleApiClient) {
        super(SafetyNet.API, googleApiClient);
    }

    @Override // com.google.android.gms.internal.zzbay, com.google.android.gms.internal.zzbaz
    public final /* bridge */ /* synthetic */ void setResult(Object obj) {
        super.setResult((Result) obj);
    }
}
