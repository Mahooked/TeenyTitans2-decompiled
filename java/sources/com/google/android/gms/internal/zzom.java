package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzom extends zzym {
    private /* synthetic */ zzog zzIk;

    zzom(zzog zzogVar) {
        this.zzIk = zzogVar;
    }

    @Override // com.google.android.gms.internal.zzym
    public final void zzd(com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        zzaka zzakaVar = (zzaka) this.zzIk.zzIh.get();
        if (zzakaVar == null) {
            return;
        }
        zzaiVar.zza("/loadHtml", this.zzIk.zze(zzaiVar));
        zzaiVar.zza("/showOverlay", this.zzIk.zzf(zzaiVar));
        zzaiVar.zza("/hideOverlay", this.zzIk.zzg(zzaiVar));
        zzakaVar.zziw().zza("/sendMessageToSdk", this.zzIk.zzh(zzaiVar));
    }
}
