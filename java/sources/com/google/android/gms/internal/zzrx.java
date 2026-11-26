package com.google.android.gms.internal;

import android.text.TextUtils;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
final class zzrx implements zzrd {
    zzrx() {
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        zzaks zzaksVar;
        if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEC)).booleanValue()) {
            zzaks zzaksVarZziH = zzakaVar.zziH();
            if (zzaksVarZziH == null) {
                try {
                    zzaks zzaksVar2 = new zzaks(zzakaVar, Float.parseFloat(map.get("duration")), "1".equals(map.get("customControlsAllowed")));
                    zzakaVar.zza(zzaksVar2);
                    zzaksVar = zzaksVar2;
                } catch (NullPointerException | NumberFormatException e) {
                    zzafr.zzb("Unable to parse videoMeta message.", e);
                    com.google.android.gms.ads.internal.zzbs.zzbD().zza(e, "VideoMetaGmsgHandler.onGmsg");
                    return;
                }
            } else {
                zzaksVar = zzaksVarZziH;
            }
            boolean zEquals = "1".equals(map.get("muted"));
            float f = Float.parseFloat(map.get("currentTime"));
            int i = Integer.parseInt(map.get("playbackState"));
            int i2 = (i < 0 || 3 < i) ? 0 : i;
            String str = map.get("aspectRatio");
            float f2 = TextUtils.isEmpty(str) ? 0.0f : Float.parseFloat(str);
            if (zzafr.zzz(3)) {
                zzafr.zzaC(new StringBuilder(String.valueOf(str).length() + 79).append("Video Meta GMSG: isMuted : ").append(zEquals).append(" , playbackState : ").append(i2).append(" , aspectRatio : ").append(str).toString());
            }
            zzaksVar.zza(f, i2, zEquals, f2);
        }
    }
}
