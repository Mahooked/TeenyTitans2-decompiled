package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class zzaz extends adj<zzaz> {
    private Long zzbM = null;
    private Long zzbN = null;
    public Long zzcu = null;
    public Long zzcv = null;
    public Long zzcw = null;

    public zzaz() {
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
                    this.zzbM = Long.valueOf(adgVar.zzLG());
                    break;
                case 16:
                    this.zzbN = Long.valueOf(adgVar.zzLG());
                    break;
                case MotionEventCompat.AXIS_DISTANCE /* 24 */:
                    this.zzcu = Long.valueOf(adgVar.zzLG());
                    break;
                case 32:
                    this.zzcv = Long.valueOf(adgVar.zzLG());
                    break;
                case MotionEventCompat.AXIS_GENERIC_9 /* 40 */:
                    this.zzcw = Long.valueOf(adgVar.zzLG());
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
        if (this.zzbM != null) {
            adhVar.zzb(1, this.zzbM.longValue());
        }
        if (this.zzbN != null) {
            adhVar.zzb(2, this.zzbN.longValue());
        }
        if (this.zzcu != null) {
            adhVar.zzb(3, this.zzcu.longValue());
        }
        if (this.zzcv != null) {
            adhVar.zzb(4, this.zzcv.longValue());
        }
        if (this.zzcw != null) {
            adhVar.zzb(5, this.zzcw.longValue());
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.zzbM != null) {
            iZzn += adh.zze(1, this.zzbM.longValue());
        }
        if (this.zzbN != null) {
            iZzn += adh.zze(2, this.zzbN.longValue());
        }
        if (this.zzcu != null) {
            iZzn += adh.zze(3, this.zzcu.longValue());
        }
        if (this.zzcv != null) {
            iZzn += adh.zze(4, this.zzcv.longValue());
        }
        return this.zzcw != null ? iZzn + adh.zze(5, this.zzcw.longValue()) : iZzn;
    }
}
