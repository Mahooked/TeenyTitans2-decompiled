package com.google.android.gms.internal;

import android.content.Context;
import android.graphics.Bitmap;
import android.support.annotation.Nullable;
import android.support.annotation.VisibleForTesting;
import android.view.View;
import com.google.android.gms.common.internal.zzbo;
import java.io.UnsupportedEncodingException;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.LinkedList;
import java.util.Locale;
import java.util.Map;
import org.apache.http.protocol.HTTP;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaeg implements zzaet {
    private final Context mContext;
    private final Object mLock;
    private final zzaeq zzVu;
    private final adt zzWX;
    private final LinkedHashMap<String, aeb> zzWY;
    private final zzael zzWZ;

    @VisibleForTesting
    boolean zzXa;
    private HashSet<String> zzXb;
    private boolean zzXc;
    private boolean zzXd;
    private boolean zzXe;

    public zzaeg(Context context, zzaje zzajeVar, zzaai zzaaiVar) {
        this(context, zzajeVar, zzaaiVar, new zzael());
    }

    @VisibleForTesting
    private zzaeg(Context context, zzaje zzajeVar, zzaai zzaaiVar, zzael zzaelVar) {
        this.mLock = new Object();
        this.zzXb = new HashSet<>();
        this.zzXc = false;
        this.zzXd = false;
        this.zzXe = false;
        zzbo.zzb(zzaaiVar.zzTJ, "SafeBrowsing config is not present.");
        this.mContext = context.getApplicationContext() != null ? context.getApplicationContext() : context;
        this.zzWY = new LinkedHashMap<>();
        this.zzWZ = zzaelVar;
        this.zzVu = zzaaiVar.zzTJ;
        Iterator<String> it = this.zzVu.zzXv.iterator();
        while (it.hasNext()) {
            this.zzXb.add(it.next().toLowerCase(Locale.ENGLISH));
        }
        this.zzXb.remove("cookie".toLowerCase(Locale.ENGLISH));
        adt adtVar = new adt();
        adtVar.zzcsJ = 8;
        adtVar.url = zzaaiVar.zzPi;
        adtVar.zzcsL = zzaaiVar.zzPi;
        adtVar.zzcsN = new adu();
        adtVar.zzcsN.zzXr = this.zzVu.zzXr;
        aec aecVar = new aec();
        aecVar.zzctu = zzajeVar.zzaP;
        com.google.android.gms.common.zze.zzoW();
        long jZzau = com.google.android.gms.common.zze.zzau(this.mContext);
        if (jZzau > 0) {
            aecVar.zzctv = Long.valueOf(jZzau);
        }
        adtVar.zzcsX = aecVar;
        this.zzWX = adtVar;
    }

    @Nullable
    private final aeb zzaB(String str) {
        aeb aebVar;
        synchronized (this.mLock) {
            aebVar = this.zzWY.get(str);
        }
        return aebVar;
    }

    @VisibleForTesting
    final void send() {
        boolean z = true;
        if ((!this.zzXa || !this.zzVu.zzXx) && ((!this.zzXe || !this.zzVu.zzXw) && (this.zzXa || !this.zzVu.zzXu))) {
            z = false;
        }
        if (z) {
            synchronized (this.mLock) {
                this.zzWX.zzcsO = new aeb[this.zzWY.size()];
                this.zzWY.values().toArray(this.zzWX.zzcsO);
                if (zzaes.isEnabled()) {
                    String strValueOf = String.valueOf(this.zzWX.url);
                    String strValueOf2 = String.valueOf(this.zzWX.zzcsP);
                    StringBuilder sb = new StringBuilder(new StringBuilder(String.valueOf(strValueOf).length() + 53 + String.valueOf(strValueOf2).length()).append("Sending SB report\n  url: ").append(strValueOf).append("\n  clickUrl: ").append(strValueOf2).append("\n  resources: \n").toString());
                    for (aeb aebVar : this.zzWX.zzcsO) {
                        sb.append("    [");
                        sb.append(aebVar.zzctt.length);
                        sb.append("] ");
                        sb.append(aebVar.url);
                    }
                    zzaes.zzaC(sb.toString());
                }
                zzajm<String> zzajmVarZza = new zzaie(this.mContext).zza(1, this.zzVu.zzXs, null, adp.zzc(this.zzWX));
                if (zzaes.isEnabled()) {
                    zzajmVarZza.zzc(new zzaej(this));
                }
            }
        }
    }

    /* JADX WARN: Code duplicated, block: B:10:0x0013 A[Catch: all -> 0x00ac, TryCatch #1 {, blocks: (B:6:0x0007, B:7:0x0009, B:10:0x0013, B:11:0x0021, B:13:0x0023, B:16:0x004d, B:17:0x005a, B:19:0x0060, B:20:0x0066, B:22:0x006c, B:23:0x0072, B:25:0x0078, B:26:0x007e, B:28:0x008c, B:31:0x00a6, B:38:0x00b5, B:39:0x00c2, B:40:0x00c7), top: B:44:0x0007, inners: #0 }] */
    /* JADX WARN: Code duplicated, block: B:13:0x0023 A[Catch: all -> 0x00ac, TryCatch #1 {, blocks: (B:6:0x0007, B:7:0x0009, B:10:0x0013, B:11:0x0021, B:13:0x0023, B:16:0x004d, B:17:0x005a, B:19:0x0060, B:20:0x0066, B:22:0x006c, B:23:0x0072, B:25:0x0078, B:26:0x007e, B:28:0x008c, B:31:0x00a6, B:38:0x00b5, B:39:0x00c2, B:40:0x00c7), top: B:44:0x0007, inners: #0 }] */
    /* JADX WARN: Code duplicated, block: B:19:0x0060 A[Catch: all -> 0x00ac, TRY_LEAVE, TryCatch #1 {, blocks: (B:6:0x0007, B:7:0x0009, B:10:0x0013, B:11:0x0021, B:13:0x0023, B:16:0x004d, B:17:0x005a, B:19:0x0060, B:20:0x0066, B:22:0x006c, B:23:0x0072, B:25:0x0078, B:26:0x007e, B:28:0x008c, B:31:0x00a6, B:38:0x00b5, B:39:0x00c2, B:40:0x00c7), top: B:44:0x0007, inners: #0 }] */
    /* JADX WARN: Code duplicated, block: B:22:0x006c A[Catch: UnsupportedEncodingException -> 0x00a5, all -> 0x00ac, TryCatch #0 {UnsupportedEncodingException -> 0x00a5, blocks: (B:20:0x0066, B:22:0x006c, B:23:0x0072, B:25:0x0078, B:26:0x007e, B:28:0x008c), top: B:42:0x0066, outer: #1 }] */
    /* JADX WARN: Code duplicated, block: B:25:0x0078 A[Catch: UnsupportedEncodingException -> 0x00a5, all -> 0x00ac, TryCatch #0 {UnsupportedEncodingException -> 0x00a5, blocks: (B:20:0x0066, B:22:0x006c, B:23:0x0072, B:25:0x0078, B:26:0x007e, B:28:0x008c), top: B:42:0x0066, outer: #1 }] */
    /* JADX WARN: Code duplicated, block: B:35:0x00af  */
    /* JADX WARN: Code duplicated, block: B:36:0x00b2  */
    /* JADX WARN: Code duplicated, block: B:46:0x008c A[SYNTHETIC] */
    /* JADX WARN: Code duplicated, block: B:49:0x005a A[SYNTHETIC] */
    /* JADX WARN: Code duplicated, block: B:9:0x0011 A[DONT_INVERT] */
    @Override // com.google.android.gms.internal.zzaet
    public final void zza(String str, Map<String, String> map, int i) {
        aeb aebVar;
        LinkedList linkedList;
        String key;
        String value;
        synchronized (this.mLock) {
            if (i != 3) {
                if (this.zzWY.containsKey(str)) {
                    if (i == 3) {
                        this.zzWY.get(str).zzcts = Integer.valueOf(i);
                    }
                    return;
                }
                aebVar = new aeb();
                aebVar.zzcts = Integer.valueOf(i);
                aebVar.zzbuM = Integer.valueOf(this.zzWY.size());
                aebVar.url = str;
                aebVar.zzctn = new adw();
                if (this.zzXb.size() > 0) {
                    linkedList = new LinkedList();
                    for (Map.Entry<String, String> entry : map.entrySet()) {
                        if (entry.getKey() != null) {
                            key = entry.getKey();
                        } else {
                            key = "";
                        }
                        if (entry.getValue() != null) {
                            value = entry.getValue();
                        } else {
                            value = "";
                        }
                        if (this.zzXb.contains(key.toLowerCase(Locale.ENGLISH))) {
                            adv advVar = new adv();
                            advVar.zzcsZ = key.getBytes(HTTP.UTF_8);
                            advVar.zzcnR = value.getBytes(HTTP.UTF_8);
                            linkedList.add(advVar);
                        }
                    }
                    adv[] advVarArr = new adv[linkedList.size()];
                    linkedList.toArray(advVarArr);
                    aebVar.zzctn.zzctb = advVarArr;
                }
                this.zzWY.put(str, aebVar);
                return;
            }
            this.zzXe = true;
            if (this.zzWY.containsKey(str)) {
                if (i == 3) {
                    this.zzWY.get(str).zzcts = Integer.valueOf(i);
                }
                return;
            }
            aebVar = new aeb();
            aebVar.zzcts = Integer.valueOf(i);
            aebVar.zzbuM = Integer.valueOf(this.zzWY.size());
            aebVar.url = str;
            aebVar.zzctn = new adw();
            if (this.zzXb.size() > 0 && map != null) {
                linkedList = new LinkedList();
                while (r5.hasNext()) {
                    try {
                        if (entry.getKey() != null) {
                            key = entry.getKey();
                        } else {
                            key = "";
                        }
                        if (entry.getValue() != null) {
                            value = entry.getValue();
                        } else {
                            value = "";
                        }
                        if (this.zzXb.contains(key.toLowerCase(Locale.ENGLISH))) {
                            adv advVar2 = new adv();
                            advVar2.zzcsZ = key.getBytes(HTTP.UTF_8);
                            advVar2.zzcnR = value.getBytes(HTTP.UTF_8);
                            linkedList.add(advVar2);
                        }
                    } catch (UnsupportedEncodingException e) {
                        zzaes.zzaC("Cannot convert string to bytes, skip header.");
                    }
                }
                adv[] advVarArr2 = new adv[linkedList.size()];
                linkedList.toArray(advVarArr2);
                aebVar.zzctn.zzctb = advVarArr2;
            }
            this.zzWY.put(str, aebVar);
            return;
            throw th;
        }
    }

    @Override // com.google.android.gms.internal.zzaet
    public final void zzaA(String str) {
        synchronized (this.mLock) {
            this.zzWX.zzcsP = str;
        }
    }

    @Override // com.google.android.gms.internal.zzaet
    public final zzaeq zzgY() {
        return this.zzVu;
    }

    @Override // com.google.android.gms.internal.zzaet
    public final boolean zzgZ() {
        return com.google.android.gms.common.util.zzq.zzsc() && this.zzVu.zzXt && !this.zzXd;
    }

    @Override // com.google.android.gms.internal.zzaet
    public final void zzha() {
        this.zzXc = true;
    }

    @Override // com.google.android.gms.internal.zzaet
    public final void zzhb() {
        synchronized (this.mLock) {
            zzajm<Map<String, String>> zzajmVarZza = this.zzWZ.zza(this.mContext, this.zzWY.keySet());
            zzajmVarZza.zzc(new zzaei(this, zzajmVarZza));
        }
    }

    @VisibleForTesting
    final void zzi(@Nullable Map<String, String> map) throws JSONException {
        if (map == null) {
            return;
        }
        for (String str : map.keySet()) {
            JSONArray jSONArrayOptJSONArray = new JSONObject(map.get(str)).optJSONArray("matches");
            if (jSONArrayOptJSONArray != null) {
                synchronized (this.mLock) {
                    int length = jSONArrayOptJSONArray.length();
                    aeb aebVarZzaB = zzaB(str);
                    if (aebVarZzaB == null) {
                        String strValueOf = String.valueOf(str);
                        zzaes.zzaC(strValueOf.length() != 0 ? "Cannot find the corresponding resource object for ".concat(strValueOf) : new String("Cannot find the corresponding resource object for "));
                    } else {
                        aebVarZzaB.zzctt = new String[length];
                        for (int i = 0; i < length; i++) {
                            aebVarZzaB.zzctt[i] = jSONArrayOptJSONArray.getJSONObject(i).getString("threat_type");
                        }
                        this.zzXa = (length > 0) | this.zzXa;
                    }
                }
            }
        }
    }

    @Override // com.google.android.gms.internal.zzaet
    public final void zzk(View view) {
        if (this.zzVu.zzXt && !this.zzXd) {
            com.google.android.gms.ads.internal.zzbs.zzbz();
            Bitmap bitmapZzm = zzagz.zzm(view);
            if (bitmapZzm == null) {
                zzaes.zzaC("Failed to capture the webview bitmap.");
            } else {
                this.zzXd = true;
                zzagz.zzb(new zzaeh(this, bitmapZzm));
            }
        }
    }
}
