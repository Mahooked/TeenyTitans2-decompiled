package com.fasterxml.jackson.core.io;

import com.fasterxml.jackson.core.util.BufferRecycler;
import com.fasterxml.jackson.core.util.ByteArrayBuilder;
import com.fasterxml.jackson.core.util.TextBuffer;
import java.lang.ref.SoftReference;
import org.apache.commons.io.IOUtils;

/* JADX INFO: loaded from: classes.dex */
public final class JsonStringEncoder {
    private static final int SURR1_FIRST = 55296;
    private static final int SURR1_LAST = 56319;
    private static final int SURR2_FIRST = 56320;
    private static final int SURR2_LAST = 57343;
    protected ByteArrayBuilder _bytes;
    protected final char[] _qbuf = new char[6];
    protected TextBuffer _text;
    private static final char[] HC = CharTypes.copyHexChars();
    private static final byte[] HB = CharTypes.copyHexBytes();
    protected static final ThreadLocal<SoftReference<JsonStringEncoder>> _threadEncoder = new ThreadLocal<>();

    public JsonStringEncoder() {
        this._qbuf[0] = IOUtils.DIR_SEPARATOR_WINDOWS;
        this._qbuf[2] = '0';
        this._qbuf[3] = '0';
    }

    public static JsonStringEncoder getInstance() {
        SoftReference<JsonStringEncoder> ref = _threadEncoder.get();
        JsonStringEncoder enc = ref == null ? null : ref.get();
        if (enc == null) {
            JsonStringEncoder enc2 = new JsonStringEncoder();
            _threadEncoder.set(new SoftReference<>(enc2));
            return enc2;
        }
        return enc;
    }

    public char[] quoteAsString(String input) {
        TextBuffer textBuffer = this._text;
        if (textBuffer == null) {
            textBuffer = new TextBuffer(null);
            this._text = textBuffer;
        }
        char[] outputBuffer = textBuffer.emptyAndGetCurrentSegment();
        int[] escCodes = CharTypes.get7BitOutputEscapes();
        int escCodeCount = escCodes.length;
        int inPtr = 0;
        int inputLen = input.length();
        int outPtr = 0;
        loop0: while (inPtr < inputLen) {
            while (true) {
                char c = input.charAt(inPtr);
                if (c >= escCodeCount || escCodes[c] == 0) {
                    if (outPtr >= outputBuffer.length) {
                        outputBuffer = textBuffer.finishCurrentSegment();
                        outPtr = 0;
                    }
                    int outPtr2 = outPtr + 1;
                    outputBuffer[outPtr] = c;
                    inPtr++;
                    if (inPtr >= inputLen) {
                        outPtr = outPtr2;
                        break loop0;
                    }
                    outPtr = outPtr2;
                }
            }
            int inPtr2 = inPtr + 1;
            char d = input.charAt(inPtr);
            int escCode = escCodes[d];
            int length = escCode < 0 ? _appendNumeric(d, this._qbuf) : _appendNamed(escCode, this._qbuf);
            if (outPtr + length > outputBuffer.length) {
                int first = outputBuffer.length - outPtr;
                if (first > 0) {
                    System.arraycopy(this._qbuf, 0, outputBuffer, outPtr, first);
                }
                outputBuffer = textBuffer.finishCurrentSegment();
                int second = length - first;
                System.arraycopy(this._qbuf, first, outputBuffer, 0, second);
                outPtr = second;
            } else {
                System.arraycopy(this._qbuf, 0, outputBuffer, outPtr, length);
                outPtr += length;
            }
            inPtr = inPtr2;
        }
        textBuffer.setCurrentLength(outPtr);
        return textBuffer.contentsAsArray();
    }

