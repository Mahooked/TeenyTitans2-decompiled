package com.fasterxml.jackson.databind.ser;

import com.fasterxml.jackson.annotation.JsonInclude;
import com.fasterxml.jackson.databind.AnnotationIntrospector;
import com.fasterxml.jackson.databind.BeanDescription;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonSerializer;
import com.fasterxml.jackson.databind.SerializationConfig;
import com.fasterxml.jackson.databind.SerializationFeature;
import com.fasterxml.jackson.databind.SerializerProvider;
import com.fasterxml.jackson.databind.annotation.JsonSerialize;
import com.fasterxml.jackson.databind.introspect.Annotated;
import com.fasterxml.jackson.databind.introspect.AnnotatedMember;
import com.fasterxml.jackson.databind.introspect.BeanPropertyDefinition;
import com.fasterxml.jackson.databind.jsontype.TypeSerializer;
import com.fasterxml.jackson.databind.util.Annotations;
import com.fasterxml.jackson.databind.util.ArrayBuilders;
import com.fasterxml.jackson.databind.util.NameTransformer;

/* JADX INFO: loaded from: classes.dex */
public class PropertyBuilder {
    protected final AnnotationIntrospector _annotationIntrospector;
    protected final BeanDescription _beanDesc;
    protected final SerializationConfig _config;
    protected Object _defaultBean;
    protected final JsonInclude.Include _defaultInclusion;

    public PropertyBuilder(SerializationConfig config, BeanDescription beanDesc) {
        this._config = config;
        this._beanDesc = beanDesc;
        this._defaultInclusion = beanDesc.findSerializationInclusion(config.getSerializationInclusion());
        this._annotationIntrospector = this._config.getAnnotationIntrospector();
    }

    public Annotations getClassAnnotations() {
        return this._beanDesc.getClassAnnotations();
    }

    protected BeanPropertyWriter buildWriter(SerializerProvider prov, BeanPropertyDefinition propDef, JavaType declaredType, JsonSerializer<?> ser, TypeSerializer typeSer, TypeSerializer contentTypeSer, AnnotatedMember am, boolean defaultUseStaticTyping) throws JsonMappingException {
        JavaType serializationType = findSerializationType(am, defaultUseStaticTyping, declaredType);
        if (contentTypeSer != null) {
            if (serializationType == null) {
                serializationType = declaredType;
            }
            JavaType ct = serializationType.getContentType();
            if (ct == null) {
                throw new IllegalStateException("Problem trying to create BeanPropertyWriter for property '" + propDef.getName() + "' (of type " + this._beanDesc.getType() + "); serialization type " + serializationType + " has no content");
            }
            serializationType = serializationType.withContentTypeHandler(contentTypeSer);
            serializationType.getContentType();
        }
        Object valueToSuppress = null;
        boolean suppressNulls = false;
        JsonInclude.Include inclusion = propDef.findInclusion();
        if ((inclusion == null || inclusion == JsonInclude.Include.USE_DEFAULTS) && (inclusion = this._defaultInclusion) == null) {
            inclusion = JsonInclude.Include.ALWAYS;
        }
        switch (inclusion) {
            case NON_DEFAULT:
                valueToSuppress = getDefaultValue(propDef.getName(), am);
                if (valueToSuppress == null) {
                    suppressNulls = true;
                } else if (valueToSuppress.getClass().isArray()) {
                    valueToSuppress = ArrayBuilders.getArrayComparator(valueToSuppress);
                }
                break;
            case NON_ABSENT:
                suppressNulls = true;
                if (declaredType.isReferenceType()) {
                    valueToSuppress = BeanPropertyWriter.MARKER_FOR_EMPTY;
                }
                break;
            case NON_EMPTY:
                suppressNulls = true;
                valueToSuppress = BeanPropertyWriter.MARKER_FOR_EMPTY;
                break;
            case NON_NULL:
                suppressNulls = true;
            default:
                if (declaredType.isContainerType() && !this._config.isEnabled(SerializationFeature.WRITE_EMPTY_JSON_ARRAYS)) {
                    valueToSuppress = BeanPropertyWriter.MARKER_FOR_EMPTY;
                }
                break;
        }
        BeanPropertyWriter bpw = new BeanPropertyWriter(propDef, am, this._beanDesc.getClassAnnotations(), declaredType, ser, typeSer, serializationType, suppressNulls, valueToSuppress);
        Object serDef = this._annotationIntrospector.findNullSerializer(am);
        if (serDef != null) {
            bpw.assignNullSerializer(prov.serializerInstance(am, serDef));
        }
        NameTransformer unwrapper = this._annotationIntrospector.findUnwrappingNameTransformer(am);
        if (unwrapper != null) {
            return bpw.unwrappingWriter(unwrapper);
        }
        return bpw;
    }

    protected JavaType findSerializationType(Annotated a, boolean useStaticTyping, JavaType declaredType) {
        Class<?> serClass = this._annotationIntrospector.findSerializationType(a);
        if (serClass != null) {
            Class<?> rawDeclared = declaredType.getRawClass();
            if (serClass.isAssignableFrom(rawDeclared)) {
                declaredType = declaredType.widenBy(serClass);
            } else {
                if (!rawDeclared.isAssignableFrom(serClass)) {
                    throw new IllegalArgumentException("Illegal concrete-type annotation for method '" + a.getName() + "': class " + serClass.getName() + " not a super-type of (declared) class " + rawDeclared.getName());
                }
                declaredType = this._config.constructSpecializedType(declaredType, serClass);
            }
            useStaticTyping = true;
        }
        JavaType secondary = BasicSerializerFactory.modifySecondaryTypesByAnnotation(this._config, a, declaredType);
        if (secondary != declaredType) {
            useStaticTyping = true;
            declaredType = secondary;
        }
        JsonSerialize.Typing typing = this._annotationIntrospector.findSerializationTyping(a);
        if (typing != null && typing != JsonSerialize.Typing.DEFAULT_TYPING) {
            useStaticTyping = typing == JsonSerialize.Typing.STATIC;
        }
        if (useStaticTyping) {
            return declaredType;
        }
        return null;
    }

    protected Object getDefaultBean() {
        if (this._defaultBean == null) {
            this._defaultBean = this._beanDesc.instantiateBean(this._config.canOverrideAccessModifiers());
            if (this._defaultBean == null) {
                Class<?> cls = this._beanDesc.getClassInfo().getAnnotated();
                throw new IllegalArgumentException("Class " + cls.getName() + " has no default constructor; can not instantiate default bean value to support 'properties=JsonSerialize.Inclusion.NON_DEFAULT' annotation");
            }
        }
        return this._defaultBean;
    }

    protected Object getDefaultValue(String name, AnnotatedMember member) {
        Object defaultBean = getDefaultBean();
        try {
            return member.getValue(defaultBean);
        } catch (Exception e) {
            return _throwWrapped(e, name, defaultBean);
        }
    }

    protected Object _throwWrapped(Exception e, String propName, Object defaultBean) throws Throwable {
        Throwable t = e;
        while (t.getCause() != null) {
            t = t.getCause();
        }
        if (t instanceof Error) {
            throw ((Error) t);
        }
        if (t instanceof RuntimeException) {
            throw ((RuntimeException) t);
        }
        throw new IllegalArgumentException("Failed to get property '" + propName + "' of default " + defaultBean.getClass().getName() + " instance");
    }
}
