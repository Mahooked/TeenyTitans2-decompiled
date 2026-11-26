package com.fasterxml.jackson.databind.deser.impl;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import java.io.IOException;
import java.io.Serializable;

/* JADX INFO: loaded from: classes.dex */
public final class TypeWrappedDeserializer extends JsonDeserializer<Object> implements Serializable {
    private static final long serialVersionUID = 1;
    protected final JsonDeserializer<Object> _deserializer;
    protected final TypeDeserializer _typeDeserializer;

    public TypeWrappedDeserializer(TypeDeserializer typeDeser, JsonDeserializer<?> deser) {
        this._typeDeserializer = typeDeser;
        this._deserializer = deser;
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Class<?> handledType() {
        return this._deserializer.handledType();
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Object deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        return this._deserializer.deserializeWithType(jp2, ctxt, this._typeDeserializer);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Object deserializeWithType(JsonParser jp2, DeserializationContext ctxt, TypeDeserializer typeDeserializer) throws IOException {
        throw new IllegalStateException("Type-wrapped deserializer's deserializeWithType should never get called");
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Object deserialize(JsonParser jp2, DeserializationContext ctxt, Object intoValue) throws IOException {
        return this._deserializer.deserialize(jp2, ctxt, intoValue);
    }
}
