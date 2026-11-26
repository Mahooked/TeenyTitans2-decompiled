package com.google.android.gms.ads.internal;

import android.content.Context;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.internal.zzacs;
import com.google.android.gms.internal.zzadz;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzua;
import com.google.android.gms.internal.zzub;
import com.google.android.gms.internal.zzut;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzaz implements Runnable {
    private /* synthetic */ zzay zzuN;

    zzaz(zzay zzayVar) {
        this.zzuN = zzayVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        Context context = this.zzuN.zzuM.mContext;
        Runnable runnable = this.zzuN.zzuL;
        com.google.android.gms.common.internal.zzbo.zzcz("Adapters must be initialized on the main thread.");
        Map<String, zzub> mapZzhm = zzbs.zzbD().zzhD().zzhm();
        if (mapZzhm == null || mapZzhm.isEmpty()) {
            return;
        }
        if (runnable != null) {
            try {
                runnable.run();
            } catch (Throwable th) {
                zzafr.zzc("Could not initialize rewarded ads.", th);
                return;
            }
        }
        zzacs zzacsVarZzgO = zzacs.zzgO();
        if (zzacsVarZzgO != null) {
            Collection<zzub> collectionValues = mapZzhm.values();
            HashMap map = new HashMap();
            IObjectWrapper iObjectWrapperZzw = com.google.android.gms.dynamic.zzn.zzw(context);
            Iterator<zzub> it = collectionValues.iterator();
            while (it.hasNext()) {
                for (zzua zzuaVar : it.next().zzLY) {
                    String str = zzuaVar.zzLP;
                    for (String str2 : zzuaVar.zzLJ) {
                        if (!map.containsKey(str2)) {
                            map.put(str2, new ArrayList());
                        }
                        if (str != null) {
                            ((Collection) map.get(str2)).add(str);
                        }
                    }
                }
            }
            for (Map.Entry entry : map.entrySet()) {
                String str3 = (String) entry.getKey();
                try {
                    zzadz zzadzVarZzav = zzacsVarZzgO.zzav(str3);
                    if (zzadzVarZzav != null) {
                        zzut zzutVarZzgW = zzadzVarZzav.zzgW();
                        if (!zzutVarZzgW.isInitialized() && zzutVarZzgW.zzfu()) {
                            zzutVarZzgW.zza(iObjectWrapperZzw, zzadzVarZzav.zzgX(), (List<String>) entry.getValue());
                            String strValueOf = String.valueOf(str3);
                            zzafr.zzaC(strValueOf.length() != 0 ? "Initialized rewarded video mediation adapter ".concat(strValueOf) : new String("Initialized rewarded video mediation adapter "));
                        }
                    }
                } catch (Throwable th2) {
                    zzafr.zzc(new StringBuilder(String.valueOf(str3).length() + 56).append("Failed to initialize rewarded video mediation adapter \"").append(str3).append("\"").toString(), th2);
                }
            }
        }
    }
}
