package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.content.Context;
import android.content.res.Resources;
import android.os.Build;
import android.os.Bundle;
import android.os.Looper;
import android.os.Parcelable;
import android.security.NetworkSecurityPolicy;
import android.support.v4.view.MotionEventCompat;
import com.google.android.gms.dynamite.DynamiteModule;
import com.google.android.gms.dynamite.descriptors.com.google.android.gms.ads.dynamite.ModuleDescriptor;
import java.math.BigInteger;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.concurrent.Future;
import java.util.concurrent.atomic.AtomicInteger;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzafk implements zzags, zzgy {
    private Context mContext;
    private String zzJP;
    private String zzYy;
    private String zzYz;
    private zzfh zzsR;
    private zzaje zztW;
    private final Object mLock = new Object();
    private BigInteger zzYr = BigInteger.ONE;
    private final HashSet<zzafh> zzYs = new HashSet<>();
    private final HashMap<String, zzafn> zzYt = new HashMap<>();
    private boolean zzYu = false;
    private boolean zzVf = true;
    private int zzYv = 0;
    private boolean zzuH = false;
    private zzmr zzYw = null;
    private boolean zzVg = true;
    private boolean zzVh = true;
    private zzgz zzYx = null;
    private zzgu zzyy = null;
    private Boolean zzYA = null;
    private boolean zzYB = false;
    private boolean zzYC = false;
    private boolean zzVp = false;
    private boolean zzYD = false;
    private String zzYE = "";
    private long zzYF = 0;
    private long zzYG = 0;
    private long zzYH = 0;
    private int zzYI = -1;
    private JSONObject zzYJ = new JSONObject();
    private int zzYK = 0;
    private final AtomicInteger zzYL = new AtomicInteger(0);
    private final String mSessionId = zzagz.zzhP();
    private final zzafl zzYq = new zzafl(this.mSessionId);

    public zzafk(zzagz zzagzVar) {
    }

    private final Future zzj(long j) {
        Future futureZza;
        synchronized (this.mLock) {
            this.zzYG = j;
            futureZza = zzaft.zza(this.mContext, j);
        }
        return futureZza;
    }

    private final Future zzy(int i) {
        Future futureZzb;
        synchronized (this.mLock) {
            this.zzYI = i;
            futureZzb = zzaft.zzb(this.mContext, i);
        }
        return futureZzb;
    }

    public final Resources getResources() {
        if (this.zztW.zzaaQ) {
            return this.mContext.getResources();
        }
        try {
            DynamiteModule dynamiteModuleZza = DynamiteModule.zza(this.mContext, DynamiteModule.zzaSL, ModuleDescriptor.MODULE_ID);
            if (dynamiteModuleZza != null) {
                return dynamiteModuleZza.zztC().getResources();
            }
            return null;
        } catch (DynamiteModule.zzc e) {
            zzafr.zzc("Cannot load resource from dynamite apk or local jar", e);
            return null;
        }
    }

    public final String getSessionId() {
        return this.mSessionId;
    }

    public final zzgz zzA(Context context) {
        if (!((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCT)).booleanValue()) {
            return null;
        }
        if (!((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDb)).booleanValue()) {
            if (!((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCZ)).booleanValue()) {
                return null;
            }
        }
        if (zzhn() && zzho()) {
            return null;
        }
        synchronized (this.mLock) {
            if (Looper.getMainLooper() == null || context == null) {
                return null;
            }
            if (this.zzyy == null) {
                this.zzyy = new zzgu();
            }
            if (this.zzYx == null) {
                this.zzYx = new zzgz(this.zzyy, zzzi.zzc(this.mContext, this.zztW));
            }
            this.zzYx.zzcM();
            return this.zzYx;
        }
    }

    public final Bundle zza(Context context, zzafm zzafmVar, String str) {
        Bundle bundle;
        synchronized (this.mLock) {
            bundle = new Bundle();
            bundle.putBundle("app", this.zzYq.zzo(context, str));
            Bundle bundle2 = new Bundle();
            for (String str2 : this.zzYt.keySet()) {
                bundle2.putBundle(str2, this.zzYt.get(str2).toBundle());
            }
            bundle.putBundle("slots", bundle2);
            ArrayList<? extends Parcelable> arrayList = new ArrayList<>();
            Iterator<zzafh> it = this.zzYs.iterator();
            while (it.hasNext()) {
                arrayList.add(it.next().toBundle());
            }
            bundle.putParcelableArrayList("ads", arrayList);
            zzafmVar.zza(this.zzYs);
            this.zzYs.clear();
        }
        return bundle;
    }

    public final void zza(zzafh zzafhVar) {
        synchronized (this.mLock) {
            this.zzYs.add(zzafhVar);
        }
    }

    public final void zza(Boolean bool) {
        synchronized (this.mLock) {
            this.zzYA = bool;
        }
    }

    public final void zza(String str, zzafn zzafnVar) {
        synchronized (this.mLock) {
            this.zzYt.put(str, zzafnVar);
        }
    }

    public final void zza(Throwable th, String str) {
        zzzi.zzc(this.mContext, this.zztW).zza(th, str);
    }

    /* JADX WARN: Code duplicated, block: B:10:0x0017  */
    public final Future zzaF(String str) {
        Future futureZzp;
        synchronized (this.mLock) {
            if (str == null) {
                futureZzp = null;
            } else if (str.equals(this.zzYy)) {
                futureZzp = null;
            } else {
                this.zzYy = str;
                futureZzp = zzaft.zzp(this.mContext, str);
            }
            throw th;
        }
        return futureZzp;
    }

    /* JADX WARN: Code duplicated, block: B:10:0x0017  */
    public final Future zzaG(String str) {
        Future futureZzq;
        synchronized (this.mLock) {
            if (str == null) {
                futureZzq = null;
            } else if (str.equals(this.zzYz)) {
                futureZzq = null;
            } else {
                this.zzYz = str;
                futureZzq = zzaft.zzq(this.mContext, str);
            }
            throw th;
        }
        return futureZzq;
    }

    public final Future zzb(Context context, String str, String str2, boolean z) {
        Future futureZzr;
        int i = 0;
        synchronized (this.mLock) {
            JSONArray jSONArrayOptJSONArray = this.zzYJ.optJSONArray(str);
            JSONArray jSONArray = jSONArrayOptJSONArray == null ? new JSONArray() : jSONArrayOptJSONArray;
            int length = jSONArray.length();
            while (true) {
                if (i >= jSONArray.length()) {
                    i = length;
                    break;
                }
                JSONObject jSONObjectOptJSONObject = jSONArray.optJSONObject(i);
                if (jSONObjectOptJSONObject == null || !str2.equals(jSONObjectOptJSONObject.optString("template_id"))) {
                    i++;
                } else {
                    if (!z || jSONObjectOptJSONObject.optBoolean("uses_media_view", false) != z) {
                        break;
                    }
                    futureZzr = null;
                }
            }
            try {
                JSONObject jSONObject = new JSONObject();
                jSONObject.put("template_id", str2);
                jSONObject.put("uses_media_view", z);
                jSONObject.put("timestamp_ms", com.google.android.gms.ads.internal.zzbs.zzbF().currentTimeMillis());
                jSONArray.put(i, jSONObject);
                this.zzYJ.put(str, jSONArray);
            } catch (JSONException e) {
                zzafr.zzc("Could not update native advanced settings", e);
            }
            futureZzr = zzaft.zzr(this.mContext, this.zzYJ.toString());
        }
        return futureZzr;
    }

    public final void zzb(HashSet<zzafh> hashSet) {
        synchronized (this.mLock) {
            this.zzYs.addAll(hashSet);
        }
    }

    @TargetApi(MotionEventCompat.AXIS_BRAKE)
    public final void zzd(Context context, zzaje zzajeVar) {
        synchronized (this.mLock) {
            if (!this.zzuH) {
                this.mContext = context.getApplicationContext();
                this.zztW = zzajeVar;
                com.google.android.gms.ads.internal.zzbs.zzbC().zza(this);
                zzaft.zza(context, this);
                zzaft.zzb(context, this);
                zzaft.zzh(context, this);
                zzaft.zzf(context, this);
                zzaft.zzc(context, this);
                zzaft.zzd(context, this);
                zzaft.zze(context, this);
                zzaft.zzg(context, this);
                zzaft.zzi(context, this);
                zzaft.zzj(context, this);
                zzaft.zzk(context, this);
                zzzi.zzc(this.mContext, this.zztW);
                this.zzJP = com.google.android.gms.ads.internal.zzbs.zzbz().zzs(context, zzajeVar.zzaP);
                if (Build.VERSION.SDK_INT >= 23 && !NetworkSecurityPolicy.getInstance().isCleartextTrafficPermitted()) {
                    this.zzYC = true;
                }
                this.zzsR = new zzfh(context.getApplicationContext(), this.zztW, com.google.android.gms.ads.internal.zzbs.zzbz().zze(context, zzajeVar));
                zzmq zzmqVar = new zzmq(this.mContext, this.zztW.zzaP);
                try {
                    com.google.android.gms.ads.internal.zzbs.zzbG();
                    this.zzYw = zzmt.zza(zzmqVar);
                } catch (IllegalArgumentException e) {
                    zzafr.zzc("Cannot initialize CSI reporter.", e);
                }
                this.zzuH = true;
            }
        }
    }

    public final Future zze(Context context, boolean z) {
        Future futureZze;
        synchronized (this.mLock) {
            if (z != this.zzVf) {
                this.zzVf = z;
                futureZze = zzaft.zze(context, z);
            } else {
                futureZze = null;
            }
        }
        return futureZze;
    }

    public final Future zzf(Context context, boolean z) {
        Future futureZzh;
        synchronized (this.mLock) {
            if (z != this.zzVp) {
                this.zzVp = z;
                futureZzh = zzaft.zzh(context, z);
            } else {
                futureZzh = null;
            }
        }
        return futureZzh;
    }

    /* JADX WARN: Code duplicated, block: B:21:0x0080 A[Catch: all -> 0x00c9, TryCatch #1 {, blocks: (B:4:0x0003, B:6:0x001f, B:7:0x0028, B:9:0x0030, B:10:0x0038, B:12:0x004a, B:13:0x0053, B:15:0x005b, B:16:0x0063, B:18:0x006b, B:19:0x0078, B:21:0x0080, B:22:0x0088, B:24:0x0090, B:25:0x0096, B:26:0x00c0, B:33:0x00cc, B:29:0x00c3), top: B:37:0x0003, inners: #0 }] */
    /* JADX WARN: Code duplicated, block: B:24:0x0090 A[Catch: all -> 0x00c9, TryCatch #1 {, blocks: (B:4:0x0003, B:6:0x001f, B:7:0x0028, B:9:0x0030, B:10:0x0038, B:12:0x004a, B:13:0x0053, B:15:0x005b, B:16:0x0063, B:18:0x006b, B:19:0x0078, B:21:0x0080, B:22:0x0088, B:24:0x0090, B:25:0x0096, B:26:0x00c0, B:33:0x00cc, B:29:0x00c3), top: B:37:0x0003, inners: #0 }] */
    /* JADX WARN: Code duplicated, block: B:33:0x00cc A[Catch: all -> 0x00c9, TRY_ENTER, TRY_LEAVE, TryCatch #1 {, blocks: (B:4:0x0003, B:6:0x001f, B:7:0x0028, B:9:0x0030, B:10:0x0038, B:12:0x004a, B:13:0x0053, B:15:0x005b, B:16:0x0063, B:18:0x006b, B:19:0x0078, B:21:0x0080, B:22:0x0088, B:24:0x0090, B:25:0x0096, B:26:0x00c0, B:33:0x00cc, B:29:0x00c3), top: B:37:0x0003, inners: #0 }] */
    @Override // com.google.android.gms.internal.zzags
    public final void zzf(Bundle bundle) {
        String string;
        synchronized (this.mLock) {
            this.zzVf = bundle.getBoolean("use_https", this.zzVf);
            this.zzYv = bundle.getInt("webview_cache_version", this.zzYv);
            if (bundle.containsKey("content_url_opted_out")) {
                zzx(bundle.getBoolean("content_url_opted_out"));
            }
            if (bundle.containsKey("content_url_hashes")) {
                this.zzYy = bundle.getString("content_url_hashes");
            }
            this.zzVp = bundle.getBoolean("auto_collect_location", this.zzVp);
            if (bundle.containsKey("content_vertical_opted_out")) {
                zzy(bundle.getBoolean("content_vertical_opted_out"));
            }
            if (bundle.containsKey("content_vertical_hashes")) {
                this.zzYz = bundle.getString("content_vertical_hashes");
            }
            if (bundle.containsKey("native_advanced_settings")) {
                try {
                    this.zzYJ = new JSONObject(bundle.getString("native_advanced_settings"));
                } catch (JSONException e) {
                    zzafr.zzc("Could not convert native advanced settings to json object", e);
                }
                if (bundle.containsKey("version_code")) {
                    this.zzYK = bundle.getInt("version_code");
                }
                if (bundle.containsKey("app_settings_json")) {
                    string = bundle.getString("app_settings_json");
                } else {
                    string = this.zzYE;
                }
                this.zzYE = string;
                this.zzYF = bundle.getLong("app_settings_last_update_ms", this.zzYF);
                this.zzYG = bundle.getLong("app_last_background_time_ms", this.zzYG);
                this.zzYI = bundle.getInt("request_in_session_count", this.zzYI);
                this.zzYH = bundle.getLong("first_ad_req_time_ms", this.zzYH);
            } else {
                if (bundle.containsKey("version_code")) {
                    this.zzYK = bundle.getInt("version_code");
                }
                if (bundle.containsKey("app_settings_json")) {
                    string = bundle.getString("app_settings_json");
                } else {
                    string = this.zzYE;
                }
                this.zzYE = string;
                this.zzYF = bundle.getLong("app_settings_last_update_ms", this.zzYF);
                this.zzYG = bundle.getLong("app_last_background_time_ms", this.zzYG);
                this.zzYI = bundle.getInt("request_in_session_count", this.zzYI);
                this.zzYH = bundle.getLong("first_ad_req_time_ms", this.zzYH);
            }
            throw th;
        }
    }

    @Override // com.google.android.gms.internal.zzgy
    public final void zzf(boolean z) {
        long jCurrentTimeMillis = com.google.android.gms.ads.internal.zzbs.zzbF().currentTimeMillis();
        if (!z) {
            zzj(jCurrentTimeMillis);
            zzy(this.zzYq.zzYI);
            return;
        }
        if (jCurrentTimeMillis - this.zzYG > ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDL)).longValue()) {
            this.zzYq.zzYI = -1;
        } else {
            this.zzYq.zzYI = this.zzYI;
        }
    }

    public final int zzhA() {
        int i;
        synchronized (this.mLock) {
            i = this.zzYK;
        }
        return i;
    }

    final int zzhB() {
        int i;
        synchronized (this.mLock) {
            i = this.zzYI;
        }
        return i;
    }

    public final boolean zzhC() {
        return this.zzYD;
    }

    public final zzafj zzhD() {
        zzafj zzafjVar;
        synchronized (this.mLock) {
            zzafjVar = new zzafj(this.zzYE, this.zzYF);
        }
        return zzafjVar;
    }

    public final JSONObject zzhE() {
        JSONObject jSONObject;
        synchronized (this.mLock) {
            jSONObject = this.zzYJ;
        }
        return jSONObject;
    }

    public final Future zzhF() {
        Future futureZzC;
        synchronized (this.mLock) {
            futureZzC = zzaft.zzC(this.mContext);
        }
        return futureZzC;
    }

    public final zzfh zzhG() {
        return this.zzsR;
    }

    public final void zzhH() {
        this.zzYL.incrementAndGet();
    }

    public final void zzhI() {
        this.zzYL.decrementAndGet();
    }

    public final int zzhJ() {
        return this.zzYL.get();
    }

    public final boolean zzhn() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzVg;
        }
        return z;
    }

    public final boolean zzho() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzVh;
        }
        return z;
    }

    public final String zzhp() {
        String string;
        synchronized (this.mLock) {
            string = this.zzYr.toString();
            this.zzYr = this.zzYr.add(BigInteger.ONE);
        }
        return string;
    }

    public final zzafl zzhq() {
        zzafl zzaflVar;
        synchronized (this.mLock) {
            zzaflVar = this.zzYq;
        }
        return zzaflVar;
    }

    public final zzmr zzhr() {
        zzmr zzmrVar;
        synchronized (this.mLock) {
            zzmrVar = this.zzYw;
        }
        return zzmrVar;
    }

    public final boolean zzhs() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzVf || this.zzYC;
        }
        return z;
    }

    public final String zzht() {
        String str;
        synchronized (this.mLock) {
            str = this.zzJP;
        }
        return str;
    }

    public final String zzhu() {
        String str;
        synchronized (this.mLock) {
            str = this.zzYy;
        }
        return str;
    }

    public final String zzhv() {
        String str;
        synchronized (this.mLock) {
            str = this.zzYz;
        }
        return str;
    }

    public final Boolean zzhw() {
        Boolean bool;
        synchronized (this.mLock) {
            bool = this.zzYA;
        }
        return bool;
    }

    public final boolean zzhx() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzVp;
        }
        return z;
    }

    final long zzhy() {
        long j;
        synchronized (this.mLock) {
            j = this.zzYG;
        }
        return j;
    }

    final long zzhz() {
        long j;
        synchronized (this.mLock) {
            j = this.zzYH;
        }
        return j;
    }

    final Future zzk(long j) {
        Future futureZzb;
        synchronized (this.mLock) {
            this.zzYH = j;
            futureZzb = zzaft.zzb(this.mContext, j);
        }
        return futureZzb;
    }

    /* JADX WARN: Code duplicated, block: B:10:0x0021  */
    public final Future zzn(Context context, String str) {
        Future futureZza;
        this.zzYF = com.google.android.gms.ads.internal.zzbs.zzbF().currentTimeMillis();
        synchronized (this.mLock) {
            if (str == null) {
                futureZza = null;
            } else if (str.equals(this.zzYE)) {
                futureZza = null;
            } else {
                this.zzYE = str;
                futureZza = zzaft.zza(context, str, this.zzYF);
            }
            throw th;
        }
        return futureZza;
    }

    public final Future zzx(int i) {
        Future futureZza;
        synchronized (this.mLock) {
            this.zzYK = i;
            futureZza = zzaft.zza(this.mContext, i);
        }
        return futureZza;
    }

    public final void zzx(boolean z) {
        synchronized (this.mLock) {
            if (this.zzVg != z) {
                zzaft.zzg(this.mContext, z);
            }
            this.zzVg = z;
            zzgz zzgzVarZzA = zzA(this.mContext);
            if (zzgzVarZzA != null && !zzgzVarZzA.isAlive()) {
                zzafr.zzaS("start fetching content...");
                zzgzVarZzA.zzcM();
            }
        }
    }

    public final void zzy(boolean z) {
        synchronized (this.mLock) {
            if (this.zzVh != z) {
                zzaft.zzg(this.mContext, z);
            }
            zzaft.zzg(this.mContext, z);
            this.zzVh = z;
            zzgz zzgzVarZzA = zzA(this.mContext);
            if (zzgzVarZzA != null && !zzgzVarZzA.isAlive()) {
                zzafr.zzaS("start fetching content...");
                zzgzVarZzA.zzcM();
            }
        }
    }

    public final void zzz(boolean z) {
        this.zzYD = z;
    }
}
