package com.fasterxml.jackson.databind.node;

import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.databind.JsonSerializable;
import com.fasterxml.jackson.databind.SerializerProvider;
import com.fasterxml.jackson.databind.util.RawValue;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public class POJONode extends ValueNode {
    protected final Object _value;

    public POJONode(Object v) {
        this._value = v;
    }

    @Override // com.fasterxml.jackson.databind.JsonNode
    public JsonNodeType getNodeType() {
        return JsonNodeType.POJO;
    }

    @Override // com.fasterxml.jackson.databind.node.ValueNode, com.fasterxml.jackson.databind.node.BaseJsonNode, com.fasterxml.jackson.core.TreeNode
    public JsonToken asToken() {
        return JsonToken.VALUE_EMBEDDED_OBJECT;
    }

    @Override // com.fasterxml.jackson.databind.JsonNode
    public byte[] binaryValue() throws IOException {
        return this._value instanceof byte[] ? (byte[]) this._value : super.binaryValue();
    }

    @Override // com.fasterxml.jackson.databind.JsonNode
    public String asText() {
        return this._value == null ? "null" : this._value.toString();
    }

    @Override // com.fasterxml.jackson.databind.JsonNode
    public String asText(String defaultValue) {
        if (this._value == null) {
            return defaultValue;
        }
        String defaultValue2 = this._value.toString();
        return defaultValue2;
    }

    @Override // com.fasterxml.jackson.databind.JsonNode
    public boolean asBoolean(boolean defaultValue) {
        if (this._value != null && (this._value instanceof Boolean)) {
            return ((Boolean) this._value).booleanValue();
        }
        return defaultValue;
    }

    @Override // com.fasterxml.jackson.databind.JsonNode
    public int asInt(int defaultValue) {
        if (this._value instanceof Number) {
            int defaultValue2 = ((Number) this._value).intValue();
            return defaultValue2;
        }
        return defaultValue;
    }

    @Override // com.fasterxml.jackson.databind.JsonNode
    public long asLong(long defaultValue) {
        if (this._value instanceof Number) {
            long defaultValue2 = ((Number) this._value).longValue();
            return defaultValue2;
        }
        return defaultValue;
    }

    @Override // com.fasterxml.jackson.databind.JsonNode
    public double asDouble(double defaultValue) {
        if (this._value instanceof Number) {
            double defaultValue2 = ((Number) this._value).doubleValue();
            return defaultValue2;
        }
        return defaultValue;
    }

    @Override // com.fasterxml.jackson.databind.node.BaseJsonNode, com.fasterxml.jackson.databind.JsonSerializable
    public final void serialize(JsonGenerator gen, SerializerProvider serializers) throws IOException {
        if (this._value == null) {
            serializers.defaultSerializeNull(gen);
        } else if (this._value instanceof JsonSerializable) {
            ((JsonSerializable) this._value).serialize(gen, serializers);
        } else {
            gen.writeObject(this._value);
        }
    }

    public Object getPojo() {
        return this._value;
    }

    @Override // com.fasterxml.jackson.databind.JsonNode
    public boolean equals(Object o) {
        if (o == this) {
            return true;
        }
        if (o == null || !(o instanceof POJONode)) {
            return false;
        }
        return _pojoEquals((POJONode) o);
    }

    protected boolean _pojoEquals(POJONode other) {
        if (this._value == null) {
            return other._value == null;
        }
        return this._value.equals(other._value);
    }

    @Override // com.fasterxml.jackson.databind.node.BaseJsonNode
    public int hashCode() {
        return this._value.hashCode();
    }

    @Override // com.fasterxml.jackson.databind.node.ValueNode, com.fasterxml.jackson.databind.JsonNode
    public String toString() {
        if (this._value instanceof byte[]) {
            return String.format("(binary value of %d bytes)", Integer.valueOf(((byte[]) this._value).length));
        }
        if (this._value instanceof RawValue) {
            return String.format("(raw value '%s')", ((RawValue) this._value).toString());
        }
        return String.valueOf(this._value);
    }
}
