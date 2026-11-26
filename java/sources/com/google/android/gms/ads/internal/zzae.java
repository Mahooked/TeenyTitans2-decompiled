package com.google.android.gms.ads.internal;

import android.content.Context;
import com.google.android.gms.internal.zzajr;
import com.google.android.gms.internal.zzcu;
import com.google.android.gms.internal.zzrd;

/* JADX INFO: loaded from: classes.dex */
final class zzae implements Runnable {
    private /* synthetic */ com.google.android.gms.ads.internal.js.zzl zztA;
    final /* synthetic */ zzrd zztB;
    final /* synthetic */ String zztC;
    final /* synthetic */ String zztD;
    final /* synthetic */ boolean zztE;
    final /* synthetic */ Context zztF;

    zzae(zzac zzacVar, com.google.android.gms.ads.internal.js.zzl zzlVar, zzrd zzrdVar, String str, String str2, boolean z, Context context) {
        this.zztA = zzlVar;
        this.zztB = zzrdVar;
        this.zztC = str;
        this.zztD = str2;
        this.zztE = z;
        this.zztF = context;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zztA.zzb((zzcu) null).zza(new zzaf(this), new zzajr());
    }
}
