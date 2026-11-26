package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class zzat extends adj<zzat> {
    public String zzaH = null;
    public Long zzaI = null;
    private String stackTrace = null;
    private String zzaJ = null;
    private String zzaK = null;
    private Long zzaL = null;
    private Long zzaM = null;
    private String zzaN = null;
    private Long zzaO = null;
    private String zzaP = null;

    public zzat() {
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
                    this.zzaH = adgVar.readString();
                    break;
                case 16:
                    this.zzaI = Long.valueOf(adgVar.zzLG());
                    break;
                case 26:
                    this.stackTrace = adgVar.readString();
                    break;
                case MotionEventCompat.AXIS_GENERIC_3 /* 34 */:
                    this.zzaJ = adgVar.readString();
                    break;
                case MotionEventCompat.AXIS_GENERIC_11 /* 42 */:
                    this.zzaK = adgVar.readString();
                    break;
                case 48:
                    this.zzaL = Long.valueOf(adgVar.zzLG());
                    break;
                case 56:
                    this.zzaM = Long.valueOf(adgVar.zzLG());
                    break;
                case 66:
                    this.zzaN = adgVar.readString();
                    break;
                case 72:
                    this.zzaO = Long.valueOf(adgVar.zzLG());
                    break;
                case 82:
                    this.zzaP = adgVar.readString();
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
        if (this.zzaH != null) {
            adhVar.zzl(1, this.zzaH);
        }
        if (this.zzaI != null) {
            adhVar.zzb(2, this.zzaI.longValue());
        }
        if (this.stackTrace != null) {
            adhVar.zzl(3, this.stackTrace);
        }
        if (this.zzaJ != null) {
            adhVar.zzl(4, this.zzaJ);
        }
        if (this.zzaK != null) {
            adhVar.zzl(5, this.zzaK);
        }
        if (this.zzaL != null) {
            adhVar.zzb(6, this.zzaL.longValue());
        }
        if (this.zzaM != null) {
            adhVar.zzb(7, this.zzaM.longValue());
        }
        if (this.zzaN != null) {
            adhVar.zzl(8, this.zzaN);
        }
        if (this.zzaO != null) {
            adhVar.zzb(9, this.zzaO.longValue());
        }
        if (this.zzaP != null) {
            adhVar.zzl(10, this.zzaP);
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.zzaH != null) {
            iZzn += adh.zzm(1, this.zzaH);
        }
        if (this.zzaI != null) {
            iZzn += adh.zze(2, this.zzaI.longValue());
        }
        if (this.stackTrace != null) {
            iZzn += adh.zzm(3, this.stackTrace);
        }
        if (this.zzaJ != null) {
            iZzn += adh.zzm(4, this.zzaJ);
        }
        if (this.zzaK != null) {
            iZzn += adh.zzm(5, this.zzaK);
        }
        if (this.zzaL != null) {
            iZzn += adh.zze(6, this.zzaL.longValue());
        }
        if (this.zzaM != null) {
            iZzn += adh.zze(7, this.zzaM.longValue());
        }
        if (this.zzaN != null) {
            iZzn += adh.zzm(8, this.zzaN);
        }
        if (this.zzaO != null) {
            iZzn += adh.zze(9, this.zzaO.longValue());
        }
        return this.zzaP != null ? iZzn + adh.zzm(10, this.zzaP) : iZzn;
    }
}
