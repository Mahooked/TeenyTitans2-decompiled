package com.amazon.android.framework.task.pipeline;

/* JADX INFO: loaded from: classes.dex */
final class b implements com.amazon.android.m.c {
    private /* synthetic */ e a;

    b(e eVar) {
        this.a = eVar;
    }

    @Override // com.amazon.android.m.c
    public final com.amazon.android.m.f a() {
        return com.amazon.android.j.c.RESUME;
    }

    @Override // com.amazon.android.m.c
    public final /* bridge */ /* synthetic */ void a(com.amazon.android.m.d dVar) {
        e.a(this.a);
    }

    @Override // com.amazon.android.m.c
    public final com.amazon.android.m.a b() {
        return com.amazon.android.m.a.MIDDLE;
    }

    public final String toString() {
        return "ForegroundTaskPipeline:onResume listener";
    }
}
