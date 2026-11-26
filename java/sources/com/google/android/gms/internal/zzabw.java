package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzabw implements zzrd {
    private /* synthetic */ zzabu zzUR;

    zzabw(zzabu zzabuVar) {
        this.zzUR = zzabuVar;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        synchronized (this.zzUR.mLock) {
            if (this.zzUR.zzUM.isDone()) {
                return;
            }
            zzaca zzacaVar = new zzaca(-2, map);
            if (this.zzUR.zzQx.equals(zzacaVar.getRequestId())) {
                String url = zzacaVar.getUrl();
                if (url == null) {
                    zzafr.zzaT("URL missing in loadAdUrl GMSG.");
                    return;
                }
                if (url.contains("%40mediation_adapters%40")) {
                    String strReplaceAll = url.replaceAll("%40mediation_adapters%40", zzafo.zzb(zzakaVar.getContext(), map.get("check_adapters"), this.zzUR.zzUL));
                    zzacaVar.setUrl(strReplaceAll);
                    String strValueOf = String.valueOf(strReplaceAll);
                    zzafr.v(strValueOf.length() != 0 ? "Ad request URL modified to ".concat(strValueOf) : new String("Ad request URL modified to "));
                }
                this.zzUR.zzUM.zzg(zzacaVar);
            }
        }
    }
}
