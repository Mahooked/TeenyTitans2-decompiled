package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public abstract class zzzy implements zzahp<Void>, zzzw {
    private final Object mLock = new Object();
    private final zzajp<zzaae> zzSs;
    private final zzzw zzSt;

    public zzzy(zzajp<zzaae> zzajpVar, zzzw zzzwVar) {
        this.zzSs = zzajpVar;
        this.zzSt = zzzwVar;
    }

    @Override // com.google.android.gms.internal.zzahp
    public final void cancel() {
        zzgA();
    }

    @Override // com.google.android.gms.internal.zzzw
    public final void zza(zzaai zzaaiVar) {
        synchronized (this.mLock) {
            this.zzSt.zza(zzaaiVar);
            zzgA();
        }
    }

    final boolean zza(zzaam zzaamVar, zzaae zzaaeVar) {
        try {
            zzaamVar.zza(zzaaeVar, new zzaah(this));
            return true;
        } catch (Throwable th) {
            zzafr.zzc("Could not fetch ad response from ad request service due to an Exception.", th);
            com.google.android.gms.ads.internal.zzbs.zzbD().zza(th, "AdRequestClientTask.getAdResponseFromService");
            this.zzSt.zza(new zzaai(0));
            return false;
        }
    }

    public abstract void zzgA();

    public abstract zzaam zzgB();

    @Override // com.google.android.gms.internal.zzahp
    public final /* synthetic */ Void zzgp() {
        zzaam zzaamVarZzgB = zzgB();
        if (zzaamVarZzgB == null) {
            this.zzSt.zza(new zzaai(0));
            zzgA();
        } else {
            this.zzSs.zza(new zzzz(this, zzaamVarZzgB), new zzaaa(this));
        }
        return null;
    }
}
