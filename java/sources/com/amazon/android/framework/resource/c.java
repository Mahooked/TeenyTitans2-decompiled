package com.amazon.android.framework.resource;

import com.amazon.android.framework.util.KiwiLogger;
import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public final class c implements a {
    private static KiwiLogger a = new KiwiLogger("ResourceManagerImpl");
    private List b = new ArrayList();
    private boolean c = false;

    public c() {
        a(this);
    }

    private Object a(Class cls) {
        for (Object obj : this.b) {
            if (cls.isAssignableFrom(obj.getClass())) {
                return obj;
            }
        }
        return null;
    }

    @Override // com.amazon.android.framework.resource.a
    public final void a() {
        Iterator it = this.b.iterator();
        while (it.hasNext()) {
            b(it.next());
        }
    }

    @Override // com.amazon.android.framework.resource.a
    public final void a(Object obj) {
        if (KiwiLogger.TRACE_ON) {
            a.trace("Registering resource: " + obj);
        }
        com.amazon.android.d.a.a();
        if (this.c) {
            throw new IllegalStateException("Attempt made to register resource after population has begun!");
        }
        com.amazon.android.d.a.a(obj, "resource");
        com.amazon.android.d.a.b(a((Class) obj.getClass()) != null, "Resource already registered: " + obj);
        this.b.add(obj);
    }

    @Override // com.amazon.android.framework.resource.a
    public final void b(Object obj) {
        com.amazon.android.d.a.a(obj, "target");
        if (KiwiLogger.TRACE_ON) {
            a.trace("Populating: " + obj);
        }
        if (!obj.getClass().getName().startsWith("com.amazon.")) {
            a.trace("Ignoring: " + obj + ", not a kiwi class");
            return;
        }
        for (Class<?> superclass = obj.getClass(); superclass != Object.class; superclass = superclass.getSuperclass()) {
            for (Field field : superclass.getDeclaredFields()) {
                if (field.getAnnotation(Resource.class) != null) {
                    Class<?> type = field.getType();
                    Object objA = a((Class) type);
                    com.amazon.android.d.a.a(objA != null, "no resource found for type: " + type);
                    field.setAccessible(true);
                    try {
                        field.set(obj, objA);
                    } catch (Exception e) {
                        throw new com.amazon.android.d.b("Failed to populate field: " + field, e);
                    }
                }
            }
        }
        if (obj instanceof b) {
            ((b) obj).onResourcesPopulated();
        }
    }
}
