package com.amazon.android.framework.util;

import java.lang.reflect.Method;

/* JADX INFO: loaded from: classes.dex */
public final class d {
    private static final KiwiLogger a = new KiwiLogger("Reflection");

    public static Class a(String str) {
        com.amazon.android.d.a.a(str, "String className");
        try {
            return Thread.currentThread().getContextClassLoader().loadClass(str);
        } catch (ClassNotFoundException e) {
            if (KiwiLogger.TRACE_ON) {
                a.trace("ClassNoFound: " + str);
            }
            return null;
        }
    }

    public static boolean a(Method method) {
        com.amazon.android.d.a.a(method, "Method m");
        return method.getParameterTypes().length > 0;
    }

    public static boolean b(Method method) {
        com.amazon.android.d.a.a(method, "Method m");
        return method.getReturnType().equals(Void.TYPE);
    }

    public static boolean c(Method method) {
        com.amazon.android.d.a.a(method, "Method m");
        return (method.getModifiers() & 8) != 0;
    }
}
