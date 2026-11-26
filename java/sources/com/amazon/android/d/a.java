package com.amazon.android.d;

import android.os.Looper;

/* JADX INFO: loaded from: classes.dex */
public final class a {
    public static void a() {
        long id = Looper.getMainLooper().getThread().getId();
        if (Thread.currentThread().getId() != id) {
            a("Executing thread must be thread: " + id + ", was: " + Thread.currentThread().getId());
        }
    }

    public static void a(Object obj, String str) {
        if (obj == null) {
            a("Argument: " + str + " cannot be null");
        }
    }

    private static void a(String str) {
        throw new b(str);
    }

    public static void a(boolean z, String str) {
        if (z) {
            return;
        }
        a(str);
    }

    public static void b(boolean z, String str) {
        if (z) {
            a(str);
        }
    }
}
