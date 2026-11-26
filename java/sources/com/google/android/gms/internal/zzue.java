package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import android.os.RemoteException;
import android.os.SystemClock;
import android.text.TextUtils;
import com.comscore.streaming.Constants;
import com.google.ads.mediation.AdUrlAdapter;
import com.google.ads.mediation.admob.AdMobAdapter;
import com.google.android.gms.ads.formats.NativeAdOptions;
import com.google.android.gms.ads.mediation.MediationAdapter;
import java.util.ArrayList;
import java.util.List;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzue implements zzui {
    private final Context mContext;
    private final Object mLock = new Object();
    private int zzMA = -2;
    private zzuz zzMB;
    private final String zzMs;
    private final long zzMt;
    private final zzub zzMu;
    private final zzua zzMv;
    private final List<String> zzMw;
    private final List<String> zzMx;
    private final boolean zzMy;
    private zzut zzMz;
    private final zzuq zzsX;
    private final zzon zztS;
    private final List<String> zztT;
    private final zzaje zztW;
    private zzir zzuT;
    private final zziv zzuZ;
    private final boolean zzwJ;

    public zzue(Context context, String str, zzuq zzuqVar, zzub zzubVar, zzua zzuaVar, zzir zzirVar, zziv zzivVar, zzaje zzajeVar, boolean z, boolean z2, zzon zzonVar, List<String> list, List<String> list2, List<String> list3) {
        this.mContext = context;
        this.zzsX = zzuqVar;
        this.zzMv = zzuaVar;
        if ("com.google.ads.mediation.customevent.CustomEventAdapter".equals(str)) {
            this.zzMs = zzfj();
        } else {
            this.zzMs = str;
        }
        this.zzMu = zzubVar;
        this.zzMt = zzubVar.zzLZ != -1 ? zzubVar.zzLZ : Constants.HEARTBEAT_STAGE_ONE_INTERVAL;
        this.zzuT = zzirVar;
        this.zzuZ = zzivVar;
        this.zztW = zzajeVar;
        this.zzwJ = z;
        this.zzMy = z2;
        this.zztS = zzonVar;
        this.zztT = list;
        this.zzMw = list2;
        this.zzMx = list3;
    }

    private static zzut zza(MediationAdapter mediationAdapter) {
        return new zzvj(mediationAdapter);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zza(zzud zzudVar) {
        String strZzaf = zzaf(this.zzMv.zzLP);
        try {
            if (this.zztW.zzaaP < 4100000) {
                if (this.zzuZ.zzAt) {
                    this.zzMz.zza(com.google.android.gms.dynamic.zzn.zzw(this.mContext), this.zzuT, strZzaf, zzudVar);
                    return;
                } else {
                    this.zzMz.zza(com.google.android.gms.dynamic.zzn.zzw(this.mContext), this.zzuZ, this.zzuT, strZzaf, zzudVar);
                    return;
                }
            }
            if (!this.zzwJ && !this.zzMv.zzfi()) {
                if (this.zzuZ.zzAt) {
                    this.zzMz.zza(com.google.android.gms.dynamic.zzn.zzw(this.mContext), this.zzuT, strZzaf, this.zzMv.zzLH, zzudVar);
                    return;
                }
                if (!this.zzMy) {
                    this.zzMz.zza(com.google.android.gms.dynamic.zzn.zzw(this.mContext), this.zzuZ, this.zzuT, strZzaf, this.zzMv.zzLH, zzudVar);
                    return;
                } else if (this.zzMv.zzLS != null) {
                    this.zzMz.zza(com.google.android.gms.dynamic.zzn.zzw(this.mContext), this.zzuT, strZzaf, this.zzMv.zzLH, zzudVar, new zzon(zzag(this.zzMv.zzLW)), this.zzMv.zzLV);
                    return;
                } else {
                    this.zzMz.zza(com.google.android.gms.dynamic.zzn.zzw(this.mContext), this.zzuZ, this.zzuT, strZzaf, this.zzMv.zzLH, zzudVar);
                    return;
                }
            }
            ArrayList arrayList = new ArrayList(this.zztT);
            if (this.zzMw != null) {
                for (String str : this.zzMw) {
                    String str2 = ":false";
                    if (this.zzMx != null && this.zzMx.contains(str)) {
                        str2 = ":true";
                    }
                    arrayList.add(new StringBuilder(String.valueOf(str).length() + 7 + String.valueOf(str2).length()).append("custom:").append(str).append(str2).toString());
                }
            }
            this.zzMz.zza(com.google.android.gms.dynamic.zzn.zzw(this.mContext), this.zzuT, strZzaf, this.zzMv.zzLH, zzudVar, this.zztS, arrayList);
        } catch (RemoteException e) {
            zzafr.zzc("Could not request ad from mediation adapter.", e);
            zzo(5);
        }
    }

    private final String zzaf(String str) {
        if (str == null || !zzfm() || zzp(2)) {
            return str;
        }
        try {
            JSONObject jSONObject = new JSONObject(str);
            jSONObject.remove("cpm_floor_cents");
            return jSONObject.toString();
        } catch (JSONException e) {
            zzafr.zzaT("Could not remove field. Returning the original value");
            return str;
        }
    }

    private static NativeAdOptions zzag(String str) {
        int i = 0;
        NativeAdOptions.Builder builder = new NativeAdOptions.Builder();
        if (str == null) {
            return builder.build();
        }
        try {
            JSONObject jSONObject = new JSONObject(str);
            builder.setRequestMultipleImages(jSONObject.optBoolean("multiple_images", false));
            builder.setReturnUrlsForImageAssets(jSONObject.optBoolean("only_urls", false));
            String strOptString = jSONObject.optString("native_image_orientation", "any");
            if ("landscape".equals(strOptString)) {
                i = 2;
            } else if ("portrait".equals(strOptString)) {
                i = 1;
            } else if (!"any".equals(strOptString)) {
                i = -1;
            }
            builder.setImageOrientation(i);
        } catch (JSONException e) {
            zzafr.zzc("Exception occurred when creating native ad options", e);
        }
        return builder.build();
    }

    private final String zzfj() {
        try {
            return (TextUtils.isEmpty(this.zzMv.zzLL) || !this.zzsX.zzai(this.zzMv.zzLL)) ? "com.google.ads.mediation.customevent.CustomEventAdapter" : "com.google.android.gms.ads.mediation.customevent.CustomEventAdapter";
        } catch (RemoteException e) {
            zzafr.zzaT("Fail to determine the custom event's version, assuming the old one.");
        }
        return "com.google.ads.mediation.customevent.CustomEventAdapter";
    }

    private final zzuz zzfk() {
        if (this.zzMA != 0 || !zzfm()) {
            return null;
        }
        try {
            if (zzp(4) && this.zzMB != null && this.zzMB.zzfo() != 0) {
                return this.zzMB;
            }
        } catch (RemoteException e) {
            zzafr.zzaT("Could not get cpm value from MediationResponseMetadata");
        }
        return new zzug(zzfn());
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final zzut zzfl() {
        String strValueOf = String.valueOf(this.zzMs);
        zzafr.zzaS(strValueOf.length() != 0 ? "Instantiating mediation adapter: ".concat(strValueOf) : new String("Instantiating mediation adapter: "));
        if (!this.zzwJ && !this.zzMv.zzfi()) {
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEG)).booleanValue() && "com.google.ads.mediation.admob.AdMobAdapter".equals(this.zzMs)) {
                return zza(new AdMobAdapter());
            }
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEH)).booleanValue() && "com.google.ads.mediation.AdUrlAdapter".equals(this.zzMs)) {
                return zza(new AdUrlAdapter());
            }
            if ("com.google.ads.mediation.admob.AdMobCustomTabsAdapter".equals(this.zzMs)) {
                return new zzvj(new zzwd());
            }
        }
        try {
            return this.zzsX.zzah(this.zzMs);
        } catch (RemoteException e) {
            String strValueOf2 = String.valueOf(this.zzMs);
            zzafr.zza(strValueOf2.length() != 0 ? "Could not instantiate mediation adapter: ".concat(strValueOf2) : new String("Could not instantiate mediation adapter: "), e);
            return null;
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final boolean zzfm() {
        return this.zzMu.zzMj != -1;
    }

    private final int zzfn() {
        if (this.zzMv.zzLP == null) {
            return 0;
        }
        try {
            JSONObject jSONObject = new JSONObject(this.zzMv.zzLP);
            if ("com.google.ads.mediation.admob.AdMobAdapter".equals(this.zzMs)) {
                return jSONObject.optInt("cpm_cents", 0);
            }
            int iOptInt = zzp(2) ? jSONObject.optInt("cpm_floor_cents", 0) : 0;
            return iOptInt == 0 ? jSONObject.optInt("penalized_average_cpm_cents", 0) : iOptInt;
        } catch (JSONException e) {
            zzafr.zzaT("Could not convert to json. Returning 0");
            return 0;
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final boolean zzp(int i) {
        Bundle interstitialAdapterInfo;
        try {
            if (this.zzwJ) {
                interstitialAdapterInfo = this.zzMz.zzft();
            } else {
                interstitialAdapterInfo = this.zzuZ.zzAt ? this.zzMz.getInterstitialAdapterInfo() : this.zzMz.zzfs();
            }
            return interstitialAdapterInfo != null && (interstitialAdapterInfo.getInt("capabilities", 0) & i) == i;
        } catch (RemoteException e) {
            zzafr.zzaT("Could not get adapter info. Returning false");
            return false;
        }
    }

    public final void cancel() {
        synchronized (this.mLock) {
            try {
                if (this.zzMz != null) {
                    this.zzMz.destroy();
                }
            } catch (RemoteException e) {
                zzafr.zzc("Could not destroy mediation adapter.", e);
            }
            this.zzMA = -1;
            this.mLock.notify();
        }
    }

    public final zzuh zza(long j, long j2) {
        zzuh zzuhVar;
        synchronized (this.mLock) {
            long jElapsedRealtime = SystemClock.elapsedRealtime();
            zzud zzudVar = new zzud();
            zzagz.zzZr.post(new zzuf(this, zzudVar));
            long j3 = this.zzMt;
            while (this.zzMA == -2) {
                long jElapsedRealtime2 = SystemClock.elapsedRealtime();
                long j4 = j3 - (jElapsedRealtime2 - jElapsedRealtime);
                long j5 = j2 - (jElapsedRealtime2 - j);
                if (j4 <= 0 || j5 <= 0) {
                    zzafr.zzaS("Timed out waiting for adapter.");
                    this.zzMA = 3;
                } else {
                    try {
                        this.mLock.wait(Math.min(j4, j5));
                    } catch (InterruptedException e) {
                        this.zzMA = -1;
                    }
                }
            }
            zzuhVar = new zzuh(this.zzMv, this.zzMz, this.zzMs, zzudVar, this.zzMA, zzfk(), com.google.android.gms.ads.internal.zzbs.zzbF().elapsedRealtime() - jElapsedRealtime);
        }
        return zzuhVar;
    }

    @Override // com.google.android.gms.internal.zzui
    public final void zza(int i, zzuz zzuzVar) {
        synchronized (this.mLock) {
            this.zzMA = 0;
            this.zzMB = zzuzVar;
            this.mLock.notify();
        }
    }

    @Override // com.google.android.gms.internal.zzui
    public final void zzo(int i) {
        synchronized (this.mLock) {
            this.zzMA = i;
            this.mLock.notify();
        }
    }
}
