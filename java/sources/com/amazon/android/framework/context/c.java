package com.amazon.android.framework.context;

import com.amazon.android.framework.task.Task;

/* JADX INFO: loaded from: classes.dex */
final class c implements Task {
    private /* synthetic */ d a;

    c(d dVar) {
        this.a = dVar;
    }

    @Override // com.amazon.android.framework.task.Task
    public final void execute() {
        d.a(this.a);
    }

    public final String toString() {
        return "ContextManager: init app shutdown on main thread";
    }
}
