package com.google.android.gms.internal;

import android.content.DialogInterface;
import android.webkit.JsPromptResult;

/* JADX INFO: loaded from: classes.dex */
final class zzala implements DialogInterface.OnCancelListener {
    private /* synthetic */ JsPromptResult zzacI;

    zzala(JsPromptResult jsPromptResult) {
        this.zzacI = jsPromptResult;
    }

    @Override // android.content.DialogInterface.OnCancelListener
    public final void onCancel(DialogInterface dialogInterface) {
        this.zzacI.cancel();
    }
}
