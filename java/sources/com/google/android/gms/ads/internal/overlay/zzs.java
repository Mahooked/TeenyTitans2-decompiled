package com.google.android.gms.ads.internal.overlay;

import android.graphics.Bitmap;
import com.google.android.gms.ads.internal.zzbs;
import com.google.android.gms.internal.zzafp;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@zzzn
final class zzs extends zzafp {
    final /* synthetic */ zzm zzOY;

    private zzs(zzm zzmVar) {
        this.zzOY = zzmVar;
    }

    /* synthetic */ zzs(zzm zzmVar, zzn zznVar) {
        this(zzmVar);
    }

    @Override // com.google.android.gms.internal.zzafp
    public void onStop() {
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        Bitmap bitmapZza = zzbs.zzbU().zza(Integer.valueOf(this.zzOY.zzOG.zzPo.zzuw));
        if (bitmapZza != null) {
            zzagz.zzZr.post(new zzt(this, zzbs.zzbB().zza(this.zzOY.mActivity, bitmapZza, this.zzOY.zzOG.zzPo.zzuu, this.zzOY.zzOG.zzPo.zzuv)));
        }
    }
}
