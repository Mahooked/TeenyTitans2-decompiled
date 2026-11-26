package com.google.android.gms.internal;

import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class aea extends adj<aea> {
    public Integer zzcsJ = null;
    public String mimeType = null;
    public byte[] zzctl = null;

    public aea() {
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
                    int position = adgVar.getPosition();
                    int iZzLC = adgVar.zzLC();
                    switch (iZzLC) {
                        case 0:
                        case 1:
                            this.zzcsJ = Integer.valueOf(iZzLC);
                            break;
                        default:
                            adgVar.zzcp(position);
                            zza(adgVar, iZzLA);
                            break;
                    }
                    break;
                case 18:
                    this.mimeType = adgVar.readString();
                    break;
                case 26:
                    this.zzctl = adgVar.readBytes();
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
        if (this.zzcsJ != null) {
            adhVar.zzr(1, this.zzcsJ.intValue());
        }
        if (this.mimeType != null) {
            adhVar.zzl(2, this.mimeType);
        }
        if (this.zzctl != null) {
            adhVar.zzb(3, this.zzctl);
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.zzcsJ != null) {
            iZzn += adh.zzs(1, this.zzcsJ.intValue());
        }
        if (this.mimeType != null) {
            iZzn += adh.zzm(2, this.mimeType);
        }
        return this.zzctl != null ? iZzn + adh.zzc(3, this.zzctl) : iZzn;
    }
}
