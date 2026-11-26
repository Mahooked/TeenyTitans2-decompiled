package com.amazon.android.licensing;

/* JADX INFO: loaded from: classes.dex */
public enum k implements com.amazon.android.i.c {
    EXPIRATION,
    CUSTOMER_ID,
    DEVICE_ID,
    PACKAGE_NAME,
    CHECKSUM;

    @Override // com.amazon.android.i.c
    public final String a() {
        return name();
    }
}
