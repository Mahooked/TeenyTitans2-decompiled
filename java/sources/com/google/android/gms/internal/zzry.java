package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzry extends zzafp {
    final zzaka zzJH;
    final zzsb zzJL;
    private final String zzJM;

    zzry(zzaka zzakaVar, zzsb zzsbVar, String str) {
        this.zzJH = zzakaVar;
        this.zzJL = zzsbVar;
        this.zzJM = str;
        com.google.android.gms.ads.internal.zzbs.zzbW().zza(this);
    }

    @Override // com.google.android.gms.internal.zzafp
    public void onStop() {
        this.zzJL.abort();
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        try {
            this.zzJL.zzU(this.zzJM);
        } finally {
            zzagz.zzZr.post(new zzrz(this));
        }
    }
}
