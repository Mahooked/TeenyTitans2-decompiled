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
public class AsWrapperTypeDeserializer extends TypeDeserializerBase implements Serializable {
    private static final long serialVersionUID = 1;

    public AsWrapperTypeDeserializer(JavaType bt, TypeIdResolver idRes, String typePropertyName, boolean typeIdVisible, Class<?> defaultImpl) {
        super(bt, idRes, typePropertyName, typeIdVisible, defaultImpl);
    }

    protected AsWrapperTypeDeserializer(AsWrapperTypeDeserializer src, BeanProperty property) {
        super(src, property);
    }

    @Override // com.fasterxml.jackson.databind.jsontype.impl.TypeDeserializerBase, com.fasterxml.jackson.databind.jsontype.TypeDeserializer
    public TypeDeserializer forProperty(BeanProperty prop) {
        return prop == this._property ? this : new AsWrapperTypeDeserializer(this, prop);
    }

    @Override // com.fasterxml.jackson.databind.jsontype.impl.TypeDeserializerBase, com.fasterxml.jackson.databind.jsontype.TypeDeserializer
    public JsonTypeInfo.As getTypeInclusion() {
        return JsonTypeInfo.As.WRAPPER_OBJECT;
    }

    @Override // com.fasterxml.jackson.databind.jsontype.TypeDeserializer
    public Object deserializeTypedFromObject(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        return _deserialize(jp2, ctxt);
    }

    @Override // com.fasterxml.jackson.databind.jsontype.TypeDeserializer
    public Object deserializeTypedFromArray(JsonParser jp2, DeserializationContext ctxt) throws IOException {
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

    protected Object _deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        Object typeId;
        if (jp2.canReadTypeId() && (typeId = jp2.getTypeId()) != null) {
            return _deserializeWithNativeTypeId(jp2, ctxt, typeId);
        }
        if (jp2.getCurrentToken() != JsonToken.START_OBJECT) {
            throw ctxt.wrongTokenException(jp2, JsonToken.START_OBJECT, "need JSON Object to contain As.WRAPPER_OBJECT type information for class " + baseTypeName());
        }
        if (jp2.nextToken() != JsonToken.FIELD_NAME) {
            throw ctxt.wrongTokenException(jp2, JsonToken.FIELD_NAME, "need JSON String that contains type id (for subtype of " + baseTypeName() + ")");
        }
        String typeId2 = jp2.getText();
        JsonDeserializer<Object> deser = _findDeserializer(ctxt, typeId2);
        jp2.nextToken();
        if (this._typeIdVisible && jp2.getCurrentToken() == JsonToken.START_OBJECT) {
            TokenBuffer tb = new TokenBuffer((ObjectCodec) null, false);
            tb.writeStartObject();
            tb.writeFieldName(this._typePropertyName);
            tb.writeString(typeId2);
            jp2 = JsonParserSequence.createFlattened(tb.asParser(jp2), jp2);
            jp2.nextToken();
        }
        Object objDeserialize = deser.deserialize(jp2, ctxt);
        if (jp2.nextToken() != JsonToken.END_OBJECT) {
            throw ctxt.wrongTokenException(jp2, JsonToken.END_OBJECT, "expected closing END_OBJECT after type information and deserialized value");
        }
        return objDeserialize;
    }
}
