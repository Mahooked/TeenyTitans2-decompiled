package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;
import org.apache.http.HttpStatus;

/* JADX INFO: loaded from: classes.dex */
public final class zzax extends adj<zzax> {
    public Integer zzbH;
    public Integer zzbI;
    public Integer zzbO;
    public Integer zzbV;
    public String zzaY = null;
    public String zzaP = null;
    public Long zzaZ = null;
    private Long zzba = null;
    public Long zzbb = null;
    public Long zzbc = null;
    private Long zzbd = null;
    private Long zzbe = null;
    private Long zzbf = null;
    private Long zzbg = null;
    private Long zzbh = null;
    public Long zzbi = null;
    private String zzbj = null;
    public Long zzbk = null;
    public Long zzbl = null;
    public Long zzbm = null;
    public Long zzbn = null;
    private Long zzbo = null;
    private Long zzbp = null;
    public Long zzbq = null;
    public Long zzbr = null;
    public Long zzbs = null;
    public String zzaT = null;
    public String zzbt = null;
    public Long zzbu = null;
    public Long zzbv = null;
    public Long zzbw = null;
    public String zzaV = null;
    public Long zzbx = null;
    public Long zzby = null;
    public Long zzbz = null;
    private zzba zzbA = null;
    public Long zzbB = null;
    public Long zzbC = null;
    public Long zzbD = null;
    public Long zzbE = null;
    public Long zzbF = null;
    public Long zzbG = null;
    public String zzaW = null;
    public String zzaX = null;
    public Long zzbJ = null;
    public Long zzbK = null;
    private Long zzbL = null;
    private Long zzbM = null;
    private Long zzbN = null;
    public zzay zzbP = null;
    public zzay[] zzbQ = zzay.zzo();
    public zzaz zzbR = null;
    private Long zzbS = null;
    private Long zzbT = null;
    public String zzbU = null;
    public Boolean zzbW = null;
    private String zzbX = null;
    public Long zzbY = null;
    public zzbd zzbZ = null;

