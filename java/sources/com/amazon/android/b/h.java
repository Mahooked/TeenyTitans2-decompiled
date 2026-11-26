package com.amazon.android.b;

import com.amazon.android.framework.exception.KiwiException;

/* JADX INFO: loaded from: classes.dex */
public class h extends KiwiException {
    private static final long serialVersionUID = 1;

    public h(String str, String str2) {
        super("MALFORMED_RESPONSE", str, str2);
    }
}
