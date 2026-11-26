package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzro implements zzrd {
    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        if (com.google.android.gms.ads.internal.zzbs.zzbY().zzp(zzakaVar.getContext())) {
            int i = -1;
            try {
                i = Integer.parseInt(map.get("eventType"));
            } catch (Exception e) {
                zzafr.zzb("Parse Scion log event type error", e);
            }
            String str = map.get("eventId");
            switch (i) {
                case 0:
                    com.google.android.gms.ads.internal.zzbs.zzbY().zzf(zzakaVar.getContext(), str);
                    break;
                case 1:
                    com.google.android.gms.ads.internal.zzbs.zzbY().zzg(zzakaVar.getContext(), str);
                    break;
                case 2:
                    com.google.android.gms.ads.internal.zzbs.zzbY().zzi(zzakaVar.getContext(), str);
                    break;
            }
        }
    }
}
