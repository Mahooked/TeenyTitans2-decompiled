package com.amazon.android.framework.task.pipeline;

import com.amazon.android.framework.context.ContextManager;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.task.Task;
import com.amazon.android.framework.util.KiwiLogger;
import com.amazon.android.m.g;
import java.util.ArrayList;
import java.util.Date;
import java.util.Iterator;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public final class e implements com.amazon.android.framework.resource.b, f {
    private static final KiwiLogger a = new KiwiLogger("ForegroundTaskPipeline");

    @Resource
    private ContextManager b;

    @Resource
    private g c;
    private f e;
    private List f = new ArrayList();
    private f d = a.b("KIWI_UI");

    public e(f fVar) {
        this.e = fVar;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public void a(Task task, boolean z) {
        if (!this.b.isVisible()) {
            if (KiwiLogger.TRACE_ON) {
                a.trace("No UI visible to execute task: " + task + ", placing into pending queue until task is visible");
            }
            this.f.add(task);
        } else if (z) {
            this.d.b(task);
        } else {
            this.d.a(task);
        }
    }

    static /* synthetic */ void a(e eVar) {
        if (KiwiLogger.TRACE_ON) {
            a.trace("Activity resumed, scheduling tasks on UI thread");
        }
        Iterator it = eVar.f.iterator();
        while (it.hasNext()) {
            eVar.a((Task) it.next(), true);
        }
        eVar.f.clear();
    }

    private Task c(Task task) {
        return new c(this, task);
    }

    @Override // com.amazon.android.framework.task.pipeline.f
    public final void a() {
        this.d.a();
        this.e.a();
        this.f.clear();
    }

    @Override // com.amazon.android.framework.task.pipeline.f
    public final void a(Task task) {
        a(task, false);
    }

    @Override // com.amazon.android.framework.task.pipeline.f
    public final void a(Task task, long j) {
        this.e.a(c(task), j);
    }

    @Override // com.amazon.android.framework.task.pipeline.f
    public final void a(Task task, Date date) {
        this.e.a(c(task), date);
    }

    @Override // com.amazon.android.framework.task.pipeline.f
    public final void b(Task task) {
        a(task, true);
    }

    @Override // com.amazon.android.framework.resource.b
    public final void onResourcesPopulated() {
        this.c.a(new b(this));
    }
}
