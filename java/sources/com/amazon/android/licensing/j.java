package com.amazon.android.licensing;

import com.amazon.android.framework.exception.KiwiException;
import com.amazon.android.framework.prompt.PromptContent;

/* JADX INFO: loaded from: classes.dex */
final class j implements h {
    /* synthetic */ j() {
        this((byte) 0);
    }

    private j(byte b) {
    }

    @Override // com.amazon.android.licensing.h
    public final /* bridge */ /* synthetic */ PromptContent a(KiwiException kiwiException) {
        return ((com.amazon.android.r.a) kiwiException).a.a.containsKey(k.EXPIRATION) ? i.d : i.e;
    }
}
