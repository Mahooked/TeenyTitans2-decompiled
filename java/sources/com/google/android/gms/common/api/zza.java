package com.google.android.gms.common.api;

import android.support.v4.util.ArrayMap;
import android.text.TextUtils;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.internal.zzbo;
import com.google.android.gms.internal.zzbat;
import java.util.ArrayList;

/* JADX INFO: loaded from: classes.dex */
public final class zza extends Exception {
    private final ArrayMap<zzbat<?>, ConnectionResult> zzaAB;

    public zza(ArrayMap<zzbat<?>, ConnectionResult> arrayMap) {
        this.zzaAB = arrayMap;
    }

    @Override // java.lang.Throwable
    public final String getMessage() {
        ArrayList arrayList = new ArrayList();
        boolean z = true;
        for (zzbat<?> zzbatVar : this.zzaAB.keySet()) {
            ConnectionResult connectionResult = this.zzaAB.get(zzbatVar);
            if (connectionResult.isSuccess()) {
                z = false;
            }
            String strValueOf = String.valueOf(zzbatVar.zzpr());
            String strValueOf2 = String.valueOf(connectionResult);
            arrayList.add(new StringBuilder(String.valueOf(strValueOf).length() + 2 + String.valueOf(strValueOf2).length()).append(strValueOf).append(": ").append(strValueOf2).toString());
        }
        StringBuilder sb = new StringBuilder();
        if (z) {
            sb.append("None of the queried APIs are available. ");
        } else {
            sb.append("Some of the queried APIs are unavailable. ");
        }
        sb.append(TextUtils.join("; ", arrayList));
        return sb.toString();
    }

    public final ConnectionResult zza(GoogleApi<? extends Api.ApiOptions> googleApi) {
        Object objZzph = googleApi.zzph();
        zzbo.zzb(this.zzaAB.get(objZzph) != null, "The given API was not part of the availability request.");
        return this.zzaAB.get(objZzph);
    }

    public final ArrayMap<zzbat<?>, ConnectionResult> zzpf() {
        return this.zzaAB;
    }
}
