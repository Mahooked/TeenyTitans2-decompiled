package com.google.android.gms.internal;

import android.support.v4.util.SimpleArrayMap;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Future;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzzh implements zzyv<zznu> {
    private final boolean zzSe;

    public zzzh(boolean z) {
        this.zzSe = z;
    }

    private static <K, V> SimpleArrayMap<K, V> zza(SimpleArrayMap<K, Future<V>> simpleArrayMap) throws ExecutionException, InterruptedException {
        SimpleArrayMap<K, V> simpleArrayMap2 = new SimpleArrayMap<>();
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 >= simpleArrayMap.size()) {
                return simpleArrayMap2;
            }
            simpleArrayMap2.put(simpleArrayMap.keyAt(i2), simpleArrayMap.valueAt(i2).get());
            i = i2 + 1;
        }
    }

    @Override // com.google.android.gms.internal.zzyv
    public final /* synthetic */ zzoa zza(zzyn zzynVar, JSONObject jSONObject) throws ExecutionException, JSONException, InterruptedException {
        SimpleArrayMap simpleArrayMap = new SimpleArrayMap();
        SimpleArrayMap simpleArrayMap2 = new SimpleArrayMap();
        zzajm<zznn> zzajmVarZzd = zzynVar.zzd(jSONObject);
        zzajm<zzaka> zzajmVarZzc = zzynVar.zzc(jSONObject, "video");
        JSONArray jSONArray = jSONObject.getJSONArray("custom_assets");
        for (int i = 0; i < jSONArray.length(); i++) {
            JSONObject jSONObject2 = jSONArray.getJSONObject(i);
            String string = jSONObject2.getString("type");
            if ("string".equals(string)) {
                simpleArrayMap2.put(jSONObject2.getString("name"), jSONObject2.getString("string_value"));
            } else if ("image".equals(string)) {
                simpleArrayMap.put(jSONObject2.getString("name"), zzynVar.zza(jSONObject2, "image_value", this.zzSe));
            } else {
                String strValueOf = String.valueOf(string);
                zzafr.zzaT(strValueOf.length() != 0 ? "Unknown custom asset type: ".concat(strValueOf) : new String("Unknown custom asset type: "));
            }
        }
        zzaka zzakaVarZzb = zzyn.zzb(zzajmVarZzc);
        return new zznu(jSONObject.getString("custom_template_id"), zza(simpleArrayMap), simpleArrayMap2, zzajmVarZzd.get(), zzakaVarZzb != null ? zzakaVarZzb.zziH() : null, zzakaVarZzb != null ? zzakaVarZzb.getView() : null);
    }
}
