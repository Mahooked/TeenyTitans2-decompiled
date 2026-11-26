package com.amazon.android.framework.prompt;

/* JADX INFO: loaded from: classes.dex */
final class k implements com.amazon.android.m.c {
    private /* synthetic */ PromptManagerImpl a;

    k(PromptManagerImpl promptManagerImpl) {
        this.a = promptManagerImpl;
    }

    @Override // com.amazon.android.m.c
    public final com.amazon.android.m.f a() {
        return com.amazon.android.j.c.RESUME;
    }

    @Override // com.amazon.android.m.c
    public final /* bridge */ /* synthetic */ void a(com.amazon.android.m.d dVar) {
        this.a.onResume(((com.amazon.android.j.b) dVar).a);
    }

    @Override // com.amazon.android.m.c
    public final com.amazon.android.m.a b() {
        return com.amazon.android.m.a.MIDDLE;
    }
}
