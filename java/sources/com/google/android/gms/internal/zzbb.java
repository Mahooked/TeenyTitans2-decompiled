package com.google.android.gms.internal;

import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class zzbb extends adj<zzbb> {
    public byte[] zzcC = null;
    public byte[] zzcD = null;

    public zzbb() {
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
                    this.zzcC = adgVar.readBytes();
                    break;
                case 18:
                    this.zzcD = adgVar.readBytes();
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
        if (this.zzcC != null) {
            adhVar.zzb(1, this.zzcC);
        }
        if (this.zzcD != null) {
            adhVar.zzb(2, this.zzcD);
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.zzcC != null) {
            iZzn += adh.zzc(1, this.zzcC);
        }
        return this.zzcD != null ? iZzn + adh.zzc(2, this.zzcD) : iZzn;
    }
}
