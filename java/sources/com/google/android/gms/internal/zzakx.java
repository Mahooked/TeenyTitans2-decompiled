package com.google.android.gms.internal;

import android.content.DialogInterface;
import android.webkit.JsResult;

/* JADX INFO: loaded from: classes.dex */
final class zzakx implements DialogInterface.OnCancelListener {
    private /* synthetic */ JsResult zzacH;

    zzakx(JsResult jsResult) {
        this.zzacH = jsResult;
    }

    @Override // android.content.DialogInterface.OnCancelListener
    public final void onCancel(DialogInterface dialogInterface) {
        this.zzacH.cancel();
    }
}
