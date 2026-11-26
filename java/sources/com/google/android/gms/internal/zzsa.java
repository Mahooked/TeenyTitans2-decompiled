package com.google.android.gms.internal;

import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzsa implements Iterable<zzry> {
    private final List<zzry> zzJO = new LinkedList();

    public static boolean zze(zzaka zzakaVar) {
        zzry zzryVarZzg = zzg(zzakaVar);
        if (zzryVarZzg == null) {
            return false;
        }
        zzryVarZzg.zzJL.abort();
        return true;
    }

    public static boolean zzf(zzaka zzakaVar) {
        return zzg(zzakaVar) != null;
    }

    private static zzry zzg(zzaka zzakaVar) {
        for (zzry zzryVar : com.google.android.gms.ads.internal.zzbs.zzbW()) {
            if (zzryVar.zzJH == zzakaVar) {
                return zzryVar;
            }
        }
        return null;
    }

    @Override // java.lang.Iterable
    public final Iterator<zzry> iterator() {
        return this.zzJO.iterator();
    }

    public final void zza(zzry zzryVar) {
        this.zzJO.add(zzryVar);
    }

    public final void zzb(zzry zzryVar) {
        this.zzJO.remove(zzryVar);
    }

    public final int zzeE() {
        return this.zzJO.size();
    }
}
