package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;
import java.util.Arrays;

/* JADX INFO: loaded from: classes.dex */
public final class aei extends adj<aei> implements Cloneable {
    private byte[] zzctM = ads.zzcsI;
    private String zzctN = "";
    private byte[][] zzctO = ads.zzcsH;
    private boolean zzctP = false;

    public aei() {
        this.zzcso = null;
        this.zzcsx = -1;
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    /* JADX INFO: renamed from: zzMc, reason: merged with bridge method [inline-methods] */
    public aei clone() {
        try {
            aei aeiVar = (aei) super.clone();
            if (this.zzctO != null && this.zzctO.length > 0) {
                aeiVar.zzctO = (byte[][]) this.zzctO.clone();
            }
            return aeiVar;
        } catch (CloneNotSupportedException e) {
            throw new AssertionError(e);
        }
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof aei)) {
            return false;
        }
        aei aeiVar = (aei) obj;
        if (!Arrays.equals(this.zzctM, aeiVar.zzctM)) {
            return false;
        }
        if (this.zzctN == null) {
            if (aeiVar.zzctN != null) {
                return false;
            }
        } else if (!this.zzctN.equals(aeiVar.zzctN)) {
            return false;
        }
        if (adn.zza(this.zzctO, aeiVar.zzctO) && this.zzctP == aeiVar.zzctP) {
            if (this.zzcso == null || this.zzcso.isEmpty()) {
                return aeiVar.zzcso == null || aeiVar.zzcso.isEmpty();
            }
            return this.zzcso.equals(aeiVar.zzcso);
        }
        return false;
    }

    public final int hashCode() {
        int iHashCode = 0;
        int iHashCode2 = ((this.zzctP ? 1231 : 1237) + (((((this.zzctN == null ? 0 : this.zzctN.hashCode()) + ((((getClass().getName().hashCode() + 527) * 31) + Arrays.hashCode(this.zzctM)) * 31)) * 31) + adn.zzc(this.zzctO)) * 31)) * 31;
        if (this.zzcso != null && !this.zzcso.isEmpty()) {
            iHashCode = this.zzcso.hashCode();
        }
        return iHashCode2 + iHashCode;
    }

    @Override // com.google.android.gms.internal.adj
    /* JADX INFO: renamed from: zzLN */
    public final /* synthetic */ adj clone() throws CloneNotSupportedException {
        return (aei) clone();
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    /* JADX INFO: renamed from: zzLO */
    public final /* synthetic */ adp clone() throws CloneNotSupportedException {
        return (aei) clone();
    }

    @Override // com.google.android.gms.internal.adp
    public final /* synthetic */ adp zza(adg adgVar) throws IOException {
        while (true) {
            int iZzLA = adgVar.zzLA();
            switch (iZzLA) {
                case 0:
                    break;
                case 10:
                    this.zzctM = adgVar.readBytes();
                    break;
                case 18:
                    int iZzb = ads.zzb(adgVar, 18);
                    int length = this.zzctO == null ? 0 : this.zzctO.length;
                    byte[][] bArr = new byte[iZzb + length][];
                    if (length != 0) {
                        System.arraycopy(this.zzctO, 0, bArr, 0, length);
                    }
                    while (length < bArr.length - 1) {
                        bArr[length] = adgVar.readBytes();
                        adgVar.zzLA();
                        length++;
                    }
                    bArr[length] = adgVar.readBytes();
                    this.zzctO = bArr;
                    break;
                case MotionEventCompat.AXIS_DISTANCE /* 24 */:
                    this.zzctP = adgVar.zzLD();
                    break;
                case MotionEventCompat.AXIS_GENERIC_3 /* 34 */:
                    this.zzctN = adgVar.readString();
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
        if (!Arrays.equals(this.zzctM, ads.zzcsI)) {
            adhVar.zzb(1, this.zzctM);
        }
        if (this.zzctO != null && this.zzctO.length > 0) {
            for (int i = 0; i < this.zzctO.length; i++) {
                byte[] bArr = this.zzctO[i];
                if (bArr != null) {
                    adhVar.zzb(2, bArr);
                }
            }
        }
        if (this.zzctP) {
            adhVar.zzk(3, this.zzctP);
        }
        if (this.zzctN != null && !this.zzctN.equals("")) {
            adhVar.zzl(4, this.zzctN);
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (!Arrays.equals(this.zzctM, ads.zzcsI)) {
            iZzn += adh.zzc(1, this.zzctM);
        }
        if (this.zzctO != null && this.zzctO.length > 0) {
            int iZzJ = 0;
            int i = 0;
            for (int i2 = 0; i2 < this.zzctO.length; i2++) {
                byte[] bArr = this.zzctO[i2];
                if (bArr != null) {
                    i++;
                    iZzJ += adh.zzJ(bArr);
                }
            }
            iZzn = iZzn + iZzJ + (i * 1);
        }
        if (this.zzctP) {
            iZzn += adh.zzct(3) + 1;
        }
        return (this.zzctN == null || this.zzctN.equals("")) ? iZzn : iZzn + adh.zzm(4, this.zzctN);
    }
}
