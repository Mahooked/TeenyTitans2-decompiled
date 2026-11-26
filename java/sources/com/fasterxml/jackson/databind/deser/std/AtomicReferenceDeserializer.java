package com.fasterxml.jackson.databind.deser.std;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.deser.ContextualDeserializer;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import java.io.IOException;
import java.util.concurrent.atomic.AtomicReference;

/* JADX INFO: loaded from: classes.dex */
public class AtomicReferenceDeserializer extends StdDeserializer<AtomicReference<?>> implements ContextualDeserializer {
    private static final long serialVersionUID = 1;
    protected final JavaType _referencedType;
    protected final JsonDeserializer<?> _valueDeserializer;
    protected final TypeDeserializer _valueTypeDeserializer;

    public AtomicReferenceDeserializer(JavaType referencedType) {
        this(referencedType, null, null);
    }

    public AtomicReferenceDeserializer(JavaType referencedType, TypeDeserializer typeDeser, JsonDeserializer<?> deser) {
        super((Class<?>) AtomicReference.class);
        this._referencedType = referencedType;
        this._valueDeserializer = deser;
        this._valueTypeDeserializer = typeDeser;
    }

    public AtomicReferenceDeserializer withResolved(TypeDeserializer typeDeser, JsonDeserializer<?> valueDeser) {
        return new AtomicReferenceDeserializer(this._referencedType, typeDeser, valueDeser);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public AtomicReference<?> getNullValue(DeserializationContext ctxt) {
        return new AtomicReference<>();
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    @Deprecated
    public AtomicReference<?> getNullValue() {
        return new AtomicReference<>();
    }

    @Override // com.fasterxml.jackson.databind.deser.ContextualDeserializer
    public JsonDeserializer<?> createContextual(DeserializationContext ctxt, BeanProperty property) throws JsonMappingException {
        JsonDeserializer<?> deser = this._valueDeserializer;
        TypeDeserializer typeDeser = this._valueTypeDeserializer;
        if (deser == null) {
            deser = ctxt.findContextualValueDeserializer(this._referencedType, property);
        }
        if (typeDeser != null) {
            typeDeser = typeDeser.forProperty(property);
        }
        return (deser == this._valueDeserializer && typeDeser == this._valueTypeDeserializer) ? this : withResolved(typeDeser, deser);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public AtomicReference<?> deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        return this._valueTypeDeserializer != null ? new AtomicReference<>(this._valueDeserializer.deserializeWithType(jp2, ctxt, this._valueTypeDeserializer)) : new AtomicReference<>(this._valueDeserializer.deserialize(jp2, ctxt));
    }

    @Override // com.fasterxml.jackson.databind.deser.std.StdDeserializer, com.fasterxml.jackson.databind.JsonDeserializer
    public Object[] deserializeWithType(JsonParser jp2, DeserializationContext ctxt, TypeDeserializer typeDeserializer) throws IOException {
        return (Object[]) typeDeserializer.deserializeTypedFromAny(jp2, ctxt);
    }
}
