package com.amazon.android.framework.task.pipeline;

import com.amazon.android.framework.task.Task;

/* JADX INFO: loaded from: classes.dex */
final class c implements Task {
    private /* synthetic */ Task a;
    private /* synthetic */ e b;

    c(e eVar, Task task) {
        this.b = eVar;
        this.a = task;
    }

    @Override // com.amazon.android.framework.task.Task
    public final void execute() {
        this.b.a(this.a, true);
    }

    public final String toString() {
        return "Future:PostToUITask: " + this.a.toString();
    }
}
