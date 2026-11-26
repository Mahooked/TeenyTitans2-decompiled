package com.fasterxml.jackson.databind.deser.std;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.AnnotationIntrospector;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.KeyDeserializer;
import com.fasterxml.jackson.databind.annotation.JacksonStdImpl;
import com.fasterxml.jackson.databind.deser.ContextualDeserializer;
import com.fasterxml.jackson.databind.deser.ContextualKeyDeserializer;
import com.fasterxml.jackson.databind.deser.ResolvableDeserializer;
import com.fasterxml.jackson.databind.deser.SettableBeanProperty;
import com.fasterxml.jackson.databind.deser.UnresolvedForwardReference;
import com.fasterxml.jackson.databind.deser.ValueInstantiator;
import com.fasterxml.jackson.databind.deser.impl.PropertyBasedCreator;
import com.fasterxml.jackson.databind.deser.impl.PropertyValueBuffer;
import com.fasterxml.jackson.databind.deser.impl.ReadableObjectId;
import com.fasterxml.jackson.databind.introspect.AnnotatedMember;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import com.fasterxml.jackson.databind.util.ArrayBuilders;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@JacksonStdImpl
public class MapDeserializer extends ContainerDeserializerBase<Map<Object, Object>> implements ContextualDeserializer, ResolvableDeserializer {
    private static final long serialVersionUID = 1;
    protected JsonDeserializer<Object> _delegateDeserializer;
    protected final boolean _hasDefaultCreator;
    protected HashSet<String> _ignorableProperties;
    protected final KeyDeserializer _keyDeserializer;
    protected final JavaType _mapType;
    protected PropertyBasedCreator _propertyBasedCreator;
    protected boolean _standardStringKey;
    protected final JsonDeserializer<Object> _valueDeserializer;
    protected final ValueInstantiator _valueInstantiator;
    protected final TypeDeserializer _valueTypeDeserializer;

    public MapDeserializer(JavaType mapType, ValueInstantiator valueInstantiator, KeyDeserializer keyDeser, JsonDeserializer<Object> valueDeser, TypeDeserializer valueTypeDeser) {
        super(mapType);
        this._mapType = mapType;
        this._keyDeserializer = keyDeser;
        this._valueDeserializer = valueDeser;
        this._valueTypeDeserializer = valueTypeDeser;
        this._valueInstantiator = valueInstantiator;
        this._hasDefaultCreator = valueInstantiator.canCreateUsingDefault();
        this._delegateDeserializer = null;
        this._propertyBasedCreator = null;
        this._standardStringKey = _isStdKeyDeser(mapType, keyDeser);
    }

    protected MapDeserializer(MapDeserializer src) {
        super(src._mapType);
        this._mapType = src._mapType;
        this._keyDeserializer = src._keyDeserializer;
        this._valueDeserializer = src._valueDeserializer;
        this._valueTypeDeserializer = src._valueTypeDeserializer;
        this._valueInstantiator = src._valueInstantiator;
        this._propertyBasedCreator = src._propertyBasedCreator;
        this._delegateDeserializer = src._delegateDeserializer;
        this._hasDefaultCreator = src._hasDefaultCreator;
        this._ignorableProperties = src._ignorableProperties;
        this._standardStringKey = src._standardStringKey;
    }

    protected MapDeserializer(MapDeserializer src, KeyDeserializer keyDeser, JsonDeserializer<Object> valueDeser, TypeDeserializer valueTypeDeser, HashSet<String> ignorable) {
        super(src._mapType);
        this._mapType = src._mapType;
        this._keyDeserializer = keyDeser;
        this._valueDeserializer = valueDeser;
        this._valueTypeDeserializer = valueTypeDeser;
        this._valueInstantiator = src._valueInstantiator;
        this._propertyBasedCreator = src._propertyBasedCreator;
        this._delegateDeserializer = src._delegateDeserializer;
        this._hasDefaultCreator = src._hasDefaultCreator;
        this._ignorableProperties = ignorable;
        this._standardStringKey = _isStdKeyDeser(this._mapType, keyDeser);
    }

