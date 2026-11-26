package com.google.android.gms.internal;

import android.content.Context;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaev implements zzgi {
    private final Context mContext;
    private final String zztV;
    private boolean zzXy = false;
    private final Object mLock = new Object();

    public zzaev(Context context, String str) {
        this.mContext = context;
        this.zztV = str;
    }

    @Override // com.google.android.gms.internal.zzgi
    public final void zza(zzgh zzghVar) {
        zzu(zzghVar.zzxS);
    }

    public final void zzu(boolean z) {
        if (com.google.android.gms.ads.internal.zzbs.zzbY().zzp(this.mContext)) {
            synchronized (this.mLock) {
                if (this.zzXy == z) {
                    return;
                }
                this.zzXy = z;
                if (this.zzXy) {
                    com.google.android.gms.ads.internal.zzbs.zzbY().zzc(this.mContext, this.zztV);
                } else {
                    com.google.android.gms.ads.internal.zzbs.zzbY().zzd(this.mContext, this.zztV);
                }
            }
        }
    }
}
