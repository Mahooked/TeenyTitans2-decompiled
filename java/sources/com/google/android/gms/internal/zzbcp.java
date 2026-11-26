package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import android.os.Looper;
import android.support.annotation.NonNull;
import android.support.v4.app.FragmentActivity;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.zzbo;
import java.io.FileDescriptor;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Queue;
import java.util.Set;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicReference;
import java.util.concurrent.locks.Lock;

/* JADX INFO: loaded from: classes.dex */
public final class zzbcp extends GoogleApiClient implements zzbdq {
    private final Context mContext;
    private final int zzaBb;
    private final GoogleApiAvailability zzaBd;
    private Api.zza<? extends zzctk, zzctl> zzaBe;
    private com.google.android.gms.common.internal.zzq zzaCA;
    private Map<Api<?>, Boolean> zzaCD;
    private final Lock zzaCv;
    private volatile boolean zzaDA;
    private final zzbcu zzaDD;
    private zzbdk zzaDE;
    final Map<Api.zzc<?>, Api.zze> zzaDF;
    private final ArrayList<zzbbi> zzaDI;
    private Integer zzaDJ;
    final zzbev zzaDL;
    private final com.google.android.gms.common.internal.zzac zzaDy;
    private final Looper zzrM;
    private zzbdp zzaDz = null;
    final Queue<zzbay<?, ?>> zzaCJ = new LinkedList();
    private long zzaDB = 120000;
    private long zzaDC = 5000;
    Set<Scope> zzaDG = new HashSet();
    private final zzbea zzaDH = new zzbea();
    Set<zzbes> zzaDK = null;
    private final com.google.android.gms.common.internal.zzad zzaDM = new zzbcq(this);
    private boolean zzaBh = false;

