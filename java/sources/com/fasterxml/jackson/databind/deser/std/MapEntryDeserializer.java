package com.fasterxml.jackson.databind.deser.std;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.KeyDeserializer;
import com.fasterxml.jackson.databind.annotation.JacksonStdImpl;
import com.fasterxml.jackson.databind.deser.ContextualDeserializer;
import com.fasterxml.jackson.databind.deser.ContextualKeyDeserializer;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import java.io.IOException;
import java.util.AbstractMap;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@JacksonStdImpl
public class MapEntryDeserializer extends ContainerDeserializerBase<Map.Entry<Object, Object>> implements ContextualDeserializer {
    private static final long serialVersionUID = 1;
    protected final KeyDeserializer _keyDeserializer;
    protected final JavaType _type;
    protected final JsonDeserializer<Object> _valueDeserializer;
    protected final TypeDeserializer _valueTypeDeserializer;

    public MapEntryDeserializer(JavaType type, KeyDeserializer keyDeser, JsonDeserializer<Object> valueDeser, TypeDeserializer valueTypeDeser) {
        super(type);
        if (type.containedTypeCount() != 2) {
            throw new IllegalArgumentException("Missing generic type information for " + type);
        }
        this._type = type;
        this._keyDeserializer = keyDeser;
        this._valueDeserializer = valueDeser;
        this._valueTypeDeserializer = valueTypeDeser;
    }

    protected MapEntryDeserializer(MapEntryDeserializer src) {
        super(src._type);
        this._type = src._type;
        this._keyDeserializer = src._keyDeserializer;
        this._valueDeserializer = src._valueDeserializer;
        this._valueTypeDeserializer = src._valueTypeDeserializer;
    }

    protected MapEntryDeserializer(MapEntryDeserializer src, KeyDeserializer keyDeser, JsonDeserializer<Object> valueDeser, TypeDeserializer valueTypeDeser) {
        super(src._type);
        this._type = src._type;
        this._keyDeserializer = keyDeser;
        this._valueDeserializer = valueDeser;
        this._valueTypeDeserializer = valueTypeDeser;
    }

    protected MapEntryDeserializer withResolved(KeyDeserializer keyDeser, TypeDeserializer valueTypeDeser, JsonDeserializer<?> valueDeser) {
        return (this._keyDeserializer == keyDeser && this._valueDeserializer == valueDeser && this._valueTypeDeserializer == valueTypeDeser) ? this : new MapEntryDeserializer(this, keyDeser, valueDeser, valueTypeDeser);
    }

    /* JADX WARN: Multi-variable type inference failed */
    /* JADX WARN: Type inference failed for: r1v3, types: [com.fasterxml.jackson.databind.KeyDeserializer] */
    /* JADX WARN: Type inference failed for: r1v5 */
    /* JADX WARN: Type inference failed for: r1v6 */
    /* JADX WARN: Type inference failed for: r1v7 */
    /* JADX WARN: Type inference failed for: r6v0, types: [com.fasterxml.jackson.databind.deser.std.MapEntryDeserializer] */
    @Override // com.fasterxml.jackson.databind.deser.ContextualDeserializer
    public JsonDeserializer<?> createContextual(DeserializationContext deserializationContext, BeanProperty beanProperty) throws JsonMappingException {
        ?? CreateContextual;
        JsonDeserializer<?> jsonDeserializerHandleSecondaryContextualization;
        Object obj = this._keyDeserializer;
        if (obj == null) {
            CreateContextual = deserializationContext.findKeyDeserializer(this._type.containedType(0), beanProperty);
        } else {
            boolean z = obj instanceof ContextualKeyDeserializer;
            CreateContextual = obj;
            if (z) {
                CreateContextual = ((ContextualKeyDeserializer) obj).createContextual(deserializationContext, beanProperty);
            }
        }
        JsonDeserializer<?> jsonDeserializerFindConvertingContentDeserializer = findConvertingContentDeserializer(deserializationContext, beanProperty, this._valueDeserializer);
        JavaType javaTypeContainedType = this._type.containedType(1);
        if (jsonDeserializerFindConvertingContentDeserializer == null) {
            jsonDeserializerHandleSecondaryContextualization = deserializationContext.findContextualValueDeserializer(javaTypeContainedType, beanProperty);
        } else {
            jsonDeserializerHandleSecondaryContextualization = deserializationContext.handleSecondaryContextualization(jsonDeserializerFindConvertingContentDeserializer, beanProperty, javaTypeContainedType);
        }
        TypeDeserializer typeDeserializerForProperty = this._valueTypeDeserializer;
        if (typeDeserializerForProperty != null) {
            typeDeserializerForProperty = typeDeserializerForProperty.forProperty(beanProperty);
        }
        return withResolved(CreateContextual, typeDeserializerForProperty, jsonDeserializerHandleSecondaryContextualization);
    }

