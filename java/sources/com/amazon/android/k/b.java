package com.amazon.android.k;

import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.task.Task;
import com.amazon.android.framework.util.KiwiLogger;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.concurrent.atomic.AtomicBoolean;

/* JADX INFO: loaded from: classes.dex */
public abstract class b extends c implements com.amazon.android.framework.resource.b {
    private static final KiwiLogger a = new KiwiLogger("TaskWorkflow");

    @Resource
    private com.amazon.android.framework.resource.a b;
    private List c = new ArrayList();
    private AtomicBoolean d = new AtomicBoolean(false);

    protected void a() {
    }

    protected final void a(Task task) {
        com.amazon.android.d.a.a(task, "task");
        this.c.add(task);
        if (task instanceof a) {
            ((a) task).setWorkflow(this);
        }
    }

    protected abstract String b();

    public final void c() {
        this.d.set(true);
    }

    @Override // com.amazon.android.framework.task.Task
    public final void execute() {
        try {
            if (KiwiLogger.TRACE_ON) {
                a.trace("Exiting task workflow: " + this);
            }
            for (Task task : this.c) {
                if (this.d.get()) {
                    if (!KiwiLogger.TRACE_ON) {
                        break;
                    }
                    a.trace("Finished set, exiting task workflow early");
                    break;
                }
                task.execute();
            }
        } finally {
            a();
        }
    }

    @Override // com.amazon.android.framework.resource.b
    public final void onResourcesPopulated() {
        Iterator it = this.c.iterator();
        while (it.hasNext()) {
            this.b.b((Task) it.next());
        }
    }

    public final String toString() {
        return b();
    }
}
