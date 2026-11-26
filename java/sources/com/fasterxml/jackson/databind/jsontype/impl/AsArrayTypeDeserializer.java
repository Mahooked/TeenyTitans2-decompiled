package com.fasterxml.jackson.databind.jsontype.impl;

import com.fasterxml.jackson.annotation.JsonTypeInfo;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.core.ObjectCodec;
import com.fasterxml.jackson.core.util.JsonParserSequence;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import com.fasterxml.jackson.databind.jsontype.TypeIdResolver;
import com.fasterxml.jackson.databind.util.TokenBuffer;
import java.io.IOException;
import java.io.Serializable;

/* JADX INFO: loaded from: classes.dex */
public class AsArrayTypeDeserializer extends TypeDeserializerBase implements Serializable {
    private static final long serialVersionUID = 1;

    public AsArrayTypeDeserializer(JavaType bt, TypeIdResolver idRes, String typePropertyName, boolean typeIdVisible, Class<?> defaultImpl) {
        super(bt, idRes, typePropertyName, typeIdVisible, defaultImpl);
    }

    public AsArrayTypeDeserializer(AsArrayTypeDeserializer src, BeanProperty property) {
        super(src, property);
    }

    @Override // com.fasterxml.jackson.databind.jsontype.impl.TypeDeserializerBase, com.fasterxml.jackson.databind.jsontype.TypeDeserializer
    public TypeDeserializer forProperty(BeanProperty prop) {
        return prop == this._property ? this : new AsArrayTypeDeserializer(this, prop);
    }

    @Override // com.fasterxml.jackson.databind.jsontype.impl.TypeDeserializerBase, com.fasterxml.jackson.databind.jsontype.TypeDeserializer
    public JsonTypeInfo.As getTypeInclusion() {
        return JsonTypeInfo.As.WRAPPER_ARRAY;
    }

    @Override // com.fasterxml.jackson.databind.jsontype.TypeDeserializer
    public Object deserializeTypedFromArray(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        return _deserialize(jp2, ctxt);
    }

    @Override // com.fasterxml.jackson.databind.jsontype.TypeDeserializer
    public Object deserializeTypedFromObject(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        return _deserialize(jp2, ctxt);
    }

    @Override // com.fasterxml.jackson.databind.jsontype.TypeDeserializer
    public Object deserializeTypedFromScalar(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        return _deserialize(jp2, ctxt);
    }

    @Override // com.fasterxml.jackson.databind.jsontype.TypeDeserializer
    public Object deserializeTypedFromAny(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        return _deserialize(jp2, ctxt);
    }

    protected Object _deserialize(JsonParser p, DeserializationContext ctxt) throws IOException {
        Object typeId;
        if (p.canReadTypeId() && (typeId = p.getTypeId()) != null) {
            return _deserializeWithNativeTypeId(p, ctxt, typeId);
        }
        boolean hadStartArray = p.isExpectedStartArrayToken();
        String typeId2 = _locateTypeId(p, ctxt);
        JsonDeserializer<Object> deser = _findDeserializer(ctxt, typeId2);
        if (this._typeIdVisible && !_usesExternalId() && p.getCurrentToken() == JsonToken.START_OBJECT) {
            TokenBuffer tb = new TokenBuffer((ObjectCodec) null, false);
            tb.writeStartObject();
            tb.writeFieldName(this._typePropertyName);
            tb.writeString(typeId2);
            p = JsonParserSequence.createFlattened(tb.asParser(p), p);
            p.nextToken();
        }
        Object objDeserialize = deser.deserialize(p, ctxt);
        if (hadStartArray && p.nextToken() != JsonToken.END_ARRAY) {
            throw ctxt.wrongTokenException(p, JsonToken.END_ARRAY, "expected closing END_ARRAY after type information and deserialized value");
        }
        return objDeserialize;
    }

    protected String _locateTypeId(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        if (!jp2.isExpectedStartArrayToken()) {
            if (this._defaultImpl != null) {
                return this._idResolver.idFromBaseType();
            }
            throw ctxt.wrongTokenException(jp2, JsonToken.START_ARRAY, "need JSON Array to contain As.WRAPPER_ARRAY type information for class " + baseTypeName());
        }
        JsonToken t = jp2.nextToken();
        if (t == JsonToken.VALUE_STRING) {
            String text = jp2.getText();
            jp2.nextToken();
            return text;
        }
        if (this._defaultImpl != null) {
            return this._idResolver.idFromBaseType();
        }
        throw ctxt.wrongTokenException(jp2, JsonToken.VALUE_STRING, "need JSON String that contains type id (for subtype of " + baseTypeName() + ")");
    }

    protected boolean _usesExternalId() {
        return false;
    }
}
