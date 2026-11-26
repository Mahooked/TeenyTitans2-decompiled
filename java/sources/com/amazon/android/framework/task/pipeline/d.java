package com.amazon.android.framework.task.pipeline;

import android.os.SystemClock;
import com.amazon.android.framework.task.Task;
import com.amazon.android.framework.util.KiwiLogger;
import java.util.Date;

/* JADX INFO: loaded from: classes.dex */
final class d implements Runnable {
    private /* synthetic */ Task a;
    private /* synthetic */ a b;

    d(a aVar, Task task) {
        this.b = aVar;
        this.a = task;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.b.c.remove(this);
        try {
            if (KiwiLogger.TRACE_ON) {
                a.a.trace(this.b.d + ": Executing Task: " + this.a + ", current time: " + new Date() + ", uptime: " + SystemClock.uptimeMillis());
            }
            this.a.execute();
        } catch (Throwable th) {
            if (KiwiLogger.ERROR_ON) {
                a.a.error("Task Failed with unhandled exception: " + th, th);
            }
        } finally {
            if (KiwiLogger.TRACE_ON) {
                a.a.trace(this.b.d + ": Task finished executing: " + this.a);
            }
        }
    }

    public final String toString() {
        return this.a.toString();
    }
}
