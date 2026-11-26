package com.amazon.android.framework.task;

/* JADX INFO: loaded from: classes.dex */
final class f implements b {
    private /* synthetic */ long a;
    private /* synthetic */ a b;

    f(a aVar, long j) {
        this.b = aVar;
        this.a = j;
    }

    @Override // com.amazon.android.framework.task.b
    public final void a(Task task, com.amazon.android.framework.task.pipeline.f fVar) {
        fVar.a(task, this.a);
    }
}
