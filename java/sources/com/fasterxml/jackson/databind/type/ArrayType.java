package com.fasterxml.jackson.databind.type;

import com.fasterxml.jackson.databind.JavaType;
import java.lang.reflect.Array;

/* JADX INFO: loaded from: classes.dex */
public final class ArrayType extends TypeBase {
    private static final long serialVersionUID = 1;
    protected final JavaType _componentType;
    protected final Object _emptyArray;

    protected ArrayType(JavaType componentType, Object emptyInstance, Object valueHandler, Object typeHandler, boolean asStatic) {
        super(emptyInstance.getClass(), componentType.hashCode(), valueHandler, typeHandler, asStatic);
        this._componentType = componentType;
        this._emptyArray = emptyInstance;
    }

    public static ArrayType construct(JavaType componentType, Object valueHandler, Object typeHandler) {
        Object emptyInstance = Array.newInstance(componentType.getRawClass(), 0);
        return new ArrayType(componentType, emptyInstance, null, null, false);
    }

    @Override // com.fasterxml.jackson.databind.JavaType
    public ArrayType withTypeHandler(Object h) {
        return h == this._typeHandler ? this : new ArrayType(this._componentType, this._emptyArray, this._valueHandler, h, this._asStatic);
    }

    @Override // com.fasterxml.jackson.databind.JavaType
    public ArrayType withContentTypeHandler(Object h) {
        return h == this._componentType.getTypeHandler() ? this : new ArrayType(this._componentType.withTypeHandler(h), this._emptyArray, this._valueHandler, this._typeHandler, this._asStatic);
    }

    @Override // com.fasterxml.jackson.databind.JavaType
    public ArrayType withValueHandler(Object h) {
        return h == this._valueHandler ? this : new ArrayType(this._componentType, this._emptyArray, h, this._typeHandler, this._asStatic);
    }

    @Override // com.fasterxml.jackson.databind.JavaType
    public ArrayType withContentValueHandler(Object h) {
        return h == this._componentType.getValueHandler() ? this : new ArrayType(this._componentType.withValueHandler(h), this._emptyArray, this._valueHandler, this._typeHandler, this._asStatic);
    }

    @Override // com.fasterxml.jackson.databind.JavaType
    public ArrayType withStaticTyping() {
        return this._asStatic ? this : new ArrayType(this._componentType.withStaticTyping(), this._emptyArray, this._valueHandler, this._typeHandler, true);
    }

    @Override // com.fasterxml.jackson.databind.type.TypeBase
    protected String buildCanonicalName() {
        return this._class.getName();
    }

    @Override // com.fasterxml.jackson.databind.JavaType
    protected JavaType _narrow(Class<?> subclass) {
        if (!subclass.isArray()) {
            throw new IllegalArgumentException("Incompatible narrowing operation: trying to narrow " + toString() + " to class " + subclass.getName());
        }
        Class<?> newCompClass = subclass.getComponentType();
        JavaType newCompType = TypeFactory.defaultInstance().constructType(newCompClass);
        return construct(newCompType, this._valueHandler, this._typeHandler);
    }

    @Override // com.fasterxml.jackson.databind.JavaType
    public JavaType narrowContentsBy(Class<?> contentClass) {
        return contentClass == this._componentType.getRawClass() ? this : construct(this._componentType.narrowBy(contentClass), this._valueHandler, this._typeHandler);
    }

    @Override // com.fasterxml.jackson.databind.JavaType
    public JavaType widenContentsBy(Class<?> contentClass) {
        return contentClass == this._componentType.getRawClass() ? this : construct(this._componentType.widenBy(contentClass), this._valueHandler, this._typeHandler);
    }

    @Override // com.fasterxml.jackson.databind.JavaType, com.fasterxml.jackson.core.type.ResolvedType
    public boolean isArrayType() {
        return true;
    }

    @Override // com.fasterxml.jackson.databind.JavaType, com.fasterxml.jackson.core.type.ResolvedType
    public boolean isAbstract() {
        return false;
    }

    @Override // com.fasterxml.jackson.databind.JavaType, com.fasterxml.jackson.core.type.ResolvedType
    public boolean isConcrete() {
        return true;
    }

    @Override // com.fasterxml.jackson.databind.JavaType, com.fasterxml.jackson.core.type.ResolvedType
    public boolean hasGenericTypes() {
        return this._componentType.hasGenericTypes();
    }

    @Override // com.fasterxml.jackson.databind.JavaType, com.fasterxml.jackson.core.type.ResolvedType
    public String containedTypeName(int index) {
        if (index == 0) {
            return "E";
        }
        return null;
    }

    @Override // com.fasterxml.jackson.databind.JavaType, com.fasterxml.jackson.core.type.ResolvedType
    public Class<?> getParameterSource() {
        return null;
    }

    @Override // com.fasterxml.jackson.databind.JavaType, com.fasterxml.jackson.core.type.ResolvedType
    public boolean isContainerType() {
        return true;
    }

    @Override // com.fasterxml.jackson.databind.JavaType, com.fasterxml.jackson.core.type.ResolvedType
    public JavaType getContentType() {
        return this._componentType;
    }

    @Override // com.fasterxml.jackson.databind.JavaType, com.fasterxml.jackson.core.type.ResolvedType
    public int containedTypeCount() {
        return 1;
    }

    @Override // com.fasterxml.jackson.databind.JavaType, com.fasterxml.jackson.core.type.ResolvedType
    public JavaType containedType(int index) {
        if (index == 0) {
            return this._componentType;
        }
        return null;
    }

    @Override // com.fasterxml.jackson.databind.type.TypeBase, com.fasterxml.jackson.databind.JavaType
    public StringBuilder getGenericSignature(StringBuilder sb) {
        sb.append('[');
        return this._componentType.getGenericSignature(sb);
    }

    @Override // com.fasterxml.jackson.databind.type.TypeBase, com.fasterxml.jackson.databind.JavaType
    public StringBuilder getErasedSignature(StringBuilder sb) {
        sb.append('[');
        return this._componentType.getErasedSignature(sb);
    }

    @Override // com.fasterxml.jackson.databind.JavaType
    public String toString() {
        return "[array type, component type: " + this._componentType + "]";
    }

    @Override // com.fasterxml.jackson.databind.JavaType
    public boolean equals(Object o) {
        if (o == this) {
            return true;
        }
        if (o == null || o.getClass() != getClass()) {
            return false;
        }
        ArrayType other = (ArrayType) o;
        return this._componentType.equals(other._componentType);
    }
}
