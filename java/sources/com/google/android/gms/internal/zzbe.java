package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class zzbe extends adj<zzbe> {
    private Integer zzcK;
    private Integer zzcL;
    public byte[][] zzcJ = ads.zzcsH;
    public byte[] zzcE = null;

    public zzbe() {
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
                    int iZzb = ads.zzb(adgVar, 10);
                    int length = this.zzcJ == null ? 0 : this.zzcJ.length;
                    byte[][] bArr = new byte[iZzb + length][];
                    if (length != 0) {
                        System.arraycopy(this.zzcJ, 0, bArr, 0, length);
                    }
                    while (length < bArr.length - 1) {
                        bArr[length] = adgVar.readBytes();
                        adgVar.zzLA();
                        length++;
                    }
                    bArr[length] = adgVar.readBytes();
                    this.zzcJ = bArr;
                    break;
                case 18:
                    this.zzcE = adgVar.readBytes();
                    break;
                case MotionEventCompat.AXIS_DISTANCE /* 24 */:
                    int position = adgVar.getPosition();
                    int iZzLF = adgVar.zzLF();
                    switch (iZzLF) {
                        case 0:
                        case 1:
                            this.zzcK = Integer.valueOf(iZzLF);
                            break;
                        default:
                            adgVar.zzcp(position);
                            zza(adgVar, iZzLA);
                            break;
                    }
                    break;
                case 32:
                    int position2 = adgVar.getPosition();
                    int iZzLF2 = adgVar.zzLF();
                    switch (iZzLF2) {
                        case 0:
                        case 1:
                        case 2:
                            this.zzcL = Integer.valueOf(iZzLF2);
                            break;
                        default:
                            adgVar.zzcp(position2);
                            zza(adgVar, iZzLA);
                            break;
                    }
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
        if (this.zzcJ != null && this.zzcJ.length > 0) {
            for (int i = 0; i < this.zzcJ.length; i++) {
                byte[] bArr = this.zzcJ[i];
                if (bArr != null) {
                    adhVar.zzb(1, bArr);
                }
            }
        }
        if (this.zzcE != null) {
            adhVar.zzb(2, this.zzcE);
        }
        if (this.zzcK != null) {
            adhVar.zzr(3, this.zzcK.intValue());
        }
        if (this.zzcL != null) {
            adhVar.zzr(4, this.zzcL.intValue());
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzs;
        int iZzn = super.zzn();
        if (this.zzcJ == null || this.zzcJ.length <= 0) {
            iZzs = iZzn;
        } else {
            int iZzJ = 0;
            int i = 0;
            for (int i2 = 0; i2 < this.zzcJ.length; i2++) {
                byte[] bArr = this.zzcJ[i2];
                if (bArr != null) {
                    i++;
                    iZzJ += adh.zzJ(bArr);
                }
            }
            iZzs = iZzn + iZzJ + (i * 1);
        }
        if (this.zzcE != null) {
            iZzs += adh.zzc(2, this.zzcE);
        }
        if (this.zzcK != null) {
            iZzs += adh.zzs(3, this.zzcK.intValue());
        }
        return this.zzcL != null ? iZzs + adh.zzs(4, this.zzcL.intValue()) : iZzs;
    }
}
