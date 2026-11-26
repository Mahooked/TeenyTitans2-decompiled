package com.amazon.android.framework.prompt;

/* JADX INFO: loaded from: classes.dex */
final class o implements com.amazon.android.m.c {
    private /* synthetic */ PromptManagerImpl a;

    o(PromptManagerImpl promptManagerImpl) {
        this.a = promptManagerImpl;
    }

    @Override // com.amazon.android.m.c
    public final com.amazon.android.m.f a() {
        return com.amazon.android.j.d.DESTROY;
    }

    @Override // com.amazon.android.m.c
    public final /* bridge */ /* synthetic */ void a(com.amazon.android.m.d dVar) {
        this.a.finish();
    }

    @Override // com.amazon.android.m.c
    public final com.amazon.android.m.a b() {
        return com.amazon.android.m.a.FIRST;
    }
}
