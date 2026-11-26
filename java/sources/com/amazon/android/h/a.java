package com.amazon.android.h;

import com.amazon.android.framework.task.Task;

/* JADX INFO: loaded from: classes.dex */
final class a implements Task {
    private /* synthetic */ b a;

    a(b bVar) {
        this.a = bVar;
    }

    @Override // com.amazon.android.framework.task.Task
    public final void execute() {
        this.a.expire();
    }

    public final String toString() {
        return "Expire: " + this.a.toString();
    }
}
