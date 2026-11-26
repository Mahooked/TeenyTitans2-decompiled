package com.amazon.android.framework.context;

import android.app.Activity;
import com.amazon.android.framework.task.Task;

/* JADX INFO: loaded from: classes.dex */
final class a implements Task {
    private /* synthetic */ d a;

    a(d dVar) {
        this.a = dVar;
    }

    @Override // com.amazon.android.framework.task.Task
    public final void execute() {
        Activity root = this.a.getRoot();
        d.a.trace("Finishing Root Task: " + root);
        if (root != null) {
            d.a.trace("Finishing Root");
            root.finish();
        }
    }

    public final String toString() {
        return "ContextManager: kill root task";
    }
}
