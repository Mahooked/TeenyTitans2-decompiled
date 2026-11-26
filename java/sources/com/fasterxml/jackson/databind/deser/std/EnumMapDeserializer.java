package com.fasterxml.jackson.databind.deser.std;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.KeyDeserializer;
import com.fasterxml.jackson.databind.deser.ContextualDeserializer;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import java.io.IOException;
import java.util.EnumMap;

/* JADX INFO: loaded from: classes.dex */
public class EnumMapDeserializer extends ContainerDeserializerBase<EnumMap<?, ?>> implements ContextualDeserializer {
    private static final long serialVersionUID = 1;
    protected final Class<?> _enumClass;
    protected KeyDeserializer _keyDeserializer;
    protected final JavaType _mapType;
    protected JsonDeserializer<Object> _valueDeserializer;
    protected final TypeDeserializer _valueTypeDeserializer;

    public EnumMapDeserializer(JavaType mapType, KeyDeserializer keyDeserializer, JsonDeserializer<?> valueDeser, TypeDeserializer valueTypeDeser) {
        super(mapType);
        this._mapType = mapType;
        this._enumClass = mapType.getKeyType().getRawClass();
        this._keyDeserializer = keyDeserializer;
        this._valueDeserializer = valueDeser;
        this._valueTypeDeserializer = valueTypeDeser;
    }

    public EnumMapDeserializer withResolved(KeyDeserializer keyDeserializer, JsonDeserializer<?> valueDeserializer, TypeDeserializer valueTypeDeser) {
        return (keyDeserializer == this._keyDeserializer && valueDeserializer == this._valueDeserializer && valueTypeDeser == this._valueTypeDeserializer) ? this : new EnumMapDeserializer(this._mapType, keyDeserializer, valueDeserializer, this._valueTypeDeserializer);
    }

    @Override // com.fasterxml.jackson.databind.deser.ContextualDeserializer
    public JsonDeserializer<?> createContextual(DeserializationContext ctxt, BeanProperty property) throws JsonMappingException {
        JsonDeserializer<?> vd;
        KeyDeserializer kd = this._keyDeserializer;
        if (kd == null) {
            kd = ctxt.findKeyDeserializer(this._mapType.getKeyType(), property);
        }
        JsonDeserializer<?> vd2 = this._valueDeserializer;
        JavaType vt = this._mapType.getContentType();
        if (vd2 == null) {
            vd = ctxt.findContextualValueDeserializer(vt, property);
        } else {
            vd = ctxt.handleSecondaryContextualization(vd2, property, vt);
        }
        TypeDeserializer vtd = this._valueTypeDeserializer;
        if (vtd != null) {
            vtd = vtd.forProperty(property);
        }
        return withResolved(kd, vd, vtd);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public boolean isCachable() {
        return this._valueDeserializer == null && this._keyDeserializer == null && this._valueTypeDeserializer == null;
    }

    @Override // com.fasterxml.jackson.databind.deser.std.ContainerDeserializerBase
    public JavaType getContentType() {
        return this._mapType.getContentType();
    }

    @Override // com.fasterxml.jackson.databind.deser.std.ContainerDeserializerBase
    public JsonDeserializer<Object> getContentDeserializer() {
        return this._valueDeserializer;
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public EnumMap<?, ?> deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        Object value;
        if (jp2.getCurrentToken() != JsonToken.START_OBJECT) {
            return _deserializeFromEmpty(jp2, ctxt);
        }
        EnumMap<?, ?> enumMapConstructMap = constructMap();
        JsonDeserializer<Object> valueDes = this._valueDeserializer;
        TypeDeserializer typeDeser = this._valueTypeDeserializer;
        while (jp2.nextToken() == JsonToken.FIELD_NAME) {
            String keyName = jp2.getCurrentName();
            Enum<?> key = (Enum) this._keyDeserializer.deserializeKey(keyName, ctxt);
            if (key == null) {
                if (!ctxt.isEnabled(DeserializationFeature.READ_UNKNOWN_ENUM_VALUES_AS_NULL)) {
                    throw ctxt.weirdStringException(keyName, this._enumClass, "value not one of declared Enum instance names for " + this._mapType.getKeyType());
                }
                jp2.nextToken();
                jp2.skipChildren();
            } else {
                JsonToken t = jp2.nextToken();
                try {
                    if (t == JsonToken.VALUE_NULL) {
                        value = valueDes.getNullValue(ctxt);
                    } else if (typeDeser == null) {
                        value = valueDes.deserialize(jp2, ctxt);
                    } else {
                        value = valueDes.deserializeWithType(jp2, ctxt, typeDeser);
                    }
                    enumMapConstructMap.put(key, value);
                } catch (Exception e) {
                    wrapAndThrow(e, enumMapConstructMap, keyName);
                    return null;
                }
            }
        }
        return enumMapConstructMap;
    }

    @Override // com.fasterxml.jackson.databind.deser.std.StdDeserializer, com.fasterxml.jackson.databind.JsonDeserializer
    public Object deserializeWithType(JsonParser jp2, DeserializationContext ctxt, TypeDeserializer typeDeserializer) throws IOException {
        return typeDeserializer.deserializeTypedFromObject(jp2, ctxt);
    }

    protected EnumMap<?, ?> constructMap() {
        return new EnumMap<>(this._enumClass);
    }
}
