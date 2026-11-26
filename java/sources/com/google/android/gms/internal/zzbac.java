package com.google.android.gms.internal;

import java.nio.ByteBuffer;
import java.nio.ByteOrder;

/* JADX INFO: loaded from: classes.dex */
public final class zzbac {
    private static long zza(long j, long j2, long j3) {
        long j4 = (j ^ j2) * j3;
        long j5 = ((j4 ^ (j4 >>> 47)) ^ j2) * j3;
        return (j5 ^ (j5 >>> 47)) * j3;
    }

    private static long zza(byte[] bArr, int i, int i2) {
        long[] jArr = new long[2];
        long[] jArr2 = new long[2];
        int i3 = (((i2 - 1) / 64) << 6) + 0;
        int i4 = (((i2 - 1) & 63) + i3) - 63;
        long jZzc = 95310865018149119L + zzc(bArr, 0);
        long j = 2480279821605975764L;
        long j2 = 1390051526045402406L;
        int i5 = i;
        while (true) {
            long jRotateRight = Long.rotateRight(jZzc + j + jArr[0] + zzc(bArr, i5 + 8), 37) * (-5435081209227447693L);
            long jRotateRight2 = Long.rotateRight(j + jArr[1] + zzc(bArr, i5 + 48), 42) * (-5435081209227447693L);
            long j3 = jRotateRight ^ jArr2[1];
            long jZzc2 = jRotateRight2 + jArr[0] + zzc(bArr, i5 + 40);
            long jRotateRight3 = Long.rotateRight(j2 + jArr2[0], 33) * (-5435081209227447693L);
            zza(bArr, i5, jArr[1] * (-5435081209227447693L), jArr2[0] + j3, jArr);
            zza(bArr, i5 + 32, jRotateRight3 + jArr2[1], jZzc2 + zzc(bArr, i5 + 16), jArr2);
            i5 += 64;
            if (i5 == i3) {
                long j4 = (-5435081209227447693L) + ((255 & j3) << 1);
                jArr2[0] = jArr2[0] + ((long) ((i2 - 1) & 63));
                jArr[0] = jArr[0] + jArr2[0];
                jArr2[0] = jArr2[0] + jArr[0];
                long jRotateRight4 = Long.rotateRight(jRotateRight3 + jZzc2 + jArr[0] + zzc(bArr, i4 + 8), 37) * j4;
                long jRotateRight5 = Long.rotateRight(jArr[1] + jZzc2 + zzc(bArr, i4 + 48), 42) * j4;
                long j5 = jRotateRight4 ^ (jArr2[1] * 9);
                long jZzc3 = jRotateRight5 + (jArr[0] * 9) + zzc(bArr, i4 + 40);
                long jRotateRight6 = Long.rotateRight(jArr2[0] + j3, 33) * j4;
                zza(bArr, i4, jArr[1] * j4, j5 + jArr2[0], jArr);
                zza(bArr, i4 + 32, jRotateRight6 + jArr2[1], jZzc3 + zzc(bArr, i4 + 16), jArr2);
                return zza(zza(jArr[0], jArr2[0], j4) + (((jZzc3 >>> 47) ^ jZzc3) * (-4348849565147123417L)) + j5, zza(jArr[1], jArr2[1], j4) + jRotateRight6, j4);
            }
            j2 = j3;
            j = jZzc2;
            jZzc = jRotateRight3;
        }
    }

    private static void zza(byte[] bArr, int i, long j, long j2, long[] jArr) {
        long jZzc = zzc(bArr, i);
        long jZzc2 = zzc(bArr, i + 8);
        long jZzc3 = zzc(bArr, i + 16);
        long jZzc4 = zzc(bArr, i + 24);
        long j3 = jZzc + j;
        long j4 = jZzc2 + j3 + jZzc3;
        long jRotateRight = Long.rotateRight(j4, 44) + Long.rotateRight(j2 + j3 + jZzc4, 21);
        jArr[0] = j4 + jZzc4;
        jArr[1] = j3 + jRotateRight;
    }

