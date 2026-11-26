package com.fasterxml.jackson.databind.cfg;

import com.fasterxml.jackson.annotation.JsonAutoDetect;
import com.fasterxml.jackson.annotation.PropertyAccessor;
import com.fasterxml.jackson.core.Base64Variant;
import com.fasterxml.jackson.databind.AnnotationIntrospector;
import com.fasterxml.jackson.databind.PropertyNamingStrategy;
import com.fasterxml.jackson.databind.introspect.AnnotationIntrospectorPair;
import com.fasterxml.jackson.databind.introspect.ClassIntrospector;
import com.fasterxml.jackson.databind.introspect.VisibilityChecker;
import com.fasterxml.jackson.databind.jsontype.TypeResolverBuilder;
import com.fasterxml.jackson.databind.type.TypeFactory;
import com.fasterxml.jackson.databind.util.StdDateFormat;
import java.io.Serializable;
import java.text.DateFormat;
import java.util.Locale;
import java.util.TimeZone;

/* JADX INFO: loaded from: classes.dex */
public final class BaseSettings implements Serializable {
    private static final TimeZone DEFAULT_TIMEZONE = TimeZone.getTimeZone("GMT");
    private static final long serialVersionUID = 1;
    protected final AnnotationIntrospector _annotationIntrospector;
    protected final ClassIntrospector _classIntrospector;
    protected final DateFormat _dateFormat;
    protected final Base64Variant _defaultBase64;
    protected final HandlerInstantiator _handlerInstantiator;
    protected final Locale _locale;
    protected final PropertyNamingStrategy _propertyNamingStrategy;
    protected final TimeZone _timeZone;
    protected final TypeFactory _typeFactory;
    protected final TypeResolverBuilder<?> _typeResolverBuilder;
    protected final VisibilityChecker<?> _visibilityChecker;

    public BaseSettings(ClassIntrospector ci, AnnotationIntrospector ai, VisibilityChecker<?> vc, PropertyNamingStrategy pns, TypeFactory tf, TypeResolverBuilder<?> typer, DateFormat dateFormat, HandlerInstantiator hi, Locale locale, TimeZone tz, Base64Variant defaultBase64) {
        this._classIntrospector = ci;
        this._annotationIntrospector = ai;
        this._visibilityChecker = vc;
        this._propertyNamingStrategy = pns;
        this._typeFactory = tf;
        this._typeResolverBuilder = typer;
        this._dateFormat = dateFormat;
        this._handlerInstantiator = hi;
        this._locale = locale;
        this._timeZone = tz;
        this._defaultBase64 = defaultBase64;
    }

    public BaseSettings withClassIntrospector(ClassIntrospector ci) {
        return this._classIntrospector == ci ? this : new BaseSettings(ci, this._annotationIntrospector, this._visibilityChecker, this._propertyNamingStrategy, this._typeFactory, this._typeResolverBuilder, this._dateFormat, this._handlerInstantiator, this._locale, this._timeZone, this._defaultBase64);
    }

    public BaseSettings withAnnotationIntrospector(AnnotationIntrospector ai) {
        return this._annotationIntrospector == ai ? this : new BaseSettings(this._classIntrospector, ai, this._visibilityChecker, this._propertyNamingStrategy, this._typeFactory, this._typeResolverBuilder, this._dateFormat, this._handlerInstantiator, this._locale, this._timeZone, this._defaultBase64);
    }

    public BaseSettings withInsertedAnnotationIntrospector(AnnotationIntrospector ai) {
        return withAnnotationIntrospector(AnnotationIntrospectorPair.create(ai, this._annotationIntrospector));
    }

    public BaseSettings withAppendedAnnotationIntrospector(AnnotationIntrospector ai) {
        return withAnnotationIntrospector(AnnotationIntrospectorPair.create(this._annotationIntrospector, ai));
    }

    public BaseSettings withVisibilityChecker(VisibilityChecker<?> vc) {
        return this._visibilityChecker == vc ? this : new BaseSettings(this._classIntrospector, this._annotationIntrospector, vc, this._propertyNamingStrategy, this._typeFactory, this._typeResolverBuilder, this._dateFormat, this._handlerInstantiator, this._locale, this._timeZone, this._defaultBase64);
    }

    public BaseSettings withVisibility(PropertyAccessor forMethod, JsonAutoDetect.Visibility visibility) {
        return new BaseSettings(this._classIntrospector, this._annotationIntrospector, this._visibilityChecker.withVisibility(forMethod, visibility), this._propertyNamingStrategy, this._typeFactory, this._typeResolverBuilder, this._dateFormat, this._handlerInstantiator, this._locale, this._timeZone, this._defaultBase64);
    }

