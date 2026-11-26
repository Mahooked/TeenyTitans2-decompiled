package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.zzbo;
import java.util.Iterator;
import java.util.LinkedList;

/* JADX INFO: loaded from: classes.dex */
@zzzn
final class zztn {
    private final LinkedList<zzto> zzKr;
    private zzir zzKs;
    private final int zzKt;
    private boolean zzKu;
    private final String zztV;

    zztn(zzir zzirVar, String str, int i) {
        zzbo.zzu(zzirVar);
        zzbo.zzu(str);
        this.zzKr = new LinkedList<>();
        this.zzKs = zzirVar;
        this.zztV = str;
        this.zzKt = i;
    }

    final String getAdUnitId() {
        return this.zztV;
    }

    final int getNetworkType() {
        return this.zzKt;
    }

    final int size() {
        return this.zzKr.size();
    }

    final void zza(zzsi zzsiVar, zzir zzirVar) {
        this.zzKr.add(new zzto(this, zzsiVar, zzirVar));
    }

    final boolean zzb(zzsi zzsiVar) {
        zzto zztoVar = new zzto(this, zzsiVar);
        this.zzKr.add(zztoVar);
        return zztoVar.load();
    }

    final zzir zzeI() {
        return this.zzKs;
    }

    final int zzeJ() {
        int i = 0;
        Iterator<zzto> it = this.zzKr.iterator();
        while (true) {
            int i2 = i;
            if (!it.hasNext()) {
                return i2;
            }
            i = it.next().zzKz ? i2 + 1 : i2;
        }
    }

    final int zzeK() {
        int i = 0;
        Iterator<zzto> it = this.zzKr.iterator();
        while (true) {
            int i2 = i;
            if (!it.hasNext()) {
                return i2;
            }
            i = it.next().load() ? i2 + 1 : i2;
        }
    }

    final void zzeL() {
        this.zzKu = true;
    }

    final boolean zzeM() {
        return this.zzKu;
    }

    final zzto zzm(@Nullable zzir zzirVar) {
        if (zzirVar != null) {
            this.zzKs = zzirVar;
        }
        return this.zzKr.remove();
    }
}
