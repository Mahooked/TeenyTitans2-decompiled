package com.google.android.gms.internal;

import android.content.Context;
import java.util.WeakHashMap;
import java.util.concurrent.Future;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzacd {
    private WeakHashMap<Context, zzacf> zzWe = new WeakHashMap<>();

    public final Future<zzacb> zzn(Context context) {
        return zzagt.zza(new zzace(this, context));
    }
}
