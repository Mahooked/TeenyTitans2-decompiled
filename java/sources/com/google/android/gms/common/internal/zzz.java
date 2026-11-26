package com.google.android.gms.common.internal;

import android.accounts.Account;
import android.content.Context;
import android.os.IInterface;
import android.os.Looper;
import android.support.annotation.NonNull;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Scope;
import java.util.Iterator;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzz<T extends IInterface> extends zzd<T> implements Api.zze, zzad {
    private final zzq zzaCA;
    private final Account zzajb;
    private final Set<Scope> zzame;

    protected zzz(Context context, Looper looper, int i, zzq zzqVar, GoogleApiClient.ConnectionCallbacks connectionCallbacks, GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener) {
        this(context, looper, zzae.zzaC(context), GoogleApiAvailability.getInstance(), i, zzqVar, (GoogleApiClient.ConnectionCallbacks) zzbo.zzu(connectionCallbacks), (GoogleApiClient.OnConnectionFailedListener) zzbo.zzu(onConnectionFailedListener));
    }

    private zzz(Context context, Looper looper, zzae zzaeVar, GoogleApiAvailability googleApiAvailability, int i, zzq zzqVar, GoogleApiClient.ConnectionCallbacks connectionCallbacks, GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener) {
        super(context, looper, zzaeVar, googleApiAvailability, i, connectionCallbacks == null ? null : new zzaa(connectionCallbacks), onConnectionFailedListener == null ? null : new zzab(onConnectionFailedListener), zzqVar.zzrr());
        this.zzaCA = zzqVar;
        this.zzajb = zzqVar.getAccount();
        Set<Scope> setZzro = zzqVar.zzro();
        Set<Scope> setZzb = zzb(setZzro);
        Iterator<Scope> it = setZzb.iterator();
        while (it.hasNext()) {
            if (!setZzro.contains(it.next())) {
                throw new IllegalStateException("Expanding scopes is not permitted, use implied scopes instead");
            }
        }
        this.zzame = setZzb;
    }

    @Override // com.google.android.gms.common.internal.zzd
    public final Account getAccount() {
        return this.zzajb;
    }

    @NonNull
    protected Set<Scope> zzb(@NonNull Set<Scope> set) {
        return set;
    }

    @Override // com.google.android.gms.common.internal.zzd
    public com.google.android.gms.common.zzc[] zzrd() {
        return new com.google.android.gms.common.zzc[0];
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final Set<Scope> zzrh() {
        return this.zzame;
    }

    protected final zzq zzry() {
        return this.zzaCA;
    }
}
