package com.google.android.gms.internal;

import android.content.Context;
import android.content.SharedPreferences;
import android.support.annotation.Nullable;
import org.apache.http.client.methods.HttpHead;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzacg extends zzack {
    private final Object mLock = new Object();

    @Nullable
    private SharedPreferences zzWi;

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzgN() {
        this.zzWi.edit().putLong("js_last_update", com.google.android.gms.ads.internal.zzbs.zzbF().currentTimeMillis()).apply();
    }

    @Override // com.google.android.gms.internal.zzack
    public final void zza(Context context, zzabu zzabuVar, zzaje zzajeVar) {
        synchronized (this.mLock) {
            if (this.zzWi == null) {
                this.zzWi = context.getSharedPreferences("google_ads_flags_meta", 0);
            }
        }
        if (com.google.android.gms.ads.internal.zzbs.zzbF().currentTimeMillis() - this.zzWi.getLong("js_last_update", 0L) < ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFm)).longValue()) {
            zze(zzabuVar);
            return;
        }
        zzach zzachVar = new zzach(this, context, zzabuVar);
        JSONObject jSONObject = new JSONObject();
        try {
            jSONObject.put("js", zzajeVar.zzaP);
            jSONObject.put("mf", com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFn));
            jSONObject.put("cl", "162978962");
            jSONObject.put("rapid_rc", "dev");
            jSONObject.put("rapid_rollup", HttpHead.METHOD_NAME);
            zzabuVar.zzUN.zza(new zzaci(this, zzachVar, jSONObject, zzabuVar), new zzacj(this, zzabuVar));
        } catch (JSONException e) {
            zzafr.zzb("Unable to populate SDK Core Constants parameters.", e);
            zze(zzabuVar);
        }
    }
}
