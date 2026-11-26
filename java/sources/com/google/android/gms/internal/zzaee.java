package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.google.android.gms.ads.reward.RewardItem;
import java.util.Arrays;
import org.json.JSONArray;
import org.json.JSONException;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaee extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzaee> CREATOR = new zzaef();
    public final String type;
    public final int zzWW;

    public zzaee(RewardItem rewardItem) {
        this(rewardItem.getType(), rewardItem.getAmount());
    }

    public zzaee(String str, int i) {
        this.type = str;
        this.zzWW = i;
    }

    @Nullable
    public static zzaee zza(JSONArray jSONArray) throws JSONException {
        if (jSONArray == null || jSONArray.length() == 0) {
            return null;
        }
        return new zzaee(jSONArray.getJSONObject(0).optString("rb_type"), jSONArray.getJSONObject(0).optInt("rb_amount"));
    }

    @Nullable
    public static zzaee zzaz(@Nullable String str) {
        if (TextUtils.isEmpty(str)) {
            return null;
        }
        try {
            return zza(new JSONArray(str));
        } catch (JSONException e) {
            return null;
        }
    }

    public final boolean equals(Object obj) {
        if (obj == null || !(obj instanceof zzaee)) {
            return false;
        }
        zzaee zzaeeVar = (zzaee) obj;
        return com.google.android.gms.common.internal.zzbe.equal(this.type, zzaeeVar.type) && com.google.android.gms.common.internal.zzbe.equal(Integer.valueOf(this.zzWW), Integer.valueOf(zzaeeVar.zzWW));
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.type, Integer.valueOf(this.zzWW)});
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.type, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 3, this.zzWW);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
