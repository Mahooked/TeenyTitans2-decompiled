package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import com.google.ads.AdSize;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class adt extends adj<adt> {
    public Integer zzcsJ = null;
    private Integer zzcsK = null;
    public String url = null;
    public String zzcsL = null;
    private String zzcsM = null;
    public adu zzcsN = null;
    public aeb[] zzcsO = aeb.zzLX();
    public String zzcsP = null;
    public aea zzcsQ = null;
    private Boolean zzcsR = null;
    private String[] zzcsS = ads.EMPTY_STRING_ARRAY;
    private String zzcsT = null;
    private Boolean zzcsU = null;
    private Boolean zzcsV = null;
    private byte[] zzcsW = null;
    public aec zzcsX = null;

    public adt() {
        this.zzcso = null;
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
                    this.url = adgVar.readString();
                    break;
                case 18:
                    this.zzcsL = adgVar.readString();
                    break;
                case 26:
                    this.zzcsM = adgVar.readString();
                    break;
                case MotionEventCompat.AXIS_GENERIC_3 /* 34 */:
                    int iZzb = ads.zzb(adgVar, 34);
                    int length = this.zzcsO == null ? 0 : this.zzcsO.length;
                    aeb[] aebVarArr = new aeb[iZzb + length];
                    if (length != 0) {
                        System.arraycopy(this.zzcsO, 0, aebVarArr, 0, length);
                    }
                    while (length < aebVarArr.length - 1) {
                        aebVarArr[length] = new aeb();
                        adgVar.zza(aebVarArr[length]);
                        adgVar.zzLA();
                        length++;
                    }
                    aebVarArr[length] = new aeb();
                    adgVar.zza(aebVarArr[length]);
                    this.zzcsO = aebVarArr;
                    break;
                case MotionEventCompat.AXIS_GENERIC_9 /* 40 */:
                    this.zzcsR = Boolean.valueOf(adgVar.zzLD());
                    break;
                case AdSize.PORTRAIT_AD_HEIGHT /* 50 */:
                    int iZzb2 = ads.zzb(adgVar, 50);
                    int length2 = this.zzcsS == null ? 0 : this.zzcsS.length;
                    String[] strArr = new String[iZzb2 + length2];
                    if (length2 != 0) {
                        System.arraycopy(this.zzcsS, 0, strArr, 0, length2);
                    }
                    while (length2 < strArr.length - 1) {
                        strArr[length2] = adgVar.readString();
                        adgVar.zzLA();
                        length2++;
                    }
                    strArr[length2] = adgVar.readString();
                    this.zzcsS = strArr;
                    break;
                case 58:
                    this.zzcsT = adgVar.readString();
                    break;
                case 64:
                    this.zzcsU = Boolean.valueOf(adgVar.zzLD());
                    break;
                case 72:
                    this.zzcsV = Boolean.valueOf(adgVar.zzLD());
                    break;
                case 80:
                    int position = adgVar.getPosition();
                    int iZzLC = adgVar.zzLC();
                    switch (iZzLC) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            this.zzcsJ = Integer.valueOf(iZzLC);
                            break;
                        default:
                            adgVar.zzcp(position);
                            zza(adgVar, iZzLA);
                            break;
                    }
                    break;
                case 88:
                    int position2 = adgVar.getPosition();
                    int iZzLC2 = adgVar.zzLC();
                    switch (iZzLC2) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            this.zzcsK = Integer.valueOf(iZzLC2);
                            break;
                        default:
                            adgVar.zzcp(position2);
                            zza(adgVar, iZzLA);
                            break;
                    }
                    break;
                case 98:
                    if (this.zzcsN == null) {
                        this.zzcsN = new adu();
                    }
                    adgVar.zza(this.zzcsN);
                    break;
                case 106:
                    this.zzcsP = adgVar.readString();
                    break;
                case 114:
                    if (this.zzcsQ == null) {
                        this.zzcsQ = new aea();
                    }
                    adgVar.zza(this.zzcsQ);
                    break;
                case 122:
                    this.zzcsW = adgVar.readBytes();
                    break;
                case 138:
                    if (this.zzcsX == null) {
                        this.zzcsX = new aec();
                    }
                    adgVar.zza(this.zzcsX);
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
        if (this.url != null) {
            adhVar.zzl(1, this.url);
        }
        if (this.zzcsL != null) {
            adhVar.zzl(2, this.zzcsL);
        }
        if (this.zzcsM != null) {
            adhVar.zzl(3, this.zzcsM);
        }
        if (this.zzcsO != null && this.zzcsO.length > 0) {
            for (int i = 0; i < this.zzcsO.length; i++) {
                aeb aebVar = this.zzcsO[i];
                if (aebVar != null) {
                    adhVar.zza(4, aebVar);
                }
            }
        }
        if (this.zzcsR != null) {
            adhVar.zzk(5, this.zzcsR.booleanValue());
        }
        if (this.zzcsS != null && this.zzcsS.length > 0) {
            for (int i2 = 0; i2 < this.zzcsS.length; i2++) {
                String str = this.zzcsS[i2];
                if (str != null) {
                    adhVar.zzl(6, str);
                }
            }
        }
        if (this.zzcsT != null) {
            adhVar.zzl(7, this.zzcsT);
        }
        if (this.zzcsU != null) {
            adhVar.zzk(8, this.zzcsU.booleanValue());
        }
        if (this.zzcsV != null) {
            adhVar.zzk(9, this.zzcsV.booleanValue());
        }
        if (this.zzcsJ != null) {
            adhVar.zzr(10, this.zzcsJ.intValue());
        }
        if (this.zzcsK != null) {
            adhVar.zzr(11, this.zzcsK.intValue());
        }
        if (this.zzcsN != null) {
            adhVar.zza(12, this.zzcsN);
        }
        if (this.zzcsP != null) {
            adhVar.zzl(13, this.zzcsP);
        }
        if (this.zzcsQ != null) {
            adhVar.zza(14, this.zzcsQ);
        }
        if (this.zzcsW != null) {
            adhVar.zzb(15, this.zzcsW);
        }
        if (this.zzcsX != null) {
            adhVar.zza(17, this.zzcsX);
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.url != null) {
            iZzn += adh.zzm(1, this.url);
        }
        if (this.zzcsL != null) {
            iZzn += adh.zzm(2, this.zzcsL);
        }
        if (this.zzcsM != null) {
            iZzn += adh.zzm(3, this.zzcsM);
        }
        if (this.zzcsO != null && this.zzcsO.length > 0) {
            int iZzb = iZzn;
            for (int i = 0; i < this.zzcsO.length; i++) {
                aeb aebVar = this.zzcsO[i];
                if (aebVar != null) {
                    iZzb += adh.zzb(4, aebVar);
                }
            }
            iZzn = iZzb;
        }
        if (this.zzcsR != null) {
            this.zzcsR.booleanValue();
            iZzn += adh.zzct(5) + 1;
        }
        if (this.zzcsS != null && this.zzcsS.length > 0) {
            int iZzhQ = 0;
            int i2 = 0;
            for (int i3 = 0; i3 < this.zzcsS.length; i3++) {
                String str = this.zzcsS[i3];
                if (str != null) {
                    i2++;
                    iZzhQ += adh.zzhQ(str);
                }
            }
            iZzn = iZzn + iZzhQ + (i2 * 1);
        }
        if (this.zzcsT != null) {
            iZzn += adh.zzm(7, this.zzcsT);
        }
        if (this.zzcsU != null) {
            this.zzcsU.booleanValue();
            iZzn += adh.zzct(8) + 1;
        }
        if (this.zzcsV != null) {
            this.zzcsV.booleanValue();
            iZzn += adh.zzct(9) + 1;
        }
        if (this.zzcsJ != null) {
            iZzn += adh.zzs(10, this.zzcsJ.intValue());
        }
        if (this.zzcsK != null) {
            iZzn += adh.zzs(11, this.zzcsK.intValue());
        }
        if (this.zzcsN != null) {
            iZzn += adh.zzb(12, this.zzcsN);
        }
        if (this.zzcsP != null) {
            iZzn += adh.zzm(13, this.zzcsP);
        }
        if (this.zzcsQ != null) {
            iZzn += adh.zzb(14, this.zzcsQ);
        }
        if (this.zzcsW != null) {
            iZzn += adh.zzc(15, this.zzcsW);
        }
        return this.zzcsX != null ? iZzn + adh.zzb(17, this.zzcsX) : iZzn;
    }
}
