package com.google.android.gms.internal;

import android.os.Looper;
import android.os.RemoteException;
import android.support.annotation.NonNull;
import android.util.Log;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Releasable;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.ResultCallback;
import com.google.android.gms.common.api.ResultTransform;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.TransformedResult;
import com.google.android.gms.common.internal.zzbo;
import java.lang.ref.WeakReference;
import java.util.ArrayList;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicReference;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzbbe<R extends Result> extends PendingResult<R> {
    static final ThreadLocal<Boolean> zzaBV = new zzbbf();
    private Status mStatus;
    private boolean zzJ;
    private final Object zzaBW;
    private zzbbg<R> zzaBX;
    private WeakReference<GoogleApiClient> zzaBY;
    private final ArrayList<PendingResult.zza> zzaBZ;
    private R zzaBj;
    private ResultCallback<? super R> zzaCa;
    private final AtomicReference<zzbex> zzaCb;
    private zzbbh zzaCc;
    private volatile boolean zzaCd;
    private boolean zzaCe;
    private com.google.android.gms.common.internal.zzao zzaCf;
    private volatile zzbes<R> zzaCg;
    private boolean zzaCh;
    private final CountDownLatch zztJ;

    @Deprecated
    zzbbe() {
        this.zzaBW = new Object();
        this.zztJ = new CountDownLatch(1);
        this.zzaBZ = new ArrayList<>();
        this.zzaCb = new AtomicReference<>();
        this.zzaCh = false;
        this.zzaBX = new zzbbg<>(Looper.getMainLooper());
        this.zzaBY = new WeakReference<>(null);
    }

    @Deprecated
    protected zzbbe(Looper looper) {
        this.zzaBW = new Object();
        this.zztJ = new CountDownLatch(1);
        this.zzaBZ = new ArrayList<>();
        this.zzaCb = new AtomicReference<>();
        this.zzaCh = false;
        this.zzaBX = new zzbbg<>(looper);
        this.zzaBY = new WeakReference<>(null);
    }

    protected zzbbe(GoogleApiClient googleApiClient) {
        this.zzaBW = new Object();
        this.zztJ = new CountDownLatch(1);
        this.zzaBZ = new ArrayList<>();
        this.zzaCb = new AtomicReference<>();
        this.zzaCh = false;
        this.zzaBX = new zzbbg<>(googleApiClient != null ? googleApiClient.getLooper() : Looper.getMainLooper());
        this.zzaBY = new WeakReference<>(googleApiClient);
    }

    private final R get() {
        R r;
        synchronized (this.zzaBW) {
            zzbo.zza(this.zzaCd ? false : true, "Result has already been consumed.");
            zzbo.zza(isReady(), "Result is not ready.");
            r = this.zzaBj;
            this.zzaBj = null;
            this.zzaCa = null;
            this.zzaCd = true;
        }
        zzbex andSet = this.zzaCb.getAndSet(null);
        if (andSet != null) {
            andSet.zzc(this);
        }
        return r;
    }

    private final void zzb(R r) {
        zzbbf zzbbfVar = null;
        this.zzaBj = r;
        this.zzaCf = null;
        this.zztJ.countDown();
        this.mStatus = this.zzaBj.getStatus();
        if (this.zzJ) {
            this.zzaCa = null;
        } else if (this.zzaCa != null) {
            this.zzaBX.removeMessages(2);
            this.zzaBX.zza(this.zzaCa, get());
        } else if (this.zzaBj instanceof Releasable) {
            this.zzaCc = new zzbbh(this, zzbbfVar);
        }
        ArrayList<PendingResult.zza> arrayList = this.zzaBZ;
        int size = arrayList.size();
        int i = 0;
        while (i < size) {
            PendingResult.zza zzaVar = arrayList.get(i);
            i++;
            zzaVar.zzo(this.mStatus);
        }
        this.zzaBZ.clear();
    }

    public static void zzc(Result result) {
        if (result instanceof Releasable) {
            try {
                ((Releasable) result).release();
            } catch (RuntimeException e) {
                String strValueOf = String.valueOf(result);
                Log.w("BasePendingResult", new StringBuilder(String.valueOf(strValueOf).length() + 18).append("Unable to release ").append(strValueOf).toString(), e);
            }
        }
    }

    @Override // com.google.android.gms.common.api.PendingResult
    public final R await() {
        zzbo.zza(Looper.myLooper() != Looper.getMainLooper(), "await must not be called on the UI thread");
        zzbo.zza(!this.zzaCd, "Result has already been consumed");
        zzbo.zza(this.zzaCg == null, "Cannot await if then() has been called.");
        try {
            this.zztJ.await();
        } catch (InterruptedException e) {
            zzs(Status.zzaBn);
        }
        zzbo.zza(isReady(), "Result is not ready.");
        return (R) get();
    }

    @Override // com.google.android.gms.common.api.PendingResult
    public final R await(long j, TimeUnit timeUnit) {
        zzbo.zza(j <= 0 || Looper.myLooper() != Looper.getMainLooper(), "await must not be called on the UI thread when time is greater than zero.");
        zzbo.zza(!this.zzaCd, "Result has already been consumed.");
        zzbo.zza(this.zzaCg == null, "Cannot await if then() has been called.");
        try {
            if (!this.zztJ.await(j, timeUnit)) {
                zzs(Status.zzaBp);
            }
        } catch (InterruptedException e) {
            zzs(Status.zzaBn);
        }
        zzbo.zza(isReady(), "Result is not ready.");
        return (R) get();
    }

    @Override // com.google.android.gms.common.api.PendingResult
    public void cancel() {
        synchronized (this.zzaBW) {
            if (this.zzJ || this.zzaCd) {
                return;
            }
            if (this.zzaCf != null) {
                try {
                    this.zzaCf.cancel();
                } catch (RemoteException e) {
                }
            }
            zzc(this.zzaBj);
            this.zzJ = true;
            zzb(zzb(Status.zzaBq));
        }
    }

    @Override // com.google.android.gms.common.api.PendingResult
    public boolean isCanceled() {
        boolean z;
        synchronized (this.zzaBW) {
            z = this.zzJ;
        }
        return z;
    }

    public final boolean isReady() {
        return this.zztJ.getCount() == 0;
    }

    public final void setResult(R r) {
        synchronized (this.zzaBW) {
            if (this.zzaCe || this.zzJ) {
                zzc(r);
                return;
            }
            if (isReady()) {
            }
            zzbo.zza(!isReady(), "Results have already been set");
            zzbo.zza(this.zzaCd ? false : true, "Result has already been consumed");
            zzb(r);
        }
    }

    @Override // com.google.android.gms.common.api.PendingResult
    public final void setResultCallback(ResultCallback<? super R> resultCallback) {
        synchronized (this.zzaBW) {
            try {
                if (resultCallback == null) {
                    this.zzaCa = null;
                    return;
                }
                zzbo.zza(!this.zzaCd, "Result has already been consumed.");
                zzbo.zza(this.zzaCg == null, "Cannot set callbacks if then() has been called.");
                if (isCanceled()) {
                    return;
                }
                if (isReady()) {
                    this.zzaBX.zza(resultCallback, get());
                } else {
                    this.zzaCa = resultCallback;
                }
            } catch (Throwable th) {
                throw th;
            }
        }
    }

    @Override // com.google.android.gms.common.api.PendingResult
    public final void setResultCallback(ResultCallback<? super R> resultCallback, long j, TimeUnit timeUnit) {
        synchronized (this.zzaBW) {
            try {
                if (resultCallback == null) {
                    this.zzaCa = null;
                    return;
                }
                zzbo.zza(!this.zzaCd, "Result has already been consumed.");
                zzbo.zza(this.zzaCg == null, "Cannot set callbacks if then() has been called.");
                if (isCanceled()) {
                    return;
                }
                if (isReady()) {
                    this.zzaBX.zza(resultCallback, get());
                } else {
                    this.zzaCa = resultCallback;
                    zzbbg<R> zzbbgVar = this.zzaBX;
                    zzbbgVar.sendMessageDelayed(zzbbgVar.obtainMessage(2, this), timeUnit.toMillis(j));
                }
            } catch (Throwable th) {
                throw th;
            }
        }
    }

    @Override // com.google.android.gms.common.api.PendingResult
    public <S extends Result> TransformedResult<S> then(ResultTransform<? super R, ? extends S> resultTransform) {
        TransformedResult<S> transformedResultThen;
        zzbo.zza(!this.zzaCd, "Result has already been consumed.");
        synchronized (this.zzaBW) {
            zzbo.zza(this.zzaCg == null, "Cannot call then() twice.");
            zzbo.zza(this.zzaCa == null, "Cannot call then() if callbacks are set.");
            zzbo.zza(this.zzJ ? false : true, "Cannot call then() if result was canceled.");
            this.zzaCh = true;
            this.zzaCg = new zzbes<>(this.zzaBY);
            transformedResultThen = this.zzaCg.then(resultTransform);
            if (isReady()) {
                this.zzaBX.zza(this.zzaCg, get());
            } else {
                this.zzaCa = this.zzaCg;
            }
        }
        return transformedResultThen;
    }

    @Override // com.google.android.gms.common.api.PendingResult
    public final void zza(PendingResult.zza zzaVar) {
        zzbo.zzb(zzaVar != null, "Callback cannot be null.");
        synchronized (this.zzaBW) {
            if (isReady()) {
                zzaVar.zzo(this.mStatus);
            } else {
                this.zzaBZ.add(zzaVar);
            }
        }
    }

    protected final void zza(com.google.android.gms.common.internal.zzao zzaoVar) {
        synchronized (this.zzaBW) {
            this.zzaCf = zzaoVar;
        }
    }

    public final void zza(zzbex zzbexVar) {
        this.zzaCb.set(zzbexVar);
    }

    @NonNull
    protected abstract R zzb(Status status);

    public final boolean zzpB() {
        boolean zIsCanceled;
        synchronized (this.zzaBW) {
            if (this.zzaBY.get() == null || !this.zzaCh) {
                cancel();
            }
            zIsCanceled = isCanceled();
        }
        return zIsCanceled;
    }

    public final void zzpC() {
        this.zzaCh = this.zzaCh || zzaBV.get().booleanValue();
    }

    @Override // com.google.android.gms.common.api.PendingResult
    public final Integer zzpo() {
        return null;
    }

    public final void zzs(Status status) {
        synchronized (this.zzaBW) {
            if (!isReady()) {
                setResult(zzb(status));
                this.zzaCe = true;
            }
        }
    }
}
