package com.amazon.android.licensing;

import com.amazon.android.framework.exception.KiwiException;
import com.amazon.android.framework.prompt.PromptContent;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class m implements h {
    private Map a;

    /* synthetic */ m() {
        this((byte) 0);
    }

    private m(byte b) {
        this.a = new a(this);
    }

    @Override // com.amazon.android.licensing.h
    public final /* bridge */ /* synthetic */ PromptContent a(KiwiException kiwiException) {
        PromptContent promptContent = (PromptContent) this.a.get(((com.amazon.android.b.e) kiwiException).getReason());
        return promptContent != null ? promptContent : i.e;
    }
}
