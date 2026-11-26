package com.amazon.mas.kiwi.util;

import android.support.v4.media.TransportMediator;
import android.support.v4.view.MotionEventCompat;
import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.FilterInputStream;
import java.io.FilterOutputStream;
import java.io.IOException;
import java.io.ObjectOutputStream;
import java.io.Serializable;
import java.io.UnsupportedEncodingException;
import java.nio.ByteBuffer;
import java.nio.CharBuffer;
import java.util.zip.GZIPInputStream;
import java.util.zip.GZIPOutputStream;

/* JADX INFO: loaded from: classes.dex */
public class Base64 {
    static final /* synthetic */ boolean $assertionsDisabled;
    public static final int DECODE = 0;
    public static final int DONT_GUNZIP = 4;
    public static final int DO_BREAK_LINES = 8;
    public static final int ENCODE = 1;
    private static final byte EQUALS_SIGN = 61;
    private static final byte EQUALS_SIGN_ENC = -1;
    public static final int GZIP = 2;
    private static final int MAX_LINE_LENGTH = 76;
    private static final byte NEW_LINE = 10;
    public static final int NO_OPTIONS = 0;
    public static final int ORDERED = 32;
    private static final String PREFERRED_ENCODING = "US-ASCII";
    public static final int URL_SAFE = 16;
    private static final byte WHITE_SPACE_ENC = -5;
    private static final byte[] _ORDERED_ALPHABET;
    private static final byte[] _ORDERED_DECODABET;
    private static final byte[] _STANDARD_ALPHABET;
    private static final byte[] _STANDARD_DECODABET;
    private static final byte[] _URL_SAFE_ALPHABET;
    private static final byte[] _URL_SAFE_DECODABET;

    static {
        $assertionsDisabled = !Base64.class.desiredAssertionStatus();
        _STANDARD_ALPHABET = new byte[]{65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 43, 47};
        _STANDARD_DECODABET = new byte[]{-9, -9, -9, -9, -9, -9, -9, -9, -9, WHITE_SPACE_ENC, WHITE_SPACE_ENC, -9, -9, WHITE_SPACE_ENC, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, WHITE_SPACE_ENC, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, 62, -9, -9, -9, 63, 52, 53, 54, 55, 56, 57, 58, 59, 60, EQUALS_SIGN, -9, -9, -9, EQUALS_SIGN_ENC, -9, -9, -9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, NEW_LINE, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, -9, -9, -9, -9, -9, -9, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9};
        _URL_SAFE_ALPHABET = new byte[]{65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 45, 95};
        _URL_SAFE_DECODABET = new byte[]{-9, -9, -9, -9, -9, -9, -9, -9, -9, WHITE_SPACE_ENC, WHITE_SPACE_ENC, -9, -9, WHITE_SPACE_ENC, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, WHITE_SPACE_ENC, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, 62, -9, -9, 52, 53, 54, 55, 56, 57, 58, 59, 60, EQUALS_SIGN, -9, -9, -9, EQUALS_SIGN_ENC, -9, -9, -9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, NEW_LINE, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, -9, -9, -9, -9, 63, -9, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9};
        _ORDERED_ALPHABET = new byte[]{45, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 95, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122};
        _ORDERED_DECODABET = new byte[]{-9, -9, -9, -9, -9, -9, -9, -9, -9, WHITE_SPACE_ENC, WHITE_SPACE_ENC, -9, -9, WHITE_SPACE_ENC, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, WHITE_SPACE_ENC, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, 0, -9, -9, 1, 2, 3, 4, 5, 6, 7, 8, 9, NEW_LINE, -9, -9, -9, EQUALS_SIGN_ENC, -9, -9, -9, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, -9, -9, -9, -9, 37, -9, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, EQUALS_SIGN, 62, 63, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9, -9};
    }