    public zzbcp(Context context, Lock lock, Looper looper, com.google.android.gms.common.internal.zzq zzqVar, GoogleApiAvailability googleApiAvailability, Api.zza<? extends zzctk, zzctl> zzaVar, Map<Api<?>, Boolean> map, List<GoogleApiClient.ConnectionCallbacks> list, List<GoogleApiClient.OnConnectionFailedListener> list2, Map<Api.zzc<?>, Api.zze> map2, int i, int i2, ArrayList<zzbbi> arrayList, boolean z) {
        this.zzaDJ = null;
        this.mContext = context;
        this.zzaCv = lock;
        this.zzaDy = new com.google.android.gms.common.internal.zzac(looper, this.zzaDM);
        this.zzrM = looper;
        this.zzaDD = new zzbcu(this, looper);
        this.zzaBd = googleApiAvailability;
        this.zzaBb = i;
        if (this.zzaBb >= 0) {
            this.zzaDJ = Integer.valueOf(i2);
        }
        this.zzaCD = map;
        this.zzaDF = map2;
        this.zzaDI = arrayList;
        this.zzaDL = new zzbev(this.zzaDF);
        Iterator<GoogleApiClient.ConnectionCallbacks> it = list.iterator();
        while (it.hasNext()) {
            this.zzaDy.registerConnectionCallbacks(it.next());
        }
        Iterator<GoogleApiClient.OnConnectionFailedListener> it2 = list2.iterator();
        while (it2.hasNext()) {
            this.zzaDy.registerConnectionFailedListener(it2.next());
        }
        this.zzaCA = zzqVar;
        this.zzaBe = zzaVar;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void resume() {
        this.zzaCv.lock();
        try {
            if (this.zzaDA) {
                zzqc();
            }
        } finally {
            this.zzaCv.unlock();
        }
    }

    public static int zza(Iterable<Api.zze> iterable, boolean z) {
        boolean z2 = false;
        boolean z3 = false;
        for (Api.zze zzeVar : iterable) {
            if (zzeVar.zzmv()) {
                z3 = true;
            }
            z2 = zzeVar.zzmG() ? true : z2;
        }
        if (z3) {
            return (z2 && z) ? 2 : 1;
        }
        return 3;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zza(GoogleApiClient googleApiClient, zzben zzbenVar, boolean z) {
        zzbfo.zzaIy.zzd(googleApiClient).setResultCallback(new zzbct(this, zzbenVar, z, googleApiClient));
    }

    private final void zzap(int i) {
        if (this.zzaDJ == null) {
            this.zzaDJ = Integer.valueOf(i);
        } else if (this.zzaDJ.intValue() != i) {
            String strValueOf = String.valueOf(zzaq(i));
            String strValueOf2 = String.valueOf(zzaq(this.zzaDJ.intValue()));
            throw new IllegalStateException(new StringBuilder(String.valueOf(strValueOf).length() + 51 + String.valueOf(strValueOf2).length()).append("Cannot use sign-in mode: ").append(strValueOf).append(". Mode was already set to ").append(strValueOf2).toString());
        }
        if (this.zzaDz != null) {
            return;
        }
        boolean z = false;
        boolean z2 = false;
        for (Api.zze zzeVar : this.zzaDF.values()) {
            if (zzeVar.zzmv()) {
                z2 = true;
            }
            z = zzeVar.zzmG() ? true : z;
        }
        switch (this.zzaDJ.intValue()) {
            case 1:
                if (!z2) {
                    throw new IllegalStateException("SIGN_IN_MODE_REQUIRED cannot be used on a GoogleApiClient that does not contain any authenticated APIs. Use connect() instead.");
                }
                if (z) {
                    throw new IllegalStateException("Cannot use SIGN_IN_MODE_REQUIRED with GOOGLE_SIGN_IN_API. Use connect(SIGN_IN_MODE_OPTIONAL) instead.");
                }
                break;
            case 2:
                if (z2) {
                    if (this.zzaBh) {
                        this.zzaDz = new zzbbp(this.mContext, this.zzaCv, this.zzrM, this.zzaBd, this.zzaDF, this.zzaCA, this.zzaCD, this.zzaBe, this.zzaDI, this, true);
                        return;
                    } else {
                        this.zzaDz = zzbbk.zza(this.mContext, this, this.zzaCv, this.zzrM, this.zzaBd, this.zzaDF, this.zzaCA, this.zzaCD, this.zzaBe, this.zzaDI);
                        return;
                    }
                }
                break;
        }
        if (!this.zzaBh || z) {
            this.zzaDz = new zzbcx(this.mContext, this, this.zzaCv, this.zzrM, this.zzaBd, this.zzaDF, this.zzaCA, this.zzaCD, this.zzaBe, this.zzaDI, this);
        } else {
            this.zzaDz = new zzbbp(this.mContext, this.zzaCv, this.zzrM, this.zzaBd, this.zzaDF, this.zzaCA, this.zzaCD, this.zzaBe, this.zzaDI, this, false);
        }
    }

    private static String zzaq(int i) {
        switch (i) {
            case 1:
                return "SIGN_IN_MODE_REQUIRED";
            case 2:
                return "SIGN_IN_MODE_OPTIONAL";
            case 3:
                return "SIGN_IN_MODE_NONE";
            default:
                return "UNKNOWN";
        }
    }

    private final void zzqc() {
        this.zzaDy.zzrA();
        this.zzaDz.connect();
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzqd() {
        this.zzaCv.lock();
        try {
            if (zzqe()) {
                zzqc();
            }
        } finally {
            this.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final ConnectionResult blockingConnect() {
        zzbo.zza(Looper.myLooper() != Looper.getMainLooper(), "blockingConnect must not be called on the UI thread");
        this.zzaCv.lock();
        try {
            if (this.zzaBb >= 0) {
                zzbo.zza(this.zzaDJ != null, "Sign-in mode should have been set explicitly by auto-manage.");
            } else if (this.zzaDJ == null) {
                this.zzaDJ = Integer.valueOf(zza(this.zzaDF.values(), false));
            } else if (this.zzaDJ.intValue() == 2) {
                throw new IllegalStateException("Cannot call blockingConnect() when sign-in mode is set to SIGN_IN_MODE_OPTIONAL. Call connect(SIGN_IN_MODE_OPTIONAL) instead.");
            }
            zzap(this.zzaDJ.intValue());
            this.zzaDy.zzrA();
            ConnectionResult connectionResultBlockingConnect = this.zzaDz.blockingConnect();
            this.zzaCv.unlock();
            return connectionResultBlockingConnect;
        } catch (Throwable th) {
            this.zzaCv.unlock();
            throw th;
        }
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final ConnectionResult blockingConnect(long j, @NonNull TimeUnit timeUnit) {
        zzbo.zza(Looper.myLooper() != Looper.getMainLooper(), "blockingConnect must not be called on the UI thread");
        zzbo.zzb(timeUnit, "TimeUnit must not be null");
        this.zzaCv.lock();
        try {
            if (this.zzaDJ == null) {
                this.zzaDJ = Integer.valueOf(zza(this.zzaDF.values(), false));
            } else if (this.zzaDJ.intValue() == 2) {
                throw new IllegalStateException("Cannot call blockingConnect() when sign-in mode is set to SIGN_IN_MODE_OPTIONAL. Call connect(SIGN_IN_MODE_OPTIONAL) instead.");
            }
            zzap(this.zzaDJ.intValue());
            this.zzaDy.zzrA();
            ConnectionResult connectionResultBlockingConnect = this.zzaDz.blockingConnect(j, timeUnit);
            this.zzaCv.unlock();
            return connectionResultBlockingConnect;
        } catch (Throwable th) {
            this.zzaCv.unlock();
            throw th;
        }
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final PendingResult<Status> clearDefaultAccountAndReconnect() {
        zzbo.zza(isConnected(), "GoogleApiClient is not connected yet.");
        zzbo.zza(this.zzaDJ.intValue() != 2, "Cannot use clearDefaultAccountAndReconnect with GOOGLE_SIGN_IN_API");
        zzben zzbenVar = new zzben(this);
        if (this.zzaDF.containsKey(zzbfo.zzajR)) {
            zza(this, zzbenVar, false);
        } else {
            AtomicReference atomicReference = new AtomicReference();
            GoogleApiClient googleApiClientBuild = new GoogleApiClient.Builder(this.mContext).addApi(zzbfo.API).addConnectionCallbacks(new zzbcr(this, atomicReference, zzbenVar)).addOnConnectionFailedListener(new zzbcs(this, zzbenVar)).setHandler(this.zzaDD).build();
            atomicReference.set(googleApiClientBuild);
            googleApiClientBuild.connect();
        }
        return zzbenVar;
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final void connect() {
        this.zzaCv.lock();
        try {
            if (this.zzaBb >= 0) {
                zzbo.zza(this.zzaDJ != null, "Sign-in mode should have been set explicitly by auto-manage.");
            } else if (this.zzaDJ == null) {
                this.zzaDJ = Integer.valueOf(zza(this.zzaDF.values(), false));
            } else if (this.zzaDJ.intValue() == 2) {
                throw new IllegalStateException("Cannot call connect() when SignInMode is set to SIGN_IN_MODE_OPTIONAL. Call connect(SIGN_IN_MODE_OPTIONAL) instead.");
            }
            connect(this.zzaDJ.intValue());
            this.zzaCv.unlock();
        } catch (Throwable th) {
            this.zzaCv.unlock();
            throw th;
        }
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final void connect(int i) {
        boolean z = true;
        this.zzaCv.lock();
        if (i != 3 && i != 1 && i != 2) {
            z = false;
        }
        try {
            zzbo.zzb(z, new StringBuilder(33).append("Illegal sign-in mode: ").append(i).toString());
            zzap(i);
            zzqc();
        } finally {
            this.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final void disconnect() {
        this.zzaCv.lock();
        try {
            this.zzaDL.release();
            if (this.zzaDz != null) {
                this.zzaDz.disconnect();
            }
            this.zzaDH.release();
            for (zzbay<?, ?> zzbayVar : this.zzaCJ) {
                zzbayVar.zza((zzbex) null);
                zzbayVar.cancel();
            }
            this.zzaCJ.clear();
            if (this.zzaDz == null) {
                return;
            }
            zzqe();
            this.zzaDy.zzrz();
        } finally {
            this.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
        printWriter.append((CharSequence) str).append("mContext=").println(this.mContext);
        printWriter.append((CharSequence) str).append("mResuming=").print(this.zzaDA);
        printWriter.append(" mWorkQueue.size()=").print(this.zzaCJ.size());
        printWriter.append(" mUnconsumedApiCalls.size()=").println(this.zzaDL.zzaFl.size());
        if (this.zzaDz != null) {
            this.zzaDz.dump(str, fileDescriptor, printWriter, strArr);
        }
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    @NonNull
    public final ConnectionResult getConnectionResult(@NonNull Api<?> api) {
        this.zzaCv.lock();
        try {
            if (!isConnected() && !this.zzaDA) {
                throw new IllegalStateException("Cannot invoke getConnectionResult unless GoogleApiClient is connected");
            }
            if (!this.zzaDF.containsKey(api.zzpd())) {
                throw new IllegalArgumentException(String.valueOf(api.getName()).concat(" was never registered with GoogleApiClient"));
            }
            ConnectionResult connectionResult = this.zzaDz.getConnectionResult(api);
            if (connectionResult != null) {
                this.zzaCv.unlock();
            } else if (this.zzaDA) {
                connectionResult = ConnectionResult.zzazX;
                this.zzaCv.unlock();
            } else {
                Log.w("GoogleApiClientImpl", zzqg());
                Log.wtf("GoogleApiClientImpl", String.valueOf(api.getName()).concat(" requested in getConnectionResult is not connected but is not present in the failed  connections map"), new Exception());
                connectionResult = new ConnectionResult(8, null);
                this.zzaCv.unlock();
            }
            return connectionResult;
        } catch (Throwable th) {
            this.zzaCv.unlock();
            throw th;
        }
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final Context getContext() {
        return this.mContext;
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final Looper getLooper() {
        return this.zzrM;
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final boolean hasConnectedApi(@NonNull Api<?> api) {
        if (!isConnected()) {
            return false;
        }
        Api.zze zzeVar = this.zzaDF.get(api.zzpd());
        return zzeVar != null && zzeVar.isConnected();
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final boolean isConnected() {
        return this.zzaDz != null && this.zzaDz.isConnected();
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final boolean isConnecting() {
        return this.zzaDz != null && this.zzaDz.isConnecting();
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final boolean isConnectionCallbacksRegistered(@NonNull GoogleApiClient.ConnectionCallbacks connectionCallbacks) {
        return this.zzaDy.isConnectionCallbacksRegistered(connectionCallbacks);
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final boolean isConnectionFailedListenerRegistered(@NonNull GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener) {
        return this.zzaDy.isConnectionFailedListenerRegistered(onConnectionFailedListener);
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final void reconnect() {
        disconnect();
        connect();
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final void registerConnectionCallbacks(@NonNull GoogleApiClient.ConnectionCallbacks connectionCallbacks) {
        this.zzaDy.registerConnectionCallbacks(connectionCallbacks);
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final void registerConnectionFailedListener(@NonNull GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener) {
        this.zzaDy.registerConnectionFailedListener(onConnectionFailedListener);
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final void stopAutoManage(@NonNull FragmentActivity fragmentActivity) {
        zzbdr zzbdrVar = new zzbdr(fragmentActivity);
        if (this.zzaBb < 0) {
            throw new IllegalStateException("Called stopAutoManage but automatic lifecycle management is not enabled.");
        }
        zzbau.zza(zzbdrVar).zzal(this.zzaBb);
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final void unregisterConnectionCallbacks(@NonNull GoogleApiClient.ConnectionCallbacks connectionCallbacks) {
        this.zzaDy.unregisterConnectionCallbacks(connectionCallbacks);
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final void unregisterConnectionFailedListener(@NonNull GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener) {
        this.zzaDy.unregisterConnectionFailedListener(onConnectionFailedListener);
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    @NonNull
    public final <C extends Api.zze> C zza(@NonNull Api.zzc<C> zzcVar) {
        C c = (C) this.zzaDF.get(zzcVar);
        zzbo.zzb(c, "Appropriate Api was not requested.");
        return c;
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final void zza(zzbes zzbesVar) {
        this.zzaCv.lock();
        try {
            if (this.zzaDK == null) {
                this.zzaDK = new HashSet();
            }
            this.zzaDK.add(zzbesVar);
        } finally {
            this.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final boolean zza(@NonNull Api<?> api) {
        return this.zzaDF.containsKey(api.zzpd());
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final boolean zza(zzbei zzbeiVar) {
        return this.zzaDz != null && this.zzaDz.zza(zzbeiVar);
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final void zzb(zzbes zzbesVar) {
        this.zzaCv.lock();
        try {
            if (this.zzaDK == null) {
                Log.wtf("GoogleApiClientImpl", "Attempted to remove pending transform when no transforms are registered.", new Exception());
            } else if (!this.zzaDK.remove(zzbesVar)) {
                Log.wtf("GoogleApiClientImpl", "Failed to remove pending transform - this may lead to memory leaks!", new Exception());
            } else if (!zzqf()) {
                this.zzaDz.zzpE();
            }
        } finally {
            this.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.internal.zzbdq
    public final void zzc(ConnectionResult connectionResult) {
        if (!com.google.android.gms.common.zze.zze(this.mContext, connectionResult.getErrorCode())) {
            zzqe();
        }
        if (this.zzaDA) {
            return;
        }
        this.zzaDy.zzk(connectionResult);
        this.zzaDy.zzrz();
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final <A extends Api.zzb, R extends Result, T extends zzbay<R, A>> T zzd(@NonNull T t) {
        zzbo.zzb(t.zzpd() != null, "This task can not be enqueued (it's probably a Batch or malformed)");
        boolean zContainsKey = this.zzaDF.containsKey(t.zzpd());
        String name = t.zzpg() != null ? t.zzpg().getName() : "the API";
        zzbo.zzb(zContainsKey, new StringBuilder(String.valueOf(name).length() + 65).append("GoogleApiClient is not configured to use ").append(name).append(" required for this call.").toString());
        this.zzaCv.lock();
        try {
            if (this.zzaDz == null) {
                this.zzaCJ.add(t);
            } else {
                t = (T) this.zzaDz.zzd(t);
            }
            return t;
        } finally {
            this.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final <A extends Api.zzb, T extends zzbay<? extends Result, A>> T zze(@NonNull T t) {
        zzbo.zzb(t.zzpd() != null, "This task can not be executed (it's probably a Batch or malformed)");
        boolean zContainsKey = this.zzaDF.containsKey(t.zzpd());
        String name = t.zzpg() != null ? t.zzpg().getName() : "the API";
        zzbo.zzb(zContainsKey, new StringBuilder(String.valueOf(name).length() + 65).append("GoogleApiClient is not configured to use ").append(name).append(" required for this call.").toString());
        this.zzaCv.lock();
        try {
            if (this.zzaDz == null) {
                throw new IllegalStateException("GoogleApiClient is not connected yet.");
            }
            if (this.zzaDA) {
                this.zzaCJ.add(t);
                while (!this.zzaCJ.isEmpty()) {
                    zzbay<?, ?> zzbayVarRemove = this.zzaCJ.remove();
                    this.zzaDL.zzb(zzbayVarRemove);
                    zzbayVarRemove.zzr(Status.zzaBo);
                }
                this.zzaCv.unlock();
            } else {
                t = (T) this.zzaDz.zze(t);
                this.zzaCv.unlock();
            }
            return t;
        } catch (Throwable th) {
            this.zzaCv.unlock();
            throw th;
        }
    }

    @Override // com.google.android.gms.internal.zzbdq
    public final void zze(int i, boolean z) {
        if (i == 1 && !z && !this.zzaDA) {
            this.zzaDA = true;
            if (this.zzaDE == null) {
                this.zzaDE = GoogleApiAvailability.zza(this.mContext.getApplicationContext(), new zzbcv(this));
            }
            this.zzaDD.sendMessageDelayed(this.zzaDD.obtainMessage(1), this.zzaDB);
            this.zzaDD.sendMessageDelayed(this.zzaDD.obtainMessage(2), this.zzaDC);
        }
        this.zzaDL.zzqM();
        this.zzaDy.zzaA(i);
        this.zzaDy.zzrz();
        if (i == 2) {
            zzqc();
        }
    }

    @Override // com.google.android.gms.internal.zzbdq
    public final void zzm(Bundle bundle) {
        while (!this.zzaCJ.isEmpty()) {
            zze(this.zzaCJ.remove());
        }
        this.zzaDy.zzn(bundle);
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final <L> zzbdw<L> zzp(@NonNull L l) {
        this.zzaCv.lock();
        try {
            return this.zzaDH.zza(l, this.zzrM, "NO_TYPE");
        } finally {
            this.zzaCv.unlock();
        }
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient
    public final void zzpl() {
        if (this.zzaDz != null) {
            this.zzaDz.zzpl();
        }
    }

    final boolean zzqe() {
        if (!this.zzaDA) {
            return false;
        }
        this.zzaDA = false;
        this.zzaDD.removeMessages(2);
        this.zzaDD.removeMessages(1);
        if (this.zzaDE != null) {
            this.zzaDE.unregister();
            this.zzaDE = null;
        }
        return true;
    }

    final boolean zzqf() {
        boolean z = false;
        this.zzaCv.lock();
        try {
            if (this.zzaDK != null) {
                z = this.zzaDK.isEmpty() ? false : true;
            }
            return z;
        } finally {
            this.zzaCv.unlock();
        }
    }

    final String zzqg() {
        StringWriter stringWriter = new StringWriter();
        dump("", null, new PrintWriter(stringWriter), null);
        return stringWriter.toString();
    }
}
