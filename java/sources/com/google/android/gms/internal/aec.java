package com.google.android.gms.internal;

import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class aec extends adj<aec> {
    public String zzctu = null;
    public Long zzctv = null;

    public aec() {
        this.zzcso = null;
        this.zzcsx = -1;
    }

    @Override // com.google.android.gms.internal.adp
    public final /* synthetic */ adp zza(adg adgVar) throws IOException {
        while (true) {
            int iZzLA = adgVar.zzLA();
            switch (iZzLA) {
                case 0:
                    break;
                case 10:
                    this.zzctu = adgVar.readString();
                    break;
                case 16:
                    this.zzctv = Long.valueOf(adgVar.zzLB());
                    break;
                default:
                    if (!super.zza(adgVar, iZzLA)) {
                    }
                    break;
            }
        }
        return this;
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    public final void zza(adh adhVar) throws IOException {
        if (this.zzctu != null) {
            adhVar.zzl(1, this.zzctu);
        }
        if (this.zzctv != null) {
            adhVar.zzb(2, this.zzctv.longValue());
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.zzctu != null) {
            iZzn += adh.zzm(1, this.zzctu);
        }
        return this.zzctv != null ? iZzn + adh.zze(2, this.zzctv.longValue()) : iZzn;
    }
}
