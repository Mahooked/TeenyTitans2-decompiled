package com.google.android.gms.internal;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Future;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicInteger;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaji {
    public static <A, B> zzajm<B> zza(zzajm<A> zzajmVar, zzajl<A, B> zzajlVar) {
        zzajg zzajgVar = new zzajg();
        zzajmVar.zzc(new zzajj(zzajgVar, zzajlVar, zzajmVar));
        return zzajgVar;
    }

    public static <T> T zza(Future<T> future, T t) {
        try {
            return future.get(((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEJ)).longValue(), TimeUnit.MILLISECONDS);
        } catch (InterruptedException e) {
            future.cancel(true);
            zzafr.zzc("InterruptedException caught while resolving future.", e);
            Thread.currentThread().interrupt();
            com.google.android.gms.ads.internal.zzbs.zzbD().zza(e, "Futures.resolveFuture");
            return t;
        } catch (Exception e2) {
            future.cancel(true);
            zzafr.zzb("Error waiting for future.", e2);
            com.google.android.gms.ads.internal.zzbs.zzbD().zza(e2, "Futures.resolveFuture");
            return t;
        }
    }

    public static <T> T zza(Future<T> future, T t, long j, TimeUnit timeUnit) {
        try {
            return future.get(j, timeUnit);
        } catch (InterruptedException e) {
            future.cancel(true);
            zzafr.zzc("InterruptedException caught while resolving future.", e);
            Thread.currentThread().interrupt();
            com.google.android.gms.ads.internal.zzbs.zzbD().zza(e, "Futures.resolveFuture");
            return t;
        } catch (Exception e2) {
            future.cancel(true);
            zzafr.zzb("Error waiting for future.", e2);
            com.google.android.gms.ads.internal.zzbs.zzbD().zza(e2, "Futures.resolveFuture");
            return t;
        }
    }

    public static <V> zzajm<List<V>> zzp(List<zzajm<V>> list) {
        zzajg zzajgVar = new zzajg();
        int size = list.size();
        AtomicInteger atomicInteger = new AtomicInteger(0);
        Iterator<zzajm<V>> it = list.iterator();
        while (it.hasNext()) {
            it.next().zzc(new zzajk(atomicInteger, size, zzajgVar, list));
        }
        return zzajgVar;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static <V> List<V> zzq(List<zzajm<V>> list) throws ExecutionException, InterruptedException {
        ArrayList arrayList = new ArrayList();
        Iterator<zzajm<V>> it = list.iterator();
        while (it.hasNext()) {
            V v = it.next().get();
            if (v != null) {
                arrayList.add(v);
            }
        }
        return arrayList;
    }
}
