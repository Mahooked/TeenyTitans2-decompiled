package com.amazon.mas.kiwi.util;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.security.CodeSigner;
import java.security.cert.Certificate;
import java.security.cert.X509Certificate;
import java.util.Enumeration;
import java.util.List;
import java.util.jar.JarEntry;
import java.util.jar.JarFile;

/* JADX INFO: loaded from: classes.dex */
public class ApkHelpers {
    private static final String CONTENT_PREFIX = "com.amazon.content.id.";

    public static String getContentID(JarFile apkSrc) {
        String contentID;
        if (apkSrc == null) {
            throw new IllegalArgumentException("apkSrc must not be null!");
        }
        Enumeration<JarEntry> entries = apkSrc.entries();
        while (entries.hasMoreElements()) {
            JarEntry ent = entries.nextElement();
            if (!ent.isDirectory() && (contentID = getContentIDFromName(ent.getName())) != null && contentID.length() != 0) {
                return contentID;
            }
        }
        return null;
    }

    public static String getContentIDFromName(String name) {
        if (name == null || name.length() == 0) {
            throw new IllegalArgumentException("name cannot be null or empty!");
        }
        int contentIdIndex = name.indexOf(CONTENT_PREFIX);
        if (contentIdIndex >= 0 && name.length() > CONTENT_PREFIX.length()) {
            return name.substring(CONTENT_PREFIX.length() + contentIdIndex);
        }
        return null;
    }

    public static boolean isSigned(JarFile jarFile) throws IOException {
        if (jarFile == null) {
            throw new IllegalArgumentException("apkSrc must not be null!");
        }
        try {
            scanJar(jarFile);
            CodeSigner[] codeSigners = getCodeSigners(jarFile);
            return codeSigners != null;
        } catch (SecurityException e) {
            return true;
        }
    }

    public static boolean isSigned(File file) throws Throwable {
        JarFile jarFile = null;
        try {
            JarFile jarFile2 = new JarFile(file);
            try {
                boolean zIsSigned = isSigned(jarFile2);
                jarFile2.close();
                return zIsSigned;
            } catch (Throwable th) {
                th = th;
                jarFile = jarFile2;
                jarFile.close();
                throw th;
            }
        } catch (Throwable th2) {
            th = th2;
        }
    }

    public static byte[] getApkSignature(String apkFileName) throws ApkInvalidException, IOException {
        if (apkFileName == null || apkFileName.length() == 0) {
            throw new IllegalArgumentException("apkFileName cannot be null or empty!");
        }
        JarFile jarFile = new JarFile(apkFileName);
        try {
            scanJar(jarFile);
            Certificate cert = getFirstSigningCert(jarFile);
            if (cert == null) {
                return null;
            }
            byte[] certBytes = null;
            if (cert instanceof X509Certificate) {
                X509Certificate xcert = (X509Certificate) cert;
                certBytes = xcert.getSignature();
            }
            return certBytes;
        } catch (SecurityException e) {
            return null;
        }
    }

    private static Certificate getFirstSigningCert(JarFile jarFile) {
        CodeSigner[] codeSigners = getCodeSigners(jarFile);
        if (codeSigners == null || codeSigners.length <= 0) {
            return null;
        }
        List<? extends Certificate> certs = codeSigners[0].getSignerCertPath().getCertificates();
        if (certs.isEmpty()) {
            return null;
        }
        Certificate cert = certs.get(0);
        return cert;
    }

    private static CodeSigner[] getCodeSigners(JarFile jarFile) {
        CodeSigner[] signers = null;
        Enumeration<JarEntry> entries = jarFile.entries();
        while (entries.hasMoreElements()) {
            JarEntry je = entries.nextElement();
            signers = je.getCodeSigners();
            if (signers != null) {
                break;
            }
        }
        return signers;
    }

    private static void scanJar(JarFile jarFile) throws IOException {
        Enumeration<JarEntry> entries = jarFile.entries();
        byte[] buffer = new byte[8192];
        while (entries.hasMoreElements()) {
            JarEntry je = entries.nextElement();
            InputStream is = null;
            try {
                try {
                    is = jarFile.getInputStream(je);
                    do {
                    } while (is.read(buffer, 0, buffer.length) != -1);
                    if (is != null) {
                        is.close();
                    }
                } catch (Exception e) {
                    throw new ApkInvalidException(e);
                }
            } catch (Throwable th) {
                if (is != null) {
                    is.close();
                }
                throw th;
            }
        }
    }
}
