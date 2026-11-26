package com.google.android.gms.ads.internal;

import android.os.RemoteException;
import android.view.View;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzrd;
import com.google.android.gms.internal.zzvc;
import com.google.android.gms.internal.zzvf;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzaw implements zzrd {
    private /* synthetic */ zzvc zzuC;
    private /* synthetic */ zzab zzuD;
    private /* synthetic */ zzvf zzuE;

    zzaw(zzvc zzvcVar, zzab zzabVar, zzvf zzvfVar) {
        this.zzuC = zzvcVar;
        this.zzuD = zzabVar;
        this.zzuE = zzvfVar;
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        View view = zzakaVar.getView();
        if (view == null) {
            return;
        }
        try {
            if (this.zzuC != null) {
                if (this.zzuC.getOverrideClickHandling()) {
                    zzar.zzb(zzakaVar);
                } else {
                    this.zzuC.zzl(com.google.android.gms.dynamic.zzn.zzw(view));
                    this.zzuD.zztt.onAdClicked();
                }
            } else if (this.zzuE != null) {
                if (this.zzuE.getOverrideClickHandling()) {
                    zzar.zzb(zzakaVar);
                } else {
                    this.zzuE.zzl(com.google.android.gms.dynamic.zzn.zzw(view));
                    this.zzuD.zztt.onAdClicked();
                }
            }
        } catch (RemoteException e) {
            zzafr.zzc("Unable to call handleClick on mapper", e);
        }
    }
}
