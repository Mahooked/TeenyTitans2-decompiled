package com.google.android.gms.internal;

import android.text.TextUtils;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzakp implements zzrd {
    private /* synthetic */ zzako zzacr;

    zzakp(zzako zzakoVar) {
        this.zzacr = zzakoVar;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        if (map != null) {
            String str = map.get("height");
            if (TextUtils.isEmpty(str)) {
                return;
            }
            try {
                int i = Integer.parseInt(str);
                synchronized (this.zzacr.mLock) {
                    if (this.zzacr.zzaci != i) {
                        this.zzacr.zzaci = i;
                        this.zzacr.requestLayout();
                    }
                }
            } catch (Exception e) {
                zzafr.zzc("Exception occurred while getting webview content height", e);
            }
        }
    }
}
