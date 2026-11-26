package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class zzaw extends adj<zzaw> {
    public String zzaT = null;
    private String zzaU = null;
    private String zzaV = null;
    private String zzaW = null;
    private String zzaX = null;

    public zzaw() {
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
                    this.zzaT = adgVar.readString();
                    break;
                case 18:
                    this.zzaU = adgVar.readString();
                    break;
                case 26:
                    this.zzaV = adgVar.readString();
                    break;
                case MotionEventCompat.AXIS_GENERIC_3 /* 34 */:
                    this.zzaW = adgVar.readString();
                    break;
                case MotionEventCompat.AXIS_GENERIC_11 /* 42 */:
                    this.zzaX = adgVar.readString();
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
        if (this.zzaT != null) {
            adhVar.zzl(1, this.zzaT);
        }
        if (this.zzaU != null) {
            adhVar.zzl(2, this.zzaU);
        }
        if (this.zzaV != null) {
            adhVar.zzl(3, this.zzaV);
        }
        if (this.zzaW != null) {
            adhVar.zzl(4, this.zzaW);
        }
        if (this.zzaX != null) {
            adhVar.zzl(5, this.zzaX);
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.zzaT != null) {
            iZzn += adh.zzm(1, this.zzaT);
        }
        if (this.zzaU != null) {
            iZzn += adh.zzm(2, this.zzaU);
        }
        if (this.zzaV != null) {
            iZzn += adh.zzm(3, this.zzaV);
        }
        if (this.zzaW != null) {
            iZzn += adh.zzm(4, this.zzaW);
        }
        return this.zzaX != null ? iZzn + adh.zzm(5, this.zzaX) : iZzn;
    }
}
