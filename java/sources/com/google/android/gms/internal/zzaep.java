package com.google.android.gms.internal;

import android.support.annotation.NonNull;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.ResultCallback;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.safetynet.SafetyNetApi;
import java.util.Map;
import java.util.concurrent.atomic.AtomicInteger;

/* JADX INFO: loaded from: classes.dex */
final class zzaep implements ResultCallback<SafetyNetApi.SafeBrowsingResult> {
    private /* synthetic */ zzajg zzXk;
    private /* synthetic */ Map zzXo;
    private /* synthetic */ AtomicInteger zzXp;
    private /* synthetic */ GoogleApiClient zzXq;
    private /* synthetic */ String zzsD;

    zzaep(zzael zzaelVar, Map map, String str, AtomicInteger atomicInteger, GoogleApiClient googleApiClient, zzajg zzajgVar) {
        this.zzXo = map;
        this.zzsD = str;
        this.zzXp = atomicInteger;
        this.zzXq = googleApiClient;
        this.zzXk = zzajgVar;
    }

    @Override // com.google.android.gms.common.api.ResultCallback
    public final /* synthetic */ void onResult(@NonNull Result result) {
        SafetyNetApi.SafeBrowsingResult safeBrowsingResult = (SafetyNetApi.SafeBrowsingResult) result;
        Status status = safeBrowsingResult.getStatus();
        if (status == null || !status.isSuccess() || safeBrowsingResult.getMetadata() == null) {
            String strValueOf = String.valueOf(status);
            zzaes.zzaC(new StringBuilder(String.valueOf(strValueOf).length() + 36).append("SafeBrowsing lookup failed. Status: ").append(strValueOf).toString());
        } else {
            this.zzXo.put(this.zzsD, safeBrowsingResult.getMetadata());
        }
        if (this.zzXp.decrementAndGet() == 0) {
            this.zzXq.disconnect();
            this.zzXk.zzg(this.zzXo);
        }
    }
}
