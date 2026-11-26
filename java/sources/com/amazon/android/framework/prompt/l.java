package com.amazon.android.framework.prompt;

import android.content.DialogInterface;

/* JADX INFO: loaded from: classes.dex */
final class l implements DialogInterface.OnClickListener {
    private /* synthetic */ SimplePrompt a;

    l(SimplePrompt simplePrompt) {
        this.a = simplePrompt;
    }

    @Override // android.content.DialogInterface.OnClickListener
    public final void onClick(DialogInterface dialogInterface, int i) {
        if (this.a.dismiss()) {
            this.a.doAction();
        }
    }
}
