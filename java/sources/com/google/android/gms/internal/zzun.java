package com.google.android.gms.internal;

import android.content.Context;
import android.text.TextUtils;
import java.util.ArrayList;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzun implements zztz {
    private final Context mContext;
    private final long mStartTime;
    private final zzaae zzMM;
    private final long zzMN;
    private final String zzMR;
    private zzue zzMW;
    private final zzub zzMu;
    private final boolean zzMy;
    private final zznb zzsK;
    private final zzuq zzsX;
    private final boolean zzwJ;
    private final Object mLock = new Object();
    private boolean zzMP = false;
    private List<zzuh> zzMS = new ArrayList();

    public zzun(Context context, zzaae zzaaeVar, zzuq zzuqVar, zzub zzubVar, boolean z, boolean z2, String str, long j, long j2, zznb zznbVar) {
        this.mContext = context;
        this.zzMM = zzaaeVar;
        this.zzsX = zzuqVar;
        this.zzMu = zzubVar;
        this.zzwJ = z;
        this.zzMy = z2;
        this.zzMR = str;
        this.mStartTime = j;
        this.zzMN = j2;
        this.zzsK = zznbVar;
    }

    @Override // com.google.android.gms.internal.zztz
    public final void cancel() {
        synchronized (this.mLock) {
            this.zzMP = true;
            if (this.zzMW != null) {
                this.zzMW.cancel();
            }
        }
    }

    /* JADX WARN: Code duplicated, block: B:48:0x0195 A[EDGE_INSN: B:48:0x0195->B:12:0x0042 BREAK  A[LOOP:0: B:7:0x0036->B:28:0x0096]] */
    @Override // com.google.android.gms.internal.zztz
    public final zzuh zzf(List<zzua> list) {
        zziv zzivVar;
        zzafr.zzaC("Starting mediation.");
        ArrayList arrayList = new ArrayList();
        zzmz zzmzVarZzdS = this.zzsK.zzdS();
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
            for (String str : zzuaVar.zzLJ) {
                zzmz zzmzVarZzdS2 = this.zzsK.zzdS();
                synchronized (this.mLock) {
                    if (this.zzMP) {
                        return new zzuh(-1);
                    }
                    this.zzMW = new zzue(this.mContext, str, this.zzsX, this.zzMu, zzuaVar, this.zzMM.zzSz, zzivVar, this.zzMM.zzvT, this.zzwJ, this.zzMy, this.zzMM.zzwj, this.zzMM.zzwq, this.zzMM.zzSO, this.zzMM.zzTj);
                    zzuh zzuhVarZza = this.zzMW.zza(this.mStartTime, this.zzMN);
                    this.zzMS.add(zzuhVarZza);
                    if (zzuhVarZza.zzMF == 0) {
                        zzafr.zzaC("Adapter succeeded.");
                        this.zzsK.zzh("mediation_network_succeed", str);
                        if (!arrayList.isEmpty()) {
                            this.zzsK.zzh("mediation_networks_fail", TextUtils.join(",", arrayList));
                        }
                        this.zzsK.zza(zzmzVarZzdS2, "mls");
                        this.zzsK.zza(zzmzVarZzdS, "ttm");
                        return zzuhVarZza;
                    }
                    arrayList.add(str);
                    this.zzsK.zza(zzmzVarZzdS2, "mlf");
                    if (zzuhVarZza.zzMH != null) {
                        zzagz.zzZr.post(new zzuo(this, zzuhVarZza));
                    }
                }
            }
        }
        if (!arrayList.isEmpty()) {
            this.zzsK.zzh("mediation_networks_fail", TextUtils.join(",", arrayList));
        }
        return new zzuh(1);
    }

    @Override // com.google.android.gms.internal.zztz
    public final List<zzuh> zzfg() {
        return this.zzMS;
    }
}
