package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzakt implements Runnable {
    private /* synthetic */ Map zzacB;
    private /* synthetic */ zzaks zzacC;

    zzakt(zzaks zzaksVar, Map map) {
        this.zzacC = zzaksVar;
        this.zzacB = map;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzacC.zzJH.zza("pubVideoCmd", this.zzacB);
    }
}
