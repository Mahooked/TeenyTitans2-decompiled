package com.google.android.gms.internal;

import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class adz extends adj<adz> {
    private Integer zzctj = null;
    private byte[] zzctk = null;
    private byte[] zzctg = null;

    public adz() {
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
                case 8:
                    this.zzctj = Integer.valueOf(adgVar.zzLC());
                    break;
                case 18:
                    this.zzctk = adgVar.readBytes();
                    break;
                case 26:
                    this.zzctg = adgVar.readBytes();
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
        if (this.zzctj != null) {
            adhVar.zzr(1, this.zzctj.intValue());
        }
        if (this.zzctk != null) {
            adhVar.zzb(2, this.zzctk);
        }
        if (this.zzctg != null) {
            adhVar.zzb(3, this.zzctg);
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.zzctj != null) {
            iZzn += adh.zzs(1, this.zzctj.intValue());
        }
        if (this.zzctk != null) {
            iZzn += adh.zzc(2, this.zzctk);
        }
        return this.zzctg != null ? iZzn + adh.zzc(3, this.zzctg) : iZzn;
    }
}
