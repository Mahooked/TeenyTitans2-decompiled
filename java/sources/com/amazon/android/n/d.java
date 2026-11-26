package com.amazon.android.n;

import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.util.KiwiLogger;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public final class d implements com.amazon.android.h.d {
    private static final KiwiLogger a = new KiwiLogger("ExpirableValueDataStore");

    @Resource
    private com.amazon.android.framework.resource.a b;
    private final Map c = new HashMap();

    /* JADX INFO: Access modifiers changed from: private */
    @Override // com.amazon.android.h.d
    /* JADX INFO: renamed from: a, reason: merged with bridge method [inline-methods] */
    public synchronized void observe(b bVar) {
        if (KiwiLogger.TRACE_ON) {
            a.trace("Observed expiration: " + bVar + " removing from store!");
        }
        Iterator it = this.c.entrySet().iterator();
        while (it.hasNext()) {
            if (((Map.Entry) it.next()).getValue() == bVar) {
                if (KiwiLogger.TRACE_ON) {
                    a.trace("Removing entry from store: " + bVar);
                }
                it.remove();
            }
        }
    }

    public final synchronized void a(String str, b bVar) {
        com.amazon.android.d.a.a(str, "key");
        com.amazon.android.d.a.a(bVar, "value");
        if (KiwiLogger.TRACE_ON) {
            a.trace("Placing value into store with key: " + str + ", expiration: " + bVar.getExpiration());
        }
        this.b.b(bVar);
        bVar.register(this);
        this.c.put(str, bVar);
    }

    public final synchronized void a(String str, Object obj) {
        if (KiwiLogger.TRACE_ON) {
            a.trace("Placing non-expiring value into store with key: " + str);
        }
        this.c.put(str, new c(this, obj, new Date()));
    }

    public final synchronized boolean a(String str) {
        return b(str) != null;
    }

    public final synchronized Object b(String str) {
        Object obj;
        if (KiwiLogger.TRACE_ON) {
            a.trace("Fetching value: " + str);
        }
        b bVar = (b) this.c.get(str);
        if (bVar == null) {
            if (KiwiLogger.TRACE_ON) {
                a.trace("Value not present in store, returning null");
            }
            obj = null;
        } else {
            obj = bVar.a;
        }
        return obj;
    }

    public final synchronized void c(String str) {
        b bVar = (b) this.c.get(str);
        if (bVar != null) {
            if (KiwiLogger.TRACE_ON) {
                a.trace("Removing value associated with key: " + str + ", value: " + bVar);
            }
            this.c.remove(str);
            bVar.discard();
        }
    }

    public final String toString() {
        return this.c.toString();
    }
}
