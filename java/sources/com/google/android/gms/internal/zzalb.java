package com.google.android.gms.internal;

import android.content.DialogInterface;
import android.webkit.JsPromptResult;

/* JADX INFO: loaded from: classes.dex */
final class zzalb implements DialogInterface.OnClickListener {
    private /* synthetic */ JsPromptResult zzacI;

    zzalb(JsPromptResult jsPromptResult) {
        this.zzacI = jsPromptResult;
    }

    @Override // android.content.DialogInterface.OnClickListener
    public final void onClick(DialogInterface dialogInterface, int i) {
        this.zzacI.cancel();
    }
}
