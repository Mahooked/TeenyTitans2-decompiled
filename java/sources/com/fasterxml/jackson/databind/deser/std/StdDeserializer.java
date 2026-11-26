package com.fasterxml.jackson.databind.deser.std;

import android.support.v4.view.MotionEventCompat;
import android.support.v4.widget.ExploreByTouchHelper;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.core.io.NumberInput;
import com.fasterxml.jackson.databind.AnnotationIntrospector;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.KeyDeserializer;
import com.fasterxml.jackson.databind.introspect.AnnotatedMember;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import com.fasterxml.jackson.databind.util.ClassUtil;
import com.fasterxml.jackson.databind.util.Converter;
import java.io.IOException;
import java.io.Serializable;
import java.util.Date;

/* JADX INFO: loaded from: classes.dex */
public abstract class StdDeserializer<T> extends JsonDeserializer<T> implements Serializable {
    protected static final int F_MASK_INT_COERCIONS = DeserializationFeature.USE_BIG_INTEGER_FOR_INTS.getMask() | DeserializationFeature.USE_LONG_FOR_INTS.getMask();
    private static final long serialVersionUID = 1;
    protected final Class<?> _valueClass;

    protected StdDeserializer(Class<?> vc) {
        this._valueClass = vc;
    }

    protected StdDeserializer(JavaType valueType) {
        this._valueClass = valueType == null ? null : valueType.getRawClass();
    }

