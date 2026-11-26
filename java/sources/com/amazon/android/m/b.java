package com.amazon.android.m;

import com.amazon.android.framework.util.KiwiLogger;
import java.util.HashMap;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public final class b implements g {
    private static final KiwiLogger a = new KiwiLogger("EventManagerImpl");
    private final Map b = new HashMap();

    @Override // com.amazon.android.m.g
    public final void a(c cVar) {
        com.amazon.android.d.a.a(cVar, "listener");
        com.amazon.android.d.a.a();
        f fVarA = cVar.a();
        a.trace("Registering listener for event: " + fVarA + ", " + cVar);
        e eVar = (e) this.b.get(fVarA);
        if (eVar == null) {
            eVar = new e();
            this.b.put(fVarA, eVar);
        }
        eVar.a(cVar);
    }

    @Override // com.amazon.android.m.g
    public final void a(d dVar) {
        f fVarA = dVar.a();
        if (KiwiLogger.TRACE_ON) {
            a.trace("Posting event: " + fVarA);
        }
        if (this.b.containsKey(fVarA)) {
            ((e) this.b.get(fVarA)).a(dVar);
        } else if (KiwiLogger.TRACE_ON) {
            a.trace("No registered listeners, returning");
        }
    }
}
