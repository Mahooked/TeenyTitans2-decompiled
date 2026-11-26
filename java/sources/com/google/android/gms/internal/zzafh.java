package com.google.android.gms.internal;

import android.os.Bundle;
import android.os.Parcelable;
import android.os.SystemClock;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.LinkedList;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzafh {
    private final Object mLock;
    private boolean zzVa;
    private final LinkedList<zzafi> zzXZ;
    private final String zzYa;
    private final String zzYb;
    private long zzYc;
    private long zzYd;
    private long zzYe;
    private long zzYf;
    private long zzYg;
    private long zzYh;
    private final zzafk zzvt;

    private zzafh(zzafk zzafkVar, String str, String str2) {
        this.mLock = new Object();
        this.zzYc = -1L;
        this.zzYd = -1L;
        this.zzVa = false;
        this.zzYe = -1L;
        this.zzYf = 0L;
        this.zzYg = -1L;
        this.zzYh = -1L;
        this.zzvt = zzafkVar;
        this.zzYa = str;
        this.zzYb = str2;
        this.zzXZ = new LinkedList<>();
    }

    public zzafh(String str, String str2) {
        this(com.google.android.gms.ads.internal.zzbs.zzbD(), str, str2);
    }

    public final Bundle toBundle() {
        Bundle bundle;
        synchronized (this.mLock) {
            bundle = new Bundle();
            bundle.putString("seq_num", this.zzYa);
            bundle.putString("slotid", this.zzYb);
            bundle.putBoolean("ismediation", this.zzVa);
            bundle.putLong("treq", this.zzYg);
            bundle.putLong("tresponse", this.zzYh);
            bundle.putLong("timp", this.zzYd);
            bundle.putLong("tload", this.zzYe);
            bundle.putLong("pcc", this.zzYf);
            bundle.putLong("tfetch", this.zzYc);
            ArrayList<? extends Parcelable> arrayList = new ArrayList<>();
            Iterator<zzafi> it = this.zzXZ.iterator();
            while (it.hasNext()) {
                arrayList.add(it.next().toBundle());
            }
            bundle.putParcelableArrayList("tclick", arrayList);
        }
        return bundle;
    }

    public final void zzh(long j) {
        synchronized (this.mLock) {
            this.zzYh = j;
            if (this.zzYh != -1) {
                this.zzvt.zza(this);
            }
        }
    }

    public final void zzhc() {
        synchronized (this.mLock) {
            if (this.zzYh != -1 && this.zzYd == -1) {
                this.zzYd = SystemClock.elapsedRealtime();
                this.zzvt.zza(this);
            }
            this.zzvt.zzhq().zzhc();
        }
    }

    public final void zzhd() {
        synchronized (this.mLock) {
            if (this.zzYh != -1) {
                zzafi zzafiVar = new zzafi();
                zzafiVar.zzhh();
                this.zzXZ.add(zzafiVar);
                this.zzYf++;
                this.zzvt.zzhq().zzhd();
                this.zzvt.zza(this);
            }
        }
    }

    public final void zzhe() {
        synchronized (this.mLock) {
            if (this.zzYh != -1 && !this.zzXZ.isEmpty()) {
                zzafi last = this.zzXZ.getLast();
                if (last.zzhf() == -1) {
                    last.zzhg();
                    this.zzvt.zza(this);
                }
            }
        }
    }

    public final void zzi(long j) {
        synchronized (this.mLock) {
            if (this.zzYh != -1) {
                this.zzYc = j;
                this.zzvt.zza(this);
            }
        }
    }

    public final void zzo(zzir zzirVar) {
        synchronized (this.mLock) {
            this.zzYg = SystemClock.elapsedRealtime();
            this.zzvt.zzhq().zzb(zzirVar, this.zzYg);
        }
    }

    public final void zzv(boolean z) {
        synchronized (this.mLock) {
            if (this.zzYh != -1) {
                this.zzYe = SystemClock.elapsedRealtime();
                if (!z) {
                    this.zzYd = this.zzYe;
                    this.zzvt.zza(this);
                }
            }
        }
    }

    public final void zzw(boolean z) {
        synchronized (this.mLock) {
            if (this.zzYh != -1) {
                this.zzVa = z;
                this.zzvt.zza(this);
            }
        }
    }
}
