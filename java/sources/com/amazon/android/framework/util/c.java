package com.amazon.android.framework.util;

import java.util.Iterator;
import java.util.WeakHashMap;

/* JADX INFO: loaded from: classes.dex */
public final class c implements Iterable {
    public WeakHashMap a = new WeakHashMap();

    public final void a(Object obj) {
        this.a.put(obj, null);
    }

    public final void b(Object obj) {
        this.a.remove(obj);
    }

    @Override // java.lang.Iterable
    public final Iterator iterator() {
        return this.a.keySet().iterator();
    }

    public final String toString() {
        return this.a.keySet().toString();
    }
}
