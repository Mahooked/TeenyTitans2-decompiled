package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzuf implements Runnable {
    private /* synthetic */ zzud zzMC;
    private /* synthetic */ zzue zzMD;

    zzuf(zzue zzueVar, zzud zzudVar) {
        this.zzMD = zzueVar;
        this.zzMC = zzudVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        synchronized (this.zzMD.mLock) {
            if (this.zzMD.zzMA != -2) {
                return;
            }
            this.zzMD.zzMz = this.zzMD.zzfl();
            if (this.zzMD.zzMz == null) {
                this.zzMD.zzo(4);
                return;
            }
            if (!this.zzMD.zzfm() || this.zzMD.zzp(1)) {
                this.zzMC.zza(this.zzMD);
                this.zzMD.zza(this.zzMC);
            } else {
                String str = this.zzMD.zzMs;
                zzafr.zzaT(new StringBuilder(String.valueOf(str).length() + 56).append("Ignoring adapter ").append(str).append(" as delayed impression is not supported").toString());
                this.zzMD.zzo(2);
            }
        }
    }
}
