package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class aeg extends adj<aeg> implements Cloneable {
    private String[] zzctG = ads.EMPTY_STRING_ARRAY;
    private String[] zzctH = ads.EMPTY_STRING_ARRAY;
    private int[] zzctI = ads.zzcsC;
    private long[] zzctJ = ads.zzcsD;
    private long[] zzctK = ads.zzcsD;

    public aeg() {
        this.zzcso = null;
        this.zzcsx = -1;
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    /* JADX INFO: renamed from: zzMa, reason: merged with bridge method [inline-methods] */
    public aeg clone() {
        try {
            aeg aegVar = (aeg) super.clone();
            if (this.zzctG != null && this.zzctG.length > 0) {
                aegVar.zzctG = (String[]) this.zzctG.clone();
            }
            if (this.zzctH != null && this.zzctH.length > 0) {
                aegVar.zzctH = (String[]) this.zzctH.clone();
            }
            if (this.zzctI != null && this.zzctI.length > 0) {
                aegVar.zzctI = (int[]) this.zzctI.clone();
            }
            if (this.zzctJ != null && this.zzctJ.length > 0) {
                aegVar.zzctJ = (long[]) this.zzctJ.clone();
            }
            if (this.zzctK != null && this.zzctK.length > 0) {
                aegVar.zzctK = (long[]) this.zzctK.clone();
            }
            return aegVar;
        } catch (CloneNotSupportedException e) {
            throw new AssertionError(e);
        }
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof aeg)) {
            return false;
        }
        aeg aegVar = (aeg) obj;
        if (adn.equals(this.zzctG, aegVar.zzctG) && adn.equals(this.zzctH, aegVar.zzctH) && adn.equals(this.zzctI, aegVar.zzctI) && adn.equals(this.zzctJ, aegVar.zzctJ) && adn.equals(this.zzctK, aegVar.zzctK)) {
            if (this.zzcso == null || this.zzcso.isEmpty()) {
                return aegVar.zzcso == null || aegVar.zzcso.isEmpty();
            }
            return this.zzcso.equals(aegVar.zzcso);
        }
        return false;
    }

    public final int hashCode() {
        return ((this.zzcso == null || this.zzcso.isEmpty()) ? 0 : this.zzcso.hashCode()) + ((((((((((((getClass().getName().hashCode() + 527) * 31) + adn.hashCode(this.zzctG)) * 31) + adn.hashCode(this.zzctH)) * 31) + adn.hashCode(this.zzctI)) * 31) + adn.hashCode(this.zzctJ)) * 31) + adn.hashCode(this.zzctK)) * 31);
    }

    @Override // com.google.android.gms.internal.adj
    /* JADX INFO: renamed from: zzLN */
    public final /* synthetic */ adj clone() throws CloneNotSupportedException {
        return (aeg) clone();
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    /* JADX INFO: renamed from: zzLO */
    public final /* synthetic */ adp clone() throws CloneNotSupportedException {
        return (aeg) clone();
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
                    int length = this.zzctG == null ? 0 : this.zzctG.length;
                    String[] strArr = new String[iZzb + length];
                    if (length != 0) {
                        System.arraycopy(this.zzctG, 0, strArr, 0, length);
                    }
                    while (length < strArr.length - 1) {
                        strArr[length] = adgVar.readString();
                        adgVar.zzLA();
                        length++;
                    }
                    strArr[length] = adgVar.readString();
                    this.zzctG = strArr;
                    break;
                case 18:
                    int iZzb2 = ads.zzb(adgVar, 18);
                    int length2 = this.zzctH == null ? 0 : this.zzctH.length;
                    String[] strArr2 = new String[iZzb2 + length2];
                    if (length2 != 0) {
                        System.arraycopy(this.zzctH, 0, strArr2, 0, length2);
                    }
                    while (length2 < strArr2.length - 1) {
                        strArr2[length2] = adgVar.readString();
                        adgVar.zzLA();
                        length2++;
                    }
                    strArr2[length2] = adgVar.readString();
                    this.zzctH = strArr2;
                    break;
                case MotionEventCompat.AXIS_DISTANCE /* 24 */:
                    int iZzb3 = ads.zzb(adgVar, 24);
                    int length3 = this.zzctI == null ? 0 : this.zzctI.length;
                    int[] iArr = new int[iZzb3 + length3];
                    if (length3 != 0) {
                        System.arraycopy(this.zzctI, 0, iArr, 0, length3);
                    }
                    while (length3 < iArr.length - 1) {
                        iArr[length3] = adgVar.zzLC();
                        adgVar.zzLA();
                        length3++;
                    }
                    iArr[length3] = adgVar.zzLC();
                    this.zzctI = iArr;
                    break;
                case 26:
                    int iZzcn = adgVar.zzcn(adgVar.zzLF());
                    int position = adgVar.getPosition();
                    int i = 0;
                    while (adgVar.zzLK() > 0) {
                        adgVar.zzLC();
                        i++;
                    }
                    adgVar.zzcp(position);
                    int length4 = this.zzctI == null ? 0 : this.zzctI.length;
                    int[] iArr2 = new int[i + length4];
                    if (length4 != 0) {
                        System.arraycopy(this.zzctI, 0, iArr2, 0, length4);
                    }
                    while (length4 < iArr2.length) {
                        iArr2[length4] = adgVar.zzLC();
                        length4++;
                    }
                    this.zzctI = iArr2;
                    adgVar.zzco(iZzcn);
                    break;
                case 32:
                    int iZzb4 = ads.zzb(adgVar, 32);
                    int length5 = this.zzctJ == null ? 0 : this.zzctJ.length;
                    long[] jArr = new long[iZzb4 + length5];
                    if (length5 != 0) {
                        System.arraycopy(this.zzctJ, 0, jArr, 0, length5);
                    }
                    while (length5 < jArr.length - 1) {
                        jArr[length5] = adgVar.zzLB();
                        adgVar.zzLA();
                        length5++;
                    }
                    jArr[length5] = adgVar.zzLB();
                    this.zzctJ = jArr;
                    break;
                case MotionEventCompat.AXIS_GENERIC_3 /* 34 */:
                    int iZzcn2 = adgVar.zzcn(adgVar.zzLF());
                    int position2 = adgVar.getPosition();
                    int i2 = 0;
                    while (adgVar.zzLK() > 0) {
                        adgVar.zzLB();
                        i2++;
                    }
                    adgVar.zzcp(position2);
                    int length6 = this.zzctJ == null ? 0 : this.zzctJ.length;
                    long[] jArr2 = new long[i2 + length6];
                    if (length6 != 0) {
                        System.arraycopy(this.zzctJ, 0, jArr2, 0, length6);
                    }
                    while (length6 < jArr2.length) {
                        jArr2[length6] = adgVar.zzLB();
                        length6++;
                    }
                    this.zzctJ = jArr2;
                    adgVar.zzco(iZzcn2);
                    break;
                case MotionEventCompat.AXIS_GENERIC_9 /* 40 */:
                    int iZzb5 = ads.zzb(adgVar, 40);
                    int length7 = this.zzctK == null ? 0 : this.zzctK.length;
                    long[] jArr3 = new long[iZzb5 + length7];
                    if (length7 != 0) {
                        System.arraycopy(this.zzctK, 0, jArr3, 0, length7);
                    }
                    while (length7 < jArr3.length - 1) {
                        jArr3[length7] = adgVar.zzLB();
                        adgVar.zzLA();
                        length7++;
                    }
                    jArr3[length7] = adgVar.zzLB();
                    this.zzctK = jArr3;
                    break;
                case MotionEventCompat.AXIS_GENERIC_11 /* 42 */:
                    int iZzcn3 = adgVar.zzcn(adgVar.zzLF());
                    int position3 = adgVar.getPosition();
                    int i3 = 0;
                    while (adgVar.zzLK() > 0) {
                        adgVar.zzLB();
                        i3++;
                    }
                    adgVar.zzcp(position3);
                    int length8 = this.zzctK == null ? 0 : this.zzctK.length;
                    long[] jArr4 = new long[i3 + length8];
                    if (length8 != 0) {
                        System.arraycopy(this.zzctK, 0, jArr4, 0, length8);
                    }
                    while (length8 < jArr4.length) {
                        jArr4[length8] = adgVar.zzLB();
                        length8++;
                    }
                    this.zzctK = jArr4;
                    adgVar.zzco(iZzcn3);
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
        if (this.zzctG != null && this.zzctG.length > 0) {
            for (int i = 0; i < this.zzctG.length; i++) {
                String str = this.zzctG[i];
                if (str != null) {
                    adhVar.zzl(1, str);
                }
            }
        }
        if (this.zzctH != null && this.zzctH.length > 0) {
            for (int i2 = 0; i2 < this.zzctH.length; i2++) {
                String str2 = this.zzctH[i2];
                if (str2 != null) {
                    adhVar.zzl(2, str2);
                }
            }
        }
        if (this.zzctI != null && this.zzctI.length > 0) {
            for (int i3 = 0; i3 < this.zzctI.length; i3++) {
                adhVar.zzr(3, this.zzctI[i3]);
            }
        }
        if (this.zzctJ != null && this.zzctJ.length > 0) {
            for (int i4 = 0; i4 < this.zzctJ.length; i4++) {
                adhVar.zzb(4, this.zzctJ[i4]);
            }
        }
        if (this.zzctK != null && this.zzctK.length > 0) {
            for (int i5 = 0; i5 < this.zzctK.length; i5++) {
                adhVar.zzb(5, this.zzctK[i5]);
            }
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int length;
        int iZzn = super.zzn();
        if (this.zzctG == null || this.zzctG.length <= 0) {
            length = iZzn;
        } else {
            int iZzhQ = 0;
            int i = 0;
            for (int i2 = 0; i2 < this.zzctG.length; i2++) {
                String str = this.zzctG[i2];
                if (str != null) {
                    i++;
                    iZzhQ += adh.zzhQ(str);
                }
            }
            length = iZzn + iZzhQ + (i * 1);
        }
        if (this.zzctH != null && this.zzctH.length > 0) {
            int iZzhQ2 = 0;
            int i3 = 0;
            for (int i4 = 0; i4 < this.zzctH.length; i4++) {
                String str2 = this.zzctH[i4];
                if (str2 != null) {
                    i3++;
                    iZzhQ2 += adh.zzhQ(str2);
                }
            }
            length = length + iZzhQ2 + (i3 * 1);
        }
        if (this.zzctI != null && this.zzctI.length > 0) {
            int iZzcr = 0;
            for (int i5 = 0; i5 < this.zzctI.length; i5++) {
                iZzcr += adh.zzcr(this.zzctI[i5]);
            }
            length = length + iZzcr + (this.zzctI.length * 1);
        }
        if (this.zzctJ != null && this.zzctJ.length > 0) {
            int iZzaP = 0;
            for (int i6 = 0; i6 < this.zzctJ.length; i6++) {
                iZzaP += adh.zzaP(this.zzctJ[i6]);
            }
            length = length + iZzaP + (this.zzctJ.length * 1);
        }
        if (this.zzctK == null || this.zzctK.length <= 0) {
            return length;
        }
        int iZzaP2 = 0;
        for (int i7 = 0; i7 < this.zzctK.length; i7++) {
            iZzaP2 += adh.zzaP(this.zzctK[i7]);
        }
        return length + iZzaP2 + (this.zzctK.length * 1);
    }
}
