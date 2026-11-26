package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;
import com.comscore.streaming.Constants;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzuk implements zztz {
    private final Context mContext;
    private final long mStartTime;
    private final zzaae zzMM;
    private final long zzMN;
    private final String zzMR;
    private final zzub zzMu;
    private final boolean zzMy;
    private final zzuq zzsX;
    private final boolean zzwJ;
    private final Object mLock = new Object();
    private boolean zzMP = false;
    private final Map<zzajm<zzuh>, zzue> zzMQ = new HashMap();
    private List<zzuh> zzMS = new ArrayList();
    private final int zzMO = 2;

    public zzuk(Context context, zzaae zzaaeVar, zzuq zzuqVar, zzub zzubVar, boolean z, boolean z2, String str, long j, long j2, int i) {
        this.mContext = context;
        this.zzMM = zzaaeVar;
        this.zzsX = zzuqVar;
        this.zzMu = zzubVar;
        this.zzwJ = z;
        this.zzMy = z2;
        this.zzMR = str;
        this.mStartTime = j;
        this.zzMN = j2;
    }

    private final void zza(zzajm<zzuh> zzajmVar) {
        zzagz.zzZr.post(new zzum(this, zzajmVar));
    }

    private final zzuh zzg(List<zzajm<zzuh>> list) {
        synchronized (this.mLock) {
            if (this.zzMP) {
                return new zzuh(-1);
            }
            for (zzajm<zzuh> zzajmVar : list) {
                try {
                    zzuh zzuhVar = zzajmVar.get();
                    this.zzMS.add(zzuhVar);
                    if (zzuhVar != null && zzuhVar.zzMF == 0) {
                        zza(zzajmVar);
                        return zzuhVar;
                    }
                } catch (InterruptedException | ExecutionException e) {
                    zzafr.zzc("Exception while processing an adapter; continuing with other adapters", e);
                }
            }
            zza((zzajm<zzuh>) null);
            return new zzuh(1);
        }
    }

    /* JADX WARN: Code duplicated, block: B:53:0x00cb  */
    private final zzuh zzh(List<zzajm<zzuh>> list) {
        zzuh zzuhVar;
        zzuh zzuhVar2;
        zzajm<zzuh> zzajmVar;
        int iZzfo;
        zzuz zzuzVar;
        synchronized (this.mLock) {
            if (this.zzMP) {
                return new zzuh(-1);
            }
            int i = -1;
            zzajm<zzuh> zzajmVar2 = null;
            zzuh zzuhVar3 = null;
            long j = this.zzMu.zzMk != -1 ? this.zzMu.zzMk : Constants.HEARTBEAT_STAGE_ONE_INTERVAL;
            long jMax = j;
            for (zzajm<zzuh> zzajmVar3 : list) {
                long jCurrentTimeMillis = com.google.android.gms.ads.internal.zzbs.zzbF().currentTimeMillis();
                if (jMax == 0) {
                    try {
                        try {
                            if (zzajmVar3.isDone()) {
                                zzuhVar = zzajmVar3.get();
                            }
                            this.zzMS.add(zzuhVar);
                            if (zzuhVar != null || zzuhVar.zzMF != 0 || (zzuzVar = zzuhVar.zzMK) == null || zzuzVar.zzfo() <= i) {
                                zzuhVar2 = zzuhVar3;
                                zzajmVar = zzajmVar2;
                                iZzfo = i;
                            } else {
                                iZzfo = zzuzVar.zzfo();
                                zzuh zzuhVar4 = zzuhVar;
                                zzajmVar = zzajmVar3;
                                zzuhVar2 = zzuhVar4;
                            }
                            jMax = Math.max(jMax - (com.google.android.gms.ads.internal.zzbs.zzbF().currentTimeMillis() - jCurrentTimeMillis), 0L);
                            zzajmVar2 = zzajmVar;
                            i = iZzfo;
                            zzuhVar3 = zzuhVar2;
                        } catch (Throwable th) {
                            Math.max(jMax - (com.google.android.gms.ads.internal.zzbs.zzbF().currentTimeMillis() - jCurrentTimeMillis), 0L);
                            throw th;
                        }
                    } catch (RemoteException | InterruptedException | ExecutionException | TimeoutException e) {
                        zzafr.zzc("Exception while processing an adapter; continuing with other adapters", e);
                        jMax = Math.max(jMax - (com.google.android.gms.ads.internal.zzbs.zzbF().currentTimeMillis() - jCurrentTimeMillis), 0L);
                    }
                }
                zzuhVar = zzajmVar3.get(jMax, TimeUnit.MILLISECONDS);
                this.zzMS.add(zzuhVar);
                if (zzuhVar != null) {
                    zzuhVar2 = zzuhVar3;
                    zzajmVar = zzajmVar2;
                    iZzfo = i;
                } else {
                    zzuhVar2 = zzuhVar3;
                    zzajmVar = zzajmVar2;
                    iZzfo = i;
                }
                jMax = Math.max(jMax - (com.google.android.gms.ads.internal.zzbs.zzbF().currentTimeMillis() - jCurrentTimeMillis), 0L);
                zzajmVar2 = zzajmVar;
                i = iZzfo;
                zzuhVar3 = zzuhVar2;
            }
            zza(zzajmVar2);
            return zzuhVar3 == null ? new zzuh(1) : zzuhVar3;
        }
    }

    @Override // com.google.android.gms.internal.zztz
    public final void cancel() {
        synchronized (this.mLock) {
            this.zzMP = true;
            Iterator<zzue> it = this.zzMQ.values().iterator();
            while (it.hasNext()) {
                it.next().cancel();
            }
        }
    }

    /* JADX WARN: Code duplicated, block: B:29:0x00f5 A[EDGE_INSN: B:29:0x00f5->B:12:0x003e BREAK  A[LOOP:0: B:7:0x0032->B:22:0x00d2]] */
    @Override // com.google.android.gms.internal.zztz
    public final zzuh zzf(List<zzua> list) {
        zziv zzivVar;
        zzafr.zzaC("Starting mediation.");
        ExecutorService executorServiceNewCachedThreadPool = Executors.newCachedThreadPool();
        ArrayList arrayList = new ArrayList();
        zziv zzivVar2 = this.zzMM.zzvX;
        int[] iArr = new int[2];
        if (zzivVar2.zzAu == null) {
            zzivVar = zzivVar2;
            break;
        }
        com.google.android.gms.ads.internal.zzbs.zzbS();
        if (!zzuj.zza(this.zzMR, iArr)) {
            zzivVar = zzivVar2;
            break;
        }
        int i = iArr[0];
        int i2 = iArr[1];
        zziv[] zzivVarArr = zzivVar2.zzAu;
        int length = zzivVarArr.length;
        int i3 = 0;
        while (true) {
            if (i3 >= length) {
                zzivVar = zzivVar2;
                break;
            }
            zzivVar = zzivVarArr[i3];
            if (i == zzivVar.width && i2 == zzivVar.height) {
                break;
            }
            i3++;
        }
        for (zzua zzuaVar : list) {
            String strValueOf = String.valueOf(zzuaVar.zzLI);
            zzafr.zzaS(strValueOf.length() != 0 ? "Trying mediation network: ".concat(strValueOf) : new String("Trying mediation network: "));
            Iterator<String> it = zzuaVar.zzLJ.iterator();
            while (it.hasNext()) {
                zzue zzueVar = new zzue(this.mContext, it.next(), this.zzsX, this.zzMu, zzuaVar, this.zzMM.zzSz, zzivVar, this.zzMM.zzvT, this.zzwJ, this.zzMy, this.zzMM.zzwj, this.zzMM.zzwq, this.zzMM.zzSO, this.zzMM.zzTj);
                zzajm<zzuh> zzajmVarZza = zzagt.zza(executorServiceNewCachedThreadPool, new zzul(this, zzueVar));
                this.zzMQ.put(zzajmVarZza, zzueVar);
                arrayList.add(zzajmVarZza);
            }
        }
        switch (this.zzMO) {
            case 2:
                return zzh(arrayList);
            default:
                return zzg(arrayList);
        }
    }

    @Override // com.google.android.gms.internal.zztz
    public final List<zzuh> zzfg() {
        return this.zzMS;
    }
}
