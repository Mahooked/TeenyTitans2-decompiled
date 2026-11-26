package com.google.android.gms.internal;

import android.database.ContentObserver;
import android.os.Handler;

/* JADX INFO: loaded from: classes.dex */
final class hj extends ContentObserver {
    hj(Handler handler) {
        super(null);
    }

    @Override // android.database.ContentObserver
    public final void onChange(boolean z) {
        hi.zzbUd.set(true);
    }
}
