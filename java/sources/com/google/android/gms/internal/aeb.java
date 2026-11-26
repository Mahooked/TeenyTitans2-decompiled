package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import com.google.ads.AdSize;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class aeb extends adj<aeb> {
    private static volatile aeb[] zzctm;
    public Integer zzbuM = null;
    public String url = null;
    public adw zzctn = null;
    private ady zzcto = null;
    private Integer zzctp = null;
    private int[] zzctq = ads.zzcsC;
    private String zzctr = null;
    public Integer zzcts = null;
    public String[] zzctt = ads.EMPTY_STRING_ARRAY;

    public aeb() {
        this.zzcso = null;
        this.zzcsx = -1;
    }

    public static aeb[] zzLX() {
        if (zzctm == null) {
            synchronized (adn.zzcsw) {
                if (zzctm == null) {
                    zzctm = new aeb[0];
                }
            }
        }
        return zzctm;
    }

    @Override // com.google.android.gms.internal.adp
    public final /* synthetic */ adp zza(adg adgVar) throws IOException {
        while (true) {
            int iZzLA = adgVar.zzLA();
            switch (iZzLA) {
                case 0:
                    break;
                case 8:
                    this.zzbuM = Integer.valueOf(adgVar.zzLC());
                    break;
                case 18:
                    this.url = adgVar.readString();
                    break;
                case 26:
                    if (this.zzctn == null) {
                        this.zzctn = new adw();
                    }
                    adgVar.zza(this.zzctn);
                    break;
                case MotionEventCompat.AXIS_GENERIC_3 /* 34 */:
                    if (this.zzcto == null) {
                        this.zzcto = new ady();
                    }
                    adgVar.zza(this.zzcto);
                    break;
                case MotionEventCompat.AXIS_GENERIC_9 /* 40 */:
                    this.zzctp = Integer.valueOf(adgVar.zzLC());
                    break;
                case 48:
                    int iZzb = ads.zzb(adgVar, 48);
                    int length = this.zzctq == null ? 0 : this.zzctq.length;
                    int[] iArr = new int[iZzb + length];
                    if (length != 0) {
                        System.arraycopy(this.zzctq, 0, iArr, 0, length);
                    }
                    while (length < iArr.length - 1) {
                        iArr[length] = adgVar.zzLC();
                        adgVar.zzLA();
                        length++;
                    }
                    iArr[length] = adgVar.zzLC();
                    this.zzctq = iArr;
                    break;
                case AdSize.PORTRAIT_AD_HEIGHT /* 50 */:
                    int iZzcn = adgVar.zzcn(adgVar.zzLF());
                    int position = adgVar.getPosition();
                    int i = 0;
                    while (adgVar.zzLK() > 0) {
                        adgVar.zzLC();
                        i++;
                    }
                    adgVar.zzcp(position);
                    int length2 = this.zzctq == null ? 0 : this.zzctq.length;
                    int[] iArr2 = new int[i + length2];
                    if (length2 != 0) {
                        System.arraycopy(this.zzctq, 0, iArr2, 0, length2);
                    }
                    while (length2 < iArr2.length) {
                        iArr2[length2] = adgVar.zzLC();
                        length2++;
                    }
                    this.zzctq = iArr2;
                    adgVar.zzco(iZzcn);
                    break;
                case 58:
                    this.zzctr = adgVar.readString();
                    break;
                case 64:
                    int position2 = adgVar.getPosition();
                    int iZzLC = adgVar.zzLC();
                    switch (iZzLC) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            this.zzcts = Integer.valueOf(iZzLC);
                            break;
                        default:
                            adgVar.zzcp(position2);
                            zza(adgVar, iZzLA);
                            break;
                    }
                    break;
                case 74:
                    int iZzb2 = ads.zzb(adgVar, 74);
                    int length3 = this.zzctt == null ? 0 : this.zzctt.length;
                    String[] strArr = new String[iZzb2 + length3];
                    if (length3 != 0) {
                        System.arraycopy(this.zzctt, 0, strArr, 0, length3);
                    }
                    while (length3 < strArr.length - 1) {
                        strArr[length3] = adgVar.readString();
                        adgVar.zzLA();
                        length3++;
                    }
                    strArr[length3] = adgVar.readString();
                    this.zzctt = strArr;
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
        adhVar.zzr(1, this.zzbuM.intValue());
        if (this.url != null) {
            adhVar.zzl(2, this.url);
        }
        if (this.zzctn != null) {
            adhVar.zza(3, this.zzctn);
        }
        if (this.zzcto != null) {
            adhVar.zza(4, this.zzcto);
        }
        if (this.zzctp != null) {
            adhVar.zzr(5, this.zzctp.intValue());
        }
        if (this.zzctq != null && this.zzctq.length > 0) {
            for (int i = 0; i < this.zzctq.length; i++) {
                adhVar.zzr(6, this.zzctq[i]);
            }
        }
        if (this.zzctr != null) {
            adhVar.zzl(7, this.zzctr);
        }
        if (this.zzcts != null) {
            adhVar.zzr(8, this.zzcts.intValue());
        }
        if (this.zzctt != null && this.zzctt.length > 0) {
            for (int i2 = 0; i2 < this.zzctt.length; i2++) {
                String str = this.zzctt[i2];
                if (str != null) {
                    adhVar.zzl(9, str);
                }
            }
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn() + adh.zzs(1, this.zzbuM.intValue());
        if (this.url != null) {
            iZzn += adh.zzm(2, this.url);
        }
        if (this.zzctn != null) {
            iZzn += adh.zzb(3, this.zzctn);
        }
        if (this.zzcto != null) {
            iZzn += adh.zzb(4, this.zzcto);
        }
        if (this.zzctp != null) {
            iZzn += adh.zzs(5, this.zzctp.intValue());
        }
        if (this.zzctq != null && this.zzctq.length > 0) {
            int iZzcr = 0;
            for (int i = 0; i < this.zzctq.length; i++) {
                iZzcr += adh.zzcr(this.zzctq[i]);
            }
            iZzn = iZzn + iZzcr + (this.zzctq.length * 1);
        }
        if (this.zzctr != null) {
            iZzn += adh.zzm(7, this.zzctr);
        }
        if (this.zzcts != null) {
            iZzn += adh.zzs(8, this.zzcts.intValue());
        }
        if (this.zzctt == null || this.zzctt.length <= 0) {
            return iZzn;
        }
        int iZzhQ = 0;
        int i2 = 0;
        for (int i3 = 0; i3 < this.zzctt.length; i3++) {
            String str = this.zzctt[i3];
            if (str != null) {
                i2++;
                iZzhQ += adh.zzhQ(str);
            }
        }
        return iZzn + iZzhQ + (i2 * 1);
    }
}
