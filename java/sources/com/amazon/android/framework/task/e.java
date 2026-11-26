package com.amazon.android.framework.task;

import java.util.Date;

/* JADX INFO: loaded from: classes.dex */
final class e implements b {
    private /* synthetic */ Date a;
    private /* synthetic */ a b;

    e(a aVar, Date date) {
        this.b = aVar;
        this.a = date;
    }

    @Override // com.amazon.android.framework.task.b
    public final void a(Task task, com.amazon.android.framework.task.pipeline.f fVar) {
        fVar.a(task, this.a);
    }
}
