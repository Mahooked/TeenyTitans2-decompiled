package com.amazon.android.framework.task.pipeline;

import android.os.Handler;
import android.os.HandlerThread;
import android.os.Looper;
import android.os.SystemClock;
import com.amazon.android.framework.task.Task;
import com.amazon.android.framework.util.KiwiLogger;
import java.util.Collections;
import java.util.Date;
import java.util.HashSet;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
public final class a implements f {
    private static final KiwiLogger a = new KiwiLogger("SimpleTaskPipeline");
    private final Handler b;
    private final Set c;
    private final String d;

    private a(HandlerThread handlerThread) {
        this.c = Collections.synchronizedSet(new HashSet());
        this.d = handlerThread.getName();
        handlerThread.start();
        this.b = new Handler(handlerThread.getLooper());
    }

    private a(String str) {
        this.c = Collections.synchronizedSet(new HashSet());
        this.d = str;
        this.b = new Handler();
    }

    public static a a(String str) {
        return new a(new HandlerThread("KIWI_" + str));
    }

    public static a b(String str) {
        return new a(str);
    }

    private Runnable c(Task task) {
        d dVar = new d(this, task);
        this.c.add(dVar);
        return dVar;
    }

    @Override // com.amazon.android.framework.task.pipeline.f
    public final void a() {
        for (Runnable runnable : this.c) {
            if (KiwiLogger.TRACE_ON) {
                a.trace(this.d + ": Removing callback: " + runnable);
            }
            this.b.removeCallbacks(runnable);
        }
        this.c.clear();
        if (this.b.getLooper() == Looper.getMainLooper() || !this.b.getLooper().getThread().isAlive()) {
            return;
        }
        a.trace("Interrupting looper thread!");
        this.b.getLooper().getThread().interrupt();
        a.trace("Quitting looper: " + this.b.getLooper().getThread() + ", " + this.b.getLooper().getThread().isAlive());
        this.b.getLooper().quit();
    }

    @Override // com.amazon.android.framework.task.pipeline.f
    public final void a(Task task) {
        if (KiwiLogger.TRACE_ON) {
            a.trace("Scheduling task: " + task);
        }
        this.b.post(c(task));
    }

    @Override // com.amazon.android.framework.task.pipeline.f
    public final void a(Task task, long j) {
        if (KiwiLogger.TRACE_ON) {
            a.trace(this.d + ": Scheduling task: " + task + ", with delay: " + j);
        }
        this.b.postDelayed(c(task), j);
    }

    @Override // com.amazon.android.framework.task.pipeline.f
    public final void a(Task task, Date date) {
        long jUptimeMillis = SystemClock.uptimeMillis() + (date.getTime() - System.currentTimeMillis());
        if (KiwiLogger.TRACE_ON) {
            a.trace(this.d + ": Scheduling task: " + task + ", at time: " + date + ", System uptimeMillis: " + System.currentTimeMillis() + ", uptimeMillis: " + jUptimeMillis);
        }
        this.b.postAtTime(c(task), jUptimeMillis);
    }

    @Override // com.amazon.android.framework.task.pipeline.f
    public final void b(Task task) {
        if (KiwiLogger.TRACE_ON) {
            a.trace(this.d + ": Scheduling task immediately: " + task);
        }
        this.b.postAtFrontOfQueue(c(task));
    }
}
