package com.google.android.gms.internal;

import android.app.PendingIntent;
import android.content.Context;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.util.ArrayMap;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.zzbo;
import java.io.FileDescriptor;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;
import java.util.WeakHashMap;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.locks.Lock;

/* JADX INFO: loaded from: classes.dex */
final class zzbbk implements zzbdp {
    private final Context mContext;
    private final zzbcp zzaCl;
    private final zzbcx zzaCm;
    private final zzbcx zzaCn;
    private final Map<Api.zzc<?>, zzbcx> zzaCo;
    private final Api.zze zzaCq;
    private Bundle zzaCr;
    private final Lock zzaCv;
    private final Looper zzrM;
    private final Set<zzbei> zzaCp = Collections.newSetFromMap(new WeakHashMap());
    private ConnectionResult zzaCs = null;
    private ConnectionResult zzaCt = null;
    private boolean zzaCu = false;
    private int zzaCw = 0;

    private zzbbk(Context context, zzbcp zzbcpVar, Lock lock, Looper looper, com.google.android.gms.common.zze zzeVar, Map<Api.zzc<?>, Api.zze> map, Map<Api.zzc<?>, Api.zze> map2, com.google.android.gms.common.internal.zzq zzqVar, Api.zza<? extends zzctk, zzctl> zzaVar, Api.zze zzeVar2, ArrayList<zzbbi> arrayList, ArrayList<zzbbi> arrayList2, Map<Api<?>, Boolean> map3, Map<Api<?>, Boolean> map4) {
        this.mContext = context;
        this.zzaCl = zzbcpVar;
        this.zzaCv = lock;
        this.zzrM = looper;
        this.zzaCq = zzeVar2;
        this.zzaCm = new zzbcx(context, this.zzaCl, lock, looper, zzeVar, map2, null, map4, null, arrayList2, new zzbbm(this, null));
        this.zzaCn = new zzbcx(context, this.zzaCl, lock, looper, zzeVar, map, zzqVar, map3, zzaVar, arrayList, new zzbbn(this, null));
        ArrayMap arrayMap = new ArrayMap();
        Iterator<Api.zzc<?>> it = map2.keySet().iterator();
        while (it.hasNext()) {
            arrayMap.put(it.next(), this.zzaCm);
        }
        Iterator<Api.zzc<?>> it2 = map.keySet().iterator();
        while (it2.hasNext()) {
            arrayMap.put(it2.next(), this.zzaCn);
        }
        this.zzaCo = Collections.unmodifiableMap(arrayMap);
    }

    public static zzbbk zza(Context context, zzbcp zzbcpVar, Lock lock, Looper looper, com.google.android.gms.common.zze zzeVar, Map<Api.zzc<?>, Api.zze> map, com.google.android.gms.common.internal.zzq zzqVar, Map<Api<?>, Boolean> map2, Api.zza<? extends zzctk, zzctl> zzaVar, ArrayList<zzbbi> arrayList) {
        Api.zze zzeVar2 = null;
        ArrayMap arrayMap = new ArrayMap();
        ArrayMap arrayMap2 = new ArrayMap();
        for (Map.Entry<Api.zzc<?>, Api.zze> entry : map.entrySet()) {
            Api.zze value = entry.getValue();
            if (value.zzmG()) {
                zzeVar2 = value;
            }
            if (value.zzmv()) {
                arrayMap.put(entry.getKey(), value);
            } else {
                arrayMap2.put(entry.getKey(), value);
            }
        }
        zzbo.zza(!arrayMap.isEmpty(), "CompositeGoogleApiClient should not be used without any APIs that require sign-in.");
        ArrayMap arrayMap3 = new ArrayMap();
        ArrayMap arrayMap4 = new ArrayMap();
        for (Api<?> api : map2.keySet()) {
            Api.zzc<?> zzcVarZzpd = api.zzpd();
            if (arrayMap.containsKey(zzcVarZzpd)) {
                arrayMap3.put(api, map2.get(api));
            } else {
                if (!arrayMap2.containsKey(zzcVarZzpd)) {
                    throw new IllegalStateException("Each API in the isOptionalMap must have a corresponding client in the clients map.");
                }
                arrayMap4.put(api, map2.get(api));
            }
        }
        ArrayList arrayList2 = new ArrayList();
        ArrayList arrayList3 = new ArrayList();
        ArrayList<zzbbi> arrayList4 = arrayList;
        int size = arrayList4.size();
        int i = 0;
        while (i < size) {
            zzbbi zzbbiVar = arrayList4.get(i);
            i++;
            zzbbi zzbbiVar2 = zzbbiVar;
            if (arrayMap3.containsKey(zzbbiVar2.zzayW)) {
                arrayList2.add(zzbbiVar2);
            } else {
                if (!arrayMap4.containsKey(zzbbiVar2.zzayW)) {
                    throw new IllegalStateException("Each ClientCallbacks must have a corresponding API in the isOptionalMap");
                }
                arrayList3.add(zzbbiVar2);
            }
        }
        return new zzbbk(context, zzbcpVar, lock, looper, zzeVar, arrayMap, arrayMap2, zzqVar, zzaVar, zzeVar2, arrayList2, arrayList3, arrayMap3, arrayMap4);
    }

