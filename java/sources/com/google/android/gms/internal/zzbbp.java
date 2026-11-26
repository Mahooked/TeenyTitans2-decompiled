package com.google.android.gms.internal;

import android.content.Context;
import android.os.Looper;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.util.ArrayMap;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import java.io.FileDescriptor;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.Map;
import java.util.Queue;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.locks.Condition;
import java.util.concurrent.locks.Lock;

/* JADX INFO: loaded from: classes.dex */
public final class zzbbp implements zzbdp {
    private final zzbdb zzaAN;
    private final com.google.android.gms.common.internal.zzq zzaCA;
    private final Map<Api<?>, Boolean> zzaCD;
    private final zzbcp zzaCE;
    private final com.google.android.gms.common.zze zzaCF;
    private final Condition zzaCG;
    private final boolean zzaCH;
    private final boolean zzaCI;
    private boolean zzaCK;
    private Map<zzbat<?>, ConnectionResult> zzaCL;
    private Map<zzbat<?>, ConnectionResult> zzaCM;
    private zzbbs zzaCN;
    private ConnectionResult zzaCO;
    private final Lock zzaCv;
    private final Looper zzrM;
    private final Map<Api.zzc<?>, zzbbo<?>> zzaCB = new HashMap();
    private final Map<Api.zzc<?>, zzbbo<?>> zzaCC = new HashMap();
    private final Queue<zzbay<?, ?>> zzaCJ = new LinkedList();

