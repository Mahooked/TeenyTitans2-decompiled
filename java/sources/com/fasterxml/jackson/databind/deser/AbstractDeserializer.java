package com.fasterxml.jackson.databind.deser;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.BeanDescription;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.deser.impl.ObjectIdReader;
import com.fasterxml.jackson.databind.deser.impl.ReadableObjectId;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import java.io.IOException;
import java.io.Serializable;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public class AbstractDeserializer extends JsonDeserializer<Object> implements Serializable {
    private static final long serialVersionUID = 1;
    protected final boolean _acceptBoolean;
    protected final boolean _acceptDouble;
    protected final boolean _acceptInt;
    protected final boolean _acceptString;
    protected final Map<String, SettableBeanProperty> _backRefProperties;
    protected final JavaType _baseType;
    protected final ObjectIdReader _objectIdReader;

    public AbstractDeserializer(BeanDeserializerBuilder builder, BeanDescription beanDesc, Map<String, SettableBeanProperty> backRefProps) {
        this._baseType = beanDesc.getType();
        this._objectIdReader = builder.getObjectIdReader();
        this._backRefProperties = backRefProps;
        Class<?> cls = this._baseType.getRawClass();
        this._acceptString = cls.isAssignableFrom(String.class);
        this._acceptBoolean = cls == Boolean.TYPE || cls.isAssignableFrom(Boolean.class);
        this._acceptInt = cls == Integer.TYPE || cls.isAssignableFrom(Integer.class);
        this._acceptDouble = cls == Double.TYPE || cls.isAssignableFrom(Double.class);
    }

    protected AbstractDeserializer(BeanDescription beanDesc) {
        this._baseType = beanDesc.getType();
        this._objectIdReader = null;
        this._backRefProperties = null;
        Class<?> cls = this._baseType.getRawClass();
        this._acceptString = cls.isAssignableFrom(String.class);
        this._acceptBoolean = cls == Boolean.TYPE || cls.isAssignableFrom(Boolean.class);
        this._acceptInt = cls == Integer.TYPE || cls.isAssignableFrom(Integer.class);
        this._acceptDouble = cls == Double.TYPE || cls.isAssignableFrom(Double.class);
    }

    public static AbstractDeserializer constructForNonPOJO(BeanDescription beanDesc) {
        return new AbstractDeserializer(beanDesc);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Class<?> handledType() {
        return this._baseType.getRawClass();
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public boolean isCachable() {
        return true;
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public ObjectIdReader getObjectIdReader() {
        return this._objectIdReader;
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public SettableBeanProperty findBackReference(String logicalName) {
        if (this._backRefProperties == null) {
            return null;
        }
        return this._backRefProperties.get(logicalName);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Object deserializeWithType(JsonParser jp2, DeserializationContext ctxt, TypeDeserializer typeDeserializer) throws IOException {
        JsonToken t;
        if (this._objectIdReader != null && (t = jp2.getCurrentToken()) != null && t.isScalarValue()) {
            return _deserializeFromObjectId(jp2, ctxt);
        }
        Object result = _deserializeIfNatural(jp2, ctxt);
        return result == null ? typeDeserializer.deserializeTypedFromObject(jp2, ctxt) : result;
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Object deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        throw ctxt.instantiationException(this._baseType.getRawClass(), "abstract types either need to be mapped to concrete types, have custom deserializer, or be instantiated with additional type information");
    }

    /* JADX WARN: Can't fix incorrect switch cases order, some code will duplicate */
    protected Object _deserializeIfNatural(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        switch (jp2.getCurrentTokenId()) {
            case 6:
                if (this._acceptString) {
                    return jp2.getText();
                }
                return null;
            case 7:
                if (this._acceptInt) {
                    return Integer.valueOf(jp2.getIntValue());
                }
                return null;
            case 8:
                if (this._acceptDouble) {
                    return Double.valueOf(jp2.getDoubleValue());
                }
                return null;
            case 9:
                if (this._acceptBoolean) {
                    return Boolean.TRUE;
                }
                return null;
            case 10:
                if (this._acceptBoolean) {
                    return Boolean.FALSE;
                }
                return null;
            default:
                return null;
        }
    }

    protected Object _deserializeFromObjectId(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        Object id = this._objectIdReader.readObjectReference(jp2, ctxt);
        ReadableObjectId roid = ctxt.findObjectId(id, this._objectIdReader.generator, this._objectIdReader.resolver);
        Object pojo = roid.resolve();
        if (pojo == null) {
            throw new UnresolvedForwardReference("Could not resolve Object Id [" + id + "] -- unresolved forward-reference?", jp2.getCurrentLocation(), roid);
        }
        return pojo;
    }
}
