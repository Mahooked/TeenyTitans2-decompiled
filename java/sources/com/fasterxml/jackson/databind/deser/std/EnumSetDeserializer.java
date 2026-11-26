package com.fasterxml.jackson.databind.deser.std;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.deser.ContextualDeserializer;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import java.io.IOException;
import java.util.EnumSet;

/* JADX INFO: loaded from: classes.dex */
public class EnumSetDeserializer extends StdDeserializer<EnumSet<?>> implements ContextualDeserializer {
    private static final long serialVersionUID = 1;
    protected final Class<Enum> _enumClass;
    protected JsonDeserializer<Enum<?>> _enumDeserializer;
    protected final JavaType _enumType;

    /* JADX WARN: Multi-variable type inference failed */
    public EnumSetDeserializer(JavaType enumType, JsonDeserializer<?> jsonDeserializer) {
        super((Class<?>) EnumSet.class);
        this._enumType = enumType;
        this._enumClass = enumType.getRawClass();
        if (!this._enumClass.isEnum()) {
            throw new IllegalArgumentException("Type " + enumType + " not Java Enum type");
        }
        this._enumDeserializer = jsonDeserializer;
    }

    public EnumSetDeserializer withDeserializer(JsonDeserializer<?> deser) {
        return this._enumDeserializer == deser ? this : new EnumSetDeserializer(this._enumType, deser);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public boolean isCachable() {
        return this._enumType.getValueHandler() == null;
    }

    @Override // com.fasterxml.jackson.databind.deser.ContextualDeserializer
    public JsonDeserializer<?> createContextual(DeserializationContext ctxt, BeanProperty property) throws JsonMappingException {
        JsonDeserializer<?> deser;
        JsonDeserializer<?> deser2 = this._enumDeserializer;
        if (deser2 == null) {
            deser = ctxt.findContextualValueDeserializer(this._enumType, property);
        } else {
            deser = ctxt.handleSecondaryContextualization(deser2, property, this._enumType);
        }
        return withDeserializer(deser);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public EnumSet<?> deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        if (!jp2.isExpectedStartArrayToken()) {
            throw ctxt.mappingException(EnumSet.class);
        }
        EnumSet<?> enumSetConstructSet = constructSet();
        while (true) {
            try {
                JsonToken t = jp2.nextToken();
                if (t != JsonToken.END_ARRAY) {
                    if (t == JsonToken.VALUE_NULL) {
                        throw ctxt.mappingException(this._enumClass);
                    }
                    Enum<?> value = this._enumDeserializer.deserialize(jp2, ctxt);
                    if (value != null) {
                        enumSetConstructSet.add(value);
                    }
                } else {
                    return enumSetConstructSet;
                }
            } catch (Exception e) {
                throw JsonMappingException.wrapWithPath(e, enumSetConstructSet, enumSetConstructSet.size());
            }
        }
    }

    @Override // com.fasterxml.jackson.databind.deser.std.StdDeserializer, com.fasterxml.jackson.databind.JsonDeserializer
    public Object deserializeWithType(JsonParser jp2, DeserializationContext ctxt, TypeDeserializer typeDeserializer) throws IOException {
        return typeDeserializer.deserializeTypedFromArray(jp2, ctxt);
    }

    private EnumSet constructSet() {
        return EnumSet.noneOf(this._enumClass);
    }
}
