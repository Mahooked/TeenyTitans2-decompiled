package com.google.android.gms.internal;

import java.util.HashMap;

/* JADX INFO: loaded from: classes.dex */
public final class zzdh extends zzbs<Integer, Long> {
    public Long zzcv;
    public Long zzcw;
    public Long zzrj;

    public zzdh() {
    }

    public zzdh(String str) {
        zzi(str);
    }

    @Override // com.google.android.gms.internal.zzbs
    protected final void zzi(String str) {
        HashMap mapZzj = zzj(str);
        if (mapZzj != null) {
            this.zzrj = (Long) mapZzj.get(0);
            this.zzcv = (Long) mapZzj.get(1);
            this.zzcw = (Long) mapZzj.get(2);
        }
    }

    @Override // com.google.android.gms.internal.zzbs
    protected final HashMap<Integer, Long> zzv() {
        HashMap<Integer, Long> map = new HashMap<>();
        map.put(0, this.zzrj);
        map.put(1, this.zzcv);
        map.put(2, this.zzcw);
        return map;
    }
}
