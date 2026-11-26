package com.amazon.android.l;

import android.app.Application;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.util.KiwiLogger;
import java.io.IOException;
import java.security.GeneralSecurityException;
import java.security.PublicKey;
import java.security.cert.CertPath;
import java.security.cert.Certificate;
import java.security.cert.CertificateFactory;
import java.security.cert.X509Certificate;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.jar.JarEntry;
import java.util.jar.JarFile;

/* JADX INFO: loaded from: classes.dex */
public final class c {
    private static final KiwiLogger a = new KiwiLogger("DataAuthenticationKeyLoader");

    @Resource
    private Application b;

    @Resource
    private com.amazon.android.n.a c;

    private static CertPath a(JarFile jarFile, JarEntry jarEntry) throws com.amazon.android.g.a {
        try {
            if (KiwiLogger.TRACE_ON) {
                a.trace("Extracting cert from entry: " + jarEntry.getName());
            }
            CertificateFactory certificateFactory = CertificateFactory.getInstance("X.509");
            if (KiwiLogger.TRACE_ON) {
                a.trace("Generating certificates from entry input stream");
            }
            return certificateFactory.generateCertPath(new ArrayList(certificateFactory.generateCertificates(jarFile.getInputStream(jarEntry))));
        } catch (Exception e) {
            throw com.amazon.android.g.a.a(e);
        }
    }

    private static JarEntry a(JarFile jarFile) throws com.amazon.android.g.a {
        if (KiwiLogger.TRACE_ON) {
            a.trace("Searching for cert in apk");
        }
        Enumeration<JarEntry> enumerationEntries = jarFile.entries();
        while (enumerationEntries.hasMoreElements()) {
            JarEntry jarEntryNextElement = enumerationEntries.nextElement();
            if (!jarEntryNextElement.isDirectory() && jarEntryNextElement.getName().equals("kiwi")) {
                return jarEntryNextElement;
            }
        }
        throw new com.amazon.android.g.a("CERT_NOT_FOUND", null);
    }

    private JarFile b() throws com.amazon.android.g.a {
        String packageCodePath = this.b.getPackageCodePath();
        if (KiwiLogger.TRACE_ON) {
            a.trace("Opening apk: " + packageCodePath);
        }
        try {
            return new JarFile(packageCodePath, false);
        } catch (IOException e) {
            throw com.amazon.android.g.a.a(e);
        }
    }

    private static b c() throws com.amazon.android.g.a {
        try {
            return new b();
        } catch (GeneralSecurityException e) {
            throw new com.amazon.android.g.a("FAILED_TO_ESTABLISH_TRUST", e);
        }
    }

    /* JADX WARN: Code duplicated, block: B:42:0x00cd  */
    public final PublicKey a() throws com.amazon.android.g.a {
        boolean z;
        if (KiwiLogger.TRACE_ON) {
            a.trace("Loading data authentication key...");
        }
        if (KiwiLogger.TRACE_ON) {
            a.trace("Checking KiwiDataStore for key...");
        }
        PublicKey publicKey = (PublicKey) this.c.a("DATA_AUTHENTICATION_KEY");
        if (KiwiLogger.TRACE_ON) {
            a.trace("Key was cached: " + (publicKey != null));
        }
        if (publicKey == null) {
            if (KiwiLogger.TRACE_ON) {
                a.trace("Loading authentication key from apk...");
            }
            JarFile jarFileB = b();
            CertPath certPathA = a(jarFileB, a(jarFileB));
            if (certPathA == null || certPathA.getCertificates().size() <= 0) {
                z = false;
            } else {
                Certificate certificate = certPathA.getCertificates().get(0);
                if (certificate instanceof X509Certificate) {
                    String name = ((X509Certificate) certificate).getSubjectX500Principal().getName();
                    if (KiwiLogger.TRACE_ON) {
                        a.trace("Kiwi Cert Details: " + name);
                    }
                    z = name.contains("O=Amazon.com\\, Inc.") && name.contains("C=US") && name.contains("ST=Washington") && name.contains("L=Seattle");
                } else {
                    z = false;
                }
            }
            if (!z) {
                throw new com.amazon.android.g.a("CERT_INVALID", null);
            }
            if (!c().a(certPathA)) {
                throw new com.amazon.android.g.a("VERIFICATION_FAILED", null);
            }
            publicKey = ((X509Certificate) certPathA.getCertificates().get(0)).getPublicKey();
            if (KiwiLogger.TRACE_ON) {
                a.trace("Placing auth key into storage");
            }
            this.c.a("DATA_AUTHENTICATION_KEY", publicKey);
        }
        return publicKey;
    }
}
