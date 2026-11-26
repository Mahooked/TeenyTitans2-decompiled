package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class zzbd extends adj<zzbd> {
    public Long zzcx = null;
    private String zzcH = null;
    private byte[] zzcI = null;

    public zzbd() {
        this.zzcsx = -1;
    }

    @Override // com.google.android.gms.internal.adp
    public final /* synthetic */ adp zza(adg adgVar) throws IOException {
        while (true) {
            int iZzLA = adgVar.zzLA();
            switch (iZzLA) {
                case 0:
                    break;
                case 8:
                    this.zzcx = Long.valueOf(adgVar.zzLG());
                    break;
                case 26:
                    this.zzcH = adgVar.readString();
                    break;
                case MotionEventCompat.AXIS_GENERIC_3 /* 34 */:
                    this.zzcI = adgVar.readBytes();
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
        if (this.zzcx != null) {
            adhVar.zzb(1, this.zzcx.longValue());
        }
        if (this.zzcH != null) {
            adhVar.zzl(3, this.zzcH);
        }
        if (this.zzcI != null) {
            adhVar.zzb(4, this.zzcI);
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.zzcx != null) {
            iZzn += adh.zze(1, this.zzcx.longValue());
        }
        if (this.zzcH != null) {
            iZzn += adh.zzm(3, this.zzcH);
        }
        return this.zzcI != null ? iZzn + adh.zzc(4, this.zzcI) : iZzn;
    }
}
