package com.amazon.android.i;

import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public final class a implements Iterable {
    public Map a = new HashMap();

    public final a a(Object obj, Object obj2, c cVar) {
        boolean zEquals;
        if (obj == null) {
            zEquals = obj2 == null;
        } else {
            zEquals = obj.equals(obj2);
        }
        if (!zEquals) {
            this.a.put(cVar, new b(cVar, "'" + obj + "' != '" + obj2 + "'"));
        }
        return this;
    }

    public final boolean a() {
        return !this.a.isEmpty();
    }

    @Override // java.lang.Iterable
    public final Iterator iterator() {
        return this.a.values().iterator();
    }

    public final String toString() {
        StringBuilder sb = new StringBuilder("Verifier:");
        Iterator it = this.a.values().iterator();
        while (it.hasNext()) {
            sb.append("\n\t" + ((b) it.next()));
        }
        return sb.toString();
    }
}
