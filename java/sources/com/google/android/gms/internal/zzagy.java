package com.google.android.gms.internal;

import java.util.concurrent.ThreadFactory;
import java.util.concurrent.atomic.AtomicInteger;

/* JADX INFO: loaded from: classes.dex */
final class zzagy implements ThreadFactory {
    private final AtomicInteger zzXJ = new AtomicInteger(1);
    private /* synthetic */ String zzZq;

    zzagy(String str) {
        this.zzZq = str;
    }

    @Override // java.util.concurrent.ThreadFactory
    public final Thread newThread(Runnable runnable) {
        String str = this.zzZq;
        return new Thread(runnable, new StringBuilder(String.valueOf(str).length() + 23).append("AdWorker(").append(str).append(") #").append(this.zzXJ.getAndIncrement()).toString());
    }
}
