package com.google.android.gms.internal;

import android.content.Context;
import com.google.android.gms.common.internal.zzbo;
import java.util.concurrent.atomic.AtomicBoolean;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public abstract class zzxr implements zzahp<Void>, zzakf {
    protected final Context mContext;
    protected final zzaka zzJH;
    private zzxy zzQP;
    private zzafg zzQQ;
    protected zzaai zzQR;
    private Runnable zzQS;
    private Object zzQT = new Object();
    private AtomicBoolean zzQU = new AtomicBoolean(true);

    protected zzxr(Context context, zzafg zzafgVar, zzaka zzakaVar, zzxy zzxyVar) {
        this.mContext = context;
        this.zzQQ = zzafgVar;
        this.zzQR = this.zzQQ.zzXY;
        this.zzJH = zzakaVar;
        this.zzQP = zzxyVar;
    }

    @Override // com.google.android.gms.internal.zzahp
    public void cancel() {
        if (this.zzQU.getAndSet(false)) {
            this.zzJH.stopLoading();
            com.google.android.gms.ads.internal.zzbs.zzbB();
            zzahe.zzk(this.zzJH);
            zzr(-1);
            zzagz.zzZr.removeCallbacks(this.zzQS);
        }
    }

    @Override // com.google.android.gms.internal.zzakf
    public final void zza(zzaka zzakaVar, boolean z) {
        zzafr.zzaC("WebView finished loading.");
        if (this.zzQU.getAndSet(false)) {
            zzr(z ? -2 : 0);
            zzagz.zzZr.removeCallbacks(this.zzQS);
        }
    }

    protected abstract void zzgo();

    @Override // com.google.android.gms.internal.zzahp
    public final /* synthetic */ Void zzgp() {
        zzbo.zzcz("Webview render task needs to be called on UI thread.");
        this.zzQS = new zzxs(this);
        zzagz.zzZr.postDelayed(this.zzQS, ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEL)).longValue());
        zzgo();
        return null;
    }

    protected void zzr(int i) {
        if (i != -2) {
            this.zzQR = new zzaai(i, this.zzQR.zzMg);
        }
        this.zzJH.zzir();
        zzxy zzxyVar = this.zzQP;
        zzaae zzaaeVar = this.zzQQ.zzUj;
        zzxyVar.zzb(new zzaff(zzaaeVar.zzSz, this.zzJH, this.zzQR.zzMa, i, this.zzQR.zzMb, this.zzQR.zzTq, this.zzQR.orientation, this.zzQR.zzMg, zzaaeVar.zzSC, this.zzQR.zzTo, null, null, null, null, null, this.zzQR.zzTp, this.zzQQ.zzvX, this.zzQR.zzTn, this.zzQQ.zzXR, this.zzQR.zzTs, this.zzQR.zzTt, this.zzQQ.zzXL, null, this.zzQR.zzTD, this.zzQR.zzTE, this.zzQR.zzTF, this.zzQR.zzTG, this.zzQR.zzTH, null, this.zzQR.zzMd, this.zzQR.zzTK, this.zzQQ.zzXX));
    }
}
