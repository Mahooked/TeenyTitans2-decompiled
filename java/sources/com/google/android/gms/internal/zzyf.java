package com.google.android.gms.internal;

import android.content.Context;
import java.util.concurrent.CancellationException;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Future;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzyf extends zzafp {
    private final Object mLock;
    private final zzxy zzQP;
    private final zzafg zzQQ;
    private final zzaai zzQR;
    private final zzyn zzRg;
    private Future<zzaff> zzRh;

    public zzyf(Context context, com.google.android.gms.ads.internal.zzbb zzbbVar, zzafg zzafgVar, zzcu zzcuVar, zzxy zzxyVar, zznb zznbVar) {
        this(zzafgVar, zzxyVar, new zzyn(context, zzbbVar, new zzaie(context), zzcuVar, zzafgVar, zznbVar));
    }

    private zzyf(zzafg zzafgVar, zzxy zzxyVar, zzyn zzynVar) {
        this.mLock = new Object();
        this.zzQQ = zzafgVar;
        this.zzQR = zzafgVar.zzXY;
        this.zzQP = zzxyVar;
        this.zzRg = zzynVar;
    }

    @Override // com.google.android.gms.internal.zzafp
    public void onStop() {
        synchronized (this.mLock) {
            if (this.zzRh != null) {
                this.zzRh.cancel(true);
            }
        }
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        zzaff zzaffVar;
        int i = -2;
        try {
            synchronized (this.mLock) {
                this.zzRh = zzagt.zza(this.zzRg);
            }
            zzaffVar = this.zzRh.get(60000L, TimeUnit.MILLISECONDS);
        } catch (InterruptedException e) {
            i = 0;
            zzaffVar = null;
        } catch (CancellationException e2) {
            i = 0;
            zzaffVar = null;
        } catch (ExecutionException e3) {
            i = 0;
            zzaffVar = null;
        } catch (TimeoutException e4) {
            zzafr.zzaT("Timed out waiting for native ad.");
            i = 2;
            this.zzRh.cancel(true);
            zzaffVar = null;
        }
        if (zzaffVar == null) {
            zzaffVar = new zzaff(this.zzQQ.zzUj.zzSz, null, null, i, null, null, this.zzQR.orientation, this.zzQR.zzMg, this.zzQQ.zzUj.zzSC, false, null, null, null, null, null, this.zzQR.zzTp, this.zzQQ.zzvX, this.zzQR.zzTn, this.zzQQ.zzXR, this.zzQR.zzTs, this.zzQR.zzTt, this.zzQQ.zzXL, null, null, null, null, this.zzQQ.zzXY.zzTG, this.zzQQ.zzXY.zzTH, null, null, this.zzQR.zzTK, this.zzQQ.zzXX);
        }
        zzagz.zzZr.post(new zzyg(this, zzaffVar));
    }
}
