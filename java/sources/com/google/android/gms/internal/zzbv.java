package com.google.android.gms.internal;

import android.support.v4.media.session.PlaybackStateCompat;
import java.io.UnsupportedEncodingException;
import java.nio.ByteBuffer;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;
import java.util.Arrays;
import java.util.Iterator;
import java.util.Vector;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
final class zzbv {
    private static boolean zzlP = false;
    private static MessageDigest zzlQ = null;
    private static final Object zzlR = new Object();
    private static final Object zzlS = new Object();
    static CountDownLatch zzlT = new CountDownLatch(1);

    static String zza(zzax zzaxVar, String str, boolean z) throws NoSuchAlgorithmException, UnsupportedEncodingException {
        byte[] bArrZza;
        byte[] bArrZzc = adp.zzc(zzaxVar);
        if (z) {
            bArrZza = zza(bArrZzc, str, true);
        } else {
            Vector<byte[]> vectorZza = zza(bArrZzc, 255);
            if (vectorZza == null || vectorZza.size() == 0) {
                bArrZza = zza(adp.zzc(zzb(PlaybackStateCompat.ACTION_SKIP_TO_QUEUE_ITEM)), str, true);
            } else {
                zzbe zzbeVar = new zzbe();
                zzbeVar.zzcJ = new byte[vectorZza.size()][];
                Iterator<byte[]> it = vectorZza.iterator();
                int i = 0;
                while (it.hasNext()) {
                    zzbeVar.zzcJ[i] = zza(it.next(), str, false);
                    i++;
                }
                zzbeVar.zzcE = zzb(bArrZzc);
                bArrZza = adp.zzc(zzbeVar);
            }
        }
        return zzbt.zza(bArrZza, true);
    }

    static String zza(String str, String str2, boolean z) {
        byte[] bArrZza = zza(str, str2, true, ((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEO)).booleanValue());
        return bArrZza != null ? zzbt.zza(bArrZza, true) : Integer.toString(7);
    }

    private static Vector<byte[]> zza(byte[] bArr, int i) {
        if (bArr == null || bArr.length <= 0) {
            return null;
        }
        int length = ((bArr.length + 255) - 1) / 255;
        Vector<byte[]> vector = new Vector<>();
        for (int i2 = 0; i2 < length; i2++) {
            int i3 = i2 * 255;
            try {
                vector.add(Arrays.copyOfRange(bArr, i3, bArr.length - i3 > 255 ? i3 + 255 : bArr.length));
            } catch (IndexOutOfBoundsException e) {
                return null;
            }
        }
        return vector;
    }

    private static byte[] zza(String str, String str2, boolean z, boolean z2) {
        zzbb zzbbVar = new zzbb();
        try {
            zzbbVar.zzcC = str.length() < 3 ? str.getBytes("ISO-8859-1") : zzbt.zza(str, true);
            zzbbVar.zzcD = str2.length() < 3 ? str2.getBytes("ISO-8859-1") : zzbt.zza(str2, true);
            return adp.zzc(zzbbVar);
        } catch (UnsupportedEncodingException | NoSuchAlgorithmException e) {
            return null;
        }
    }

    private static byte[] zza(byte[] bArr, String str, boolean z) throws NoSuchAlgorithmException, UnsupportedEncodingException {
        byte[] bArrArray;
        int i = z ? 239 : 255;
        if (bArr.length > i) {
            bArr = adp.zzc(zzb(PlaybackStateCompat.ACTION_SKIP_TO_QUEUE_ITEM));
        }
        if (bArr.length < i) {
            byte[] bArr2 = new byte[i - bArr.length];
            new SecureRandom().nextBytes(bArr2);
            bArrArray = ByteBuffer.allocate(i + 1).put((byte) bArr.length).put(bArr).put(bArr2).array();
        } else {
            bArrArray = ByteBuffer.allocate(i + 1).put((byte) bArr.length).put(bArr).array();
        }
        if (z) {
            bArrArray = ByteBuffer.allocate(256).put(zzb(bArrArray)).put(bArrArray).array();
        }
        byte[] bArr3 = new byte[256];
        new zzby().zza(bArrArray, bArr3);
        if (str != null && str.length() > 0) {
            if (str.length() > 32) {
                str = str.substring(0, 32);
            }
            new acg(str.getBytes(HTTP.UTF_8)).zzG(bArr3);
        }
        return bArr3;
    }

    private static zzax zzb(long j) {
        zzax zzaxVar = new zzax();
        zzaxVar.zzbq = Long.valueOf(PlaybackStateCompat.ACTION_SKIP_TO_QUEUE_ITEM);
        return zzaxVar;
    }

    public static byte[] zzb(byte[] bArr) throws NoSuchAlgorithmException {
        byte[] bArrDigest;
        synchronized (zzlR) {
            MessageDigest messageDigestZzx = zzx();
            if (messageDigestZzx == null) {
                throw new NoSuchAlgorithmException("Cannot compute hash");
            }
            messageDigestZzx.reset();
            messageDigestZzx.update(bArr);
            bArrDigest = zzlQ.digest();
        }
        return bArrDigest;
    }

    static void zzw() {
        synchronized (zzlS) {
            if (!zzlP) {
                zzlP = true;
                new Thread(new zzbx()).start();
            }
        }
    }

    private static MessageDigest zzx() {
        zzw();
        boolean zAwait = false;
        try {
            zAwait = zzlT.await(2L, TimeUnit.SECONDS);
        } catch (InterruptedException e) {
        }
        if (zAwait && zzlQ != null) {
            return zzlQ;
        }
        return null;
    }
}
