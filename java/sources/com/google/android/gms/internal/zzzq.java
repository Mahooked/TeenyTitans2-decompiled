package com.google.android.gms.internal;

import android.content.Context;
import android.content.SharedPreferences;
import android.support.annotation.NonNull;
import android.text.TextUtils;
import android.webkit.CookieManager;
import java.util.Collections;
import java.util.HashSet;
import java.util.Set;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzzq extends zzafp implements zzzw {
    private final Context mContext;
    private zzaae zzMM;
    private zzub zzMu;
    private zzaai zzQR;
    private Runnable zzQS;
    private final Object zzQT = new Object();
    private final zzzp zzSm;
    private final zzaaf zzSn;
    private final zzij zzSo;
    zzahp zzSp;

    public zzzq(Context context, zzaaf zzaafVar, zzzp zzzpVar, zzij zzijVar) {
        this.zzSm = zzzpVar;
        this.mContext = context;
        this.zzSn = zzaafVar;
        this.zzSo = zzijVar;
    }

    private final zziv zzb(zzaae zzaaeVar) throws zzzt {
        if (((this.zzMM == null || this.zzMM.zzwn == null || this.zzMM.zzwn.size() <= 1) ? false : true) && this.zzMu != null && !this.zzMu.zzMp) {
            return null;
        }
        if (this.zzQR.zzAw) {
            for (zziv zzivVar : zzaaeVar.zzvX.zzAu) {
                if (zzivVar.zzAw) {
                    return new zziv(zzivVar, zzaaeVar.zzvX.zzAu);
                }
            }
        }
        if (this.zzQR.zzTr == null) {
            throw new zzzt("The ad response must specify one of the supported ad sizes.", 0);
        }
        String[] strArrSplit = this.zzQR.zzTr.split("x");
        if (strArrSplit.length != 2) {
            String strValueOf = String.valueOf(this.zzQR.zzTr);
            throw new zzzt(strValueOf.length() != 0 ? "Invalid ad size format from the ad response: ".concat(strValueOf) : new String("Invalid ad size format from the ad response: "), 0);
        }
        try {
            int i = Integer.parseInt(strArrSplit[0]);
            int i2 = Integer.parseInt(strArrSplit[1]);
            for (zziv zzivVar2 : zzaaeVar.zzvX.zzAu) {
                float f = this.mContext.getResources().getDisplayMetrics().density;
                int i3 = zzivVar2.width == -1 ? (int) (zzivVar2.widthPixels / f) : zzivVar2.width;
                int i4 = zzivVar2.height == -2 ? (int) (zzivVar2.heightPixels / f) : zzivVar2.height;
                if (i == i3 && i2 == i4 && !zzivVar2.zzAw) {
                    return new zziv(zzivVar2, zzaaeVar.zzvX.zzAu);
                }
            }
            String strValueOf2 = String.valueOf(this.zzQR.zzTr);
            throw new zzzt(strValueOf2.length() != 0 ? "The ad size from the ad response was not one of the requested sizes: ".concat(strValueOf2) : new String("The ad size from the ad response was not one of the requested sizes: "), 0);
        } catch (NumberFormatException e) {
            String strValueOf3 = String.valueOf(this.zzQR.zzTr);
            throw new zzzt(strValueOf3.length() != 0 ? "Invalid ad size number from the ad response: ".concat(strValueOf3) : new String("Invalid ad size number from the ad response: "), 0);
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzd(int i, String str) {
        if (i == 3 || i == -1) {
            zzafr.zzaS(str);
        } else {
            zzafr.zzaT(str);
        }
        if (this.zzQR == null) {
            this.zzQR = new zzaai(i);
        } else {
            this.zzQR = new zzaai(i, this.zzQR.zzMg);
        }
        this.zzSm.zza(new zzafg(this.zzMM != null ? this.zzMM : new zzaae(this.zzSn, -1L, null, null, null), this.zzQR, this.zzMu, (zziv) null, i, -1L, this.zzQR.zzTs, (JSONObject) null, this.zzSo));
    }

    @Override // com.google.android.gms.internal.zzafp
    public void onStop() {
        synchronized (this.zzQT) {
            if (this.zzSp != null) {
                this.zzSp.cancel();
            }
        }
    }

    final zzahp zza(zzaje zzajeVar, zzajp<zzaae> zzajpVar) {
        Context context = this.mContext;
        if (new zzzv(context).zza(zzajeVar)) {
            zzafr.zzaC("Fetching ad response from local ad request service.");
            zzaab zzaabVar = new zzaab(context, zzajpVar, this);
            zzaabVar.zzgp();
            return zzaabVar;
        }
        zzafr.zzaC("Fetching ad response from remote ad request service.");
        zzji.zzds();
        if (zzaiy.zzX(context)) {
            return new zzaac(context, zzajeVar, zzajpVar, this);
        }
        zzafr.zzaT("Failed to connect to remote ad request service.");
        return null;
    }

    @Override // com.google.android.gms.internal.zzzw
    public final void zza(@NonNull zzaai zzaaiVar) {
        JSONObject jSONObject;
        zzafr.zzaC("Received ad response.");
        this.zzQR = zzaaiVar;
        long jElapsedRealtime = com.google.android.gms.ads.internal.zzbs.zzbF().elapsedRealtime();
        synchronized (this.zzQT) {
            this.zzSp = null;
        }
        com.google.android.gms.ads.internal.zzbs.zzbD().zzf(this.mContext, this.zzQR.zzSV);
        if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDY)).booleanValue()) {
            if (this.zzQR.zzTh) {
                com.google.android.gms.ads.internal.zzbs.zzbD();
                Context context = this.mContext;
                String str = this.zzMM.zzvR;
                SharedPreferences sharedPreferences = context.getSharedPreferences("admob", 0);
                Set<String> stringSet = sharedPreferences.getStringSet("never_pool_slots", Collections.emptySet());
                if (!stringSet.contains(str)) {
                    HashSet hashSet = new HashSet(stringSet);
                    hashSet.add(str);
                    SharedPreferences.Editor editorEdit = sharedPreferences.edit();
                    editorEdit.putStringSet("never_pool_slots", hashSet);
                    editorEdit.apply();
                }
            } else {
                com.google.android.gms.ads.internal.zzbs.zzbD();
                Context context2 = this.mContext;
                String str2 = this.zzMM.zzvR;
                SharedPreferences sharedPreferences2 = context2.getSharedPreferences("admob", 0);
                Set<String> stringSet2 = sharedPreferences2.getStringSet("never_pool_slots", Collections.emptySet());
                if (stringSet2.contains(str2)) {
                    HashSet hashSet2 = new HashSet(stringSet2);
                    hashSet2.remove(str2);
                    SharedPreferences.Editor editorEdit2 = sharedPreferences2.edit();
                    editorEdit2.putStringSet("never_pool_slots", hashSet2);
                    editorEdit2.apply();
                }
            }
        }
        try {
            if (this.zzQR.errorCode != -2 && this.zzQR.errorCode != -3) {
                throw new zzzt(new StringBuilder(66).append("There was a problem getting an ad response. ErrorCode: ").append(this.zzQR.errorCode).toString(), this.zzQR.errorCode);
            }
            if (this.zzQR.errorCode != -3) {
                if (TextUtils.isEmpty(this.zzQR.body)) {
                    throw new zzzt("No fill from ad server.", 3);
                }
                com.google.android.gms.ads.internal.zzbs.zzbD().zze(this.mContext, this.zzQR.zzSH);
                if (this.zzQR.zzTo) {
                    try {
                        this.zzMu = new zzub(this.zzQR.body);
                        com.google.android.gms.ads.internal.zzbs.zzbD().zzz(this.zzMu.zzMe);
                    } catch (JSONException e) {
                        zzafr.zzb("Could not parse mediation config.", e);
                        String strValueOf = String.valueOf(this.zzQR.body);
                        throw new zzzt(strValueOf.length() != 0 ? "Could not parse mediation config: ".concat(strValueOf) : new String("Could not parse mediation config: "), 0);
                    }
                } else {
                    com.google.android.gms.ads.internal.zzbs.zzbD().zzz(this.zzQR.zzMe);
                }
                if (!TextUtils.isEmpty(this.zzQR.zzSW)) {
                    if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGf)).booleanValue()) {
                        zzafr.zzaC("Received cookie from server. Setting webview cookie in CookieManager.");
                        CookieManager cookieManagerZzS = com.google.android.gms.ads.internal.zzbs.zzbB().zzS(this.mContext);
                        if (cookieManagerZzS != null) {
                            cookieManagerZzS.setCookie("googleads.g.doubleclick.net", this.zzQR.zzSW);
                        }
                    }
                }
            }
            zziv zzivVarZzb = this.zzMM.zzvX.zzAu != null ? zzb(this.zzMM) : null;
            com.google.android.gms.ads.internal.zzbs.zzbD().zzx(this.zzQR.zzTy);
            com.google.android.gms.ads.internal.zzbs.zzbD().zzy(this.zzQR.zzTL);
            if (TextUtils.isEmpty(this.zzQR.zzTw)) {
                jSONObject = null;
            } else {
                try {
                    jSONObject = new JSONObject(this.zzQR.zzTw);
                } catch (Exception e2) {
                    zzafr.zzb("Error parsing the JSON for Active View.", e2);
                    jSONObject = null;
                }
            }
            this.zzSm.zza(new zzafg(this.zzMM, this.zzQR, this.zzMu, zzivVarZzb, -2, jElapsedRealtime, this.zzQR.zzTs, jSONObject, this.zzSo));
            zzagz.zzZr.removeCallbacks(this.zzQS);
        } catch (zzzt e3) {
            zzd(e3.getErrorCode(), e3.getMessage());
            zzagz.zzZr.removeCallbacks(this.zzQS);
        }
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        String string;
        zzafr.zzaC("AdLoaderBackgroundTask started.");
        this.zzQS = new zzzr(this);
        zzagz.zzZr.postDelayed(this.zzQS, ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEK)).longValue());
        long jElapsedRealtime = com.google.android.gms.ads.internal.zzbs.zzbF().elapsedRealtime();
        if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEI)).booleanValue() && this.zzSn.zzSz.extras != null && (string = this.zzSn.zzSz.extras.getString("_ad")) != null) {
            this.zzMM = new zzaae(this.zzSn, jElapsedRealtime, null, null, null);
            zza(zzabt.zza(this.mContext, this.zzMM, string));
            return;
        }
        zzajt zzajtVar = new zzajt();
        zzagt.zza(new zzzs(this, zzajtVar));
        String strZzu = com.google.android.gms.ads.internal.zzbs.zzbY().zzu(this.mContext);
        String strZzv = com.google.android.gms.ads.internal.zzbs.zzbY().zzv(this.mContext);
        String strZzw = com.google.android.gms.ads.internal.zzbs.zzbY().zzw(this.mContext);
        com.google.android.gms.ads.internal.zzbs.zzbY().zzh(this.mContext, strZzw);
        this.zzMM = new zzaae(this.zzSn, jElapsedRealtime, strZzu, strZzv, strZzw);
        zzajtVar.zzf(this.zzMM);
    }
}
