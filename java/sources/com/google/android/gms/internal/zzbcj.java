package com.google.android.gms.internal;

import android.support.annotation.WorkerThread;
import com.google.android.gms.common.api.Api;
import java.util.ArrayList;

/* JADX INFO: loaded from: classes.dex */
final class zzbcj extends zzbcn {
    private /* synthetic */ zzbcd zzaDp;
    private final ArrayList<Api.zze> zzaDv;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    public zzbcj(zzbcd zzbcdVar, ArrayList<Api.zze> arrayList) {
        super(zzbcdVar, null);
        this.zzaDp = zzbcdVar;
        this.zzaDv = arrayList;
    }

    @Override // com.google.android.gms.internal.zzbcn
    @WorkerThread
    public final void zzpV() {
        this.zzaDp.zzaCZ.zzaCl.zzaDG = this.zzaDp.zzqb();
        ArrayList<Api.zze> arrayList = this.zzaDv;
        int size = arrayList.size();
        int i = 0;
        while (i < size) {
            Api.zze zzeVar = arrayList.get(i);
            i++;
            zzeVar.zza(this.zzaDp.zzaDl, this.zzaDp.zzaCZ.zzaCl.zzaDG);
        }
    }
}
