package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import android.text.TextUtils;
import com.google.ads.mediation.admob.AdMobAdapter;
import java.util.ArrayList;
import java.util.List;
import java.util.ListIterator;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzyd extends zzxt {
    private final zzaka zzJH;
    private zzub zzMu;
    private zztz zzRc;
    protected zzuh zzRd;
    private boolean zzRe;
    private final zznb zzsK;
    private zzuq zzsX;

    zzyd(Context context, zzafg zzafgVar, zzuq zzuqVar, zzxy zzxyVar, zznb zznbVar, zzaka zzakaVar) {
        super(context, zzafgVar, zzxyVar);
        this.zzsX = zzuqVar;
        this.zzMu = zzafgVar.zzXN;
        this.zzsK = zznbVar;
        this.zzJH = zzakaVar;
    }

    private static String zzi(List<zzuh> list) {
        int i;
        if (list == null) {
            return "".toString();
        }
        String string = "";
        for (zzuh zzuhVar : list) {
            if (zzuhVar != null && zzuhVar.zzMG != null && !TextUtils.isEmpty(zzuhVar.zzMG.zzLK)) {
                String strValueOf = String.valueOf(string);
                String str = zzuhVar.zzMG.zzLK;
                switch (zzuhVar.zzMF) {
                    case -1:
                        i = 4;
                        break;
                    case 0:
                        i = 0;
                        break;
                    case 1:
                        i = 1;
                        break;
                    case 2:
                    default:
                        i = 6;
                        break;
                    case 3:
                        i = 2;
                        break;
                    case 4:
                        i = 3;
                        break;
                    case 5:
                        i = 5;
                        break;
                }
                String strValueOf2 = String.valueOf(new StringBuilder(String.valueOf(str).length() + 33).append(str).append(".").append(i).append(".").append(zzuhVar.zzML).toString());
                string = new StringBuilder(String.valueOf(strValueOf).length() + 1 + String.valueOf(strValueOf2).length()).append(strValueOf).append(strValueOf2).append("_").toString();
            }
        }
        return string.substring(0, Math.max(0, string.length() - 1));
    }

    @Override // com.google.android.gms.internal.zzxt, com.google.android.gms.internal.zzafp
    public void onStop() {
        synchronized (this.zzQT) {
            super.onStop();
            if (this.zzRc != null) {
                this.zzRc.cancel();
            }
        }
    }

    @Override // com.google.android.gms.internal.zzxt
    protected final void zzd(long j) throws zzxw {
        Bundle bundle;
        synchronized (this.zzQT) {
            this.zzRc = this.zzMu.zzMj != -1 ? new zzuk(this.mContext, this.zzQQ.zzUj, this.zzsX, this.zzMu, this.zzQR.zzAv, this.zzQR.zzAx, this.zzQR.zzTI, j, ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEL)).longValue(), 2) : new zzun(this.mContext, this.zzQQ.zzUj, this.zzsX, this.zzMu, this.zzQR.zzAv, this.zzQR.zzAx, this.zzQR.zzTI, j, ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEL)).longValue(), this.zzsK);
        }
        ArrayList arrayList = new ArrayList(this.zzMu.zzLY);
        boolean z = false;
        Bundle bundle2 = this.zzQQ.zzUj.zzSz.zzzX;
        if (bundle2 != null && (bundle = bundle2.getBundle("com.google.ads.mediation.admob.AdMobAdapter")) != null) {
            z = bundle.getBoolean("_skipMediation");
        }
        if (z) {
            ListIterator listIterator = arrayList.listIterator();
            while (listIterator.hasNext()) {
                if (!((zzua) listIterator.next()).zzLJ.contains("com.google.ads.mediation.admob.AdMobAdapter")) {
                    listIterator.remove();
                }
            }
        }
        this.zzRd = this.zzRc.zzf(arrayList);
        switch (this.zzRd.zzMF) {
            case 0:
                if (this.zzRd.zzMG == null || this.zzRd.zzMG.zzLS == null) {
                    return;
                }
                CountDownLatch countDownLatch = new CountDownLatch(1);
                zzagz.zzZr.post(new zzye(this, countDownLatch));
                try {
                    countDownLatch.await(10L, TimeUnit.SECONDS);
                    synchronized (this.zzQT) {
                        if (!this.zzRe) {
                            throw new zzxw("View could not be prepared", 0);
                        }
                        if (this.zzJH.isDestroyed()) {
                            throw new zzxw("Assets not loaded, web view is destroyed", 0);
                        }
                    }
                    return;
                } catch (InterruptedException e) {
                    String strValueOf = String.valueOf(e);
                    throw new zzxw(new StringBuilder(String.valueOf(strValueOf).length() + 38).append("Interrupted while waiting for latch : ").append(strValueOf).toString(), 0);
                }
            case 1:
                throw new zzxw("No fill from any mediation ad networks.", 3);
            default:
                throw new zzxw(new StringBuilder(40).append("Unexpected mediation result: ").append(this.zzRd.zzMF).toString(), 0);
        }
    }

    @Override // com.google.android.gms.internal.zzxt
    protected final zzaff zzs(int i) {
        zzaae zzaaeVar = this.zzQQ.zzUj;
        return new zzaff(zzaaeVar.zzSz, this.zzJH, this.zzQR.zzMa, i, this.zzQR.zzMb, this.zzQR.zzTq, this.zzQR.orientation, this.zzQR.zzMg, zzaaeVar.zzSC, this.zzQR.zzTo, this.zzRd != null ? this.zzRd.zzMG : null, this.zzRd != null ? this.zzRd.zzMH : null, this.zzRd != null ? this.zzRd.zzMI : AdMobAdapter.class.getName(), this.zzMu, this.zzRd != null ? this.zzRd.zzMJ : null, this.zzQR.zzTp, this.zzQQ.zzvX, this.zzQR.zzTn, this.zzQQ.zzXR, this.zzQR.zzTs, this.zzQR.zzTt, this.zzQQ.zzXL, null, this.zzQR.zzTD, this.zzQR.zzTE, this.zzQR.zzTF, this.zzMu != null ? this.zzMu.zzMl : false, this.zzQR.zzTH, this.zzRc != null ? zzi(this.zzRc.zzfg()) : null, this.zzQR.zzMd, this.zzQR.zzTK, this.zzQQ.zzXX);
    }
}
