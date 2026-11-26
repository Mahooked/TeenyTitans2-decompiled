package com.amazon.android.licensing;

import com.amazon.android.framework.exception.KiwiException;
import com.amazon.android.framework.prompt.PromptContent;

/* JADX INFO: loaded from: classes.dex */
final class g implements h {
    private /* synthetic */ PromptContent a;
    private /* synthetic */ LicenseFailurePromptContentMapper b;

    g(LicenseFailurePromptContentMapper licenseFailurePromptContentMapper, PromptContent promptContent) {
        this.b = licenseFailurePromptContentMapper;
        this.a = promptContent;
    }

    @Override // com.amazon.android.licensing.h
    public final PromptContent a(KiwiException kiwiException) {
        return this.a;
    }
}
