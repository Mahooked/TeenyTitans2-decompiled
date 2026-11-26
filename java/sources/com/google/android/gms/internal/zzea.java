package com.google.android.gms.internal;

import java.io.UnsupportedEncodingException;
import java.lang.reflect.Method;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
public class zzea {
    private static String TAG = zzea.class.getSimpleName();
    private final String className;
    private final zzdb zzpJ;
    private final String zzrv;
    private final Class<?>[] zzry;
    private final int zzrw = 2;
    private volatile Method zzrx = null;
    private CountDownLatch zzrz = new CountDownLatch(1);

    public zzea(zzdb zzdbVar, String str, String str2, Class<?>... clsArr) {
        this.zzpJ = zzdbVar;
        this.className = str;
        this.zzrv = str2;
        this.zzry = clsArr;
        this.zzpJ.zzC().submit(new zzeb(this));
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzX() {
        try {
            Class clsLoadClass = this.zzpJ.zzD().loadClass(zzb(this.zzpJ.zzF(), this.className));
            if (clsLoadClass == null) {
                this.zzrz.countDown();
                return;
            }
            this.zzrx = clsLoadClass.getMethod(zzb(this.zzpJ.zzF(), this.zzrv), this.zzry);
            if (this.zzrx == null) {
                this.zzrz.countDown();
            } else {
                this.zzrz.countDown();
            }
        } catch (zzcx e) {
            this.zzrz.countDown();
        } catch (UnsupportedEncodingException e2) {
            this.zzrz.countDown();
        } catch (ClassNotFoundException e3) {
            this.zzrz.countDown();
        } catch (NoSuchMethodException e4) {
            this.zzrz.countDown();
        } catch (NullPointerException e5) {
            this.zzrz.countDown();
        } catch (Throwable th) {
            this.zzrz.countDown();
            throw th;
        }
    }

    private final String zzb(byte[] bArr, String str) throws zzcx, UnsupportedEncodingException {
        return new String(this.zzpJ.zzE().zza(bArr, str), HTTP.UTF_8);
    }

    public final Method zzY() {
        if (this.zzrx != null) {
            return this.zzrx;
        }
        try {
            if (this.zzrz.await(2L, TimeUnit.SECONDS)) {
                return this.zzrx;
            }
            return null;
        } catch (InterruptedException e) {
            return null;
        }
    }
}