    private final void zza(ConnectionResult connectionResult) {
        switch (this.zzaCw) {
            case 2:
                this.zzaCl.zzc(connectionResult);
            case 1:
                zzpG();
                break;
            default:
                Log.wtf("CompositeGAC", "Attempted to call failure callbacks in CONNECTION_MODE_NONE. Callbacks should be disabled via GmsClientSupervisor", new Exception());
                break;
        }
        this.zzaCw = 0;
    }

    private static boolean zzb(ConnectionResult connectionResult) {
        return connectionResult != null && connectionResult.isSuccess();
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzd(int i, boolean z) {
        this.zzaCl.zze(i, z);
        this.zzaCt = null;
        this.zzaCs = null;
    }

    private final boolean zzf(zzbay<? extends Result, ? extends Api.zzb> zzbayVar) {
        Object objZzpd = zzbayVar.zzpd();
        zzbo.zzb(this.zzaCo.containsKey(objZzpd), "GoogleApiClient is not configured to use the API required for this call.");
        return this.zzaCo.get(objZzpd).equals(this.zzaCn);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzl(Bundle bundle) {
        if (this.zzaCr == null) {
            this.zzaCr = bundle;
        } else if (bundle != null) {
            this.zzaCr.putAll(bundle);
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzpF() {
        if (!zzb(this.zzaCs)) {
            if (this.zzaCs != null && zzb(this.zzaCt)) {
                this.zzaCn.disconnect();
                zza(this.zzaCs);
                return;
            } else {
                if (this.zzaCs == null || this.zzaCt == null) {
                    return;
                }
                ConnectionResult connectionResult = this.zzaCs;
                if (this.zzaCn.zzaDX < this.zzaCm.zzaDX) {
                    connectionResult = this.zzaCt;
                }
                zza(connectionResult);
                return;
            }
        }
        if (zzb(this.zzaCt) || zzpH()) {
            switch (this.zzaCw) {
                case 2:
                    this.zzaCl.zzm(this.zzaCr);
                case 1:
                    zzpG();
                    break;
                default:
                    Log.wtf("CompositeGAC", "Attempted to call success callbacks in CONNECTION_MODE_NONE. Callbacks should be disabled via GmsClientSupervisor", new AssertionError());
                    break;
            }
            this.zzaCw = 0;
            return;
        }
        if (this.zzaCt != null) {
            if (this.zzaCw == 1) {
                zzpG();
            } else {
                zza(this.zzaCt);
                this.zzaCm.disconnect();
            }
        }
    }

    private final void zzpG() {
        Iterator<zzbei> it = this.zzaCp.iterator();
        while (it.hasNext()) {
            it.next().zzmF();
        }
        this.zzaCp.clear();
    }

    private final boolean zzpH() {
        return this.zzaCt != null && this.zzaCt.getErrorCode() == 4;
    }

    @Nullable
    private final PendingIntent zzpI() {
        if (this.zzaCq == null) {
            return null;
        }
        return PendingIntent.getActivity(this.mContext, System.identityHashCode(this.zzaCl), this.zzaCq.zzmH(), 134217728);
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final ConnectionResult blockingConnect() {
        throw new UnsupportedOperationException();
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final ConnectionResult blockingConnect(long j, @NonNull TimeUnit timeUnit) {
        throw new UnsupportedOperationException();
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final void connect() {
        this.zzaCw = 2;
        this.zzaCu = false;
        this.zzaCt = null;
        this.zzaCs = null;
        this.zzaCm.connect();
        this.zzaCn.connect();
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final void disconnect() {
        this.zzaCt = null;
        this.zzaCs = null;
        this.zzaCw = 0;
        this.zzaCm.disconnect();
        this.zzaCn.disconnect();
        zzpG();
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
        printWriter.append((CharSequence) str).append("authClient").println(":");
        this.zzaCn.dump(String.valueOf(str).concat("  "), fileDescriptor, printWriter, strArr);
        printWriter.append((CharSequence) str).append("anonClient").println(":");
        this.zzaCm.dump(String.valueOf(str).concat("  "), fileDescriptor, printWriter, strArr);
    }

    @Override // com.google.android.gms.internal.zzbdp
    @Nullable
    public final ConnectionResult getConnectionResult(@NonNull Api<?> api) {
        if (this.zzaCo.get(api.zzpd()).equals(this.zzaCn)) {
            return zzpH() ? new ConnectionResult(4, zzpI()) : this.zzaCn.getConnectionResult(api);
        }
        return this.zzaCm.getConnectionResult(api);
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final boolean isConnected() {
        boolean z = true;
        this.zzaCv.lock();
        try {
            if (!this.zzaCm.isConnected() || (!this.zzaCn.isConnected() && !zzpH() && this.zzaCw != 1)) {
                z = false;
            }
            return z;
        } finally {
            this.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final boolean isConnecting() {
        this.zzaCv.lock();
        try {
            return this.zzaCw == 2;
        } finally {
            this.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final boolean zza(zzbei zzbeiVar) {
        this.zzaCv.lock();
        try {
            if ((!isConnecting() && !isConnected()) || this.zzaCn.isConnected()) {
                return false;
            }
            this.zzaCp.add(zzbeiVar);
            if (this.zzaCw == 0) {
                this.zzaCw = 1;
            }
            this.zzaCt = null;
            this.zzaCn.connect();
            return true;
        } finally {
            this.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final <A extends Api.zzb, R extends Result, T extends zzbay<R, A>> T zzd(@NonNull T t) {
        if (!zzf((zzbay<? extends Result, ? extends Api.zzb>) t)) {
            return (T) this.zzaCm.zzd(t);
        }
        if (!zzpH()) {
            return (T) this.zzaCn.zzd(t);
        }
        t.zzr(new Status(4, null, zzpI()));
        return t;
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final <A extends Api.zzb, T extends zzbay<? extends Result, A>> T zze(@NonNull T t) {
        if (!zzf((zzbay<? extends Result, ? extends Api.zzb>) t)) {
            return (T) this.zzaCm.zze(t);
        }
        if (!zzpH()) {
            return (T) this.zzaCn.zze(t);
        }
        t.zzr(new Status(4, null, zzpI()));
        return t;
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final void zzpE() {
        this.zzaCm.zzpE();
        this.zzaCn.zzpE();
    }

    @Override // com.google.android.gms.internal.zzbdp
    public final void zzpl() {
        this.zzaCv.lock();
        try {
            boolean zIsConnecting = isConnecting();
            this.zzaCn.disconnect();
            this.zzaCt = new ConnectionResult(4);
            if (zIsConnecting) {
                new Handler(this.zzrM).post(new zzbbl(this));
            } else {
                zzpG();
            }
        } finally {
            this.zzaCv.unlock();
        }
    }
}
