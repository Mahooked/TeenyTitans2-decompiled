package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class zzbc extends adj<zzbc> {
    public byte[] data = null;
    public byte[] zzcE = null;
    public byte[] zzcF = null;
    public byte[] zzcG = null;

    public zzbc() {
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
                    this.data = adgVar.readBytes();
                    break;
                case 18:
                    this.zzcE = adgVar.readBytes();
                    break;
                case 26:
                    this.zzcF = adgVar.readBytes();
                    break;
                case MotionEventCompat.AXIS_GENERIC_3 /* 34 */:
                    this.zzcG = adgVar.readBytes();
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
        if (this.data != null) {
            adhVar.zzb(1, this.data);
        }
        if (this.zzcE != null) {
            adhVar.zzb(2, this.zzcE);
        }
        if (this.zzcF != null) {
            adhVar.zzb(3, this.zzcF);
        }
        if (this.zzcG != null) {
            adhVar.zzb(4, this.zzcG);
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.data != null) {
            iZzn += adh.zzc(1, this.data);
        }
        if (this.zzcE != null) {
            iZzn += adh.zzc(2, this.zzcE);
        }
        if (this.zzcF != null) {
            iZzn += adh.zzc(3, this.zzcF);
        }
        return this.zzcG != null ? iZzn + adh.zzc(4, this.zzcG) : iZzn;
    }
}
