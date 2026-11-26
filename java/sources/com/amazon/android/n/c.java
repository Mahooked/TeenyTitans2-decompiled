package com.amazon.android.n;

import com.amazon.android.framework.util.KiwiLogger;
import java.util.Date;

/* JADX INFO: loaded from: classes.dex */
final class c extends b {
    private /* synthetic */ d b;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    c(d dVar, Object obj, Date date) {
        super(obj, date);
        this.b = dVar;
    }

    @Override // com.amazon.android.h.b
    protected final void doExpiration() {
        if (KiwiLogger.ERROR_ON) {
            d.a.error("Woah, non-expirable value was expired!!!!");
        }
    }
}
