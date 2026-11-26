package com.google.android.gms.internal;

import java.util.HashMap;

/* JADX INFO: loaded from: classes.dex */
public final class zzbu extends zzbs<Integer, Object> {
    public String zzaT;
    public String zzaV;
    public String zzaW;
    public String zzaX;
    public long zzlO;

    public zzbu() {
        this.zzaT = "E";
        this.zzlO = -1L;
        this.zzaV = "E";
        this.zzaW = "E";
        this.zzaX = "E";
    }

    public zzbu(String str) {
        this();
        zzi(str);
    }

    @Override // com.google.android.gms.internal.zzbs
    protected final void zzi(String str) {
        HashMap mapZzj = zzj(str);
        if (mapZzj != null) {
            this.zzaT = mapZzj.get(0) == null ? "E" : (String) mapZzj.get(0);
            this.zzlO = mapZzj.get(1) == null ? -1L : ((Long) mapZzj.get(1)).longValue();
            this.zzaV = mapZzj.get(2) == null ? "E" : (String) mapZzj.get(2);
            this.zzaW = mapZzj.get(3) == null ? "E" : (String) mapZzj.get(3);
            this.zzaX = mapZzj.get(4) == null ? "E" : (String) mapZzj.get(4);
        }
    }

    @Override // com.google.android.gms.internal.zzbs
    protected final HashMap<Integer, Object> zzv() {
        HashMap<Integer, Object> map = new HashMap<>();
        map.put(0, this.zzaT);
        map.put(4, this.zzaX);
        map.put(3, this.zzaW);
        map.put(2, this.zzaV);
        map.put(1, Long.valueOf(this.zzlO));
        return map;
    }
}
