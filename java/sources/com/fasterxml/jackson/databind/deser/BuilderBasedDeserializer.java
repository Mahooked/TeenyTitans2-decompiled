package com.fasterxml.jackson.databind.deser;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.BeanDescription;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.deser.impl.BeanAsArrayBuilderDeserializer;
import com.fasterxml.jackson.databind.deser.impl.BeanPropertyMap;
import com.fasterxml.jackson.databind.deser.impl.ExternalTypeHandler;
import com.fasterxml.jackson.databind.deser.impl.ObjectIdReader;
import com.fasterxml.jackson.databind.deser.impl.PropertyBasedCreator;
import com.fasterxml.jackson.databind.deser.impl.PropertyValueBuffer;
import com.fasterxml.jackson.databind.introspect.AnnotatedMethod;
import com.fasterxml.jackson.databind.util.NameTransformer;
import com.fasterxml.jackson.databind.util.TokenBuffer;
import java.io.IOException;
import java.util.HashSet;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public class BuilderBasedDeserializer extends BeanDeserializerBase {
    private static final long serialVersionUID = 1;
    protected final AnnotatedMethod _buildMethod;

    @Override // com.fasterxml.jackson.databind.deser.BeanDeserializerBase
    public /* bridge */ /* synthetic */ BeanDeserializerBase withIgnorableProperties(HashSet x0) {
        return withIgnorableProperties((HashSet<String>) x0);
    }

    public BuilderBasedDeserializer(BeanDeserializerBuilder builder, BeanDescription beanDesc, BeanPropertyMap properties, Map<String, SettableBeanProperty> backRefs, HashSet<String> ignorableProps, boolean ignoreAllUnknown, boolean hasViews) {
        super(builder, beanDesc, properties, backRefs, ignorableProps, ignoreAllUnknown, hasViews);
        this._buildMethod = builder.getBuildMethod();
        if (this._objectIdReader != null) {
            throw new IllegalArgumentException("Can not use Object Id with Builder-based deserialization (type " + beanDesc.getType() + ")");
        }
    }

    protected BuilderBasedDeserializer(BuilderBasedDeserializer src) {
        this(src, src._ignoreAllUnknown);
    }

    protected BuilderBasedDeserializer(BuilderBasedDeserializer src, boolean ignoreAllUnknown) {
        super(src, ignoreAllUnknown);
        this._buildMethod = src._buildMethod;
    }

    protected BuilderBasedDeserializer(BuilderBasedDeserializer src, NameTransformer unwrapper) {
        super(src, unwrapper);
        this._buildMethod = src._buildMethod;
    }

    public BuilderBasedDeserializer(BuilderBasedDeserializer src, ObjectIdReader oir) {
        super(src, oir);
        this._buildMethod = src._buildMethod;
    }

    public BuilderBasedDeserializer(BuilderBasedDeserializer src, HashSet<String> ignorableProps) {
        super(src, ignorableProps);
        this._buildMethod = src._buildMethod;
    }

    @Override // com.fasterxml.jackson.databind.deser.BeanDeserializerBase, com.fasterxml.jackson.databind.JsonDeserializer
    public JsonDeserializer<Object> unwrappingDeserializer(NameTransformer unwrapper) {
        return new BuilderBasedDeserializer(this, unwrapper);
    }

    @Override // com.fasterxml.jackson.databind.deser.BeanDeserializerBase
    public BuilderBasedDeserializer withObjectIdReader(ObjectIdReader oir) {
        return new BuilderBasedDeserializer(this, oir);
    }

    @Override // com.fasterxml.jackson.databind.deser.BeanDeserializerBase
    public BuilderBasedDeserializer withIgnorableProperties(HashSet<String> ignorableProps) {
        return new BuilderBasedDeserializer(this, ignorableProps);
    }

    /* JADX INFO: Access modifiers changed from: protected */
    @Override // com.fasterxml.jackson.databind.deser.BeanDeserializerBase
    public BeanAsArrayBuilderDeserializer asArrayDeserializer() {
        SettableBeanProperty[] props = this._beanProperties.getPropertiesInInsertionOrder();
        return new BeanAsArrayBuilderDeserializer(this, props, this._buildMethod);
    }

    protected final Object finishBuild(DeserializationContext ctxt, Object builder) throws IOException {
        if (this._buildMethod != null) {
            try {
                return this._buildMethod.getMember().invoke(builder, new Object[0]);
            } catch (Exception e) {
                wrapInstantiationProblem(e, ctxt);
                return null;
            }
        }
        return builder;
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public final Object deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        JsonToken t = jp2.getCurrentToken();
        if (t == JsonToken.START_OBJECT) {
            JsonToken t2 = jp2.nextToken();
            if (this._vanillaProcessing) {
                return finishBuild(ctxt, vanillaDeserialize(jp2, ctxt, t2));
            }
            Object builder = deserializeFromObject(jp2, ctxt);
            return finishBuild(ctxt, builder);
        }
        switch (t) {
            case VALUE_STRING:
                return finishBuild(ctxt, deserializeFromString(jp2, ctxt));
            case VALUE_NUMBER_INT:
                return finishBuild(ctxt, deserializeFromNumber(jp2, ctxt));
            case VALUE_NUMBER_FLOAT:
                return finishBuild(ctxt, deserializeFromDouble(jp2, ctxt));
            case VALUE_EMBEDDED_OBJECT:
                return jp2.getEmbeddedObject();
            case VALUE_TRUE:
            case VALUE_FALSE:
                return finishBuild(ctxt, deserializeFromBoolean(jp2, ctxt));
            case START_ARRAY:
                return finishBuild(ctxt, deserializeFromArray(jp2, ctxt));
            case FIELD_NAME:
            case END_OBJECT:
                return finishBuild(ctxt, deserializeFromObject(jp2, ctxt));
            default:
                throw ctxt.mappingException(handledType());
        }
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Object deserialize(JsonParser jp2, DeserializationContext ctxt, Object builder) throws IOException {
        return finishBuild(ctxt, _deserialize(jp2, ctxt, builder));
    }

    protected final Object _deserialize(JsonParser jp2, DeserializationContext ctxt, Object builder) throws IOException {
        Class<?> view;
        if (this._injectables != null) {
            injectValues(ctxt, builder);
        }
        if (this._unwrappedPropertyHandler != null) {
            return deserializeWithUnwrapped(jp2, ctxt, builder);
        }
        if (this._externalTypeIdHandler != null) {
            return deserializeWithExternalTypeId(jp2, ctxt, builder);
        }
        if (this._needViewProcesing && (view = ctxt.getActiveView()) != null) {
            return deserializeWithView(jp2, ctxt, builder, view);
        }
        JsonToken t = jp2.getCurrentToken();
        if (t == JsonToken.START_OBJECT) {
            t = jp2.nextToken();
        }
        while (t == JsonToken.FIELD_NAME) {
            String propName = jp2.getCurrentName();
            jp2.nextToken();
            SettableBeanProperty prop = this._beanProperties.find(propName);
            if (prop != null) {
                try {
                    builder = prop.deserializeSetAndReturn(jp2, ctxt, builder);
                } catch (Exception e) {
                    wrapAndThrow(e, builder, propName, ctxt);
                }
            } else {
                handleUnknownVanilla(jp2, ctxt, handledType(), propName);
            }
            t = jp2.nextToken();
        }
        return builder;
    }

    private final Object vanillaDeserialize(JsonParser jp2, DeserializationContext ctxt, JsonToken t) throws IOException {
        Object bean = this._valueInstantiator.createUsingDefault(ctxt);
        while (jp2.getCurrentToken() != JsonToken.END_OBJECT) {
            String propName = jp2.getCurrentName();
            jp2.nextToken();
            SettableBeanProperty prop = this._beanProperties.find(propName);
            if (prop != null) {
                try {
                    bean = prop.deserializeSetAndReturn(jp2, ctxt, bean);
                } catch (Exception e) {
                    wrapAndThrow(e, bean, propName, ctxt);
                }
            } else {
                handleUnknownVanilla(jp2, ctxt, bean, propName);
            }
            jp2.nextToken();
        }
        return bean;
    }

    @Override // com.fasterxml.jackson.databind.deser.BeanDeserializerBase
    public Object deserializeFromObject(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        Class<?> view;
        if (this._nonStandardCreation) {
            if (this._unwrappedPropertyHandler != null) {
                return deserializeWithUnwrapped(jp2, ctxt);
            }
            if (this._externalTypeIdHandler != null) {
                return deserializeWithExternalTypeId(jp2, ctxt);
            }
            return deserializeFromObjectUsingNonDefault(jp2, ctxt);
        }
        Object bean = this._valueInstantiator.createUsingDefault(ctxt);
        if (this._injectables != null) {
            injectValues(ctxt, bean);
        }
        if (this._needViewProcesing && (view = ctxt.getActiveView()) != null) {
            return deserializeWithView(jp2, ctxt, bean, view);
        }
        while (jp2.getCurrentToken() != JsonToken.END_OBJECT) {
            String propName = jp2.getCurrentName();
            jp2.nextToken();
            SettableBeanProperty prop = this._beanProperties.find(propName);
            if (prop != null) {
                try {
                    bean = prop.deserializeSetAndReturn(jp2, ctxt, bean);
                } catch (Exception e) {
                    wrapAndThrow(e, bean, propName, ctxt);
                }
            } else {
                handleUnknownVanilla(jp2, ctxt, bean, propName);
            }
            jp2.nextToken();
        }
        return bean;
    }

    @Override // com.fasterxml.jackson.databind.deser.BeanDeserializerBase
    protected final Object _deserializeUsingPropertyBased(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        PropertyBasedCreator creator = this._propertyBasedCreator;
        PropertyValueBuffer buffer = creator.startBuilding(jp2, ctxt, this._objectIdReader);
        TokenBuffer unknown = null;
        JsonToken t = jp2.getCurrentToken();
        while (t == JsonToken.FIELD_NAME) {
            String propName = jp2.getCurrentName();
            jp2.nextToken();
            SettableBeanProperty creatorProp = creator.findCreatorProperty(propName);
            if (creatorProp != null) {
                if (buffer.assignParameter(creatorProp, creatorProp.deserialize(jp2, ctxt))) {
                    jp2.nextToken();
                    try {
                        Object bean = creator.build(ctxt, buffer);
                        if (bean.getClass() != this._beanType.getRawClass()) {
                            return handlePolymorphic(jp2, ctxt, bean, unknown);
                        }
                        if (unknown != null) {
                            bean = handleUnknownProperties(ctxt, bean, unknown);
                        }
                        return _deserialize(jp2, ctxt, bean);
                    } catch (Exception e) {
                        wrapAndThrow(e, this._beanType.getRawClass(), propName, ctxt);
                    }
                } else {
                    continue;
                }
            } else if (!buffer.readIdProperty(propName)) {
                SettableBeanProperty prop = this._beanProperties.find(propName);
                if (prop != null) {
                    buffer.bufferProperty(prop, prop.deserialize(jp2, ctxt));
                } else if (this._ignorableProps != null && this._ignorableProps.contains(propName)) {
                    handleIgnoredProperty(jp2, ctxt, handledType(), propName);
                } else if (this._anySetter != null) {
                    buffer.bufferAnyProperty(this._anySetter, propName, this._anySetter.deserialize(jp2, ctxt));
                } else {
                    if (unknown == null) {
                        unknown = new TokenBuffer(jp2, ctxt);
                    }
                    unknown.writeFieldName(propName);
                    unknown.copyCurrentStructure(jp2);
                }
            }
            t = jp2.nextToken();
        }
        try {
            Object bean2 = creator.build(ctxt, buffer);
            if (unknown != null) {
                if (bean2.getClass() != this._beanType.getRawClass()) {
                    return handlePolymorphic(null, ctxt, bean2, unknown);
                }
                return handleUnknownProperties(ctxt, bean2, unknown);
            }
            return bean2;
        } catch (Exception e2) {
            wrapInstantiationProblem(e2, ctxt);
            return null;
        }
    }

    protected final Object deserializeWithView(JsonParser jp2, DeserializationContext ctxt, Object bean, Class<?> activeView) throws IOException {
        JsonToken t = jp2.getCurrentToken();
        while (t == JsonToken.FIELD_NAME) {
            String propName = jp2.getCurrentName();
            jp2.nextToken();
            SettableBeanProperty prop = this._beanProperties.find(propName);
            if (prop != null) {
                if (!prop.visibleInView(activeView)) {
                    jp2.skipChildren();
                } else {
                    try {
                        bean = prop.deserializeSetAndReturn(jp2, ctxt, bean);
                    } catch (Exception e) {
                        wrapAndThrow(e, bean, propName, ctxt);
                    }
                }
            } else {
                handleUnknownVanilla(jp2, ctxt, bean, propName);
            }
            t = jp2.nextToken();
        }
        return bean;
    }

    protected Object deserializeWithUnwrapped(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        if (this._delegateDeserializer != null) {
            return this._valueInstantiator.createUsingDelegate(ctxt, this._delegateDeserializer.deserialize(jp2, ctxt));
        }
        if (this._propertyBasedCreator != null) {
            return deserializeUsingPropertyBasedWithUnwrapped(jp2, ctxt);
        }
        TokenBuffer tokens = new TokenBuffer(jp2, ctxt);
        tokens.writeStartObject();
        Object bean = this._valueInstantiator.createUsingDefault(ctxt);
        if (this._injectables != null) {
            injectValues(ctxt, bean);
        }
        Class<?> activeView = this._needViewProcesing ? ctxt.getActiveView() : null;
        while (jp2.getCurrentToken() != JsonToken.END_OBJECT) {
            String propName = jp2.getCurrentName();
            jp2.nextToken();
            SettableBeanProperty prop = this._beanProperties.find(propName);
            if (prop != null) {
                if (activeView != null && !prop.visibleInView(activeView)) {
                    jp2.skipChildren();
                } else {
                    try {
                        bean = prop.deserializeSetAndReturn(jp2, ctxt, bean);
                    } catch (Exception e) {
                        wrapAndThrow(e, bean, propName, ctxt);
                    }
                }
            } else if (this._ignorableProps != null && this._ignorableProps.contains(propName)) {
                handleIgnoredProperty(jp2, ctxt, bean, propName);
            } else {
                tokens.writeFieldName(propName);
                tokens.copyCurrentStructure(jp2);
                if (this._anySetter != null) {
                    try {
                        this._anySetter.deserializeAndSet(jp2, ctxt, bean, propName);
                    } catch (Exception e2) {
                        wrapAndThrow(e2, bean, propName, ctxt);
                    }
                }
            }
            jp2.nextToken();
        }
        tokens.writeEndObject();
        this._unwrappedPropertyHandler.processUnwrapped(jp2, ctxt, bean, tokens);
        return bean;
    }

    protected Object deserializeWithUnwrapped(JsonParser jp2, DeserializationContext ctxt, Object bean) throws Exception {
        JsonToken t = jp2.getCurrentToken();
        if (t == JsonToken.START_OBJECT) {
            t = jp2.nextToken();
        }
        TokenBuffer tokens = new TokenBuffer(jp2, ctxt);
        tokens.writeStartObject();
        Class<?> activeView = this._needViewProcesing ? ctxt.getActiveView() : null;
        while (t == JsonToken.FIELD_NAME) {
            String propName = jp2.getCurrentName();
            SettableBeanProperty prop = this._beanProperties.find(propName);
            jp2.nextToken();
            if (prop != null) {
                if (activeView != null && !prop.visibleInView(activeView)) {
                    jp2.skipChildren();
                } else {
                    try {
                        bean = prop.deserializeSetAndReturn(jp2, ctxt, bean);
                    } catch (Exception e) {
                        wrapAndThrow(e, bean, propName, ctxt);
                    }
                }
            } else if (this._ignorableProps != null && this._ignorableProps.contains(propName)) {
                handleIgnoredProperty(jp2, ctxt, bean, propName);
            } else {
                tokens.writeFieldName(propName);
                tokens.copyCurrentStructure(jp2);
                if (this._anySetter != null) {
                    this._anySetter.deserializeAndSet(jp2, ctxt, bean, propName);
                }
            }
            t = jp2.nextToken();
        }
        tokens.writeEndObject();
        this._unwrappedPropertyHandler.processUnwrapped(jp2, ctxt, bean, tokens);
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
                if (buffer.assignParameter(creatorProp, creatorProp.deserialize(p, ctxt))) {
                    JsonToken t2 = p.nextToken();
                    try {
                        Object bean = creator.build(ctxt, buffer);
                        while (t2 == JsonToken.FIELD_NAME) {
                            p.nextToken();
                            tokens.copyCurrentStructure(p);
                            t2 = p.nextToken();
                        }
                        tokens.writeEndObject();
                        if (bean.getClass() != this._beanType.getRawClass()) {
                            throw ctxt.mappingException("Can not create polymorphic instances with unwrapped values");
                        }
                        return this._unwrappedPropertyHandler.processUnwrapped(p, ctxt, bean, tokens);
                    } catch (Exception e) {
                        wrapAndThrow(e, this._beanType.getRawClass(), propName, ctxt);
                    }
                } else {
                    continue;
                }
            } else if (!buffer.readIdProperty(propName)) {
                SettableBeanProperty prop = this._beanProperties.find(propName);
                if (prop != null) {
                    buffer.bufferProperty(prop, prop.deserialize(p, ctxt));
                } else if (this._ignorableProps != null && this._ignorableProps.contains(propName)) {
                    handleIgnoredProperty(p, ctxt, handledType(), propName);
                } else {
                    tokens.writeFieldName(propName);
                    tokens.copyCurrentStructure(p);
                    if (this._anySetter != null) {
                        buffer.bufferAnyProperty(this._anySetter, propName, this._anySetter.deserialize(p, ctxt));
                    }
                }
            }
            t = p.nextToken();
        }
        try {
            return this._unwrappedPropertyHandler.processUnwrapped(p, ctxt, creator.build(ctxt, buffer), tokens);
        } catch (Exception e2) {
            wrapInstantiationProblem(e2, ctxt);
            return null;
        }
    }

    protected Object deserializeWithExternalTypeId(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        return this._propertyBasedCreator != null ? deserializeUsingPropertyBasedWithExternalTypeId(jp2, ctxt) : deserializeWithExternalTypeId(jp2, ctxt, this._valueInstantiator.createUsingDefault(ctxt));
    }

    protected Object deserializeWithExternalTypeId(JsonParser jp2, DeserializationContext ctxt, Object bean) throws IOException {
        Class<?> activeView = this._needViewProcesing ? ctxt.getActiveView() : null;
        ExternalTypeHandler ext = this._externalTypeIdHandler.start();
        while (jp2.getCurrentToken() != JsonToken.END_OBJECT) {
            String propName = jp2.getCurrentName();
            jp2.nextToken();
            SettableBeanProperty prop = this._beanProperties.find(propName);
            if (prop != null) {
                if (activeView != null && !prop.visibleInView(activeView)) {
                    jp2.skipChildren();
                } else {
                    try {
                        bean = prop.deserializeSetAndReturn(jp2, ctxt, bean);
                    } catch (Exception e) {
                        wrapAndThrow(e, bean, propName, ctxt);
                    }
                }
            } else if (this._ignorableProps != null && this._ignorableProps.contains(propName)) {
                handleIgnoredProperty(jp2, ctxt, bean, propName);
            } else if (!ext.handlePropertyValue(jp2, ctxt, propName, bean)) {
                if (this._anySetter != null) {
                    try {
                        this._anySetter.deserializeAndSet(jp2, ctxt, bean, propName);
                    } catch (Exception e2) {
                        wrapAndThrow(e2, bean, propName, ctxt);
                    }
                } else {
                    handleUnknownProperty(jp2, ctxt, bean, propName);
                }
            }
            jp2.nextToken();
        }
        return ext.complete(jp2, ctxt, bean);
    }

    protected Object deserializeUsingPropertyBasedWithExternalTypeId(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        throw new IllegalStateException("Deserialization with Builder, External type id, @JsonCreator not yet implemented");
    }
}
