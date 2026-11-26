package com.amazon.android.l;

import com.amazon.android.framework.util.KiwiLogger;
import com.amazon.mas.kiwi.util.Base64;
import java.io.IOException;
import java.security.PublicKey;
import java.util.HashMap;
import java.util.Map;
import java.util.StringTokenizer;

/* JADX INFO: loaded from: classes.dex */
public final class d {
    private static final KiwiLogger a = new KiwiLogger("SignedToken");
    private final Map b = new HashMap();

    public d(String str, PublicKey publicKey) throws com.amazon.android.g.c, com.amazon.android.g.b {
        String strB = b(str);
        int iLastIndexOf = strB.lastIndexOf("|");
        if (iLastIndexOf == -1) {
            throw com.amazon.android.g.b.a();
        }
        String strSubstring = strB.substring(0, iLastIndexOf);
        String strSubstring2 = strB.substring(iLastIndexOf + 1);
        if (KiwiLogger.TRACE_ON) {
            a.trace("Token data: " + strSubstring);
            a.trace("Signature: " + strSubstring2);
        }
        if (!a.a(strSubstring, strSubstring2, publicKey)) {
            throw new com.amazon.android.g.c();
        }
        c(strSubstring);
    }

    private static String b(String str) throws com.amazon.android.g.b {
        try {
            return new String(Base64.decode(str.getBytes()));
        } catch (IOException e) {
            throw new com.amazon.android.g.b("DECODE", e.getMessage());
        }
    }

    private void c(String str) throws com.amazon.android.g.b {
        StringTokenizer stringTokenizer = new StringTokenizer(str, ",");
        while (stringTokenizer.hasMoreElements()) {
            String strNextToken = stringTokenizer.nextToken();
            a.trace("Field: " + strNextToken);
            int iIndexOf = strNextToken.indexOf("=");
            if (iIndexOf == -1) {
                throw com.amazon.android.g.b.a();
            }
            String strSubstring = strNextToken.substring(0, iIndexOf);
            String strSubstring2 = strNextToken.substring(iIndexOf + 1);
            a.trace("FieldName: " + strSubstring);
            a.trace("FieldValue: " + strSubstring2);
            this.b.put(strSubstring, strSubstring2);
        }
    }

    public final String a(String str) {
        com.amazon.android.d.a.a(str, "key");
        return (String) this.b.get(str);
    }

    public final String toString() {
        return "Signed Token: " + this.b;
    }
}
