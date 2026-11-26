package com.amazon.android.e;

import android.content.Intent;
import android.support.v4.internal.view.SupportMenu;
import com.amazon.android.framework.context.ContextManager;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.task.TaskManager;
import com.amazon.android.framework.task.pipeline.TaskPipelineId;
import com.amazon.android.framework.util.KiwiLogger;
import com.amazon.android.m.g;
import java.util.Random;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.atomic.AtomicReference;

/* JADX INFO: loaded from: classes.dex */
public final class c implements b, com.amazon.android.framework.resource.b {
    private static final KiwiLogger a = new KiwiLogger("ActivityResultManagerImpl");
    private static final Random b = new Random();

    @Resource
    private TaskManager c;

    @Resource
    private ContextManager d;

    @Resource
    private g e;
    private AtomicReference f = new AtomicReference();
    private BlockingQueue g = new LinkedBlockingQueue();

    @Override // com.amazon.android.e.b
    public final f a(Intent intent) {
        a aVar = new a(intent, b.nextInt(SupportMenu.USER_MASK) + 1);
        if (!this.f.compareAndSet(null, aVar)) {
            a.error("StartActivityForResult called while ActivityResultManager is already awaiting a result");
            return null;
        }
        a.trace("Starting activity for result: " + intent + ", " + intent.getFlags() + ", requestId: " + aVar.a);
        this.c.enqueueAtFront(TaskPipelineId.FOREGROUND, new d(this, aVar));
        try {
            a.trace("Blocking for request: " + aVar.a);
            return (f) this.g.take();
        } catch (InterruptedException e) {
            a.trace("Interrupted while awaiting for request, returning null");
            return null;
        } finally {
            a.trace("Received Response: " + aVar.a);
            this.f.set(null);
        }
    }

    @Override // com.amazon.android.e.b
    public final boolean a(f fVar) {
        if (KiwiLogger.TRACE_ON) {
            a.trace("Recieved ActivityResult: " + fVar);
        }
        a aVar = (a) this.f.get();
        if (aVar == null) {
            if (KiwiLogger.TRACE_ON) {
                a.trace("We don't have a current open request, returning");
            }
            return false;
        }
        if (aVar.a != fVar.a) {
            if (KiwiLogger.TRACE_ON) {
                a.trace("We don't have a request with code: " + fVar.a + ", returning");
            }
            return false;
        }
        if (KiwiLogger.TRACE_ON) {
            a.trace("Signaling thread waiting for request: " + fVar.a);
        }
        this.g.add(fVar);
        return true;
    }

    @Override // com.amazon.android.framework.resource.b
    public final void onResourcesPopulated() {
        this.e.a(new e(this));
    }
}
