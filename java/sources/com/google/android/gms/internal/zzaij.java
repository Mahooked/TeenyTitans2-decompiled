package com.google.android.gms.internal;

import java.io.ByteArrayInputStream;
import java.io.InputStream;

/* JADX INFO: loaded from: classes.dex */
final class zzaij<T> extends zzp<InputStream> {
    private final zzv<T> zzaD;
    private final zzaii<T> zzaaj;

    public zzaij(String str, zzaii<T> zzaiiVar, zzv<T> zzvVar) {
        super(0, str, new zzaik(zzvVar, zzaiiVar));
        this.zzaaj = zzaiiVar;
        this.zzaD = zzvVar;
    }

    @Override // com.google.android.gms.internal.zzp
    protected final zzt<InputStream> zza(zzn zznVar) {
        return zzt.zza(new ByteArrayInputStream(zznVar.data), zzam.zzb(zznVar));
    }

    @Override // com.google.android.gms.internal.zzp
    protected final /* synthetic */ void zza(InputStream inputStream) {
        zzajm zzajmVarZza = zzagt.zza(new zzail(this, inputStream));
        zzajmVarZza.zzd(new zzaim(this, zzajmVarZza));
    }
}
