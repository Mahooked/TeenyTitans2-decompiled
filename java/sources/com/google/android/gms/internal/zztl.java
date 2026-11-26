package com.google.android.gms.internal;

import android.content.SharedPreferences;
import android.os.Bundle;
import android.os.Parcel;
import android.support.annotation.Nullable;
import android.util.Base64;
import com.fasterxml.jackson.core.util.MinimalPrettyPrinter;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.Map;
import java.util.Set;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zztl {
    private final Map<zztm, zztn> zzKo = new HashMap();
    private final LinkedList<zztm> zzKp = new LinkedList<>();

    @Nullable
    private zzsi zzKq;

    private static String[] zzY(String str) {
        try {
            String[] strArrSplit = str.split("\u0000");
            for (int i = 0; i < strArrSplit.length; i++) {
                strArrSplit[i] = new String(Base64.decode(strArrSplit[i], 0), HTTP.UTF_8);
            }
            return strArrSplit;
        } catch (UnsupportedEncodingException e) {
            return new String[0];
        }
    }

    private static boolean zzZ(String str) {
        try {
            return Pattern.matches((String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEf), str);
        } catch (RuntimeException e) {
            com.google.android.gms.ads.internal.zzbs.zzbD().zza(e, "InterstitialAdPool.isExcludedAdUnit");
            return false;
        }
    }

    private static void zza(String str, zztm zztmVar) {
        if (zzafr.zzz(2)) {
            zzafr.v(String.format(str, zztmVar));
        }
    }

    private static String zzaa(String str) {
        try {
            Matcher matcher = Pattern.compile("([^/]+/[0-9]+).*").matcher(str);
            return matcher.matches() ? matcher.group(1) : str;
        } catch (RuntimeException e) {
            return str;
        }
    }

    private static void zzc(Bundle bundle, String str) {
        while (true) {
            String[] strArrSplit = str.split("/", 2);
            if (strArrSplit.length == 0) {
                return;
            }
            String str2 = strArrSplit[0];
            if (strArrSplit.length == 1) {
                bundle.remove(str2);
                return;
            }
            bundle = bundle.getBundle(str2);
            if (bundle == null) {
                return;
            } else {
                str = strArrSplit[1];
            }
        }
    }

    private final String zzeH() {
        try {
            StringBuilder sb = new StringBuilder();
            Iterator<zztm> it = this.zzKp.iterator();
            while (it.hasNext()) {
                sb.append(Base64.encodeToString(it.next().toString().getBytes(HTTP.UTF_8), 0));
                if (it.hasNext()) {
                    sb.append("\u0000");
                }
            }
            return sb.toString();
        } catch (UnsupportedEncodingException e) {
            return "";
        }
    }

    static Set<String> zzi(zzir zzirVar) {
        HashSet hashSet = new HashSet();
        hashSet.addAll(zzirVar.extras.keySet());
        Bundle bundle = zzirVar.zzzX.getBundle("com.google.ads.mediation.admob.AdMobAdapter");
        if (bundle != null) {
            hashSet.addAll(bundle.keySet());
        }
        return hashSet;
    }

    static zzir zzj(zzir zzirVar) {
        zzir zzirVarZzl = zzl(zzirVar);
        Bundle bundle = zzirVarZzl.zzzX.getBundle("com.google.ads.mediation.admob.AdMobAdapter");
        if (bundle != null) {
            bundle.putBoolean("_skipMediation", true);
        }
        zzirVarZzl.extras.putBoolean("_skipMediation", true);
        return zzirVarZzl;
    }

    private static zzir zzk(zzir zzirVar) {
        zzir zzirVarZzl = zzl(zzirVar);
        for (String str : ((String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEb)).split(",")) {
            zzc(zzirVarZzl.zzzX, str);
            if (str.startsWith("com.google.ads.mediation.admob.AdMobAdapter/")) {
                zzc(zzirVarZzl.extras, str.replaceFirst("com.google.ads.mediation.admob.AdMobAdapter/", ""));
            }
        }
        return zzirVarZzl;
    }

    private static zzir zzl(zzir zzirVar) {
        Parcel parcelObtain = Parcel.obtain();
        zzirVar.writeToParcel(parcelObtain, 0);
        parcelObtain.setDataPosition(0);
        zzir zzirVarCreateFromParcel = zzir.CREATOR.createFromParcel(parcelObtain);
        parcelObtain.recycle();
        if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDR)).booleanValue()) {
            zzir.zzh(zzirVarCreateFromParcel);
        }
        return zzirVarCreateFromParcel;
    }

    @Nullable
    final zzto zza(zzir zzirVar, String str) {
        zztn zztnVar;
        if (zzZ(str)) {
            return null;
        }
        int i = new zzacc(this.zzKq.getApplicationContext()).zzgM().zzVS;
        zzir zzirVarZzk = zzk(zzirVar);
        String strZzaa = zzaa(str);
        zztm zztmVar = new zztm(zzirVarZzk, strZzaa, i);
        zztn zztnVar2 = this.zzKo.get(zztmVar);
        if (zztnVar2 == null) {
            zza("Interstitial pool created at %s.", zztmVar);
            zztn zztnVar3 = new zztn(zzirVarZzk, strZzaa, i);
            this.zzKo.put(zztmVar, zztnVar3);
            zztnVar = zztnVar3;
        } else {
            zztnVar = zztnVar2;
        }
        this.zzKp.remove(zztmVar);
        this.zzKp.add(zztmVar);
        zztnVar.zzeL();
        while (this.zzKp.size() > ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEc)).intValue()) {
            zztm zztmVarRemove = this.zzKp.remove();
            zztn zztnVar4 = this.zzKo.get(zztmVarRemove);
            zza("Evicting interstitial queue for %s.", zztmVarRemove);
            while (zztnVar4.size() > 0) {
                zzto zztoVarZzm = zztnVar4.zzm(null);
                if (zztoVarZzm.zzKz) {
                    zztp.zzeN().zzeP();
                }
                zztoVarZzm.zzKv.zzbb();
            }
            this.zzKo.remove(zztmVarRemove);
        }
        while (zztnVar.size() > 0) {
            zzto zztoVarZzm2 = zztnVar.zzm(zzirVarZzk);
            if (zztoVarZzm2.zzKz) {
                if (com.google.android.gms.ads.internal.zzbs.zzbF().currentTimeMillis() - zztoVarZzm2.zzKy > 1000 * ((long) ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEe)).intValue())) {
                    zza("Expired interstitial at %s.", zztmVar);
                    zztp.zzeN().zzeO();
                }
            }
            String str2 = zztoVarZzm2.zzKw != null ? " (inline) " : MinimalPrettyPrinter.DEFAULT_ROOT_VALUE_SEPARATOR;
            zza(new StringBuilder(String.valueOf(str2).length() + 34).append("Pooled interstitial").append(str2).append("returned at %s.").toString(), zztmVar);
            return zztoVarZzm2;
        }
        return null;
    }

    final void zza(zzsi zzsiVar) {
        if (this.zzKq == null) {
            this.zzKq = zzsiVar.zzeF();
            if (this.zzKq != null) {
                SharedPreferences sharedPreferences = this.zzKq.getApplicationContext().getSharedPreferences("com.google.android.gms.ads.internal.interstitial.InterstitialAdPool", 0);
                while (this.zzKp.size() > 0) {
                    zztm zztmVarRemove = this.zzKp.remove();
                    zztn zztnVar = this.zzKo.get(zztmVarRemove);
                    zza("Flushing interstitial queue for %s.", zztmVarRemove);
                    while (zztnVar.size() > 0) {
                        zztnVar.zzm(null).zzKv.zzbb();
                    }
                    this.zzKo.remove(zztmVarRemove);
                }
                try {
                    HashMap map = new HashMap();
                    for (Map.Entry<String, ?> entry : sharedPreferences.getAll().entrySet()) {
                        if (!entry.getKey().equals("PoolKeys")) {
                            zztr zztrVarZzab = zztr.zzab((String) entry.getValue());
                            zztm zztmVar = new zztm(zztrVarZzab.zzuT, zztrVarZzab.zztV, zztrVarZzab.zzKt);
                            if (!this.zzKo.containsKey(zztmVar)) {
                                this.zzKo.put(zztmVar, new zztn(zztrVarZzab.zzuT, zztrVarZzab.zztV, zztrVarZzab.zzKt));
                                map.put(zztmVar.toString(), zztmVar);
                                zza("Restored interstitial queue for %s.", zztmVar);
                            }
                        }
                    }
                    for (String str : zzY(sharedPreferences.getString("PoolKeys", ""))) {
                        zztm zztmVar2 = (zztm) map.get(str);
                        if (this.zzKo.containsKey(zztmVar2)) {
                            this.zzKp.add(zztmVar2);
                        }
                    }
                } catch (IOException | RuntimeException e) {
                    com.google.android.gms.ads.internal.zzbs.zzbD().zza(e, "InterstitialAdPool.restore");
                    zzafr.zzc("Malformed preferences value for InterstitialAdPool.", e);
                    this.zzKo.clear();
                    this.zzKp.clear();
                }
            }
        }
    }

    final void zzb(zzir zzirVar, String str) {
        if (this.zzKq == null) {
            return;
        }
        int i = new zzacc(this.zzKq.getApplicationContext()).zzgM().zzVS;
        zzir zzirVarZzk = zzk(zzirVar);
        String strZzaa = zzaa(str);
        zztm zztmVar = new zztm(zzirVarZzk, strZzaa, i);
        zztn zztnVar = this.zzKo.get(zztmVar);
        if (zztnVar == null) {
            zza("Interstitial pool created at %s.", zztmVar);
            zztnVar = new zztn(zzirVarZzk, strZzaa, i);
            this.zzKo.put(zztmVar, zztnVar);
        }
        zztnVar.zza(this.zzKq, zzirVar);
        zztnVar.zzeL();
        zza("Inline entry added to the queue at %s.", zztmVar);
    }

    final void zzeG() {
        int size;
        int iZzeJ;
        if (this.zzKq == null) {
            return;
        }
        for (Map.Entry<zztm, zztn> entry : this.zzKo.entrySet()) {
            zztm key = entry.getKey();
            zztn value = entry.getValue();
            if (zzafr.zzz(2) && (iZzeJ = value.zzeJ()) < (size = value.size())) {
                zzafr.v(String.format("Loading %s/%s pooled interstitials for %s.", Integer.valueOf(size - iZzeJ), Integer.valueOf(size), key));
            }
            int iZzeK = value.zzeK() + 0;
            while (value.size() < ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEd)).intValue()) {
                zza("Pooling and loading one new interstitial for %s.", key);
                if (value.zzb(this.zzKq)) {
                    iZzeK++;
                }
            }
            zztp.zzeN().zzn(iZzeK);
        }
        if (this.zzKq != null) {
            SharedPreferences.Editor editorEdit = this.zzKq.getApplicationContext().getSharedPreferences("com.google.android.gms.ads.internal.interstitial.InterstitialAdPool", 0).edit();
            editorEdit.clear();
            for (Map.Entry<zztm, zztn> entry2 : this.zzKo.entrySet()) {
                zztm key2 = entry2.getKey();
                zztn value2 = entry2.getValue();
                if (value2.zzeM()) {
                    editorEdit.putString(key2.toString(), new zztr(value2).zzeW());
                    zza("Saved interstitial queue for %s.", key2);
                }
            }
            editorEdit.putString("PoolKeys", zzeH());
            editorEdit.apply();
        }
    }
}
