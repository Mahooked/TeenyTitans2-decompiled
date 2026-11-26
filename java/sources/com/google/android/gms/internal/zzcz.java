package com.google.android.gms.internal;

import java.util.HashMap;

/* JADX INFO: loaded from: classes.dex */
public final class zzcz extends zzbs<Integer, Object> {
    public Boolean zzqA;
    public Long zzqy;
    public Boolean zzqz;

    public zzcz() {
    }

    public zzcz(String str) {
        zzi(str);
    }

    @Override // com.google.android.gms.internal.zzbs
    protected final void zzi(String str) {
        HashMap mapZzj = zzj(str);
        if (mapZzj != null) {
            this.zzqy = (Long) mapZzj.get(0);
            this.zzqz = (Boolean) mapZzj.get(1);
            this.zzqA = (Boolean) mapZzj.get(2);
        }
    }

    @Override // com.google.android.gms.internal.zzbs
    protected final HashMap<Integer, Object> zzv() {
        HashMap<Integer, Object> map = new HashMap<>();
        map.put(0, this.zzqy);
        map.put(1, this.zzqz);
        map.put(2, this.zzqA);
        return map;
    }
}
