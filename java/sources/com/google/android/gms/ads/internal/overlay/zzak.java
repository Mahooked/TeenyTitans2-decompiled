package com.google.android.gms.ads.internal.overlay;

import android.annotation.TargetApi;
import android.graphics.SurfaceTexture;
import com.google.android.gms.ads.internal.zzbs;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zzzn;
import java.util.concurrent.TimeUnit;

/* JADX INFO: loaded from: classes.dex */
@zzzn
@TargetApi(14)
public final class zzak {
    private long zzPJ;
    private final long zzPI = TimeUnit.MILLISECONDS.toNanos(((Long) zzbs.zzbL().zzd(zzmo.zzCx)).longValue());
    private boolean zzPK = true;

    zzak() {
    }

    public final void zza(SurfaceTexture surfaceTexture, zzx zzxVar) {
        if (zzxVar == null) {
            return;
        }
        long timestamp = surfaceTexture.getTimestamp();
        if (this.zzPK || Math.abs(timestamp - this.zzPJ) >= this.zzPI) {
            this.zzPK = false;
            this.zzPJ = timestamp;
            zzagz.zzZr.post(new zzal(this, zzxVar));
        }
    }

    public final void zzfU() {
        this.zzPK = true;
    }
}
