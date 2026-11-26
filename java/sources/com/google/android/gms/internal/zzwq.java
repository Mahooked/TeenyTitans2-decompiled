package com.google.android.gms.internal;

import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzwq {
    private final boolean zzNR;
    private final boolean zzNS;
    private final boolean zzNT;
    private final boolean zzNU;
    private final boolean zzNV;

    private zzwq(zzws zzwsVar) {
        this.zzNR = zzwsVar.zzNR;
        this.zzNS = zzwsVar.zzNS;
        this.zzNT = zzwsVar.zzNT;
        this.zzNU = zzwsVar.zzNU;
        this.zzNV = zzwsVar.zzNV;
    }

    public final JSONObject toJson() {
        try {
            return new JSONObject().put("sms", this.zzNR).put("tel", this.zzNS).put("calendar", this.zzNT).put("storePicture", this.zzNU).put("inlineVideo", this.zzNV);
        } catch (JSONException e) {
            zzafr.zzb("Error occured while obtaining the MRAID capabilities.", e);
            return null;
        }
    }
}
