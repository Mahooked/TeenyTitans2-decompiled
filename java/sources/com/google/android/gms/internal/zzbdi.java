package com.google.android.gms.internal;

import com.google.android.gms.common.ConnectionResult;
import java.util.Collections;

/* JADX INFO: loaded from: classes.dex */
final class zzbdi implements Runnable {
    private /* synthetic */ ConnectionResult zzaEw;
    private /* synthetic */ zzbdh zzaEy;

    zzbdi(zzbdh zzbdhVar, ConnectionResult connectionResult) {
        this.zzaEy = zzbdhVar;
        this.zzaEw = connectionResult;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (!this.zzaEw.isSuccess()) {
            ((zzbdd) this.zzaEy.zzaEm.zzaCB.get(this.zzaEy.zzaAK)).onConnectionFailed(this.zzaEw);
            return;
        }
        zzbdh.zza(this.zzaEy, true);
        if (this.zzaEy.zzaCy.zzmv()) {
            this.zzaEy.zzqz();
        } else {
            this.zzaEy.zzaCy.zza(null, Collections.emptySet());
        }
    }
}
