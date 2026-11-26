package com.google.android.gms.internal;

import com.fasterxml.jackson.core.util.MinimalPrettyPrinter;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.security.MessageDigest;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzhk extends zzhf {
    private MessageDigest zzzd;

    @Override // com.google.android.gms.internal.zzhf
    public final byte[] zzy(String str) {
        byte[] bArrArray;
        byte[] bArr;
        String[] strArrSplit = str.split(MinimalPrettyPrinter.DEFAULT_ROOT_VALUE_SEPARATOR);
        if (strArrSplit.length == 1) {
            int iZzA = zzhj.zzA(strArrSplit[0]);
            ByteBuffer byteBufferAllocate = ByteBuffer.allocate(4);
            byteBufferAllocate.order(ByteOrder.LITTLE_ENDIAN);
            byteBufferAllocate.putInt(iZzA);
            bArrArray = byteBufferAllocate.array();
        } else if (strArrSplit.length < 5) {
            byte[] bArr2 = new byte[strArrSplit.length << 1];
            for (int i = 0; i < strArrSplit.length; i++) {
                int iZzA2 = zzhj.zzA(strArrSplit[i]);
                int i2 = (iZzA2 >> 16) ^ (65535 & iZzA2);
                byte[] bArr3 = {(byte) i2, (byte) (i2 >> 8)};
                bArr2[i << 1] = bArr3[0];
                bArr2[(i << 1) + 1] = bArr3[1];
            }
            bArrArray = bArr2;
        } else {
            bArrArray = new byte[strArrSplit.length];
            for (int i3 = 0; i3 < strArrSplit.length; i3++) {
                int iZzA3 = zzhj.zzA(strArrSplit[i3]);
                bArrArray[i3] = (byte) ((iZzA3 >> 24) ^ (((iZzA3 & 255) ^ ((iZzA3 >> 8) & 255)) ^ ((iZzA3 >> 16) & 255)));
            }
        }
        this.zzzd = zzcW();
        synchronized (this.mLock) {
            if (this.zzzd == null) {
                bArr = new byte[0];
            } else {
                this.zzzd.reset();
                this.zzzd.update(bArrArray);
                byte[] bArrDigest = this.zzzd.digest();
                bArr = new byte[bArrDigest.length > 4 ? 4 : bArrDigest.length];
                System.arraycopy(bArrDigest, 0, bArr, 0, bArr.length);
            }
        }
        return bArr;
    }
}
