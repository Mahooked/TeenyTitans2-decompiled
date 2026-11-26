package com.google.android.gms.internal;

import android.support.v4.media.TransportMediator;
import android.support.v4.view.MotionEventCompat;
import com.google.ads.AdSize;
import java.io.IOException;
import java.util.Arrays;

/* JADX INFO: loaded from: classes.dex */
public final class aej extends adj<aej> implements Cloneable {
    public long zzctQ = 0;
    public long zzctR = 0;
    private long zzctS = 0;
    private String tag = "";
    public int zzctT = 0;
    public int zzrB = 0;
    private boolean zzccZ = false;
    private aek[] zzctU = aek.zzMe();
    private byte[] zzctV = ads.zzcsI;
    private aeh zzctW = null;
    public byte[] zzctX = ads.zzcsI;
    private String zzctY = "";
    private String zzctZ = "";
    private aeg zzcua = null;
    private String zzcub = "";
    public long zzcuc = 180000;
    private aei zzcud = null;
    public byte[] zzcue = ads.zzcsI;
    private String zzcuf = "";
    private int zzcug = 0;
    private int[] zzcuh = ads.zzcsC;
    private long zzcui = 0;
    private ael zzcmX = null;

    public aej() {
        this.zzcso = null;
        this.zzcsx = -1;
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    /* JADX INFO: renamed from: zzMd, reason: merged with bridge method [inline-methods] */
    public final aej clone() {
        try {
            aej aejVar = (aej) super.clone();
            if (this.zzctU != null && this.zzctU.length > 0) {
                aejVar.zzctU = new aek[this.zzctU.length];
                int i = 0;
                while (true) {
                    int i2 = i;
                    if (i2 >= this.zzctU.length) {
                        break;
                    }
                    if (this.zzctU[i2] != null) {
                        aejVar.zzctU[i2] = (aek) this.zzctU[i2].clone();
                    }
                    i = i2 + 1;
                }
            }
            if (this.zzctW != null) {
                aejVar.zzctW = (aeh) this.zzctW.clone();
            }
            if (this.zzcua != null) {
                aejVar.zzcua = (aeg) this.zzcua.clone();
            }
            if (this.zzcud != null) {
                aejVar.zzcud = (aei) this.zzcud.clone();
            }
            if (this.zzcuh != null && this.zzcuh.length > 0) {
                aejVar.zzcuh = (int[]) this.zzcuh.clone();
            }
            if (this.zzcmX != null) {
                aejVar.zzcmX = (ael) this.zzcmX.clone();
            }
            return aejVar;
        } catch (CloneNotSupportedException e) {
            throw new AssertionError(e);
        }
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof aej)) {
            return false;
        }
        aej aejVar = (aej) obj;
        if (this.zzctQ == aejVar.zzctQ && this.zzctR == aejVar.zzctR && this.zzctS == aejVar.zzctS) {
            if (this.tag == null) {
                if (aejVar.tag != null) {
                    return false;
                }
            } else if (!this.tag.equals(aejVar.tag)) {
                return false;
            }
            if (this.zzctT == aejVar.zzctT && this.zzrB == aejVar.zzrB && this.zzccZ == aejVar.zzccZ && adn.equals(this.zzctU, aejVar.zzctU) && Arrays.equals(this.zzctV, aejVar.zzctV)) {
                if (this.zzctW == null) {
                    if (aejVar.zzctW != null) {
                        return false;
                    }
                } else if (!this.zzctW.equals(aejVar.zzctW)) {
                    return false;
                }
                if (!Arrays.equals(this.zzctX, aejVar.zzctX)) {
                    return false;
                }
                if (this.zzctY == null) {
                    if (aejVar.zzctY != null) {
                        return false;
                    }
                } else if (!this.zzctY.equals(aejVar.zzctY)) {
                    return false;
                }
                if (this.zzctZ == null) {
                    if (aejVar.zzctZ != null) {
                        return false;
                    }
                } else if (!this.zzctZ.equals(aejVar.zzctZ)) {
                    return false;
                }
                if (this.zzcua == null) {
                    if (aejVar.zzcua != null) {
                        return false;
                    }
                } else if (!this.zzcua.equals(aejVar.zzcua)) {
                    return false;
                }
                if (this.zzcub == null) {
                    if (aejVar.zzcub != null) {
                        return false;
                    }
                } else if (!this.zzcub.equals(aejVar.zzcub)) {
                    return false;
                }
                if (this.zzcuc != aejVar.zzcuc) {
                    return false;
                }
                if (this.zzcud == null) {
                    if (aejVar.zzcud != null) {
                        return false;
                    }
                } else if (!this.zzcud.equals(aejVar.zzcud)) {
                    return false;
                }
                if (!Arrays.equals(this.zzcue, aejVar.zzcue)) {
                    return false;
                }
                if (this.zzcuf == null) {
                    if (aejVar.zzcuf != null) {
                        return false;
                    }
                } else if (!this.zzcuf.equals(aejVar.zzcuf)) {
                    return false;
                }
                if (this.zzcug == aejVar.zzcug && adn.equals(this.zzcuh, aejVar.zzcuh) && this.zzcui == aejVar.zzcui) {
                    if (this.zzcmX == null) {
                        if (aejVar.zzcmX != null) {
                            return false;
                        }
                    } else if (!this.zzcmX.equals(aejVar.zzcmX)) {
                        return false;
                    }
                    if (this.zzcso == null || this.zzcso.isEmpty()) {
                        return aejVar.zzcso == null || aejVar.zzcso.isEmpty();
                    }
                    return this.zzcso.equals(aejVar.zzcso);
                }
                return false;
            }
            return false;
        }
        return false;
    }

    public final int hashCode() {
        int iHashCode = 0;
        int iHashCode2 = ((this.zzcmX == null ? 0 : this.zzcmX.hashCode()) + (((((((((this.zzcuf == null ? 0 : this.zzcuf.hashCode()) + (((((this.zzcud == null ? 0 : this.zzcud.hashCode()) + (((((this.zzcub == null ? 0 : this.zzcub.hashCode()) + (((this.zzcua == null ? 0 : this.zzcua.hashCode()) + (((this.zzctZ == null ? 0 : this.zzctZ.hashCode()) + (((this.zzctY == null ? 0 : this.zzctY.hashCode()) + (((((this.zzctW == null ? 0 : this.zzctW.hashCode()) + (((((((this.zzccZ ? 1231 : 1237) + (((((((this.tag == null ? 0 : this.tag.hashCode()) + ((((((((getClass().getName().hashCode() + 527) * 31) + ((int) (this.zzctQ ^ (this.zzctQ >>> 32)))) * 31) + ((int) (this.zzctR ^ (this.zzctR >>> 32)))) * 31) + ((int) (this.zzctS ^ (this.zzctS >>> 32)))) * 31)) * 31) + this.zzctT) * 31) + this.zzrB) * 31)) * 31) + adn.hashCode(this.zzctU)) * 31) + Arrays.hashCode(this.zzctV)) * 31)) * 31) + Arrays.hashCode(this.zzctX)) * 31)) * 31)) * 31)) * 31)) * 31) + ((int) (this.zzcuc ^ (this.zzcuc >>> 32)))) * 31)) * 31) + Arrays.hashCode(this.zzcue)) * 31)) * 31) + this.zzcug) * 31) + adn.hashCode(this.zzcuh)) * 31) + ((int) (this.zzcui ^ (this.zzcui >>> 32)))) * 31)) * 31;
        if (this.zzcso != null && !this.zzcso.isEmpty()) {
            iHashCode = this.zzcso.hashCode();
        }
        return iHashCode2 + iHashCode;
    }

    @Override // com.google.android.gms.internal.adj
    /* JADX INFO: renamed from: zzLN */
    public final /* synthetic */ adj clone() throws CloneNotSupportedException {
        return (aej) clone();
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    /* JADX INFO: renamed from: zzLO */
    public final /* synthetic */ adp clone() throws CloneNotSupportedException {
        return (aej) clone();
    }

    @Override // com.google.android.gms.internal.adp
    public final /* synthetic */ adp zza(adg adgVar) throws IOException {
        while (true) {
            int iZzLA = adgVar.zzLA();
            switch (iZzLA) {
                case 0:
                    break;
                case 8:
                    this.zzctQ = adgVar.zzLB();
                    break;
                case 18:
                    this.tag = adgVar.readString();
                    break;
                case 26:
                    int iZzb = ads.zzb(adgVar, 26);
                    int length = this.zzctU == null ? 0 : this.zzctU.length;
                    aek[] aekVarArr = new aek[iZzb + length];
                    if (length != 0) {
                        System.arraycopy(this.zzctU, 0, aekVarArr, 0, length);
                    }
                    while (length < aekVarArr.length - 1) {
                        aekVarArr[length] = new aek();
                        adgVar.zza(aekVarArr[length]);
                        adgVar.zzLA();
                        length++;
                    }
                    aekVarArr[length] = new aek();
                    adgVar.zza(aekVarArr[length]);
                    this.zzctU = aekVarArr;
                    break;
                case MotionEventCompat.AXIS_GENERIC_3 /* 34 */:
                    this.zzctV = adgVar.readBytes();
                    break;
                case AdSize.PORTRAIT_AD_HEIGHT /* 50 */:
                    this.zzctX = adgVar.readBytes();
                    break;
                case 58:
                    if (this.zzcua == null) {
                        this.zzcua = new aeg();
                    }
                    adgVar.zza(this.zzcua);
                    break;
                case 66:
                    this.zzctY = adgVar.readString();
                    break;
                case 74:
                    if (this.zzctW == null) {
                        this.zzctW = new aeh();
                    }
                    adgVar.zza(this.zzctW);
                    break;
                case 80:
                    this.zzccZ = adgVar.zzLD();
                    break;
                case 88:
                    this.zzctT = adgVar.zzLC();
                    break;
                case 96:
                    this.zzrB = adgVar.zzLC();
                    break;
                case 106:
                    this.zzctZ = adgVar.readString();
                    break;
                case 114:
                    this.zzcub = adgVar.readString();
                    break;
                case 120:
                    this.zzcuc = adgVar.zzLE();
                    break;
                case TransportMediator.KEYCODE_MEDIA_RECORD /* 130 */:
                    if (this.zzcud == null) {
                        this.zzcud = new aei();
                    }
                    adgVar.zza(this.zzcud);
                    break;
                case 136:
                    this.zzctR = adgVar.zzLB();
                    break;
                case 146:
                    this.zzcue = adgVar.readBytes();
                    break;
                case 152:
                    int position = adgVar.getPosition();
                    int iZzLC = adgVar.zzLC();
                    switch (iZzLC) {
                        case 0:
                        case 1:
                        case 2:
                            this.zzcug = iZzLC;
                            break;
                        default:
                            adgVar.zzcp(position);
                            zza(adgVar, iZzLA);
                            break;
                    }
                    break;
                case 160:
                    int iZzb2 = ads.zzb(adgVar, 160);
                    int length2 = this.zzcuh == null ? 0 : this.zzcuh.length;
                    int[] iArr = new int[iZzb2 + length2];
                    if (length2 != 0) {
                        System.arraycopy(this.zzcuh, 0, iArr, 0, length2);
                    }
                    while (length2 < iArr.length - 1) {
                        iArr[length2] = adgVar.zzLC();
                        adgVar.zzLA();
                        length2++;
                    }
                    iArr[length2] = adgVar.zzLC();
                    this.zzcuh = iArr;
                    break;
                case 162:
                    int iZzcn = adgVar.zzcn(adgVar.zzLF());
                    int position2 = adgVar.getPosition();
                    int i = 0;
                    while (adgVar.zzLK() > 0) {
                        adgVar.zzLC();
                        i++;
                    }
                    adgVar.zzcp(position2);
                    int length3 = this.zzcuh == null ? 0 : this.zzcuh.length;
                    int[] iArr2 = new int[i + length3];
                    if (length3 != 0) {
                        System.arraycopy(this.zzcuh, 0, iArr2, 0, length3);
                    }
                    while (length3 < iArr2.length) {
                        iArr2[length3] = adgVar.zzLC();
                        length3++;
                    }
                    this.zzcuh = iArr2;
                    adgVar.zzco(iZzcn);
                    break;
                case 168:
                    this.zzctS = adgVar.zzLB();
                    break;
                case 176:
                    this.zzcui = adgVar.zzLB();
                    break;
                case 186:
                    if (this.zzcmX == null) {
                        this.zzcmX = new ael();
                    }
                    adgVar.zza(this.zzcmX);
                    break;
                case 194:
                    this.zzcuf = adgVar.readString();
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
        if (this.zzctQ != 0) {
            adhVar.zzb(1, this.zzctQ);
        }
        if (this.tag != null && !this.tag.equals("")) {
            adhVar.zzl(2, this.tag);
        }
        if (this.zzctU != null && this.zzctU.length > 0) {
            for (int i = 0; i < this.zzctU.length; i++) {
                aek aekVar = this.zzctU[i];
                if (aekVar != null) {
                    adhVar.zza(3, aekVar);
                }
            }
        }
        if (!Arrays.equals(this.zzctV, ads.zzcsI)) {
            adhVar.zzb(4, this.zzctV);
        }
        if (!Arrays.equals(this.zzctX, ads.zzcsI)) {
            adhVar.zzb(6, this.zzctX);
        }
        if (this.zzcua != null) {
            adhVar.zza(7, this.zzcua);
        }
        if (this.zzctY != null && !this.zzctY.equals("")) {
            adhVar.zzl(8, this.zzctY);
        }
        if (this.zzctW != null) {
            adhVar.zza(9, this.zzctW);
        }
        if (this.zzccZ) {
            adhVar.zzk(10, this.zzccZ);
        }
        if (this.zzctT != 0) {
            adhVar.zzr(11, this.zzctT);
        }
        if (this.zzrB != 0) {
            adhVar.zzr(12, this.zzrB);
        }
        if (this.zzctZ != null && !this.zzctZ.equals("")) {
            adhVar.zzl(13, this.zzctZ);
        }
        if (this.zzcub != null && !this.zzcub.equals("")) {
            adhVar.zzl(14, this.zzcub);
        }
        if (this.zzcuc != 180000) {
            adhVar.zzd(15, this.zzcuc);
        }
        if (this.zzcud != null) {
            adhVar.zza(16, this.zzcud);
        }
        if (this.zzctR != 0) {
            adhVar.zzb(17, this.zzctR);
        }
        if (!Arrays.equals(this.zzcue, ads.zzcsI)) {
            adhVar.zzb(18, this.zzcue);
        }
        if (this.zzcug != 0) {
            adhVar.zzr(19, this.zzcug);
        }
        if (this.zzcuh != null && this.zzcuh.length > 0) {
            for (int i2 = 0; i2 < this.zzcuh.length; i2++) {
                adhVar.zzr(20, this.zzcuh[i2]);
            }
        }
        if (this.zzctS != 0) {
            adhVar.zzb(21, this.zzctS);
        }
        if (this.zzcui != 0) {
            adhVar.zzb(22, this.zzcui);
        }
        if (this.zzcmX != null) {
            adhVar.zza(23, this.zzcmX);
        }
        if (this.zzcuf != null && !this.zzcuf.equals("")) {
            adhVar.zzl(24, this.zzcuf);
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.zzctQ != 0) {
            iZzn += adh.zze(1, this.zzctQ);
        }
        if (this.tag != null && !this.tag.equals("")) {
            iZzn += adh.zzm(2, this.tag);
        }
        if (this.zzctU != null && this.zzctU.length > 0) {
            int iZzb = iZzn;
            for (int i = 0; i < this.zzctU.length; i++) {
                aek aekVar = this.zzctU[i];
                if (aekVar != null) {
                    iZzb += adh.zzb(3, aekVar);
                }
            }
            iZzn = iZzb;
        }
        if (!Arrays.equals(this.zzctV, ads.zzcsI)) {
            iZzn += adh.zzc(4, this.zzctV);
        }
        if (!Arrays.equals(this.zzctX, ads.zzcsI)) {
            iZzn += adh.zzc(6, this.zzctX);
        }
        if (this.zzcua != null) {
            iZzn += adh.zzb(7, this.zzcua);
        }
        if (this.zzctY != null && !this.zzctY.equals("")) {
            iZzn += adh.zzm(8, this.zzctY);
        }
        if (this.zzctW != null) {
            iZzn += adh.zzb(9, this.zzctW);
        }
        if (this.zzccZ) {
            iZzn += adh.zzct(10) + 1;
        }
        if (this.zzctT != 0) {
            iZzn += adh.zzs(11, this.zzctT);
        }
        if (this.zzrB != 0) {
            iZzn += adh.zzs(12, this.zzrB);
        }
        if (this.zzctZ != null && !this.zzctZ.equals("")) {
            iZzn += adh.zzm(13, this.zzctZ);
        }
        if (this.zzcub != null && !this.zzcub.equals("")) {
            iZzn += adh.zzm(14, this.zzcub);
        }
        if (this.zzcuc != 180000) {
            iZzn += adh.zzf(15, this.zzcuc);
        }
        if (this.zzcud != null) {
            iZzn += adh.zzb(16, this.zzcud);
        }
        if (this.zzctR != 0) {
            iZzn += adh.zze(17, this.zzctR);
        }
        if (!Arrays.equals(this.zzcue, ads.zzcsI)) {
            iZzn += adh.zzc(18, this.zzcue);
        }
        if (this.zzcug != 0) {
            iZzn += adh.zzs(19, this.zzcug);
        }
        if (this.zzcuh != null && this.zzcuh.length > 0) {
            int iZzcr = 0;
            for (int i2 = 0; i2 < this.zzcuh.length; i2++) {
                iZzcr += adh.zzcr(this.zzcuh[i2]);
            }
            iZzn = iZzn + iZzcr + (this.zzcuh.length * 2);
        }
        if (this.zzctS != 0) {
            iZzn += adh.zze(21, this.zzctS);
        }
        if (this.zzcui != 0) {
            iZzn += adh.zze(22, this.zzcui);
        }
        if (this.zzcmX != null) {
            iZzn += adh.zzb(23, this.zzcmX);
        }
        return (this.zzcuf == null || this.zzcuf.equals("")) ? iZzn : iZzn + adh.zzm(24, this.zzcuf);
    }
}
