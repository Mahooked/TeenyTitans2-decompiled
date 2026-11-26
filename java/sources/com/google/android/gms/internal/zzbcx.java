package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import android.os.Looper;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Result;
import java.io.FileDescriptor;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.locks.Condition;
import java.util.concurrent.locks.Lock;

/* JADX INFO: loaded from: classes.dex */
public final class zzbcx implements zzbbj, zzbdp {
    private final Context mContext;
    private Api.zza<? extends zzctk, zzctl> zzaBe;
    private com.google.android.gms.common.internal.zzq zzaCA;
    private Map<Api<?>, Boolean> zzaCD;
    private final com.google.android.gms.common.zze zzaCF;
    final zzbcp zzaCl;
    private final Lock zzaCv;
    final Map<Api.zzc<?>, Api.zze> zzaDF;
    private final Condition zzaDS;
    private final zzbcz zzaDT;
    private volatile zzbcw zzaDV;
    int zzaDX;
    final zzbdq zzaDY;
    final Map<Api.zzc<?>, ConnectionResult> zzaDU = new HashMap();
    private ConnectionResult zzaDW = null;

    public zzbcx(Context context, zzbcp zzbcpVar, Lock lock, Looper looper, com.google.android.gms.common.zze zzeVar, Map<Api.zzc<?>, Api.zze> map, com.google.android.gms.common.internal.zzq zzqVar, Map<Api<?>, Boolean> map2, Api.zza<? extends zzctk, zzctl> zzaVar, ArrayList<zzbbi> arrayList, zzbdq zzbdqVar) {
        this.mContext = context;
        this.zzaCv = lock;
        this.zzaCF = zzeVar;
        this.zzaDF = map;
        this.zzaCA = zzqVar;
        this.zzaCD = map2;
        this.zzaBe = zzaVar;
        this.zzaCl = zzbcpVar;
        this.zzaDY = zzbdqVar;
        ArrayList<zzbbi> arrayList2 = arrayList;
        int size = arrayList2.size();
        int i = 0;
        while (i < size) {
            zzbbi zzbbiVar = arrayList2.get(i);
            i++;
            zzbbiVar.zza(this);
        }
        this.zzaDT = new zzbcz(this, looper);
        this.zzaDS = lock.newCondition();
        this.zzaDV = new zzbco(this);
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final ConnectionResult blockingConnect() {
        connect();
        while (isConnecting()) {
            try {
                this.zzaDS.await();
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                return new ConnectionResult(15, null);
            }
        }
        if (isConnected()) {
            return ConnectionResult.zzazX;
        }
        return this.zzaDW != null ? this.zzaDW : new ConnectionResult(13, null);
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final ConnectionResult blockingConnect(long j, TimeUnit timeUnit) {
        connect();
        long nanos = timeUnit.toNanos(j);
        while (isConnecting()) {
            if (nanos <= 0) {
                disconnect();
                return new ConnectionResult(14, null);
            }
            try {
                nanos = this.zzaDS.awaitNanos(nanos);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                return new ConnectionResult(15, null);
            }
            Thread.currentThread().interrupt();
            return new ConnectionResult(15, null);
        }
        if (isConnected()) {
            return ConnectionResult.zzazX;
        }
        return this.zzaDW != null ? this.zzaDW : new ConnectionResult(13, null);
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final void connect() {
        this.zzaDV.connect();
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final void disconnect() {
        if (this.zzaDV.disconnect()) {
            this.zzaDU.clear();
        }
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
        String strConcat = String.valueOf(str).concat("  ");
        printWriter.append((CharSequence) str).append("mState=").println(this.zzaDV);
        for (Api<?> api : this.zzaCD.keySet()) {
            printWriter.append((CharSequence) str).append((CharSequence) api.getName()).println(":");
            this.zzaDF.get(api.zzpd()).dump(strConcat, fileDescriptor, printWriter, strArr);
        }
    }

    @Override // com.google.android.gms.internal.zzbdp
    @Nullable
    public final ConnectionResult getConnectionResult(@NonNull Api<?> api) {
        Api.zzc<?> zzcVarZzpd = api.zzpd();
        if (this.zzaDF.containsKey(zzcVarZzpd)) {
            if (this.zzaDF.get(zzcVarZzpd).isConnected()) {
                return ConnectionResult.zzazX;
            }
            if (this.zzaDU.containsKey(zzcVarZzpd)) {
                return this.zzaDU.get(zzcVarZzpd);
            }
        }
        return null;
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final boolean isConnected() {
        return this.zzaDV instanceof zzbca;
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final boolean isConnecting() {
        return this.zzaDV instanceof zzbcd;
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks
    public final void onConnected(@Nullable Bundle bundle) {
        this.zzaCv.lock();
        try {
            this.zzaDV.onConnected(bundle);
        } finally {
            this.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks
    public final void onConnectionSuspended(int i) {
        this.zzaCv.lock();
        try {
            this.zzaDV.onConnectionSuspended(i);
        } finally {
            this.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.internal.zzbbj
    public final void zza(@NonNull ConnectionResult connectionResult, @NonNull Api<?> api, boolean z) {
        this.zzaCv.lock();
        try {
            this.zzaDV.zza(connectionResult, api, z);
        } finally {
            this.zzaCv.unlock();
        }
    }

    final void zza(zzbcy zzbcyVar) {
        this.zzaDT.sendMessage(this.zzaDT.obtainMessage(1, zzbcyVar));
    }

    final void zza(RuntimeException runtimeException) {
        this.zzaDT.sendMessage(this.zzaDT.obtainMessage(2, runtimeException));
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final boolean zza(zzbei zzbeiVar) {
        return false;
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final <A extends Api.zzb, R extends Result, T extends zzbay<R, A>> T zzd(@NonNull T t) {
        t.zzpC();
        return (T) this.zzaDV.zzd(t);
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final <A extends Api.zzb, T extends zzbay<? extends Result, A>> T zze(@NonNull T t) {
        t.zzpC();
        return (T) this.zzaDV.zze(t);
    }

    final void zzg(ConnectionResult connectionResult) {
        this.zzaCv.lock();
        try {
            this.zzaDW = connectionResult;
            this.zzaDV = new zzbco(this);
            this.zzaDV.begin();
            this.zzaDS.signalAll();
        } finally {
            this.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final void zzpE() {
        if (isConnected()) {
            ((zzbca) this.zzaDV).zzpU();
        }
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final void zzpl() {
    }

    final void zzqh() {
        this.zzaCv.lock();
        try {
            this.zzaDV = new zzbcd(this, this.zzaCA, this.zzaCD, this.zzaCF, this.zzaBe, this.zzaCv, this.mContext);
            this.zzaDV.begin();
            this.zzaDS.signalAll();
        } finally {
            this.zzaCv.unlock();
        }
    }

    final void zzqi() {
        this.zzaCv.lock();
        try {
            this.zzaCl.zzqe();
            this.zzaDV = new zzbca(this);
            this.zzaDV.begin();
            this.zzaDS.signalAll();
        } finally {
            this.zzaCv.unlock();
        }
    }
}
