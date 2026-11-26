package com.fasterxml.jackson.databind.deser.impl;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JsonDeserializer;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public class NoClassDefFoundDeserializer<T> extends JsonDeserializer<T> {
    private final NoClassDefFoundError _cause;

    public NoClassDefFoundDeserializer(NoClassDefFoundError cause) {
        this._cause = cause;
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public T deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        throw this._cause;
    }
}
