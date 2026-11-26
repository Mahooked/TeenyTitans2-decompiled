package com.google.android.gms.internal;

import android.content.Context;
import android.support.annotation.NonNull;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.GoogleApi;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Status;

/* JADX INFO: loaded from: classes.dex */
public final class zzazw extends GoogleApi<Api.ApiOptions.NoOptions> implements zzazt {
    private zzazw(@NonNull Context context) {
        super(context, zzazn.API, (Api.ApiOptions) null, new zzbas());
    }

    public static zzazt zzaq(@NonNull Context context) {
        return new zzazw(context);
    }

    @Override // com.google.android.gms.internal.zzazt
    public final PendingResult<Status> zza(zzazu zzazuVar) {
        return zzc(new zzazz(zzazuVar, zzpi()));
    }
}
