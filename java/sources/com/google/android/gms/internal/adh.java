package com.google.android.gms.internal;

import android.support.v4.media.TransportMediator;
import java.io.IOException;
import java.nio.BufferOverflowException;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.ReadOnlyBufferException;

/* JADX INFO: loaded from: classes.dex */
public final class adh {
    private final ByteBuffer zzcsn;

    private adh(ByteBuffer byteBuffer) {
        this.zzcsn = byteBuffer;
        this.zzcsn.order(ByteOrder.LITTLE_ENDIAN);
    }

    private adh(byte[] bArr, int i, int i2) {
        this(ByteBuffer.wrap(bArr, i, i2));
    }

    public static adh zzI(byte[] bArr) {
        return zzc(bArr, 0, bArr.length);
    }

    public static int zzJ(byte[] bArr) {
        return zzcv(bArr.length) + bArr.length;
    }

    private static int zza(CharSequence charSequence, byte[] bArr, int i, int i2) {
        int i3;
        int length = charSequence.length();
        int i4 = 0;
        int i5 = i + i2;
        while (i4 < length && i4 + i < i5) {
            char cCharAt = charSequence.charAt(i4);
            if (cCharAt >= 128) {
                break;
            }
            bArr[i + i4] = (byte) cCharAt;
            i4++;
        }
        if (i4 == length) {
            return i + length;
        }
        int i6 = i + i4;
        while (i4 < length) {
            char cCharAt2 = charSequence.charAt(i4);
            if (cCharAt2 < 128 && i6 < i5) {
                i3 = i6 + 1;
                bArr[i6] = (byte) cCharAt2;
            } else if (cCharAt2 < 2048 && i6 <= i5 - 2) {
                int i7 = i6 + 1;
                bArr[i6] = (byte) ((cCharAt2 >>> 6) | 960);
                i3 = i7 + 1;
                bArr[i7] = (byte) ((cCharAt2 & '?') | 128);
            } else {
                if ((cCharAt2 >= 55296 && 57343 >= cCharAt2) || i6 > i5 - 3) {
                    if (i6 > i5 - 4) {
                        throw new ArrayIndexOutOfBoundsException(new StringBuilder(37).append("Failed writing ").append(cCharAt2).append(" at index ").append(i6).toString());
                    }
                    if (i4 + 1 != charSequence.length()) {
                        i4++;
                        char cCharAt3 = charSequence.charAt(i4);
                        if (Character.isSurrogatePair(cCharAt2, cCharAt3)) {
                            int codePoint = Character.toCodePoint(cCharAt2, cCharAt3);
                            int i8 = i6 + 1;
                            bArr[i6] = (byte) ((codePoint >>> 18) | 240);
                            int i9 = i8 + 1;
                            bArr[i8] = (byte) (((codePoint >>> 12) & 63) | 128);
                            int i10 = i9 + 1;
                            bArr[i9] = (byte) (((codePoint >>> 6) & 63) | 128);
                            i3 = i10 + 1;
                            bArr[i10] = (byte) ((codePoint & 63) | 128);
                        }
                    }
                    throw new IllegalArgumentException(new StringBuilder(39).append("Unpaired surrogate at index ").append(i4 - 1).toString());
                }
                int i11 = i6 + 1;
                bArr[i6] = (byte) ((cCharAt2 >>> '\f') | 480);
                int i12 = i11 + 1;
                bArr[i11] = (byte) (((cCharAt2 >>> 6) & 63) | 128);
                i3 = i12 + 1;
                bArr[i12] = (byte) ((cCharAt2 & '?') | 128);
            }
            i4++;
            i6 = i3;
        }
        return i6;
    }

    private static void zza(CharSequence charSequence, ByteBuffer byteBuffer) {
        if (byteBuffer.isReadOnly()) {
            throw new ReadOnlyBufferException();
        }
        if (!byteBuffer.hasArray()) {
            zzb(charSequence, byteBuffer);
            return;
        }
        try {
            byteBuffer.position(zza(charSequence, byteBuffer.array(), byteBuffer.arrayOffset() + byteBuffer.position(), byteBuffer.remaining()) - byteBuffer.arrayOffset());
        } catch (ArrayIndexOutOfBoundsException e) {
            BufferOverflowException bufferOverflowException = new BufferOverflowException();
            bufferOverflowException.initCause(e);
            throw bufferOverflowException;
        }
    }

    private final void zzaO(long j) throws IOException {
        while (((-128) & j) != 0) {
            zzcs((((int) j) & TransportMediator.KEYCODE_MEDIA_PAUSE) | 128);
            j >>>= 7;
        }
        zzcs((int) j);
    }

