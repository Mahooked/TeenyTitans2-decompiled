package com.google.android.gms.internal;

import java.util.List;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.atomic.AtomicInteger;

/* JADX INFO: loaded from: classes.dex */
final class zzajk implements Runnable {
    private /* synthetic */ AtomicInteger zzaaY;
    private /* synthetic */ int zzaaZ;
    private /* synthetic */ zzajg zzaba;
    private /* synthetic */ List zzabb;

    zzajk(AtomicInteger atomicInteger, int i, zzajg zzajgVar, List list) {
        this.zzaaY = atomicInteger;
        this.zzaaZ = i;
        this.zzaba = zzajgVar;
        this.zzabb = list;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzaaY.incrementAndGet() >= this.zzaaZ) {
            try {
                this.zzaba.zzg(zzaji.zzq(this.zzabb));
            } catch (InterruptedException | ExecutionException e) {
                zzafr.zzc("Unable to convert list of futures to a future of list", e);
            }
        }
    }
}
