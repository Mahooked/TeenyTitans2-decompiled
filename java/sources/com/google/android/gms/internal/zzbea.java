package com.google.android.gms.internal;

import android.os.Looper;
import android.support.annotation.NonNull;
import com.google.android.gms.common.internal.zzbo;
import java.util.Collections;
import java.util.Iterator;
import java.util.Set;
import java.util.WeakHashMap;

/* JADX INFO: loaded from: classes.dex */
public final class zzbea {
    private final Set<zzbdw<?>> zzauB = Collections.newSetFromMap(new WeakHashMap());

    public static <L> zzbdy<L> zza(@NonNull L l, @NonNull String str) {
        zzbo.zzb(l, "Listener must not be null");
        zzbo.zzb(str, "Listener type must not be null");
        zzbo.zzh(str, "Listener type must not be empty");
        return new zzbdy<>(l, str);
    }

    public static <L> zzbdw<L> zzb(@NonNull L l, @NonNull Looper looper, @NonNull String str) {
        zzbo.zzb(l, "Listener must not be null");
        zzbo.zzb(looper, "Looper must not be null");
        zzbo.zzb(str, "Listener type must not be null");
        return new zzbdw<>(looper, l, str);
    }

    public final void release() {
        Iterator<zzbdw<?>> it = this.zzauB.iterator();
        while (it.hasNext()) {
            it.next().clear();
        }
        this.zzauB.clear();
    }

    public final <L> zzbdw<L> zza(@NonNull L l, @NonNull Looper looper, @NonNull String str) {
        zzbdw<L> zzbdwVarZzb = zzb(l, looper, str);
        this.zzauB.add(zzbdwVarZzb);
        return zzbdwVarZzb;
    }
}
