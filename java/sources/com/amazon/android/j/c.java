package com.amazon.android.j;

import com.amazon.android.m.f;

/* JADX INFO: loaded from: classes.dex */
public enum c implements f {
    CREATE,
    DESTROY,
    RESUME,
    PAUSE,
    START,
    STOP;

    @Override // java.lang.Enum
    public final String toString() {
        return "ACTIVITY_" + name();
    }
}
