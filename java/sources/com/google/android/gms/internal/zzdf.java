package com.google.android.gms.internal;

import java.util.HashMap;

/* JADX INFO: loaded from: classes.dex */
public final class zzdf extends zzbs<Integer, Long> {
    public Long zzcb;
    public Long zzcd;
    public Long zzch;
    public Long zzci;
    public Long zzqZ;
    public Long zzra;
    public Long zzrb;
    public Long zzrc;
    public Long zzrd;
    public Long zzre;
    public Long zzrf;
    public Long zzrg;
    public Long zzrh;

    public zzdf() {
    }

    public zzdf(String str) {
        zzi(str);
    }

    @Override // com.google.android.gms.internal.zzbs
    protected final void zzi(String str) {
        HashMap mapZzj = zzj(str);
        if (mapZzj != null) {
            this.zzqZ = (Long) mapZzj.get(0);
            this.zzra = (Long) mapZzj.get(1);
            this.zzrb = (Long) mapZzj.get(2);
            this.zzcd = (Long) mapZzj.get(3);
            this.zzcb = (Long) mapZzj.get(4);
            this.zzrc = (Long) mapZzj.get(5);
            this.zzrd = (Long) mapZzj.get(6);
            this.zzre = (Long) mapZzj.get(7);
            this.zzci = (Long) mapZzj.get(8);
            this.zzch = (Long) mapZzj.get(9);
            this.zzrf = (Long) mapZzj.get(10);
            this.zzrg = (Long) mapZzj.get(11);
            this.zzrh = (Long) mapZzj.get(12);
        }
    }

    @Override // com.google.android.gms.internal.zzbs
    protected final HashMap<Integer, Long> zzv() {
        HashMap<Integer, Long> map = new HashMap<>();
        map.put(0, this.zzqZ);
        map.put(1, this.zzra);
        map.put(2, this.zzrb);
        map.put(3, this.zzcd);
        map.put(4, this.zzcb);
        map.put(5, this.zzrc);
        map.put(6, this.zzrd);
        map.put(7, this.zzre);
        map.put(8, this.zzci);
        map.put(9, this.zzch);
        map.put(10, this.zzrf);
        map.put(11, this.zzrg);
        map.put(12, this.zzrh);
        return map;
    }
}
