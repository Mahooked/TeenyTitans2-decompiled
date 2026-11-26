package com.amazon.android.m;

import com.amazon.android.framework.util.KiwiLogger;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public final class e implements Comparator {
    private static final KiwiLogger a = new KiwiLogger("EventListenerNotificationQueue");
    private List b = new ArrayList();

    public final void a(c cVar) {
        if (KiwiLogger.TRACE_ON) {
            a.trace("Adding listener: " + cVar);
        }
        this.b.add(cVar);
        Collections.sort(this.b, this);
    }

    public final void a(d dVar) {
        for (c cVar : this.b) {
            if (KiwiLogger.TRACE_ON) {
                a.trace("Notifying listener: " + cVar);
            }
            cVar.a(dVar);
        }
    }

    @Override // java.util.Comparator
    public final /* bridge */ /* synthetic */ int compare(Object obj, Object obj2) {
        return ((c) obj).b().compareTo(((c) obj2).b());
    }
}
