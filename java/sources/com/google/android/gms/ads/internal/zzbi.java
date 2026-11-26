package com.google.android.gms.ads.internal;

import android.support.annotation.Nullable;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzir;
import com.google.android.gms.internal.zzzn;
import java.lang.ref.WeakReference;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzbi {
    private final zzbk zzuS;

    @Nullable
    private zzir zzuT;
    private boolean zzuU;
    private boolean zzuV;
    private long zzuW;
    private final Runnable zzv;

    public zzbi(zza zzaVar) {
        this(zzaVar, new zzbk(zzagz.zzZr));
    }

    private zzbi(zza zzaVar, zzbk zzbkVar) {
        this.zzuU = false;
        this.zzuV = false;
        this.zzuW = 0L;
        this.zzuS = zzbkVar;
        this.zzv = new zzbj(this, new WeakReference(zzaVar));
    }

    static /* synthetic */ boolean zza(zzbi zzbiVar, boolean z) {
        zzbiVar.zzuU = false;
        return false;
    }

    public final void cancel() {
        this.zzuU = false;
        this.zzuS.removeCallbacks(this.zzv);
    }

    public final void pause() {
        this.zzuV = true;
        if (this.zzuU) {
            this.zzuS.removeCallbacks(this.zzv);
        }
    }

    public final void resume() {
        this.zzuV = false;
        if (this.zzuU) {
            this.zzuU = false;
            zza(this.zzuT, this.zzuW);
        }
    }

    public final void zza(zzir zzirVar, long j) {
        if (this.zzuU) {
            zzafr.zzaT("An ad refresh is already scheduled.");
            return;
        }
        this.zzuT = zzirVar;
        this.zzuU = true;
        this.zzuW = j;
        if (this.zzuV) {
            return;
        }
        zzafr.zzaS(new StringBuilder(65).append("Scheduling ad refresh ").append(j).append(" milliseconds from now.").toString());
        this.zzuS.postDelayed(this.zzv, j);
    }

    public final boolean zzbo() {
        return this.zzuU;
    }

    public final void zzf(zzir zzirVar) {
        this.zzuT = zzirVar;
    }

    public final void zzg(zzir zzirVar) {
        zza(zzirVar, 60000L);
    }
}
