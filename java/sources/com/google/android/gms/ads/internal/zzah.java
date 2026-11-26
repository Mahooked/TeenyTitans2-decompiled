package com.google.android.gms.ads.internal;

import android.content.Context;
import android.view.MotionEvent;
import android.view.View;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagt;
import com.google.android.gms.internal.zzaiy;
import com.google.android.gms.internal.zzcp;
import com.google.android.gms.internal.zzct;
import com.google.android.gms.internal.zzji;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zzzn;
import java.util.List;
import java.util.Vector;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.atomic.AtomicReference;

/* JADX INFO: loaded from: classes.dex */
@zzzn
final class zzah implements zzcp, Runnable {
    private zzbt zzsP;
    private final List<Object[]> zztH = new Vector();
    private final AtomicReference<zzcp> zztI = new AtomicReference<>();
    private CountDownLatch zztJ = new CountDownLatch(1);

    public zzah(zzbt zzbtVar) {
        this.zzsP = zzbtVar;
        zzji.zzds();
        if (zzaiy.zzil()) {
            zzagt.zza(this);
        } else {
            run();
        }
    }

    private final boolean zzaU() {
        try {
            this.zztJ.await();
            return true;
        } catch (InterruptedException e) {
            zzafr.zzc("Interrupted during GADSignals creation.", e);
            return false;
        }
    }

    private final void zzaV() {
        if (this.zztH.isEmpty()) {
            return;
        }
        for (Object[] objArr : this.zztH) {
            if (objArr.length == 1) {
                this.zztI.get().zza((MotionEvent) objArr[0]);
            } else if (objArr.length == 3) {
                this.zztI.get().zza(((Integer) objArr[0]).intValue(), ((Integer) objArr[1]).intValue(), ((Integer) objArr[2]).intValue());
            }
        }
        this.zztH.clear();
    }

    private static Context zze(Context context) {
        Context applicationContext;
        return (((Boolean) zzbs.zzbL().zzd(zzmo.zzCi)).booleanValue() && (applicationContext = context.getApplicationContext()) != null) ? applicationContext : context;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            this.zztI.set(zzct.zza(this.zzsP.zzvT.zzaP, zze(this.zzsP.zzqD), !((Boolean) zzbs.zzbL().zzd(zzmo.zzDO)).booleanValue() && (this.zzsP.zzvT.zzaaQ)));
        } finally {
            this.zztJ.countDown();
            this.zzsP = null;
        }
    }

    @Override // com.google.android.gms.internal.zzcp
    public final String zza(Context context) {
        zzcp zzcpVar;
        if (!zzaU() || (zzcpVar = this.zztI.get()) == null) {
            return "";
        }
        zzaV();
        return zzcpVar.zza(zze(context));
    }

    @Override // com.google.android.gms.internal.zzcp
    public final String zza(Context context, String str, View view) {
        zzcp zzcpVar;
        if (!zzaU() || (zzcpVar = this.zztI.get()) == null) {
            return "";
        }
        zzaV();
        return zzcpVar.zza(zze(context), str, view);
    }

    @Override // com.google.android.gms.internal.zzcp
    public final void zza(int i, int i2, int i3) {
        zzcp zzcpVar = this.zztI.get();
        if (zzcpVar == null) {
            this.zztH.add(new Object[]{Integer.valueOf(i), Integer.valueOf(i2), Integer.valueOf(i3)});
        } else {
            zzaV();
            zzcpVar.zza(i, i2, i3);
        }
    }

    @Override // com.google.android.gms.internal.zzcp
    public final void zza(MotionEvent motionEvent) {
        zzcp zzcpVar = this.zztI.get();
        if (zzcpVar == null) {
            this.zztH.add(new Object[]{motionEvent});
        } else {
            zzaV();
            zzcpVar.zza(motionEvent);
        }
    }
}
