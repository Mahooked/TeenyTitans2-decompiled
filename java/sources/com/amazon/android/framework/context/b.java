package com.amazon.android.framework.context;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

/* JADX INFO: loaded from: classes.dex */
final class b extends BroadcastReceiver {
    private /* synthetic */ d a;

    b(d dVar) {
        this.a = dVar;
    }

    @Override // android.content.BroadcastReceiver
    public final void onReceive(Context context, Intent intent) {
        d.a(this.a, intent);
    }
}
