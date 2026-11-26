package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
@zzzn
final class zzrj {
    private final zzrk zzJx;
    private final boolean zzJy;
    private final String zzJz;

    public zzrj(zzre zzreVar, boolean z, zzrk zzrkVar, String str) {
        this.zzJy = z;
        this.zzJx = zzrkVar;
        this.zzJz = str;
    }

    public final String getReason() {
        return this.zzJz;
    }

    public final boolean isSuccess() {
        return this.zzJy;
    }

    public final zzrk zzeC() {
        return this.zzJx;
    }
}
