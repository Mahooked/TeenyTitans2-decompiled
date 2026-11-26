package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class zzba extends adj<zzba> {
    private Long zzcx = null;
    private Integer zzcy = null;
    private Boolean zzcz = null;
    private int[] zzcA = ads.zzcsC;
    private Long zzcB = null;

    public zzba() {
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
                case 16:
                    this.zzcy = Integer.valueOf(adgVar.zzLF());
                    break;
                case MotionEventCompat.AXIS_DISTANCE /* 24 */:
                    this.zzcz = Boolean.valueOf(adgVar.zzLD());
                    break;
                case 32:
                    int iZzb = ads.zzb(adgVar, 32);
                    int length = this.zzcA == null ? 0 : this.zzcA.length;
                    int[] iArr = new int[iZzb + length];
                    if (length != 0) {
                        System.arraycopy(this.zzcA, 0, iArr, 0, length);
                    }
                    while (length < iArr.length - 1) {
                        iArr[length] = adgVar.zzLF();
                        adgVar.zzLA();
                        length++;
                    }
                    iArr[length] = adgVar.zzLF();
                    this.zzcA = iArr;
                    break;
                case MotionEventCompat.AXIS_GENERIC_3 /* 34 */:
                    int iZzcn = adgVar.zzcn(adgVar.zzLF());
                    int position = adgVar.getPosition();
                    int i = 0;
                    while (adgVar.zzLK() > 0) {
                        adgVar.zzLF();
                        i++;
                    }
                    adgVar.zzcp(position);
                    int length2 = this.zzcA == null ? 0 : this.zzcA.length;
                    int[] iArr2 = new int[i + length2];
                    if (length2 != 0) {
                        System.arraycopy(this.zzcA, 0, iArr2, 0, length2);
                    }
                    while (length2 < iArr2.length) {
                        iArr2[length2] = adgVar.zzLF();
                        length2++;
                    }
                    this.zzcA = iArr2;
                    adgVar.zzco(iZzcn);
                    break;
                case MotionEventCompat.AXIS_GENERIC_9 /* 40 */:
                    this.zzcB = Long.valueOf(adgVar.zzLG());
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
        if (this.zzcy != null) {
            adhVar.zzr(2, this.zzcy.intValue());
        }
        if (this.zzcz != null) {
            adhVar.zzk(3, this.zzcz.booleanValue());
        }
        if (this.zzcA != null && this.zzcA.length > 0) {
            for (int i = 0; i < this.zzcA.length; i++) {
                adhVar.zzr(4, this.zzcA[i]);
            }
        }
        if (this.zzcB != null) {
            adhVar.zza(5, this.zzcB.longValue());
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.zzcx != null) {
            iZzn += adh.zze(1, this.zzcx.longValue());
        }
        if (this.zzcy != null) {
            iZzn += adh.zzs(2, this.zzcy.intValue());
        }
        if (this.zzcz != null) {
            this.zzcz.booleanValue();
            iZzn += adh.zzct(3) + 1;
        }
        if (this.zzcA != null && this.zzcA.length > 0) {
            int iZzcr = 0;
            for (int i = 0; i < this.zzcA.length; i++) {
                iZzcr += adh.zzcr(this.zzcA[i]);
            }
            iZzn = iZzn + iZzcr + (this.zzcA.length * 1);
        }
        if (this.zzcB == null) {
            return iZzn;
        }
        return iZzn + adh.zzct(5) + adh.zzaP(this.zzcB.longValue());
    }
}