    public zzbbp(Context context, Lock lock, Looper looper, com.google.android.gms.common.zze zzeVar, Map<Api.zzc<?>, Api.zze> map, com.google.android.gms.common.internal.zzq zzqVar, Map<Api<?>, Boolean> map2, Api.zza<? extends zzctk, zzctl> zzaVar, ArrayList<zzbbi> arrayList, zzbcp zzbcpVar, boolean z) {
        boolean z2;
        boolean z3;
        boolean z4;
        this.zzaCv = lock;
        this.zzrM = looper;
        this.zzaCG = lock.newCondition();
        this.zzaCF = zzeVar;
        this.zzaCE = zzbcpVar;
        this.zzaCD = map2;
        this.zzaCA = zzqVar;
        this.zzaCH = z;
        HashMap map3 = new HashMap();
        for (Api<?> api : map2.keySet()) {
            map3.put(api.zzpd(), api);
        }
        HashMap map4 = new HashMap();
        ArrayList<zzbbi> arrayList2 = arrayList;
        int size = arrayList2.size();
        int i = 0;
        while (i < size) {
            zzbbi zzbbiVar = arrayList2.get(i);
            i++;
            zzbbi zzbbiVar2 = zzbbiVar;
            map4.put(zzbbiVar2.zzayW, zzbbiVar2);
        }
        boolean z5 = true;
        boolean z6 = false;
        boolean z7 = false;
        for (Map.Entry<Api.zzc<?>, Api.zze> entry : map.entrySet()) {
            Api api2 = (Api) map3.get(entry.getKey());
            Api.zze value = entry.getValue();
            if (value.zzpe()) {
                z2 = true;
                if (this.zzaCD.get(api2).booleanValue()) {
                    z3 = z5;
                    z4 = z6;
                } else {
                    z3 = z5;
                    z4 = true;
                }
            } else {
                z2 = z7;
                z3 = false;
                z4 = z6;
            }
            zzbbo<?> zzbboVar = new zzbbo<>(context, api2, looper, value, (zzbbi) map4.get(api2), zzqVar, zzaVar);
            this.zzaCB.put(entry.getKey(), zzbboVar);
            if (value.zzmv()) {
                this.zzaCC.put(entry.getKey(), zzbboVar);
            }
            z7 = z2;
            z5 = z3;
            z6 = z4;
        }
        this.zzaCI = (!z7 || z5 || z6) ? false : true;
        this.zzaAN = zzbdb.zzqk();
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final boolean zza(zzbbo<?> zzbboVar, ConnectionResult connectionResult) {
        return !connectionResult.isSuccess() && !connectionResult.hasResolution() && this.zzaCD.get(zzbboVar.zzpg()).booleanValue() && zzbboVar.zzpJ().zzpe() && this.zzaCF.isUserResolvableError(connectionResult.getErrorCode());
    }

    static /* synthetic */ boolean zza(zzbbp zzbbpVar, boolean z) {
        zzbbpVar.zzaCK = false;
        return false;
    }

    @Nullable
    private final ConnectionResult zzb(@NonNull Api.zzc<?> zzcVar) {
        this.zzaCv.lock();
        try {
            zzbbo<?> zzbboVar = this.zzaCB.get(zzcVar);
            if (this.zzaCL == null || zzbboVar == null) {
                return null;
            }
            return this.zzaCL.get(zzbboVar.zzph());
        } finally {
            this.zzaCv.unlock();
        }
    }

    private final <T extends zzbay<? extends Result, ? extends Api.zzb>> boolean zzg(@NonNull T t) {
        Api.zzc<?> zzcVarZzpd = t.zzpd();
        ConnectionResult connectionResultZzb = zzb(zzcVarZzpd);
        if (connectionResultZzb == null || connectionResultZzb.getErrorCode() != 4) {
            return false;
        }
        t.zzr(new Status(4, null, this.zzaAN.zza(this.zzaCB.get(zzcVarZzpd).zzph(), System.identityHashCode(this.zzaCE))));
        return true;
    }

    private final boolean zzpK() {
        this.zzaCv.lock();
        try {
            if (!this.zzaCK || !this.zzaCH) {
                return false;
            }
            Iterator<Api.zzc<?>> it = this.zzaCC.keySet().iterator();
            while (it.hasNext()) {
                ConnectionResult connectionResultZzb = zzb(it.next());
                if (connectionResultZzb == null || !connectionResultZzb.isSuccess()) {
                    return false;
                }
            }
            return true;
        } finally {
            this.zzaCv.unlock();
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzpL() {
        if (this.zzaCA == null) {
            this.zzaCE.zzaDG = Collections.emptySet();
            return;
        }
        HashSet hashSet = new HashSet(this.zzaCA.zzrn());
        Map<Api<?>, com.google.android.gms.common.internal.zzr> mapZzrp = this.zzaCA.zzrp();
        for (Api<?> api : mapZzrp.keySet()) {
            ConnectionResult connectionResult = getConnectionResult(api);
            if (connectionResult != null && connectionResult.isSuccess()) {
                hashSet.addAll(mapZzrp.get(api).zzame);
            }
        }
        this.zzaCE.zzaDG = hashSet;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzpM() {
        while (!this.zzaCJ.isEmpty()) {
            zze(this.zzaCJ.remove());
        }
        this.zzaCE.zzm(null);
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Nullable
    public final ConnectionResult zzpN() {
        ConnectionResult connectionResult;
        int i;
        int i2 = 0;
        ConnectionResult connectionResult2 = null;
        int i3 = 0;
        ConnectionResult connectionResult3 = null;
        for (zzbbo<?> zzbboVar : this.zzaCB.values()) {
            Api<?> apiZzpg = zzbboVar.zzpg();
            ConnectionResult connectionResult4 = this.zzaCL.get(zzbboVar.zzph());
            if (!connectionResult4.isSuccess() && (!this.zzaCD.get(apiZzpg).booleanValue() || connectionResult4.hasResolution() || this.zzaCF.isUserResolvableError(connectionResult4.getErrorCode()))) {
                if (connectionResult4.getErrorCode() == 4 && this.zzaCH) {
                    int priority = apiZzpg.zzpb().getPriority();
                    if (connectionResult2 == null || i2 > priority) {
                        i2 = priority;
                        connectionResult2 = connectionResult4;
                    }
                } else {
                    int priority2 = apiZzpg.zzpb().getPriority();
                    if (connectionResult3 == null || i3 > priority2) {
                        connectionResult = connectionResult4;
                        i = priority2;
                    } else {
                        i = i3;
                        connectionResult = connectionResult3;
                    }
                    i3 = i;
                    connectionResult3 = connectionResult;
                }
            }
        }
        return (connectionResult3 == null || connectionResult2 == null || i3 <= i2) ? connectionResult3 : connectionResult2;
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final ConnectionResult blockingConnect() {
        connect();
        while (isConnecting()) {
            try {
                this.zzaCG.await();
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                return new ConnectionResult(15, null);
            }
        }
        if (isConnected()) {
            return ConnectionResult.zzazX;
        }
        return this.zzaCO != null ? this.zzaCO : new ConnectionResult(13, null);
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
                nanos = this.zzaCG.awaitNanos(nanos);
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
        return this.zzaCO != null ? this.zzaCO : new ConnectionResult(13, null);
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final void connect() {
        this.zzaCv.lock();
        try {
            if (this.zzaCK) {
                return;
            }
            this.zzaCK = true;
            this.zzaCL = null;
            this.zzaCM = null;
            this.zzaCN = null;
            this.zzaCO = null;
            this.zzaAN.zzps();
            this.zzaAN.zza(this.zzaCB.values()).addOnCompleteListener(new zzbgv(this.zzrM), new zzbbr(this));
        } finally {
            this.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final void disconnect() {
        this.zzaCv.lock();
        try {
            this.zzaCK = false;
            this.zzaCL = null;
            this.zzaCM = null;
            if (this.zzaCN != null) {
                this.zzaCN.cancel();
                this.zzaCN = null;
            }
            this.zzaCO = null;
            while (!this.zzaCJ.isEmpty()) {
                zzbay<?, ?> zzbayVarRemove = this.zzaCJ.remove();
                zzbayVarRemove.zza((zzbex) null);
                zzbayVarRemove.cancel();
            }
            this.zzaCG.signalAll();
        } finally {
            this.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
    }

    @Override // com.google.android.gms.internal.zzbdp
    @Nullable
    public final ConnectionResult getConnectionResult(@NonNull Api<?> api) {
        return zzb(api.zzpd());
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final boolean isConnected() {
        this.zzaCv.lock();
        try {
            return this.zzaCL != null && this.zzaCO == null;
        } finally {
            this.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final boolean isConnecting() {
        this.zzaCv.lock();
        try {
            return this.zzaCL == null && this.zzaCK;
        } finally {
            this.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final boolean zza(zzbei zzbeiVar) {
        Lock lock;
        this.zzaCv.lock();
        try {
            if (!this.zzaCK || zzpK()) {
                return false;
            }
            this.zzaAN.zzps();
            this.zzaCN = new zzbbs(this, zzbeiVar);
            this.zzaAN.zza(this.zzaCC.values()).addOnCompleteListener(new zzbgv(this.zzrM), this.zzaCN);
            return true;
        } finally {
            this.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final <A extends Api.zzb, R extends Result, T extends zzbay<R, A>> T zzd(@NonNull T t) {
        if (this.zzaCH && zzg(t)) {
            return t;
        }
        if (isConnected()) {
            this.zzaCE.zzaDL.zzb(t);
            return (T) this.zzaCB.get(t.zzpd()).zza(t);
        }
        this.zzaCJ.add(t);
        return t;
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final <A extends Api.zzb, T extends zzbay<? extends Result, A>> T zze(@NonNull T t) {
        Api.zzc<A> zzcVarZzpd = t.zzpd();
        if (this.zzaCH && zzg(t)) {
            return t;
        }
        this.zzaCE.zzaDL.zzb(t);
        return (T) this.zzaCB.get(zzcVarZzpd).zzb(t);
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final void zzpE() {
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final void zzpl() {
        this.zzaCv.lock();
        try {
            this.zzaAN.zzpl();
            if (this.zzaCN != null) {
                this.zzaCN.cancel();
                this.zzaCN = null;
            }
            if (this.zzaCM == null) {
                this.zzaCM = new ArrayMap(this.zzaCC.size());
            }
            ConnectionResult connectionResult = new ConnectionResult(4);
            Iterator<zzbbo<?>> it = this.zzaCC.values().iterator();
            while (it.hasNext()) {
                this.zzaCM.put(it.next().zzph(), connectionResult);
            }
            if (this.zzaCL != null) {
                this.zzaCL.putAll(this.zzaCM);
            }
        } finally {
            this.zzaCv.unlock();
        }
    }
}