    public BaseSettings withPropertyNamingStrategy(PropertyNamingStrategy pns) {
        return this._propertyNamingStrategy == pns ? this : new BaseSettings(this._classIntrospector, this._annotationIntrospector, this._visibilityChecker, pns, this._typeFactory, this._typeResolverBuilder, this._dateFormat, this._handlerInstantiator, this._locale, this._timeZone, this._defaultBase64);
    }

    public BaseSettings withTypeFactory(TypeFactory tf) {
        return this._typeFactory == tf ? this : new BaseSettings(this._classIntrospector, this._annotationIntrospector, this._visibilityChecker, this._propertyNamingStrategy, tf, this._typeResolverBuilder, this._dateFormat, this._handlerInstantiator, this._locale, this._timeZone, this._defaultBase64);
    }

    public BaseSettings withTypeResolverBuilder(TypeResolverBuilder<?> typer) {
        return this._typeResolverBuilder == typer ? this : new BaseSettings(this._classIntrospector, this._annotationIntrospector, this._visibilityChecker, this._propertyNamingStrategy, this._typeFactory, typer, this._dateFormat, this._handlerInstantiator, this._locale, this._timeZone, this._defaultBase64);
    }

    public BaseSettings withDateFormat(DateFormat df) {
        if (this._dateFormat != df) {
            if (df != null && hasExplicitTimeZone()) {
                df = _force(df, this._timeZone);
            }
            return new BaseSettings(this._classIntrospector, this._annotationIntrospector, this._visibilityChecker, this._propertyNamingStrategy, this._typeFactory, this._typeResolverBuilder, df, this._handlerInstantiator, this._locale, this._timeZone, this._defaultBase64);
        }
        return this;
    }

    public BaseSettings withHandlerInstantiator(HandlerInstantiator hi) {
        return this._handlerInstantiator == hi ? this : new BaseSettings(this._classIntrospector, this._annotationIntrospector, this._visibilityChecker, this._propertyNamingStrategy, this._typeFactory, this._typeResolverBuilder, this._dateFormat, hi, this._locale, this._timeZone, this._defaultBase64);
    }

    public BaseSettings with(Locale l) {
        return this._locale == l ? this : new BaseSettings(this._classIntrospector, this._annotationIntrospector, this._visibilityChecker, this._propertyNamingStrategy, this._typeFactory, this._typeResolverBuilder, this._dateFormat, this._handlerInstantiator, l, this._timeZone, this._defaultBase64);
    }

    public BaseSettings with(TimeZone tz) {
        if (tz == null) {
            throw new IllegalArgumentException();
        }
        if (tz != this._timeZone) {
            DateFormat df = _force(this._dateFormat, tz);
            return new BaseSettings(this._classIntrospector, this._annotationIntrospector, this._visibilityChecker, this._propertyNamingStrategy, this._typeFactory, this._typeResolverBuilder, df, this._handlerInstantiator, this._locale, tz, this._defaultBase64);
        }
        return this;
    }

    public BaseSettings with(Base64Variant base64) {
        return base64 == this._defaultBase64 ? this : new BaseSettings(this._classIntrospector, this._annotationIntrospector, this._visibilityChecker, this._propertyNamingStrategy, this._typeFactory, this._typeResolverBuilder, this._dateFormat, this._handlerInstantiator, this._locale, this._timeZone, base64);
    }

    public ClassIntrospector getClassIntrospector() {
        return this._classIntrospector;
    }

    public AnnotationIntrospector getAnnotationIntrospector() {
        return this._annotationIntrospector;
    }

    public VisibilityChecker<?> getVisibilityChecker() {
        return this._visibilityChecker;
    }

    public PropertyNamingStrategy getPropertyNamingStrategy() {
        return this._propertyNamingStrategy;
    }

    public TypeFactory getTypeFactory() {
        return this._typeFactory;
    }

    public TypeResolverBuilder<?> getTypeResolverBuilder() {
        return this._typeResolverBuilder;
    }

    public DateFormat getDateFormat() {
        return this._dateFormat;
    }

    public HandlerInstantiator getHandlerInstantiator() {
        return this._handlerInstantiator;
    }

    public Locale getLocale() {
        return this._locale;
    }

    public TimeZone getTimeZone() {
        TimeZone tz = this._timeZone;
        return tz == null ? DEFAULT_TIMEZONE : tz;
    }

    public boolean hasExplicitTimeZone() {
        return this._timeZone != null;
    }

    public Base64Variant getBase64Variant() {
        return this._defaultBase64;
    }

    private DateFormat _force(DateFormat df, TimeZone tz) {
        if (df instanceof StdDateFormat) {
            return ((StdDateFormat) df).withTimeZone(tz);
        }
        DateFormat df2 = (DateFormat) df.clone();
        df2.setTimeZone(tz);
        return df2;
    }
}
