package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzahv implements Runnable {
    private /* synthetic */ zzahq zzZI;

    zzahv(zzahq zzahqVar) {
        this.zzZI = zzahqVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        com.google.android.gms.ads.internal.zzbs.zzbH().zzg(this.zzZI.mContext, this.zzZI.zztV, this.zzZI.zzwH);
    }
}
