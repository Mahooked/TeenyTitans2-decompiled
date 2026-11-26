package com.fasterxml.jackson.databind;

import com.fasterxml.jackson.annotation.ObjectIdGenerator;
import com.fasterxml.jackson.annotation.ObjectIdResolver;
import com.fasterxml.jackson.core.Base64Variant;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.cfg.ContextAttributes;
import com.fasterxml.jackson.databind.deser.ContextualDeserializer;
import com.fasterxml.jackson.databind.deser.ContextualKeyDeserializer;
import com.fasterxml.jackson.databind.deser.DeserializationProblemHandler;
import com.fasterxml.jackson.databind.deser.DeserializerCache;
import com.fasterxml.jackson.databind.deser.DeserializerFactory;
import com.fasterxml.jackson.databind.deser.UnresolvedForwardReference;
import com.fasterxml.jackson.databind.deser.impl.ReadableObjectId;
import com.fasterxml.jackson.databind.deser.impl.TypeWrappedDeserializer;
import com.fasterxml.jackson.databind.exc.InvalidFormatException;
import com.fasterxml.jackson.databind.exc.UnrecognizedPropertyException;
import com.fasterxml.jackson.databind.introspect.Annotated;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import com.fasterxml.jackson.databind.node.JsonNodeFactory;
import com.fasterxml.jackson.databind.type.TypeFactory;
import com.fasterxml.jackson.databind.util.ArrayBuilders;
import com.fasterxml.jackson.databind.util.ClassUtil;
import com.fasterxml.jackson.databind.util.LinkedNode;
import com.fasterxml.jackson.databind.util.ObjectBuffer;
import java.io.IOException;
import java.io.Serializable;
import java.text.DateFormat;
import java.text.ParseException;
import java.util.Calendar;
import java.util.Collection;
import java.util.Date;
import java.util.Locale;
import java.util.TimeZone;
import java.util.concurrent.atomic.AtomicReference;

/* JADX INFO: loaded from: classes.dex */
public abstract class DeserializationContext extends DatabindContext implements Serializable {
    private static final int MAX_ERROR_STR_LEN = 500;
    private static final long serialVersionUID = 1;
    protected transient ArrayBuilders _arrayBuilders;
    protected transient ContextAttributes _attributes;
    protected final DeserializerCache _cache;
    protected final DeserializationConfig _config;
    protected LinkedNode<JavaType> _currentType;
    protected transient DateFormat _dateFormat;
    protected final DeserializerFactory _factory;
    protected final int _featureFlags;
    protected final InjectableValues _injectableValues;
    protected transient ObjectBuffer _objectBuffer;
    protected transient JsonParser _parser;
    protected final Class<?> _view;

    public abstract void checkUnresolvedObjectId() throws UnresolvedForwardReference;

    public abstract JsonDeserializer<Object> deserializerInstance(Annotated annotated, Object obj) throws JsonMappingException;

    @Deprecated
    public abstract ReadableObjectId findObjectId(Object obj, ObjectIdGenerator<?> objectIdGenerator);

    public abstract ReadableObjectId findObjectId(Object obj, ObjectIdGenerator<?> objectIdGenerator, ObjectIdResolver objectIdResolver);

    public abstract KeyDeserializer keyDeserializerInstance(Annotated annotated, Object obj) throws JsonMappingException;

    protected DeserializationContext(DeserializerFactory df) {
        this(df, (DeserializerCache) null);
    }

    protected DeserializationContext(DeserializerFactory df, DeserializerCache cache) {
        if (df == null) {
            throw new IllegalArgumentException("Can not pass null DeserializerFactory");
        }
        this._factory = df;
        this._cache = cache == null ? new DeserializerCache() : cache;
        this._featureFlags = 0;
        this._config = null;
        this._injectableValues = null;
        this._view = null;
        this._attributes = null;
    }

