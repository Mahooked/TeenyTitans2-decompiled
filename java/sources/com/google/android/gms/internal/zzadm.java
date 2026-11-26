package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzadm extends zzafp implements zzads, zzadv {
    private final Context mContext;
    private final String zzMs;
    private final zzafg zzQQ;
    private final String zzWA;
    private final zzua zzWB;
    private final long zzWC;
    private zzadp zzWE;
    private final zzadz zzWy;
    private final zzadv zzWz;
    private int zzWD = 0;
    private int mErrorCode = 3;
    private final Object mLock = new Object();

    public zzadm(Context context, String str, String str2, zzua zzuaVar, zzafg zzafgVar, zzadz zzadzVar, zzadv zzadvVar, long j) {
        this.mContext = context;
        this.zzMs = str;
        this.zzWA = str2;
        this.zzWB = zzuaVar;
        this.zzQQ = zzafgVar;
        this.zzWy = zzadzVar;
        this.zzWz = zzadvVar;
        this.zzWC = j;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zza(zzir zzirVar, zzut zzutVar) {
        this.zzWy.zzgX().zza((zzadv) this);
        try {
            if ("com.google.ads.mediation.admob.AdMobAdapter".equals(this.zzMs)) {
                zzutVar.zza(zzirVar, this.zzWA, this.zzWB.zzLH);
            } else {
                zzutVar.zzc(zzirVar, this.zzWA);
            }
        } catch (RemoteException e) {
            zzafr.zzc("Fail to load ad from adapter.", e);
            zza(this.zzMs, 0);
        }
    }

    private final boolean zzf(long j) {
        long jElapsedRealtime = this.zzWC - (com.google.android.gms.ads.internal.zzbs.zzbF().elapsedRealtime() - j);
        if (jElapsedRealtime <= 0) {
            this.mErrorCode = 4;
            return false;
        }
        try {
            this.mLock.wait(jElapsedRealtime);
            return true;
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
            this.mErrorCode = 5;
            return false;
        }
    }

    @Override // com.google.android.gms.internal.zzafp
    public void onStop() {
    }

    @Override // com.google.android.gms.internal.zzadv
    public final void zza(String str, int i) {
        synchronized (this.mLock) {
            this.zzWD = 2;
            this.mErrorCode = i;
            this.mLock.notify();
        }
    }

    @Override // com.google.android.gms.internal.zzadv
    public final void zzaw(String str) {
        synchronized (this.mLock) {
            this.zzWD = 1;
            this.mLock.notify();
        }
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        long jElapsedRealtime;
        if (this.zzWy == null || this.zzWy.zzgX() == null || this.zzWy.zzgW() == null) {
            return;
        }
        zzadu zzaduVarZzgX = this.zzWy.zzgX();
        zzaduVarZzgX.zza((zzadv) null);
        zzaduVarZzgX.zza((zzads) this);
        zzir zzirVar = this.zzQQ.zzUj.zzSz;
        zzut zzutVarZzgW = this.zzWy.zzgW();
        try {
            if (zzutVarZzgW.isInitialized()) {
                zzaiy.zzaaH.post(new zzadn(this, zzirVar, zzutVarZzgW));
            } else {
                zzaiy.zzaaH.post(new zzado(this, zzutVarZzgW, zzirVar, zzaduVarZzgX));
            }
            while (true) {
                synchronized (this.mLock) {
                    if (this.zzWD != 0) {
                        this.zzWE = new zzadr().zzg(com.google.android.gms.ads.internal.zzbs.zzbF().elapsedRealtime() - jElapsedRealtime).zzw(1 == this.zzWD ? 6 : this.mErrorCode).zzax(this.zzMs).zzay(this.zzWB.zzLK).zzgU();
                    } else if (!zzf(jElapsedRealtime)) {
                        this.zzWE = new zzadr().zzw(this.mErrorCode).zzg(com.google.android.gms.ads.internal.zzbs.zzbF().elapsedRealtime() - jElapsedRealtime).zzax(this.zzMs).zzay(this.zzWB.zzLK).zzgU();
                    }
                }
            }
        } catch (RemoteException e) {
            zzafr.zzc("Fail to check if adapter is initialized.", e);
            zza(this.zzMs, 0);
        }
        jElapsedRealtime = com.google.android.gms.ads.internal.zzbs.zzbF().elapsedRealtime();
        zzaduVarZzgX.zza((zzadv) null);
        zzaduVarZzgX.zza((zzads) null);
        if (this.zzWD == 1) {
            this.zzWz.zzaw(this.zzMs);
        } else {
            this.zzWz.zza(this.zzMs, this.mErrorCode);
        }
    }

    public final zzadp zzgR() {
        zzadp zzadpVar;
        synchronized (this.mLock) {
            zzadpVar = this.zzWE;
        }
        return zzadpVar;
    }

    public final zzua zzgS() {
        return this.zzWB;
    }

    @Override // com.google.android.gms.internal.zzads
    public final void zzgT() {
        zza(this.zzQQ.zzUj.zzSz, this.zzWy.zzgW());
    }

    @Override // com.google.android.gms.internal.zzads
    public final void zzv(int i) {
        zza(this.zzMs, 0);
    }
}
