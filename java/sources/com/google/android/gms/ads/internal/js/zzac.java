package com.google.android.gms.ads.internal.js;

import com.google.android.gms.common.internal.zzbo;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzahz;
import com.google.android.gms.internal.zzajr;
import com.google.android.gms.internal.zzajt;

/* JADX INFO: loaded from: classes.dex */
public final class zzac extends zzajt<zza> {
    private zzahz<zza> zzLc;
    private final Object mLock = new Object();
    private boolean zzLu = false;
    private int zzLv = 0;

    public zzac(zzahz<zza> zzahzVar) {
        this.zzLc = zzahzVar;
    }

    private final void zzfd() {
        synchronized (this.mLock) {
            zzbo.zzae(this.zzLv >= 0);
            if (this.zzLu && this.zzLv == 0) {
                zzafr.v("No reference is left (including root). Cleaning up engine.");
                zza(new zzaf(this), new zzajr());
            } else {
                zzafr.v("There are still references to the engine. Not destroying.");
            }
        }
    }

    public final zzy zzfa() {
        zzy zzyVar = new zzy(this);
        synchronized (this.mLock) {
            zza(new zzad(this, zzyVar), new zzae(this, zzyVar));
            zzbo.zzae(this.zzLv >= 0);
            this.zzLv++;
        }
        return zzyVar;
    }

    protected final void zzfb() {
        synchronized (this.mLock) {
            zzbo.zzae(this.zzLv > 0);
            zzafr.v("Releasing 1 reference for JS Engine");
            this.zzLv--;
            zzfd();
        }
    }

    public final void zzfc() {
        synchronized (this.mLock) {
            zzbo.zzae(this.zzLv >= 0);
            zzafr.v("Releasing root reference. JS Engine will be destroyed once other references are released.");
            this.zzLu = true;
            zzfd();
        }
    }
}