    private static int zzb(byte[] bArr, int i) {
        return (bArr[i] & 255) | ((bArr[i + 1] & 255) << 8) | ((bArr[i + 2] & 255) << 16) | ((bArr[i + 3] & 255) << 24);
    }

    private static long zzc(byte[] bArr, int i) {
        ByteBuffer byteBufferWrap = ByteBuffer.wrap(bArr, i, 8);
        byteBufferWrap.order(ByteOrder.LITTLE_ENDIAN);
        return byteBufferWrap.getLong();
    }

    public static long zzf(byte[] bArr) {
        int length = bArr.length;
        if (length < 0 || length > bArr.length) {
            throw new IndexOutOfBoundsException(new StringBuilder(67).append("Out of bound index with offput: 0 and length: ").append(length).toString());
        }
        if (length > 32) {
            if (length > 64) {
                return zza(bArr, 0, length);
            }
            long j = (-7286425919675154353L) + ((long) (length << 1));
            long jZzc = zzc(bArr, 0) * (-7286425919675154353L);
            long jZzc2 = zzc(bArr, 8);
            long jZzc3 = zzc(bArr, (length + 0) - 8) * j;
            long jZzc4 = (zzc(bArr, (length + 0) - 16) * (-7286425919675154353L)) + Long.rotateRight(jZzc + jZzc2, 43) + Long.rotateRight(jZzc3, 30);
            long jZza = zza(jZzc4, Long.rotateRight(jZzc2 - 7286425919675154353L, 18) + jZzc + jZzc3, j);
            long jZzc5 = zzc(bArr, 16) * j;
            long jZzc6 = zzc(bArr, 24);
            long jZzc7 = (jZzc4 + zzc(bArr, (length + 0) - 32)) * j;
            return zza(((zzc(bArr, (length + 0) - 24) + jZza) * j) + Long.rotateRight(jZzc5 + jZzc6, 43) + Long.rotateRight(jZzc7, 30), Long.rotateRight(jZzc6 + jZzc, 18) + jZzc5 + jZzc7, j);
        }
        if (length > 16) {
            long j2 = (-7286425919675154353L) + ((long) (length << 1));
            long jZzc8 = (-5435081209227447693L) * zzc(bArr, 0);
            long jZzc9 = zzc(bArr, 8);
            long jZzc10 = zzc(bArr, (length + 0) - 8) * j2;
            return zza((zzc(bArr, (length + 0) - 16) * (-7286425919675154353L)) + Long.rotateRight(jZzc8 + jZzc9, 43) + Long.rotateRight(jZzc10, 30), jZzc8 + Long.rotateRight((-7286425919675154353L) + jZzc9, 18) + jZzc10, j2);
        }
        if (length >= 8) {
            long j3 = (-7286425919675154353L) + ((long) (length << 1));
            long jZzc11 = (-7286425919675154353L) + zzc(bArr, 0);
            long jZzc12 = zzc(bArr, (length + 0) - 8);
            return zza((Long.rotateRight(jZzc12, 37) * j3) + jZzc11, (Long.rotateRight(jZzc11, 25) + jZzc12) * j3, j3);
        }
        if (length >= 4) {
            return zza(((((long) zzb(bArr, 0)) & 4294967295L) << 3) + ((long) length), ((long) zzb(bArr, (length + 0) - 4)) & 4294967295L, (-7286425919675154353L) + ((long) (length << 1)));
        }
        if (length <= 0) {
            return -7286425919675154353L;
        }
        long j4 = (((long) (((bArr[(length - 1) + 0] & 255) << 2) + length)) * (-4348849565147123417L)) ^ (((long) ((bArr[0] & 255) + ((bArr[(length >> 1) + 0] & 255) << 8))) * (-7286425919675154353L));
        return (j4 ^ (j4 >>> 47)) * (-7286425919675154353L);
    }
}
