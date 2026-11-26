package com.google.android.gms.internal;

import android.database.ContentObserver;
import android.os.Handler;

/* JADX INFO: loaded from: classes.dex */
final class zzfl extends ContentObserver {
    private /* synthetic */ zzfi zzxh;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    public zzfl(zzfi zzfiVar, Handler handler) {
        super(handler);
        this.zzxh = zzfiVar;
    }

    @Override // android.database.ContentObserver
    public final void onChange(boolean z) {
        super.onChange(z);
        this.zzxh.zzcp();
    }
}
