package com.amazon.android.b;

import com.amazon.android.framework.exception.KiwiException;

/* JADX INFO: loaded from: classes.dex */
public class b extends KiwiException {
    private static final long serialVersionUID = 1;

    public b(Throwable th) {
        super("UNHANDLED_EXCEPTION", th);
    }
}
