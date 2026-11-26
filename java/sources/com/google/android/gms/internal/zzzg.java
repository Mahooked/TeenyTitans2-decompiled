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
public final class zzzg implements zzyv<zzns> {
    private final boolean zzSe;
    private final boolean zzSf;

    public zzzg(boolean z, boolean z2) {
        this.zzSe = z;
        this.zzSf = z2;
    }

    @Override // com.google.android.gms.internal.zzyv
    public final /* synthetic */ zzoa zza(zzyn zzynVar, JSONObject jSONObject) throws ExecutionException, JSONException, InterruptedException {
        List<zzajm<zznp>> listZza = zzynVar.zza(jSONObject, "images", false, this.zzSe, this.zzSf);
        zzajm<zznp> zzajmVarZza = zzynVar.zza(jSONObject, "secondary_image", false, this.zzSe);
        zzajm<zznn> zzajmVarZzd = zzynVar.zzd(jSONObject);
        zzajm<zzaka> zzajmVarZzc = zzynVar.zzc(jSONObject, "video");
        ArrayList arrayList = new ArrayList();
        Iterator<zzajm<zznp>> it = listZza.iterator();
        while (it.hasNext()) {
            arrayList.add(it.next().get());
        }
        zzaka zzakaVarZzb = zzyn.zzb(zzajmVarZzc);
        return new zzns(jSONObject.getString("headline"), arrayList, jSONObject.getString("body"), zzajmVarZza.get(), jSONObject.getString("call_to_action"), jSONObject.getString("advertiser"), zzajmVarZzd.get(), new Bundle(), zzakaVarZzb != null ? zzakaVarZzb.zziH() : null, zzakaVarZzb != null ? zzakaVarZzb.getView() : null);
    }
}
