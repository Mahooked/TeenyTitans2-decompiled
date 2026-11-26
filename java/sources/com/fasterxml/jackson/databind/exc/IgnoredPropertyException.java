package com.fasterxml.jackson.databind.exc;

import com.fasterxml.jackson.core.JsonLocation;
import com.fasterxml.jackson.core.JsonParser;
import java.util.Collection;

/* JADX INFO: loaded from: classes.dex */
public class IgnoredPropertyException extends PropertyBindingException {
    private static final long serialVersionUID = 1;

    public IgnoredPropertyException(String msg, JsonLocation loc, Class<?> referringClass, String propName, Collection<Object> propertyIds) {
        super(msg, loc, referringClass, propName, propertyIds);
    }

    public static IgnoredPropertyException from(JsonParser jp2, Object fromObjectOrClass, String propertyName, Collection<Object> propertyIds) {
        Class<?> ref;
        if (fromObjectOrClass == null) {
            throw new IllegalArgumentException();
        }
        if (fromObjectOrClass instanceof Class) {
            ref = (Class) fromObjectOrClass;
        } else {
            ref = fromObjectOrClass.getClass();
        }
        String msg = "Ignored field \"" + propertyName + "\" (class " + ref.getName() + ") encountered; mapper configured not to allow this";
        IgnoredPropertyException e = new IgnoredPropertyException(msg, jp2.getCurrentLocation(), ref, propertyName, propertyIds);
        e.prependPath(fromObjectOrClass, propertyName);
        return e;
    }
}
