package com.google.android.gms.internal;

import android.accounts.Account;
import android.content.Context;
import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.os.RemoteException;
import android.util.Log;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.internal.zzbo;
import com.google.android.gms.common.internal.zzbp;

/* JADX INFO: loaded from: classes.dex */
public final class zzctu extends com.google.android.gms.common.internal.zzz<zzcts> implements zzctk {
    private final com.google.android.gms.common.internal.zzq zzaCA;
    private Integer zzaHn;
    private final Bundle zzbCL;
    private final boolean zzbCT;

    public zzctu(Context context, Looper looper, boolean z, com.google.android.gms.common.internal.zzq zzqVar, Bundle bundle, GoogleApiClient.ConnectionCallbacks connectionCallbacks, GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener) {
        super(context, looper, 44, zzqVar, connectionCallbacks, onConnectionFailedListener);
        this.zzbCT = z;
        this.zzaCA = zzqVar;
        this.zzbCL = bundle;
        this.zzaHn = zzqVar.zzru();
    }

    public zzctu(Context context, Looper looper, boolean z, com.google.android.gms.common.internal.zzq zzqVar, zzctl zzctlVar, GoogleApiClient.ConnectionCallbacks connectionCallbacks, GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener) {
        this(context, looper, true, zzqVar, zza(zzqVar), connectionCallbacks, onConnectionFailedListener);
    }

    public static Bundle zza(com.google.android.gms.common.internal.zzq zzqVar) {
        zzctl zzctlVarZzrt = zzqVar.zzrt();
        Integer numZzru = zzqVar.zzru();
        Bundle bundle = new Bundle();
        bundle.putParcelable("com.google.android.gms.signin.internal.clientRequestedAccount", zzqVar.getAccount());
        if (numZzru != null) {
            bundle.putInt("com.google.android.gms.common.internal.ClientSettings.sessionId", numZzru.intValue());
        }
        if (zzctlVarZzrt != null) {
            bundle.putBoolean("com.google.android.gms.signin.internal.offlineAccessRequested", zzctlVarZzrt.zzAr());
            bundle.putBoolean("com.google.android.gms.signin.internal.idTokenRequested", zzctlVarZzrt.isIdTokenRequested());
            bundle.putString("com.google.android.gms.signin.internal.serverClientId", zzctlVarZzrt.getServerClientId());
            bundle.putBoolean("com.google.android.gms.signin.internal.usePromptModeForAuthCode", true);
            bundle.putBoolean("com.google.android.gms.signin.internal.forceCodeForRefreshToken", zzctlVarZzrt.zzAs());
            bundle.putString("com.google.android.gms.signin.internal.hostedDomain", zzctlVarZzrt.zzAt());
            bundle.putBoolean("com.google.android.gms.signin.internal.waitForAccessTokenRefresh", zzctlVarZzrt.zzAu());
            if (zzctlVarZzrt.zzAv() != null) {
                bundle.putLong("com.google.android.gms.signin.internal.authApiSignInModuleVersion", zzctlVarZzrt.zzAv().longValue());
            }
            if (zzctlVarZzrt.zzAw() != null) {
                bundle.putLong("com.google.android.gms.signin.internal.realClientLibraryVersion", zzctlVarZzrt.zzAw().longValue());
            }
        }
        return bundle;
    }

    @Override // com.google.android.gms.internal.zzctk
    public final void connect() {
        zza(new com.google.android.gms.common.internal.zzm(this));
    }

    @Override // com.google.android.gms.internal.zzctk
    public final void zzAq() {
        try {
            ((zzcts) zzrf()).zzbv(this.zzaHn.intValue());
        } catch (RemoteException e) {
            Log.w("SignInClientImpl", "Remote service probably died when clearAccountFromSessionStore is called");
        }
    }

    @Override // com.google.android.gms.internal.zzctk
    public final void zza(com.google.android.gms.common.internal.zzal zzalVar, boolean z) {
        try {
            ((zzcts) zzrf()).zza(zzalVar, this.zzaHn.intValue(), z);
        } catch (RemoteException e) {
            Log.w("SignInClientImpl", "Remote service probably died when saveDefaultAccount is called");
        }
    }

    @Override // com.google.android.gms.internal.zzctk
    public final void zza(zzctq zzctqVar) {
        zzbo.zzb(zzctqVar, "Expecting a valid ISignInCallbacks");
        try {
            Account accountZzrl = this.zzaCA.zzrl();
            ((zzcts) zzrf()).zza(new zzctv(new zzbp(accountZzrl, this.zzaHn.intValue(), "<<default account>>".equals(accountZzrl.name) ? com.google.android.gms.auth.api.signin.internal.zzy.zzaj(getContext()).zzmN() : null)), zzctqVar);
        } catch (RemoteException e) {
            Log.w("SignInClientImpl", "Remote service probably died when signIn is called");
            try {
                zzctqVar.zzb(new zzctx(8));
            } catch (RemoteException e2) {
                Log.wtf("SignInClientImpl", "ISignInCallbacks#onSignInComplete should be executed from the same process, unexpected RemoteException.", e);
            }
        }
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final /* synthetic */ IInterface zzd(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.signin.internal.ISignInService");
        return iInterfaceQueryLocalInterface instanceof zzcts ? (zzcts) iInterfaceQueryLocalInterface : new zzctt(iBinder);
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final String zzdb() {
        return "com.google.android.gms.signin.service.START";
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final String zzdc() {
        return "com.google.android.gms.signin.internal.ISignInService";
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final Bundle zzmo() {
        if (!getContext().getPackageName().equals(this.zzaCA.zzrq())) {
            this.zzbCL.putString("com.google.android.gms.signin.internal.realClientPackageName", this.zzaCA.zzrq());
        }
        return this.zzbCL;
    }

    @Override // com.google.android.gms.common.internal.zzd, com.google.android.gms.common.api.Api.zze
    public final boolean zzmv() {
        return this.zzbCT;
    }
}
