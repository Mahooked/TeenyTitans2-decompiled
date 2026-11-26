package com.amazon.android.h;

import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.task.TaskManager;
import com.amazon.android.framework.task.pipeline.TaskPipelineId;
import com.amazon.android.framework.util.KiwiLogger;
import java.util.Date;
import java.util.Iterator;
import java.util.List;
import java.util.Vector;
import java.util.concurrent.atomic.AtomicBoolean;

/* JADX INFO: loaded from: classes.dex */
public abstract class b implements com.amazon.android.framework.resource.b {
    private static final KiwiLogger LOGGER = new KiwiLogger("Expirable");
    private AtomicBoolean expired = new AtomicBoolean(false);
    private final List observers = new Vector();

    @Resource
    protected TaskManager taskManager;

    private void notifyObservers() {
        Iterator it = this.observers.iterator();
        while (it.hasNext()) {
            ((d) it.next()).observe(this);
        }
    }

    private void scheduleExpiration() {
        this.taskManager.enqueueAtTime(TaskPipelineId.BACKGROUND, new a(this), getExpiration());
    }

    public final void discard() {
        if (this.expired.compareAndSet(false, true)) {
            notifyObservers();
        }
    }

    protected abstract void doExpiration();

    public void expire() {
        if (this.expired.compareAndSet(false, true)) {
            if (KiwiLogger.TRACE_ON) {
                LOGGER.trace("Expiring: " + this);
            }
            doExpiration();
            if (KiwiLogger.TRACE_ON) {
                LOGGER.trace("Notifying Observers of expiration: " + this);
            }
            notifyObservers();
        }
    }

    protected abstract Date getExpiration();

    protected boolean isExpired() {
        return this.expired.get();
    }

    @Override // com.amazon.android.framework.resource.b
    public final void onResourcesPopulated() {
        scheduleExpiration();
        onResourcesPopulatedImpl();
    }

    protected void onResourcesPopulatedImpl() {
    }

    public final void register(d dVar) {
        this.observers.add(dVar);
    }
}
