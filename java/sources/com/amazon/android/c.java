package com.amazon.android;

import com.amazon.android.framework.util.KiwiLogger;
import java.util.HashMap;

/* JADX INFO: loaded from: classes.dex */
final class c implements com.amazon.android.m.c {
    private /* synthetic */ Kiwi a;

    c(Kiwi kiwi) {
        this.a = kiwi;
    }

    @Override // com.amazon.android.m.c
    public final com.amazon.android.m.f a() {
        return com.amazon.android.j.c.RESUME;
    }

    @Override // com.amazon.android.m.c
    public final /* bridge */ /* synthetic */ void a(com.amazon.android.m.d dVar) {
        HashMap map = new HashMap();
        map.put("EventName", com.amazon.android.j.c.RESUME.name());
        map.put("ActivityName", ((com.amazon.android.j.b) dVar).a.getClass().getName());
        map.put("Timestamp", Long.valueOf(System.currentTimeMillis()));
        com.amazon.android.s.a aVar = new com.amazon.android.s.a(map);
        if (KiwiLogger.TRACE_ON) {
            Kiwi.LOGGER.trace("Adding lifecycle RESUME command to pipeline");
        }
        Kiwi.unsafeAddCommandToCommandTaskPipeline(aVar);
    }

    @Override // com.amazon.android.m.c
    public final com.amazon.android.m.a b() {
        return com.amazon.android.m.a.MIDDLE;
    }
}
