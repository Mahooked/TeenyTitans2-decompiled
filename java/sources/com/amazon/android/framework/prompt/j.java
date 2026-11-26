package com.amazon.android.framework.prompt;

import android.content.DialogInterface;

/* JADX INFO: loaded from: classes.dex */
final class j implements DialogInterface.OnClickListener {
    private /* synthetic */ int a;
    private /* synthetic */ SimplePrompt b;

    j(SimplePrompt simplePrompt, int i) {
        this.b = simplePrompt;
        this.a = i;
    }

    @Override // android.content.DialogInterface.OnClickListener
    public final void onClick(DialogInterface dialogInterface, int i) {
        if (this.b.dismiss()) {
            this.b.doAction(this.a);
        }
    }
}