    protected MapDeserializer withResolved(KeyDeserializer keyDeser, TypeDeserializer valueTypeDeser, JsonDeserializer<?> valueDeser, HashSet<String> ignorable) {
        return (this._keyDeserializer == keyDeser && this._valueDeserializer == valueDeser && this._valueTypeDeserializer == valueTypeDeser && this._ignorableProperties == ignorable) ? this : new MapDeserializer(this, keyDeser, valueDeser, valueTypeDeser, ignorable);
    }

    protected final boolean _isStdKeyDeser(JavaType mapType, KeyDeserializer keyDeser) {
        JavaType keyType;
        if (keyDeser == null || (keyType = mapType.getKeyType()) == null) {
            return true;
        }
        Class<?> rawKeyType = keyType.getRawClass();
        return (rawKeyType == String.class || rawKeyType == Object.class) && isDefaultKeyDeserializer(keyDeser);
    }

    public void setIgnorableProperties(String[] ignorable) {
        this._ignorableProperties = (ignorable == null || ignorable.length == 0) ? null : ArrayBuilders.arrayToSet(ignorable);
    }

    @Override // com.fasterxml.jackson.databind.deser.ResolvableDeserializer
    public void resolve(DeserializationContext ctxt) throws JsonMappingException {
        if (this._valueInstantiator.canCreateUsingDelegate()) {
            JavaType delegateType = this._valueInstantiator.getDelegateType(ctxt.getConfig());
            if (delegateType == null) {
                throw new IllegalArgumentException("Invalid delegate-creator definition for " + this._mapType + ": value instantiator (" + this._valueInstantiator.getClass().getName() + ") returned true for 'canCreateUsingDelegate()', but null for 'getDelegateType()'");
            }
            this._delegateDeserializer = findDeserializer(ctxt, delegateType, null);
        }
        if (this._valueInstantiator.canCreateFromObjectWith()) {
            SettableBeanProperty[] creatorProps = this._valueInstantiator.getFromObjectArguments(ctxt.getConfig());
            this._propertyBasedCreator = PropertyBasedCreator.construct(ctxt, this._valueInstantiator, creatorProps);
        }
        this._standardStringKey = _isStdKeyDeser(this._mapType, this._keyDeserializer);
    }

