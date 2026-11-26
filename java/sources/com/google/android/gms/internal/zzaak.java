package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;
import android.support.annotation.Nullable;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaak extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzaak> CREATOR = new zzaal();
    private boolean zzTN;

    @Nullable
    private List<String> zzTO;

    public zzaak() {
        this(false, Collections.emptyList());
    }

    public zzaak(boolean z, List<String> list) {
        this.zzTN = z;
        this.zzTO = list;
    }

    @Nullable
    public static zzaak zze(JSONObject jSONObject) {
        if (jSONObject == null) {
            return new zzaak();
        }
        JSONArray jSONArrayOptJSONArray = jSONObject.optJSONArray("reporting_urls");
        ArrayList arrayList = new ArrayList();
        if (jSONArrayOptJSONArray != null) {
            for (int i = 0; i < jSONArrayOptJSONArray.length(); i++) {
                try {
                    arrayList.add(jSONArrayOptJSONArray.getString(i));
                } catch (JSONException e) {
                    zzafr.zzc("Error grabbing url from json.", e);
                }
            }
        }
        return new zzaak(jSONObject.optBoolean("enable_protection"), arrayList);
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.zzTN);
        com.google.android.gms.common.internal.safeparcel.zzd.zzb(parcel, 3, this.zzTO, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
