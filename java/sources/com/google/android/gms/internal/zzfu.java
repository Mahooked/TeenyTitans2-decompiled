package com.google.android.gms.internal;

import android.content.Context;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzfu implements zzgd {
    private final Context mContext;
    private final zzfi zzxn;
    private com.google.android.gms.ads.internal.js.zzy zzxt;
    private boolean zzxu;
    private final zzrd zzxp = new zzfz(this);
    private final zzrd zzxq = new zzga(this);
    private final zzrd zzxr = new zzgb(this);
    private final zzrd zzxv = new zzgc(this);

    public zzfu(zzfi zzfiVar, com.google.android.gms.ads.internal.js.zzl zzlVar, Context context) {
        this.zzxn = zzfiVar;
        this.mContext = context;
        this.zzxt = zzlVar.zzb((zzcu) null);
        this.zzxt.zza(new zzfv(this), new zzfw(this));
        String strValueOf = String.valueOf(this.zzxn.zzwQ.zzcm());
        zzafr.zzaC(strValueOf.length() != 0 ? "Core JS tracking ad unit: ".concat(strValueOf) : new String("Core JS tracking ad unit: "));
    }

    static /* synthetic */ boolean zza(zzfu zzfuVar, boolean z) {
        zzfuVar.zzxu = true;
        return true;
    }

    final void zzb(com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        zzaiVar.zza("/updateActiveView", this.zzxp);
        zzaiVar.zza("/untrackActiveViewUnit", this.zzxq);
        zzaiVar.zza("/visibilityChanged", this.zzxr);
        if (com.google.android.gms.ads.internal.zzbs.zzbY().zzp(this.mContext)) {
            zzaiVar.zza("/logScionEvent", this.zzxv);
        }
    }

    @Override // com.google.android.gms.internal.zzgd
    public final void zzb(JSONObject jSONObject, boolean z) {
        this.zzxt.zza(new zzfx(this, jSONObject), new zzajr());
    }

    final void zzc(com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        zzaiVar.zzb("/visibilityChanged", this.zzxr);
        zzaiVar.zzb("/untrackActiveViewUnit", this.zzxq);
        zzaiVar.zzb("/updateActiveView", this.zzxp);
        if (com.google.android.gms.ads.internal.zzbs.zzbY().zzp(this.mContext)) {
            zzaiVar.zzb("/logScionEvent", this.zzxv);
        }
    }

    @Override // com.google.android.gms.internal.zzgd
    public final boolean zzcy() {
        return this.zzxu;
    }

    @Override // com.google.android.gms.internal.zzgd
    public final void zzcz() {
        this.zzxt.zza(new zzfy(this), new zzajr());
        this.zzxt.release();
    }
}
