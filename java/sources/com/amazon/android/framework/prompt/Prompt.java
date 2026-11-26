package com.amazon.android.framework.prompt;

import android.app.Activity;
import android.app.Dialog;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.task.pipeline.TaskPipelineId;
import com.amazon.android.framework.util.KiwiLogger;
import java.util.Random;
import java.util.concurrent.atomic.AtomicBoolean;

/* JADX INFO: loaded from: classes.dex */
public abstract class Prompt extends com.amazon.android.h.c {
    private static final KiwiLogger LOGGER = new KiwiLogger("Prompt");
    private Activity context;

    @Resource
    private com.amazon.android.n.a dataStore;
    private Dialog dialog;
    private final AtomicBoolean dismissed = new AtomicBoolean(false);
    private final int identifier = createIdentifier();
    private f manualExpirationReason;

    public Prompt() {
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Creating Prompt: " + this.identifier);
        }
    }

    private int createIdentifier() {
        int iNextInt = new Random().nextInt(2146249079) + 1234567;
        if (iNextInt <= 1234567) {
            return 1234567;
        }
        return iNextInt;
    }

    private void dismissDialog() {
        if (KiwiLogger.TRACE_ON) {
            LOGGER.error("Dismissing dialog: " + this.identifier);
        }
        try {
            this.context.dismissDialog(this.identifier);
            this.context.removeDialog(this.identifier);
        } catch (Exception e) {
            if (KiwiLogger.TRACE_ON) {
                LOGGER.error("Unable to remove dialog: " + this.identifier);
            }
        }
        this.context = null;
        this.dialog = null;
    }

    private void expire(f fVar) {
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Expiring prompt pre-maturely: id: " + getIdentifier() + ", prompt: " + this + ",, reason: " + fVar);
        }
        this.manualExpirationReason = fVar;
        expire();
    }

    private boolean isCompatible(Activity activity) {
        if (this.dataStore.b("TEST_MODE")) {
            return false;
        }
        return doCompatibilityCheck(activity);
    }

    private void showDialog(Activity activity) {
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Showing prompt, id: " + getIdentifier() + ", prompt: " + this + ", activity: " + activity);
        }
        if (this.context != null) {
            dismissDialog();
        }
        activity.showDialog(getIdentifier());
    }

    public final Dialog create(Activity activity) {
        this.context = activity;
        this.dialog = doCreate(activity);
        this.dialog.setCancelable(false);
        this.dialog.setOnKeyListener(new d(this));
        return this.dialog;
    }

    protected final boolean dismiss() {
        com.amazon.android.d.a.a();
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Dismissing Prompt: " + this.identifier);
        }
        if (!this.dismissed.compareAndSet(false, true)) {
            if (KiwiLogger.TRACE_ON) {
                LOGGER.error("Prompt has already been dismissed");
            }
            return false;
        }
        if (this.context != null) {
            dismissDialog();
        }
        discard();
        return true;
    }

    protected boolean doCompatibilityCheck(Activity activity) {
        return true;
    }

    protected abstract Dialog doCreate(Activity activity);

    @Override // com.amazon.android.h.b
    protected final void doExpiration() {
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Expiring prompt: " + this);
        }
        this.taskManager.enqueue(TaskPipelineId.FOREGROUND, new c(this));
        doExpiration(getExpirationReason());
    }

    protected abstract void doExpiration(f fVar);

    protected f getExpirationReason() {
        if (isExpired()) {
            return this.manualExpirationReason == null ? f.EXPIRATION_DURATION_ELAPSED : this.manualExpirationReason;
        }
        return null;
    }

    public int getIdentifier() {
        return this.identifier;
    }

    public void onFocusChanged(Activity activity, boolean z) {
        if (activity != this.context) {
            if (KiwiLogger.TRACE_ON) {
                LOGGER.trace("Unrecognized context");
            }
        } else {
            if (!z || this.dialog.isShowing()) {
                return;
            }
            if (KiwiLogger.TRACE_ON) {
                LOGGER.trace("showing dialog because it was not showing");
            }
            this.dialog.show();
        }
    }

    public final void show(Activity activity) {
        com.amazon.android.d.a.a(activity, "activity");
        com.amazon.android.d.a.a();
        if (isCompatible(activity)) {
            showDialog(activity);
        } else {
            expire(f.NOT_COMPATIBLE);
        }
    }
}
