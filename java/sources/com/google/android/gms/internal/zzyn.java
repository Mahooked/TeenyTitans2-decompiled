package com.google.android.gms.internal;

import android.content.Context;
import android.graphics.Color;
import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.os.RemoteException;
import android.text.TextUtils;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Iterator;
import java.util.List;
import java.util.UUID;
import java.util.concurrent.Callable;
import java.util.concurrent.CancellationException;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Future;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzyn implements Callable<zzaff> {
    private static long zzRj = TimeUnit.SECONDS.toMillis(60);
    private final Context mContext;
    private int mErrorCode;
    private final Object mLock = new Object();
    private final zzcu zzIc;
    private final zzafg zzQQ;
    private String zzRA;
    private boolean zzRB;
    private final zzaie zzRv;
    private final com.google.android.gms.ads.internal.zzbb zzRw;
    private boolean zzRx;
    private List<String> zzRy;
    private JSONObject zzRz;
    private final zznb zzsK;
    private zzyh zzuP;

    public zzyn(Context context, com.google.android.gms.ads.internal.zzbb zzbbVar, zzaie zzaieVar, zzcu zzcuVar, zzafg zzafgVar, zznb zznbVar) {
        this.mContext = context;
        this.zzRw = zzbbVar;
        this.zzRv = zzaieVar;
        this.zzQQ = zzafgVar;
        this.zzIc = zzcuVar;
        this.zzsK = zznbVar;
        if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFu)).booleanValue()) {
            this.zzuP = zzbbVar.zzbi();
        }
        if (this.zzuP == null) {
            this.zzuP = new zzyh(context, zzafgVar, zzbbVar, zzcuVar);
            this.zzuP.zzgs();
            this.zzRB = true;
        }
        this.zzRx = false;
        this.mErrorCode = -2;
        this.zzRy = null;
        this.zzRA = null;
    }

    private final zzaff zza(zzoa zzoaVar) {
        int i;
        synchronized (this.mLock) {
            i = this.mErrorCode;
            if (zzoaVar == null && this.mErrorCode == -2) {
                i = 0;
            }
        }
        return new zzaff(this.zzQQ.zzUj.zzSz, null, this.zzQQ.zzXY.zzMa, i, this.zzQQ.zzXY.zzMb, this.zzRy, this.zzQQ.zzXY.orientation, this.zzQQ.zzXY.zzMg, this.zzQQ.zzUj.zzSC, false, null, null, null, null, null, 0L, this.zzQQ.zzvX, this.zzQQ.zzXY.zzTn, this.zzQQ.zzXR, this.zzQQ.zzXS, this.zzQQ.zzXY.zzTt, this.zzRz, i != -2 ? null : zzoaVar, null, null, null, this.zzQQ.zzXY.zzTG, this.zzQQ.zzXY.zzTH, null, this.zzQQ.zzXY.zzMd, this.zzRA, this.zzQQ.zzXX);
    }

    private final zzajm<zznp> zza(JSONObject jSONObject, boolean z, boolean z2) throws JSONException {
        String string = z ? jSONObject.getString("url") : jSONObject.optString("url");
        double dOptDouble = jSONObject.optDouble("scale", 1.0d);
        boolean zOptBoolean = jSONObject.optBoolean("is_transparent", true);
        if (!TextUtils.isEmpty(string)) {
            return z2 ? new zzajh(new zznp(null, Uri.parse(string), dOptDouble)) : this.zzRv.zza(string, new zzyu(this, z, dOptDouble, zOptBoolean, string));
        }
        zzc(0, z);
        return new zzajh(null);
    }

    static zzaka zzb(zzajm<zzaka> zzajmVar) {
        try {
            return zzajmVar.get(((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFz)).intValue(), TimeUnit.SECONDS);
        } catch (InterruptedException e) {
            zzafr.zzc("InterruptedException occurred while waiting for video to load", e);
            Thread.currentThread().interrupt();
            return null;
        } catch (CancellationException e2) {
            e = e2;
            zzafr.zzc("Exception occurred while waiting for video to load", e);
            return null;
        } catch (ExecutionException e3) {
            e = e3;
            zzafr.zzc("Exception occurred while waiting for video to load", e);
            return null;
        } catch (TimeoutException e4) {
            e = e4;
            zzafr.zzc("Exception occurred while waiting for video to load", e);
            return null;
        }
    }

    private static Integer zzb(JSONObject jSONObject, String str) {
        try {
            JSONObject jSONObject2 = jSONObject.getJSONObject(str);
            return Integer.valueOf(Color.rgb(jSONObject2.getInt("r"), jSONObject2.getInt("g"), jSONObject2.getInt("b")));
        } catch (JSONException e) {
            return null;
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzc(zzpj zzpjVar, String str) {
        try {
            zzpt zzptVarZzs = this.zzRw.zzs(zzpjVar.getCustomTemplateId());
            if (zzptVarZzs != null) {
                zzptVarZzs.zzb(zzpjVar, str);
            }
        } catch (RemoteException e) {
            zzafr.zzc(new StringBuilder(String.valueOf(str).length() + 40).append("Failed to call onCustomClick for asset ").append(str).append(".").toString(), e);
        }
    }

    private static String[] zzd(JSONObject jSONObject, String str) throws JSONException {
        JSONArray jSONArrayOptJSONArray = jSONObject.optJSONArray(str);
        if (jSONArrayOptJSONArray == null) {
            return null;
        }
        String[] strArr = new String[jSONArrayOptJSONArray.length()];
        for (int i = 0; i < jSONArrayOptJSONArray.length(); i++) {
            strArr[i] = jSONArrayOptJSONArray.getString(i);
        }
        return strArr;
    }

    /* JADX INFO: Access modifiers changed from: private */
    /* JADX WARN: Code duplicated, block: B:53:0x0113  */
    @Override // java.util.concurrent.Callable
    /* JADX INFO: renamed from: zzgw, reason: merged with bridge method [inline-methods] */
    public final zzaff call() {
        JSONObject jSONObject;
        zzyv zzzhVar;
        zzoa zzoaVar;
        try {
            if (this.zzRB) {
                this.zzuP.zzgt();
            }
            String string = UUID.randomUUID().toString();
            if (zzgx()) {
                jSONObject = null;
            } else {
                zzajg zzajgVar = new zzajg();
                this.zzuP.zza(new zzyo(this, string, new zzyw(this), zzajgVar));
                jSONObject = (JSONObject) zzajgVar.get(zzRj, TimeUnit.MILLISECONDS);
            }
            if (zzgx() || jSONObject == null) {
                zzzhVar = null;
            } else {
                String string2 = jSONObject.getString("template_id");
                boolean z = this.zzQQ.zzUj.zzwj != null ? this.zzQQ.zzUj.zzwj.zzIn : false;
                boolean z2 = this.zzQQ.zzUj.zzwj != null ? this.zzQQ.zzUj.zzwj.zzIp : false;
                if ("2".equals(string2)) {
                    zzzhVar = new zzzf(z, z2);
                } else if ("1".equals(string2)) {
                    zzzhVar = new zzzg(z, z2);
                } else {
                    if ("3".equals(string2)) {
                        String string3 = jSONObject.getString("custom_template_id");
                        zzajg zzajgVar2 = new zzajg();
                        zzagz.zzZr.post(new zzyq(this, zzajgVar2, string3));
                        if (zzajgVar2.get(zzRj, TimeUnit.MILLISECONDS) != null) {
                            zzzhVar = new zzzh(z);
                        } else {
                            String strValueOf = String.valueOf(jSONObject.getString("custom_template_id"));
                            zzafr.e(strValueOf.length() != 0 ? "No handler for custom template: ".concat(strValueOf) : new String("No handler for custom template: "));
                        }
                    } else {
                        zzt(0);
                    }
                    zzzhVar = null;
                }
            }
            if (zzgx() || zzzhVar == null || jSONObject == null) {
                zzoaVar = null;
            } else {
                JSONObject jSONObject2 = jSONObject.getJSONObject("tracking_urls_and_actions");
                String[] strArrZzd = zzd(jSONObject2, "impression_tracking_urls");
                this.zzRy = strArrZzd == null ? null : Arrays.asList(strArrZzd);
                this.zzRz = jSONObject2.optJSONObject("active_view");
                this.zzRA = jSONObject.optString("debug_signals");
                zzoa zzoaVarZza = zzzhVar.zza(this, jSONObject);
                zzoaVarZza.zzb(new zzoc(this.mContext, this.zzRw, this.zzuP, this.zzIc, jSONObject, zzoaVarZza, this.zzQQ.zzUj.zzvT, string));
                zzoaVar = zzoaVarZza;
            }
            if (zzoaVar instanceof zznu) {
                zzyw zzywVar = new zzyw(this);
                zzyr zzyrVar = new zzyr(this, (zznu) zzoaVar);
                zzywVar.zzRW = zzyrVar;
                this.zzuP.zza(new zzys(this, zzyrVar));
            }
            return zza(zzoaVar);
        } catch (InterruptedException e) {
            if (!this.zzRx) {
                zzt(0);
            }
            return zza((zzoa) null);
        } catch (CancellationException e2) {
            if (!this.zzRx) {
                zzt(0);
            }
            return zza((zzoa) null);
        } catch (ExecutionException e3) {
            if (!this.zzRx) {
                zzt(0);
            }
            return zza((zzoa) null);
        } catch (TimeoutException e4) {
            zzafr.zzc("Timeout when loading native ad.", e4);
            if (!this.zzRx) {
                zzt(0);
            }
            return zza((zzoa) null);
        } catch (JSONException e5) {
            zzafr.zzc("Malformed native JSON response.", e5);
            if (!this.zzRx) {
                zzt(0);
            }
            return zza((zzoa) null);
        } catch (Exception e6) {
            zzafr.zzc("Error occured while doing native ads initialization.", e6);
            if (!this.zzRx) {
                zzt(0);
            }
            return zza((zzoa) null);
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static List<Drawable> zzj(List<zznp> list) throws RemoteException {
        ArrayList arrayList = new ArrayList();
        Iterator<zznp> it = list.iterator();
        while (it.hasNext()) {
            arrayList.add((Drawable) com.google.android.gms.dynamic.zzn.zzE(it.next().zzeg()));
        }
        return arrayList;
    }

    public final zzajm<zznp> zza(JSONObject jSONObject, String str, boolean z, boolean z2) throws JSONException {
        JSONObject jSONObject2 = z ? jSONObject.getJSONObject(str) : jSONObject.optJSONObject(str);
        if (jSONObject2 == null) {
            jSONObject2 = new JSONObject();
        }
        return zza(jSONObject2, z, z2);
    }

    public final List<zzajm<zznp>> zza(JSONObject jSONObject, String str, boolean z, boolean z2, boolean z3) throws JSONException {
        JSONArray jSONArrayOptJSONArray = jSONObject.optJSONArray(str);
        ArrayList arrayList = new ArrayList();
        if (jSONArrayOptJSONArray == null || jSONArrayOptJSONArray.length() == 0) {
            zzc(0, false);
            return arrayList;
        }
        int length = z3 ? jSONArrayOptJSONArray.length() : 1;
        for (int i = 0; i < length; i++) {
            JSONObject jSONObject2 = jSONArrayOptJSONArray.getJSONObject(i);
            if (jSONObject2 == null) {
                jSONObject2 = new JSONObject();
            }
            arrayList.add(zza(jSONObject2, false, z2));
        }
        return arrayList;
    }

    public final Future<zznp> zza(JSONObject jSONObject, String str, boolean z) throws JSONException {
        JSONObject jSONObject2 = jSONObject.getJSONObject(str);
        boolean zOptBoolean = jSONObject2.optBoolean("require", true);
        if (jSONObject2 == null) {
            jSONObject2 = new JSONObject();
        }
        return zza(jSONObject2, zOptBoolean, z);
    }

    public final zzajm<zzaka> zzc(JSONObject jSONObject, String str) throws JSONException {
        JSONObject jSONObjectOptJSONObject = jSONObject.optJSONObject(str);
        if (jSONObjectOptJSONObject == null) {
            return new zzajh(null);
        }
        if (TextUtils.isEmpty(jSONObjectOptJSONObject.optString("vast_xml"))) {
            zzafr.zzaT("Required field 'vast_xml' is missing");
            return new zzajh(null);
        }
        zzyx zzyxVar = new zzyx(this.mContext, this.zzIc, this.zzQQ, this.zzsK, this.zzRw);
        zzajg zzajgVar = new zzajg();
        com.google.android.gms.ads.internal.zzbs.zzbz();
        zzagz.runOnUiThread(new zzyy(zzyxVar, jSONObjectOptJSONObject, zzajgVar));
        return zzajgVar;
    }

    public final void zzc(int i, boolean z) {
        if (z) {
            zzt(i);
        }
    }

    public final zzajm<zznn> zzd(JSONObject jSONObject) throws JSONException {
        JSONObject jSONObjectOptJSONObject = jSONObject.optJSONObject("attribution");
        if (jSONObjectOptJSONObject == null) {
            return new zzajh(null);
        }
        String strOptString = jSONObjectOptJSONObject.optString("text");
        int iOptInt = jSONObjectOptJSONObject.optInt("text_size", -1);
        Integer numZzb = zzb(jSONObjectOptJSONObject, "text_color");
        Integer numZzb2 = zzb(jSONObjectOptJSONObject, "bg_color");
        int iOptInt2 = jSONObjectOptJSONObject.optInt("animation_ms", 1000);
        int iOptInt3 = jSONObjectOptJSONObject.optInt("presentation_ms", 4000);
        int i = (this.zzQQ.zzUj.zzwj == null || this.zzQQ.zzUj.zzwj.versionCode < 2) ? 1 : this.zzQQ.zzUj.zzwj.zzIq;
        boolean zOptBoolean = jSONObjectOptJSONObject.optBoolean("allow_pub_rendering");
        List<zzajm<zznp>> arrayList = new ArrayList<>();
        if (jSONObjectOptJSONObject.optJSONArray("images") != null) {
            arrayList = zza(jSONObjectOptJSONObject, "images", false, false, true);
        } else {
            arrayList.add(zza(jSONObjectOptJSONObject, "image", false, false));
        }
        return zzaji.zza(zzaji.zzp(arrayList), (zzajl) new zzyt(this, strOptString, numZzb2, numZzb, iOptInt, iOptInt3, iOptInt2, i, zOptBoolean));
    }

    public final boolean zzgx() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzRx;
        }
        return z;
    }

    public final void zzt(int i) {
        synchronized (this.mLock) {
            this.zzRx = true;
            this.mErrorCode = i;
        }
    }
}
