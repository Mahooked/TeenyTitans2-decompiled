package com.google.android.gms.internal;

import android.content.SharedPreferences;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzmk {
    private final Collection<zzme> zzBP = new ArrayList();
    private final Collection<zzme<String>> zzBQ = new ArrayList();
    private final Collection<zzme<String>> zzBR = new ArrayList();

    public final void zza(SharedPreferences.Editor editor, int i, JSONObject jSONObject) {
        for (zzme zzmeVar : this.zzBP) {
            if (zzmeVar.getSource() == 1) {
                zzmeVar.zza(editor, zzmeVar.zzb(jSONObject));
            }
        }
    }

    public final void zza(zzme zzmeVar) {
        this.zzBP.add(zzmeVar);
    }

    public final void zzb(zzme<String> zzmeVar) {
        this.zzBQ.add(zzmeVar);
    }

    public final void zzc(zzme<String> zzmeVar) {
        this.zzBR.add(zzmeVar);
    }

    public final List<String> zzdJ() {
        ArrayList arrayList = new ArrayList();
        Iterator<zzme<String>> it = this.zzBQ.iterator();
        while (it.hasNext()) {
            String str = (String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(it.next());
            if (str != null) {
                arrayList.add(str);
            }
        }
        return arrayList;
    }

    public final List<String> zzdK() {
        List<String> listZzdJ = zzdJ();
        Iterator<zzme<String>> it = this.zzBR.iterator();
        while (it.hasNext()) {
            String str = (String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(it.next());
            if (str != null) {
                listZzdJ.add(str);
            }
        }
        return listZzdJ;
    }
}
