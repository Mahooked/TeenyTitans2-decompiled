package com.google.android.gms.internal;

import android.os.Handler;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
final class zzsj {
    private final List<zzth> zztH = new LinkedList();

    zzsj() {
    }

    final void zza(zzti zztiVar) {
        Handler handler = zzagz.zzZr;
        Iterator<zzth> it = this.zztH.iterator();
        while (it.hasNext()) {
            handler.post(new zztg(this, it.next(), zztiVar));
        }
        this.zztH.clear();
    }
}
