package com.amazon.android.n;

import com.amazon.android.framework.resource.Resource;

/* JADX INFO: loaded from: classes.dex */
public final class a implements com.amazon.android.framework.resource.b {
    public d a = new d();

    @Resource
    private com.amazon.android.framework.resource.a b;

    public final Object a(String str) {
        return this.a.b(str);
    }

    public final void a(String str, Object obj) {
        this.a.a(str, obj);
    }

    public final boolean b(String str) {
        return this.a.a(str);
    }

    @Override // com.amazon.android.framework.resource.b
    public final void onResourcesPopulated() {
        this.b.b(this.a);
    }

    public final String toString() {
        return this.a.toString();
    }
}
