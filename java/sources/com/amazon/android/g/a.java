package com.amazon.android.g;

import com.amazon.android.framework.exception.KiwiException;

/* JADX INFO: loaded from: classes.dex */
public class a extends KiwiException {
    private static final long serialVersionUID = 1;

    public a(String str, Throwable th) {
        super("DATA_AUTH_KEY_LOAD_FAILURE", str, th);
    }

    public static a a(Throwable th) {
        return new a("CERT_FAILED_TO_LOAD", th);
    }
}