    protected DeserializationContext(DeserializationContext src, DeserializerFactory factory) {
        this._cache = src._cache;
        this._factory = factory;
        this._config = src._config;
        this._featureFlags = src._featureFlags;
        this._view = src._view;
        this._parser = src._parser;
        this._injectableValues = src._injectableValues;
        this._attributes = src._attributes;
    }

    protected DeserializationContext(DeserializationContext src, DeserializationConfig config, JsonParser p, InjectableValues injectableValues) {
        this._cache = src._cache;
        this._factory = src._factory;
        this._config = config;
        this._featureFlags = config.getDeserializationFeatures();
        this._view = config.getActiveView();
        this._parser = p;
        this._injectableValues = injectableValues;
        this._attributes = config.getAttributes();
    }

    protected DeserializationContext(DeserializationContext src) {
        this._cache = new DeserializerCache();
        this._factory = src._factory;
        this._config = src._config;
        this._featureFlags = src._featureFlags;
        this._view = src._view;
        this._injectableValues = null;
    }

    @Override // com.fasterxml.jackson.databind.DatabindContext
    public DeserializationConfig getConfig() {
        return this._config;
    }

    @Override // com.fasterxml.jackson.databind.DatabindContext
    public final Class<?> getActiveView() {
        return this._view;
    }

    @Override // com.fasterxml.jackson.databind.DatabindContext
    public final AnnotationIntrospector getAnnotationIntrospector() {
        return this._config.getAnnotationIntrospector();
    }

    @Override // com.fasterxml.jackson.databind.DatabindContext
    public final TypeFactory getTypeFactory() {
        return this._config.getTypeFactory();
    }

    @Override // com.fasterxml.jackson.databind.DatabindContext
    public Locale getLocale() {
        return this._config.getLocale();
    }

    @Override // com.fasterxml.jackson.databind.DatabindContext
    public TimeZone getTimeZone() {
        return this._config.getTimeZone();
    }

    @Override // com.fasterxml.jackson.databind.DatabindContext
    public Object getAttribute(Object key) {
        return this._attributes.getAttribute(key);
    }

    @Override // com.fasterxml.jackson.databind.DatabindContext
    public DeserializationContext setAttribute(Object key, Object value) {
        this._attributes = this._attributes.withPerCallAttribute(key, value);
        return this;
    }

    public JavaType getContextualType() {
        if (this._currentType == null) {
            return null;
        }
        return this._currentType.value();
    }

    public DeserializerFactory getFactory() {
        return this._factory;
    }

    public final boolean isEnabled(DeserializationFeature feat) {
        return (this._featureFlags & feat.getMask()) != 0;
    }

    public final int getDeserializationFeatures() {
        return this._featureFlags;
    }

    public final boolean hasDeserializationFeatures(int featureMask) {
        return (this._featureFlags & featureMask) == featureMask;
    }

    public final boolean hasSomeOfFeatures(int featureMask) {
        return (this._featureFlags & featureMask) != 0;
    }

    public final JsonParser getParser() {
        return this._parser;
    }

    public final Object findInjectableValue(Object valueId, BeanProperty forProperty, Object beanInstance) {
        if (this._injectableValues == null) {
            throw new IllegalStateException("No 'injectableValues' configured, can not inject value with id [" + valueId + "]");
        }
        return this._injectableValues.findInjectableValue(valueId, this, forProperty, beanInstance);
    }

    public final Base64Variant getBase64Variant() {
        return this._config.getBase64Variant();
    }

    public final JsonNodeFactory getNodeFactory() {
        return this._config.getNodeFactory();
    }

    @Deprecated
    public boolean hasValueDeserializerFor(JavaType type) {
        return hasValueDeserializerFor(type, null);
    }

    public boolean hasValueDeserializerFor(JavaType type, AtomicReference<Throwable> cause) {
        try {
            return this._cache.hasValueDeserializerFor(this, this._factory, type);
        } catch (JsonMappingException e) {
            if (cause != null) {
                cause.set(e);
            }
            return false;
        } catch (RuntimeException e2) {
            if (cause == null) {
                throw e2;
            }
            cause.set(e2);
            return false;
        }
    }

