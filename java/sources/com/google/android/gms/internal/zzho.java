package com.google.android.gms.internal;

import java.nio.charset.Charset;
import java.security.MessageDigest;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzho extends zzhf {
    private MessageDigest zzzd;
    private final int zzzg;
    private final int zzzh;

    public zzho(int i) {
        int i2 = i / 8;
        this.zzzg = i % 8 > 0 ? i2 + 1 : i2;
        this.zzzh = i;
    }

    @Override // com.google.android.gms.internal.zzhf
    public final byte[] zzy(String str) {
        byte[] bArr;
        synchronized (this.mLock) {
            this.zzzd = zzcW();
            if (this.zzzd == null) {
                bArr = new byte[0];
            } else {
                this.zzzd.reset();
                this.zzzd.update(str.getBytes(Charset.forName(HTTP.UTF_8)));
                byte[] bArrDigest = this.zzzd.digest();
                bArr = new byte[bArrDigest.length > this.zzzg ? this.zzzg : bArrDigest.length];
                System.arraycopy(bArrDigest, 0, bArr, 0, bArr.length);
                if (this.zzzh % 8 > 0) {
                    long j = 0;
                    for (int i = 0; i < bArr.length; i++) {
                        if (i > 0) {
                            j <<= 8;
                        }
                        j += (long) (bArr[i] & 255);
                    }
                    long j2 = j >>> (8 - (this.zzzh % 8));
                    for (int i2 = this.zzzg - 1; i2 >= 0; i2--) {
                        bArr[i2] = (byte) (255 & j2);
                        j2 >>>= 8;
                    }
                }
            }
        }
        return bArr;
    }
}