    public byte[] quoteAsUTF8(String text) {
        int outputPtr;
        int ch;
        int outputPtr2;
        int outputPtr3;
        int outputPtr4;
        ByteArrayBuilder bb = this._bytes;
        if (bb == null) {
            bb = new ByteArrayBuilder((BufferRecycler) null);
            this._bytes = bb;
        }
        int inputPtr = 0;
        int inputEnd = text.length();
        int outputPtr5 = 0;
        byte[] outputBuffer = bb.resetAndGetFirstSegment();
        loop0: while (inputPtr < inputEnd) {
            int[] escCodes = CharTypes.get7BitOutputEscapes();
            while (true) {
                int ch2 = text.charAt(inputPtr);
                if (ch2 > 127 || escCodes[ch2] != 0) {
                    break;
                }
                if (outputPtr5 >= outputBuffer.length) {
                    outputBuffer = bb.finishCurrentSegment();
                    outputPtr5 = 0;
                }
                int outputPtr6 = outputPtr5 + 1;
                outputBuffer[outputPtr5] = (byte) ch2;
                inputPtr++;
                if (inputPtr >= inputEnd) {
                    outputPtr5 = outputPtr6;
                    break loop0;
                }
                outputPtr5 = outputPtr6;
            }
            if (outputPtr5 >= outputBuffer.length) {
                outputBuffer = bb.finishCurrentSegment();
                outputPtr5 = 0;
            }
            int inputPtr2 = inputPtr + 1;
            int ch3 = text.charAt(inputPtr);
            if (ch3 <= 127) {
                int escape = escCodes[ch3];
                outputPtr5 = _appendByte(ch3, escape, bb, outputPtr5);
                outputBuffer = bb.getCurrentSegment();
                inputPtr = inputPtr2;
            } else {
                if (ch3 <= 2047) {
                    outputBuffer[outputPtr5] = (byte) ((ch3 >> 6) | 192);
                    ch = (ch3 & 63) | 128;
                    outputPtr2 = outputPtr5 + 1;
                    inputPtr = inputPtr2;
                } else if (ch3 < 55296 || ch3 > 57343) {
                    int outputPtr7 = outputPtr5 + 1;
                    outputBuffer[outputPtr5] = (byte) ((ch3 >> 12) | 224);
                    if (outputPtr7 >= outputBuffer.length) {
                        outputBuffer = bb.finishCurrentSegment();
                        outputPtr = 0;
                    } else {
                        outputPtr = outputPtr7;
                    }
                    outputBuffer[outputPtr] = (byte) (((ch3 >> 6) & 63) | 128);
                    ch = (ch3 & 63) | 128;
                    outputPtr2 = outputPtr + 1;
                    inputPtr = inputPtr2;
                } else {
                    if (ch3 > 56319) {
                        _illegal(ch3);
                    }
                    if (inputPtr2 >= inputEnd) {
                        _illegal(ch3);
                    }
                    inputPtr = inputPtr2 + 1;
                    int ch4 = _convert(ch3, text.charAt(inputPtr2));
                    if (ch4 > 1114111) {
                        _illegal(ch4);
                    }
                    int outputPtr8 = outputPtr5 + 1;
                    outputBuffer[outputPtr5] = (byte) ((ch4 >> 18) | 240);
                    if (outputPtr8 >= outputBuffer.length) {
                        outputBuffer = bb.finishCurrentSegment();
                        outputPtr3 = 0;
                    } else {
                        outputPtr3 = outputPtr8;
                    }
                    int outputPtr9 = outputPtr3 + 1;
                    outputBuffer[outputPtr3] = (byte) (((ch4 >> 12) & 63) | 128);
                    if (outputPtr9 >= outputBuffer.length) {
                        outputBuffer = bb.finishCurrentSegment();
                        outputPtr4 = 0;
                    } else {
                        outputPtr4 = outputPtr9;
                    }
                    outputBuffer[outputPtr4] = (byte) (((ch4 >> 6) & 63) | 128);
                    ch = (ch4 & 63) | 128;
                    outputPtr2 = outputPtr4 + 1;
                }
                if (outputPtr2 >= outputBuffer.length) {
                    outputBuffer = bb.finishCurrentSegment();
                    outputPtr2 = 0;
                }
                outputBuffer[outputPtr2] = (byte) ch;
                outputPtr5 = outputPtr2 + 1;
            }
        }
        return this._bytes.completeAndCoalesce(outputPtr5);
    }

