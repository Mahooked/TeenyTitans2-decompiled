package com.google.android.gms.ads.internal;

import com.google.android.gms.internal.zzaff;
import com.google.android.gms.internal.zzakh;
import com.google.android.gms.internal.zzge;

/* JADX INFO: loaded from: classes.dex */
final class zzam implements zzakh {
    private /* synthetic */ zzaff zztx;
    private /* synthetic */ zzal zzun;

    zzam(zzal zzalVar, zzaff zzaffVar) {
        this.zzun = zzalVar;
        this.zztx = zzaffVar;
    }

    @Override // com.google.android.gms.internal.zzakh
    public final void zzaT() {
        new zzge(this.zzun.zzsP.zzqD, this.zztx.zzPg.getView()).zza(this.zztx.zzPg);
    }
}
