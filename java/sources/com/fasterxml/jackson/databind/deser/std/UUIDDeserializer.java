package com.fasterxml.jackson.databind.deser.std;

import android.support.v4.media.TransportMediator;
import com.fasterxml.jackson.core.Base64Variants;
import com.fasterxml.jackson.databind.DeserializationContext;
import java.io.IOException;
import java.util.Arrays;
import java.util.UUID;

/* JADX INFO: loaded from: classes.dex */
public class UUIDDeserializer extends FromStringDeserializer<UUID> {
    static final int[] HEX_DIGITS = new int[TransportMediator.KEYCODE_MEDIA_PAUSE];
    private static final long serialVersionUID = 1;

    static {
        Arrays.fill(HEX_DIGITS, -1);
        for (int i = 0; i < 10; i++) {
            HEX_DIGITS[i + 48] = i;
        }
        for (int i2 = 0; i2 < 6; i2++) {
            HEX_DIGITS[i2 + 97] = i2 + 10;
            HEX_DIGITS[i2 + 65] = i2 + 10;
        }
    }

    public UUIDDeserializer() {
        super(UUID.class);
    }

    /* JADX INFO: Access modifiers changed from: protected */
    @Override // com.fasterxml.jackson.databind.deser.std.FromStringDeserializer
    public UUID _deserialize(String id, DeserializationContext ctxt) throws IOException {
        if (id.length() != 36) {
            if (id.length() == 24) {
                byte[] stuff = Base64Variants.getDefaultVariant().decode(id);
                return _fromBytes(stuff, ctxt);
            }
            _badFormat(id);
        }
        if (id.charAt(8) != '-' || id.charAt(13) != '-' || id.charAt(18) != '-' || id.charAt(23) != '-') {
            _badFormat(id);
        }
        long l1 = intFromChars(id, 0);
        long l2 = ((long) shortFromChars(id, 9)) << 16;
        long hi = (l1 << 32) + (l2 | ((long) shortFromChars(id, 14)));
        int i1 = (shortFromChars(id, 19) << 16) | shortFromChars(id, 24);
        long l3 = i1;
        long l4 = intFromChars(id, 28);
        long lo = (l3 << 32) | ((l4 << 32) >>> 32);
        return new UUID(hi, lo);
    }

    /* JADX INFO: Access modifiers changed from: protected */
    @Override // com.fasterxml.jackson.databind.deser.std.FromStringDeserializer
    public UUID _deserializeEmbedded(Object ob, DeserializationContext ctxt) throws IOException {
        if (ob instanceof byte[]) {
            return _fromBytes((byte[]) ob, ctxt);
        }
        super._deserializeEmbedded(ob, ctxt);
        return null;
    }

    private void _badFormat(String uuidStr) {
        throw new NumberFormatException("UUID has to be represented by the standard 36-char representation");
    }

    static int intFromChars(String str, int index) {
        return (byteFromChars(str, index) << 24) + (byteFromChars(str, index + 2) << 16) + (byteFromChars(str, index + 4) << 8) + byteFromChars(str, index + 6);
    }

    static int shortFromChars(String str, int index) {
        return (byteFromChars(str, index) << 8) + byteFromChars(str, index + 2);
    }

    static int byteFromChars(String str, int index) {
        int hex;
        char c1 = str.charAt(index);
        char c2 = str.charAt(index + 1);
        if (c1 > 127 || c2 > 127 || (hex = (HEX_DIGITS[c1] << 4) | HEX_DIGITS[c2]) < 0) {
            if (c1 > 127 || HEX_DIGITS[c1] < 0) {
                int hex2 = _badChar(str, index, c1);
                return hex2;
            }
            int hex3 = _badChar(str, index + 1, c2);
            return hex3;
        }
        return hex;
    }

    static int _badChar(String uuidStr, int index, char c) {
        throw new NumberFormatException("Non-hex character '" + c + "', not valid character for a UUID String' (value 0x" + Integer.toHexString(c) + ") for UUID String \"" + uuidStr + "\"");
    }

    private UUID _fromBytes(byte[] bytes, DeserializationContext ctxt) throws IOException {
        if (bytes.length != 16) {
            ctxt.mappingException("Can only construct UUIDs from byte[16]; got %d bytes", Integer.valueOf(bytes.length));
        }
        return new UUID(_long(bytes, 0), _long(bytes, 8));
    }

    private static long _long(byte[] b, int offset) {
        long l1 = ((long) _int(b, offset)) << 32;
        long l2 = _int(b, offset + 4);
        return l1 | ((l2 << 32) >>> 32);
    }

    private static int _int(byte[] b, int offset) {
        return (b[offset] << 24) | ((b[offset + 1] & 255) << 16) | ((b[offset + 2] & 255) << 8) | (b[offset + 3] & 255);
    }
}
