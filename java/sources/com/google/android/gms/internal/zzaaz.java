package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import android.text.TextUtils;
import com.google.android.gms.ads.identifier.AdvertisingIdClient;
import com.google.android.gms.common.GooglePlayServicesNotAvailableException;
import com.google.android.gms.common.GooglePlayServicesRepairableException;
import java.io.IOException;
import java.util.HashMap;
import java.util.concurrent.CancellationException;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Future;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaaz extends zzafp {
    private final Context mContext;
    private final Object zzQT;
    private final zzzp zzSm;
    private final zzaaf zzSn;
    private zzij zzSo;
    private com.google.android.gms.ads.internal.js.zzy zzUb;
    private static long zzTW = TimeUnit.SECONDS.toMillis(10);
    private static final Object zzuF = new Object();
    private static boolean zzTX = false;
    private static com.google.android.gms.ads.internal.js.zzl zzRl = null;
    private static zzre zzTY = null;
    private static zzrn zzTZ = null;
    private static zzrd zzUa = null;

    public zzaaz(Context context, zzaaf zzaafVar, zzzp zzzpVar, zzij zzijVar) {
        super(true);
        this.zzQT = new Object();
        this.zzSm = zzzpVar;
        this.mContext = context;
        this.zzSn = zzaafVar;
        this.zzSo = zzijVar;
        synchronized (zzuF) {
            if (!zzTX) {
                zzTZ = new zzrn();
                zzTY = new zzre(context.getApplicationContext(), zzaafVar.zzvT);
                zzUa = new zzabh();
                zzRl = new com.google.android.gms.ads.internal.js.zzl(this.mContext.getApplicationContext(), this.zzSn.zzvT, (String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzBX), new zzabg(), new zzabf());
                zzTX = true;
            }
        }
    }

    private final JSONObject zza(zzaae zzaaeVar, String str) {
        zzacb zzacbVar;
        AdvertisingIdClient.Info advertisingIdInfo;
        Bundle bundle = zzaaeVar.zzSz.extras.getBundle("sdk_less_server_data");
        if (bundle == null) {
            return null;
        }
        try {
            zzacbVar = com.google.android.gms.ads.internal.zzbs.zzbI().zzn(this.mContext).get();
        } catch (Exception e) {
            zzafr.zzc("Error grabbing device info: ", e);
            zzacbVar = null;
        }
        Context context = this.mContext;
        zzabk zzabkVar = new zzabk();
        zzabkVar.zzUj = zzaaeVar;
        zzabkVar.zzUk = zzacbVar;
        JSONObject jSONObjectZza = zzabt.zza(context, zzabkVar);
        if (jSONObjectZza == null) {
            return null;
        }
        try {
            advertisingIdInfo = AdvertisingIdClient.getAdvertisingIdInfo(this.mContext);
        } catch (GooglePlayServicesNotAvailableException | GooglePlayServicesRepairableException | IOException | IllegalStateException e2) {
            zzafr.zzc("Cannot get advertising id info", e2);
            advertisingIdInfo = null;
        }
        HashMap map = new HashMap();
        map.put("request_id", str);
        map.put("request_param", jSONObjectZza);
        map.put("data", bundle);
        if (advertisingIdInfo != null) {
            map.put("adid", advertisingIdInfo.getId());
            map.put("lat", Integer.valueOf(advertisingIdInfo.isLimitAdTrackingEnabled() ? 1 : 0));
        }
        try {
            return com.google.android.gms.ads.internal.zzbs.zzbz().zzj(map);
        } catch (JSONException e3) {
            return null;
        }
    }

    protected static void zza(com.google.android.gms.ads.internal.js.zza zzaVar) {
        zzaVar.zza("/loadAd", zzTZ);
        zzaVar.zza("/fetchHttpRequest", zzTY);
        zzaVar.zza("/invalidRequest", zzUa);
    }

    protected static void zzb(com.google.android.gms.ads.internal.js.zza zzaVar) {
        zzaVar.zzb("/loadAd", zzTZ);
        zzaVar.zzb("/fetchHttpRequest", zzTY);
        zzaVar.zzb("/invalidRequest", zzUa);
    }

    private final zzaai zzd(zzaae zzaaeVar) {
        com.google.android.gms.ads.internal.zzbs.zzbz();
        String strZzhO = zzagz.zzhO();
        JSONObject jSONObjectZza = zza(zzaaeVar, strZzhO);
        if (jSONObjectZza == null) {
            return new zzaai(0);
        }
        long jElapsedRealtime = com.google.android.gms.ads.internal.zzbs.zzbF().elapsedRealtime();
        Future<JSONObject> futureZzS = zzTZ.zzS(strZzhO);
        zzaiy.zzaaH.post(new zzabb(this, jSONObjectZza, strZzhO));
        try {
            JSONObject jSONObject = futureZzS.get(zzTW - (com.google.android.gms.ads.internal.zzbs.zzbF().elapsedRealtime() - jElapsedRealtime), TimeUnit.MILLISECONDS);
            if (jSONObject == null) {
                return new zzaai(-1);
            }
            zzaai zzaaiVarZza = zzabt.zza(this.mContext, zzaaeVar, jSONObject.toString());
            return (zzaaiVarZza.errorCode == -3 || !TextUtils.isEmpty(zzaaiVarZza.body)) ? zzaaiVarZza : new zzaai(3);
        } catch (InterruptedException e) {
            return new zzaai(-1);
        } catch (CancellationException e2) {
            return new zzaai(-1);
        } catch (ExecutionException e3) {
            return new zzaai(0);
        } catch (TimeoutException e4) {
            return new zzaai(2);
        }
    }

    @Override // com.google.android.gms.internal.zzafp
    public void onStop() {
        synchronized (this.zzQT) {
            zzaiy.zzaaH.post(new zzabe(this));
        }
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        zzafr.zzaC("SdkLessAdLoaderBackgroundTask started.");
        String strZzw = com.google.android.gms.ads.internal.zzbs.zzbY().zzw(this.mContext);
        zzaae zzaaeVar = new zzaae(this.zzSn, -1L, com.google.android.gms.ads.internal.zzbs.zzbY().zzu(this.mContext), com.google.android.gms.ads.internal.zzbs.zzbY().zzv(this.mContext), strZzw);
        com.google.android.gms.ads.internal.zzbs.zzbY().zzh(this.mContext, strZzw);
        zzaai zzaaiVarZzd = zzd(zzaaeVar);
        zzaiy.zzaaH.post(new zzaba(this, new zzafg(zzaaeVar, zzaaiVarZzd, (zzub) null, (zziv) null, zzaaiVarZzd.errorCode, com.google.android.gms.ads.internal.zzbs.zzbF().elapsedRealtime(), zzaaiVarZzd.zzTs, (JSONObject) null, this.zzSo)));
    }
}
