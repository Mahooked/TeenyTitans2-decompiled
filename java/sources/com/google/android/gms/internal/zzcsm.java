package com.google.android.gms.internal;

import android.os.SystemClock;
import android.util.SparseArray;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.safetynet.SafeBrowsingData;

/* JADX INFO: loaded from: classes.dex */
final class zzcsm extends zzcru {
    private /* synthetic */ zzcsa.zzf zzbCb;

    zzcsm(zzcsa.zzf zzfVar) {
        this.zzbCb = zzfVar;
    }

    @Override // com.google.android.gms.internal.zzcru, com.google.android.gms.internal.zzcrw
    public final void zza(Status status, SafeBrowsingData safeBrowsingData) {
        DataHolder blacklistsDataHolder = safeBrowsingData.getBlacklistsDataHolder();
        if (blacklistsDataHolder != null) {
            try {
                int count = blacklistsDataHolder.getCount();
                if (count != 0) {
                    if (zzcsa.zzbBM != null) {
                        zzcsa.zzbBM.clear();
                    }
                    zzcsa.zzbBM = new SparseArray<>();
                    for (int i = 0; i < count; i++) {
                        zzcsr zzcsrVar = new zzcsr(blacklistsDataHolder, i);
                        zzcsa.zzbBM.put(zzcsrVar.getThreatType(), zzcsrVar);
                    }
                    zzcsa.zzbBN = SystemClock.elapsedRealtime();
                }
                if (!blacklistsDataHolder.isClosed()) {
                    blacklistsDataHolder.close();
                }
            } catch (Throwable th) {
                if (!blacklistsDataHolder.isClosed()) {
                    blacklistsDataHolder.close();
                }
                throw th;
            }
        }
        this.zzbCb.setResult(new zzcsa.zzi(status, safeBrowsingData));
    }
}
