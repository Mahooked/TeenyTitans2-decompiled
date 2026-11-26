package com.fasterxml.jackson.databind.jsontype.impl;

import com.fasterxml.jackson.annotation.JsonTypeInfo;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.core.util.JsonParserSequence;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import com.fasterxml.jackson.databind.jsontype.TypeIdResolver;
import com.fasterxml.jackson.databind.util.TokenBuffer;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public class AsPropertyTypeDeserializer extends AsArrayTypeDeserializer {
    private static final long serialVersionUID = 1;
    protected final JsonTypeInfo.As _inclusion;

    public AsPropertyTypeDeserializer(JavaType bt, TypeIdResolver idRes, String typePropertyName, boolean typeIdVisible, Class<?> defaultImpl) {
        this(bt, idRes, typePropertyName, typeIdVisible, defaultImpl, JsonTypeInfo.As.PROPERTY);
    }

    public AsPropertyTypeDeserializer(JavaType bt, TypeIdResolver idRes, String typePropertyName, boolean typeIdVisible, Class<?> defaultImpl, JsonTypeInfo.As inclusion) {
        super(bt, idRes, typePropertyName, typeIdVisible, defaultImpl);
        this._inclusion = inclusion;
    }

    public AsPropertyTypeDeserializer(AsPropertyTypeDeserializer src, BeanProperty property) {
        super(src, property);
        this._inclusion = src._inclusion;
    }

    @Override // com.fasterxml.jackson.databind.jsontype.impl.AsArrayTypeDeserializer, com.fasterxml.jackson.databind.jsontype.impl.TypeDeserializerBase, com.fasterxml.jackson.databind.jsontype.TypeDeserializer
    public TypeDeserializer forProperty(BeanProperty prop) {
        return prop == this._property ? this : new AsPropertyTypeDeserializer(this, prop);
    }

    @Override // com.fasterxml.jackson.databind.jsontype.impl.AsArrayTypeDeserializer, com.fasterxml.jackson.databind.jsontype.impl.TypeDeserializerBase, com.fasterxml.jackson.databind.jsontype.TypeDeserializer
    public JsonTypeInfo.As getTypeInclusion() {
        return this._inclusion;
    }

    @Override // com.fasterxml.jackson.databind.jsontype.impl.AsArrayTypeDeserializer, com.fasterxml.jackson.databind.jsontype.TypeDeserializer
    public Object deserializeTypedFromObject(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        Object typeId;
        if (jp2.canReadTypeId() && (typeId = jp2.getTypeId()) != null) {
            return _deserializeWithNativeTypeId(jp2, ctxt, typeId);
        }
        JsonToken t = jp2.getCurrentToken();
        if (t == JsonToken.START_OBJECT) {
            t = jp2.nextToken();
        } else {
            if (t == JsonToken.START_ARRAY) {
                return _deserializeTypedUsingDefaultImpl(jp2, ctxt, null);
            }
            if (t != JsonToken.FIELD_NAME) {
                return _deserializeTypedUsingDefaultImpl(jp2, ctxt, null);
            }
        }
        TokenBuffer tb = null;
        while (t == JsonToken.FIELD_NAME) {
            String name = jp2.getCurrentName();
            jp2.nextToken();
            if (this._typePropertyName.equals(name)) {
                return _deserializeTypedForId(jp2, ctxt, tb);
            }
            if (tb == null) {
                tb = new TokenBuffer(jp2, ctxt);
            }
            tb.writeFieldName(name);
            tb.copyCurrentStructure(jp2);
            t = jp2.nextToken();
        }
        return _deserializeTypedUsingDefaultImpl(jp2, ctxt, tb);
    }

    protected Object _deserializeTypedForId(JsonParser jp2, DeserializationContext ctxt, TokenBuffer tb) throws IOException {
        String typeId = jp2.getText();
        JsonDeserializer<Object> deser = _findDeserializer(ctxt, typeId);
        if (this._typeIdVisible) {
            if (tb == null) {
                tb = new TokenBuffer(jp2, ctxt);
            }
            tb.writeFieldName(jp2.getCurrentName());
            tb.writeString(typeId);
        }
        if (tb != null) {
            jp2 = JsonParserSequence.createFlattened(tb.asParser(jp2), jp2);
        }
        jp2.nextToken();
        return deser.deserialize(jp2, ctxt);
    }

    protected Object _deserializeTypedUsingDefaultImpl(JsonParser jp2, DeserializationContext ctxt, TokenBuffer tb) throws IOException {
        JsonDeserializer<Object> deser = _findDefaultImplDeserializer(ctxt);
        if (deser != null) {
            if (tb != null) {
                tb.writeEndObject();
                jp2 = tb.asParser(jp2);
                jp2.nextToken();
            }
            return deser.deserialize(jp2, ctxt);
        }
        Object result = TypeDeserializer.deserializeIfNatural(jp2, ctxt, this._baseType);
        if (result == null) {
            if (jp2.getCurrentToken() == JsonToken.START_ARRAY) {
                return super.deserializeTypedFromAny(jp2, ctxt);
            }
            throw ctxt.wrongTokenException(jp2, JsonToken.FIELD_NAME, "missing property '" + this._typePropertyName + "' that is to contain type id  (for class " + baseTypeName() + ")");
        }
        return result;
    }

    @Override // com.fasterxml.jackson.databind.jsontype.impl.AsArrayTypeDeserializer, com.fasterxml.jackson.databind.jsontype.TypeDeserializer
    public Object deserializeTypedFromAny(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        return jp2.getCurrentToken() == JsonToken.START_ARRAY ? super.deserializeTypedFromArray(jp2, ctxt) : deserializeTypedFromObject(jp2, ctxt);
    }
}
