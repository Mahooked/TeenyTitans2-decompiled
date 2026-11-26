package com.amazon.android.p;

import com.amazon.android.framework.util.KiwiLogger;

/* JADX INFO: loaded from: classes.dex */
public final class c implements d {
    private static final KiwiLogger a = new KiwiLogger("MetricsManagerImpl");
    private a b = new a();

    @Override // com.amazon.android.p.d
    public final synchronized a a() {
        a aVar;
        if (this.b.a()) {
            aVar = this.b;
        } else {
            aVar = this.b;
            this.b = new a();
        }
        return aVar;
    }

    @Override // com.amazon.android.p.d
    public final synchronized void a(b bVar) {
        if (KiwiLogger.TRACE_ON) {
            a.trace("Recording Metric: " + bVar);
        }
        this.b.a.add(bVar);
    }
}