    public static int zzaP(long j) {
        if (((-128) & j) == 0) {
            return 1;
        }
        if (((-16384) & j) == 0) {
            return 2;
        }
        if (((-2097152) & j) == 0) {
            return 3;
        }
        if (((-268435456) & j) == 0) {
            return 4;
        }
        if (((-34359738368L) & j) == 0) {
            return 5;
        }
        if (((-4398046511104L) & j) == 0) {
            return 6;
        }
        if (((-562949953421312L) & j) == 0) {
            return 7;
        }
        if (((-72057594037927936L) & j) == 0) {
            return 8;
        }
        return (Long.MIN_VALUE & j) == 0 ? 9 : 10;
    }

    private final void zzaQ(long j) throws IOException {
        if (this.zzcsn.remaining() < 8) {
            throw new adi(this.zzcsn.position(), this.zzcsn.limit());
        }
        this.zzcsn.putLong(j);
    }

    private static long zzaR(long j) {
        return (j << 1) ^ (j >> 63);
    }

    public static int zzb(int i, adp adpVar) {
        int iZzct = zzct(i);
        int iZzLV = adpVar.zzLV();
        return iZzct + iZzLV + zzcv(iZzLV);
    }

    private static int zzb(CharSequence charSequence) {
        int i;
        int i2 = 0;
        int length = charSequence.length();
        int i3 = 0;
        while (i3 < length && charSequence.charAt(i3) < 128) {
            i3++;
        }
        int i4 = length;
        while (true) {
            if (i3 < length) {
                char cCharAt = charSequence.charAt(i3);
                if (cCharAt >= 2048) {
                    int length2 = charSequence.length();
                    while (i3 < length2) {
                        char cCharAt2 = charSequence.charAt(i3);
                        if (cCharAt2 < 2048) {
                            i2 += (127 - cCharAt2) >>> 31;
                        } else {
                            i2 += 2;
                            if (55296 <= cCharAt2 && cCharAt2 <= 57343) {
                                if (Character.codePointAt(charSequence, i3) < 65536) {
                                    throw new IllegalArgumentException(new StringBuilder(39).append("Unpaired surrogate at index ").append(i3).toString());
                                }
                                i3++;
                            }
                        }
                        i3++;
                    }
                    i = i4 + i2;
                    break;
                }
                i4 += (127 - cCharAt) >>> 31;
                i3++;
            } else {
                i = i4;
                break;
            }
        }
        if (i >= length) {
            return i;
        }
        throw new IllegalArgumentException(new StringBuilder(54).append("UTF-8 length does not fit in int: ").append(((long) i) + 4294967296L).toString());
    }

    private static void zzb(CharSequence charSequence, ByteBuffer byteBuffer) {
        int length = charSequence.length();
        int i = 0;
        while (i < length) {
            char cCharAt = charSequence.charAt(i);
            if (cCharAt < 128) {
                byteBuffer.put((byte) cCharAt);
            } else if (cCharAt < 2048) {
                byteBuffer.put((byte) ((cCharAt >>> 6) | 960));
                byteBuffer.put((byte) ((cCharAt & '?') | 128));
            } else {
                if (cCharAt >= 55296 && 57343 >= cCharAt) {
                    if (i + 1 != charSequence.length()) {
                        i++;
                        char cCharAt2 = charSequence.charAt(i);
                        if (Character.isSurrogatePair(cCharAt, cCharAt2)) {
                            int codePoint = Character.toCodePoint(cCharAt, cCharAt2);
                            byteBuffer.put((byte) ((codePoint >>> 18) | 240));
                            byteBuffer.put((byte) (((codePoint >>> 12) & 63) | 128));
                            byteBuffer.put((byte) (((codePoint >>> 6) & 63) | 128));
                            byteBuffer.put((byte) ((codePoint & 63) | 128));
                        }
                    }
                    throw new IllegalArgumentException(new StringBuilder(39).append("Unpaired surrogate at index ").append(i - 1).toString());
                }
                byteBuffer.put((byte) ((cCharAt >>> '\f') | 480));
                byteBuffer.put((byte) (((cCharAt >>> 6) & 63) | 128));
                byteBuffer.put((byte) ((cCharAt & '?') | 128));
            }
            i++;
        }
    }

    public static int zzc(int i, byte[] bArr) {
        return zzct(i) + zzJ(bArr);
    }

    public static adh zzc(byte[] bArr, int i, int i2) {
        return new adh(bArr, 0, i2);
    }

    public static int zzcr(int i) {
        if (i >= 0) {
            return zzcv(i);
        }
        return 10;
    }

    private final void zzcs(int i) throws IOException {
        byte b = (byte) i;
        if (!this.zzcsn.hasRemaining()) {
            throw new adi(this.zzcsn.position(), this.zzcsn.limit());
        }
        this.zzcsn.put(b);
    }

