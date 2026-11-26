package com.google.android.gms.internal;

import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzabb implements Runnable {
    private /* synthetic */ zzaaz zzUc;
    final /* synthetic */ JSONObject zzUd;
    final /* synthetic */ String zzUe;

    zzabb(zzaaz zzaazVar, JSONObject jSONObject, String str) {
        this.zzUc = zzaazVar;
        this.zzUd = jSONObject;
        this.zzUe = str;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzUc.zzUb = zzaaz.zzRl.zzb((zzcu) null);
        this.zzUc.zzUb.zza(new zzabc(this), new zzabd(this));
    }
}
