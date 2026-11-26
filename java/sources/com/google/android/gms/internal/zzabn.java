package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzabn implements Runnable {
    final /* synthetic */ zzabu zzUA;
    private /* synthetic */ zzmz zzUB;
    final /* synthetic */ String zzUC;
    private /* synthetic */ com.google.android.gms.ads.internal.js.zzl zztA;
    final /* synthetic */ zznb zztd;

    zzabn(com.google.android.gms.ads.internal.js.zzl zzlVar, zzabu zzabuVar, zznb zznbVar, zzmz zzmzVar, String str) {
        this.zztA = zzlVar;
        this.zzUA = zzabuVar;
        this.zztd = zznbVar;
        this.zzUB = zzmzVar;
        this.zzUC = str;
    }

    @Override // java.lang.Runnable
    public final void run() {
        com.google.android.gms.ads.internal.js.zzy zzyVarZzb = this.zztA.zzb((zzcu) null);
        this.zzUA.zzUN = zzyVarZzb;
        this.zztd.zza(this.zzUB, "rwc");
        zzyVarZzb.zza(new zzabo(this, this.zztd.zzdS()), new zzabp(this));
    }
}