    protected StdDeserializer(StdDeserializer<?> src) {
        this._valueClass = src._valueClass;
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Class<?> handledType() {
        return this._valueClass;
    }

    @Deprecated
    public final Class<?> getValueClass() {
        return this._valueClass;
    }

    public JavaType getValueType() {
        return null;
    }

    protected boolean isDefaultDeserializer(JsonDeserializer<?> deserializer) {
        return ClassUtil.isJacksonStdImpl(deserializer);
    }

    protected boolean isDefaultKeyDeserializer(KeyDeserializer keyDeser) {
        return ClassUtil.isJacksonStdImpl(keyDeser);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public Object deserializeWithType(JsonParser jp2, DeserializationContext ctxt, TypeDeserializer typeDeserializer) throws IOException {
        return typeDeserializer.deserializeTypedFromAny(jp2, ctxt);
    }

    protected final boolean _parseBooleanPrimitive(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        JsonToken t = jp2.getCurrentToken();
        if (t == JsonToken.VALUE_TRUE) {
            return true;
        }
        if (t == JsonToken.VALUE_FALSE || t == JsonToken.VALUE_NULL) {
            return false;
        }
        if (t == JsonToken.VALUE_NUMBER_INT) {
            if (jp2.getNumberType() == JsonParser.NumberType.INT) {
                return jp2.getIntValue() != 0;
            }
            return _parseBooleanFromNumber(jp2, ctxt);
        }
        if (t == JsonToken.VALUE_STRING) {
            String text = jp2.getText().trim();
            if ("true".equals(text) || "True".equals(text)) {
                return true;
            }
            if ("false".equals(text) || "False".equals(text) || text.length() == 0 || _hasTextualNull(text)) {
                return false;
            }
            throw ctxt.weirdStringException(text, this._valueClass, "only \"true\" or \"false\" recognized");
        }
        if (t == JsonToken.START_ARRAY && ctxt.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
            jp2.nextToken();
            boolean parsed = _parseBooleanPrimitive(jp2, ctxt);
            if (jp2.nextToken() != JsonToken.END_ARRAY) {
                throw ctxt.wrongTokenException(jp2, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'boolean' value but there was more than a single value in the array");
            }
            return parsed;
        }
        throw ctxt.mappingException(this._valueClass, t);
    }

    protected final Boolean _parseBoolean(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_TRUE) {
            return Boolean.TRUE;
        }
        if (currentToken == JsonToken.VALUE_FALSE) {
            return Boolean.FALSE;
        }
        if (currentToken == JsonToken.VALUE_NUMBER_INT) {
            if (jsonParser.getNumberType() == JsonParser.NumberType.INT) {
                return jsonParser.getIntValue() == 0 ? Boolean.FALSE : Boolean.TRUE;
            }
            return Boolean.valueOf(_parseBooleanFromNumber(jsonParser, deserializationContext));
        }
        if (currentToken == JsonToken.VALUE_NULL) {
            return (Boolean) getNullValue(deserializationContext);
        }
        if (currentToken == JsonToken.VALUE_STRING) {
            String strTrim = jsonParser.getText().trim();
            if ("true".equals(strTrim) || "True".equals(strTrim)) {
                return Boolean.TRUE;
            }
            if ("false".equals(strTrim) || "False".equals(strTrim)) {
                return Boolean.FALSE;
            }
            if (strTrim.length() == 0) {
                return (Boolean) getEmptyValue(deserializationContext);
            }
            if (_hasTextualNull(strTrim)) {
                return (Boolean) getNullValue(deserializationContext);
            }
            throw deserializationContext.weirdStringException(strTrim, this._valueClass, "only \"true\" or \"false\" recognized");
        }
        if (currentToken == JsonToken.START_ARRAY && deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
            jsonParser.nextToken();
            Boolean bool_parseBoolean = _parseBoolean(jsonParser, deserializationContext);
            if (jsonParser.nextToken() != JsonToken.END_ARRAY) {
                throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'Boolean' value but there was more than a single value in the array");
            }
            return bool_parseBoolean;
        }
        throw deserializationContext.mappingException(this._valueClass, currentToken);
    }

    protected final boolean _parseBooleanFromNumber(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        if (jp2.getNumberType() == JsonParser.NumberType.LONG) {
            return (jp2.getLongValue() == 0 ? Boolean.FALSE : Boolean.TRUE).booleanValue();
        }
        String str = jp2.getText();
        if ("0.0".equals(str) || "0".equals(str)) {
            return Boolean.FALSE.booleanValue();
        }
        return Boolean.TRUE.booleanValue();
    }

    protected Byte _parseByte(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        Byte bValueOf;
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_NUMBER_INT) {
            return Byte.valueOf(jsonParser.getByteValue());
        }
        if (currentToken == JsonToken.VALUE_STRING) {
            String strTrim = jsonParser.getText().trim();
            if (_hasTextualNull(strTrim)) {
                return (Byte) getNullValue(deserializationContext);
            }
            try {
                if (strTrim.length() == 0) {
                    bValueOf = (Byte) getEmptyValue(deserializationContext);
                } else {
                    int i = NumberInput.parseInt(strTrim);
                    if (i < -128 || i > 255) {
                        throw deserializationContext.weirdStringException(strTrim, this._valueClass, "overflow, value can not be represented as 8-bit value");
                    }
                    bValueOf = Byte.valueOf((byte) i);
                }
                return bValueOf;
            } catch (IllegalArgumentException e) {
                throw deserializationContext.weirdStringException(strTrim, this._valueClass, "not a valid Byte value");
            }
        }
        if (currentToken == JsonToken.VALUE_NUMBER_FLOAT) {
            if (!deserializationContext.isEnabled(DeserializationFeature.ACCEPT_FLOAT_AS_INT)) {
                _failDoubleToIntCoercion(jsonParser, deserializationContext, "Byte");
            }
            return Byte.valueOf(jsonParser.getByteValue());
        }
        if (currentToken == JsonToken.VALUE_NULL) {
            return (Byte) getNullValue(deserializationContext);
        }
        if (currentToken == JsonToken.START_ARRAY && deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
            jsonParser.nextToken();
            Byte b_parseByte = _parseByte(jsonParser, deserializationContext);
            if (jsonParser.nextToken() != JsonToken.END_ARRAY) {
                throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'Byte' value but there was more than a single value in the array");
            }
            return b_parseByte;
        }
        throw deserializationContext.mappingException(this._valueClass, currentToken);
    }

    protected Short _parseShort(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        Short shValueOf;
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_NUMBER_INT) {
            return Short.valueOf(jsonParser.getShortValue());
        }
        if (currentToken == JsonToken.VALUE_STRING) {
            String strTrim = jsonParser.getText().trim();
            try {
                if (strTrim.length() == 0) {
                    shValueOf = (Short) getEmptyValue(deserializationContext);
                } else if (_hasTextualNull(strTrim)) {
                    shValueOf = (Short) getNullValue(deserializationContext);
                } else {
                    int i = NumberInput.parseInt(strTrim);
                    if (i < -32768 || i > 32767) {
                        throw deserializationContext.weirdStringException(strTrim, this._valueClass, "overflow, value can not be represented as 16-bit value");
                    }
                    shValueOf = Short.valueOf((short) i);
                }
                return shValueOf;
            } catch (IllegalArgumentException e) {
                throw deserializationContext.weirdStringException(strTrim, this._valueClass, "not a valid Short value");
            }
        }
        if (currentToken == JsonToken.VALUE_NUMBER_FLOAT) {
            if (!deserializationContext.isEnabled(DeserializationFeature.ACCEPT_FLOAT_AS_INT)) {
                _failDoubleToIntCoercion(jsonParser, deserializationContext, "Short");
            }
            return Short.valueOf(jsonParser.getShortValue());
        }
        if (currentToken == JsonToken.VALUE_NULL) {
            return (Short) getNullValue(deserializationContext);
        }
        if (currentToken == JsonToken.START_ARRAY && deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
            jsonParser.nextToken();
            Short sh_parseShort = _parseShort(jsonParser, deserializationContext);
            if (jsonParser.nextToken() != JsonToken.END_ARRAY) {
                throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'Short' value but there was more than a single value in the array");
            }
            return sh_parseShort;
        }
        throw deserializationContext.mappingException(this._valueClass, currentToken);
    }

