package com.fasterxml.jackson.databind.deser.std;

import com.fasterxml.jackson.core.Base64Variants;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.annotation.JacksonStdImpl;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import com.fasterxml.jackson.databind.util.ArrayBuilders;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public abstract class PrimitiveArrayDeserializers<T> extends StdDeserializer<T> {
    protected PrimitiveArrayDeserializers(Class<T> cls) {
        super((Class<?>) cls);
    }

    public static JsonDeserializer<?> forType(Class<?> rawType) {
        if (rawType == Integer.TYPE) {
            return IntDeser.instance;
        }
        if (rawType == Long.TYPE) {
            return LongDeser.instance;
        }
        if (rawType == Byte.TYPE) {
            return new ByteDeser();
        }
        if (rawType == Short.TYPE) {
            return new ShortDeser();
        }
        if (rawType == Float.TYPE) {
            return new FloatDeser();
        }
        if (rawType == Double.TYPE) {
            return new DoubleDeser();
        }
        if (rawType == Boolean.TYPE) {
            return new BooleanDeser();
        }
        if (rawType == Character.TYPE) {
            return new CharDeser();
        }
        throw new IllegalStateException();
    }

    @Override // com.fasterxml.jackson.databind.deser.std.StdDeserializer, com.fasterxml.jackson.databind.JsonDeserializer
    public Object deserializeWithType(JsonParser jp2, DeserializationContext ctxt, TypeDeserializer typeDeserializer) throws IOException {
        return typeDeserializer.deserializeTypedFromArray(jp2, ctxt);
    }

    @JacksonStdImpl
    static final class CharDeser extends PrimitiveArrayDeserializers<char[]> {
        private static final long serialVersionUID = 1;

        public CharDeser() {
            super(char[].class);
        }

        @Override // com.fasterxml.jackson.databind.JsonDeserializer
        public char[] deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
            JsonToken t = jp2.getCurrentToken();
            if (t == JsonToken.VALUE_STRING) {
                char[] buffer = jp2.getTextCharacters();
                int offset = jp2.getTextOffset();
                int len = jp2.getTextLength();
                char[] result = new char[len];
                System.arraycopy(buffer, offset, result, 0, len);
                return result;
            }
            if (jp2.isExpectedStartArrayToken()) {
                StringBuilder sb = new StringBuilder(64);
                while (true) {
                    JsonToken t2 = jp2.nextToken();
                    if (t2 != JsonToken.END_ARRAY) {
                        if (t2 != JsonToken.VALUE_STRING) {
                            throw ctxt.mappingException(Character.TYPE);
                        }
                        String str = jp2.getText();
                        if (str.length() != 1) {
                            throw JsonMappingException.from(jp2, "Can not convert a JSON String of length " + str.length() + " into a char element of char array");
                        }
                        sb.append(str.charAt(0));
                    } else {
                        char[] result2 = sb.toString().toCharArray();
                        return result2;
                    }
                }
            } else {
                if (t == JsonToken.VALUE_EMBEDDED_OBJECT) {
                    Object ob = jp2.getEmbeddedObject();
                    if (ob == null) {
                        return null;
                    }
                    if (ob instanceof char[]) {
                        char[] result3 = (char[]) ob;
                        return result3;
                    }
                    if (ob instanceof String) {
                        char[] result4 = ((String) ob).toCharArray();
                        return result4;
                    }
                    if (ob instanceof byte[]) {
                        char[] result5 = Base64Variants.getDefaultVariant().encode((byte[]) ob, false).toCharArray();
                        return result5;
                    }
                }
                throw ctxt.mappingException(this._valueClass);
            }
        }
    }

    @JacksonStdImpl
    static final class BooleanDeser extends PrimitiveArrayDeserializers<boolean[]> {
        private static final long serialVersionUID = 1;

        public BooleanDeser() {
            super(boolean[].class);
        }

        @Override // com.fasterxml.jackson.databind.JsonDeserializer
        public boolean[] deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
            if (!jp2.isExpectedStartArrayToken()) {
                return handleNonArray(jp2, ctxt);
            }
            ArrayBuilders.BooleanBuilder builder = ctxt.getArrayBuilders().getBooleanBuilder();
            boolean[] chunk = builder.resetAndStart();
            int ix = 0;
            while (jp2.nextToken() != JsonToken.END_ARRAY) {
                try {
                    boolean value = _parseBooleanPrimitive(jp2, ctxt);
                    if (ix >= chunk.length) {
                        chunk = builder.appendCompletedChunk(chunk, ix);
                        ix = 0;
                    }
                    int ix2 = ix;
                    ix = ix2 + 1;
                    chunk[ix2] = value;
                } catch (Exception e) {
                    throw JsonMappingException.wrapWithPath(e, chunk, builder.bufferedSize() + ix);
                }
            }
            return builder.completeAndClearBuffer(chunk, ix);
        }

        private final boolean[] handleNonArray(JsonParser jp2, DeserializationContext ctxt) throws IOException {
            if (jp2.getCurrentToken() == JsonToken.VALUE_STRING && ctxt.isEnabled(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT) && jp2.getText().length() == 0) {
                return null;
            }
            if (!ctxt.isEnabled(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY)) {
                throw ctxt.mappingException(this._valueClass);
            }
            return new boolean[]{_parseBooleanPrimitive(jp2, ctxt)};
        }
    }

    @JacksonStdImpl
    static final class ByteDeser extends PrimitiveArrayDeserializers<byte[]> {
        private static final long serialVersionUID = 1;

        public ByteDeser() {
            super(byte[].class);
        }

        @Override // com.fasterxml.jackson.databind.JsonDeserializer
        public byte[] deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
            byte value;
            JsonToken t = jp2.getCurrentToken();
            if (t == JsonToken.VALUE_STRING) {
                return jp2.getBinaryValue(ctxt.getBase64Variant());
            }
            if (t == JsonToken.VALUE_EMBEDDED_OBJECT) {
                Object ob = jp2.getEmbeddedObject();
                if (ob == null) {
                    return null;
                }
                if (ob instanceof byte[]) {
                    return (byte[]) ob;
                }
            }
            if (!jp2.isExpectedStartArrayToken()) {
                return handleNonArray(jp2, ctxt);
            }
            ArrayBuilders.ByteBuilder builder = ctxt.getArrayBuilders().getByteBuilder();
            byte[] chunk = builder.resetAndStart();
            int ix = 0;
            while (true) {
                try {
                    JsonToken t2 = jp2.nextToken();
                    if (t2 != JsonToken.END_ARRAY) {
                        if (t2 == JsonToken.VALUE_NUMBER_INT || t2 == JsonToken.VALUE_NUMBER_FLOAT) {
                            value = jp2.getByteValue();
                        } else {
                            if (t2 != JsonToken.VALUE_NULL) {
                                throw ctxt.mappingException(this._valueClass.getComponentType());
                            }
                            value = 0;
                        }
                        if (ix >= chunk.length) {
                            chunk = builder.appendCompletedChunk(chunk, ix);
                            ix = 0;
                        }
                        int ix2 = ix;
                        ix = ix2 + 1;
                        chunk[ix2] = value;
                    } else {
                        return builder.completeAndClearBuffer(chunk, ix);
                    }
                } catch (Exception e) {
                    throw JsonMappingException.wrapWithPath(e, chunk, builder.bufferedSize() + ix);
                }
            }
        }

        private final byte[] handleNonArray(JsonParser jp2, DeserializationContext ctxt) throws IOException {
            byte value;
            if (jp2.getCurrentToken() == JsonToken.VALUE_STRING && ctxt.isEnabled(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT) && jp2.getText().length() == 0) {
                return null;
            }
            if (!ctxt.isEnabled(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY)) {
                throw ctxt.mappingException(this._valueClass);
            }
            JsonToken t = jp2.getCurrentToken();
            if (t == JsonToken.VALUE_NUMBER_INT || t == JsonToken.VALUE_NUMBER_FLOAT) {
                value = jp2.getByteValue();
            } else {
                if (t != JsonToken.VALUE_NULL) {
                    throw ctxt.mappingException(this._valueClass.getComponentType());
                }
                value = 0;
            }
            return new byte[]{value};
        }
    }

    @JacksonStdImpl
    static final class ShortDeser extends PrimitiveArrayDeserializers<short[]> {
        private static final long serialVersionUID = 1;

        public ShortDeser() {
            super(short[].class);
        }

        @Override // com.fasterxml.jackson.databind.JsonDeserializer
        public short[] deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
            if (!jp2.isExpectedStartArrayToken()) {
                return handleNonArray(jp2, ctxt);
            }
            ArrayBuilders.ShortBuilder builder = ctxt.getArrayBuilders().getShortBuilder();
            short[] chunk = builder.resetAndStart();
            int ix = 0;
            while (jp2.nextToken() != JsonToken.END_ARRAY) {
                try {
                    short value = _parseShortPrimitive(jp2, ctxt);
                    if (ix >= chunk.length) {
                        chunk = builder.appendCompletedChunk(chunk, ix);
                        ix = 0;
                    }
                    int ix2 = ix;
                    ix = ix2 + 1;
                    chunk[ix2] = value;
                } catch (Exception e) {
                    throw JsonMappingException.wrapWithPath(e, chunk, builder.bufferedSize() + ix);
                }
            }
            return builder.completeAndClearBuffer(chunk, ix);
        }

        private final short[] handleNonArray(JsonParser jp2, DeserializationContext ctxt) throws IOException {
            if (jp2.getCurrentToken() == JsonToken.VALUE_STRING && ctxt.isEnabled(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT) && jp2.getText().length() == 0) {
                return null;
            }
            if (!ctxt.isEnabled(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY)) {
                throw ctxt.mappingException(this._valueClass);
            }
            return new short[]{_parseShortPrimitive(jp2, ctxt)};
        }
    }

    @JacksonStdImpl
    static final class IntDeser extends PrimitiveArrayDeserializers<int[]> {
        public static final IntDeser instance = new IntDeser();
        private static final long serialVersionUID = 1;

        public IntDeser() {
            super(int[].class);
        }

        @Override // com.fasterxml.jackson.databind.JsonDeserializer
        public int[] deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
            if (!jp2.isExpectedStartArrayToken()) {
                return handleNonArray(jp2, ctxt);
            }
            ArrayBuilders.IntBuilder builder = ctxt.getArrayBuilders().getIntBuilder();
            int[] chunk = (int[]) builder.resetAndStart();
            int ix = 0;
            while (jp2.nextToken() != JsonToken.END_ARRAY) {
                try {
                    int value = _parseIntPrimitive(jp2, ctxt);
                    if (ix >= chunk.length) {
                        chunk = (int[]) builder.appendCompletedChunk(chunk, ix);
                        ix = 0;
                    }
                    int ix2 = ix;
                    ix = ix2 + 1;
                    chunk[ix2] = value;
                } catch (Exception e) {
                    throw JsonMappingException.wrapWithPath(e, chunk, builder.bufferedSize() + ix);
                }
            }
            return (int[]) builder.completeAndClearBuffer(chunk, ix);
        }

        private final int[] handleNonArray(JsonParser jp2, DeserializationContext ctxt) throws IOException {
            if (jp2.getCurrentToken() == JsonToken.VALUE_STRING && ctxt.isEnabled(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT) && jp2.getText().length() == 0) {
                return null;
            }
            if (!ctxt.isEnabled(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY)) {
                throw ctxt.mappingException(this._valueClass);
            }
            return new int[]{_parseIntPrimitive(jp2, ctxt)};
        }
    }

    @JacksonStdImpl
    static final class LongDeser extends PrimitiveArrayDeserializers<long[]> {
        public static final LongDeser instance = new LongDeser();
        private static final long serialVersionUID = 1;

        public LongDeser() {
            super(long[].class);
        }

        @Override // com.fasterxml.jackson.databind.JsonDeserializer
        public long[] deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
            if (!jp2.isExpectedStartArrayToken()) {
                return handleNonArray(jp2, ctxt);
            }
            ArrayBuilders.LongBuilder builder = ctxt.getArrayBuilders().getLongBuilder();
            long[] chunk = (long[]) builder.resetAndStart();
            int ix = 0;
            while (jp2.nextToken() != JsonToken.END_ARRAY) {
                try {
                    long value = _parseLongPrimitive(jp2, ctxt);
                    if (ix >= chunk.length) {
                        chunk = (long[]) builder.appendCompletedChunk(chunk, ix);
                        ix = 0;
                    }
                    int ix2 = ix;
                    ix = ix2 + 1;
                    chunk[ix2] = value;
                } catch (Exception e) {
                    throw JsonMappingException.wrapWithPath(e, chunk, builder.bufferedSize() + ix);
                }
            }
            return (long[]) builder.completeAndClearBuffer(chunk, ix);
        }

        private final long[] handleNonArray(JsonParser jp2, DeserializationContext ctxt) throws IOException {
            if (jp2.getCurrentToken() == JsonToken.VALUE_STRING && ctxt.isEnabled(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT) && jp2.getText().length() == 0) {
                return null;
            }
            if (!ctxt.isEnabled(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY)) {
                throw ctxt.mappingException(this._valueClass);
            }
            return new long[]{_parseLongPrimitive(jp2, ctxt)};
        }
    }

    @JacksonStdImpl
    static final class FloatDeser extends PrimitiveArrayDeserializers<float[]> {
        private static final long serialVersionUID = 1;

        public FloatDeser() {
            super(float[].class);
        }

        @Override // com.fasterxml.jackson.databind.JsonDeserializer
        public float[] deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
            if (!jp2.isExpectedStartArrayToken()) {
                return handleNonArray(jp2, ctxt);
            }
            ArrayBuilders.FloatBuilder builder = ctxt.getArrayBuilders().getFloatBuilder();
            float[] chunk = (float[]) builder.resetAndStart();
            int ix = 0;
            while (jp2.nextToken() != JsonToken.END_ARRAY) {
                try {
                    float value = _parseFloatPrimitive(jp2, ctxt);
                    if (ix >= chunk.length) {
                        chunk = (float[]) builder.appendCompletedChunk(chunk, ix);
                        ix = 0;
                    }
                    int ix2 = ix;
                    ix = ix2 + 1;
                    chunk[ix2] = value;
                } catch (Exception e) {
                    throw JsonMappingException.wrapWithPath(e, chunk, builder.bufferedSize() + ix);
                }
            }
            return (float[]) builder.completeAndClearBuffer(chunk, ix);
        }

        private final float[] handleNonArray(JsonParser jp2, DeserializationContext ctxt) throws IOException {
            if (jp2.getCurrentToken() == JsonToken.VALUE_STRING && ctxt.isEnabled(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT) && jp2.getText().length() == 0) {
                return null;
            }
            if (!ctxt.isEnabled(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY)) {
                throw ctxt.mappingException(this._valueClass);
            }
            return new float[]{_parseFloatPrimitive(jp2, ctxt)};
        }
    }

    @JacksonStdImpl
    static final class DoubleDeser extends PrimitiveArrayDeserializers<double[]> {
        private static final long serialVersionUID = 1;

        public DoubleDeser() {
            super(double[].class);
        }

        @Override // com.fasterxml.jackson.databind.JsonDeserializer
        public double[] deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
            if (!jp2.isExpectedStartArrayToken()) {
                return handleNonArray(jp2, ctxt);
            }
            ArrayBuilders.DoubleBuilder builder = ctxt.getArrayBuilders().getDoubleBuilder();
            double[] chunk = (double[]) builder.resetAndStart();
            int ix = 0;
            while (jp2.nextToken() != JsonToken.END_ARRAY) {
                try {
                    double value = _parseDoublePrimitive(jp2, ctxt);
                    if (ix >= chunk.length) {
                        chunk = (double[]) builder.appendCompletedChunk(chunk, ix);
                        ix = 0;
                    }
                    int ix2 = ix;
                    ix = ix2 + 1;
                    chunk[ix2] = value;
                } catch (Exception e) {
                    throw JsonMappingException.wrapWithPath(e, chunk, builder.bufferedSize() + ix);
                }
            }
            return (double[]) builder.completeAndClearBuffer(chunk, ix);
        }

        private final double[] handleNonArray(JsonParser jp2, DeserializationContext ctxt) throws IOException {
            if (jp2.getCurrentToken() == JsonToken.VALUE_STRING && ctxt.isEnabled(DeserializationFeature.ACCEPT_EMPTY_STRING_AS_NULL_OBJECT) && jp2.getText().length() == 0) {
                return null;
            }
            if (!ctxt.isEnabled(DeserializationFeature.ACCEPT_SINGLE_VALUE_AS_ARRAY)) {
                throw ctxt.mappingException(this._valueClass);
            }
            return new double[]{_parseDoublePrimitive(jp2, ctxt)};
        }
    }
}
