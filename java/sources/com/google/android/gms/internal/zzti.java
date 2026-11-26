package com.google.android.gms.internal;

import android.support.annotation.Nullable;

/* JADX INFO: loaded from: classes.dex */
@zzzn
final class zzti {

    @Nullable
    zzke zzKi;

    @Nullable
    zznh zzKj;

    @Nullable
    zzjl zzKk;

    @Nullable
    zzadd zzKl;

    @Nullable
    zzjo zztK;

    zzti() {
    }

    final void zzd(com.google.android.gms.ads.internal.zzal zzalVar) {
        if (this.zztK != null) {
            zzalVar.zza(new zztj(this.zztK));
        }
        if (this.zzKi != null) {
            zzalVar.zza(this.zzKi);
        }
        if (this.zzKj != null) {
            zzalVar.zza(this.zzKj);
        }
        if (this.zzKk != null) {
            zzalVar.zza(this.zzKk);
        }
        if (this.zzKl != null) {
            zzalVar.zza(this.zzKl);
        }
    }
}
