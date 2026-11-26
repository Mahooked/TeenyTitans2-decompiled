package com.amazon.android.framework.context;

import android.app.Activity;
import android.app.Application;
import android.app.Service;
import android.content.Intent;
import android.content.IntentFilter;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.task.TaskManager;
import com.amazon.android.framework.task.pipeline.TaskPipelineId;
import com.amazon.android.framework.util.KiwiLogger;
import com.amazon.android.m.g;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.AtomicReference;

/* JADX INFO: loaded from: classes.dex */
public final class d implements ContextManager, com.amazon.android.framework.resource.b {
    public static final KiwiLogger a = new KiwiLogger("ContextManagerImpl");

    @Resource
    private Application i;

    @Resource
    private TaskManager j;

    @Resource
    private g k;
    private String l;
    private final AtomicReference b = new AtomicReference();
    private final List c = new ArrayList();
    private final com.amazon.android.framework.util.c d = new com.amazon.android.framework.util.c();
    private final com.amazon.android.framework.util.c e = new com.amazon.android.framework.util.c();
    private final AtomicBoolean f = new AtomicBoolean(false);
    private final AtomicBoolean g = new AtomicBoolean(false);
    private final AtomicBoolean h = new AtomicBoolean(false);
    private boolean m = false;

    private static Activity a(Activity activity) {
        Activity parent = activity;
        while (parent.isChild()) {
            parent = parent.getParent();
        }
        return parent;
    }

    static /* synthetic */ void a(d dVar) {
        if (KiwiLogger.TRACE_ON) {
            a.trace("----------- EXECUTING FINISH ACTIVITIES -----------");
            a.trace(dVar.i.getPackageName());
            a.trace("---------------------------------------------------");
        }
        Activity root = dVar.getRoot();
        for (Activity activity : dVar.c) {
            if (!activity.equals(root) && activity != null) {
                if (activity.isChild()) {
                    a.trace("Not finishing activity: " + activity + ", it is a child of: " + activity.getParent());
                } else {
                    a.trace("Finishing Activity: " + activity);
                    activity.finish();
                }
            }
        }
        Activity root2 = dVar.getRoot();
        if (root2 == null) {
            a.trace("Shutdown found no root, no activities to pop off stack!");
        } else {
            a.trace("Moving task to background");
            root2.moveTaskToBack(true);
            a.trace("Popping activity stack, root: " + root2);
            Intent intent = new Intent(root2, root2.getClass());
            intent.addFlags(67108864);
            intent.addFlags(536870912);
            root2.startActivity(intent);
        }
        dVar.j.enqueue(TaskPipelineId.FOREGROUND, new a(dVar));
    }

    static /* synthetic */ void a(d dVar, Intent intent) {
        if (KiwiLogger.TRACE_ON) {
            a.trace("Received broadcast intent: " + intent);
        }
        if (!intent.getAction().equals(dVar.l)) {
            if (KiwiLogger.ERROR_ON) {
                a.error("Received broadcast for unrequested action: " + intent.getAction());
                return;
            }
            return;
        }
        if (!dVar.g.get()) {
            if (KiwiLogger.ERROR_ON) {
                a.error("Received intent to shutdown app when we are not in shutdown state: " + intent);
                return;
            }
            return;
        }
        if (!dVar.h.compareAndSet(false, true)) {
            if (KiwiLogger.TRACE_ON) {
                a.trace("shutdown broadcast already received, ignoring");
                return;
            }
            return;
        }
        if (KiwiLogger.TRACE_ON) {
            a.trace("Stopping services in response to broadcast");
            a.trace("Service to stop: " + dVar.d.a.size());
        }
        for (Service service : dVar.d) {
            if (service != null) {
                if (KiwiLogger.TRACE_ON) {
                    a.trace("Stopping service: " + service);
                }
                service.stopSelf();
            }
        }
    }

    private void a(com.amazon.android.j.c cVar, Activity activity) {
        this.k.a(new com.amazon.android.j.b(cVar, activity));
    }

    private void a(com.amazon.android.j.d dVar) {
        this.k.a(new com.amazon.android.j.a(dVar, this.i));
    }

    @Override // com.amazon.android.framework.context.ContextManager
    public final void finishActivities() {
        if (this.f.compareAndSet(false, true)) {
            if (KiwiLogger.TRACE_ON) {
                a.trace("---------- SCHEDULING FINISH ACTIVITIES -----------");
                a.trace(this.i.getPackageName());
                a.trace(Thread.currentThread().toString());
                a.trace("---------------------------------------------------");
            }
            this.j.enqueue(TaskPipelineId.FOREGROUND, new c(this));
        }
    }

    @Override // com.amazon.android.framework.context.ContextManager
    public final Activity getRoot() {
        com.amazon.android.d.a.a();
        for (int size = this.c.size() - 1; size >= 0; size--) {
            Activity activity = (Activity) this.c.get(size);
            if (activity == null ? false : activity.isTaskRoot()) {
                return activity;
            }
        }
        return null;
    }

    @Override // com.amazon.android.framework.context.ContextManager
    public final Activity getVisible() {
        com.amazon.android.d.a.a();
        return (Activity) this.b.get();
    }

    @Override // com.amazon.android.framework.context.ContextManager
    public final boolean hasAppShutdownBeenRequested() {
        return this.g.get() || this.f.get();
    }