    public byte[] encodeAsUTF8(String text) {
        int inputPtr;
        int outputPtr;
        int outputPtr2;
        int inputPtr2;
        int outputPtr3;
        ByteArrayBuilder byteBuilder = this._bytes;
        if (byteBuilder == null) {
            byteBuilder = new ByteArrayBuilder((BufferRecycler) null);
            this._bytes = byteBuilder;
        }
        int inputEnd = text.length();
        int outputPtr4 = 0;
        byte[] outputBuffer = byteBuilder.resetAndGetFirstSegment();
        int outputEnd = outputBuffer.length;
        int inputPtr3 = 0;
        loop0: while (inputPtr3 < inputEnd) {
            int inputPtr4 = inputPtr3 + 1;
            int c = text.charAt(inputPtr3);
            while (true) {
                inputPtr = inputPtr4;
                if (c <= 127) {
                    if (outputPtr4 >= outputEnd) {
                        outputBuffer = byteBuilder.finishCurrentSegment();
                        outputEnd = outputBuffer.length;
                        outputPtr4 = 0;
                    }
                    int outputPtr5 = outputPtr4 + 1;
                    outputBuffer[outputPtr4] = (byte) c;
                    if (inputPtr >= inputEnd) {
                        outputPtr4 = outputPtr5;
                        break loop0;
                    }
                    inputPtr4 = inputPtr + 1;
                    c = text.charAt(inputPtr);
                    outputPtr4 = outputPtr5;
                }
            }
            if (outputPtr4 >= outputEnd) {
                outputBuffer = byteBuilder.finishCurrentSegment();
                outputEnd = outputBuffer.length;
                outputPtr = 0;
            } else {
                outputPtr = outputPtr4;
            }
            if (c < 2048) {
                outputPtr2 = outputPtr + 1;
                outputBuffer[outputPtr] = (byte) ((c >> 6) | 192);
                inputPtr2 = inputPtr;
            } else if (c < 55296 || c > 57343) {
                int outputPtr6 = outputPtr + 1;
                outputBuffer[outputPtr] = (byte) ((c >> 12) | 224);
                if (outputPtr6 >= outputEnd) {
                    outputBuffer = byteBuilder.finishCurrentSegment();
                    outputEnd = outputBuffer.length;
                    outputPtr6 = 0;
                }
                outputBuffer[outputPtr6] = (byte) (((c >> 6) & 63) | 128);
                outputPtr2 = outputPtr6 + 1;
                inputPtr2 = inputPtr;
            } else {
                if (c > 56319) {
                    _illegal(c);
                }
                if (inputPtr >= inputEnd) {
                    _illegal(c);
                }
                inputPtr2 = inputPtr + 1;
                c = _convert(c, text.charAt(inputPtr));
                if (c > 1114111) {
                    _illegal(c);
                }
                int outputPtr7 = outputPtr + 1;
                outputBuffer[outputPtr] = (byte) ((c >> 18) | 240);
                if (outputPtr7 >= outputEnd) {
                    outputBuffer = byteBuilder.finishCurrentSegment();
                    outputEnd = outputBuffer.length;
                    outputPtr7 = 0;
                }
                int outputPtr8 = outputPtr7 + 1;
                outputBuffer[outputPtr7] = (byte) (((c >> 12) & 63) | 128);
                if (outputPtr8 >= outputEnd) {
                    outputBuffer = byteBuilder.finishCurrentSegment();
                    outputEnd = outputBuffer.length;
                    outputPtr3 = 0;
                } else {
                    outputPtr3 = outputPtr8;
                }
                outputBuffer[outputPtr3] = (byte) (((c >> 6) & 63) | 128);
                outputPtr2 = outputPtr3 + 1;
            }
            if (outputPtr2 >= outputEnd) {
                outputBuffer = byteBuilder.finishCurrentSegment();
                outputEnd = outputBuffer.length;
                outputPtr2 = 0;
            }
            outputBuffer[outputPtr2] = (byte) ((c & 63) | 128);
            outputPtr4 = outputPtr2 + 1;
            inputPtr3 = inputPtr2;
        }
        return this._bytes.completeAndCoalesce(outputPtr4);
    }

    private int _appendNumeric(int value, char[] qbuf) {
        qbuf[1] = 'u';
        qbuf[4] = HC[value >> 4];
        qbuf[5] = HC[value & 15];
        return 6;
    }

    private int _appendNamed(int esc, char[] qbuf) {
        qbuf[1] = (char) esc;
        return 2;
    }

    private int _appendByte(int ch, int esc, ByteArrayBuilder bb, int ptr) {
        bb.setCurrentSegmentLength(ptr);
        bb.append(92);
        if (esc < 0) {
            bb.append(117);
            if (ch > 255) {
                int hi = ch >> 8;
                bb.append(HB[hi >> 4]);
                bb.append(HB[hi & 15]);
                ch &= 255;
            } else {
                bb.append(48);
                bb.append(48);
            }
            bb.append(HB[ch >> 4]);
            bb.append(HB[ch & 15]);
        } else {
            bb.append((byte) esc);
        }
        return bb.getCurrentSegmentLength();
    }

    private static int _convert(int p1, int p2) {
        if (p2 < 56320 || p2 > 57343) {
            throw new IllegalArgumentException("Broken surrogate pair: first char 0x" + Integer.toHexString(p1) + ", second 0x" + Integer.toHexString(p2) + "; illegal combination");
        }
        return 65536 + ((p1 - 55296) << 10) + (p2 - 56320);
    }

    private static void _illegal(int c) {
        throw new IllegalArgumentException(UTF8Writer.illegalSurrogateDesc(c));
    }
}
