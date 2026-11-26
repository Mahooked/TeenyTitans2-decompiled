package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaeq extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzaeq> CREATOR = new zzaer();
    public final String zzXr;
    public final String zzXs;
    public final boolean zzXt;
    public final boolean zzXu;
    public final List<String> zzXv;
    public final boolean zzXw;
    public final boolean zzXx;

    public zzaeq(String str, String str2, boolean z, boolean z2, List<String> list, boolean z3, boolean z4) {
        this.zzXr = str;
        this.zzXs = str2;
        this.zzXt = z;
        this.zzXu = z2;
        this.zzXv = list;
        this.zzXw = z3;
        this.zzXx = z4;
    }

    @Nullable
    public static zzaeq zzf(JSONObject jSONObject) throws JSONException {
        if (jSONObject == null) {
            return null;
        }
        String strOptString = jSONObject.optString("click_string", "");
        String strOptString2 = jSONObject.optString("report_url", "");
        boolean zOptBoolean = jSONObject.optBoolean("rendered_ad_enabled", false);
        boolean zOptBoolean2 = jSONObject.optBoolean("non_malicious_reporting_enabled", false);
        JSONArray jSONArrayOptJSONArray = jSONObject.optJSONArray("allowed_headers");
        if (jSONArrayOptJSONArray == null) {
            jSONArrayOptJSONArray = new JSONArray();
        }
        ArrayList arrayList = new ArrayList();
        for (int i = 0; i < jSONArrayOptJSONArray.length(); i++) {
            String strOptString3 = jSONArrayOptJSONArray.optString(i);
            if (!TextUtils.isEmpty(strOptString3)) {
                arrayList.add(strOptString3.toLowerCase(Locale.ENGLISH));
            }
        }
        return new zzaeq(strOptString, strOptString2, zOptBoolean, zOptBoolean2, arrayList, jSONObject.optBoolean("protection_enabled", false), jSONObject.optBoolean("malicious_reporting_enabled", false));
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.zzXr, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, this.zzXs, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 4, this.zzXt);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 5, this.zzXu);
        com.google.android.gms.common.internal.safeparcel.zzd.zzb(parcel, 6, this.zzXv, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 7, this.zzXw);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 8, this.zzXx);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
