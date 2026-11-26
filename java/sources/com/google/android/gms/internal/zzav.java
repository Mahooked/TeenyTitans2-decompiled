package com.google.android.gms.internal;

import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class zzav extends adj<zzav> {
    public Integer zzaS;

    public zzav() {
        this.zzcsx = -1;
    }

    @Override // com.google.android.gms.internal.adp
    public final /* synthetic */ adp zza(adg adgVar) throws IOException {
        while (true) {
            int iZzLA = adgVar.zzLA();
            switch (iZzLA) {
                case 0:
                    break;
                case 216:
                    int position = adgVar.getPosition();
                    int iZzLF = adgVar.zzLF();
                    switch (iZzLF) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            this.zzaS = Integer.valueOf(iZzLF);
                            break;
                        default:
                            adgVar.zzcp(position);
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
        if (this.zzaS != null) {
            adhVar.zzr(27, this.zzaS.intValue());
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        return this.zzaS != null ? iZzn + adh.zzs(27, this.zzaS.intValue()) : iZzn;
    }
}
