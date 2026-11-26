package com.unity3d.player;

import android.app.Activity;
import android.content.Context;
import android.os.Looper;
import java.util.concurrent.Semaphore;
import java.util.concurrent.TimeUnit;

/* JADX INFO: loaded from: classes.dex */
class b {
    protected d b;
    protected String e;
    protected m a = null;
    protected Context c = null;
    protected String d = null;

    b(String str, d dVar) {
        this.b = null;
        this.e = "";
        this.e = str;
        this.b = dVar;
    }

    protected void reportError(String str) {
        if (this.b != null) {
            this.b.reportError(this.e + " Error [" + this.d + "]", str);
        } else {
            e.Log(6, this.e + " Error [" + this.d + "]: " + str);
        }
    }

    protected void runOnUiThread(Runnable runnable) {
        if (this.c instanceof Activity) {
            ((Activity) this.c).runOnUiThread(runnable);
        } else {
            e.Log(5, "Not running " + this.e + " from an Activity; Ignoring execution request...");
        }
    }

    protected boolean runOnUiThreadWithSync(final Runnable runnable) {
        boolean z = false;
        if (Looper.getMainLooper().getThread() == Thread.currentThread()) {
            runnable.run();
            return true;
        }
        final Semaphore semaphore = new Semaphore(0);
        runOnUiThread(new Runnable() { // from class: com.unity3d.player.b.1
            @Override // java.lang.Runnable
            public final void run() {
                try {
                    runnable.run();
                } catch (Exception e) {
                    b.this.reportError("Exception unloading Google VR on UI Thread. " + e.getLocalizedMessage());
                } finally {
                    semaphore.release();
                }
            }
        });
        try {
            if (semaphore.tryAcquire(4L, TimeUnit.SECONDS)) {
                z = true;
            } else {
                reportError("Timeout waiting for vr state change!");
            }
        } catch (InterruptedException e) {
            reportError("Interrupted while trying to acquire sync lock. " + e.getLocalizedMessage());
        }
        return z;
    }
}
