package com.google.android.gms.internal;

import android.app.PendingIntent;
import android.content.Context;
import android.os.Handler;
import android.os.HandlerThread;
import android.os.Looper;
import android.support.annotation.NonNull;
import android.support.annotation.WorkerThread;
import com.comscore.streaming.Constants;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.GoogleApi;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.zzbo;
import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.TaskCompletionSource;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.atomic.AtomicInteger;

/* JADX INFO: loaded from: classes.dex */
public final class zzbdb implements Handler.Callback {
    private static zzbdb zzaEf;
    private final Context mContext;
    private final Handler mHandler;
    private final GoogleApiAvailability zzaBd;
    public static final Status zzaEc = new Status(4, "Sign-out occurred while this API call was in progress.");
    private static final Status zzaEd = new Status(4, "The user must be signed in to make this API call.");
    private static final Object zzuF = new Object();
    private long zzaDC = 5000;
    private long zzaDB = 120000;
    private long zzaEe = Constants.HEARTBEAT_STAGE_ONE_INTERVAL;
    private int zzaEg = -1;
    private final AtomicInteger zzaEh = new AtomicInteger(1);
    private final AtomicInteger zzaEi = new AtomicInteger(0);
    private final Map<zzbat<?>, zzbdd<?>> zzaCB = new ConcurrentHashMap(5, 0.75f, 1);
    private zzbbw zzaEj = null;
    private final Set<zzbat<?>> zzaEk = new com.google.android.gms.common.util.zza();
    private final Set<zzbat<?>> zzaEl = new com.google.android.gms.common.util.zza();

    private zzbdb(Context context, Looper looper, GoogleApiAvailability googleApiAvailability) {
        this.mContext = context;
        this.mHandler = new Handler(looper, this);
        this.zzaBd = googleApiAvailability;
        this.mHandler.sendMessage(this.mHandler.obtainMessage(6));
    }

    public static zzbdb zzay(Context context) {
        zzbdb zzbdbVar;
        synchronized (zzuF) {
            if (zzaEf == null) {
                HandlerThread handlerThread = new HandlerThread("GoogleApiHandler", 9);
                handlerThread.start();
                zzaEf = new zzbdb(context.getApplicationContext(), handlerThread.getLooper(), GoogleApiAvailability.getInstance());
            }
            zzbdbVar = zzaEf;
        }
        return zzbdbVar;
    }

    @WorkerThread
    private final void zzc(GoogleApi<?> googleApi) {
        Object objZzph = googleApi.zzph();
        zzbdd<?> zzbddVar = this.zzaCB.get(objZzph);
        if (zzbddVar == null) {
            zzbddVar = new zzbdd<>(this, googleApi);
            this.zzaCB.put((zzbat<?>) objZzph, zzbddVar);
        }
        if (zzbddVar.zzmv()) {
            this.zzaEl.add((zzbat<?>) objZzph);
        }
        zzbddVar.connect();
    }

    public static zzbdb zzqk() {
        zzbdb zzbdbVar;
        synchronized (zzuF) {
            zzbo.zzb(zzaEf, "Must guarantee manager is non-null before using getInstance");
            zzbdbVar = zzaEf;
        }
        return zzbdbVar;
    }

    public static void zzql() {
        synchronized (zzuF) {
            if (zzaEf != null) {
                zzbdb zzbdbVar = zzaEf;
                zzbdbVar.zzaEi.incrementAndGet();
                zzbdbVar.mHandler.sendMessageAtFrontOfQueue(zzbdbVar.mHandler.obtainMessage(10));
            }
        }
    }

    @WorkerThread
    private final void zzqn() {
        Iterator<zzbat<?>> it = this.zzaEl.iterator();
        while (it.hasNext()) {
            this.zzaCB.remove(it.next()).signOut();
        }
        this.zzaEl.clear();
    }

