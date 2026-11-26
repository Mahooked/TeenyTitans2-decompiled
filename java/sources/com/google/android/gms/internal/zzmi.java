package com.google.android.gms.internal;

import android.content.SharedPreferences;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzmi extends zzme<Float> {
    zzmi(int i, String str, Float f) {
        super(i, str, f, null);
    }

    @Override // com.google.android.gms.internal.zzme
    public final /* synthetic */ Float zza(SharedPreferences sharedPreferences) {
        return Float.valueOf(sharedPreferences.getFloat(getKey(), zzdI().floatValue()));
    }

    @Override // com.google.android.gms.internal.zzme
    public final /* synthetic */ void zza(SharedPreferences.Editor editor, Float f) {
        editor.putFloat(getKey(), f.floatValue());
    }

    @Override // com.google.android.gms.internal.zzme
    public final /* synthetic */ Float zzb(JSONObject jSONObject) {
        return Float.valueOf((float) jSONObject.optDouble(getKey(), zzdI().floatValue()));
    }
}