    public static int zzct(int i) {
        return zzcv(i << 3);
    }

    public static int zzcv(int i) {
        if ((i & (-128)) == 0) {
            return 1;
        }
        if ((i & (-16384)) == 0) {
            return 2;
        }
        if (((-2097152) & i) == 0) {
            return 3;
        }
        return ((-268435456) & i) == 0 ? 4 : 5;
    }

    public static int zzcw(int i) {
        return (i << 1) ^ (i >> 31);
    }

    public static int zze(int i, long j) {
        return zzct(i) + zzaP(j);
    }

    public static int zzf(int i, long j) {
        return zzct(i) + zzaP(zzaR(j));
    }

    public static int zzhQ(String str) {
        int iZzb = zzb(str);
        return iZzb + zzcv(iZzb);
    }

    public static int zzm(int i, String str) {
        return zzct(i) + zzhQ(str);
    }

    public static int zzs(int i, int i2) {
        return zzct(i) + zzcr(i2);
    }

    public final void zzK(byte[] bArr) throws IOException {
        int length = bArr.length;
        if (this.zzcsn.remaining() < length) {
            throw new adi(this.zzcsn.position(), this.zzcsn.limit());
        }
        this.zzcsn.put(bArr, 0, length);
    }

    public final void zzLM() {
        if (this.zzcsn.remaining() != 0) {
            throw new IllegalStateException("Did not write as much data as expected.");
        }
    }

    public final void zza(int i, double d) throws IOException {
        zzt(i, 1);
        zzaQ(Double.doubleToLongBits(d));
    }

    public final void zza(int i, long j) throws IOException {
        zzt(i, 0);
        zzaO(j);
    }

    public final void zza(int i, adp adpVar) throws IOException {
        zzt(i, 2);
        zzb(adpVar);
    }

    public final void zzb(int i, long j) throws IOException {
        zzt(i, 0);
        zzaO(j);
    }

    public final void zzb(int i, byte[] bArr) throws IOException {
        zzt(i, 2);
        zzcu(bArr.length);
        zzK(bArr);
    }

    public final void zzb(adp adpVar) throws IOException {
        zzcu(adpVar.zzLU());
        adpVar.zza(this);
    }

    public final void zzc(int i, float f) throws IOException {
        zzt(i, 5);
        int iFloatToIntBits = Float.floatToIntBits(f);
        if (this.zzcsn.remaining() < 4) {
            throw new adi(this.zzcsn.position(), this.zzcsn.limit());
        }
        this.zzcsn.putInt(iFloatToIntBits);
    }

    public final void zzc(int i, long j) throws IOException {
        zzt(i, 1);
        zzaQ(j);
    }

    public final void zzcu(int i) throws IOException {
        while ((i & (-128)) != 0) {
            zzcs((i & TransportMediator.KEYCODE_MEDIA_PAUSE) | 128);
            i >>>= 7;
        }
        zzcs(i);
    }

    public final void zzd(int i, long j) throws IOException {
        zzt(i, 0);
        zzaO(zzaR(j));
    }

    public final void zzk(int i, boolean z) throws IOException {
        zzt(i, 0);
        byte b = (byte) (z ? 1 : 0);
        if (!this.zzcsn.hasRemaining()) {
            throw new adi(this.zzcsn.position(), this.zzcsn.limit());
        }
        this.zzcsn.put(b);
    }

    public final void zzl(int i, String str) throws IOException {
        zzt(i, 2);
        try {
            int iZzcv = zzcv(str.length());
            if (iZzcv != zzcv(str.length() * 3)) {
                zzcu(zzb(str));
                zza(str, this.zzcsn);
                return;
            }
            int iPosition = this.zzcsn.position();
            if (this.zzcsn.remaining() < iZzcv) {
                throw new adi(iZzcv + iPosition, this.zzcsn.limit());
            }
            this.zzcsn.position(iPosition + iZzcv);
            zza(str, this.zzcsn);
            int iPosition2 = this.zzcsn.position();
            this.zzcsn.position(iPosition);
            zzcu((iPosition2 - iPosition) - iZzcv);
            this.zzcsn.position(iPosition2);
        } catch (BufferOverflowException e) {
            adi adiVar = new adi(this.zzcsn.position(), this.zzcsn.limit());
            adiVar.initCause(e);
            throw adiVar;
        }
    }

    public final void zzr(int i, int i2) throws IOException {
        zzt(i, 0);
        if (i2 >= 0) {
            zzcu(i2);
        } else {
            zzaO(i2);
        }
    }

    public final void zzt(int i, int i2) throws IOException {
        zzcu((i << 3) | i2);
    }
}
