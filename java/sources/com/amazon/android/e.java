package com.amazon.android;

import com.amazon.android.framework.util.KiwiLogger;

/* JADX INFO: loaded from: classes.dex */
final class e implements com.amazon.android.m.c {
    private /* synthetic */ Kiwi a;

    e(Kiwi kiwi) {
        this.a = kiwi;
    }

    @Override // com.amazon.android.m.c
    public final com.amazon.android.m.f a() {
        return com.amazon.android.j.d.DESTROY;
    }

    @Override // com.amazon.android.m.c
    public final /* bridge */ /* synthetic */ void a(com.amazon.android.m.d dVar) {
        if (KiwiLogger.TRACE_ON) {
            Kiwi.LOGGER.trace("Beginning shutdown process for application: " + this.a.application.getPackageName());
        }
        Kiwi.b();
    }

    @Override // com.amazon.android.m.c
    public final com.amazon.android.m.a b() {
        return com.amazon.android.m.a.LAST;
    }
}
