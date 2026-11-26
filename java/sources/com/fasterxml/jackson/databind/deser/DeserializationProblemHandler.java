package com.fasterxml.jackson.databind.deser;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JsonDeserializer;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public abstract class DeserializationProblemHandler {
    public boolean handleUnknownProperty(DeserializationContext ctxt, JsonParser jp2, JsonDeserializer<?> deserializer, Object beanOrClass, String propertyName) throws IOException {
        return false;
    }
}
