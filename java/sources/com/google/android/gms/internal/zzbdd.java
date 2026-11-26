package com.google.android.gms.internal;

import android.os.Bundle;
import android.os.DeadObjectException;
import android.os.Looper;
import android.os.Message;
import android.os.RemoteException;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.ApiOptions;
import com.google.android.gms.common.api.GoogleApi;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.zzbo;
import com.google.android.gms.tasks.TaskCompletionSource;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.Map;
import java.util.Queue;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
public final class zzbdd<O extends Api.ApiOptions> implements GoogleApiClient.ConnectionCallbacks, GoogleApiClient.OnConnectionFailedListener, zzbbj {
    private final zzbat<O> zzaAK;
    private final Api.zze zzaCy;
    private boolean zzaDA;
    private /* synthetic */ zzbdb zzaEm;
    private final Api.zzb zzaEo;
    private final zzbbt zzaEp;
    private final int zzaEs;
    private final zzbej zzaEt;
    private final Queue<zzbam> zzaEn = new LinkedList();
    private final Set<zzbav> zzaEq = new HashSet();
    private final Map<zzbdy<?>, zzbef> zzaEr = new HashMap();
    private ConnectionResult zzaEu = null;

    @WorkerThread
    public zzbdd(zzbdb zzbdbVar, GoogleApi<O> googleApi) {
        this.zzaEm = zzbdbVar;
        this.zzaCy = googleApi.zza(zzbdbVar.mHandler.getLooper(), this);
        if (this.zzaCy instanceof com.google.android.gms.common.internal.zzbx) {
            this.zzaEo = null;
        } else {
            this.zzaEo = this.zzaCy;
        }
        this.zzaAK = googleApi.zzph();
        this.zzaEp = new zzbbt();
        this.zzaEs = googleApi.getInstanceId();
        if (this.zzaCy.zzmv()) {
            this.zzaEt = googleApi.zza(zzbdbVar.mContext, zzbdbVar.mHandler);
        } else {
            this.zzaEt = null;
        }
    }

    @WorkerThread
    private final void zzb(zzbam zzbamVar) {
        zzbamVar.zza(this.zzaEp, zzmv());
        try {
            zzbamVar.zza((zzbdd<?>) this);
        } catch (DeadObjectException e) {
            onConnectionSuspended(1);
            this.zzaCy.disconnect();
        }
    }

    @WorkerThread
    private final void zzi(ConnectionResult connectionResult) {
        Iterator<zzbav> it = this.zzaEq.iterator();
        while (it.hasNext()) {
            it.next().zza(this.zzaAK, connectionResult);
        }
        this.zzaEq.clear();
    }

    /* JADX INFO: Access modifiers changed from: private */
    @WorkerThread
    public final void zzqq() {
        zzqt();
        zzi(ConnectionResult.zzazX);
        zzqv();
        Iterator<zzbef> it = this.zzaEr.values().iterator();
        while (it.hasNext()) {
            try {
                it.next().zzaBu.zzb(this.zzaEo, new TaskCompletionSource<>());
            } catch (DeadObjectException e) {
                onConnectionSuspended(1);
                this.zzaCy.disconnect();
            } catch (RemoteException e2) {
            }
        }
        while (this.zzaCy.isConnected() && !this.zzaEn.isEmpty()) {
            zzb(this.zzaEn.remove());
        }
        zzqw();
    }

    /* JADX INFO: Access modifiers changed from: private */
    @WorkerThread
    public final void zzqr() {
        zzqt();
        this.zzaDA = true;
        this.zzaEp.zzpQ();
        this.zzaEm.mHandler.sendMessageDelayed(Message.obtain(this.zzaEm.mHandler, 9, this.zzaAK), this.zzaEm.zzaDC);
        this.zzaEm.mHandler.sendMessageDelayed(Message.obtain(this.zzaEm.mHandler, 11, this.zzaAK), this.zzaEm.zzaDB);
        this.zzaEm.zzaEg = -1;
    }

    @WorkerThread
    private final void zzqv() {
        if (this.zzaDA) {
            this.zzaEm.mHandler.removeMessages(11, this.zzaAK);
            this.zzaEm.mHandler.removeMessages(9, this.zzaAK);
            this.zzaDA = false;
        }
    }

    private final void zzqw() {
        this.zzaEm.mHandler.removeMessages(12, this.zzaAK);
        this.zzaEm.mHandler.sendMessageDelayed(this.zzaEm.mHandler.obtainMessage(12, this.zzaAK), this.zzaEm.zzaEe);
    }

    @WorkerThread
    public final void connect() {
        zzbo.zza(this.zzaEm.mHandler);
        if (this.zzaCy.isConnected() || this.zzaCy.isConnecting()) {
            return;
        }
        if (this.zzaCy.zzpe() && this.zzaEm.zzaEg != 0) {
            this.zzaEm.zzaEg = this.zzaEm.zzaBd.isGooglePlayServicesAvailable(this.zzaEm.mContext);
            if (this.zzaEm.zzaEg != 0) {
                onConnectionFailed(new ConnectionResult(this.zzaEm.zzaEg, null));
                return;
            }
        }
        zzbdh zzbdhVar = new zzbdh(this.zzaEm, this.zzaCy, this.zzaAK);
        if (this.zzaCy.zzmv()) {
            this.zzaEt.zza(zzbdhVar);
        }
        this.zzaCy.zza(zzbdhVar);
    }

    public final int getInstanceId() {
        return this.zzaEs;
    }

