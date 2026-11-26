package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzbce implements Runnable {
    private /* synthetic */ zzbcd zzaDp;

    zzbce(zzbcd zzbcdVar) {
        this.zzaDp = zzbcdVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        com.google.android.gms.common.zze.zzat(this.zzaDp.mContext);
    }
}