    @Override // com.amazon.android.framework.context.ContextManager
    public final boolean isVisible() {
        return this.b.get() != null;
    }

    @Override // com.amazon.android.framework.context.ContextManager
    public final void onCreate(Activity activity) {
        com.amazon.android.d.a.a(activity, "activity");
        com.amazon.android.d.a.a();
        this.c.add(activity);
        if (KiwiLogger.TRACE_ON) {
            a.trace("Activity->onCreate.  Activity: " + activity + ", Total Activities: " + this.c.size());
        }
        a(com.amazon.android.j.c.CREATE, activity);
        if (!this.m && this.c.size() == 1) {
            this.m = true;
            a(com.amazon.android.j.d.CREATE);
        }
    }

    @Override // com.amazon.android.framework.context.ContextManager
    public final void onCreate(Service service) {
        com.amazon.android.d.a.a();
        if (KiwiLogger.TRACE_ON) {
            a.trace("Service->onCreate: " + service);
        }
        this.d.a(service);
    }

    /* JADX WARN: Code duplicated, block: B:18:0x0093  */
    @Override // com.amazon.android.framework.context.ContextManager
    public final void onDestroy(Activity activity) {
        boolean z;
        com.amazon.android.d.a.a(activity, "activity");
        com.amazon.android.d.a.a();
        this.c.remove(activity);
        if (KiwiLogger.TRACE_ON) {
            a.trace("Activity->onDestroy.  Activity: " + activity + ", Total Activities: " + this.c.size());
        }
        a(com.amazon.android.j.c.DESTROY, activity);
        if (KiwiLogger.TRACE_ON) {
            a.trace("Checking if application is destroyed");
        }
        if (this.c.isEmpty()) {
            a.trace("App is destroyed: " + activity.isTaskRoot() + ", " + activity.isFinishing());
            if (activity.isTaskRoot() && activity.isFinishing()) {
                z = true;
            } else {
                z = false;
            }
        } else {
            z = false;
        }
        if (z) {
            this.m = false;
            a(com.amazon.android.j.d.DESTROY);
        }
    }

    @Override // com.amazon.android.framework.context.ContextManager
    public final void onDestroy(Service service) {
        com.amazon.android.d.a.a();
        this.d.b(service);
    }

    @Override // com.amazon.android.framework.context.ContextManager
    public final void onPause(Activity activity) {
        com.amazon.android.d.a.a();
        if (KiwiLogger.TRACE_ON) {
            a.trace("Activity paused: " + activity + ", visible activity: " + getVisible());
        }
        if (getVisible() == activity) {
            if (KiwiLogger.TRACE_ON) {
                a.trace("Setting visible activity to null");
            }
            this.b.set(null);
            a(com.amazon.android.j.c.PAUSE, activity);
        }
    }

    @Override // com.amazon.android.framework.resource.b
    public final void onResourcesPopulated() {
        this.l = "com.amazon." + this.i.getPackageName() + ".shutdown";
        this.i.registerReceiver(new b(this), new IntentFilter(this.l));
    }

    @Override // com.amazon.android.framework.context.ContextManager
    public final void onResume(Activity activity) {
        com.amazon.android.d.a.a();
        a.trace("Activity resumed: " + activity + ", is child: " + activity.isChild());
        Activity activityA = a(activity);
        a.trace("Setting visible to: " + activityA);
        this.b.set(activityA);
        if (KiwiLogger.TRACE_ON) {
            a.trace("Activity now visible: " + activity + ", publishing resume event");
        }
        a(com.amazon.android.j.c.RESUME, activity);
    }

    @Override // com.amazon.android.framework.context.ContextManager
    public final void onStart(Activity activity) {
        com.amazon.android.d.a.a(activity, "activity");
        com.amazon.android.d.a.a();
        a.trace("Activity started: " + activity);
        this.e.a(activity);
        a(com.amazon.android.j.c.START, activity);
        if (this.e.a.size() == 1) {
            a(com.amazon.android.j.d.START);
        }
    }

    @Override // com.amazon.android.framework.context.ContextManager
    public final void onStop(Activity activity) {
        com.amazon.android.d.a.a(activity, "activity");
        com.amazon.android.d.a.a();
        a.trace("Activity stopped: " + activity);
        this.e.b(activity);
        a(com.amazon.android.j.c.STOP, activity);
        if (this.e.a.isEmpty()) {
            a(com.amazon.android.j.d.STOP);
        }
    }

    @Override // com.amazon.android.framework.context.ContextManager
    public final void stopServices() {
        if (!this.g.compareAndSet(false, true) && KiwiLogger.TRACE_ON) {
            a.trace("Ignoring duplicate stopServices request");
        }
        if (KiwiLogger.TRACE_ON) {
            a.trace("------------- STOPPING SERVICES -------------------");
            a.trace(this.i.getPackageName());
            a.trace("---------------------------------------------------");
        }
        Intent intent = new Intent(this.l);
        intent.setPackage(this.i.getPackageName());
        if (KiwiLogger.TRACE_ON) {
            a.trace("Sending Broadcast!!!!: " + intent + ", Thread: " + Thread.currentThread());
        }
        this.i.sendBroadcast(intent);
    }
}
