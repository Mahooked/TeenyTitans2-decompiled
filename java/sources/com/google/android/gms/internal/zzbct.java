package com.google.android.gms.internal;

import android.support.annotation.NonNull;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.ResultCallback;
import com.google.android.gms.common.api.Status;

/* JADX INFO: loaded from: classes.dex */
final class zzbct implements ResultCallback<Status> {
    private /* synthetic */ zzbcp zzaDN;
    private /* synthetic */ zzben zzaDP;
    private /* synthetic */ boolean zzaDQ;
    private /* synthetic */ GoogleApiClient zzaqW;

    zzbct(zzbcp zzbcpVar, zzben zzbenVar, boolean z, GoogleApiClient googleApiClient) {
        this.zzaDN = zzbcpVar;
        this.zzaDP = zzbenVar;
        this.zzaDQ = z;
        this.zzaqW = googleApiClient;
    }

    @Override // com.google.android.gms.common.api.ResultCallback
    public final /* synthetic */ void onResult(@NonNull Result result) {
        Status status = (Status) result;
        com.google.android.gms.auth.api.signin.internal.zzy.zzaj(this.zzaDN.mContext).zzmP();
        if (status.isSuccess() && this.zzaDN.isConnected()) {
            this.zzaDN.reconnect();
        }
        this.zzaDP.setResult(status);
        if (this.zzaDQ) {
            this.zzaqW.disconnect();
        }
    }
}
