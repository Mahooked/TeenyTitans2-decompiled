package com.google.android.gms.common.api;

import android.support.annotation.NonNull;
import com.google.android.gms.common.api.Result;

/* JADX INFO: loaded from: classes.dex */
public class Response<T extends Result> {
    private T zzaBj;

    public Response() {
    }

    protected Response(@NonNull T t) {
        this.zzaBj = t;
    }

    @NonNull
    protected T getResult() {
        return this.zzaBj;
    }

    public void setResult(@NonNull T t) {
        this.zzaBj = t;
    }
}
