package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
@zzzn
final class zzzt extends Exception {
    private final int mErrorCode;

    public zzzt(String str, int i) {
        super(str);
        this.mErrorCode = i;
    }

    public final int getErrorCode() {
        return this.mErrorCode;
    }
}
