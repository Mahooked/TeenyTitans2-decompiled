package com.google.android.gms.internal;

import android.content.Context;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaiq extends zzafp {
    private final String zzD;
    private final zzajd zzaat;

    public zzaiq(Context context, String str, String str2) {
        this(str2, com.google.android.gms.ads.internal.zzbs.zzbz().zzs(context, str));
    }

    public zzaiq(String str, String str2) {
        this.zzaat = new zzajd(str2);
        this.zzD = str;
    }

    @Override // com.google.android.gms.internal.zzafp
    public void onStop() {
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        this.zzaat.zzaN(this.zzD);
    }
}
