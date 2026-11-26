package com.google.android.gms.internal;

import android.text.TextUtils;
import java.util.Map;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
final class zzoh implements zzrd {
    final /* synthetic */ com.google.android.gms.ads.internal.js.zzai zzIj;
    final /* synthetic */ zzog zzIk;

    zzoh(zzog zzogVar, com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        this.zzIk = zzogVar;
        this.zzIj = zzaiVar;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        zzaka zzakaVar2 = (zzaka) this.zzIk.zzIh.get();
        if (zzakaVar2 == null) {
            this.zzIj.zzb("/loadHtml", this);
            return;
        }
        zzakaVar2.zziw().zza(new zzoi(this, map));
        String str = map.get("overlayHtml");
        String str2 = map.get("baseUrl");
        if (TextUtils.isEmpty(str2)) {
            zzakaVar2.loadData(str, "text/html", HTTP.UTF_8);
        } else {
            zzakaVar2.loadDataWithBaseURL(str2, str, "text/html", HTTP.UTF_8, null);
        }
    }
}
