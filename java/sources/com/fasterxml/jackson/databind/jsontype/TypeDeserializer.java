package com.fasterxml.jackson.databind.jsontype;

import com.fasterxml.jackson.annotation.JsonTypeInfo;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JavaType;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public abstract class TypeDeserializer {
    public abstract Object deserializeTypedFromAny(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException;

    public abstract Object deserializeTypedFromArray(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException;

    public abstract Object deserializeTypedFromObject(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException;

    public abstract Object deserializeTypedFromScalar(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException;

    public abstract TypeDeserializer forProperty(BeanProperty beanProperty);

    public abstract Class<?> getDefaultImpl();

    public abstract String getPropertyName();

    public abstract TypeIdResolver getTypeIdResolver();

    public abstract JsonTypeInfo.As getTypeInclusion();

    public static Object deserializeIfNatural(JsonParser jp2, DeserializationContext ctxt, JavaType baseType) throws IOException {
        return deserializeIfNatural(jp2, ctxt, baseType.getRawClass());
    }

    public static Object deserializeIfNatural(JsonParser jp2, DeserializationContext ctxt, Class<?> base) throws IOException {
        JsonToken t = jp2.getCurrentToken();
        if (t == null) {
            return null;
        }
        switch (t) {
            case VALUE_STRING:
                if (base.isAssignableFrom(String.class)) {
                    return jp2.getText();
                }
                return null;
            case VALUE_NUMBER_INT:
                if (base.isAssignableFrom(Integer.class)) {
                    return Integer.valueOf(jp2.getIntValue());
                }
                return null;
            case VALUE_NUMBER_FLOAT:
                if (base.isAssignableFrom(Double.class)) {
                    return Double.valueOf(jp2.getDoubleValue());
                }
                return null;
            case VALUE_TRUE:
                if (base.isAssignableFrom(Boolean.class)) {
                    return Boolean.TRUE;
                }
                return null;
            case VALUE_FALSE:
                if (base.isAssignableFrom(Boolean.class)) {
                    return Boolean.FALSE;
                }
                return null;
            default:
                return null;
        }
    }
}
