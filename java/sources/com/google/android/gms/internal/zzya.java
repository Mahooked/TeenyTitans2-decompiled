package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzya extends zzafp {
    private final zzxy zzQP;
    private final zzafg zzQQ;
    private final zzaai zzQR;

    public zzya(zzafg zzafgVar, zzxy zzxyVar) {
        this.zzQQ = zzafgVar;
        this.zzQR = this.zzQQ.zzXY;
        this.zzQP = zzxyVar;
    }

    @Override // com.google.android.gms.internal.zzafp
    public void onStop() {
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        zzagz.zzZr.post(new zzyb(this, new zzaff(this.zzQQ.zzUj.zzSz, null, null, 0, null, null, this.zzQR.orientation, this.zzQR.zzMg, this.zzQQ.zzUj.zzSC, false, null, null, null, null, null, this.zzQR.zzTp, this.zzQQ.zzvX, this.zzQR.zzTn, this.zzQQ.zzXR, this.zzQR.zzTs, this.zzQR.zzTt, this.zzQQ.zzXL, null, null, null, null, this.zzQQ.zzXY.zzTG, this.zzQQ.zzXY.zzTH, null, null, null, this.zzQQ.zzXX)));
    }
}
