package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzoq implements Runnable {
    private /* synthetic */ zzoc zzIA;
    private /* synthetic */ zzop zzIB;

    zzoq(zzop zzopVar, zzoc zzocVar) {
        this.zzIB = zzopVar;
        this.zzIA = zzocVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        zzaka zzakaVarZzes;
        try {
            zzakaVarZzes = this.zzIA.zzes();
        } catch (Exception e) {
            zzafr.zzb("Error obtaining overlay.", e);
            zzakaVarZzes = null;
        }
        if (zzakaVarZzes != null && this.zzIB.zzss != null) {
            this.zzIB.zzss.addView(zzakaVarZzes.getView());
        }
        if (this.zzIA instanceof zznx) {
            return;
        }
        this.zzIB.zza(this.zzIA);
    }
}
