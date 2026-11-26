package com.amazon.android.framework.prompt;

import android.content.DialogInterface;
import android.view.KeyEvent;

/* JADX INFO: loaded from: classes.dex */
final class d implements DialogInterface.OnKeyListener {
    private /* synthetic */ Prompt a;

    d(Prompt prompt) {
        this.a = prompt;
    }

    @Override // android.content.DialogInterface.OnKeyListener
    public final boolean onKey(DialogInterface dialogInterface, int i, KeyEvent keyEvent) {
        return i == 84;
    }
}
