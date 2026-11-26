package com.google.android.gms.internal;

import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.PendingResults;
import com.google.android.gms.common.api.Status;
import java.util.ArrayList;
import java.util.TimeZone;

/* JADX INFO: loaded from: classes.dex */
public final class zzazp {
    private boolean zzazA;
    private final aej zzazB;
    private boolean zzazC;
    private /* synthetic */ zzazn zzazD;
    private String zzazk;
    private int zzazl;
    private String zzazm;
    private String zzazn;
    private int zzazp;
    private final zzazr zzazu;
    private ArrayList<Integer> zzazv;
    private ArrayList<String> zzazw;
    private ArrayList<Integer> zzazx;
    private ArrayList<zzcqn> zzazy;
    private ArrayList<byte[]> zzazz;

    private zzazp(zzazn zzaznVar, byte[] bArr) {
        this(zzaznVar, bArr, (zzazr) null);
    }

    /* synthetic */ zzazp(zzazn zzaznVar, byte[] bArr, zzazo zzazoVar) {
        this(zzaznVar, bArr);
    }

    private zzazp(zzazn zzaznVar, byte[] bArr, zzazr zzazrVar) {
        this.zzazD = zzaznVar;
        this.zzazl = this.zzazD.zzazl;
        this.zzazk = this.zzazD.zzazk;
        this.zzazm = this.zzazD.zzazm;
        zzazn zzaznVar2 = this.zzazD;
        this.zzazn = null;
        this.zzazp = 0;
        this.zzazv = null;
        this.zzazw = null;
        this.zzazx = null;
        this.zzazy = null;
        this.zzazz = null;
        this.zzazA = true;
        this.zzazB = new aej();
        this.zzazC = false;
        this.zzazm = zzaznVar.zzazm;
        this.zzazn = null;
        this.zzazB.zzctQ = zzaznVar.zzazr.currentTimeMillis();
        this.zzazB.zzctR = zzaznVar.zzazr.elapsedRealtime();
        aej aejVar = this.zzazB;
        zzazs unused = zzaznVar.zzazs;
        aejVar.zzcuc = TimeZone.getDefault().getOffset(this.zzazB.zzctQ) / 1000;
        if (bArr != null) {
            this.zzazB.zzctX = bArr;
        }
        this.zzazu = null;
    }

    public final zzazp zzai(int i) {
        this.zzazB.zzctT = i;
        return this;
    }

    public final zzazp zzaj(int i) {
        this.zzazB.zzrB = i;
        return this;
    }

    public final void zzoS() {
        zzoT();
    }

    @Deprecated
    public final PendingResult<Status> zzoT() {
        if (this.zzazC) {
            throw new IllegalStateException("do not reuse LogEventBuilder");
        }
        this.zzazC = true;
        zzazu zzazuVar = new zzazu(new zzbak(this.zzazD.packageName, this.zzazD.zzazj, this.zzazl, this.zzazk, this.zzazm, this.zzazn, this.zzazD.zzazo, 0), this.zzazB, null, null, zzazn.zzb((ArrayList<Integer>) null), null, zzazn.zzb((ArrayList<Integer>) null), null, null, this.zzazA);
        zzbak zzbakVar = zzazuVar.zzazE;
        return this.zzazD.zzazt.zzg(zzbakVar.zzazk, zzbakVar.zzazl) ? this.zzazD.zzazq.zza(zzazuVar) : PendingResults.immediatePendingResult(Status.zzaBm);
    }
}