    public zzax() {
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
                    this.zzaY = adgVar.readString();
                    break;
                case 18:
                    this.zzaP = adgVar.readString();
                    break;
                case MotionEventCompat.AXIS_DISTANCE /* 24 */:
                    this.zzaZ = Long.valueOf(adgVar.zzLG());
                    break;
                case 32:
                    this.zzba = Long.valueOf(adgVar.zzLG());
                    break;
                case MotionEventCompat.AXIS_GENERIC_9 /* 40 */:
                    this.zzbb = Long.valueOf(adgVar.zzLG());
                    break;
                case 48:
                    this.zzbc = Long.valueOf(adgVar.zzLG());
                    break;
                case 56:
                    this.zzbd = Long.valueOf(adgVar.zzLG());
                    break;
                case 64:
                    this.zzbe = Long.valueOf(adgVar.zzLG());
                    break;
                case 72:
                    this.zzbf = Long.valueOf(adgVar.zzLG());
                    break;
                case 80:
                    this.zzbg = Long.valueOf(adgVar.zzLG());
                    break;
                case 88:
                    this.zzbh = Long.valueOf(adgVar.zzLG());
                    break;
                case 96:
                    this.zzbi = Long.valueOf(adgVar.zzLG());
                    break;
                case 106:
                    this.zzbj = adgVar.readString();
                    break;
                case 112:
                    this.zzbk = Long.valueOf(adgVar.zzLG());
                    break;
                case 120:
                    this.zzbl = Long.valueOf(adgVar.zzLG());
                    break;
                case 128:
                    this.zzbm = Long.valueOf(adgVar.zzLG());
                    break;
                case 136:
                    this.zzbn = Long.valueOf(adgVar.zzLG());
                    break;
                case 144:
                    this.zzbo = Long.valueOf(adgVar.zzLG());
                    break;
                case 152:
                    this.zzbp = Long.valueOf(adgVar.zzLG());
                    break;
                case 160:
                    this.zzbq = Long.valueOf(adgVar.zzLG());
                    break;
                case 168:
                    this.zzbT = Long.valueOf(adgVar.zzLG());
                    break;
                case 176:
                    this.zzbr = Long.valueOf(adgVar.zzLG());
                    break;
                case 184:
                    this.zzbs = Long.valueOf(adgVar.zzLG());
                    break;
                case 194:
                    this.zzbU = adgVar.readString();
                    break;
                case HttpStatus.SC_OK /* 200 */:
                    this.zzbY = Long.valueOf(adgVar.zzLG());
                    break;
                case 208:
                    int position = adgVar.getPosition();
                    int iZzLF = adgVar.zzLF();
                    switch (iZzLF) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            this.zzbV = Integer.valueOf(iZzLF);
                            break;
                        default:
                            adgVar.zzcp(position);
                            zza(adgVar, iZzLA);
                            break;
                    }
                    break;
                case 218:
                    this.zzaT = adgVar.readString();
                    break;
                case 224:
                    this.zzbW = Boolean.valueOf(adgVar.zzLD());
                    break;
                case 234:
                    this.zzbt = adgVar.readString();
                    break;
                case 242:
                    this.zzbX = adgVar.readString();
                    break;
                case 248:
                    this.zzbu = Long.valueOf(adgVar.zzLG());
                    break;
                case 256:
                    this.zzbv = Long.valueOf(adgVar.zzLG());
                    break;
                case 264:
                    this.zzbw = Long.valueOf(adgVar.zzLG());
                    break;
                case 274:
                    this.zzaV = adgVar.readString();
                    break;
                case 280:
                    this.zzbx = Long.valueOf(adgVar.zzLG());
                    break;
                case 288:
                    this.zzby = Long.valueOf(adgVar.zzLG());
                    break;
                case 296:
                    this.zzbz = Long.valueOf(adgVar.zzLG());
                    break;
                case 306:
                    if (this.zzbA == null) {
                        this.zzbA = new zzba();
                    }
                    adgVar.zza(this.zzbA);
                    break;
                case 312:
                    this.zzbB = Long.valueOf(adgVar.zzLG());
                    break;
                case 320:
                    this.zzbC = Long.valueOf(adgVar.zzLG());
                    break;
                case 328:
                    this.zzbD = Long.valueOf(adgVar.zzLG());
                    break;
                case 336:
                    this.zzbE = Long.valueOf(adgVar.zzLG());
                    break;
                case 346:
                    int iZzb = ads.zzb(adgVar, 346);
                    int length = this.zzbQ == null ? 0 : this.zzbQ.length;
                    zzay[] zzayVarArr = new zzay[iZzb + length];
                    if (length != 0) {
                        System.arraycopy(this.zzbQ, 0, zzayVarArr, 0, length);
                    }
                    while (length < zzayVarArr.length - 1) {
                        zzayVarArr[length] = new zzay();
                        adgVar.zza(zzayVarArr[length]);
                        adgVar.zzLA();
                        length++;
                    }
                    zzayVarArr[length] = new zzay();
                    adgVar.zza(zzayVarArr[length]);
                    this.zzbQ = zzayVarArr;
                    break;
                case 352:
                    this.zzbF = Long.valueOf(adgVar.zzLG());
                    break;
                case 360:
                    this.zzbG = Long.valueOf(adgVar.zzLG());
                    break;
                case 370:
                    this.zzaW = adgVar.readString();
                    break;
                case 378:
                    this.zzaX = adgVar.readString();
                    break;
                case 384:
                    int position2 = adgVar.getPosition();
                    int iZzLF2 = adgVar.zzLF();
                    switch (iZzLF2) {
                        case 0:
                        case 1:
                        case 2:
                        case 1000:
                            this.zzbH = Integer.valueOf(iZzLF2);
                            break;
                        default:
                            adgVar.zzcp(position2);
                            zza(adgVar, iZzLA);
                            break;
                    }
                    break;
                case 392:
                    int position3 = adgVar.getPosition();
                    int iZzLF3 = adgVar.zzLF();
                    switch (iZzLF3) {
                        case 0:
                        case 1:
                        case 2:
                        case 1000:
                            this.zzbI = Integer.valueOf(iZzLF3);
                            break;
                        default:
                            adgVar.zzcp(position3);
                            zza(adgVar, iZzLA);
                            break;
                    }
                    break;
                case HttpStatus.SC_PAYMENT_REQUIRED /* 402 */:
                    if (this.zzbP == null) {
                        this.zzbP = new zzay();
                    }
                    adgVar.zza(this.zzbP);
                    break;
                case HttpStatus.SC_REQUEST_TIMEOUT /* 408 */:
                    this.zzbJ = Long.valueOf(adgVar.zzLG());
                    break;
                case HttpStatus.SC_REQUESTED_RANGE_NOT_SATISFIABLE /* 416 */:
                    this.zzbK = Long.valueOf(adgVar.zzLG());
                    break;
                case HttpStatus.SC_FAILED_DEPENDENCY /* 424 */:
                    this.zzbL = Long.valueOf(adgVar.zzLG());
                    break;
                case 432:
                    this.zzbM = Long.valueOf(adgVar.zzLG());
                    break;
                case 440:
                    this.zzbN = Long.valueOf(adgVar.zzLG());
                    break;
                case 448:
                    int position4 = adgVar.getPosition();
                    int iZzLF4 = adgVar.zzLF();
                    switch (iZzLF4) {
                        case 0:
                        case 1:
                        case 2:
                        case 1000:
                            this.zzbO = Integer.valueOf(iZzLF4);
                            break;
                        default:
                            adgVar.zzcp(position4);
                            zza(adgVar, iZzLA);
                            break;
                    }
                    break;
                case 458:
                    if (this.zzbR == null) {
                        this.zzbR = new zzaz();
                    }
                    adgVar.zza(this.zzbR);
                    break;
                case 464:
                    this.zzbS = Long.valueOf(adgVar.zzLG());
                    break;
                case 1610:
                    if (this.zzbZ == null) {
                        this.zzbZ = new zzbd();
                    }
                    adgVar.zza(this.zzbZ);
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
        if (this.zzaY != null) {
            adhVar.zzl(1, this.zzaY);
        }
        if (this.zzaP != null) {
            adhVar.zzl(2, this.zzaP);
        }
        if (this.zzaZ != null) {
            adhVar.zzb(3, this.zzaZ.longValue());
        }
        if (this.zzba != null) {
            adhVar.zzb(4, this.zzba.longValue());
        }
        if (this.zzbb != null) {
            adhVar.zzb(5, this.zzbb.longValue());
        }
        if (this.zzbc != null) {
            adhVar.zzb(6, this.zzbc.longValue());
        }
        if (this.zzbd != null) {
            adhVar.zzb(7, this.zzbd.longValue());
        }
        if (this.zzbe != null) {
            adhVar.zzb(8, this.zzbe.longValue());
        }
        if (this.zzbf != null) {
            adhVar.zzb(9, this.zzbf.longValue());
        }
        if (this.zzbg != null) {
            adhVar.zzb(10, this.zzbg.longValue());
        }
        if (this.zzbh != null) {
            adhVar.zzb(11, this.zzbh.longValue());
        }
        if (this.zzbi != null) {
            adhVar.zzb(12, this.zzbi.longValue());
        }
        if (this.zzbj != null) {
            adhVar.zzl(13, this.zzbj);
        }
        if (this.zzbk != null) {
            adhVar.zzb(14, this.zzbk.longValue());
        }
        if (this.zzbl != null) {
            adhVar.zzb(15, this.zzbl.longValue());
        }
        if (this.zzbm != null) {
            adhVar.zzb(16, this.zzbm.longValue());
        }
        if (this.zzbn != null) {
            adhVar.zzb(17, this.zzbn.longValue());
        }
        if (this.zzbo != null) {
            adhVar.zzb(18, this.zzbo.longValue());
        }
        if (this.zzbp != null) {
            adhVar.zzb(19, this.zzbp.longValue());
        }
        if (this.zzbq != null) {
            adhVar.zzb(20, this.zzbq.longValue());
        }
        if (this.zzbT != null) {
            adhVar.zzb(21, this.zzbT.longValue());
        }
        if (this.zzbr != null) {
            adhVar.zzb(22, this.zzbr.longValue());
        }
        if (this.zzbs != null) {
            adhVar.zzb(23, this.zzbs.longValue());
        }
        if (this.zzbU != null) {
            adhVar.zzl(24, this.zzbU);
        }
        if (this.zzbY != null) {
            adhVar.zzb(25, this.zzbY.longValue());
        }
        if (this.zzbV != null) {
            adhVar.zzr(26, this.zzbV.intValue());
        }
        if (this.zzaT != null) {
            adhVar.zzl(27, this.zzaT);
        }
        if (this.zzbW != null) {
            adhVar.zzk(28, this.zzbW.booleanValue());
        }
        if (this.zzbt != null) {
            adhVar.zzl(29, this.zzbt);
        }
        if (this.zzbX != null) {
            adhVar.zzl(30, this.zzbX);
        }
        if (this.zzbu != null) {
            adhVar.zzb(31, this.zzbu.longValue());
        }
        if (this.zzbv != null) {
            adhVar.zzb(32, this.zzbv.longValue());
        }
        if (this.zzbw != null) {
            adhVar.zzb(33, this.zzbw.longValue());
        }
        if (this.zzaV != null) {
            adhVar.zzl(34, this.zzaV);
        }
        if (this.zzbx != null) {
            adhVar.zzb(35, this.zzbx.longValue());
        }
        if (this.zzby != null) {
            adhVar.zzb(36, this.zzby.longValue());
        }
        if (this.zzbz != null) {
            adhVar.zzb(37, this.zzbz.longValue());
        }
        if (this.zzbA != null) {
            adhVar.zza(38, this.zzbA);
        }
        if (this.zzbB != null) {
            adhVar.zzb(39, this.zzbB.longValue());
        }
        if (this.zzbC != null) {
            adhVar.zzb(40, this.zzbC.longValue());
        }
        if (this.zzbD != null) {
            adhVar.zzb(41, this.zzbD.longValue());
        }
        if (this.zzbE != null) {
            adhVar.zzb(42, this.zzbE.longValue());
        }
        if (this.zzbQ != null && this.zzbQ.length > 0) {
            for (int i = 0; i < this.zzbQ.length; i++) {
                zzay zzayVar = this.zzbQ[i];
                if (zzayVar != null) {
                    adhVar.zza(43, zzayVar);
                }
            }
        }
        if (this.zzbF != null) {
            adhVar.zzb(44, this.zzbF.longValue());
        }
        if (this.zzbG != null) {
            adhVar.zzb(45, this.zzbG.longValue());
        }
        if (this.zzaW != null) {
            adhVar.zzl(46, this.zzaW);
        }
        if (this.zzaX != null) {
            adhVar.zzl(47, this.zzaX);
        }
        if (this.zzbH != null) {
            adhVar.zzr(48, this.zzbH.intValue());
        }
        if (this.zzbI != null) {
            adhVar.zzr(49, this.zzbI.intValue());
        }
        if (this.zzbP != null) {
            adhVar.zza(50, this.zzbP);
        }
        if (this.zzbJ != null) {
            adhVar.zzb(51, this.zzbJ.longValue());
        }
        if (this.zzbK != null) {
            adhVar.zzb(52, this.zzbK.longValue());
        }
        if (this.zzbL != null) {
            adhVar.zzb(53, this.zzbL.longValue());
        }
        if (this.zzbM != null) {
            adhVar.zzb(54, this.zzbM.longValue());
        }
        if (this.zzbN != null) {
            adhVar.zzb(55, this.zzbN.longValue());
        }
        if (this.zzbO != null) {
            adhVar.zzr(56, this.zzbO.intValue());
        }
        if (this.zzbR != null) {
            adhVar.zza(57, this.zzbR);
        }
        if (this.zzbS != null) {
            adhVar.zzb(58, this.zzbS.longValue());
        }
        if (this.zzbZ != null) {
            adhVar.zza(HttpStatus.SC_CREATED, this.zzbZ);
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.zzaY != null) {
            iZzn += adh.zzm(1, this.zzaY);
        }
        if (this.zzaP != null) {
            iZzn += adh.zzm(2, this.zzaP);
        }
        if (this.zzaZ != null) {
            iZzn += adh.zze(3, this.zzaZ.longValue());
        }
        if (this.zzba != null) {
            iZzn += adh.zze(4, this.zzba.longValue());
        }
        if (this.zzbb != null) {
            iZzn += adh.zze(5, this.zzbb.longValue());
        }
        if (this.zzbc != null) {
            iZzn += adh.zze(6, this.zzbc.longValue());
        }
        if (this.zzbd != null) {
            iZzn += adh.zze(7, this.zzbd.longValue());
        }
        if (this.zzbe != null) {
            iZzn += adh.zze(8, this.zzbe.longValue());
        }
        if (this.zzbf != null) {
            iZzn += adh.zze(9, this.zzbf.longValue());
        }
        if (this.zzbg != null) {
            iZzn += adh.zze(10, this.zzbg.longValue());
        }
        if (this.zzbh != null) {
            iZzn += adh.zze(11, this.zzbh.longValue());
        }
        if (this.zzbi != null) {
            iZzn += adh.zze(12, this.zzbi.longValue());
        }
        if (this.zzbj != null) {
            iZzn += adh.zzm(13, this.zzbj);
        }
        if (this.zzbk != null) {
            iZzn += adh.zze(14, this.zzbk.longValue());
        }
        if (this.zzbl != null) {
            iZzn += adh.zze(15, this.zzbl.longValue());
        }
        if (this.zzbm != null) {
            iZzn += adh.zze(16, this.zzbm.longValue());
        }
        if (this.zzbn != null) {
            iZzn += adh.zze(17, this.zzbn.longValue());
        }
        if (this.zzbo != null) {
            iZzn += adh.zze(18, this.zzbo.longValue());
        }
        if (this.zzbp != null) {
            iZzn += adh.zze(19, this.zzbp.longValue());
        }
        if (this.zzbq != null) {
            iZzn += adh.zze(20, this.zzbq.longValue());
        }
        if (this.zzbT != null) {
            iZzn += adh.zze(21, this.zzbT.longValue());
        }
        if (this.zzbr != null) {
            iZzn += adh.zze(22, this.zzbr.longValue());
        }
        if (this.zzbs != null) {
            iZzn += adh.zze(23, this.zzbs.longValue());
        }
        if (this.zzbU != null) {
            iZzn += adh.zzm(24, this.zzbU);
        }
        if (this.zzbY != null) {
            iZzn += adh.zze(25, this.zzbY.longValue());
        }
        if (this.zzbV != null) {
            iZzn += adh.zzs(26, this.zzbV.intValue());
        }
        if (this.zzaT != null) {
            iZzn += adh.zzm(27, this.zzaT);
        }
        if (this.zzbW != null) {
            this.zzbW.booleanValue();
            iZzn += adh.zzct(28) + 1;
        }
        if (this.zzbt != null) {
            iZzn += adh.zzm(29, this.zzbt);
        }
        if (this.zzbX != null) {
            iZzn += adh.zzm(30, this.zzbX);
        }
        if (this.zzbu != null) {
            iZzn += adh.zze(31, this.zzbu.longValue());
        }
        if (this.zzbv != null) {
            iZzn += adh.zze(32, this.zzbv.longValue());
        }
        if (this.zzbw != null) {
            iZzn += adh.zze(33, this.zzbw.longValue());
        }
        if (this.zzaV != null) {
            iZzn += adh.zzm(34, this.zzaV);
        }
        if (this.zzbx != null) {
            iZzn += adh.zze(35, this.zzbx.longValue());
        }
        if (this.zzby != null) {
            iZzn += adh.zze(36, this.zzby.longValue());
        }
        if (this.zzbz != null) {
            iZzn += adh.zze(37, this.zzbz.longValue());
        }
        if (this.zzbA != null) {
            iZzn += adh.zzb(38, this.zzbA);
        }
        if (this.zzbB != null) {
            iZzn += adh.zze(39, this.zzbB.longValue());
        }
        if (this.zzbC != null) {
            iZzn += adh.zze(40, this.zzbC.longValue());
        }
        if (this.zzbD != null) {
            iZzn += adh.zze(41, this.zzbD.longValue());
        }
        if (this.zzbE != null) {
            iZzn += adh.zze(42, this.zzbE.longValue());
        }
        if (this.zzbQ != null && this.zzbQ.length > 0) {
            int iZzb = iZzn;
            for (int i = 0; i < this.zzbQ.length; i++) {
                zzay zzayVar = this.zzbQ[i];
                if (zzayVar != null) {
                    iZzb += adh.zzb(43, zzayVar);
                }
            }
            iZzn = iZzb;
        }
        if (this.zzbF != null) {
            iZzn += adh.zze(44, this.zzbF.longValue());
        }
        if (this.zzbG != null) {
            iZzn += adh.zze(45, this.zzbG.longValue());
        }
        if (this.zzaW != null) {
            iZzn += adh.zzm(46, this.zzaW);
        }
        if (this.zzaX != null) {
            iZzn += adh.zzm(47, this.zzaX);
        }
        if (this.zzbH != null) {
            iZzn += adh.zzs(48, this.zzbH.intValue());
        }
        if (this.zzbI != null) {
            iZzn += adh.zzs(49, this.zzbI.intValue());
        }
        if (this.zzbP != null) {
            iZzn += adh.zzb(50, this.zzbP);
        }
        if (this.zzbJ != null) {
            iZzn += adh.zze(51, this.zzbJ.longValue());
        }
        if (this.zzbK != null) {
            iZzn += adh.zze(52, this.zzbK.longValue());
        }
        if (this.zzbL != null) {
            iZzn += adh.zze(53, this.zzbL.longValue());
        }
        if (this.zzbM != null) {
            iZzn += adh.zze(54, this.zzbM.longValue());
        }
        if (this.zzbN != null) {
            iZzn += adh.zze(55, this.zzbN.longValue());
        }
        if (this.zzbO != null) {
            iZzn += adh.zzs(56, this.zzbO.intValue());
        }
        if (this.zzbR != null) {
            iZzn += adh.zzb(57, this.zzbR);
        }
        if (this.zzbS != null) {
            iZzn += adh.zze(58, this.zzbS.longValue());
        }
        return this.zzbZ != null ? iZzn + adh.zzb(HttpStatus.SC_CREATED, this.zzbZ) : iZzn;
    }
}
