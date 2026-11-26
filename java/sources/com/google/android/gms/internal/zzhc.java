package com.google.android.gms.internal;

import android.webkit.ValueCallback;

/* JADX INFO: loaded from: classes.dex */
final class zzhc implements ValueCallback<String> {
    private /* synthetic */ zzhb zzyO;

    zzhc(zzhb zzhbVar) {
        this.zzyO = zzhbVar;
    }

    @Override // android.webkit.ValueCallback
    public final /* synthetic */ void onReceiveValue(String str) {
        this.zzyO.zzyJ.zza(this.zzyO.zzyL, this.zzyO.zzyM, str, this.zzyO.zzyN);
    }
}
