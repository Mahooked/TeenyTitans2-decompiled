package com.google.android.gms.ads.internal.overlay;

import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@zzzn
final class zzas implements Runnable {
    private zzaa zzQK;
    private boolean zzyr = false;

    zzas(zzaa zzaaVar) {
        this.zzQK = zzaaVar;
    }

    private final void zzgl() {
        zzagz.zzZr.removeCallbacks(this);
        zzagz.zzZr.postDelayed(this, 250L);
    }

    public final void pause() {
        this.zzyr = true;
    }

    public final void resume() {
        this.zzyr = false;
        zzgl();
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzyr) {
            return;
        }
        this.zzQK.zzgc();
        zzgl();
    }
}
