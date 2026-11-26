package com.amazon.android.framework.task.command;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.content.pm.ActivityInfo;
import android.content.pm.PackageManager;
import android.os.RemoteException;
import com.amazon.android.framework.prompt.Prompt;
import com.amazon.android.framework.util.KiwiLogger;
import com.amazon.venezia.command.r;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

/* JADX INFO: loaded from: classes.dex */
public final class d extends Prompt {
    private static final KiwiLogger a = new KiwiLogger("DecisionDialog");
    private final g c;
    private final BlockingQueue d = new LinkedBlockingQueue();
    private final Thread b = Thread.currentThread();

    public d(r rVar) throws RemoteException {
        this.c = new g(rVar);
    }

    private static ActivityInfo a(Activity activity) {
        try {
            return activity.getPackageManager().getActivityInfo(activity.getComponentName(), 128);
        } catch (PackageManager.NameNotFoundException e) {
            if (KiwiLogger.ERROR_ON) {
                a.error("Unable to get info for activity: " + activity);
            }
            return null;
        }
    }

    private void a(AlertDialog alertDialog, n nVar, int i) {
        if (nVar == null) {
            return;
        }
        alertDialog.setButton(i, nVar.b, new e(this, nVar));
    }

    private static boolean a(n nVar) {
        return (nVar == null || nVar.c == null) ? false : true;
    }

    private com.amazon.venezia.command.n c() throws com.amazon.android.b.c {
        try {
            if (KiwiLogger.TRACE_ON) {
                a.trace("Blocking while awaiting customer decision: " + Thread.currentThread());
            }
            return ((n) this.d.take()).a;
        } catch (InterruptedException e) {
            if (KiwiLogger.TRACE_ON) {
                a.trace("Interrupted while awaiting decision, throwing decision expired!");
            }
            throw new com.amazon.android.b.c(getExpirationReason() == com.amazon.android.framework.prompt.f.NOT_COMPATIBLE ? b.APP_NOT_COMPATIBLE : b.EXPIRATION_DURATION_ELAPSED);
        }
    }

    public final com.amazon.venezia.command.n a() throws com.amazon.android.b.c {
        if (KiwiLogger.TRACE_ON) {
            a.trace("GetCustomerDecision: " + this.b);
        }
        return c();
    }

    @Override // com.amazon.android.framework.prompt.Prompt
    public final boolean doCompatibilityCheck(Activity activity) {
        if (!(a(this.c.d) || a(this.c.e) || a(this.c.f))) {
            return true;
        }
        ActivityInfo activityInfoA = a(activity);
        if (activityInfoA == null) {
            return false;
        }
        boolean z = activityInfoA.launchMode == 3;
        a.trace("Single instance: " + z);
        boolean z2 = (activityInfoA.flags & 2) != 0;
        a.trace("Finish on task launch:" + z2);
        boolean z3 = (activityInfoA.flags & 128) != 0;
        a.trace("No History: " + z3);
        return (z || z2 || z3) ? false : true;
    }

    @Override // com.amazon.android.framework.prompt.Prompt
    public final Dialog doCreate(Activity activity) {
        AlertDialog.Builder builder = new AlertDialog.Builder(activity);
        builder.setTitle(this.c.a).setMessage(this.c.b).setCancelable(false);
        AlertDialog alertDialogCreate = builder.create();
        a(alertDialogCreate, this.c.d, -1);
        a(alertDialogCreate, this.c.e, -3);
        a(alertDialogCreate, this.c.f, -2);
        return alertDialogCreate;
    }

    @Override // com.amazon.android.framework.prompt.Prompt
    protected final void doExpiration(com.amazon.android.framework.prompt.f fVar) {
        if (KiwiLogger.TRACE_ON) {
            a.trace("Expiring Decision Dialog: Thread: " + Thread.currentThread());
        }
        this.b.interrupt();
    }

    @Override // com.amazon.android.h.c
    protected final long getExpirationDurationInSeconds() {
        return this.c.c;
    }

    public final String toString() {
        return "DecisionDialog: " + this.c.a;
    }
}
