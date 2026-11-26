package com.google.android.gms.internal;

import android.os.Build;
import android.os.ConditionVariable;
import java.io.IOException;
import java.util.Random;
import java.util.concurrent.ThreadLocalRandom;

/* JADX INFO: loaded from: classes.dex */
public class zzcn {
    private static final ConditionVariable zzpK = new ConditionVariable();
    protected static volatile zzazn zzpL = null;
    private static volatile Random zzpN = null;
    private zzdb zzpJ;
    protected volatile Boolean zzpM;

    public zzcn(zzdb zzdbVar) {
        this.zzpJ = zzdbVar;
        zzdbVar.zzC().execute(new zzco(this));
    }

    public static int zzy() {
        try {
            return Build.VERSION.SDK_INT >= 21 ? ThreadLocalRandom.current().nextInt() : zzz().nextInt();
        } catch (RuntimeException e) {
            return zzz().nextInt();
        }
    }

    private static Random zzz() {
        if (zzpN == null) {
            synchronized (zzcn.class) {
                if (zzpN == null) {
                    zzpN = new Random();
                }
            }
        }
        return zzpN;
    }

    public final void zza(int i, int i2, long j) throws IOException {
        try {
            zzpK.block();
            if (this.zzpM.booleanValue() && zzpL != null && this.zzpJ.zzqT) {
                zzat zzatVar = new zzat();
                zzatVar.zzaH = this.zzpJ.zzqD.getPackageName();
                zzatVar.zzaI = Long.valueOf(j);
                zzazp zzazpVarZze = zzpL.zze(adp.zzc(zzatVar));
                zzazpVarZze.zzaj(i2);
                zzazpVarZze.zzai(i);
                this.zzpJ.zzG();
                zzazpVarZze.zzoT();
            }
        } catch (Exception e) {
        }
    }
}
