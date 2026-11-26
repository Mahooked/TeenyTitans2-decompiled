package com.google.android.gms.ads.internal;

import android.text.TextUtils;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzajs;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzaf implements zzajs<com.google.android.gms.ads.internal.js.zzai> {
    private /* synthetic */ zzae zztG;

    zzaf(zzae zzaeVar) {
        this.zztG = zzaeVar;
    }

    @Override // com.google.android.gms.internal.zzajs
    public final /* synthetic */ void zzc(com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        com.google.android.gms.ads.internal.js.zzai zzaiVar2 = zzaiVar;
        zzaiVar2.zza("/appSettingsFetched", this.zztG.zztB);
        try {
            JSONObject jSONObject = new JSONObject();
            if (!TextUtils.isEmpty(this.zztG.zztC)) {
                jSONObject.put("app_id", this.zztG.zztC);
            } else if (!TextUtils.isEmpty(this.zztG.zztD)) {
                jSONObject.put("ad_unit_id", this.zztG.zztD);
            }
            jSONObject.put("is_init", this.zztG.zztE);
            jSONObject.put("pn", this.zztG.zztF.getPackageName());
            zzaiVar2.zza("AFMA_fetchAppSettings", jSONObject);
        } catch (Exception e) {
            zzaiVar2.zzb("/appSettingsFetched", this.zztG.zztB);
            zzafr.zzb("Error requesting application settings", e);
        }
    }
}