    /* JADX WARN: Code duplicated, block: B:47:0x0139  */
    /* JADX WARN: Code duplicated, block: B:48:0x018b  */
    /*  JADX ERROR: JadxRuntimeException in pass: IfRegionVisitor
        jadx.core.utils.exceptions.JadxRuntimeException: Can't remove SSA var: r1v19 com.google.android.gms.internal.zzbdd<?>, still in use, count: 2, list:
          (r1v19 com.google.android.gms.internal.zzbdd<?>) from 0x0131: INVOKE (r1v19 com.google.android.gms.internal.zzbdd<?>) VIRTUAL call: com.google.android.gms.internal.zzbdd.getInstanceId():int A[MD:():int (m), WRAPPED]
          (r1v19 com.google.android.gms.internal.zzbdd<?>) from 0x0137: PHI (r1 I:??) = (r1v10 com.google.android.gms.internal.zzbdd<?>), (r1v19 com.google.android.gms.internal.zzbdd<?>) binds: [B:65:0x023a, B:79:0x0137] A[DONT_GENERATE, DONT_INLINE]
        	at jadx.core.utils.InsnRemover.removeSsaVar(InsnRemover.java:164)
        	at jadx.core.utils.InsnRemover.unbindResult(InsnRemover.java:129)
        	at jadx.core.utils.InsnRemover.unbindInsn(InsnRemover.java:93)
        	at jadx.core.dex.visitors.regions.TernaryMod.makeTernaryInsn(TernaryMod.java:132)
        	at jadx.core.dex.visitors.regions.TernaryMod.processRegion(TernaryMod.java:67)
        	at jadx.core.dex.visitors.regions.TernaryMod.enterRegion(TernaryMod.java:50)
        	at jadx.core.dex.visitors.regions.DepthRegionTraversal.traverseInternal(DepthRegionTraversal.java:96)
        	at jadx.core.dex.visitors.regions.DepthRegionTraversal.traverse(DepthRegionTraversal.java:27)
        	at jadx.core.dex.visitors.regions.TernaryMod.process(TernaryMod.java:36)
        	at jadx.core.dex.visitors.regions.IfRegionVisitor.process(IfRegionVisitor.java:44)
        	at jadx.core.dex.visitors.regions.IfRegionVisitor.visit(IfRegionVisitor.java:30)
        */
    @Override // android.os.Handler.Callback
    @android.support.annotation.WorkerThread
    public final boolean handleMessage(android.os.Message r9) {
        /*
            Method dump skipped, instruction units count: 604
            To view this dump add '--comments-level debug' option
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzbdb.handleMessage(android.os.Message):boolean");
    }

    final PendingIntent zza(zzbat<?> zzbatVar, int i) {
        zzctk zzctkVarZzqy;
        zzbdd<?> zzbddVar = this.zzaCB.get(zzbatVar);
        if (zzbddVar != null && (zzctkVarZzqy = zzbddVar.zzqy()) != null) {
            return PendingIntent.getActivity(this.mContext, i, zzctkVarZzqy.zzmH(), 134217728);
        }
        return null;
    }

    public final <O extends Api.ApiOptions> Task<Void> zza(@NonNull GoogleApi<O> googleApi, @NonNull zzbdy<?> zzbdyVar) {
        TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
        this.mHandler.sendMessage(this.mHandler.obtainMessage(13, new zzbed(new zzbar(zzbdyVar, taskCompletionSource), this.zzaEi.get(), googleApi)));
        return taskCompletionSource.getTask();
    }

    public final <O extends Api.ApiOptions> Task<Void> zza(@NonNull GoogleApi<O> googleApi, @NonNull zzbee<Api.zzb, ?> zzbeeVar, @NonNull zzbey<Api.zzb, ?> zzbeyVar) {
        TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
        this.mHandler.sendMessage(this.mHandler.obtainMessage(8, new zzbed(new zzbap(new zzbef(zzbeeVar, zzbeyVar), taskCompletionSource), this.zzaEi.get(), googleApi)));
        return taskCompletionSource.getTask();
    }

    public final Task<Void> zza(Iterable<? extends GoogleApi<?>> iterable) {
        zzbav zzbavVar = new zzbav(iterable);
        Iterator<? extends GoogleApi<?>> it = iterable.iterator();
        while (it.hasNext()) {
            zzbdd<?> zzbddVar = this.zzaCB.get(it.next().zzph());
            if (zzbddVar == null || !zzbddVar.isConnected()) {
                this.mHandler.sendMessage(this.mHandler.obtainMessage(2, zzbavVar));
                return zzbavVar.getTask();
            }
        }
        zzbavVar.zzpu();
        return zzbavVar.getTask();
    }

    public final void zza(ConnectionResult connectionResult, int i) {
        if (zzc(connectionResult, i)) {
            return;
        }
        this.mHandler.sendMessage(this.mHandler.obtainMessage(5, i, 0, connectionResult));
    }

    public final <O extends Api.ApiOptions> void zza(GoogleApi<O> googleApi, int i, zzbay<? extends Result, Api.zzb> zzbayVar) {
        this.mHandler.sendMessage(this.mHandler.obtainMessage(4, new zzbed(new zzbao(i, zzbayVar), this.zzaEi.get(), googleApi)));
    }

    public final <O extends Api.ApiOptions, TResult> void zza(GoogleApi<O> googleApi, int i, zzbeq<Api.zzb, TResult> zzbeqVar, TaskCompletionSource<TResult> taskCompletionSource, zzbem zzbemVar) {
        this.mHandler.sendMessage(this.mHandler.obtainMessage(4, new zzbed(new zzbaq(i, zzbeqVar, taskCompletionSource, zzbemVar), this.zzaEi.get(), googleApi)));
    }

    public final void zza(@NonNull zzbbw zzbbwVar) {
        synchronized (zzuF) {
            if (this.zzaEj != zzbbwVar) {
                this.zzaEj = zzbbwVar;
                this.zzaEk.clear();
                this.zzaEk.addAll(zzbbwVar.zzpR());
            }
        }
    }

    public final void zzb(GoogleApi<?> googleApi) {
        this.mHandler.sendMessage(this.mHandler.obtainMessage(7, googleApi));
    }

    final void zzb(@NonNull zzbbw zzbbwVar) {
        synchronized (zzuF) {
            if (this.zzaEj == zzbbwVar) {
                this.zzaEj = null;
                this.zzaEk.clear();
            }
        }
    }

    final boolean zzc(ConnectionResult connectionResult, int i) {
        return this.zzaBd.zza(this.mContext, connectionResult, i);
    }

    final void zzpl() {
        this.zzaEi.incrementAndGet();
        this.mHandler.sendMessage(this.mHandler.obtainMessage(10));
    }

    public final void zzps() {
        this.mHandler.sendMessage(this.mHandler.obtainMessage(3));
    }

    public final int zzqm() {
        return this.zzaEh.getAndIncrement();
    }
}
