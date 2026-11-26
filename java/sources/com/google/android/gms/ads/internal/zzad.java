package com.google.android.gms.ads.internal;

import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzrd;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzad implements zzrd {
    private /* synthetic */ Runnable zzty;
    private /* synthetic */ zzac zztz;

    zzad(zzac zzacVar, Runnable runnable) {
        this.zztz = zzacVar;
        this.zzty = runnable;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        zzakaVar.zzb("/appSettingsFetched", this);
        synchronized (this.zztz.mLock) {
            if (map != null) {
                if ("true".equalsIgnoreCase(map.get("isSuccessful"))) {
                    zzbs.zzbD().zzn(this.zztz.mContext, map.get("appSettingsJson"));
                    try {
                        if (this.zzty != null) {
                            this.zzty.run();
                        }
                    } catch (Throwable th) {
                        zzbs.zzbD().zza(th, "ConfigLoader.maybeFetchNewAppSettings");
                        zzafr.zzc("ConfigLoader post task failed.", th);
                    }
                }
            }
        }
    }
}
