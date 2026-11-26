package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
public final class zzadr {
    private int mErrorCode;
    private String zzMs;
    private String zzWJ;
    private long zzWK;

    public final zzadr zzax(String str) {
        this.zzMs = str;
        return this;
    }

    public final zzadr zzay(String str) {
        this.zzWJ = str;
        return this;
    }

    public final zzadr zzg(long j) {
        this.zzWK = j;
        return this;
    }

    public final zzadp zzgU() {
        return new zzadp(this);
    }

    public final zzadr zzw(int i) {
        this.mErrorCode = i;
        return this;
    }
}
