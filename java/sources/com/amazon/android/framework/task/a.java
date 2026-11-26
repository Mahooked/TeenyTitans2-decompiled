package com.amazon.android.framework.task;

import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.task.pipeline.TaskPipelineId;
import com.amazon.android.framework.util.KiwiLogger;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.concurrent.atomic.AtomicBoolean;

/* JADX INFO: loaded from: classes.dex */
public final class a implements com.amazon.android.framework.resource.b, TaskManager {
    private static final KiwiLogger a = new KiwiLogger("TaskManagerImpl");

    @Resource
    private com.amazon.android.framework.resource.a b;

    @Resource
    private com.amazon.android.m.g c;
    private final AtomicBoolean d = new AtomicBoolean(false);
    private final Map e = new HashMap();

    public a() {
        com.amazon.android.framework.task.pipeline.a aVarA = com.amazon.android.framework.task.pipeline.a.a(TaskPipelineId.COMMAND.name());
        com.amazon.android.framework.task.pipeline.a aVarA2 = com.amazon.android.framework.task.pipeline.a.a(TaskPipelineId.BACKGROUND.name());
        com.amazon.android.framework.task.pipeline.e eVar = new com.amazon.android.framework.task.pipeline.e(aVarA2);
        this.e.put(TaskPipelineId.COMMAND, aVarA);
        this.e.put(TaskPipelineId.BACKGROUND, aVarA2);
        this.e.put(TaskPipelineId.FOREGROUND, eVar);
    }

    private void a(TaskPipelineId taskPipelineId, Task task, b bVar) {
        if (this.d.get()) {
            if (KiwiLogger.ERROR_ON) {
                a.error("Task enqueued after TaskManager has been finished! Task: " + task);
                return;
            }
            return;
        }
        if (KiwiLogger.TRACE_ON) {
            a.trace("Populating Task: " + task);
        }
        this.b.b(task);
        com.amazon.android.framework.task.pipeline.f fVar = (com.amazon.android.framework.task.pipeline.f) this.e.get(taskPipelineId);
        if (fVar == null) {
            throw new IllegalStateException("No pipeline registered with id: " + taskPipelineId);
        }
        bVar.a(task, fVar);
    }

    public final void a() {
        if (this.d.compareAndSet(false, true)) {
            if (KiwiLogger.TRACE_ON) {
                a.trace("TaskManager finishing....");
            }
            Iterator it = this.e.values().iterator();
            while (it.hasNext()) {
                ((com.amazon.android.framework.task.pipeline.f) it.next()).a();
            }
        }
    }

    @Override // com.amazon.android.framework.task.TaskManager
    public final void enqueue(TaskPipelineId taskPipelineId, Task task) {
        if (KiwiLogger.TRACE_ON) {
            a.trace("Enqueue task on pipeline id: " + taskPipelineId);
        }
        a(taskPipelineId, task, new c(this));
    }

    @Override // com.amazon.android.framework.task.TaskManager
    public final void enqueueAfterDelay(TaskPipelineId taskPipelineId, Task task, long j) {
        a(taskPipelineId, task, new f(this, j));
    }

    @Override // com.amazon.android.framework.task.TaskManager
    public final void enqueueAtFront(TaskPipelineId taskPipelineId, Task task) {
        a(taskPipelineId, task, new d(this));
    }

    @Override // com.amazon.android.framework.task.TaskManager
    public final void enqueueAtTime(TaskPipelineId taskPipelineId, Task task, Date date) {
        a(taskPipelineId, task, new e(this, date));
    }

    @Override // com.amazon.android.framework.resource.b
    public final void onResourcesPopulated() {
        Iterator it = this.e.values().iterator();
        while (it.hasNext()) {
            this.b.b((com.amazon.android.framework.task.pipeline.f) it.next());
        }
        this.c.a(new g(this));
    }
}
