package com.amazon.android.n;

import java.util.Date;

/* JADX INFO: loaded from: classes.dex */
public abstract class b extends com.amazon.android.h.b {
    final Object a;
    private final Date b;

    public b(Object obj, Date date) {
        this.a = obj;
        this.b = date;
    }

    @Override // com.amazon.android.h.b
    public final Date getExpiration() {
        return this.b;
    }
}