    @Override // com.fasterxml.jackson.databind.deser.std.ContainerDeserializerBase
    public JavaType getContentType() {
        return this._type.containedType(1);
    }

    @Override // com.fasterxml.jackson.databind.deser.std.ContainerDeserializerBase
    public JsonDeserializer<Object> getContentDeserializer() {
        return this._valueDeserializer;
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Map.Entry<Object, Object> deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        JsonToken t = jp2.getCurrentToken();
        if (t != JsonToken.START_OBJECT && t != JsonToken.FIELD_NAME && t != JsonToken.END_OBJECT) {
            return _deserializeFromEmpty(jp2, ctxt);
        }
        if (t == JsonToken.START_OBJECT) {
            t = jp2.nextToken();
        }
        if (t != JsonToken.FIELD_NAME) {
            if (t == JsonToken.END_OBJECT) {
                throw ctxt.mappingException("Can not deserialize a Map.Entry out of empty JSON Object");
            }
            throw ctxt.mappingException(handledType(), t);
        }
        KeyDeserializer keyDes = this._keyDeserializer;
        JsonDeserializer<Object> valueDes = this._valueDeserializer;
        TypeDeserializer typeDeser = this._valueTypeDeserializer;
        String keyStr = jp2.getCurrentName();
        Object key = keyDes.deserializeKey(keyStr, ctxt);
        Object value = null;
        try {
            if (jp2.nextToken() == JsonToken.VALUE_NULL) {
                value = valueDes.getNullValue(ctxt);
            } else if (typeDeser == null) {
                value = valueDes.deserialize(jp2, ctxt);
            } else {
                value = valueDes.deserializeWithType(jp2, ctxt, typeDeser);
            }
        } catch (Exception e) {
            wrapAndThrow(e, Map.Entry.class, keyStr);
        }
        JsonToken t2 = jp2.nextToken();
        if (t2 != JsonToken.END_OBJECT) {
            if (t2 == JsonToken.FIELD_NAME) {
                throw ctxt.mappingException("Problem binding JSON into Map.Entry: more than one entry in JSON (second field: '" + jp2.getCurrentName() + "')");
            }
            throw ctxt.mappingException("Problem binding JSON into Map.Entry: unexpected content after JSON Object entry: " + t2);
        }
        return new AbstractMap.SimpleEntry(key, value);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Map.Entry<Object, Object> deserialize(JsonParser jp2, DeserializationContext ctxt, Map.Entry<Object, Object> result) throws IOException {
        throw new IllegalStateException("Can not update Map.Entry values");
    }

    @Override // com.fasterxml.jackson.databind.deser.std.StdDeserializer, com.fasterxml.jackson.databind.JsonDeserializer
    public Object deserializeWithType(JsonParser jp2, DeserializationContext ctxt, TypeDeserializer typeDeserializer) throws IOException {
        return typeDeserializer.deserializeTypedFromObject(jp2, ctxt);
    }

    @Override // com.fasterxml.jackson.databind.deser.std.StdDeserializer
    public JavaType getValueType() {
        return this._type;
    }
}
