package com.amazon.android.framework.prompt;

import android.app.Activity;
import android.app.Dialog;
import com.amazon.android.framework.context.ContextManager;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.task.TaskManager;
import com.amazon.android.framework.task.pipeline.TaskPipelineId;
import com.amazon.android.framework.util.KiwiLogger;
import java.util.Iterator;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.concurrent.atomic.AtomicBoolean;

/* JADX INFO: loaded from: classes.dex */
public class PromptManagerImpl implements PromptManager, com.amazon.android.framework.resource.b {
    public static final KiwiLogger LOGGER = new KiwiLogger("PromptManagerImpl");

    @Resource
    private ContextManager contextManager;

    @Resource
    private com.amazon.android.m.g eventManager;

    @Resource
    private com.amazon.android.framework.resource.a resourceManager;
    private Prompt showing;

    @Resource
    private TaskManager taskManager;
    private Set pending = new LinkedHashSet();
    private final AtomicBoolean finished = new AtomicBoolean(false);

    /* JADX INFO: Access modifiers changed from: private */
    public void finish() {
        if (this.finished.compareAndSet(false, true)) {
            if (KiwiLogger.TRACE_ON) {
                LOGGER.trace("PromptManager finishing....");
            }
            Iterator it = this.pending.iterator();
            while (it.hasNext()) {
                Prompt prompt = (Prompt) it.next();
                it.remove();
                prompt.expire();
            }
            if (this.showing != null) {
                this.showing.dismiss();
            }
        }
    }

    private Prompt getNextPending() {
        if (this.pending.isEmpty()) {
            return null;
        }
        return (Prompt) this.pending.iterator().next();
    }

    /* JADX INFO: Access modifiers changed from: private */
    public void onResume(Activity activity) {
        if (this.showing != null) {
            show(this.showing, activity);
        } else {
            presentNextPending(activity);
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public void presentImpl(Prompt prompt) {
        if (this.finished.get()) {
            if (KiwiLogger.ERROR_ON) {
                LOGGER.error("Prompt: " + prompt + " presented after app destruction expiring it now!");
            }
            prompt.expire();
            return;
        }
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Presening Prompt: " + prompt);
        }
        prompt.register(this);
        this.pending.add(prompt);
        if (this.showing != null) {
            if (KiwiLogger.TRACE_ON) {
                LOGGER.trace("Dialog currently showing, not presenting given dialog");
            }
        } else {
            Activity visible = this.contextManager.getVisible();
            if (visible != null) {
                presentNextPending(visible);
            }
        }
    }

    private void presentNextPending(Activity activity) {
        Prompt nextPending = getNextPending();
        if (nextPending == null) {
            return;
        }
        show(nextPending, activity);
    }

    private void registerActivityResumedListener() {
        this.eventManager.a(new k(this));
    }

    private void registerAppDestructionListener() {
        this.eventManager.a(new o(this));
    }

    /* JADX INFO: Access modifiers changed from: private */
    public void removeExpiredPrompt(Prompt prompt) {
        this.pending.remove(prompt);
        if (this.showing == prompt) {
            this.showing = null;
            Activity visible = this.contextManager.getVisible();
            if (visible != null) {
                presentNextPending(visible);
            }
        }
    }

    private void show(Prompt prompt, Activity activity) {
        this.showing = prompt;
        prompt.show(activity);
    }

    @Override // com.amazon.android.h.d
    public void observe(Prompt prompt) {
        this.taskManager.enqueue(TaskPipelineId.FOREGROUND, new i(this, prompt));
    }

    @Override // com.amazon.android.framework.prompt.PromptManager
    public Dialog onCreateDialog(Activity activity, int i) {
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("onCreateDialog, id: " + i + ", activity: " + activity);
        }
        if (this.showing == null) {
            if (KiwiLogger.TRACE_ON) {
                LOGGER.trace("Showing dialog is null, returning");
            }
            return null;
        }
        if (this.showing.getIdentifier() != i) {
            if (KiwiLogger.TRACE_ON) {
                LOGGER.trace("Showing dialog id does not match given id: " + i + ", returning");
            }
            return null;
        }
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Creating dialog prompt: " + this.showing);
        }
        return this.showing.create(activity);
    }

    @Override // com.amazon.android.framework.resource.b
    public void onResourcesPopulated() {
        registerActivityResumedListener();
        registerAppDestructionListener();
    }

    @Override // com.amazon.android.framework.prompt.PromptManager
    public void onWindowFocusChanged(Activity activity, boolean z) {
        if (this.showing != null) {
            this.showing.onFocusChanged(activity, z);
        }
    }

    @Override // com.amazon.android.framework.prompt.PromptManager
    public void present(Prompt prompt) {
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Scheduling presentation: " + prompt);
        }
        this.resourceManager.b(prompt);
        if (!this.finished.get()) {
            this.taskManager.enqueue(TaskPipelineId.FOREGROUND, new n(this, prompt));
        } else {
            if (KiwiLogger.ERROR_ON) {
                LOGGER.error("Prompt: " + prompt + " presented after app destruction expiring it now!");
            }
            prompt.expire();
        }
    }
}
