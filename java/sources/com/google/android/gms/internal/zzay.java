package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class zzay extends adj<zzay> {
    private static volatile zzay[] zzca;
    public Integer zzcf;
    public Integer zzcj;
    public Long zzbk = null;
    public Long zzbl = null;
    public Long zzcb = null;
    public Long zzcc = null;
    public Long zzcd = null;
    public Long zzce = null;
    public Long zzcg = null;
    public Long zzch = null;
    public Long zzci = null;
    public Long zzck = null;
    public Long zzcl = null;
    public Long zzcm = null;
    public Long zzcn = null;
    public Long zzco = null;
    public Long zzcp = null;
    public Long zzcq = null;
    public Long zzcr = null;
    public Long zzcs = null;
    public Long zzct = null;

    public zzay() {
        this.zzcsx = -1;
    }

    public static zzay[] zzo() {
        if (zzca == null) {
            synchronized (adn.zzcsw) {
                if (zzca == null) {
                    zzca = new zzay[0];
                }
            }
        }
        return zzca;
    }

    @Override // com.google.android.gms.internal.adp
    public final /* synthetic */ adp zza(adg adgVar) throws IOException {
        while (true) {
            int iZzLA = adgVar.zzLA();
            switch (iZzLA) {
                case 0:
                    break;
                case 8:
                    this.zzbk = Long.valueOf(adgVar.zzLG());
                    break;
                case 16:
                    this.zzbl = Long.valueOf(adgVar.zzLG());
                    break;
                case MotionEventCompat.AXIS_DISTANCE /* 24 */:
                    this.zzcb = Long.valueOf(adgVar.zzLG());
                    break;
                case 32:
                    this.zzcc = Long.valueOf(adgVar.zzLG());
                    break;
                case MotionEventCompat.AXIS_GENERIC_9 /* 40 */:
                    this.zzcd = Long.valueOf(adgVar.zzLG());
                    break;
                case 48:
                    this.zzce = Long.valueOf(adgVar.zzLG());
                    break;
                case 56:
                    int position = adgVar.getPosition();
                    int iZzLF = adgVar.zzLF();
                    switch (iZzLF) {
                        case 0:
                        case 1:
                        case 2:
                        case 1000:
                            this.zzcf = Integer.valueOf(iZzLF);
                            break;
                        default:
                            adgVar.zzcp(position);
                            zza(adgVar, iZzLA);
                            break;
                    }
                    break;
                case 64:
                    this.zzcg = Long.valueOf(adgVar.zzLG());
                    break;
                case 72:
                    this.zzch = Long.valueOf(adgVar.zzLG());
                    break;
                case 80:
                    this.zzci = Long.valueOf(adgVar.zzLG());
                    break;
                case 88:
                    int position2 = adgVar.getPosition();
                    int iZzLF2 = adgVar.zzLF();
                    switch (iZzLF2) {
                        case 0:
                        case 1:
                        case 2:
                        case 1000:
                            this.zzcj = Integer.valueOf(iZzLF2);
                            break;
                        default:
                            adgVar.zzcp(position2);
                            zza(adgVar, iZzLA);
                            break;
                    }
                    break;
                case 96:
                    this.zzck = Long.valueOf(adgVar.zzLG());
                    break;
                case 104:
                    this.zzcl = Long.valueOf(adgVar.zzLG());
                    break;
                case 112:
                    this.zzcm = Long.valueOf(adgVar.zzLG());
                    break;
                case 120:
                    this.zzcn = Long.valueOf(adgVar.zzLG());
                    break;
                case 128:
                    this.zzco = Long.valueOf(adgVar.zzLG());
                    break;
                case 136:
                    this.zzcp = Long.valueOf(adgVar.zzLG());
                    break;
                case 144:
                    this.zzcq = Long.valueOf(adgVar.zzLG());
                    break;
                case 152:
                    this.zzcr = Long.valueOf(adgVar.zzLG());
                    break;
                case 160:
                    this.zzcs = Long.valueOf(adgVar.zzLG());
                    break;
                case 168:
                    this.zzct = Long.valueOf(adgVar.zzLG());
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
        if (this.zzbk != null) {
            adhVar.zzb(1, this.zzbk.longValue());
        }
        if (this.zzbl != null) {
            adhVar.zzb(2, this.zzbl.longValue());
        }
        if (this.zzcb != null) {
            adhVar.zzb(3, this.zzcb.longValue());
        }
        if (this.zzcc != null) {
            adhVar.zzb(4, this.zzcc.longValue());
        }
        if (this.zzcd != null) {
            adhVar.zzb(5, this.zzcd.longValue());
        }
        if (this.zzce != null) {
            adhVar.zzb(6, this.zzce.longValue());
        }
        if (this.zzcf != null) {
            adhVar.zzr(7, this.zzcf.intValue());
        }
        if (this.zzcg != null) {
            adhVar.zzb(8, this.zzcg.longValue());
        }
        if (this.zzch != null) {
            adhVar.zzb(9, this.zzch.longValue());
        }
        if (this.zzci != null) {
            adhVar.zzb(10, this.zzci.longValue());
        }
        if (this.zzcj != null) {
            adhVar.zzr(11, this.zzcj.intValue());
        }
        if (this.zzck != null) {
            adhVar.zzb(12, this.zzck.longValue());
        }
        if (this.zzcl != null) {
            adhVar.zzb(13, this.zzcl.longValue());
        }
        if (this.zzcm != null) {
            adhVar.zzb(14, this.zzcm.longValue());
        }
        if (this.zzcn != null) {
            adhVar.zzb(15, this.zzcn.longValue());
        }
        if (this.zzco != null) {
            adhVar.zzb(16, this.zzco.longValue());
        }
        if (this.zzcp != null) {
            adhVar.zzb(17, this.zzcp.longValue());
        }
        if (this.zzcq != null) {
            adhVar.zzb(18, this.zzcq.longValue());
        }
        if (this.zzcr != null) {
            adhVar.zzb(19, this.zzcr.longValue());
        }
        if (this.zzcs != null) {
            adhVar.zzb(20, this.zzcs.longValue());
        }
        if (this.zzct != null) {
            adhVar.zzb(21, this.zzct.longValue());
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.zzbk != null) {
            iZzn += adh.zze(1, this.zzbk.longValue());
        }
        if (this.zzbl != null) {
            iZzn += adh.zze(2, this.zzbl.longValue());
        }
        if (this.zzcb != null) {
            iZzn += adh.zze(3, this.zzcb.longValue());
        }
        if (this.zzcc != null) {
            iZzn += adh.zze(4, this.zzcc.longValue());
        }
        if (this.zzcd != null) {
            iZzn += adh.zze(5, this.zzcd.longValue());
        }
        if (this.zzce != null) {
            iZzn += adh.zze(6, this.zzce.longValue());
        }
        if (this.zzcf != null) {
            iZzn += adh.zzs(7, this.zzcf.intValue());
        }
        if (this.zzcg != null) {
            iZzn += adh.zze(8, this.zzcg.longValue());
        }
        if (this.zzch != null) {
            iZzn += adh.zze(9, this.zzch.longValue());
        }
        if (this.zzci != null) {
            iZzn += adh.zze(10, this.zzci.longValue());
        }
        if (this.zzcj != null) {
            iZzn += adh.zzs(11, this.zzcj.intValue());
        }
        if (this.zzck != null) {
            iZzn += adh.zze(12, this.zzck.longValue());
        }
        if (this.zzcl != null) {
            iZzn += adh.zze(13, this.zzcl.longValue());
        }
        if (this.zzcm != null) {
            iZzn += adh.zze(14, this.zzcm.longValue());
        }
        if (this.zzcn != null) {
            iZzn += adh.zze(15, this.zzcn.longValue());
        }
        if (this.zzco != null) {
            iZzn += adh.zze(16, this.zzco.longValue());
        }
        if (this.zzcp != null) {
            iZzn += adh.zze(17, this.zzcp.longValue());
        }
        if (this.zzcq != null) {
            iZzn += adh.zze(18, this.zzcq.longValue());
        }
        if (this.zzcr != null) {
            iZzn += adh.zze(19, this.zzcr.longValue());
        }
        if (this.zzcs != null) {
            iZzn += adh.zze(20, this.zzcs.longValue());
        }
        return this.zzct != null ? iZzn + adh.zze(21, this.zzct.longValue()) : iZzn;
    }
}
