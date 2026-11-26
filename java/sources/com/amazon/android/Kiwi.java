package com.amazon.android;

import android.app.Activity;
import android.app.Application;
import android.app.Dialog;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import com.amazon.android.framework.context.ContextManager;
import com.amazon.android.framework.prompt.PromptManager;
import com.amazon.android.framework.prompt.PromptManagerImpl;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.task.Task;
import com.amazon.android.framework.task.TaskManager;
import com.amazon.android.framework.task.command.AbstractCommandTask;
import com.amazon.android.framework.task.pipeline.TaskPipelineId;
import com.amazon.android.framework.util.KiwiLogger;

/* JADX INFO: loaded from: classes.dex */
public final class Kiwi implements com.amazon.android.framework.resource.b {
    protected static final String ACTIVITY_NAME = "ActivityName";
    protected static final String EVENT_NAME = "EventName";
    private static Kiwi INSTANCE = null;
    private static final KiwiLogger LOGGER = new KiwiLogger("Kiwi");
    protected static final String TIMESTAMP = "Timestamp";

    @Resource
    private Application application;

    @Resource
    private com.amazon.android.l.c authKeyLoader;

    @Resource
    private ContextManager contextManager;

    @Resource
    private com.amazon.android.n.a dataStore;
    private final boolean drmEnabled;

    @Resource
    private com.amazon.android.m.g eventManager;

    @Resource
    private PromptManager promptManager;

    @Resource
    private com.amazon.android.e.b resultManager;

    @Resource
    private TaskManager taskManager;

