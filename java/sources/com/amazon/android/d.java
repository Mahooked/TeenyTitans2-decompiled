package com.amazon.android;

/* JADX INFO: loaded from: classes.dex */
final class d implements com.amazon.android.m.c {
    private /* synthetic */ Kiwi a;

    d(Kiwi kiwi) {
        this.a = kiwi;
    }

    @Override // com.amazon.android.m.c
    public final com.amazon.android.m.f a() {
        return com.amazon.android.j.d.CREATE;
    }

    @Override // com.amazon.android.m.c
    public final /* bridge */ /* synthetic */ void a(com.amazon.android.m.d dVar) {
        this.a.enqueueAppLaunchWorkflowTask();
    }

    @Override // com.amazon.android.m.c
    public final com.amazon.android.m.a b() {
        return com.amazon.android.m.a.LAST;
    }
}
