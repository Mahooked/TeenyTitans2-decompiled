package com.google.android.gms.ads.internal.js;

import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzrd;
import com.google.android.gms.internal.zzzn;
import java.util.AbstractMap;
import java.util.HashSet;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzak implements zzaj {
    private final zzai zzLB;
    private final HashSet<AbstractMap.SimpleEntry<String, zzrd>> zzLC = new HashSet<>();

    public zzak(zzai zzaiVar) {
        this.zzLB = zzaiVar;
    }

    @Override // com.google.android.gms.ads.internal.js.zzai
    public final void zza(String str, zzrd zzrdVar) {
        this.zzLB.zza(str, zzrdVar);
        this.zzLC.add(new AbstractMap.SimpleEntry<>(str, zzrdVar));
    }

    @Override // com.google.android.gms.ads.internal.js.zzai
    public final void zza(String str, JSONObject jSONObject) {
        this.zzLB.zza(str, jSONObject);
    }

    @Override // com.google.android.gms.ads.internal.js.zzai
    public final void zzb(String str, zzrd zzrdVar) {
        this.zzLB.zzb(str, zzrdVar);
        this.zzLC.remove(new AbstractMap.SimpleEntry(str, zzrdVar));
    }

    @Override // com.google.android.gms.ads.internal.js.zzai
    public final void zzb(String str, JSONObject jSONObject) {
        this.zzLB.zzb(str, jSONObject);
    }

    @Override // com.google.android.gms.ads.internal.js.zzaj
    public final void zzfe() {
        for (AbstractMap.SimpleEntry<String, zzrd> simpleEntry : this.zzLC) {
            String strValueOf = String.valueOf(simpleEntry.getValue().toString());
            zzafr.v(strValueOf.length() != 0 ? "Unregistering eventhandler: ".concat(strValueOf) : new String("Unregistering eventhandler: "));
            this.zzLB.zzb(simpleEntry.getKey(), simpleEntry.getValue());
        }
        this.zzLC.clear();
    }

    @Override // com.google.android.gms.ads.internal.js.zzai
    public final void zzi(String str, String str2) {
        this.zzLB.zzi(str, str2);
    }
}
