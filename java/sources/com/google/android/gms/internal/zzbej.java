package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import android.os.Handler;
import android.support.annotation.BinderThread;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import android.util.Log;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.internal.zzbo;
import com.google.android.gms.common.internal.zzbr;
import java.util.HashSet;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
public final class zzbej extends zzctp implements GoogleApiClient.ConnectionCallbacks, GoogleApiClient.OnConnectionFailedListener {
    private static Api.zza<? extends zzctk, zzctl> zzaEV = zzctg.zzajS;
    private final Context mContext;
    private final Handler mHandler;
    private final Api.zza<? extends zzctk, zzctl> zzaAx;
    private com.google.android.gms.common.internal.zzq zzaCA;
    private zzctk zzaDh;
    private final boolean zzaEW;
    private zzbel zzaEX;
    private Set<Scope> zzame;

    @WorkerThread
    public zzbej(Context context, Handler handler) {
        this.mContext = context;
        this.mHandler = handler;
        this.zzaAx = zzaEV;
        this.zzaEW = true;
    }

    @WorkerThread
    public zzbej(Context context, Handler handler, @NonNull com.google.android.gms.common.internal.zzq zzqVar, Api.zza<? extends zzctk, zzctl> zzaVar) {
        this.mContext = context;
        this.mHandler = handler;
        this.zzaCA = (com.google.android.gms.common.internal.zzq) zzbo.zzb(zzqVar, "ClientSettings must not be null");
        this.zzame = zzqVar.zzrn();
        this.zzaAx = zzaVar;
        this.zzaEW = false;
    }

    /* JADX INFO: Access modifiers changed from: private */
    @WorkerThread
    public final void zzc(zzctx zzctxVar) {
        ConnectionResult connectionResultZzpz = zzctxVar.zzpz();
        if (connectionResultZzpz.isSuccess()) {
            zzbr zzbrVarZzAx = zzctxVar.zzAx();
            ConnectionResult connectionResultZzpz2 = zzbrVarZzAx.zzpz();
            if (!connectionResultZzpz2.isSuccess()) {
                String strValueOf = String.valueOf(connectionResultZzpz2);
                Log.wtf("SignInCoordinator", new StringBuilder(String.valueOf(strValueOf).length() + 48).append("Sign-in succeeded with resolve account failure: ").append(strValueOf).toString(), new Exception());
                this.zzaEX.zzh(connectionResultZzpz2);
                this.zzaDh.disconnect();
                return;
            }
            this.zzaEX.zzb(zzbrVarZzAx.zzrH(), this.zzame);
        } else {
            this.zzaEX.zzh(connectionResultZzpz);
        }
        this.zzaDh.disconnect();
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks
    @WorkerThread
    public final void onConnected(@Nullable Bundle bundle) {
        this.zzaDh.zza(this);
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener
    @WorkerThread
    public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
        this.zzaEX.zzh(connectionResult);
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks
    @WorkerThread
    public final void onConnectionSuspended(int i) {
        this.zzaDh.disconnect();
    }

    @WorkerThread
    public final void zza(zzbel zzbelVar) {
        if (this.zzaDh != null) {
            this.zzaDh.disconnect();
        }
        if (this.zzaEW) {
            GoogleSignInOptions googleSignInOptionsZzmO = com.google.android.gms.auth.api.signin.internal.zzy.zzaj(this.mContext).zzmO();
            this.zzame = googleSignInOptionsZzmO == null ? new HashSet() : new HashSet(googleSignInOptionsZzmO.zzmA());
            this.zzaCA = new com.google.android.gms.common.internal.zzq(null, this.zzame, null, 0, null, null, null, zzctl.zzbCM);
        }
        this.zzaCA.zzc(Integer.valueOf(System.identityHashCode(this)));
        this.zzaDh = (zzctk) this.zzaAx.zza(this.mContext, this.mHandler.getLooper(), this.zzaCA, this.zzaCA.zzrt(), this, this);
        this.zzaEX = zzbelVar;
        this.zzaDh.connect();
    }

    @Override // com.google.android.gms.internal.zzctp, com.google.android.gms.internal.zzctq
    @BinderThread
    public final void zzb(zzctx zzctxVar) {
        this.mHandler.post(new zzbek(this, zzctxVar));
    }

    public final void zzqI() {
        if (this.zzaDh != null) {
            this.zzaDh.disconnect();
        }
    }

    public final zzctk zzqy() {
        return this.zzaDh;
    }
}