    /* JADX WARN: Multi-variable type inference failed */
    /* JADX WARN: Type inference failed for: r13v0, types: [com.fasterxml.jackson.databind.deser.std.MapDeserializer] */
    /* JADX WARN: Type inference failed for: r4v3, types: [com.fasterxml.jackson.databind.KeyDeserializer] */
    /* JADX WARN: Type inference failed for: r4v5 */
    /* JADX WARN: Type inference failed for: r4v6 */
    /* JADX WARN: Type inference failed for: r4v7 */
    @Override // com.fasterxml.jackson.databind.deser.ContextualDeserializer
    public JsonDeserializer<?> createContextual(DeserializationContext deserializationContext, BeanProperty beanProperty) throws JsonMappingException {
        ?? CreateContextual;
        JsonDeserializer<?> jsonDeserializerHandleSecondaryContextualization;
        AnnotatedMember member;
        String[] strArrFindPropertiesToIgnore;
        Object obj = this._keyDeserializer;
        if (obj == null) {
            CreateContextual = deserializationContext.findKeyDeserializer(this._mapType.getKeyType(), beanProperty);
        } else {
            boolean z = obj instanceof ContextualKeyDeserializer;
            CreateContextual = obj;
            if (z) {
                CreateContextual = ((ContextualKeyDeserializer) obj).createContextual(deserializationContext, beanProperty);
            }
        }
        JsonDeserializer<?> jsonDeserializerFindConvertingContentDeserializer = this._valueDeserializer;
        if (beanProperty != null) {
            jsonDeserializerFindConvertingContentDeserializer = findConvertingContentDeserializer(deserializationContext, beanProperty, jsonDeserializerFindConvertingContentDeserializer);
        }
        JavaType contentType = this._mapType.getContentType();
        if (jsonDeserializerFindConvertingContentDeserializer == null) {
            jsonDeserializerHandleSecondaryContextualization = deserializationContext.findContextualValueDeserializer(contentType, beanProperty);
        } else {
            jsonDeserializerHandleSecondaryContextualization = deserializationContext.handleSecondaryContextualization(jsonDeserializerFindConvertingContentDeserializer, beanProperty, contentType);
        }
        TypeDeserializer typeDeserializerForProperty = this._valueTypeDeserializer;
        if (typeDeserializerForProperty != null) {
            typeDeserializerForProperty = typeDeserializerForProperty.forProperty(beanProperty);
        }
        HashSet<String> hashSet = this._ignorableProperties;
        AnnotationIntrospector annotationIntrospector = deserializationContext.getAnnotationIntrospector();
        if (annotationIntrospector != null && beanProperty != null && (member = beanProperty.getMember()) != null && (strArrFindPropertiesToIgnore = annotationIntrospector.findPropertiesToIgnore(member, false)) != null) {
            hashSet = hashSet == null ? new HashSet<>() : new HashSet<>(hashSet);
            for (String str : strArrFindPropertiesToIgnore) {
                hashSet.add(str);
            }
        }
        return withResolved(CreateContextual, typeDeserializerForProperty, jsonDeserializerHandleSecondaryContextualization, hashSet);
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
    public boolean isCachable() {
        return this._valueDeserializer == null && this._keyDeserializer == null && this._valueTypeDeserializer == null && this._ignorableProperties == null;
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Map<Object, Object> deserialize(JsonParser p, DeserializationContext ctxt) throws IOException {
        if (this._propertyBasedCreator != null) {
            return _deserializeUsingCreator(p, ctxt);
        }
        if (this._delegateDeserializer != null) {
            return (Map) this._valueInstantiator.createUsingDelegate(ctxt, this._delegateDeserializer.deserialize(p, ctxt));
        }
        if (!this._hasDefaultCreator) {
            throw ctxt.instantiationException(getMapClass(), "No default constructor found");
        }
        JsonToken t = p.getCurrentToken();
        if (t != JsonToken.START_OBJECT && t != JsonToken.FIELD_NAME && t != JsonToken.END_OBJECT) {
            if (t == JsonToken.VALUE_STRING) {
                return (Map) this._valueInstantiator.createFromString(ctxt, p.getText());
            }
            return _deserializeFromEmpty(p, ctxt);
        }
        Map<Object, Object> result = (Map) this._valueInstantiator.createUsingDefault(ctxt);
        if (this._standardStringKey) {
            _readAndBindStringMap(p, ctxt, result);
            return result;
        }
        _readAndBind(p, ctxt, result);
        return result;
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Map<Object, Object> deserialize(JsonParser p, DeserializationContext ctxt, Map<Object, Object> result) throws IOException {
        p.setCurrentValue(result);
        JsonToken t = p.getCurrentToken();
        if (t != JsonToken.START_OBJECT && t != JsonToken.FIELD_NAME) {
            throw ctxt.mappingException(getMapClass());
        }
        if (this._standardStringKey) {
            _readAndBindStringMap(p, ctxt, result);
        } else {
            _readAndBind(p, ctxt, result);
        }
        return result;
    }

    @Override // com.fasterxml.jackson.databind.deser.std.StdDeserializer, com.fasterxml.jackson.databind.JsonDeserializer
    public Object deserializeWithType(JsonParser jp2, DeserializationContext ctxt, TypeDeserializer typeDeserializer) throws IOException {
        return typeDeserializer.deserializeTypedFromObject(jp2, ctxt);
    }

    public final Class<?> getMapClass() {
        return this._mapType.getRawClass();
    }

    @Override // com.fasterxml.jackson.databind.deser.std.StdDeserializer
    public JavaType getValueType() {
        return this._mapType;
    }

    protected final void _readAndBind(JsonParser p, DeserializationContext ctxt, Map<Object, Object> result) throws IOException {
        String keyStr;
        Object value;
        KeyDeserializer keyDes = this._keyDeserializer;
        JsonDeserializer<Object> valueDes = this._valueDeserializer;
        TypeDeserializer typeDeser = this._valueTypeDeserializer;
        MapReferringAccumulator referringAccumulator = null;
        boolean useObjectId = valueDes.getObjectIdReader() != null;
        if (useObjectId) {
            referringAccumulator = new MapReferringAccumulator(this._mapType.getContentType().getRawClass(), result);
        }
        if (p.isExpectedStartObjectToken()) {
            keyStr = p.nextFieldName();
        } else {
            JsonToken t = p.getCurrentToken();
            if (t != JsonToken.END_OBJECT) {
                if (t != JsonToken.FIELD_NAME) {
                    throw ctxt.mappingException(this._mapType.getRawClass(), p.getCurrentToken());
                }
                keyStr = p.getCurrentName();
            } else {
                return;
            }
        }
        while (keyStr != null) {
            Object key = keyDes.deserializeKey(keyStr, ctxt);
            JsonToken t2 = p.nextToken();
            if (this._ignorableProperties != null && this._ignorableProperties.contains(keyStr)) {
                p.skipChildren();
            } else {
                try {
                    if (t2 == JsonToken.VALUE_NULL) {
                        value = valueDes.getNullValue(ctxt);
                    } else if (typeDeser == null) {
                        value = valueDes.deserialize(p, ctxt);
                    } else {
                        value = valueDes.deserializeWithType(p, ctxt, typeDeser);
                    }
                    if (useObjectId) {
                        referringAccumulator.put(key, value);
                    } else {
                        result.put(key, value);
                    }
                } catch (UnresolvedForwardReference reference) {
                    handleUnresolvedReference(p, referringAccumulator, key, reference);
                } catch (Exception e) {
                    wrapAndThrow(e, result, keyStr);
                }
            }
            keyStr = p.nextFieldName();
        }
    }

    protected final void _readAndBindStringMap(JsonParser p, DeserializationContext ctxt, Map<Object, Object> result) throws IOException {
        String key;
        Object value;
        JsonDeserializer<Object> valueDes = this._valueDeserializer;
        TypeDeserializer typeDeser = this._valueTypeDeserializer;
        MapReferringAccumulator referringAccumulator = null;
        boolean useObjectId = valueDes.getObjectIdReader() != null;
        if (useObjectId) {
            referringAccumulator = new MapReferringAccumulator(this._mapType.getContentType().getRawClass(), result);
        }
        if (p.isExpectedStartObjectToken()) {
            key = p.nextFieldName();
        } else {
            JsonToken t = p.getCurrentToken();
            if (t != JsonToken.END_OBJECT) {
                if (t != JsonToken.FIELD_NAME) {
                    throw ctxt.mappingException(this._mapType.getRawClass(), p.getCurrentToken());
                }
                key = p.getCurrentName();
            } else {
                return;
            }
        }
        while (key != null) {
            JsonToken t2 = p.nextToken();
            if (this._ignorableProperties != null && this._ignorableProperties.contains(key)) {
                p.skipChildren();
            } else {
                try {
                    if (t2 == JsonToken.VALUE_NULL) {
                        value = valueDes.getNullValue(ctxt);
                    } else if (typeDeser == null) {
                        value = valueDes.deserialize(p, ctxt);
                    } else {
                        value = valueDes.deserializeWithType(p, ctxt, typeDeser);
                    }
                    if (useObjectId) {
                        referringAccumulator.put(key, value);
                    } else {
                        result.put(key, value);
                    }
                } catch (UnresolvedForwardReference reference) {
                    handleUnresolvedReference(p, referringAccumulator, key, reference);
                } catch (Exception e) {
                    wrapAndThrow(e, result, key);
                }
            }
            key = p.nextFieldName();
        }
    }

    public Map<Object, Object> _deserializeUsingCreator(JsonParser p, DeserializationContext ctxt) throws IOException {
        String key;
        Object value;
        PropertyBasedCreator creator = this._propertyBasedCreator;
        PropertyValueBuffer buffer = creator.startBuilding(p, ctxt, null);
        JsonDeserializer<Object> valueDes = this._valueDeserializer;
        TypeDeserializer typeDeser = this._valueTypeDeserializer;
        if (p.isExpectedStartObjectToken()) {
            key = p.nextFieldName();
        } else if (p.hasToken(JsonToken.FIELD_NAME)) {
            key = p.getCurrentName();
        } else {
            key = null;
        }
        while (key != null) {
            JsonToken t = p.nextToken();
            if (this._ignorableProperties != null && this._ignorableProperties.contains(key)) {
                p.skipChildren();
            } else {
                SettableBeanProperty prop = creator.findCreatorProperty(key);
                if (prop != null) {
                    if (buffer.assignParameter(prop, prop.deserialize(p, ctxt))) {
                        p.nextToken();
                        try {
                            Map<Object, Object> result = (Map) creator.build(ctxt, buffer);
                            _readAndBind(p, ctxt, result);
                            return result;
                        } catch (Exception e) {
                            wrapAndThrow(e, this._mapType.getRawClass(), key);
                            return null;
                        }
                    }
                } else {
                    Object actualKey = this._keyDeserializer.deserializeKey(key, ctxt);
                    try {
                        if (t == JsonToken.VALUE_NULL) {
                            value = valueDes.getNullValue(ctxt);
                        } else if (typeDeser == null) {
                            value = valueDes.deserialize(p, ctxt);
                        } else {
                            value = valueDes.deserializeWithType(p, ctxt, typeDeser);
                        }
                        buffer.bufferMapProperty(actualKey, value);
                    } catch (Exception e2) {
                        wrapAndThrow(e2, this._mapType.getRawClass(), key);
                        return null;
                    }
                }
            }
            key = p.nextFieldName();
        }
        try {
            return (Map) creator.build(ctxt, buffer);
        } catch (Exception e3) {
            wrapAndThrow(e3, this._mapType.getRawClass(), null);
            return null;
        }
    }

    @Deprecated
    protected void wrapAndThrow(Throwable t, Object ref) throws IOException {
        wrapAndThrow(t, ref, null);
    }

    private void handleUnresolvedReference(JsonParser jp2, MapReferringAccumulator accumulator, Object key, UnresolvedForwardReference reference) throws JsonMappingException {
        if (accumulator == null) {
            throw JsonMappingException.from(jp2, "Unresolved forward reference but no identity info.", reference);
        }
        ReadableObjectId.Referring referring = accumulator.handleUnresolvedReference(reference, key);
        reference.getRoid().appendReferring(referring);
    }

    private static final class MapReferringAccumulator {
        private List<MapReferring> _accumulator = new ArrayList();
        private Map<Object, Object> _result;
        private final Class<?> _valueType;

        public MapReferringAccumulator(Class<?> valueType, Map<Object, Object> result) {
            this._valueType = valueType;
            this._result = result;
        }

        public void put(Object key, Object value) {
            if (this._accumulator.isEmpty()) {
                this._result.put(key, value);
            } else {
                MapReferring ref = this._accumulator.get(this._accumulator.size() - 1);
                ref.next.put(key, value);
            }
        }

        public ReadableObjectId.Referring handleUnresolvedReference(UnresolvedForwardReference reference, Object key) {
            MapReferring id = new MapReferring(this, reference, this._valueType, key);
            this._accumulator.add(id);
            return id;
        }

        public void resolveForwardReference(Object id, Object value) throws IOException {
            Iterator<MapReferring> iterator = this._accumulator.iterator();
            Map<Object, Object> previous = this._result;
            while (iterator.hasNext()) {
                MapReferring ref = iterator.next();
                if (ref.hasId(id)) {
                    iterator.remove();
                    previous.put(ref.key, value);
                    previous.putAll(ref.next);
                    return;
                }
                previous = ref.next;
            }
            throw new IllegalArgumentException("Trying to resolve a forward reference with id [" + id + "] that wasn't previously seen as unresolved.");
        }
    }

    static final class MapReferring extends ReadableObjectId.Referring {
        private final MapReferringAccumulator _parent;
        public final Object key;
        public final Map<Object, Object> next;

        MapReferring(MapReferringAccumulator parent, UnresolvedForwardReference ref, Class<?> valueType, Object key) {
            super(ref, valueType);
            this.next = new LinkedHashMap();
            this._parent = parent;
            this.key = key;
        }

        @Override // com.fasterxml.jackson.databind.deser.impl.ReadableObjectId.Referring
        public void handleResolvedForwardReference(Object id, Object value) throws IOException {
            this._parent.resolveForwardReference(id, value);
        }
    }
}
