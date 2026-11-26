package com.amazon.android.g;

import com.amazon.android.framework.exception.KiwiException;

/* JADX INFO: loaded from: classes.dex */
public class b extends KiwiException {
    private static final long serialVersionUID = 1;

    public b(String str, String str2) {
        super("SIGNED_TOKEN_PARSE_FAILURE", str, str2);
    }

    public static b a() {
        return new b("INVALID_FORMAT", null);
    }
}
