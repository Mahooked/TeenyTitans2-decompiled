package com.amazon.android.licensing;

import com.amazon.android.framework.task.Task;
import com.amazon.android.framework.util.KiwiLogger;
import java.lang.reflect.Method;

/* JADX INFO: loaded from: classes.dex */
public final class n implements Task {
    private static final KiwiLogger a = new KiwiLogger("DRMSuccessTask");

    private static Method a(Class cls, String str) {
        com.amazon.android.d.a.a(cls, "Class<?> target");
        com.amazon.android.d.a.a(str, "String methodName");
        try {
            Method declaredMethod = cls.getDeclaredMethod(str, new Class[0]);
            if (declaredMethod == null) {
                if (KiwiLogger.TRACE_ON) {
                    a.trace("No exception thrown, but method '" + str + "' was not found, this should not happen. ");
                }
                return null;
            }
            declaredMethod.setAccessible(true);
            if (!com.amazon.android.framework.util.d.c(declaredMethod)) {
                if (KiwiLogger.TRACE_ON) {
                    a.trace("Callback " + str + " isn't static, ignoring...");
                }
                return null;
            }
            if (!com.amazon.android.framework.util.d.b(declaredMethod)) {
                if (KiwiLogger.TRACE_ON) {
                    a.trace("Callback " + str + " returns a value, ignoring...");
                }
                return null;
            }
            if (!com.amazon.android.framework.util.d.a(declaredMethod)) {
                return declaredMethod;
            }
            if (KiwiLogger.TRACE_ON) {
                a.trace("Callback " + str + " takes parameters, ignoring...");
            }
            return null;
        } catch (NoSuchMethodException e) {
            if (KiwiLogger.TRACE_ON) {
                a.trace("Did not find method " + str);
            }
            return null;
        }
    }

    @Override // com.amazon.android.framework.task.Task
    public final void execute() {
        Method methodA;
        Class clsA = com.amazon.android.framework.util.d.a("com.amazon.drm.AmazonLicenseVerificationCallback");
        if (clsA == null || (methodA = a(clsA, "onDRMSuccess")) == null) {
            return;
        }
        if (KiwiLogger.TRACE_ON) {
            a.trace("Invoking callback: " + methodA.getName());
        }
        try {
            methodA.invoke(null, new Object[0]);
            if (KiwiLogger.TRACE_ON) {
                a.trace("Callback invoked.");
            }
        } catch (Exception e) {
        }
    }
}
