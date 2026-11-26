package com.amazon.android.licensing;

import java.util.Date;

/* JADX INFO: loaded from: classes.dex */
public final class f {
    final String a;
    final String b;
    final String c;
    final Date d;
    final String e;

    public f(com.amazon.android.l.d dVar) throws com.amazon.android.g.b {
        this.a = a("checksum", dVar);
        this.b = a("customerId", dVar);
        this.c = a("deviceId", dVar);
        this.e = a("packageName", dVar);
        this.d = b("expiration", dVar);
    }

    private static String a(String str, com.amazon.android.l.d dVar) throws com.amazon.android.g.b {
        String strA = dVar.a(str);
        if (com.amazon.android.framework.util.b.a(strA)) {
            throw new com.amazon.android.g.b("MISSING_FIELD", str);
        }
        return strA;
    }

    private static Date b(String str, com.amazon.android.l.d dVar) throws com.amazon.android.g.b {
        String strA = a(str, dVar);
        try {
            return new Date(Long.parseLong(strA));
        } catch (NumberFormatException e) {
            throw new com.amazon.android.g.b("INVALID_FIELD_VALUE", str + ":" + strA);
        }
    }
}
