package com.google.android.gms.internal;

import java.lang.ref.WeakReference;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzyy implements Runnable {
    final /* synthetic */ JSONObject zzRZ;
    final /* synthetic */ zzajg zzSa;
    final /* synthetic */ zzyx zzSb;

    zzyy(zzyx zzyxVar, JSONObject jSONObject, zzajg zzajgVar) {
        this.zzSb = zzyxVar;
        this.zzRZ = jSONObject;
        this.zzSa = zzajgVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            zzaka zzakaVarZzgz = this.zzSb.zzgz();
            this.zzSb.zzRm.zzd(zzakaVarZzgz);
            WeakReference weakReference = new WeakReference(zzakaVarZzgz);
            zzakaVarZzgz.zziw().zza(this.zzSb.zza((WeakReference<zzaka>) weakReference), this.zzSb.zzb(weakReference));
            this.zzSb.zzi(zzakaVarZzgz);
            zzakaVarZzgz.zziw().zza(new zzyz(this, zzakaVarZzgz));
            zzakaVarZzgz.zziw().zza(new zzza(this));
            zzakaVarZzgz.loadUrl((String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFs));
        } catch (Exception e) {
            zzafr.zzc("Exception occurred while getting video view", e);
            this.zzSa.zzg(null);
        }
    }
}
