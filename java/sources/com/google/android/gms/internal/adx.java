package com.google.android.gms.internal;

import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class adx extends adj<adx> {
    private byte[] zzcte = null;
    private byte[] zzctf = null;
    private byte[] zzctg = null;

    public adx() {
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
                    this.zzcte = adgVar.readBytes();
                    break;
                case 18:
                    this.zzctf = adgVar.readBytes();
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
        if (this.zzcte != null) {
            adhVar.zzb(1, this.zzcte);
        }
        if (this.zzctf != null) {
            adhVar.zzb(2, this.zzctf);
        }
        if (this.zzctg != null) {
            adhVar.zzb(3, this.zzctg);
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.zzcte != null) {
            iZzn += adh.zzc(1, this.zzcte);
        }
        if (this.zzctf != null) {
            iZzn += adh.zzc(2, this.zzctf);
        }
        return this.zzctg != null ? iZzn + adh.zzc(3, this.zzctg) : iZzn;
    }
}
