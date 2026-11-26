package com.google.android.gms.internal;

import android.os.Bundle;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.concurrent.ExecutionException;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzzf implements zzyv<zznq> {
    private final boolean zzSe;
    private final boolean zzSf;

    public zzzf(boolean z, boolean z2) {
        this.zzSe = z;
        this.zzSf = z2;
    }

    @Override // com.google.android.gms.internal.zzyv
    public final /* synthetic */ zzoa zza(zzyn zzynVar, JSONObject jSONObject) throws ExecutionException, JSONException, InterruptedException {
        List<zzajm<zznp>> listZza = zzynVar.zza(jSONObject, "images", false, this.zzSe, this.zzSf);
        zzajm<zznp> zzajmVarZza = zzynVar.zza(jSONObject, "app_icon", true, this.zzSe);
        zzajm<zzaka> zzajmVarZzc = zzynVar.zzc(jSONObject, "video");
        zzajm<zznn> zzajmVarZzd = zzynVar.zzd(jSONObject);
        ArrayList arrayList = new ArrayList();
        Iterator<zzajm<zznp>> it = listZza.iterator();
        while (it.hasNext()) {
            arrayList.add(it.next().get());
        }
        zzaka zzakaVarZzb = zzyn.zzb(zzajmVarZzc);
        return new zznq(jSONObject.getString("headline"), arrayList, jSONObject.getString("body"), zzajmVarZza.get(), jSONObject.getString("call_to_action"), jSONObject.optDouble("rating", -1.0d), jSONObject.optString("store"), jSONObject.optString("price"), zzajmVarZzd.get(), new Bundle(), zzakaVarZzb != null ? zzakaVarZzb.zziH() : null, zzakaVarZzb != null ? zzakaVarZzb.getView() : null);
    }
}
