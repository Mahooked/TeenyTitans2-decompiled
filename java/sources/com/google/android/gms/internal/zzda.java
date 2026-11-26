package com.google.android.gms.internal;

import java.util.HashMap;

/* JADX INFO: loaded from: classes.dex */
public final class zzda extends zzbs<Integer, Long> {
    public Long zzqB;
    public Long zzqC;

    public zzda() {
    }

    public zzda(String str) {
        zzi(str);
    }

    @Override // com.google.android.gms.internal.zzbs
    protected final void zzi(String str) {
        HashMap mapZzj = zzj(str);
        if (mapZzj != null) {
            this.zzqB = (Long) mapZzj.get(0);
            this.zzqC = (Long) mapZzj.get(1);
        }
    }

    @Override // com.google.android.gms.internal.zzbs
    protected final HashMap<Integer, Long> zzv() {
        HashMap<Integer, Long> map = new HashMap<>();
        map.put(0, this.zzqB);
        map.put(1, this.zzqC);
        return map;
    }
}