    final boolean isConnected() {
        return this.zzaCy.isConnected();
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks
    public final void onConnected(@Nullable Bundle bundle) {
        if (Looper.myLooper() == this.zzaEm.mHandler.getLooper()) {
            zzqq();
        } else {
            this.zzaEm.mHandler.post(new zzbde(this));
        }
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener
    @WorkerThread
    public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
        zzbo.zza(this.zzaEm.mHandler);
        if (this.zzaEt != null) {
            this.zzaEt.zzqI();
        }
        zzqt();
        this.zzaEm.zzaEg = -1;
        zzi(connectionResult);
        if (connectionResult.getErrorCode() == 4) {
            zzt(zzbdb.zzaEd);
            return;
        }
        if (this.zzaEn.isEmpty()) {
            this.zzaEu = connectionResult;
            return;
        }
        synchronized (zzbdb.zzuF) {
            if (this.zzaEm.zzaEj != null && this.zzaEm.zzaEk.contains(this.zzaAK)) {
                this.zzaEm.zzaEj.zzb(connectionResult, this.zzaEs);
            } else if (!this.zzaEm.zzc(connectionResult, this.zzaEs)) {
                if (connectionResult.getErrorCode() == 18) {
                    this.zzaDA = true;
                }
                if (this.zzaDA) {
                    this.zzaEm.mHandler.sendMessageDelayed(Message.obtain(this.zzaEm.mHandler, 9, this.zzaAK), this.zzaEm.zzaDC);
                } else {
                    String strValueOf = String.valueOf(this.zzaAK.zzpr());
                    zzt(new Status(17, new StringBuilder(String.valueOf(strValueOf).length() + 38).append("API: ").append(strValueOf).append(" is not available on this device.").toString()));
                }
            }
        }
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks
    public final void onConnectionSuspended(int i) {
        if (Looper.myLooper() == this.zzaEm.mHandler.getLooper()) {
            zzqr();
        } else {
            this.zzaEm.mHandler.post(new zzbdf(this));
        }
    }

    @WorkerThread
    public final void resume() {
        zzbo.zza(this.zzaEm.mHandler);
        if (this.zzaDA) {
            connect();
        }
    }

    @WorkerThread
    public final void signOut() {
        zzbo.zza(this.zzaEm.mHandler);
        zzt(zzbdb.zzaEc);
        this.zzaEp.zzpP();
        Iterator<zzbdy<?>> it = this.zzaEr.keySet().iterator();
        while (it.hasNext()) {
            zza(new zzbar(it.next(), new TaskCompletionSource()));
        }
        zzi(new ConnectionResult(4));
        this.zzaCy.disconnect();
    }

    @Override // com.google.android.gms.internal.zzbbj
    public final void zza(ConnectionResult connectionResult, Api<?> api, boolean z) {
        if (Looper.myLooper() == this.zzaEm.mHandler.getLooper()) {
            onConnectionFailed(connectionResult);
        } else {
            this.zzaEm.mHandler.post(new zzbdg(this, connectionResult));
        }
    }

    @WorkerThread
    public final void zza(zzbam zzbamVar) {
        zzbo.zza(this.zzaEm.mHandler);
        if (this.zzaCy.isConnected()) {
            zzb(zzbamVar);
            zzqw();
            return;
        }
        this.zzaEn.add(zzbamVar);
        if (this.zzaEu == null || !this.zzaEu.hasResolution()) {
            connect();
        } else {
            onConnectionFailed(this.zzaEu);
        }
    }

    @WorkerThread
    public final void zza(zzbav zzbavVar) {
        zzbo.zza(this.zzaEm.mHandler);
        this.zzaEq.add(zzbavVar);
    }

    @WorkerThread
    public final void zzh(@NonNull ConnectionResult connectionResult) {
        zzbo.zza(this.zzaEm.mHandler);
        this.zzaCy.disconnect();
        onConnectionFailed(connectionResult);
    }

    public final boolean zzmv() {
        return this.zzaCy.zzmv();
    }

    public final Api.zze zzpJ() {
        return this.zzaCy;
    }

    @WorkerThread
    public final void zzqd() {
        zzbo.zza(this.zzaEm.mHandler);
        if (this.zzaDA) {
            zzqv();
            zzt(this.zzaEm.zzaBd.isGooglePlayServicesAvailable(this.zzaEm.mContext) == 18 ? new Status(8, "Connection timed out while waiting for Google Play services update to complete.") : new Status(8, "API failed to connect while resuming due to an unknown error."));
            this.zzaCy.disconnect();
        }
    }

    public final Map<zzbdy<?>, zzbef> zzqs() {
        return this.zzaEr;
    }

    @WorkerThread
    public final void zzqt() {
        zzbo.zza(this.zzaEm.mHandler);
        this.zzaEu = null;
    }

    @WorkerThread
    public final ConnectionResult zzqu() {
        zzbo.zza(this.zzaEm.mHandler);
        return this.zzaEu;
    }

    @WorkerThread
    public final void zzqx() {
        zzbo.zza(this.zzaEm.mHandler);
        if (this.zzaCy.isConnected() && this.zzaEr.size() == 0) {
            if (this.zzaEp.zzpO()) {
                zzqw();
            } else {
                this.zzaCy.disconnect();
            }
        }
    }

    final zzctk zzqy() {
        if (this.zzaEt == null) {
            return null;
        }
        return this.zzaEt.zzqy();
    }

    @WorkerThread
    public final void zzt(Status status) {
        zzbo.zza(this.zzaEm.mHandler);
        Iterator<zzbam> it = this.zzaEn.iterator();
        while (it.hasNext()) {
            it.next().zzp(status);
        }
        this.zzaEn.clear();
    }
}
