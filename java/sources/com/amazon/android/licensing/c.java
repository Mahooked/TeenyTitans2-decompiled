package com.amazon.android.licensing;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public final class c {
    final String a;
    final String b;
    final String c;

    public c(Map map) throws com.amazon.android.b.h {
        if (map == null) {
            throw new com.amazon.android.b.h("EMPTY", null);
        }
        this.a = a("license", map);
        this.b = a("customerId", map);
        this.c = a("deviceId", map);
    }

    private static String a(String str, Map map) throws com.amazon.android.b.h {
        String str2 = (String) map.get(str);
        if (str2 == null || str2.length() == 0) {
            throw new com.amazon.android.b.h("MISSING_FIELD", str);
        }
        return str2;
    }
}
