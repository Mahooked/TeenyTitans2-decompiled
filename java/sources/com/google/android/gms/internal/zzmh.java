package com.google.android.gms.internal;

import android.content.SharedPreferences;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzmh extends zzme<Long> {
    zzmh(int i, String str, Long l) {
        super(i, str, l, null);
    }

    @Override // com.google.android.gms.internal.zzme
    public final /* synthetic */ Long zza(SharedPreferences sharedPreferences) {
        return Long.valueOf(sharedPreferences.getLong(getKey(), zzdI().longValue()));
    }

    @Override // com.google.android.gms.internal.zzme
    public final /* synthetic */ void zza(SharedPreferences.Editor editor, Long l) {
        editor.putLong(getKey(), l.longValue());
    }

    @Override // com.google.android.gms.internal.zzme
    public final /* synthetic */ Long zzb(JSONObject jSONObject) {
        return Long.valueOf(jSONObject.optLong(getKey(), zzdI().longValue()));
    }
}
