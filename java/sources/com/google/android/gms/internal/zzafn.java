package com.google.android.gms.internal;

import android.os.Bundle;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzafn {
    private final Object mLock;
    private int zzYS;
    private int zzYT;
    private final String zzYb;
    private final zzafk zzvt;

    private zzafn(zzafk zzafkVar, String str) {
        this.mLock = new Object();
        this.zzvt = zzafkVar;
        this.zzYb = str;
    }

    public zzafn(String str) {
        this(com.google.android.gms.ads.internal.zzbs.zzbD(), str);
    }

    public final Bundle toBundle() {
        Bundle bundle;
        synchronized (this.mLock) {
            bundle = new Bundle();
            bundle.putInt("pmnli", this.zzYS);
            bundle.putInt("pmnll", this.zzYT);
        }
        return bundle;
    }

    public final void zzg(int i, int i2) {
        synchronized (this.mLock) {
            this.zzYS = i;
            this.zzYT = i2;
            this.zzvt.zza(this.zzYb, this);
        }
    }
}
