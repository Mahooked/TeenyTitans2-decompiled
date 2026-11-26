package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzdd implements Runnable {
    private /* synthetic */ zzdb zzqW;
    private /* synthetic */ int zzqX;
    private /* synthetic */ boolean zzqY;

    zzdd(zzdb zzdbVar, int i, boolean z) {
        this.zzqW = zzdbVar;
        this.zzqX = i;
        this.zzqY = z;
    }

    @Override // java.lang.Runnable
    public final void run() {
        zzax zzaxVarZzb = this.zzqW.zzb(this.zzqX, this.zzqY);
        this.zzqW.zzqL = zzaxVarZzb;
        if (zzdb.zza(this.zzqX, zzaxVarZzb)) {
            this.zzqW.zza(this.zzqX + 1, this.zzqY);
        }
    }
}
