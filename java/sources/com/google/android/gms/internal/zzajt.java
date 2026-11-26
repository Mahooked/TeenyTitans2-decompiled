package com.google.android.gms.internal;

import java.util.Iterator;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public class zzajt<T> implements zzajp<T> {
    private final Object mLock = new Object();
    private int zzLe = 0;
    private BlockingQueue<zzaju> zzabg = new LinkedBlockingQueue();
    private T zzabh;

    public int getStatus() {
        return this.zzLe;
    }

    public void reject() {
        synchronized (this.mLock) {
            if (this.zzLe != 0) {
                throw new UnsupportedOperationException();
            }
            this.zzLe = -1;
            Iterator it = this.zzabg.iterator();
            while (it.hasNext()) {
                ((zzaju) it.next()).zzabj.run();
            }
            this.zzabg.clear();
        }
    }

    @Override // com.google.android.gms.internal.zzajp
    public void zza(zzajs<T> zzajsVar, zzajq zzajqVar) {
        synchronized (this.mLock) {
            if (this.zzLe == 1) {
                zzajsVar.zzc(this.zzabh);
            } else if (this.zzLe == -1) {
                zzajqVar.run();
            } else if (this.zzLe == 0) {
                this.zzabg.add(new zzaju(this, zzajsVar, zzajqVar));
            }
        }
    }

    @Override // com.google.android.gms.internal.zzajp
    public void zzf(T t) {
        synchronized (this.mLock) {
            if (this.zzLe != 0) {
                throw new UnsupportedOperationException();
            }
            this.zzabh = t;
            this.zzLe = 1;
            Iterator it = this.zzabg.iterator();
            while (it.hasNext()) {
                ((zzaju) it.next()).zzabi.zzc(t);
            }
            this.zzabg.clear();
        }
    }
}
