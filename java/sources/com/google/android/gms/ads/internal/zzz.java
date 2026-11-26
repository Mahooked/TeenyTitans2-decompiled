package com.google.android.gms.ads.internal;

import com.google.android.gms.internal.zzaff;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzakj;

/* JADX INFO: loaded from: classes.dex */
final class zzz implements zzakj {
    private /* synthetic */ zzaff zztu;
    private /* synthetic */ Runnable zztv;

    zzz(zzx zzxVar, zzaff zzaffVar, Runnable runnable) {
        this.zztu = zzaffVar;
        this.zztv = runnable;
    }

    @Override // com.google.android.gms.internal.zzakj
    public final void zzaS() {
        if (this.zztu.zzXM) {
            return;
        }
        zzbs.zzbz();
        zzagz.zzb(this.zztv);
    }
}