    private static final byte[] getAlphabet(int options) {
        if ((options & 16) == 16) {
            return _URL_SAFE_ALPHABET;
        }
        if ((options & 32) == 32) {
            return _ORDERED_ALPHABET;
        }
        return _STANDARD_ALPHABET;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static final byte[] getDecodabet(int options) {
        if ((options & 16) == 16) {
            return _URL_SAFE_DECODABET;
        }
        if ((options & 32) == 32) {
            return _ORDERED_DECODABET;
        }
        return _STANDARD_DECODABET;
    }

    private Base64() {
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static byte[] encode3to4(byte[] b4, byte[] threeBytes, int numSigBytes, int options) {
        encode3to4(threeBytes, 0, numSigBytes, b4, 0, options);
        return b4;
    }

    /* JADX INFO: Access modifiers changed from: private */
    /* JADX WARN: Can't fix incorrect switch cases order, some code will duplicate */
    public static byte[] encode3to4(byte[] source, int srcOffset, int numSigBytes, byte[] destination, int destOffset, int options) {
        byte[] ALPHABET = getAlphabet(options);
        int inBuff = (numSigBytes > 0 ? (source[srcOffset] << 24) >>> 8 : 0) | (numSigBytes > 1 ? (source[srcOffset + 1] << 24) >>> 16 : 0) | (numSigBytes > 2 ? (source[srcOffset + 2] << 24) >>> 24 : 0);
        switch (numSigBytes) {
            case 1:
                destination[destOffset] = ALPHABET[inBuff >>> 18];
                destination[destOffset + 1] = ALPHABET[(inBuff >>> 12) & 63];
                destination[destOffset + 2] = EQUALS_SIGN;
                destination[destOffset + 3] = EQUALS_SIGN;
                return destination;
            case 2:
                destination[destOffset] = ALPHABET[inBuff >>> 18];
                destination[destOffset + 1] = ALPHABET[(inBuff >>> 12) & 63];
                destination[destOffset + 2] = ALPHABET[(inBuff >>> 6) & 63];
                destination[destOffset + 3] = EQUALS_SIGN;
                return destination;
            case 3:
                destination[destOffset] = ALPHABET[inBuff >>> 18];
                destination[destOffset + 1] = ALPHABET[(inBuff >>> 12) & 63];
                destination[destOffset + 2] = ALPHABET[(inBuff >>> 6) & 63];
                destination[destOffset + 3] = ALPHABET[inBuff & 63];
                return destination;
            default:
                return destination;
        }
    }

    public static void encode(ByteBuffer raw, ByteBuffer encoded) {
        byte[] raw3 = new byte[3];
        byte[] enc4 = new byte[4];
        while (raw.hasRemaining()) {
            int rem = Math.min(3, raw.remaining());
            raw.get(raw3, 0, rem);
            encode3to4(enc4, raw3, rem, 0);
            encoded.put(enc4);
        }
    }

    public static void encode(ByteBuffer raw, CharBuffer encoded) {
        byte[] raw3 = new byte[3];
        byte[] enc4 = new byte[4];
        while (raw.hasRemaining()) {
            int rem = Math.min(3, raw.remaining());
            raw.get(raw3, 0, rem);
            encode3to4(enc4, raw3, rem, 0);
            for (int i = 0; i < 4; i++) {
                encoded.put((char) (enc4[i] & EQUALS_SIGN_ENC));
            }
        }
    }

    public static String encodeObject(Serializable serializableObject) throws IOException {
        return encodeObject(serializableObject, 0);
    }

    public static String encodeObject(Serializable serializableObject, int options) throws Throwable {
        IOException e;
        if (serializableObject == null) {
            throw new NullPointerException("Cannot serialize a null object.");
        }
        ByteArrayOutputStream baos = null;
        OutputStream outputStream = null;
        GZIPOutputStream gzos = null;
        ObjectOutputStream oos = null;
        try {
            baos = new ByteArrayOutputStream();
            try {
                outputStream = new OutputStream(baos, options | 1);
                try {
                    if ((options & 2) != 0) {
                        GZIPOutputStream gzos2 = new GZIPOutputStream(outputStream);
                        try {
                            oos = new ObjectOutputStream(gzos2);
                            gzos = gzos2;
                        } catch (IOException e2) {
                            e = e2;
                            gzos = gzos2;
                            throw e;
                        } catch (Throwable th) {
                            th = th;
                            gzos = gzos2;
                            outputStream = outputStream;
                            baos = baos;
                            try {
                                oos.close();
                            } catch (Exception e3) {
                            }
                            try {
                                gzos.close();
                            } catch (Exception e4) {
                            }
                            try {
                                outputStream.close();
                            } catch (Exception e5) {
                            }
                            try {
                                baos.close();
                                throw th;
                            } catch (Exception e6) {
                                throw th;
                            }
                        }
                    } else {
                        oos = new ObjectOutputStream(outputStream);
                    }
                    oos.writeObject(serializableObject);
                    try {
                        oos.close();
                    } catch (Exception e7) {
                    }
                    try {
                        gzos.close();
                    } catch (Exception e8) {
                    }
                    try {
                        outputStream.close();
                    } catch (Exception e9) {
                    }
                    try {
                        baos.close();
                    } catch (Exception e10) {
                    }
                    try {
                        return new String(baos.toByteArray(), "US-ASCII");
                    } catch (UnsupportedEncodingException e11) {
                        return new String(baos.toByteArray());
                    }
                } catch (IOException e12) {
                    e = e12;
                } catch (Throwable th2) {
                    th = th2;
                    outputStream = outputStream;
                    baos = baos;
                }
            } catch (IOException e13) {
                throw e13;
            } catch (Throwable th3) {
                th = th3;
                baos = baos;
            }
        } catch (IOException e14) {
            throw e14;
        }
    }

    public static String encodeBytes(byte[] source) throws Throwable {
        String encoded = null;
        try {
            encoded = encodeBytes(source, 0, source.length, 0);
        } catch (IOException ex) {
            if (!$assertionsDisabled) {
                throw new AssertionError(ex.getMessage());
            }
        }
        if ($assertionsDisabled || encoded != null) {
            return encoded;
        }
        throw new AssertionError();
    }

    public static String encodeBytes(byte[] source, int options) throws IOException {
        return encodeBytes(source, 0, source.length, options);
    }

    public static String encodeBytes(byte[] source, int off, int len) throws Throwable {
        String encoded = null;
        try {
            encoded = encodeBytes(source, off, len, 0);
        } catch (IOException ex) {
            if (!$assertionsDisabled) {
                throw new AssertionError(ex.getMessage());
            }
        }
        if ($assertionsDisabled || encoded != null) {
            return encoded;
        }
        throw new AssertionError();
    }

    public static String encodeBytes(byte[] source, int off, int len, int options) throws Throwable {
        byte[] encoded = encodeBytesToBytes(source, off, len, options);
        try {
            return new String(encoded, "US-ASCII");
        } catch (UnsupportedEncodingException e) {
            return new String(encoded);
        }
    }

    public static byte[] encodeBytesToBytes(byte[] source) throws Throwable {
        try {
            byte[] encoded = encodeBytesToBytes(source, 0, source.length, 0);
            return encoded;
        } catch (IOException ex) {
            if ($assertionsDisabled) {
                return null;
            }
            throw new AssertionError("IOExceptions only come from GZipping, which is turned off: " + ex.getMessage());
        }
    }

    public static byte[] encodeBytesToBytes(byte[] source, int off, int len, int options) throws Throwable {
        Throwable th;
        GZIPOutputStream gzos;
        ByteArrayOutputStream baos;
        OutputStream b64os;
        IOException e;
        if (source == null) {
            throw new NullPointerException("Cannot serialize a null array.");
        }
        if (off < 0) {
            throw new IllegalArgumentException("Cannot have negative offset: " + off);
        }
        if (len < 0) {
            throw new IllegalArgumentException("Cannot have length offset: " + len);
        }
        if (off + len > source.length) {
            throw new IllegalArgumentException(String.format("Cannot have offset of %d and length of %d with array of length %d", Integer.valueOf(off), Integer.valueOf(len), Integer.valueOf(source.length)));
        }
        if ((options & 2) == 0) {
            boolean breakLines = (options & 8) != 0;
            int encLen = ((len / 3) * 4) + (len % 3 > 0 ? 4 : 0);
            if (breakLines) {
                encLen += encLen / MAX_LINE_LENGTH;
            }
            byte[] outBuff = new byte[encLen];
            int e2 = 0;
            int len2 = len - 2;
            int lineLength = 0;
            int d = 0;
            while (d < len2) {
                encode3to4(source, d + off, 3, outBuff, e2, options);
                int lineLength2 = lineLength + 4;
                if (breakLines && lineLength2 >= MAX_LINE_LENGTH) {
                    int lineLength3 = e2 + 4;
                    outBuff[lineLength3] = NEW_LINE;
                    e2++;
                    lineLength2 = 0;
                }
                int lineLength4 = lineLength2;
                int lineLength5 = d + 3;
                e2 += 4;
                lineLength = lineLength4;
                d = lineLength5;
            }
            if (d < len) {
                encode3to4(source, d + off, len - d, outBuff, e2, options);
                e2 += 4;
            }
            if (e2 > outBuff.length - 1) {
                return outBuff;
            }
            byte[] finalOut = new byte[e2];
            System.arraycopy(outBuff, 0, finalOut, 0, e2);
            return finalOut;
        }
        try {
            ByteArrayOutputStream baos2 = new ByteArrayOutputStream();
            try {
                OutputStream b64os2 = new OutputStream(baos2, options | 1);
                try {
                    GZIPOutputStream gzos2 = new GZIPOutputStream(b64os2);
                    try {
                        gzos2.write(source, off, len);
                        gzos2.close();
                        try {
                            gzos2.close();
                        } catch (Exception e3) {
                        }
                        try {
                            b64os2.close();
                        } catch (Exception e4) {
                        }
                        try {
                            baos2.close();
                        } catch (Exception e5) {
                        }
                        return baos2.toByteArray();
                    } catch (IOException e6) {
                        e = e6;
                        gzos = gzos2;
                        baos = baos2;
                        b64os = b64os2;
                        try {
                            throw e;
                        } catch (Throwable th2) {
                            th = th2;
                            try {
                                gzos.close();
                            } catch (Exception e7) {
                            }
                            try {
                                b64os.close();
                            } catch (Exception e8) {
                            }
                            try {
                                baos.close();
                                throw th;
                            } catch (Exception e9) {
                                throw th;
                            }
                        }
                    } catch (Throwable th3) {
                        gzos = gzos2;
                        baos = baos2;
                        th = th3;
                        b64os = b64os2;
                        gzos.close();
                        b64os.close();
                        baos.close();
                        throw th;
                    }
                } catch (IOException e10) {
                    e = e10;
                    gzos = null;
                    baos = baos2;
                    b64os = b64os2;
                } catch (Throwable th4) {
                    th = th4;
                    gzos = null;
                    baos = baos2;
                    b64os = b64os2;
                }
            } catch (IOException e11) {
                e = e11;
                gzos = null;
                baos = baos2;
                b64os = null;
            } catch (Throwable th5) {
                th = th5;
                gzos = null;
                baos = baos2;
                b64os = null;
            }
        } catch (IOException e12) {
            e = e12;
            gzos = null;
            baos = null;
            b64os = null;
        } catch (Throwable th6) {
            th = th6;
            gzos = null;
            baos = null;
            b64os = null;
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static int decode4to3(byte[] source, int srcOffset, byte[] destination, int destOffset, int options) {
        if (source == null) {
            throw new NullPointerException("Source array was null.");
        }
        if (destination == null) {
            throw new NullPointerException("Destination array was null.");
        }
        if (srcOffset < 0 || srcOffset + 3 >= source.length) {
            throw new IllegalArgumentException(String.format("Source array with length %d cannot have offset of %d and still process four bytes.", Integer.valueOf(source.length), Integer.valueOf(srcOffset)));
        }
        if (destOffset < 0 || destOffset + 2 >= destination.length) {
            throw new IllegalArgumentException(String.format("Destination array with length %d cannot have offset of %d and still store three bytes.", Integer.valueOf(destination.length), Integer.valueOf(destOffset)));
        }
        byte[] DECODABET = getDecodabet(options);
        if (source[srcOffset + 2] == 61) {
            destination[destOffset] = (byte) ((((DECODABET[source[srcOffset]] & EQUALS_SIGN_ENC) << 18) | ((DECODABET[source[srcOffset + 1]] & EQUALS_SIGN_ENC) << 12)) >>> 16);
            return 1;
        }
        if (source[srcOffset + 3] == 61) {
            int outBuff = ((DECODABET[source[srcOffset]] & EQUALS_SIGN_ENC) << 18) | ((DECODABET[source[srcOffset + 1]] & EQUALS_SIGN_ENC) << 12) | ((DECODABET[source[srcOffset + 2]] & EQUALS_SIGN_ENC) << 6);
            destination[destOffset] = (byte) (outBuff >>> 16);
            destination[destOffset + 1] = (byte) (outBuff >>> 8);
            return 2;
        }
        int outBuff2 = ((DECODABET[source[srcOffset]] & EQUALS_SIGN_ENC) << 18) | ((DECODABET[source[srcOffset + 1]] & EQUALS_SIGN_ENC) << 12) | ((DECODABET[source[srcOffset + 2]] & EQUALS_SIGN_ENC) << 6) | (DECODABET[source[srcOffset + 3]] & EQUALS_SIGN_ENC);
        destination[destOffset] = (byte) (outBuff2 >> 16);
        destination[destOffset + 1] = (byte) (outBuff2 >> 8);
        destination[destOffset + 2] = (byte) outBuff2;
        return 3;
    }

    public static byte[] decode(byte[] source) throws IOException {
        byte[] decoded = decode(source, 0, source.length, 0);
        return decoded;
    }

    public static byte[] decode(byte[] source, int off, int len, int options) throws IOException {
        int off2;
        int b4Posn;
        if (source == null) {
            throw new NullPointerException("Cannot decode null source array.");
        }
        if (off < 0 || off + len > source.length) {
            throw new IllegalArgumentException(String.format("Source array with length %d cannot have offset of %d and process %d bytes.", Integer.valueOf(source.length), Integer.valueOf(off), Integer.valueOf(len)));
        }
        if (len == 0) {
            return new byte[0];
        }
        if (len < 4) {
            throw new IllegalArgumentException("Base64-encoded string must have at least four characters, but length specified was " + len);
        }
        byte[] DECODABET = getDecodabet(options);
        int len34 = (len * 3) / 4;
        byte[] outBuff = new byte[len34];
        byte[] b4 = new byte[4];
        int outBuffPosn = 0;
        int i = off;
        int i2 = 0;
        int b4Posn2 = 0;
        while (i < off + len) {
            int sbiDecode = DECODABET[source[i] & EQUALS_SIGN_ENC];
            if (sbiDecode >= -5) {
                if (sbiDecode >= -1) {
                    b4Posn = i2 + 1;
                    b4[i2] = source[i];
                    if (b4Posn > 3) {
                        int b4Posn3 = decode4to3(b4, 0, outBuff, outBuffPosn, options);
                        int outBuffPosn2 = outBuffPosn + b4Posn3;
                        b4Posn = 0;
                        if (source[i] == 61) {
                            off2 = outBuffPosn2;
                            byte[] out = new byte[off2];
                            System.arraycopy(outBuff, 0, out, 0, off2);
                            return out;
                        }
                        outBuffPosn = outBuffPosn2;
                    } else {
                        continue;
                    }
                } else {
                    b4Posn = i2;
                }
                int outBuffPosn3 = i + 1;
                i = outBuffPosn3;
                i2 = b4Posn;
                b4Posn2 = sbiDecode;
            } else {
                throw new IOException(String.format("Bad Base64 input character decimal %d in array position %d", Integer.valueOf(source[i] & EQUALS_SIGN_ENC), Integer.valueOf(i)));
            }
        }
        off2 = outBuffPosn;
        byte[] out2 = new byte[off2];
        System.arraycopy(outBuff, 0, out2, 0, off2);
        return out2;
    }

    public static byte[] decode(String s) throws IOException {
        return decode(s, 0);
    }

    public static byte[] decode(String s, int options) throws IOException {
        byte[] bytes;
        IOException iOException;
        GZIPInputStream gzis;
        GZIPInputStream gzis2;
        int length;
        if (s == null) {
            throw new NullPointerException("Input string was null.");
        }
        try {
            bytes = s.getBytes("US-ASCII");
        } catch (UnsupportedEncodingException e) {
            bytes = s.getBytes();
        }
        byte[] bytes2 = decode(bytes, 0, bytes.length, options);
        boolean dontGunzip = (options & 4) != 0;
        if (bytes2 != null && bytes2.length >= 4 && !dontGunzip) {
            int head = (bytes2[0] & EQUALS_SIGN_ENC) | ((bytes2[1] << 8) & MotionEventCompat.ACTION_POINTER_INDEX_MASK);
            if (35615 == head) {
                ByteArrayInputStream bais = null;
                ByteArrayOutputStream baos = null;
                byte[] buffer = new byte[2048];
                try {
                    ByteArrayOutputStream baos2 = new ByteArrayOutputStream();
                    try {
                        ByteArrayInputStream bais2 = new ByteArrayInputStream(bytes2);
                        try {
                            GZIPInputStream gzis3 = new GZIPInputStream(bais2);
                            int length2 = 0;
                            while (true) {
                                try {
                                    length2 = gzis3.read(buffer);
                                    if (length2 < 0) {
                                        break;
                                    }
                                    baos2.write(buffer, 0, length2);
                                } catch (IOException e2) {
                                    length = length2;
                                    bais = bais2;
                                    baos = baos2;
                                    e = e2;
                                    gzis2 = gzis3;
                                    try {
                                        e.printStackTrace();
                                        try {
                                            baos.close();
                                        } catch (Exception e3) {
                                        }
                                        try {
                                            gzis2.close();
                                        } catch (Exception e4) {
                                        }
                                        try {
                                            bais.close();
                                            return bytes2;
                                        } catch (Exception e5) {
                                            return bytes2;
                                        }
                                    } catch (Throwable e6) {
                                        iOException = e6;
                                        gzis = gzis2;
                                        try {
                                            baos.close();
                                        } catch (Exception e7) {
                                        }
                                        try {
                                            gzis.close();
                                        } catch (Exception e8) {
                                        }
                                        try {
                                            bais.close();
                                            throw iOException;
                                        } catch (Exception e9) {
                                            throw iOException;
                                        }
                                    }
                                } catch (Throwable th) {
                                    iOException = th;
                                    bais = bais2;
                                    baos = baos2;
                                    gzis = gzis3;
                                    baos.close();
                                    gzis.close();
                                    bais.close();
                                    throw iOException;
                                }
                            }
                            byte[] bytes3 = baos2.toByteArray();
                            try {
                                baos2.close();
                            } catch (Exception e10) {
                            }
                            try {
                                gzis3.close();
                            } catch (Exception e11) {
                            }
                            try {
                                bais2.close();
                                return bytes3;
                            } catch (Exception e12) {
                                return bytes3;
                            }
                        } catch (IOException e13) {
                            gzis2 = null;
                            length = 0;
                            e = e13;
                            bais = bais2;
                            baos = baos2;
                        } catch (Throwable th2) {
                            iOException = th2;
                            bais = bais2;
                            baos = baos2;
                            gzis = null;
                        }
                    } catch (IOException e14) {
                        gzis2 = null;
                        length = 0;
                        e = e14;
                        baos = baos2;
                    } catch (Throwable th3) {
                        iOException = th3;
                        baos = baos2;
                        gzis = null;
                    }
                } catch (IOException e15) {
                    e = e15;
                    gzis2 = null;
                    length = 0;
                } catch (Throwable e16) {
                    iOException = e16;
                    gzis = null;
                }
            }
        }
        return bytes2;
    }

    public static void encodeToFile(byte[] dataToEncode, String filename) throws Throwable {
        if (dataToEncode == null) {
            throw new NullPointerException("Data to encode was null.");
        }
        OutputStream bos = null;
        try {
            try {
                OutputStream bos2 = new OutputStream(new FileOutputStream(filename), 1);
                try {
                    bos2.write(dataToEncode);
                    try {
                        bos2.close();
                    } catch (Exception e) {
                    }
                } catch (IOException e2) {
                } catch (Throwable th) {
                    th = th;
                    bos = bos2;
                    try {
                        bos.close();
                    } catch (Exception e3) {
                    }
                    throw th;
                }
            } catch (Throwable th2) {
                th = th2;
            }
        } catch (IOException e4) {
            throw e4;
        }
    }

    public static void decodeToFile(String dataToDecode, String filename) throws Throwable {
        OutputStream bos = null;
        try {
            try {
                OutputStream bos2 = new OutputStream(new FileOutputStream(filename), 0);
                try {
                    bos2.write(dataToDecode.getBytes("US-ASCII"));
                    try {
                        bos2.close();
                    } catch (Exception e) {
                    }
                } catch (IOException e2) {
                } catch (Throwable th) {
                    th = th;
                    bos = bos2;
                    try {
                        bos.close();
                    } catch (Exception e3) {
                    }
                    throw th;
                }
            } catch (IOException e4) {
                throw e4;
            }
        } catch (Throwable th2) {
            th = th2;
        }
    }

    public static byte[] decodeFromFile(String filename) throws Throwable {
        InputStream bis = null;
        try {
            try {
                File file = new File(filename);
                int length = 0;
                if (file.length() > 2147483647L) {
                    throw new IOException("File is too big for this convenience method (" + file.length() + " bytes).");
                }
                byte[] buffer = new byte[(int) file.length()];
                InputStream bis2 = new InputStream(new BufferedInputStream(new FileInputStream(file)), 0);
                while (true) {
                    try {
                        int numBytes = bis2.read(buffer, length, 4096);
                        if (numBytes < 0) {
                            break;
                        }
                        length += numBytes;
                    } catch (IOException e) {
                        throw e;
                    } catch (Throwable th) {
                        th = th;
                        bis = bis2;
                        try {
                            bis.close();
                        } catch (Exception e2) {
                        }
                        throw th;
                    }
                }
                byte[] decodedData = new byte[length];
                System.arraycopy(buffer, 0, decodedData, 0, length);
                try {
                    bis2.close();
                } catch (Exception e3) {
                }
                return decodedData;
            } catch (IOException e4) {
                throw e4;
            }
        } catch (Throwable th2) {
            th = th2;
        }
    }

    public static String encodeFromFile(String filename) throws Throwable {
        InputStream bis = null;
        try {
            try {
                File file = new File(filename);
                byte[] buffer = new byte[Math.max((int) ((file.length() * 1.4d) + 1.0d), 40)];
                int length = 0;
                InputStream bis2 = new InputStream(new BufferedInputStream(new FileInputStream(file)), 1);
                while (true) {
                    try {
                        int numBytes = bis2.read(buffer, length, 4096);
                        if (numBytes < 0) {
                            break;
                        }
                        length += numBytes;
                    } catch (IOException e) {
                        throw e;
                    } catch (Throwable th) {
                        th = th;
                        bis = bis2;
                        try {
                            bis.close();
                        } catch (Exception e2) {
                        }
                        throw th;
                    }
                }
                String encodedData = new String(buffer, 0, length, "US-ASCII");
                try {
                    bis2.close();
                } catch (Exception e3) {
                }
                return encodedData;
            } catch (IOException e4) {
                throw e4;
            }
        } catch (Throwable th2) {
            th = th2;
        }
    }

    public static void encodeFileToFile(String infile, String outfile) throws Throwable {
        String encoded = encodeFromFile(infile);
        java.io.OutputStream out = null;
        try {
            try {
                java.io.OutputStream out2 = new BufferedOutputStream(new FileOutputStream(outfile));
                try {
                    out2.write(encoded.getBytes("US-ASCII"));
                    try {
                        out2.close();
                    } catch (Exception e) {
                    }
                } catch (IOException e2) {
                } catch (Throwable th) {
                    th = th;
                    out = out2;
                    try {
                        out.close();
                    } catch (Exception e3) {
                    }
                    throw th;
                }
            } catch (Throwable th2) {
                th = th2;
            }
        } catch (IOException e4) {
            throw e4;
        }
    }

    public static void decodeFileToFile(String infile, String outfile) throws Throwable {
        byte[] decoded = decodeFromFile(infile);
        java.io.OutputStream out = null;
        try {
            try {
                java.io.OutputStream out2 = new BufferedOutputStream(new FileOutputStream(outfile));
                try {
                    out2.write(decoded);
                    try {
                        out2.close();
                    } catch (Exception e) {
                    }
                } catch (IOException e2) {
                } catch (Throwable th) {
                    th = th;
                    out = out2;
                    try {
                        out.close();
                    } catch (Exception e3) {
                    }
                    throw th;
                }
            } catch (IOException e4) {
                throw e4;
            }
        } catch (Throwable th2) {
            th = th2;
        }
    }

    public static class InputStream extends FilterInputStream {
        private boolean breakLines;
        private byte[] buffer;
        private int bufferLength;
        private byte[] decodabet;
        private boolean encode;
        private int lineLength;
        private int numSigBytes;
        private int options;
        private int position;

        public InputStream(java.io.InputStream in) {
            this(in, 0);
        }

        public InputStream(java.io.InputStream in, int options) {
            super(in);
            this.options = options;
            this.breakLines = (options & 8) > 0;
            this.encode = (options & 1) > 0;
            this.bufferLength = this.encode ? 4 : 3;
            this.buffer = new byte[this.bufferLength];
            this.position = -1;
            this.lineLength = 0;
            this.decodabet = Base64.getDecodabet(options);
        }

        @Override // java.io.FilterInputStream, java.io.InputStream
        public int read() throws IOException {
            int b;
            if (this.position < 0) {
                if (this.encode) {
                    byte[] b3 = new byte[3];
                    int numBinaryBytes = 0;
                    for (int i = 0; i < 3; i++) {
                        int b2 = this.in.read();
                        if (b2 < 0) {
                            break;
                        }
                        b3[i] = (byte) b2;
                        numBinaryBytes++;
                    }
                    if (numBinaryBytes <= 0) {
                        return -1;
                    }
                    Base64.encode3to4(b3, 0, numBinaryBytes, this.buffer, 0, this.options);
                    this.position = 0;
                    this.numSigBytes = 4;
                } else {
                    byte[] b4 = new byte[4];
                    int i2 = 0;
                    while (i2 < 4) {
                        do {
                            b = this.in.read();
                            if (b < 0) {
                                break;
                            }
                        } while (this.decodabet[b & TransportMediator.KEYCODE_MEDIA_PAUSE] <= -5);
                        if (b < 0) {
                            break;
                        }
                        b4[i2] = (byte) b;
                        i2++;
                    }
                    if (i2 == 4) {
                        this.numSigBytes = Base64.decode4to3(b4, 0, this.buffer, 0, this.options);
                        this.position = 0;
                    } else {
                        if (i2 == 0) {
                            return -1;
                        }
                        throw new IOException("Improperly padded Base64 input.");
                    }
                }
            }
            if (this.position >= 0) {
                if (this.position >= this.numSigBytes) {
                    return -1;
                }
                if (this.encode && this.breakLines && this.lineLength >= Base64.MAX_LINE_LENGTH) {
                    this.lineLength = 0;
                    return 10;
                }
                this.lineLength++;
                byte[] bArr = this.buffer;
                int i3 = this.position;
                this.position = i3 + 1;
                int b5 = bArr[i3];
                if (this.position >= this.bufferLength) {
                    this.position = -1;
                }
                return b5 & 255;
            }
            throw new IOException("Error in Base64 code reading stream.");
        }

        @Override // java.io.FilterInputStream, java.io.InputStream
        public int read(byte[] dest, int off, int len) throws IOException {
            int i = 0;
            while (i < len) {
                int b = read();
                if (b >= 0) {
                    dest[off + i] = (byte) b;
                    i++;
                } else {
                    if (i != 0) {
                        break;
                    }
                    return -1;
                }
            }
            return i;
        }
    }

    public static class OutputStream extends FilterOutputStream {
        private byte[] b4;
        private boolean breakLines;
        private byte[] buffer;
        private int bufferLength;
        private byte[] decodabet;
        private boolean encode;
        private int lineLength;
        private int options;
        private int position;
        private boolean suspendEncoding;

        public OutputStream(java.io.OutputStream out) {
            this(out, 1);
        }

        public OutputStream(java.io.OutputStream out, int options) {
            super(out);
            this.breakLines = (options & 8) != 0;
            this.encode = (options & 1) != 0;
            this.bufferLength = this.encode ? 3 : 4;
            this.buffer = new byte[this.bufferLength];
            this.position = 0;
            this.lineLength = 0;
            this.suspendEncoding = false;
            this.b4 = new byte[4];
            this.options = options;
            this.decodabet = Base64.getDecodabet(options);
        }

        @Override // java.io.FilterOutputStream, java.io.OutputStream
        public void write(int theByte) throws IOException {
            if (this.suspendEncoding) {
                this.out.write(theByte);
                return;
            }
            if (this.encode) {
                byte[] bArr = this.buffer;
                int i = this.position;
                this.position = i + 1;
                bArr[i] = (byte) theByte;
                if (this.position >= this.bufferLength) {
                    this.out.write(Base64.encode3to4(this.b4, this.buffer, this.bufferLength, this.options));
                    this.lineLength += 4;
                    if (this.breakLines && this.lineLength >= Base64.MAX_LINE_LENGTH) {
                        this.out.write(10);
                        this.lineLength = 0;
                    }
                    this.position = 0;
                    return;
                }
                return;
            }
            if (this.decodabet[theByte & TransportMediator.KEYCODE_MEDIA_PAUSE] > -5) {
                byte[] bArr2 = this.buffer;
                int i2 = this.position;
                this.position = i2 + 1;
                bArr2[i2] = (byte) theByte;
                if (this.position >= this.bufferLength) {
                    int len = Base64.decode4to3(this.buffer, 0, this.b4, 0, this.options);
                    this.out.write(this.b4, 0, len);
                    this.position = 0;
                    return;
                }
                return;
            }
            if (this.decodabet[theByte & TransportMediator.KEYCODE_MEDIA_PAUSE] != -5) {
                throw new IOException("Invalid character in Base64 data.");
            }
        }

        @Override // java.io.FilterOutputStream, java.io.OutputStream
        public void write(byte[] theBytes, int off, int len) throws IOException {
            if (this.suspendEncoding) {
                this.out.write(theBytes, off, len);
                return;
            }
            for (int i = 0; i < len; i++) {
                write(theBytes[off + i]);
            }
        }

        public void flushBase64() throws IOException {
            if (this.position > 0) {
                if (this.encode) {
                    this.out.write(Base64.encode3to4(this.b4, this.buffer, this.position, this.options));
                    this.position = 0;
                    return;
                }
                throw new IOException("Base64 input not properly padded.");
            }
        }

        @Override // java.io.FilterOutputStream, java.io.OutputStream, java.io.Closeable, java.lang.AutoCloseable
        public void close() throws IOException {
            flushBase64();
            super.close();
            this.buffer = null;
            this.out = null;
        }

        public void suspendEncoding() throws IOException {
            flushBase64();
            this.suspendEncoding = true;
        }

        public void resumeEncoding() {
            this.suspendEncoding = false;
        }
    }
}
