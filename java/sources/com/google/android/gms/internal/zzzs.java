package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzzs implements Runnable {
    private /* synthetic */ zzzq zzSq;
    private /* synthetic */ zzajp zzSr;

    zzzs(zzzq zzzqVar, zzajp zzajpVar) {
        this.zzSq = zzzqVar;
        this.zzSr = zzajpVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        synchronized (this.zzSq.zzQT) {
            this.zzSq.zzSp = this.zzSq.zza(this.zzSq.zzSn.zzvT, this.zzSr);
            if (this.zzSq.zzSp == null) {
                this.zzSq.zzd(0, "Could not start the ad request service.");
                zzagz.zzZr.removeCallbacks(this.zzSq.zzQS);
            }
        }
    }
}
