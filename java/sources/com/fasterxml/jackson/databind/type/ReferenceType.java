package com.fasterxml.jackson.databind.type;

import com.fasterxml.jackson.databind.JavaType;

/* JADX INFO: loaded from: classes.dex */
public class ReferenceType extends SimpleType {
    private static final long serialVersionUID = 1;
    protected final JavaType _referencedType;

    protected ReferenceType(Class<?> cls, JavaType refType, Object valueHandler, Object typeHandler, boolean asStatic) {
        super(cls, refType.hashCode(), valueHandler, typeHandler, asStatic);
        this._referencedType = refType;
    }

    public static ReferenceType construct(Class<?> cls, JavaType refType, Object valueHandler, Object typeHandler) {
        return new ReferenceType(cls, refType, null, null, false);
    }

    @Override // com.fasterxml.jackson.databind.type.SimpleType, com.fasterxml.jackson.databind.JavaType
    public ReferenceType withTypeHandler(Object h) {
        return h == this._typeHandler ? this : new ReferenceType(this._class, this._referencedType, this._valueHandler, h, this._asStatic);
    }

    @Override // com.fasterxml.jackson.databind.type.SimpleType, com.fasterxml.jackson.databind.JavaType
    public ReferenceType withContentTypeHandler(Object h) {
        return h == this._referencedType.getTypeHandler() ? this : new ReferenceType(this._class, this._referencedType.withTypeHandler(h), this._valueHandler, this._typeHandler, this._asStatic);
    }

    @Override // com.fasterxml.jackson.databind.type.SimpleType, com.fasterxml.jackson.databind.JavaType
    public ReferenceType withValueHandler(Object h) {
        return h == this._valueHandler ? this : new ReferenceType(this._class, this._referencedType, h, this._typeHandler, this._asStatic);
    }

    @Override // com.fasterxml.jackson.databind.type.SimpleType, com.fasterxml.jackson.databind.JavaType
    public ReferenceType withContentValueHandler(Object h) {
        return h == this._referencedType.getValueHandler() ? this : new ReferenceType(this._class, this._referencedType.withValueHandler(h), this._valueHandler, this._typeHandler, this._asStatic);
    }

    @Override // com.fasterxml.jackson.databind.type.SimpleType, com.fasterxml.jackson.databind.JavaType
    public ReferenceType withStaticTyping() {
        return this._asStatic ? this : new ReferenceType(this._class, this._referencedType.withStaticTyping(), this._valueHandler, this._typeHandler, true);
    }

    @Override // com.fasterxml.jackson.databind.type.SimpleType, com.fasterxml.jackson.databind.type.TypeBase
    protected String buildCanonicalName() {
        return this._class.getName() + '<' + this._referencedType.toCanonical();
    }

    @Override // com.fasterxml.jackson.databind.type.SimpleType, com.fasterxml.jackson.databind.JavaType
    protected JavaType _narrow(Class<?> subclass) {
        return new ReferenceType(subclass, this._referencedType, this._valueHandler, this._typeHandler, this._asStatic);
    }

    @Override // com.fasterxml.jackson.databind.JavaType, com.fasterxml.jackson.core.type.ResolvedType
    public JavaType getReferencedType() {
        return this._referencedType;
    }

    @Override // com.fasterxml.jackson.core.type.ResolvedType
    public boolean isReferenceType() {
        return true;
    }

    @Override // com.fasterxml.jackson.databind.type.SimpleType, com.fasterxml.jackson.databind.JavaType, com.fasterxml.jackson.core.type.ResolvedType
    public int containedTypeCount() {
        return 1;
    }

    @Override // com.fasterxml.jackson.databind.type.SimpleType, com.fasterxml.jackson.databind.JavaType, com.fasterxml.jackson.core.type.ResolvedType
    public JavaType containedType(int index) {
        if (index == 0) {
            return this._referencedType;
        }
        return null;
    }

    @Override // com.fasterxml.jackson.databind.type.SimpleType, com.fasterxml.jackson.databind.JavaType, com.fasterxml.jackson.core.type.ResolvedType
    public String containedTypeName(int index) {
        if (index == 0) {
            return "T";
        }
        return null;
    }

    @Override // com.fasterxml.jackson.databind.type.SimpleType, com.fasterxml.jackson.databind.JavaType, com.fasterxml.jackson.core.type.ResolvedType
    public Class<?> getParameterSource() {
        return this._class;
    }

    @Override // com.fasterxml.jackson.databind.type.SimpleType, com.fasterxml.jackson.databind.type.TypeBase, com.fasterxml.jackson.databind.JavaType
    public StringBuilder getErasedSignature(StringBuilder sb) {
        return _classSignature(this._class, sb, true);
    }

    @Override // com.fasterxml.jackson.databind.type.SimpleType, com.fasterxml.jackson.databind.type.TypeBase, com.fasterxml.jackson.databind.JavaType
    public StringBuilder getGenericSignature(StringBuilder sb) {
        _classSignature(this._class, sb, false);
        sb.append('<');
        StringBuilder sb2 = this._referencedType.getGenericSignature(sb);
        sb2.append(';');
        return sb2;
    }

    @Override // com.fasterxml.jackson.databind.type.SimpleType, com.fasterxml.jackson.databind.JavaType
    public String toString() {
        return new StringBuilder(40).append("[reference type, class ").append(buildCanonicalName()).append('<').append(this._referencedType).append('>').append(']').toString();
    }

    @Override // com.fasterxml.jackson.databind.type.SimpleType, com.fasterxml.jackson.databind.JavaType
    public boolean equals(Object o) {
        if (o == this) {
            return true;
        }
        if (o == null || o.getClass() != getClass()) {
            return false;
        }
        ReferenceType other = (ReferenceType) o;
        if (other._class == this._class) {
            return this._referencedType.equals(other._referencedType);
        }
        return false;
    }
}
