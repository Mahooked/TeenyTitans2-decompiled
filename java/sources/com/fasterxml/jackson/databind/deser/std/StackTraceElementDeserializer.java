package com.fasterxml.jackson.databind.deser.std;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JsonMappingException;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public class StackTraceElementDeserializer extends StdScalarDeserializer<StackTraceElement> {
    private static final long serialVersionUID = 1;

    public StackTraceElementDeserializer() {
        super((Class<?>) StackTraceElement.class);
    }

    @Override // com.fasterxml.jackson.databind.JsonDeserializer
    public StackTraceElement deserialize(JsonParser jp2, DeserializationContext ctxt) throws IOException {
        JsonToken t = jp2.getCurrentToken();
        if (t == JsonToken.START_OBJECT) {
            String className = "";
            String methodName = "";
            String fileName = "";
            int lineNumber = -1;
            while (true) {
                JsonToken t2 = jp2.nextValue();
                if (t2 != JsonToken.END_OBJECT) {
                    String propName = jp2.getCurrentName();
                    if ("className".equals(propName)) {
                        className = jp2.getText();
                    } else if ("fileName".equals(propName)) {
                        fileName = jp2.getText();
                    } else if ("lineNumber".equals(propName)) {
                        if (t2.isNumeric()) {
                            lineNumber = jp2.getIntValue();
                        } else {
                            throw JsonMappingException.from(jp2, "Non-numeric token (" + t2 + ") for property 'lineNumber'");
                        }
                    } else if ("methodName".equals(propName)) {
                        methodName = jp2.getText();
                    } else if (!"nativeMethod".equals(propName)) {
                        handleUnknownProperty(jp2, ctxt, this._valueClass, propName);
                    }
                } else {
                    return new StackTraceElement(className, methodName, fileName, lineNumber);
                }
            }
        } else {
            if (t == JsonToken.START_ARRAY && ctxt.isEnabled(DeserializationFeature.UNWRAP_SINGLE_VALUE_ARRAYS)) {
                jp2.nextToken();
                StackTraceElement stackTraceElementDeserialize = deserialize(jp2, ctxt);
                if (jp2.nextToken() != JsonToken.END_ARRAY) {
                    throw ctxt.wrongTokenException(jp2, JsonToken.END_ARRAY, "Attempted to unwrap single value array for single 'java.lang.StackTraceElement' value but there was more than a single value in the array");
                }
                return stackTraceElementDeserialize;
            }
            throw ctxt.mappingException(this._valueClass, t);
        }
    }
}
