package com.google.android.gms.internal;

import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class zzau extends adj<zzau> {
    public zzav zzaQ = null;
    public zzaw zzaR = null;

    public zzau() {
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
                    if (this.zzaQ == null) {
                        this.zzaQ = new zzav();
                    }
                    adgVar.zza(this.zzaQ);
                    break;
                case 18:
                    if (this.zzaR == null) {
                        this.zzaR = new zzaw();
                    }
                    adgVar.zza(this.zzaR);
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
        if (this.zzaQ != null) {
            adhVar.zza(1, this.zzaQ);
        }
        if (this.zzaR != null) {
            adhVar.zza(2, this.zzaR);
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.zzaQ != null) {
            iZzn += adh.zzb(1, this.zzaQ);
        }
        return this.zzaR != null ? iZzn + adh.zzb(2, this.zzaR) : iZzn;
    }
}
