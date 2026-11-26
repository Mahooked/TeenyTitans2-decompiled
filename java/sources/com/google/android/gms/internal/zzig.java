package com.google.android.gms.internal;

import android.os.Handler;
import java.util.HashMap;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzig {
    private HashMap<String, Long> zzzA;
    private final zzij zzzB;
    private zzil zzzC;
    private boolean zzzD;
    private final boolean zzzE;
    private final int zzzF;
    private int zzzG;
    private zzih zzzH;

    private zzig() {
        this.zzzD = false;
        this.zzzG = 0;
        this.zzzE = false;
        this.zzzB = new zzik();
        this.zzzC = new zzil();
        this.zzzF = 0;
        zzdi();
    }

    public zzig(zzij zzijVar, boolean z) {
        this.zzzD = false;
        this.zzzG = 0;
        this.zzzB = zzijVar;
        this.zzzA = new HashMap<>();
        this.zzzE = z;
        this.zzzF = ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGE)).intValue();
        this.zzzC = new zzil();
        zzdi();
        com.google.android.gms.ads.internal.zzbs.zzbP().zzie();
    }

    public static zzig zzde() {
        return new zzig();
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final synchronized void zzdg() {
    }

    private final synchronized void zzdh() {
        Handler handler = com.google.android.gms.ads.internal.zzbs.zzbP().getHandler();
        zzih zzihVar = new zzih(this, this.zzzG + 1);
        handler.postDelayed(zzihVar, this.zzzF);
        this.zzzG++;
        if (this.zzzH != null) {
            handler.removeCallbacks(this.zzzH);
        }
        this.zzzH = zzihVar;
    }

    private final synchronized void zzdi() {
    }

    public final synchronized void zza(zzii zziiVar) {
        if (this.zzzE) {
            zziiVar.zza(this.zzzC);
        }
        if (this.zzzD && this.zzzE) {
            zzdh();
        }
    }

    public final void zzdf() {
        if (this.zzzE) {
            this.zzzD = true;
            zzdh();
        }
    }
}
