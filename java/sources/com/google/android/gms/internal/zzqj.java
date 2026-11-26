package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzqj implements zzrd {
    private final zzqk zzIT;

    public zzqj(zzqk zzqkVar) {
        this.zzIT = zzqkVar;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        String str = map.get("name");
        if (str == null) {
            zzafr.zzaT("App event with no name parameter.");
        } else {
            this.zzIT.onAppEvent(str, map.get("info"));
        }
    }
}
