package com.amazon.android.framework.task.command;

import android.app.Application;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.pm.Signature;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.util.KiwiLogger;
import com.amazon.mas.kiwi.util.Base64;
import java.io.ByteArrayInputStream;
import java.security.cert.CertificateException;
import java.security.cert.CertificateFactory;
import java.security.cert.X509Certificate;
import java.util.Iterator;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public final class f {
    private static final KiwiLogger a = new KiwiLogger("CommandResultVerifier");

    @Resource
    private Application b;

    @Resource
    private com.amazon.android.l.c c;

    private PackageInfo a(String str) throws com.amazon.android.b.g {
        try {
            return this.b.getPackageManager().getPackageInfo(str, 64);
        } catch (PackageManager.NameNotFoundException e) {
            a.trace("getPackageInfo() caught exception" + e);
            throw new com.amazon.android.b.g();
        }
    }

    private static String a(Signature signature) throws CertificateException {
        return Base64.encodeBytes(((X509Certificate) CertificateFactory.getInstance("X509").generateCertificate(new ByteArrayInputStream(signature.toByteArray()))).getSignature());
    }

    private boolean a(String str, Signature signature) throws com.amazon.android.g.a {
        try {
            return com.amazon.android.l.a.a(a(signature), str, this.c.a());
        } catch (CertificateException e) {
            if (KiwiLogger.ERROR_ON) {
                a.error("Failed to extract fingerprint from signature: " + signature);
            }
            return false;
        }
    }

    private static boolean b(String str, Signature signature) {
        boolean zEquals = false;
        try {
            zEquals = str.equals(a(signature));
            a.trace("Signature valid: " + zEquals);
            return zEquals;
        } catch (CertificateException e) {
            a.error("Failed to extract fingerprint from signature");
            return zEquals;
        }
    }

    public final void a(String str, String str2) throws KiwiException {
        if (KiwiLogger.TRACE_ON) {
            a.trace("Verifying auth token: " + str);
        }
        for (Signature signature : a(str2).signatures) {
            if (a(str, signature)) {
                return;
            }
        }
        throw new com.amazon.android.b.d();
    }

    public final boolean a(String str, List list) {
        a.trace("checkSignatures(" + str + ", " + list);
        try {
            for (Signature signature : a(str).signatures) {
                Iterator it = list.iterator();
                while (it.hasNext()) {
                    if (b((String) it.next(), signature)) {
                        return true;
                    }
                }
            }
        } catch (com.amazon.android.b.g e) {
            a.error("isPackageSignatureValid: caught exception while checking", e);
        }
        return false;
    }
}