    protected final short _parseShortPrimitive(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        int value = _parseIntPrimitive(jp2, ctxt);
        if (value < -32768 || value > 32767) {
            throw ctxt.weirdStringException(String.valueOf(value), this._valueClass, "overflow, value can not be represented as 16-bit value");
        }
        return (short) value;
    }

    protected final int _parseIntPrimitive(JsonParser p, DeserializationContext ctxt) throws IOException {
        if (p.hasToken(JsonToken.VALUE_NUMBER_INT)) {
            return p.getIntValue();
        }
        JsonToken t = p.getCurrentToken();
        if (t == JsonToken.VALUE_STRING) {
            String text = p.getText().trim();
            if (_hasTextualNull(text)) {
                return 0;
            }
            try {
                int len = text.length();
                if (len > 9) {
                    long l = Long.parseLong(text);
                    if (l < -2147483648L || l > 2147483647L) {
                        throw ctxt.weirdStringException(text, this._valueClass, "Overflow: numeric value (" + text + ") out of range of int (" + ExploreByTouchHelper.INVALID_ID + " - 2147483647)");
                    }
                    return (int) l;
                }
                if (len != 0) {
                    return NumberInput.parseInt(text);
                }
                return 0;
            } catch (IllegalArgumentException e) {
                throw ctxt.weirdStringException(text, this._valueClass, "not a valid int value");
            }
        }
        if (t == JsonToken.VALUE_NUMBER_FLOAT) {
            if (!ctxt.isEnabled(DeserializationFeature.ACCEPT_FLOAT_AS_INT)) {
                _failDoubleToIntCoercion(p, ctxt, "int");
            }
            return p.getValueAsInt();
        }
        if (t == JsonToken.VALUE_NULL) {
            return 0;
        }
        if (t == JsonToken.START_ARRAY && ctxt.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
            p.nextToken();
            int i_parseIntPrimitive = _parseIntPrimitive(p, ctxt);
            if (p.nextToken() != JsonToken.END_ARRAY) {
                throw ctxt.wrongTokenException(p, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'int' value but there was more than a single value in the array");
            }
            return i_parseIntPrimitive;
        }
        throw ctxt.mappingException(this._valueClass, t);
    }

    protected final Integer _parseInteger(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        switch (jsonParser.getCurrentTokenId()) {
            case 3:
                if (deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
                    jsonParser.nextToken();
                    Integer num_parseInteger = _parseInteger(jsonParser, deserializationContext);
                    if (jsonParser.nextToken() != JsonToken.END_ARRAY) {
                        throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'Integer' value but there was more than a single value in the array");
                    }
                    return num_parseInteger;
                }
                break;
            case 6:
                String strTrim = jsonParser.getText().trim();
                try {
                    int length = strTrim.length();
                    if (_hasTextualNull(strTrim)) {
                        return (Integer) getNullValue(deserializationContext);
                    }
                    if (length > 9) {
                        long j = Long.parseLong(strTrim);
                        if (j < -2147483648L || j > 2147483647L) {
                            throw deserializationContext.weirdStringException(strTrim, this._valueClass, "Overflow: numeric value (" + strTrim + ") out of range of Integer (" + ExploreByTouchHelper.INVALID_ID + " - 2147483647)");
                        }
                        return Integer.valueOf((int) j);
                    }
                    if (length == 0) {
                        return (Integer) getEmptyValue(deserializationContext);
                    }
                    return Integer.valueOf(NumberInput.parseInt(strTrim));
                } catch (IllegalArgumentException e) {
                    throw deserializationContext.weirdStringException(strTrim, this._valueClass, "not a valid Integer value");
                }
            case 7:
                return Integer.valueOf(jsonParser.getIntValue());
            case 8:
                if (!deserializationContext.isEnabled(DeserializationFeature.ACCEPT_FLOAT_AS_INT)) {
                    _failDoubleToIntCoercion(jsonParser, deserializationContext, "Integer");
                }
                return Integer.valueOf(jsonParser.getValueAsInt());
            case 11:
                return (Integer) getNullValue(deserializationContext);
        }
        throw deserializationContext.mappingException(this._valueClass, jsonParser.getCurrentToken());
    }

    protected final Long _parseLong(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        switch (jsonParser.getCurrentTokenId()) {
            case 3:
                if (deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
                    jsonParser.nextToken();
                    Long l_parseLong = _parseLong(jsonParser, deserializationContext);
                    if (jsonParser.nextToken() != JsonToken.END_ARRAY) {
                        throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'Long' value but there was more than a single value in the array");
                    }
                    return l_parseLong;
                }
                break;
            case 6:
                String strTrim = jsonParser.getText().trim();
                if (strTrim.length() == 0) {
                    return (Long) getEmptyValue(deserializationContext);
                }
                if (_hasTextualNull(strTrim)) {
                    return (Long) getNullValue(deserializationContext);
                }
                try {
                    return Long.valueOf(NumberInput.parseLong(strTrim));
                } catch (IllegalArgumentException e) {
                    throw deserializationContext.weirdStringException(strTrim, this._valueClass, "not a valid Long value");
                }
            case 7:
                return Long.valueOf(jsonParser.getLongValue());
            case 8:
                if (!deserializationContext.isEnabled(DeserializationFeature.ACCEPT_FLOAT_AS_INT)) {
                    _failDoubleToIntCoercion(jsonParser, deserializationContext, "Long");
                }
                return Long.valueOf(jsonParser.getValueAsLong());
            case 11:
                return (Long) getNullValue(deserializationContext);
        }
        throw deserializationContext.mappingException(this._valueClass, jsonParser.getCurrentToken());
    }

    protected final long _parseLongPrimitive(JsonParser p, DeserializationContext ctxt) throws IOException {
        switch (p.getCurrentTokenId()) {
            case 3:
                if (ctxt.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
                    p.nextToken();
                    long j_parseLongPrimitive = _parseLongPrimitive(p, ctxt);
                    JsonToken t = p.nextToken();
                    if (t != JsonToken.END_ARRAY) {
                        throw ctxt.wrongTokenException(p, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'long' value but there was more than a single value in the array");
                    }
                    return j_parseLongPrimitive;
                }
                break;
            case 6:
                String text = p.getText().trim();
                if (text.length() == 0 || _hasTextualNull(text)) {
                    return 0L;
                }
                try {
                    return NumberInput.parseLong(text);
                } catch (IllegalArgumentException e) {
                    throw ctxt.weirdStringException(text, this._valueClass, "not a valid long value");
                }
            case 7:
                return p.getLongValue();
            case 8:
                if (!ctxt.isEnabled(DeserializationFeature.ACCEPT_FLOAT_AS_INT)) {
                    _failDoubleToIntCoercion(p, ctxt, "long");
                }
                return p.getValueAsLong();
            case 11:
                return 0L;
        }
        throw ctxt.mappingException(this._valueClass, p.getCurrentToken());
    }

    protected final Float _parseFloat(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_NUMBER_INT || currentToken == JsonToken.VALUE_NUMBER_FLOAT) {
            return Float.valueOf(jsonParser.getFloatValue());
        }
        if (currentToken == JsonToken.VALUE_STRING) {
            String strTrim = jsonParser.getText().trim();
            if (strTrim.length() == 0) {
                return (Float) getEmptyValue(deserializationContext);
            }
            if (_hasTextualNull(strTrim)) {
                return (Float) getNullValue(deserializationContext);
            }
            switch (strTrim.charAt(0)) {
                case MotionEventCompat.AXIS_GENERIC_14 /* 45 */:
                    if (_isNegInf(strTrim)) {
                        return Float.valueOf(Float.NEGATIVE_INFINITY);
                    }
                    break;
                case 'I':
                    if (_isPosInf(strTrim)) {
                        return Float.valueOf(Float.POSITIVE_INFINITY);
                    }
                    break;
                case 'N':
                    if (_isNaN(strTrim)) {
                        return Float.valueOf(Float.NaN);
                    }
                    break;
            }
            try {
                return Float.valueOf(Float.parseFloat(strTrim));
            } catch (IllegalArgumentException e) {
                throw deserializationContext.weirdStringException(strTrim, this._valueClass, "not a valid Float value");
            }
        }
        if (currentToken == JsonToken.VALUE_NULL) {
            return (Float) getNullValue(deserializationContext);
        }
        if (currentToken == JsonToken.START_ARRAY && deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
            jsonParser.nextToken();
            Float f_parseFloat = _parseFloat(jsonParser, deserializationContext);
            if (jsonParser.nextToken() != JsonToken.END_ARRAY) {
                throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'Byte' value but there was more than a single value in the array");
            }
            return f_parseFloat;
        }
        throw deserializationContext.mappingException(this._valueClass, currentToken);
    }

    protected final float _parseFloatPrimitive(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        JsonToken t = jp2.getCurrentToken();
        if (t == JsonToken.VALUE_NUMBER_INT || t == JsonToken.VALUE_NUMBER_FLOAT) {
            return jp2.getFloatValue();
        }
        if (t == JsonToken.VALUE_STRING) {
            String text = jp2.getText().trim();
            if (text.length() == 0 || _hasTextualNull(text)) {
                return 0.0f;
            }
            switch (text.charAt(0)) {
                case MotionEventCompat.AXIS_GENERIC_14 /* 45 */:
                    if (_isNegInf(text)) {
                        return Float.NEGATIVE_INFINITY;
                    }
                    break;
                case 'I':
                    if (_isPosInf(text)) {
                        return Float.POSITIVE_INFINITY;
                    }
                    break;
                case 'N':
                    if (_isNaN(text)) {
                        return Float.NaN;
                    }
                    break;
            }
            try {
                return Float.parseFloat(text);
            } catch (IllegalArgumentException e) {
                throw ctxt.weirdStringException(text, this._valueClass, "not a valid float value");
            }
        }
        if (t == JsonToken.VALUE_NULL) {
            return 0.0f;
        }
        if (t == JsonToken.START_ARRAY && ctxt.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
            jp2.nextToken();
            float f_parseFloatPrimitive = _parseFloatPrimitive(jp2, ctxt);
            if (jp2.nextToken() != JsonToken.END_ARRAY) {
                throw ctxt.wrongTokenException(jp2, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'float' value but there was more than a single value in the array");
            }
            return f_parseFloatPrimitive;
        }
        throw ctxt.mappingException(this._valueClass, t);
    }

    protected final Double _parseDouble(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_NUMBER_INT || currentToken == JsonToken.VALUE_NUMBER_FLOAT) {
            return Double.valueOf(jsonParser.getDoubleValue());
        }
        if (currentToken == JsonToken.VALUE_STRING) {
            String strTrim = jsonParser.getText().trim();
            if (strTrim.length() == 0) {
                return (Double) getEmptyValue(deserializationContext);
            }
            if (_hasTextualNull(strTrim)) {
                return (Double) getNullValue(deserializationContext);
            }
            switch (strTrim.charAt(0)) {
                case MotionEventCompat.AXIS_GENERIC_14 /* 45 */:
                    if (_isNegInf(strTrim)) {
                        return Double.valueOf(Double.NEGATIVE_INFINITY);
                    }
                    break;
                case 'I':
                    if (_isPosInf(strTrim)) {
                        return Double.valueOf(Double.POSITIVE_INFINITY);
                    }
                    break;
                case 'N':
                    if (_isNaN(strTrim)) {
                        return Double.valueOf(Double.NaN);
                    }
                    break;
            }
            try {
                return Double.valueOf(parseDouble(strTrim));
            } catch (IllegalArgumentException e) {
                throw deserializationContext.weirdStringException(strTrim, this._valueClass, "not a valid Double value");
            }
        }
        if (currentToken == JsonToken.VALUE_NULL) {
            return (Double) getNullValue(deserializationContext);
        }
        if (currentToken == JsonToken.START_ARRAY && deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
            jsonParser.nextToken();
            Double d_parseDouble = _parseDouble(jsonParser, deserializationContext);
            if (jsonParser.nextToken() != JsonToken.END_ARRAY) {
                throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'Double' value but there was more than a single value in the array");
            }
            return d_parseDouble;
        }
        throw deserializationContext.mappingException(this._valueClass, currentToken);
    }

    protected final double _parseDoublePrimitive(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        JsonToken t = jp2.getCurrentToken();
        if (t == JsonToken.VALUE_NUMBER_INT || t == JsonToken.VALUE_NUMBER_FLOAT) {
            return jp2.getDoubleValue();
        }
        if (t == JsonToken.VALUE_STRING) {
            String text = jp2.getText().trim();
            if (text.length() == 0 || _hasTextualNull(text)) {
                return 0.0d;
            }
            switch (text.charAt(0)) {
                case MotionEventCompat.AXIS_GENERIC_14 /* 45 */:
                    if (_isNegInf(text)) {
                        return Double.NEGATIVE_INFINITY;
                    }
                    break;
                case 'I':
                    if (_isPosInf(text)) {
                        return Double.POSITIVE_INFINITY;
                    }
                    break;
                case 'N':
                    if (_isNaN(text)) {
                        return Double.NaN;
                    }
                    break;
            }
            try {
                return parseDouble(text);
            } catch (IllegalArgumentException e) {
                throw ctxt.weirdStringException(text, this._valueClass, "not a valid double value");
            }
        }
        if (t == JsonToken.VALUE_NULL) {
            return 0.0d;
        }
        if (t == JsonToken.START_ARRAY && ctxt.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
            jp2.nextToken();
            double d_parseDoublePrimitive = _parseDoublePrimitive(jp2, ctxt);
            if (jp2.nextToken() != JsonToken.END_ARRAY) {
                throw ctxt.wrongTokenException(jp2, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'Byte' value but there was more than a single value in the array");
            }
            return d_parseDoublePrimitive;
        }
        throw ctxt.mappingException(this._valueClass, t);
    }

    protected Date _parseDate(JsonParser jsonParser, DeserializationContext deserializationContext) throws IOException {
        Date date;
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == JsonToken.VALUE_NUMBER_INT) {
            return new Date(jsonParser.getLongValue());
        }
        if (currentToken == JsonToken.VALUE_NULL) {
            return (Date) getNullValue(deserializationContext);
        }
        if (currentToken == JsonToken.VALUE_STRING) {
            String strTrim = null;
            try {
                strTrim = jsonParser.getText().trim();
                if (strTrim.length() == 0) {
                    date = (Date) getEmptyValue(deserializationContext);
                } else if (_hasTextualNull(strTrim)) {
                    date = (Date) getNullValue(deserializationContext);
                } else {
                    date = deserializationContext.parseDate(strTrim);
                }
                return date;
            } catch (IllegalArgumentException e) {
                throw deserializationContext.weirdStringException(strTrim, this._valueClass, "not a valid representation (error: " + e.getMessage() + ")");
            }
        }
        if (currentToken == JsonToken.START_ARRAY && deserializationContext.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
            jsonParser.nextToken();
            Date date_parseDate = _parseDate(jsonParser, deserializationContext);
            if (jsonParser.nextToken() != JsonToken.END_ARRAY) {
                throw deserializationContext.wrongTokenException(jsonParser, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'java.util.Date' value but there was more than a single value in the array");
            }
            return date_parseDate;
        }
        throw deserializationContext.mappingException(this._valueClass, currentToken);
    }

    protected static final double parseDouble(String numStr) throws NumberFormatException {
        if (NumberInput.NASTY_SMALL_DOUBLE.equals(numStr)) {
            return Double.MIN_VALUE;
        }
        return Double.parseDouble(numStr);
    }

    protected final String _parseString(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        JsonToken t = jp2.getCurrentToken();
        if (t == JsonToken.VALUE_STRING) {
            return jp2.getText();
        }
        if (t == JsonToken.START_ARRAY && ctxt.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
            jp2.nextToken();
            String str_parseString = _parseString(jp2, ctxt);
            if (jp2.nextToken() != JsonToken.END_ARRAY) {
                throw ctxt.wrongTokenException(jp2, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'String' value but there was more than a single value in the array");
            }
            return str_parseString;
        }
        String value = jp2.getValueAsString();
        if (value != null) {
            return value;
        }
        throw ctxt.mappingException(String.class, jp2.getCurrentToken());
    }

    /* JADX WARN: Code restructure failed: missing block: B:16:0x003d, code lost:
    
        if (r0.isEmpty() == false) goto L17;
     */
    /*
        Code decompiled incorrectly, please refer to instructions dump.
        To view partially-correct add '--show-bad-code' argument
    */
    protected T _deserializeFromEmpty(com.fasterxml.jackson.core.JsonParser r5, com.fasterxml.jackson.databind.DeserializationContext r6) throws java.io.IOException {
        /*
            r4 = this;
            r3 = 0
            com.fasterxml.jackson.core.JsonToken r1 = r5.getCurrentToken()
            com.fasterxml.jackson.core.JsonToken r2 = com.fasterxml.jackson.core.JsonToken.START_ARRAY
            if (r1 != r2) goto L25
            com.fasterxml.jackson.databind.DeserializationFeature r2 = com.fasterxml.jackson.databind.DeserializationFeature.ACCEPT_EMPTY_ARRAY_AS_NULL_OBJECT
            boolean r2 = r6.isEnabled(r2)
            if (r2 == 0) goto L3f
            com.fasterxml.jackson.core.JsonToken r1 = r5.nextToken()
            com.fasterxml.jackson.core.JsonToken r2 = com.fasterxml.jackson.core.JsonToken.END_ARRAY
            if (r1 != r2) goto L1a
        L19:
            return r3
        L1a:
            java.lang.Class r2 = r4.handledType()
            com.fasterxml.jackson.core.JsonToken r3 = com.fasterxml.jackson.core.JsonToken.START_ARRAY
            com.fasterxml.jackson.databind.JsonMappingException r2 = r6.mappingException(r2, r3)
            throw r2
        L25:
            com.fasterxml.jackson.core.JsonToken r2 = com.fasterxml.jackson.core.JsonToken.VALUE_STRING
            if (r1 != r2) goto L3f
            com.fasterxml.jackson.databind.DeserializationFeature r2 = com.fasterxml.jackson.databind.DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT
            boolean r2 = r6.isEnabled(r2)
            if (r2 == 0) goto L3f
            java.lang.String r2 = r5.getText()
            java.lang.String r0 = r2.trim()
            boolean r2 = r0.isEmpty()
            if (r2 != 0) goto L19
        L3f:
            java.lang.Class r2 = r4.handledType()
            com.fasterxml.jackson.databind.JsonMappingException r2 = r6.mappingException(r2)
            throw r2
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.deser.std.StdDeserializer._deserializeFromEmpty(com.fasterxml.jackson.core.JsonParser, com.fasterxml.jackson.databind.DeserializationContext):java.lang.Object");
    }

    protected boolean _hasTextualNull(String value) {
        return "null".equals(value);
    }

    protected final boolean _isNegInf(String text) {
        return "-Infinity".equals(text) || "-INF".equals(text);
    }

    protected final boolean _isPosInf(String text) {
        return "Infinity".equals(text) || "INF".equals(text);
    }

    protected final boolean _isNaN(String text) {
        return "NaN".equals(text);
    }

    protected Object _coerceIntegral(JsonParser p, DeserializationContext ctxt) throws IOException {
        int feats = ctxt.getDeserializationFeatures();
        if (DeserializationFeature.USE_BIG_INTEGER_FOR_INTS.enabledIn(feats)) {
            return p.getBigIntegerValue();
        }
        if (DeserializationFeature.USE_LONG_FOR_INTS.enabledIn(feats)) {
            return Long.valueOf(p.getLongValue());
        }
        return p.getBigIntegerValue();
    }

    protected JsonDeserializer<Object> findDeserializer(DeserializationContext ctxt, JavaType type, BeanProperty property) throws JsonMappingException {
        return ctxt.findContextualValueDeserializer(type, property);
    }

    protected final boolean _isIntNumber(String text) {
        int len = text.length();
        if (len <= 0) {
            return false;
        }
        char c = text.charAt(0);
        for (int i = (c == '-' || c == '+') ? 1 : 0; i < len; i++) {
            int ch = text.charAt(i);
            if (ch > 57 || ch < 48) {
                return false;
            }
        }
        return true;
    }

    protected JsonDeserializer<?> findConvertingContentDeserializer(DeserializationContext ctxt, BeanProperty prop, JsonDeserializer<?> existingDeserializer) throws JsonMappingException {
        AnnotatedMember member;
        Object convDef;
        AnnotationIntrospector intr = ctxt.getAnnotationIntrospector();
        if (intr == null || prop == null || (member = prop.getMember()) == null || (convDef = intr.findDeserializationContentConverter(member)) == null) {
            return existingDeserializer;
        }
        Converter<Object, Object> conv = ctxt.converterInstance(prop.getMember(), convDef);
        JavaType delegateType = conv.getInputType(ctxt.getTypeFactory());
        if (existingDeserializer == null) {
            existingDeserializer = ctxt.findContextualValueDeserializer(delegateType, prop);
        }
        return new StdDelegatingDeserializer(conv, delegateType, existingDeserializer);
    }

    protected void handleUnknownProperty(JsonParser jp2, DeserializationContext ctxt, Object instanceOrClass, String propName) throws IOException {
        if (instanceOrClass == null) {
            instanceOrClass = handledType();
        }
        if (!ctxt.handleUnknownProperty(jp2, this, instanceOrClass, propName)) {
            ctxt.reportUnknownProperty(instanceOrClass, propName, this);
            jp2.skipChildren();
        }
    }

    protected void _failDoubleToIntCoercion(JsonParser jp2, DeserializationContext ctxt, String type) throws IOException {
        throw ctxt.mappingException("Can not coerce a floating-point value ('%s') into %s; enable `DeserializationFeature.ACCEPT_FLOAT_AS_INT` to allow", jp2.getValueAsString(), type);
    }
}
