package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzaim implements Runnable {
    private /* synthetic */ zzaij zzaan;
    private /* synthetic */ zzajm zzaao;

    zzaim(zzaij zzaijVar, zzajm zzajmVar) {
        this.zzaan = zzaijVar;
        this.zzaao = zzajmVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            this.zzaan.zzaD.zzb(this.zzaao.get());
        } catch (Exception e) {
            zzafr.zzb("Error occured while dispatching http response in getter.", e);
            com.google.android.gms.ads.internal.zzbs.zzbD().zza(e, "HttpGetter.deliverResponse.1");
        }
    }
}
