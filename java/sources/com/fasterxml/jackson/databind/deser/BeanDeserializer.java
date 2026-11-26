package com.fasterxml.jackson.databind.deser;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.BeanDescription;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.deser.impl.BeanAsArrayDeserializer;
import com.fasterxml.jackson.databind.deser.impl.BeanPropertyMap;
import com.fasterxml.jackson.databind.deser.impl.ExternalTypeHandler;
import com.fasterxml.jackson.databind.deser.impl.ObjectIdReader;
import com.fasterxml.jackson.databind.deser.impl.PropertyBasedCreator;
import com.fasterxml.jackson.databind.deser.impl.PropertyValueBuffer;
import com.fasterxml.jackson.databind.util.NameTransformer;
import com.fasterxml.jackson.databind.util.TokenBuffer;
import java.io.IOException;
import java.io.Serializable;
import java.util.HashSet;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public class BeanDeserializer extends BeanDeserializerBase implements Serializable {
    private static final long serialVersionUID = 1;

    @Override // com.fasterxml.jackson.databind.deser.BeanDeserializerBase
    public /* bridge */ /* synthetic */ BeanDeserializerBase withIgnorableProperties(HashSet x0) {
        return withIgnorableProperties((HashSet<String>) x0);
    }

    public BeanDeserializer(BeanDeserializerBuilder builder, BeanDescription beanDesc, BeanPropertyMap properties, Map<String, SettableBeanProperty> backRefs, HashSet<String> ignorableProps, boolean ignoreAllUnknown, boolean hasViews) {
        super(builder, beanDesc, properties, backRefs, ignorableProps, ignoreAllUnknown, hasViews);
    }

    protected BeanDeserializer(BeanDeserializerBase src) {
        super(src, src._ignoreAllUnknown);
    }

    protected BeanDeserializer(BeanDeserializerBase src, boolean ignoreAllUnknown) {
        super(src, ignoreAllUnknown);
    }

    protected BeanDeserializer(BeanDeserializerBase src, NameTransformer unwrapper) {
        super(src, unwrapper);
    }

    public BeanDeserializer(BeanDeserializerBase src, ObjectIdReader oir) {
        super(src, oir);
    }

    public BeanDeserializer(BeanDeserializerBase src, HashSet<String> ignorableProps) {
        super(src, ignorableProps);
    }

    @Override // com.fasterxml.jackson.databind.deser.BeanDeserializerBase, com.fasterxml.jackson.databind.JsonDeserializer
    public JsonDeserializer<Object> unwrappingDeserializer(NameTransformer unwrapper) {
        return getClass() != BeanDeserializer.class ? this : new BeanDeserializer(this, unwrapper);
    }

    @Override // com.fasterxml.jackson.databind.deser.BeanDeserializerBase
    public BeanDeserializer withObjectIdReader(ObjectIdReader oir) {
        return new BeanDeserializer(this, oir);
    }

    @Override // com.fasterxml.jackson.databind.deser.BeanDeserializerBase
    public BeanDeserializer withIgnorableProperties(HashSet<String> ignorableProps) {
        return new BeanDeserializer(this, ignorableProps);
    }

    @Override // com.fasterxml.jackson.databind.deser.BeanDeserializerBase
    protected BeanDeserializerBase asArrayDeserializer() {
        SettableBeanProperty[] props = this._beanProperties.getPropertiesInInsertionOrder();
        return new BeanAsArrayDeserializer(this, props);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Object deserialize(JsonParser p, DeserializationContext ctxt) throws IOException {
        if (p.isExpectedStartObjectToken()) {
            if (this._vanillaProcessing) {
                return vanillaDeserialize(p, ctxt, p.nextToken());
            }
            p.nextToken();
            if (this._objectIdReader != null) {
                return deserializeWithObjectId(p, ctxt);
            }
            return deserializeFromObject(p, ctxt);
        }
        JsonToken t = p.getCurrentToken();
        return _deserializeOther(p, ctxt, t);
    }

    protected final Object _deserializeOther(JsonParser p, DeserializationContext ctxt, JsonToken t) throws IOException {
        switch (t) {
            case VALUE_STRING:
                return deserializeFromString(p, ctxt);
            case VALUE_NUMBER_INT:
                return deserializeFromNumber(p, ctxt);
            case VALUE_NUMBER_FLOAT:
                return deserializeFromDouble(p, ctxt);
            case VALUE_EMBEDDED_OBJECT:
                return deserializeFromEmbedded(p, ctxt);
            case VALUE_TRUE:
            case VALUE_FALSE:
                return deserializeFromBoolean(p, ctxt);
            case START_ARRAY:
                return deserializeFromArray(p, ctxt);
            case FIELD_NAME:
            case END_OBJECT:
                if (this._vanillaProcessing) {
                    return vanillaDeserialize(p, ctxt, t);
                }
                if (this._objectIdReader != null) {
                    return deserializeWithObjectId(p, ctxt);
                }
                return deserializeFromObject(p, ctxt);
            default:
                throw ctxt.mappingException(handledType());
        }
    }

    protected Object _missingToken(JsonParser p, DeserializationContext ctxt) throws IOException {
        throw ctxt.endOfInputException(handledType());
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Object deserialize(JsonParser p, DeserializationContext ctxt, Object bean) throws IOException {
        String propName;
        Class<?> view;
        p.setCurrentValue(bean);
        if (this._injectables != null) {
            injectValues(ctxt, bean);
        }
        if (this._unwrappedPropertyHandler != null) {
            return deserializeWithUnwrapped(p, ctxt, bean);
        }
        if (this._externalTypeIdHandler != null) {
            return deserializeWithExternalTypeId(p, ctxt, bean);
        }
        if (p.isExpectedStartObjectToken()) {
            propName = p.nextFieldName();
            if (propName == null) {
                return bean;
            }
        } else if (p.hasTokenId(5)) {
            propName = p.getCurrentName();
        } else {
            return bean;
        }
        if (this._needViewProcesing && (view = ctxt.getActiveView()) != null) {
            return deserializeWithView(p, ctxt, bean, view);
        }
        do {
            p.nextToken();
            SettableBeanProperty prop = this._beanProperties.find(propName);
            if (prop != null) {
                try {
                    prop.deserializeAndSet(p, ctxt, bean);
                } catch (Exception e) {
                    wrapAndThrow(e, bean, propName, ctxt);
                }
            } else {
                handleUnknownVanilla(p, ctxt, bean, propName);
            }
            propName = p.nextFieldName();
        } while (propName != null);
        return bean;
    }

    private final Object vanillaDeserialize(JsonParser p, DeserializationContext ctxt, JsonToken t) throws IOException {
        Object bean = this._valueInstantiator.createUsingDefault(ctxt);
        p.setCurrentValue(bean);
        if (p.hasTokenId(5)) {
            String propName = p.getCurrentName();
            do {
                p.nextToken();
                SettableBeanProperty prop = this._beanProperties.find(propName);
                if (prop != null) {
                    try {
                        prop.deserializeAndSet(p, ctxt, bean);
                    } catch (Exception e) {
                        wrapAndThrow(e, bean, propName, ctxt);
                    }
                } else {
                    handleUnknownVanilla(p, ctxt, bean, propName);
                }
                propName = p.nextFieldName();
            } while (propName != null);
        }
        return bean;
    }

    @Override // com.fasterxml.jackson.databind.deser.BeanDeserializerBase
    public Object deserializeFromObject(JsonParser p, DeserializationContext ctxt) throws IOException {
        Class<?> view;
        Object id;
        if (this._objectIdReader != null && this._objectIdReader.maySerializeAsObject() && p.hasTokenId(5) && this._objectIdReader.isValidReferencePropertyName(p.getCurrentName(), p)) {
            return deserializeFromObjectId(p, ctxt);
        }
        if (this._nonStandardCreation) {
            if (this._unwrappedPropertyHandler != null) {
                return deserializeWithUnwrapped(p, ctxt);
            }
            if (this._externalTypeIdHandler != null) {
                return deserializeWithExternalTypeId(p, ctxt);
            }
            Object bean = deserializeFromObjectUsingNonDefault(p, ctxt);
            if (this._injectables != null) {
                injectValues(ctxt, bean);
                return bean;
            }
            return bean;
        }
        Object bean2 = this._valueInstantiator.createUsingDefault(ctxt);
        p.setCurrentValue(bean2);
        if (p.canReadObjectId() && (id = p.getObjectId()) != null) {
            _handleTypedObjectId(p, ctxt, bean2, id);
        }
        if (this._injectables != null) {
            injectValues(ctxt, bean2);
        }
        if (this._needViewProcesing && (view = ctxt.getActiveView()) != null) {
            return deserializeWithView(p, ctxt, bean2, view);
        }
        if (p.hasTokenId(5)) {
            String propName = p.getCurrentName();
            do {
                p.nextToken();
                SettableBeanProperty prop = this._beanProperties.find(propName);
                if (prop != null) {
                    try {
                        prop.deserializeAndSet(p, ctxt, bean2);
                    } catch (Exception e) {
                        wrapAndThrow(e, bean2, propName, ctxt);
                    }
                } else {
                    handleUnknownVanilla(p, ctxt, bean2, propName);
                }
                propName = p.nextFieldName();
            } while (propName != null);
            return bean2;
        }
        return bean2;
    }

    @Override // com.fasterxml.jackson.databind.deser.BeanDeserializerBase
    protected Object _deserializeUsingPropertyBased(JsonParser p, DeserializationContext ctxt) throws IOException {
        Object bean;
        Object bean2;
        PropertyBasedCreator creator = this._propertyBasedCreator;
        PropertyValueBuffer buffer = creator.startBuilding(p, ctxt, this._objectIdReader);
        TokenBuffer unknown = null;
        JsonToken t = p.getCurrentToken();
        while (t == JsonToken.FIELD_NAME) {
            String propName = p.getCurrentName();
            p.nextToken();
            SettableBeanProperty creatorProp = creator.findCreatorProperty(propName);
            if (creatorProp != null) {
                if (buffer.assignParameter(creatorProp, _deserializeWithErrorWrapping(p, ctxt, creatorProp))) {
                    p.nextToken();
                    try {
                        bean2 = creator.build(ctxt, buffer);
                    } catch (Exception e) {
                        wrapInstantiationProblem(e, ctxt);
                        bean2 = null;
                    }
                    if (bean2 == null) {
                        throw ctxt.instantiationException(this._beanType.getRawClass(), "JSON Creator returned null");
                    }
                    p.setCurrentValue(bean2);
                    if (bean2.getClass() != this._beanType.getRawClass()) {
                        return handlePolymorphic(p, ctxt, bean2, unknown);
                    }
                    if (unknown != null) {
                        bean2 = handleUnknownProperties(ctxt, bean2, unknown);
                    }
                    return deserialize(p, ctxt, bean2);
                }
            } else if (!buffer.readIdProperty(propName)) {
                SettableBeanProperty prop = this._beanProperties.find(propName);
                if (prop != null) {
                    buffer.bufferProperty(prop, _deserializeWithErrorWrapping(p, ctxt, prop));
                } else if (this._ignorableProps != null && this._ignorableProps.contains(propName)) {
                    handleIgnoredProperty(p, ctxt, handledType(), propName);
                } else if (this._anySetter != null) {
                    try {
                        buffer.bufferAnyProperty(this._anySetter, propName, this._anySetter.deserialize(p, ctxt));
                    } catch (Exception e2) {
                        wrapAndThrow(e2, this._beanType.getRawClass(), propName, ctxt);
                    }
                } else {
                    if (unknown == null) {
                        unknown = new TokenBuffer(p, ctxt);
                    }
                    unknown.writeFieldName(propName);
                    unknown.copyCurrentStructure(p);
                }
            }
            t = p.nextToken();
        }
        try {
            bean = creator.build(ctxt, buffer);
        } catch (Exception e3) {
            wrapInstantiationProblem(e3, ctxt);
            bean = null;
        }
        if (unknown != null) {
            if (bean.getClass() != this._beanType.getRawClass()) {
                return handlePolymorphic(null, ctxt, bean, unknown);
            }
            return handleUnknownProperties(ctxt, bean, unknown);
        }
        return bean;
    }

    protected final Object _deserializeWithErrorWrapping(JsonParser p, DeserializationContext ctxt, SettableBeanProperty prop) throws IOException {
        try {
            return prop.deserialize(p, ctxt);
        } catch (Exception e) {
            wrapAndThrow(e, this._beanType.getRawClass(), prop.getName(), ctxt);
            return null;
        }
    }

    protected final Object deserializeWithView(JsonParser p, DeserializationContext ctxt, Object bean, Class<?> activeView) throws IOException {
        if (p.hasTokenId(5)) {
            String propName = p.getCurrentName();
            do {
                p.nextToken();
                SettableBeanProperty prop = this._beanProperties.find(propName);
                if (prop != null) {
                    if (!prop.visibleInView(activeView)) {
                        p.skipChildren();
                    } else {
                        try {
                            prop.deserializeAndSet(p, ctxt, bean);
                        } catch (Exception e) {
                            wrapAndThrow(e, bean, propName, ctxt);
                        }
                    }
                } else {
                    handleUnknownVanilla(p, ctxt, bean, propName);
                }
                propName = p.nextFieldName();
            } while (propName != null);
        }
        return bean;
    }

    protected Object deserializeWithUnwrapped(JsonParser p, DeserializationContext ctxt) throws IOException {
        if (this._delegateDeserializer != null) {
            return this._valueInstantiator.createUsingDelegate(ctxt, this._delegateDeserializer.deserialize(p, ctxt));
        }
        if (this._propertyBasedCreator != null) {
            return deserializeUsingPropertyBasedWithUnwrapped(p, ctxt);
        }
        TokenBuffer tokens = new TokenBuffer(p, ctxt);
        tokens.writeStartObject();
        Object bean = this._valueInstantiator.createUsingDefault(ctxt);
        p.setCurrentValue(bean);
        if (this._injectables != null) {
            injectValues(ctxt, bean);
        }
        Class<?> activeView = this._needViewProcesing ? ctxt.getActiveView() : null;
        String propName = p.hasTokenId(5) ? p.getCurrentName() : null;
        while (propName != null) {
            p.nextToken();
            SettableBeanProperty prop = this._beanProperties.find(propName);
            if (prop != null) {
                if (activeView != null && !prop.visibleInView(activeView)) {
                    p.skipChildren();
                } else {
                    try {
                        prop.deserializeAndSet(p, ctxt, bean);
                    } catch (Exception e) {
                        wrapAndThrow(e, bean, propName, ctxt);
                    }
                }
            } else if (this._ignorableProps != null && this._ignorableProps.contains(propName)) {
                handleIgnoredProperty(p, ctxt, bean, propName);
            } else {
                tokens.writeFieldName(propName);
                tokens.copyCurrentStructure(p);
                if (this._anySetter != null) {
                    try {
                        this._anySetter.deserializeAndSet(p, ctxt, bean, propName);
                    } catch (Exception e2) {
                        wrapAndThrow(e2, bean, propName, ctxt);
                    }
                }
            }
            propName = p.nextFieldName();
        }
        tokens.writeEndObject();
        this._unwrappedPropertyHandler.processUnwrapped(p, ctxt, bean, tokens);
        return bean;
    }

    protected Object deserializeWithUnwrapped(JsonParser p, DeserializationContext ctxt, Object bean) throws Exception {
        JsonToken t = p.getCurrentToken();
        if (t == JsonToken.START_OBJECT) {
            t = p.nextToken();
        }
        TokenBuffer tokens = new TokenBuffer(p, ctxt);
        tokens.writeStartObject();
        Class<?> activeView = this._needViewProcesing ? ctxt.getActiveView() : null;
        while (t == JsonToken.FIELD_NAME) {
            String propName = p.getCurrentName();
            SettableBeanProperty prop = this._beanProperties.find(propName);
            p.nextToken();
            if (prop != null) {
                if (activeView != null && !prop.visibleInView(activeView)) {
                    p.skipChildren();
                } else {
                    try {
                        prop.deserializeAndSet(p, ctxt, bean);
                    } catch (Exception e) {
                        wrapAndThrow(e, bean, propName, ctxt);
                    }
                }
            } else if (this._ignorableProps != null && this._ignorableProps.contains(propName)) {
                handleIgnoredProperty(p, ctxt, bean, propName);
            } else {
                tokens.writeFieldName(propName);
                tokens.copyCurrentStructure(p);
                if (this._anySetter != null) {
                    this._anySetter.deserializeAndSet(p, ctxt, bean, propName);
                }
            }
            t = p.nextToken();
        }
        tokens.writeEndObject();
        this._unwrappedPropertyHandler.processUnwrapped(p, ctxt, bean, tokens);
        return bean;
    }

    protected Object deserializeUsingPropertyBasedWithUnwrapped(JsonParser p, DeserializationContext ctxt) throws IOException {
        PropertyBasedCreator creator = this._propertyBasedCreator;
        PropertyValueBuffer buffer = creator.startBuilding(p, ctxt, this._objectIdReader);
        TokenBuffer tokens = new TokenBuffer(p, ctxt);
        tokens.writeStartObject();
        JsonToken t = p.getCurrentToken();
        while (t == JsonToken.FIELD_NAME) {
            String propName = p.getCurrentName();
            p.nextToken();
            SettableBeanProperty creatorProp = creator.findCreatorProperty(propName);
            if (creatorProp != null) {
                if (buffer.assignParameter(creatorProp, _deserializeWithErrorWrapping(p, ctxt, creatorProp))) {
                    JsonToken t2 = p.nextToken();
                    try {
                        Object bean = creator.build(ctxt, buffer);
                        p.setCurrentValue(bean);
                        while (t2 == JsonToken.FIELD_NAME) {
                            p.nextToken();
                            tokens.copyCurrentStructure(p);
                            t2 = p.nextToken();
                        }
                        tokens.writeEndObject();
                        if (bean.getClass() != this._beanType.getRawClass()) {
                            tokens.close();
                            throw ctxt.mappingException("Can not create polymorphic instances with unwrapped values");
                        }
                        return this._unwrappedPropertyHandler.processUnwrapped(p, ctxt, bean, tokens);
                    } catch (Exception e) {
                        wrapInstantiationProblem(e, ctxt);
                    }
                } else {
                    continue;
                }
            } else if (!buffer.readIdProperty(propName)) {
                SettableBeanProperty prop = this._beanProperties.find(propName);
                if (prop != null) {
                    buffer.bufferProperty(prop, _deserializeWithErrorWrapping(p, ctxt, prop));
                } else if (this._ignorableProps != null && this._ignorableProps.contains(propName)) {
                    handleIgnoredProperty(p, ctxt, handledType(), propName);
                } else {
                    tokens.writeFieldName(propName);
                    tokens.copyCurrentStructure(p);
                    if (this._anySetter != null) {
                        try {
                            buffer.bufferAnyProperty(this._anySetter, propName, this._anySetter.deserialize(p, ctxt));
                        } catch (Exception e2) {
                            wrapAndThrow(e2, this._beanType.getRawClass(), propName, ctxt);
                        }
                    }
                }
            }
            t = p.nextToken();
        }
        try {
            return this._unwrappedPropertyHandler.processUnwrapped(p, ctxt, creator.build(ctxt, buffer), tokens);
        } catch (Exception e3) {
            wrapInstantiationProblem(e3, ctxt);
            return null;
        }
    }

    protected Object deserializeWithExternalTypeId(JsonParser p, DeserializationContext ctxt) throws IOException {
        return this._propertyBasedCreator != null ? deserializeUsingPropertyBasedWithExternalTypeId(p, ctxt) : deserializeWithExternalTypeId(p, ctxt, this._valueInstantiator.createUsingDefault(ctxt));
    }

    protected Object deserializeWithExternalTypeId(JsonParser p, DeserializationContext ctxt, Object bean) throws IOException {
        Class<?> activeView = this._needViewProcesing ? ctxt.getActiveView() : null;
        ExternalTypeHandler ext = this._externalTypeIdHandler.start();
        JsonToken t = p.getCurrentToken();
        while (t == JsonToken.FIELD_NAME) {
            String propName = p.getCurrentName();
            JsonToken t2 = p.nextToken();
            SettableBeanProperty prop = this._beanProperties.find(propName);
            if (prop != null) {
                if (t2.isScalarValue()) {
                    ext.handleTypePropertyValue(p, ctxt, propName, bean);
                }
                if (activeView != null && !prop.visibleInView(activeView)) {
                    p.skipChildren();
                } else {
                    try {
                        prop.deserializeAndSet(p, ctxt, bean);
                    } catch (Exception e) {
                        wrapAndThrow(e, bean, propName, ctxt);
                    }
                }
            } else if (this._ignorableProps != null && this._ignorableProps.contains(propName)) {
                handleIgnoredProperty(p, ctxt, bean, propName);
            } else if (!ext.handlePropertyValue(p, ctxt, propName, bean)) {
                if (this._anySetter != null) {
                    try {
                        this._anySetter.deserializeAndSet(p, ctxt, bean, propName);
                    } catch (Exception e2) {
                        wrapAndThrow(e2, bean, propName, ctxt);
                    }
                } else {
                    handleUnknownProperty(p, ctxt, bean, propName);
                }
            }
            t = p.nextToken();
        }
        return ext.complete(p, ctxt, bean);
    }

    protected Object deserializeUsingPropertyBasedWithExternalTypeId(JsonParser p, DeserializationContext ctxt) throws IOException {
        ExternalTypeHandler ext = this._externalTypeIdHandler.start();
        PropertyBasedCreator creator = this._propertyBasedCreator;
        PropertyValueBuffer buffer = creator.startBuilding(p, ctxt, this._objectIdReader);
        TokenBuffer tokens = new TokenBuffer(p, ctxt);
        tokens.writeStartObject();
        JsonToken t = p.getCurrentToken();
        while (t == JsonToken.FIELD_NAME) {
            String propName = p.getCurrentName();
            p.nextToken();
            SettableBeanProperty creatorProp = creator.findCreatorProperty(propName);
            if (creatorProp != null) {
                if (!ext.handlePropertyValue(p, ctxt, propName, null) && buffer.assignParameter(creatorProp, _deserializeWithErrorWrapping(p, ctxt, creatorProp))) {
                    JsonToken t2 = p.nextToken();
                    try {
                        Object bean = creator.build(ctxt, buffer);
                        while (t2 == JsonToken.FIELD_NAME) {
                            p.nextToken();
                            tokens.copyCurrentStructure(p);
                            t2 = p.nextToken();
                        }
                        if (bean.getClass() != this._beanType.getRawClass()) {
                            throw ctxt.mappingException("Can not create polymorphic instances with unwrapped values");
                        }
                        return ext.complete(p, ctxt, bean);
                    } catch (Exception e) {
                        wrapAndThrow(e, this._beanType.getRawClass(), propName, ctxt);
                    }
                }
            } else if (!buffer.readIdProperty(propName)) {
                SettableBeanProperty prop = this._beanProperties.find(propName);
                if (prop != null) {
                    buffer.bufferProperty(prop, prop.deserialize(p, ctxt));
                } else if (!ext.handlePropertyValue(p, ctxt, propName, null)) {
                    if (this._ignorableProps != null && this._ignorableProps.contains(propName)) {
                        handleIgnoredProperty(p, ctxt, handledType(), propName);
                    } else if (this._anySetter != null) {
                        buffer.bufferAnyProperty(this._anySetter, propName, this._anySetter.deserialize(p, ctxt));
                    }
                }
            }
            t = p.nextToken();
        }
        try {
            return ext.complete(p, ctxt, buffer, creator);
        } catch (Exception e2) {
            wrapInstantiationProblem(e2, ctxt);
            return null;
        }
    }
}
