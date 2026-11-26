package com.google.android.gms.internal;

import android.content.Context;
import android.os.SystemClock;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public abstract class zzxt extends zzafp {
    protected final Context mContext;
    protected final Object mLock;
    protected final zzxy zzQP;
    protected final zzafg zzQQ;
    protected zzaai zzQR;
    protected final Object zzQT;

    protected zzxt(Context context, zzafg zzafgVar, zzxy zzxyVar) {
        super(true);
        this.mLock = new Object();
        this.zzQT = new Object();
        this.mContext = context;
        this.zzQQ = zzafgVar;
        this.zzQR = zzafgVar.zzXY;
        this.zzQP = zzxyVar;
    }

    @Override // com.google.android.gms.internal.zzafp
    public void onStop() {
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        synchronized (this.mLock) {
            zzafr.zzaC("AdRendererBackgroundTask started.");
            int i = this.zzQQ.errorCode;
            try {
                zzd(SystemClock.elapsedRealtime());
            } catch (zzxw e) {
                int errorCode = e.getErrorCode();
                if (errorCode == 3 || errorCode == -1) {
                    zzafr.zzaS(e.getMessage());
                } else {
                    zzafr.zzaT(e.getMessage());
                }
                if (this.zzQR == null) {
                    this.zzQR = new zzaai(errorCode);
                } else {
                    this.zzQR = new zzaai(errorCode, this.zzQR.zzMg);
                }
                zzagz.zzZr.post(new zzxu(this));
                i = errorCode;
            }
            zzagz.zzZr.post(new zzxv(this, zzs(i)));
        }
    }

    protected abstract void zzd(long j) throws zzxw;

    protected abstract zzaff zzs(int i);
}
