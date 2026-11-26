package com.amazon.android.framework.task.command;

import android.content.DialogInterface;
import com.amazon.android.framework.util.KiwiLogger;

/* JADX INFO: loaded from: classes.dex */
final class e implements DialogInterface.OnClickListener {
    private /* synthetic */ n a;
    private /* synthetic */ d b;

    e(d dVar, n nVar) {
        this.b = dVar;
        this.a = nVar;
    }

    @Override // android.content.DialogInterface.OnClickListener
    public final void onClick(DialogInterface dialogInterface, int i) {
        if (KiwiLogger.TRACE_ON) {
            d.a.trace("Choice selected!");
        }
        if (this.b.dismiss()) {
            this.b.d.add(this.a);
        }
    }
}