    public final JsonDeserializer<Object> findContextualValueDeserializer(JavaType type, BeanProperty prop) throws JsonMappingException {
        JsonDeserializer<Object> jsonDeserializerFindValueDeserializer = this._cache.findValueDeserializer(this, this._factory, type);
        if (jsonDeserializerFindValueDeserializer != null) {
            return handleSecondaryContextualization(jsonDeserializerFindValueDeserializer, prop, type);
        }
        return jsonDeserializerFindValueDeserializer;
    }

    public final JsonDeserializer<Object> findNonContextualValueDeserializer(JavaType type) throws JsonMappingException {
        return this._cache.findValueDeserializer(this, this._factory, type);
    }

    public final JsonDeserializer<Object> findRootValueDeserializer(JavaType type) throws JsonMappingException {
        JsonDeserializer<Object> jsonDeserializerFindValueDeserializer = this._cache.findValueDeserializer(this, this._factory, type);
        if (jsonDeserializerFindValueDeserializer == null) {
            return null;
        }
        JsonDeserializer<?> jsonDeserializerHandleSecondaryContextualization = handleSecondaryContextualization(jsonDeserializerFindValueDeserializer, null, type);
        TypeDeserializer typeDeser = this._factory.findTypeDeserializer(this._config, type);
        return typeDeser != null ? new TypeWrappedDeserializer(typeDeser.forProperty(null), jsonDeserializerHandleSecondaryContextualization) : jsonDeserializerHandleSecondaryContextualization;
    }

    /* JADX WARN: Multi-variable type inference failed */
    public final KeyDeserializer findKeyDeserializer(JavaType keyType, BeanProperty prop) throws JsonMappingException {
        KeyDeserializer keyDeserializerFindKeyDeserializer = this._cache.findKeyDeserializer(this, this._factory, keyType);
        if (keyDeserializerFindKeyDeserializer instanceof ContextualKeyDeserializer) {
            KeyDeserializer kd = ((ContextualKeyDeserializer) keyDeserializerFindKeyDeserializer).createContextual(this, prop);
            return kd;
        }
        return keyDeserializerFindKeyDeserializer;
    }

    public final JavaType constructType(Class<?> cls) {
        return this._config.constructType(cls);
    }

    public Class<?> findClass(String className) throws ClassNotFoundException {
        return getTypeFactory().findClass(className);
    }

    public final ObjectBuffer leaseObjectBuffer() {
        ObjectBuffer buf = this._objectBuffer;
        if (buf == null) {
            return new ObjectBuffer();
        }
        this._objectBuffer = null;
        return buf;
    }

    public final void returnObjectBuffer(ObjectBuffer buf) {
        if (this._objectBuffer == null || buf.initialCapacity() >= this._objectBuffer.initialCapacity()) {
            this._objectBuffer = buf;
        }
    }

    public final ArrayBuilders getArrayBuilders() {
        if (this._arrayBuilders == null) {
            this._arrayBuilders = new ArrayBuilders();
        }
        return this._arrayBuilders;
    }

    /* JADX WARN: Multi-variable type inference failed */
    public JsonDeserializer<?> handlePrimaryContextualization(JsonDeserializer<?> jsonDeserializer, BeanProperty beanProperty, JavaType javaType) throws JsonMappingException {
        boolean z = jsonDeserializer instanceof ContextualDeserializer;
        JsonDeserializer<?> jsonDeserializerCreateContextual = jsonDeserializer;
        if (z) {
            this._currentType = new LinkedNode<>(javaType, this._currentType);
            try {
            } finally {
                this._currentType = this._currentType.next();
            }
        }
        return jsonDeserializerCreateContextual;
    }

