package com.google.android.gms.internal;

import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class adv extends adj<adv> {
    private static volatile adv[] zzcsY;
    public byte[] zzcsZ = null;
    public byte[] zzcnR = null;

    public adv() {
        this.zzcso = null;
        this.zzcsx = -1;
    }

    public static adv[] zzLW() {
        if (zzcsY == null) {
            synchronized (adn.zzcsw) {
                if (zzcsY == null) {
                    zzcsY = new adv[0];
                }
            }
        }
        return zzcsY;
    }

    @Override // com.google.android.gms.internal.adp
    public final /* synthetic */ adp zza(adg adgVar) throws IOException {
        while (true) {
            int iZzLA = adgVar.zzLA();
            switch (iZzLA) {
                case 0:
                    break;
                case 10:
                    this.zzcsZ = adgVar.readBytes();
                    break;
                case 18:
                    this.zzcnR = adgVar.readBytes();
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
        adhVar.zzb(1, this.zzcsZ);
        if (this.zzcnR != null) {
            adhVar.zzb(2, this.zzcnR);
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn() + adh.zzc(1, this.zzcsZ);
        return this.zzcnR != null ? iZzn + adh.zzc(2, this.zzcnR) : iZzn;
    }
}
