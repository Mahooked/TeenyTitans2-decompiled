package com.fasterxml.jackson.databind.introspect;

import com.fasterxml.jackson.databind.AnnotationIntrospector;
import com.fasterxml.jackson.databind.BeanDescription;
import com.fasterxml.jackson.databind.DeserializationConfig;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.SerializationConfig;
import com.fasterxml.jackson.databind.annotation.JsonPOJOBuilder;
import com.fasterxml.jackson.databind.cfg.MapperConfig;
import com.fasterxml.jackson.databind.type.SimpleType;
import com.fasterxml.jackson.databind.util.LRUMap;
import java.io.Serializable;
import java.util.Collection;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public class BasicClassIntrospector extends ClassIntrospector implements Serializable {
    protected static final BasicBeanDescription BOOLEAN_DESC;
    protected static final BasicBeanDescription INT_DESC;
    protected static final BasicBeanDescription LONG_DESC;
    protected static final BasicBeanDescription STRING_DESC;

    @Deprecated
    public static final BasicClassIntrospector instance;
    private static final long serialVersionUID = 1;
    protected final LRUMap<JavaType, BasicBeanDescription> _cachedFCA = new LRUMap<>(16, 64);

    @Override // com.fasterxml.jackson.databind.introspect.ClassIntrospector
    public /* bridge */ /* synthetic */ BeanDescription forClassAnnotations(MapperConfig x0, JavaType x1, ClassIntrospector.MixInResolver x2) {
        return forClassAnnotations((MapperConfig<?>) x0, x1, x2);
    }

    @Override // com.fasterxml.jackson.databind.introspect.ClassIntrospector
    public /* bridge */ /* synthetic */ BeanDescription forDirectClassAnnotations(MapperConfig x0, JavaType x1, ClassIntrospector.MixInResolver x2) {
        return forDirectClassAnnotations((MapperConfig<?>) x0, x1, x2);
    }

    static {
        AnnotatedClass ac = AnnotatedClass.constructWithoutSuperTypes(String.class, null, null);
        STRING_DESC = BasicBeanDescription.forOtherUse(null, SimpleType.constructUnsafe(String.class), ac);
        AnnotatedClass ac2 = AnnotatedClass.constructWithoutSuperTypes(Boolean.TYPE, null, null);
        BOOLEAN_DESC = BasicBeanDescription.forOtherUse(null, SimpleType.constructUnsafe(Boolean.TYPE), ac2);
        AnnotatedClass ac3 = AnnotatedClass.constructWithoutSuperTypes(Integer.TYPE, null, null);
        INT_DESC = BasicBeanDescription.forOtherUse(null, SimpleType.constructUnsafe(Integer.TYPE), ac3);
        AnnotatedClass ac4 = AnnotatedClass.constructWithoutSuperTypes(Long.TYPE, null, null);
        LONG_DESC = BasicBeanDescription.forOtherUse(null, SimpleType.constructUnsafe(Long.TYPE), ac4);
        instance = new BasicClassIntrospector();
    }

    @Override // com.fasterxml.jackson.databind.introspect.ClassIntrospector
    public BasicBeanDescription forSerialization(SerializationConfig cfg, JavaType type, ClassIntrospector.MixInResolver r) {
        BasicBeanDescription desc = _findStdTypeDesc(type);
        if (desc == null) {
            desc = _findStdJdkCollectionDesc(cfg, type, r);
            if (desc == null) {
                desc = BasicBeanDescription.forSerialization(collectProperties(cfg, type, r, true, "set"));
            }
            this._cachedFCA.putIfAbsent(type, desc);
        }
        return desc;
    }

    @Override // com.fasterxml.jackson.databind.introspect.ClassIntrospector
    public BasicBeanDescription forDeserialization(DeserializationConfig cfg, JavaType type, ClassIntrospector.MixInResolver r) {
        BasicBeanDescription desc = _findStdTypeDesc(type);
        if (desc == null) {
            desc = _findStdJdkCollectionDesc(cfg, type, r);
            if (desc == null) {
                desc = BasicBeanDescription.forDeserialization(collectProperties(cfg, type, r, false, "set"));
            }
            this._cachedFCA.putIfAbsent(type, desc);
        }
        return desc;
    }

    @Override // com.fasterxml.jackson.databind.introspect.ClassIntrospector
    public BasicBeanDescription forDeserializationWithBuilder(DeserializationConfig cfg, JavaType type, ClassIntrospector.MixInResolver r) {
        BasicBeanDescription desc = BasicBeanDescription.forDeserialization(collectPropertiesWithBuilder(cfg, type, r, false));
        this._cachedFCA.putIfAbsent(type, desc);
        return desc;
    }

    @Override // com.fasterxml.jackson.databind.introspect.ClassIntrospector
    public BasicBeanDescription forCreation(DeserializationConfig cfg, JavaType type, ClassIntrospector.MixInResolver r) {
        BasicBeanDescription desc = _findStdTypeDesc(type);
        if (desc == null) {
            BasicBeanDescription desc2 = _findStdJdkCollectionDesc(cfg, type, r);
            if (desc2 == null) {
                return BasicBeanDescription.forDeserialization(collectProperties(cfg, type, r, false, "set"));
            }
            return desc2;
        }
        return desc;
    }

    @Override // com.fasterxml.jackson.databind.introspect.ClassIntrospector
    public BasicBeanDescription forClassAnnotations(MapperConfig<?> cfg, JavaType type, ClassIntrospector.MixInResolver r) {
        BasicBeanDescription desc = _findStdTypeDesc(type);
        if (desc == null) {
            BasicBeanDescription desc2 = this._cachedFCA.get(type);
            if (desc2 == null) {
                boolean useAnnotations = cfg.isAnnotationProcessingEnabled();
                AnnotatedClass ac = AnnotatedClass.construct(type.getRawClass(), useAnnotations ? cfg.getAnnotationIntrospector() : null, r);
                BasicBeanDescription desc3 = BasicBeanDescription.forOtherUse(cfg, type, ac);
                this._cachedFCA.put(type, desc3);
                return desc3;
            }
            return desc2;
        }
        return desc;
    }

    @Override // com.fasterxml.jackson.databind.introspect.ClassIntrospector
    public BasicBeanDescription forDirectClassAnnotations(MapperConfig<?> cfg, JavaType type, ClassIntrospector.MixInResolver r) {
        BasicBeanDescription desc = _findStdTypeDesc(type);
        if (desc == null) {
            boolean useAnnotations = cfg.isAnnotationProcessingEnabled();
            AnnotationIntrospector ai = cfg.getAnnotationIntrospector();
            Class<?> rawClass = type.getRawClass();
            if (!useAnnotations) {
                ai = null;
            }
            AnnotatedClass ac = AnnotatedClass.constructWithoutSuperTypes(rawClass, ai, r);
            return BasicBeanDescription.forOtherUse(cfg, type, ac);
        }
        return desc;
    }

    protected POJOPropertiesCollector collectProperties(MapperConfig<?> config, JavaType type, ClassIntrospector.MixInResolver r, boolean forSerialization, String mutatorPrefix) {
        boolean useAnnotations = config.isAnnotationProcessingEnabled();
        AnnotatedClass ac = AnnotatedClass.construct(type.getRawClass(), useAnnotations ? config.getAnnotationIntrospector() : null, r);
        return constructPropertyCollector(config, ac, type, forSerialization, mutatorPrefix);
    }

    protected POJOPropertiesCollector collectPropertiesWithBuilder(MapperConfig<?> config, JavaType type, ClassIntrospector.MixInResolver r, boolean forSerialization) {
        boolean useAnnotations = config.isAnnotationProcessingEnabled();
        AnnotationIntrospector ai = useAnnotations ? config.getAnnotationIntrospector() : null;
        AnnotatedClass ac = AnnotatedClass.construct(type.getRawClass(), ai, r);
        JsonPOJOBuilder.Value builderConfig = ai == null ? null : ai.findPOJOBuilderConfig(ac);
        String mutatorPrefix = builderConfig == null ? "with" : builderConfig.withPrefix;
        return constructPropertyCollector(config, ac, type, forSerialization, mutatorPrefix);
    }

    protected POJOPropertiesCollector constructPropertyCollector(MapperConfig<?> config, AnnotatedClass ac, JavaType type, boolean forSerialization, String mutatorPrefix) {
        return new POJOPropertiesCollector(config, forSerialization, type, ac, mutatorPrefix);
    }

    protected BasicBeanDescription _findStdTypeDesc(JavaType type) {
        Class<?> cls = type.getRawClass();
        if (cls.isPrimitive()) {
            if (cls == Boolean.TYPE) {
                return BOOLEAN_DESC;
            }
            if (cls == Integer.TYPE) {
                return INT_DESC;
            }
            if (cls == Long.TYPE) {
                return LONG_DESC;
            }
        } else if (cls == String.class) {
            return STRING_DESC;
        }
        return null;
    }

    protected boolean _isStdJDKCollection(JavaType type) {
        Class<?> raw;
        Package pkg;
        if (!type.isContainerType() || type.isArrayType() || (pkg = (raw = type.getRawClass()).getPackage()) == null) {
            return false;
        }
        String pkgName = pkg.getName();
        if (pkgName.startsWith("java.lang") || pkgName.startsWith("java.util")) {
            return Collection.class.isAssignableFrom(raw) || Map.class.isAssignableFrom(raw);
        }
        return false;
    }

    protected BasicBeanDescription _findStdJdkCollectionDesc(MapperConfig<?> cfg, JavaType type, ClassIntrospector.MixInResolver r) {
        if (!_isStdJDKCollection(type)) {
            return null;
        }
        AnnotatedClass ac = AnnotatedClass.construct(type.getRawClass(), cfg.isAnnotationProcessingEnabled() ? cfg.getAnnotationIntrospector() : null, r);
        return BasicBeanDescription.forOtherUse(cfg, type, ac);
    }
}