    /* JADX WARN: Multi-variable type inference failed */
    public JsonDeserializer<?> handleSecondaryContextualization(JsonDeserializer<?> jsonDeserializer, BeanProperty beanProperty, JavaType javaType) throws JsonMappingException {
        boolean z = jsonDeserializer instanceof ContextualDeserializer;
        JsonDeserializer<?> jsonDeserializerCreateContextual = jsonDeserializer;
        if (z) {
            this._currentType = new LinkedNode<>(javaType, this._currentType);
            try {
            } finally {
                this._currentType = this._currentType.next();
            }
        }
        return jsonDeserializerCreateContextual;
    }

    @Deprecated
    public JsonDeserializer<?> handlePrimaryContextualization(JsonDeserializer<?> deser, BeanProperty prop) throws JsonMappingException {
        return handlePrimaryContextualization(deser, prop, TypeFactory.unknownType());
    }

    /* JADX WARN: Multi-variable type inference failed */
    @Deprecated
    public JsonDeserializer<?> handleSecondaryContextualization(JsonDeserializer<?> jsonDeserializer, BeanProperty prop) throws JsonMappingException {
        if (jsonDeserializer instanceof ContextualDeserializer) {
            JsonDeserializer<?> deser = ((ContextualDeserializer) jsonDeserializer).createContextual(this, prop);
            return deser;
        }
        return jsonDeserializer;
    }

    public Date parseDate(String dateStr) throws IllegalArgumentException {
        try {
            DateFormat df = getDateFormat();
            return df.parse(dateStr);
        } catch (ParseException e) {
            throw new IllegalArgumentException(String.format("Failed to parse Date value '%s': %s", dateStr, e.getMessage()));
        }
    }

    public Calendar constructCalendar(Date d) {
        Calendar c = Calendar.getInstance(getTimeZone());
        c.setTime(d);
        return c;
    }

    public <T> T readValue(JsonParser jsonParser, Class<T> cls) throws IOException {
        return (T) readValue(jsonParser, getTypeFactory().constructType(cls));
    }

    public <T> T readValue(JsonParser jsonParser, JavaType javaType) throws IOException {
        JsonDeserializer<Object> jsonDeserializerFindRootValueDeserializer = findRootValueDeserializer(javaType);
        if (jsonDeserializerFindRootValueDeserializer == null) {
            throw mappingException("Could not find JsonDeserializer for type %s", javaType);
        }
        return (T) jsonDeserializerFindRootValueDeserializer.deserialize(jsonParser, this);
    }

    public <T> T readPropertyValue(JsonParser jsonParser, BeanProperty beanProperty, Class<T> cls) throws IOException {
        return (T) readPropertyValue(jsonParser, beanProperty, getTypeFactory().constructType(cls));
    }

    public <T> T readPropertyValue(JsonParser jsonParser, BeanProperty beanProperty, JavaType javaType) throws IOException {
        JsonDeserializer<Object> jsonDeserializerFindContextualValueDeserializer = findContextualValueDeserializer(javaType, beanProperty);
        if (jsonDeserializerFindContextualValueDeserializer == null) {
            throw mappingException("Could not find JsonDeserializer for type %s (via property %s)", javaType, beanProperty == null ? "NULL" : "'" + beanProperty.getName() + "'");
        }
        return (T) jsonDeserializerFindContextualValueDeserializer.deserialize(jsonParser, this);
    }

    public boolean handleUnknownProperty(JsonParser p, JsonDeserializer<?> deser, Object instanceOrClass, String propName) throws IOException {
        LinkedNode<DeserializationProblemHandler> h = this._config.getProblemHandlers();
        if (h != null) {
            while (h != null) {
                if (h.value().handleUnknownProperty(this, p, deser, instanceOrClass, propName)) {
                    return true;
                }
                h = h.next();
            }
        }
        return false;
    }

    public void reportUnknownProperty(Object instanceOrClass, String fieldName, JsonDeserializer<?> deser) throws JsonMappingException {
        if (!isEnabled(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES)) {
            return;
        }
        Collection<Object> propIds = deser == null ? null : deser.getKnownPropertyNames();
        throw UnrecognizedPropertyException.from(this._parser, instanceOrClass, fieldName, propIds);
    }

