package com.google.android.gms.internal;

import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzfq implements zzgd {
    private final zzfi zzxn;
    private final com.google.android.gms.ads.internal.js.zzai zzxo;
    private final zzrd zzxp = new zzfr(this);
    private final zzrd zzxq = new zzfs(this);
    private final zzrd zzxr = new zzft(this);

    public zzfq(zzfi zzfiVar, com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        this.zzxn = zzfiVar;
        this.zzxo = zzaiVar;
        com.google.android.gms.ads.internal.js.zzai zzaiVar2 = this.zzxo;
        zzaiVar2.zza("/updateActiveView", this.zzxp);
        zzaiVar2.zza("/untrackActiveViewUnit", this.zzxq);
        zzaiVar2.zza("/visibilityChanged", this.zzxr);
        String strValueOf = String.valueOf(this.zzxn.zzwQ.zzcm());
        zzafr.zzaC(strValueOf.length() != 0 ? "Custom JS tracking ad unit: ".concat(strValueOf) : new String("Custom JS tracking ad unit: "));
    }

    @Override // com.google.android.gms.internal.zzgd
    public final void zzb(JSONObject jSONObject, boolean z) {
        if (z) {
            this.zzxn.zzb(this);
        } else {
            this.zzxo.zza("AFMA_updateActiveView", jSONObject);
        }
    }

    @Override // com.google.android.gms.internal.zzgd
    public final boolean zzcy() {
        return true;
    }

    @Override // com.google.android.gms.internal.zzgd
    public final void zzcz() {
        com.google.android.gms.ads.internal.js.zzai zzaiVar = this.zzxo;
        zzaiVar.zzb("/visibilityChanged", this.zzxr);
        zzaiVar.zzb("/untrackActiveViewUnit", this.zzxq);
        zzaiVar.zzb("/updateActiveView", this.zzxp);
    }
}
