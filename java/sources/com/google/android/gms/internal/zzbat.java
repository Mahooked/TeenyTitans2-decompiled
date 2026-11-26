package com.google.android.gms.internal;

import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.ApiOptions;
import java.util.Arrays;

/* JADX INFO: loaded from: classes.dex */
public final class zzbat<O extends Api.ApiOptions> {
    private final O zzaAJ;
    private final int zzaBA;
    private final boolean zzaBz;
    private final Api<O> zzayW;

    private zzbat(Api<O> api) {
        this.zzaBz = true;
        this.zzayW = api;
        this.zzaAJ = null;
        this.zzaBA = System.identityHashCode(this);
    }

    private zzbat(Api<O> api, O o) {
        this.zzaBz = false;
        this.zzayW = api;
        this.zzaAJ = o;
        this.zzaBA = Arrays.hashCode(new Object[]{this.zzayW, this.zzaAJ});
    }

    public static <O extends Api.ApiOptions> zzbat<O> zza(Api<O> api, O o) {
        return new zzbat<>(api, o);
    }

    public static <O extends Api.ApiOptions> zzbat<O> zzb(Api<O> api) {
        return new zzbat<>(api);
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzbat)) {
            return false;
        }
        zzbat zzbatVar = (zzbat) obj;
        return !this.zzaBz && !zzbatVar.zzaBz && com.google.android.gms.common.internal.zzbe.equal(this.zzayW, zzbatVar.zzayW) && com.google.android.gms.common.internal.zzbe.equal(this.zzaAJ, zzbatVar.zzaAJ);
    }

    public final int hashCode() {
        return this.zzaBA;
    }

    public final String zzpr() {
        return this.zzayW.getName();
    }
}
