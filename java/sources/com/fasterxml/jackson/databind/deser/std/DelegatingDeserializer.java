package com.fasterxml.jackson.databind.deser.std;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.deser.ContextualDeserializer;
import com.fasterxml.jackson.databind.deser.ResolvableDeserializer;
import com.fasterxml.jackson.databind.deser.SettableBeanProperty;
import com.fasterxml.jackson.databind.deser.impl.ObjectIdReader;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import java.io.IOException;
import java.util.Collection;

/* JADX INFO: loaded from: classes.dex */
public abstract class DelegatingDeserializer extends StdDeserializer<Object> implements ContextualDeserializer, ResolvableDeserializer {
    private static final long serialVersionUID = 1;
    protected final JsonDeserializer<?> _delegatee;

    protected abstract JsonDeserializer<?> newDelegatingInstance(JsonDeserializer<?> jsonDeserializer);

    public DelegatingDeserializer(JsonDeserializer<?> delegatee) {
        super(_figureType(delegatee));
        this._delegatee = delegatee;
    }

    private static Class<?> _figureType(JsonDeserializer<?> deser) {
        Class<?> cls = deser.handledType();
        return cls != null ? cls : Object.class;
    }

    @Override // com.fasterxml.jackson.databind.deser.ResolvableDeserializer
    public void resolve(DeserializationContext ctxt) throws JsonMappingException {
        if (this._delegatee instanceof ResolvableDeserializer) {
            ((ResolvableDeserializer) this._delegatee).resolve(ctxt);
        }
    }

    @Override // com.fasterxml.jackson.databind.deser.ContextualDeserializer
    public JsonDeserializer<?> createContextual(DeserializationContext ctxt, BeanProperty property) throws JsonMappingException {
        JavaType vt = ctxt.constructType(this._delegatee.handledType());
        JsonDeserializer<?> del = ctxt.handleSecondaryContextualization(this._delegatee, property, vt);
        return del == this._delegatee ? this : newDelegatingInstance(del);
    }

    @Deprecated
    protected JsonDeserializer<?> _createContextual(DeserializationContext ctxt, BeanProperty property, JsonDeserializer<?> newDelegatee) {
        return newDelegatee == this._delegatee ? this : newDelegatingInstance(newDelegatee);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public SettableBeanProperty findBackReference(String logicalName) {
        return this._delegatee.findBackReference(logicalName);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Object deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        return this._delegatee.deserialize(jp2, ctxt);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Object deserialize(JsonParser jp2, DeserializationContext ctxt, Object intoValue) throws IOException {
        return this._delegatee.deserialize(jp2, ctxt, intoValue);
    }

    @Override // com.fasterxml.jackson.databind.deser.std.StdDeserializer, com.fasterxml.jackson.databind.JsonDeserializer
    public Object deserializeWithType(JsonParser jp2, DeserializationContext ctxt, TypeDeserializer typeDeserializer) throws IOException {
        return this._delegatee.deserializeWithType(jp2, ctxt, typeDeserializer);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public JsonDeserializer<?> replaceDelegatee(JsonDeserializer<?> delegatee) {
        return delegatee == this._delegatee ? this : newDelegatingInstance(delegatee);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Object getNullValue(DeserializationContext ctxt) throws JsonMappingException {
        return this._delegatee.getNullValue(ctxt);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Object getEmptyValue(DeserializationContext ctxt) throws JsonMappingException {
        return this._delegatee.getEmptyValue(ctxt);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    @Deprecated
    public Object getNullValue() {
        return this._delegatee.getNullValue();
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    @Deprecated
    public Object getEmptyValue() {
        return this._delegatee.getEmptyValue();
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Collection<Object> getKnownPropertyNames() {
        return this._delegatee.getKnownPropertyNames();
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public boolean isCachable() {
        return this._delegatee.isCachable();
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public ObjectIdReader getObjectIdReader() {
        return this._delegatee.getObjectIdReader();
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public JsonDeserializer<?> getDelegatee() {
        return this._delegatee;
    }
}
