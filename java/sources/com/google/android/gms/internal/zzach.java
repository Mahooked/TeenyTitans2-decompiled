package com.google.android.gms.internal;

import android.content.Context;
import java.util.Map;
import org.json.JSONException;

/* JADX INFO: loaded from: classes.dex */
final class zzach implements zzrd {
    private /* synthetic */ zzabu zzUA;
    private /* synthetic */ zzacg zzWj;
    private /* synthetic */ Context zztF;

    zzach(zzacg zzacgVar, Context context, zzabu zzabuVar) {
        this.zzWj = zzacgVar;
        this.zztF = context;
        this.zzUA = zzabuVar;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        try {
            zzmo.zza(this.zztF, 1, com.google.android.gms.ads.internal.zzbs.zzbz().zzj(map));
            this.zzWj.zzgN();
        } catch (JSONException e) {
            zzafr.zzb("Unable to save SDK Core Constants.", e);
        } finally {
            zzakaVar.zziw().zzb("/loadSdkConstants", this);
            zzacg.zze(this.zzUA);
        }
    }
}
