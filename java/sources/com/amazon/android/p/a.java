package com.amazon.android.p;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public final class a implements Serializable, Iterable {
    private static final long serialVersionUID = 1;
    final List a = new ArrayList();

    public final boolean a() {
        return this.a.isEmpty();
    }

    public final int b() {
        return this.a.size();
    }

    @Override // java.lang.Iterable
    public final Iterator iterator() {
        return this.a.iterator();
    }

    public final String toString() {
        return "MetricBatch: [" + this.a + "]";
    }
}
