package com.google.android.gms.internal;

import android.content.Context;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.NonNull;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.ApiOptions;
import com.google.android.gms.common.api.GoogleApi;

/* JADX INFO: loaded from: classes.dex */
public final class zzbbo<O extends Api.ApiOptions> extends GoogleApi<O> {
    private final Api.zza<? extends zzctk, zzctl> zzaBe;
    private final com.google.android.gms.common.internal.zzq zzaCA;
    private final Api.zze zzaCy;
    private final zzbbi zzaCz;

    public zzbbo(@NonNull Context context, Api<O> api, Looper looper, @NonNull Api.zze zzeVar, @NonNull zzbbi zzbbiVar, com.google.android.gms.common.internal.zzq zzqVar, Api.zza<? extends zzctk, zzctl> zzaVar) {
        super(context, api, looper);
        this.zzaCy = zzeVar;
        this.zzaCz = zzbbiVar;
        this.zzaCA = zzqVar;
        this.zzaBe = zzaVar;
        this.zzaAN.zzb(this);
    }

    @Override // com.google.android.gms.common.api.GoogleApi
    public final Api.zze zza(Looper looper, zzbdd<O> zzbddVar) {
        this.zzaCz.zza(zzbddVar);
        return this.zzaCy;
    }

    @Override // com.google.android.gms.common.api.GoogleApi
    public final zzbej zza(Context context, Handler handler) {
        return new zzbej(context, handler, this.zzaCA, this.zzaBe);
    }

    public final Api.zze zzpJ() {
        return this.zzaCy;
    }
}
