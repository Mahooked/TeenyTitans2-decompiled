package com.google.android.gms.internal;

import android.support.annotation.WorkerThread;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import java.util.Iterator;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzbcg extends zzbcn {
    final /* synthetic */ zzbcd zzaDp;
    private final Map<Api.zze, zzbcf> zzaDr;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    public zzbcg(zzbcd zzbcdVar, Map<Api.zze, zzbcf> map) {
        super(zzbcdVar, null);
        this.zzaDp = zzbcdVar;
        this.zzaDr = map;
    }

    @Override // com.google.android.gms.internal.zzbcn
    @WorkerThread
    public final void zzpV() {
        boolean z;
        boolean z2;
        boolean z3;
        boolean z4 = true;
        Iterator<Api.zze> it = this.zzaDr.keySet().iterator();
        boolean z5 = true;
        boolean z6 = false;
        while (true) {
            if (!it.hasNext()) {
                z4 = z6;
                z = false;
                break;
            }
            Api.zze next = it.next();
            if (!next.zzpe()) {
                z2 = false;
                z3 = z6;
            } else if (!this.zzaDr.get(next).zzaCj) {
                z = true;
                break;
            } else {
                z2 = z5;
                z3 = true;
            }
            z6 = z3;
            z5 = z2;
        }
        int iIsGooglePlayServicesAvailable = z4 ? this.zzaDp.zzaCF.isGooglePlayServicesAvailable(this.zzaDp.mContext) : 0;
        if (iIsGooglePlayServicesAvailable != 0 && (z || z5)) {
            this.zzaDp.zzaCZ.zza(new zzbch(this, this.zzaDp, new ConnectionResult(iIsGooglePlayServicesAvailable, null)));
            return;
        }
        if (this.zzaDp.zzaDj) {
            this.zzaDp.zzaDh.connect();
        }
        for (Api.zze zzeVar : this.zzaDr.keySet()) {
            zzbcf zzbcfVar = this.zzaDr.get(zzeVar);
            if (!zzeVar.zzpe() || iIsGooglePlayServicesAvailable == 0) {
                zzeVar.zza(zzbcfVar);
            } else {
                this.zzaDp.zzaCZ.zza(new zzbci(this, this.zzaDp, zzbcfVar));
            }
        }
    }
}