    private Kiwi(Application application, boolean z) {
        long jCurrentTimeMillis = System.currentTimeMillis();
        this.drmEnabled = z;
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Starting initialization process for application: " + application.getPackageName());
            LOGGER.trace("DRM enabled: " + z);
        }
        instantiateTheWorld(application);
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Kiwi.Constructor Time: " + (System.currentTimeMillis() - jCurrentTimeMillis));
        }
    }

    @Deprecated
    public static void addCommandToCommandTaskPipeline(AbstractCommandTask abstractCommandTask) {
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("WARNING: Use of deprecated method detected.");
        }
        if (isInitialized()) {
            unsafeAddCommandToCommandTaskPipeline(abstractCommandTask);
        } else {
            LOGGER.error("Kiwi subsystem is not fully initialized.  Cannot process task.");
        }
    }

    public static void addCommandToCommandTaskPipeline(AbstractCommandTask abstractCommandTask, Context context) {
        if (!isInitialized()) {
            if (context == null || context.getApplicationContext() == null) {
                LOGGER.error("Kiwi subsystem cannot be initialized because of null context. Unable to enqueue task.");
                return;
            }
            INSTANCE = new Kiwi((Application) context.getApplicationContext(), false);
        }
        unsafeAddCommandToCommandTaskPipeline(abstractCommandTask);
    }

    static /* synthetic */ Kiwi b() {
        INSTANCE = null;
        return null;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public void enqueueAppLaunchWorkflowTask() {
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Enqueuing launch workflow");
        }
        this.taskManager.enqueue(TaskPipelineId.COMMAND, getLaunchWorkflow());
    }

    private Task getLaunchWorkflow() {
        return this.drmEnabled ? new a() : new f();
    }

    public static PromptManager getPromptManager() {
        return INSTANCE.promptManager;
    }

    private static void ignoreEvent(String str, Context context) {
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace(str + " called on context: " + context + " when Kiwi is dead, ignoring...");
        }
    }

    private void instantiateTheWorld(Application application) {
        com.amazon.android.framework.resource.c cVar = new com.amazon.android.framework.resource.c();
        cVar.a(application);
        cVar.a(new com.amazon.android.framework.task.a());
        cVar.a(new com.amazon.android.n.a());
        cVar.a(new com.amazon.android.e.c());
        cVar.a(new com.amazon.android.framework.context.d());
        cVar.a(new PromptManagerImpl());
        cVar.a(new com.amazon.android.m.b());
        cVar.a(new com.amazon.android.c.a());
        cVar.a(new com.amazon.android.p.c());
        cVar.a(new com.amazon.android.framework.task.command.c());
        cVar.a(new com.amazon.android.l.c());
        cVar.a(new com.amazon.android.framework.task.command.f());
        cVar.a();
        cVar.b(this);
    }

    private static boolean isInitialized() {
        return INSTANCE != null;
    }

    public static boolean isSignedByKiwi(String str, String str2) {
        if (isInitialized()) {
            try {
                return com.amazon.android.l.a.a(str, str2, INSTANCE.authKeyLoader.a());
            } catch (com.amazon.android.g.a e) {
                LOGGER.trace("Unable to validate signature: " + e.getMessage());
            }
        } else {
            LOGGER.trace("Kiwi was not yet initialized - cannot do the IAP call");
        }
        return false;
    }

    public static boolean onActivityResult(Activity activity, int i, int i2, Intent intent) {
        if (!preProcess("onActivityResult", activity)) {
            return false;
        }
        return INSTANCE.resultManager.a(new com.amazon.android.e.f(activity, i, i2, intent));
    }

    public static void onCreate(Activity activity, boolean z) {
        long jCurrentTimeMillis = System.currentTimeMillis();
        if (!isInitialized()) {
            INSTANCE = new Kiwi(activity.getApplication(), z);
        }
        if (preProcess("onCreate", activity)) {
            INSTANCE.contextManager.onCreate(activity);
        }
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Kiwi.ActivityOnCreate Time: " + (System.currentTimeMillis() - jCurrentTimeMillis));
        }
    }

    public static void onCreate(Service service, boolean z) {
        long jCurrentTimeMillis = System.currentTimeMillis();
        if (preProcess("onCreate", service)) {
            INSTANCE.contextManager.onCreate(service);
        }
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Kiwi.ServiceOnCreate Time: " + (System.currentTimeMillis() - jCurrentTimeMillis));
        }
    }

    public static Dialog onCreateDialog(Activity activity, int i) {
        long jCurrentTimeMillis = System.currentTimeMillis();
        if (preProcess("onCreateDialog", activity)) {
            return INSTANCE.promptManager.onCreateDialog(activity, i);
        }
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Kiwi.ActivityOnCreateDialog Time: " + (System.currentTimeMillis() - jCurrentTimeMillis));
        }
        return null;
    }

    public static void onDestroy(Activity activity) {
        long jCurrentTimeMillis = System.currentTimeMillis();
        if (preProcess("onDestroy", activity)) {
            try {
                INSTANCE.contextManager.onDestroy(activity);
            } catch (Exception e) {
                LOGGER.error("Kiwi.ActivityOnDestroy Error: ", e);
            }
        }
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Kiwi.ActivityOnDestroy Time: " + (System.currentTimeMillis() - jCurrentTimeMillis));
        }
    }

    public static void onDestroy(Service service) {
        long jCurrentTimeMillis = System.currentTimeMillis();
        if (preProcess("onDestroy", service)) {
            INSTANCE.contextManager.onDestroy(service);
        }
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Kiwi.ServiceOnDestroy Time: " + (System.currentTimeMillis() - jCurrentTimeMillis));
        }
    }

    public static void onPause(Activity activity) {
        long jCurrentTimeMillis = System.currentTimeMillis();
        if (preProcess("onPause", activity)) {
            INSTANCE.contextManager.onPause(activity);
        }
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Kiwi.ActivityOnPause Time: " + (System.currentTimeMillis() - jCurrentTimeMillis));
        }
    }

    public static void onResume(Activity activity) {
        long jCurrentTimeMillis = System.currentTimeMillis();
        if (preProcess("onResume", activity)) {
            INSTANCE.contextManager.onResume(activity);
        }
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Kiwi.ActivityOnResume Time: " + (System.currentTimeMillis() - jCurrentTimeMillis));
        }
    }

    public static void onStart(Activity activity) {
        long jCurrentTimeMillis = System.currentTimeMillis();
        if (preProcess("onStart", activity)) {
            INSTANCE.contextManager.onStart(activity);
        }
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Kiwi.ActivityOnStart Time: " + (System.currentTimeMillis() - jCurrentTimeMillis));
        }
    }

    public static void onStop(Activity activity) {
        long jCurrentTimeMillis = System.currentTimeMillis();
        if (preProcess("onStop", activity)) {
            INSTANCE.contextManager.onStop(activity);
        }
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Kiwi.ActivityOnStop Time: " + (System.currentTimeMillis() - jCurrentTimeMillis));
        }
    }

    public static void onWindowFocusChanged(Activity activity, boolean z) {
        if (preProcess("onWindowFocusChanged", activity)) {
            INSTANCE.promptManager.onWindowFocusChanged(activity, z);
        }
    }

    private static boolean preProcess(String str, Context context) {
        com.amazon.android.d.a.a();
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace(str + ": " + context);
        }
        if (isInitialized()) {
            return true;
        }
        ignoreEvent(str, context);
        return false;
    }

    private void registerActivityLifeCyclePauseListener() {
        this.eventManager.a(new b(this));
    }

    private void registerActivityLifeCycleResumeListener() {
        this.eventManager.a(new c(this));
    }

    private void registerApplicationCreationListener() {
        this.eventManager.a(new d(this));
    }

    private void registerApplicationDestructionListener() {
        this.eventManager.a(new e(this));
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static void unsafeAddCommandToCommandTaskPipeline(AbstractCommandTask abstractCommandTask) {
        INSTANCE.taskManager.enqueue(TaskPipelineId.COMMAND, abstractCommandTask);
    }

    @Override // com.amazon.android.framework.resource.b
    public final void onResourcesPopulated() {
        registerApplicationCreationListener();
        registerApplicationDestructionListener();
        registerActivityLifeCyclePauseListener();
        registerActivityLifeCycleResumeListener();
    }
}
