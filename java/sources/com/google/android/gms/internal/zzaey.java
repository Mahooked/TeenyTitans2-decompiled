package com.google.android.gms.internal;

import java.util.concurrent.ThreadFactory;
import java.util.concurrent.atomic.AtomicInteger;

/* JADX INFO: loaded from: classes.dex */
final class zzaey implements ThreadFactory {
    private final AtomicInteger zzXJ = new AtomicInteger(1);

    zzaey(zzaew zzaewVar) {
    }

    @Override // java.util.concurrent.ThreadFactory
    public final Thread newThread(Runnable runnable) {
        return new Thread(runnable, new StringBuilder(42).append("AdWorker(SCION_TASK_EXECUTOR) #").append(this.zzXJ.getAndIncrement()).toString());
    }
}
