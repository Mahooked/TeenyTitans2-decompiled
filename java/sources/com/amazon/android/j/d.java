package com.amazon.android.j;

import com.amazon.android.m.f;

/* JADX INFO: loaded from: classes.dex */
public enum d implements f {
    CREATE,
    DESTROY,
    START,
    STOP;

    @Override // java.lang.Enum
    public final String toString() {
        return "APPLICATION_" + name();
    }
}
