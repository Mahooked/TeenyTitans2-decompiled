package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class adw extends adj<adw> {
    private adx zzcta = null;
    public adv[] zzctb = adv.zzLW();
    private byte[] body = null;
    private byte[] zzctc = null;
    private Integer zzctd = null;

    public adw() {
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
                    if (this.zzcta == null) {
                        this.zzcta = new adx();
                    }
                    adgVar.zza(this.zzcta);
                    break;
                case 18:
                    int iZzb = ads.zzb(adgVar, 18);
                    int length = this.zzctb == null ? 0 : this.zzctb.length;
                    adv[] advVarArr = new adv[iZzb + length];
                    if (length != 0) {
                        System.arraycopy(this.zzctb, 0, advVarArr, 0, length);
                    }
                    while (length < advVarArr.length - 1) {
                        advVarArr[length] = new adv();
                        adgVar.zza(advVarArr[length]);
                        adgVar.zzLA();
                        length++;
                    }
                    advVarArr[length] = new adv();
                    adgVar.zza(advVarArr[length]);
                    this.zzctb = advVarArr;
                    break;
                case 26:
                    this.body = adgVar.readBytes();
                    break;
                case MotionEventCompat.AXIS_GENERIC_3 /* 34 */:
                    this.zzctc = adgVar.readBytes();
                    break;
                case MotionEventCompat.AXIS_GENERIC_9 /* 40 */:
                    this.zzctd = Integer.valueOf(adgVar.zzLC());
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
        if (this.zzcta != null) {
            adhVar.zza(1, this.zzcta);
        }
        if (this.zzctb != null && this.zzctb.length > 0) {
            for (int i = 0; i < this.zzctb.length; i++) {
                adv advVar = this.zzctb[i];
                if (advVar != null) {
                    adhVar.zza(2, advVar);
                }
            }
        }
        if (this.body != null) {
            adhVar.zzb(3, this.body);
        }
        if (this.zzctc != null) {
            adhVar.zzb(4, this.zzctc);
        }
        if (this.zzctd != null) {
            adhVar.zzr(5, this.zzctd.intValue());
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.zzcta != null) {
            iZzn += adh.zzb(1, this.zzcta);
        }
        if (this.zzctb != null && this.zzctb.length > 0) {
            int iZzb = iZzn;
            for (int i = 0; i < this.zzctb.length; i++) {
                adv advVar = this.zzctb[i];
                if (advVar != null) {
                    iZzb += adh.zzb(2, advVar);
                }
            }
            iZzn = iZzb;
        }
        if (this.body != null) {
            iZzn += adh.zzc(3, this.body);
        }
        if (this.zzctc != null) {
            iZzn += adh.zzc(4, this.zzctc);
        }
        return this.zzctd != null ? iZzn + adh.zzs(5, this.zzctd.intValue()) : iZzn;
    }
}