    public JsonMappingException mappingException(Class<?> targetClass) {
        return mappingException(targetClass, this._parser.getCurrentToken());
    }

    public JsonMappingException mappingException(Class<?> targetClass, JsonToken token) {
        return JsonMappingException.from(this._parser, String.format("Can not deserialize instance of %s out of %s token", _calcName(targetClass), token));
    }

    public JsonMappingException mappingException(String message) {
        return JsonMappingException.from(getParser(), message);
    }

    public JsonMappingException mappingException(String msgTemplate, Object... args) {
        String message = String.format(msgTemplate, args);
        return JsonMappingException.from(getParser(), message);
    }

    public JsonMappingException instantiationException(Class<?> instClass, Throwable t) {
        return JsonMappingException.from(this._parser, String.format("Can not construct instance of %s, problem: %s", instClass.getName(), t.getMessage()), t);
    }

    public JsonMappingException instantiationException(Class<?> instClass, String msg) {
        return JsonMappingException.from(this._parser, String.format("Can not construct instance of %s, problem: %s", instClass.getName(), msg));
    }

    public JsonMappingException weirdStringException(String value, Class<?> instClass, String msg) {
        return InvalidFormatException.from(this._parser, String.format("Can not construct instance of %s from String value '%s': %s", instClass.getName(), _valueDesc(), msg), value, instClass);
    }

    public JsonMappingException weirdNumberException(Number value, Class<?> instClass, String msg) {
        return InvalidFormatException.from(this._parser, String.format("Can not construct instance of %s from number value (%s): %s", instClass.getName(), _valueDesc(), msg), null, instClass);
    }

    public JsonMappingException weirdKeyException(Class<?> keyClass, String keyValue, String msg) {
        return InvalidFormatException.from(this._parser, String.format("Can not construct Map key of type %s from String \"%s\": ", keyClass.getName(), _desc(keyValue), msg), keyValue, keyClass);
    }

    public JsonMappingException wrongTokenException(JsonParser p, JsonToken expToken, String msg0) {
        String msg = String.format("Unexpected token (%s), expected %s", p.getCurrentToken(), expToken);
        if (msg0 != null) {
            msg = msg + ": " + msg0;
        }
        return JsonMappingException.from(p, msg);
    }

    @Deprecated
    public JsonMappingException unknownTypeException(JavaType type, String id) {
        return JsonMappingException.from(this._parser, "Could not resolve type id '" + id + "' into a subtype of " + type);
    }

    public JsonMappingException unknownTypeException(JavaType type, String id, String extraDesc) {
        String msg = "Could not resolve type id '" + id + "' into a subtype of " + type;
        if (extraDesc != null) {
            msg = msg + ": " + extraDesc;
        }
        return JsonMappingException.from(this._parser, msg);
    }

    public JsonMappingException endOfInputException(Class<?> instClass) {
        return JsonMappingException.from(this._parser, "Unexpected end-of-input when trying to deserialize a " + instClass.getName());
    }

    protected DateFormat getDateFormat() {
        if (this._dateFormat != null) {
            return this._dateFormat;
        }
        DateFormat df = (DateFormat) this._config.getDateFormat().clone();
        this._dateFormat = df;
        return df;
    }

    protected String determineClassName(Object instance) {
        return ClassUtil.getClassDescription(instance);
    }

    protected String _calcName(Class<?> cls) {
        return cls.isArray() ? _calcName(cls.getComponentType()) + "[]" : cls.getName();
    }

    protected String _valueDesc() {
        try {
            return _desc(this._parser.getText());
        } catch (Exception e) {
            return "[N/A]";
        }
    }

    protected String _desc(String desc) {
        if (desc.length() > 500) {
            return desc.substring(0, 500) + "]...[" + desc.substring(desc.length() - 500);
        }
        return desc;
    }
}
