package com.google.android.gms.internal;

import android.content.Context;
import android.support.annotation.Nullable;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaie {
    private static zzs zzaad;
    private static final Object zzaae = new Object();
    private static zzaii<Void> zzaaf = new zzaif();

    public zzaie(Context context) {
        zzU(context);
    }

    private static zzs zzU(Context context) {
        zzs zzsVar;
        synchronized (zzaae) {
            if (zzaad == null) {
                zzaad = zzas.zza(context.getApplicationContext(), null);
            }
            zzsVar = zzaad;
        }
        return zzsVar;
    }

    public final zzajm<String> zza(int i, String str, @Nullable Map<String, String> map, @Nullable byte[] bArr) {
        zzain zzainVar = new zzain(this, null);
        zzaad.zzc(new zzaih(this, i, str, zzainVar, new zzaig(this, str, zzainVar), bArr, map));
        return zzainVar;
    }

    public final <T> zzajm<T> zza(String str, zzaii<T> zzaiiVar) {
        zzain zzainVar = new zzain(this, null);
        zzaad.zzc(new zzaij(str, zzaiiVar, zzainVar));
        return zzainVar;
    }

    public final zzajm<String> zzb(String str, Map<String, String> map) {
        return zza(0, str, map, null);
    }
}
