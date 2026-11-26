package com.google.android.gms.internal;

import java.nio.ByteBuffer;
import java.security.InvalidAlgorithmParameterException;
import java.security.InvalidKeyException;
import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;
import javax.crypto.BadPaddingException;
import javax.crypto.Cipher;
import javax.crypto.IllegalBlockSizeException;
import javax.crypto.NoSuchPaddingException;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.SecretKeySpec;

/* JADX INFO: loaded from: classes.dex */
public final class zzcw {
    private static Cipher zzqv = null;
    private static final Object zzqw = new Object();
    private static final Object zzqx = new Object();
    private final SecureRandom zzqu = null;

    public zzcw(SecureRandom secureRandom) {
    }

    private static Cipher getCipher() throws NoSuchPaddingException, NoSuchAlgorithmException {
        Cipher cipher;
        synchronized (zzqx) {
            if (zzqv == null) {
                zzqv = Cipher.getInstance("AES/CBC/PKCS5Padding");
            }
            cipher = zzqv;
        }
        return cipher;
    }

    public final byte[] zza(byte[] bArr, String str) throws zzcx {
        byte[] bArrDoFinal;
        if (bArr.length != 16) {
            throw new zzcx(this);
        }
        try {
            byte[] bArrZza = zzbt.zza(str, false);
            if (bArrZza.length <= 16) {
                throw new zzcx(this);
            }
            ByteBuffer byteBufferAllocate = ByteBuffer.allocate(bArrZza.length);
            byteBufferAllocate.put(bArrZza);
            byteBufferAllocate.flip();
            byte[] bArr2 = new byte[16];
            byte[] bArr3 = new byte[bArrZza.length - 16];
            byteBufferAllocate.get(bArr2);
            byteBufferAllocate.get(bArr3);
            SecretKeySpec secretKeySpec = new SecretKeySpec(bArr, "AES");
            synchronized (zzqw) {
                getCipher().init(2, secretKeySpec, new IvParameterSpec(bArr2));
                bArrDoFinal = getCipher().doFinal(bArr3);
            }
            return bArrDoFinal;
        } catch (IllegalArgumentException e) {
            throw new zzcx(this, e);
        } catch (InvalidAlgorithmParameterException e2) {
            throw new zzcx(this, e2);
        } catch (InvalidKeyException e3) {
            throw new zzcx(this, e3);
        } catch (NoSuchAlgorithmException e4) {
            throw new zzcx(this, e4);
        } catch (BadPaddingException e5) {
            throw new zzcx(this, e5);
        } catch (IllegalBlockSizeException e6) {
            throw new zzcx(this, e6);
        } catch (NoSuchPaddingException e7) {
            throw new zzcx(this, e7);
        }
    }

    public final String zzc(byte[] bArr, byte[] bArr2) throws zzcx {
        byte[] bArrDoFinal;
        byte[] iv;
        if (bArr.length != 16) {
            throw new zzcx(this);
        }
        try {
            SecretKeySpec secretKeySpec = new SecretKeySpec(bArr, "AES");
            synchronized (zzqw) {
                getCipher().init(1, secretKeySpec, (SecureRandom) null);
                bArrDoFinal = getCipher().doFinal(bArr2);
                iv = getCipher().getIV();
            }
            int length = bArrDoFinal.length + iv.length;
            ByteBuffer byteBufferAllocate = ByteBuffer.allocate(length);
            byteBufferAllocate.put(iv).put(bArrDoFinal);
            byteBufferAllocate.flip();
            byte[] bArr3 = new byte[length];
            byteBufferAllocate.get(bArr3);
            return zzbt.zza(bArr3, false);
        } catch (InvalidKeyException e) {
            throw new zzcx(this, e);
        } catch (NoSuchAlgorithmException e2) {
            throw new zzcx(this, e2);
        } catch (BadPaddingException e3) {
            throw new zzcx(this, e3);
        } catch (IllegalBlockSizeException e4) {
            throw new zzcx(this, e4);
        } catch (NoSuchPaddingException e5) {
            throw new zzcx(this, e5);
        }
    }

    public final byte[] zzl(String str) throws zzcx {
        try {
            byte[] bArrZza = zzbt.zza(str, false);
            if (bArrZza.length != 32) {
                throw new zzcx(this);
            }
            byte[] bArr = new byte[16];
            ByteBuffer.wrap(bArrZza, 4, 16).get(bArr);
            for (int i = 0; i < 16; i++) {
                bArr[i] = (byte) (bArr[i] ^ 68);
            }
            return bArr;
        } catch (IllegalArgumentException e) {
            throw new zzcx(this, e);
        }
    }
}
