package com.google.android.gms.internal;

import java.util.Iterator;

/* JADX INFO: loaded from: classes.dex */
final class zzgx implements Runnable {
    private /* synthetic */ zzgw zzyw;

    zzgx(zzgw zzgwVar) {
        this.zzyw = zzgwVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        synchronized (this.zzyw.mLock) {
            if (this.zzyw.zzyq && this.zzyw.zzyr) {
                zzgw.zza(this.zzyw, false);
                zzafr.zzaC("App went background");
                Iterator it = this.zzyw.zzys.iterator();
                while (it.hasNext()) {
                    try {
                        ((zzgy) it.next()).zzf(false);
                    } catch (Exception e) {
                        zzafr.zzb("OnForegroundStateChangedListener threw exception.", e);
                    }
                }
            } else {
                zzafr.zzaC("App is still foreground");
            }
        }
    }
}
