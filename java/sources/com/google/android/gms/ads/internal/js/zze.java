package com.google.android.gms.ads.internal.js;

import android.content.Context;
import android.support.annotation.Nullable;
import com.google.android.gms.ads.internal.zzbs;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzaiy;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzakm;
import com.google.android.gms.internal.zzcu;
import com.google.android.gms.internal.zzig;
import com.google.android.gms.internal.zzim;
import com.google.android.gms.internal.zziv;
import com.google.android.gms.internal.zzji;
import com.google.android.gms.internal.zzqk;
import com.google.android.gms.internal.zzrd;
import com.google.android.gms.internal.zzrm;
import com.google.android.gms.internal.zzwv;
import com.google.android.gms.internal.zzzn;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zze implements zza {
    private final zzaka zzJH;

    public zze(Context context, zzaje zzajeVar, @Nullable zzcu zzcuVar, com.google.android.gms.ads.internal.zzv zzvVar) throws zzakm {
        this.zzJH = zzbs.zzbA().zza(context, new zziv(), false, false, zzcuVar, zzajeVar, null, null, zzvVar, zzig.zzde());
        this.zzJH.getWebView().setWillNotDraw(true);
    }

    private static void runOnUiThread(Runnable runnable) {
        zzji.zzds();
        if (zzaiy.zzil()) {
            runnable.run();
        } else {
            zzagz.zzZr.post(runnable);
        }
    }

    @Override // com.google.android.gms.ads.internal.js.zza
    public final void destroy() {
        this.zzJH.destroy();
    }

    @Override // com.google.android.gms.ads.internal.js.zza
    public final void zza(zzb zzbVar) {
        this.zzJH.zziw().zza(new zzk(this, zzbVar));
    }

    @Override // com.google.android.gms.ads.internal.js.zza
    public final void zza(zzim zzimVar, com.google.android.gms.ads.internal.overlay.zzw zzwVar, zzqk zzqkVar, com.google.android.gms.ads.internal.overlay.zzag zzagVar, boolean z, zzrm zzrmVar, com.google.android.gms.ads.internal.zzw zzwVar2, zzwv zzwvVar) {
        this.zzJH.zziw().zza(zzimVar, zzwVar, zzqkVar, zzagVar, false, null, new com.google.android.gms.ads.internal.zzw(null), null, null);
    }

    @Override // com.google.android.gms.ads.internal.js.zzai
    public final void zza(String str, zzrd zzrdVar) {
        this.zzJH.zziw().zza(str, zzrdVar);
    }

    @Override // com.google.android.gms.ads.internal.js.zzai
    public final void zza(String str, JSONObject jSONObject) {
        runOnUiThread(new zzf(this, str, jSONObject));
    }

    @Override // com.google.android.gms.ads.internal.js.zza
    public final void zzac(String str) {
        runOnUiThread(new zzh(this, String.format("<!DOCTYPE html><html><head><script src=\"%s\"></script></head><body></body></html>", str)));
    }

    @Override // com.google.android.gms.ads.internal.js.zza
    public final void zzad(String str) {
        runOnUiThread(new zzj(this, str));
    }

    @Override // com.google.android.gms.ads.internal.js.zza
    public final void zzae(String str) {
        runOnUiThread(new zzi(this, str));
    }

    @Override // com.google.android.gms.ads.internal.js.zzai
    public final void zzb(String str, zzrd zzrdVar) {
        this.zzJH.zziw().zzb(str, zzrdVar);
    }

    @Override // com.google.android.gms.ads.internal.js.zzai
    public final void zzb(String str, JSONObject jSONObject) {
        this.zzJH.zzb(str, jSONObject);
    }

    @Override // com.google.android.gms.ads.internal.js.zza
    public final zzaj zzeY() {
        return new zzak(this);
    }

    @Override // com.google.android.gms.ads.internal.js.zzai
    public final void zzi(String str, String str2) {
        runOnUiThread(new zzg(this, str, str2));
    }
}
