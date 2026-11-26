package com.google.android.gms.internal;

import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class ael extends adj<ael> implements Cloneable {
    private int zzcuk = -1;
    private int zzcul = 0;

    public ael() {
        this.zzcso = null;
        this.zzcsx = -1;
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    /* JADX INFO: renamed from: zzMg, reason: merged with bridge method [inline-methods] */
    public ael clone() {
        try {
            return (ael) super.clone();
        } catch (CloneNotSupportedException e) {
            throw new AssertionError(e);
        }
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof ael)) {
            return false;
        }
        ael aelVar = (ael) obj;
        if (this.zzcuk == aelVar.zzcuk && this.zzcul == aelVar.zzcul) {
            if (this.zzcso == null || this.zzcso.isEmpty()) {
                return aelVar.zzcso == null || aelVar.zzcso.isEmpty();
            }
            return this.zzcso.equals(aelVar.zzcso);
        }
        return false;
    }

    public final int hashCode() {
        return ((this.zzcso == null || this.zzcso.isEmpty()) ? 0 : this.zzcso.hashCode()) + ((((((getClass().getName().hashCode() + 527) * 31) + this.zzcuk) * 31) + this.zzcul) * 31);
    }

    @Override // com.google.android.gms.internal.adj
    /* JADX INFO: renamed from: zzLN */
    public final /* synthetic */ adj clone() throws CloneNotSupportedException {
        return (ael) clone();
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    /* JADX INFO: renamed from: zzLO */
    public final /* synthetic */ adp clone() throws CloneNotSupportedException {
        return (ael) clone();
    }

    @Override // com.google.android.gms.internal.adp
    public final /* synthetic */ adp zza(adg adgVar) throws IOException {
        while (true) {
            int iZzLA = adgVar.zzLA();
            switch (iZzLA) {
                case 0:
                    break;
                case 8:
                    int position = adgVar.getPosition();
                    int iZzLC = adgVar.zzLC();
                    switch (iZzLC) {
                        case -1:
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
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                            this.zzcuk = iZzLC;
                            break;
                        default:
                            adgVar.zzcp(position);
                            zza(adgVar, iZzLA);
                            break;
                    }
                    break;
                case 16:
                    int position2 = adgVar.getPosition();
                    int iZzLC2 = adgVar.zzLC();
                    switch (iZzLC2) {
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
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 100:
                            this.zzcul = iZzLC2;
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
        if (this.zzcuk != -1) {
            adhVar.zzr(1, this.zzcuk);
        }
        if (this.zzcul != 0) {
            adhVar.zzr(2, this.zzcul);
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.zzcuk != -1) {
            iZzn += adh.zzs(1, this.zzcuk);
        }
        return this.zzcul != 0 ? iZzn + adh.zzs(2, this.zzcul) : iZzn;
    }
}
