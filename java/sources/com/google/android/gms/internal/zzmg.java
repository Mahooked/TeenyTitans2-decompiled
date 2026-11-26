package com.google.android.gms.internal;

import android.content.SharedPreferences;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzmg extends zzme<Integer> {
    zzmg(int i, String str, Integer num) {
        super(i, str, num, null);
    }

    @Override // com.google.android.gms.internal.zzme
    public final /* synthetic */ Integer zza(SharedPreferences sharedPreferences) {
        return Integer.valueOf(sharedPreferences.getInt(getKey(), zzdI().intValue()));
    }

    @Override // com.google.android.gms.internal.zzme
    public final /* synthetic */ void zza(SharedPreferences.Editor editor, Integer num) {
        editor.putInt(getKey(), num.intValue());
    }

    @Override // com.google.android.gms.internal.zzme
    public final /* synthetic */ Integer zzb(JSONObject jSONObject) {
        return Integer.valueOf(jSONObject.optInt(getKey(), zzdI().intValue()));
    }
}
