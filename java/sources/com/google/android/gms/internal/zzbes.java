package com.google.android.gms.internal;

import android.os.Looper;
import android.support.annotation.NonNull;
import android.util.Log;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Releasable;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.ResultCallback;
import com.google.android.gms.common.api.ResultCallbacks;
import com.google.android.gms.common.api.ResultTransform;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.TransformedResult;
import com.google.android.gms.common.internal.zzbo;
import java.lang.ref.WeakReference;

/* JADX INFO: loaded from: classes.dex */
public final class zzbes<R extends Result> extends TransformedResult<R> implements ResultCallback<R> {
    private final WeakReference<GoogleApiClient> zzaBY;
    private final zzbeu zzaFf;
    private ResultTransform<? super R, ? extends Result> zzaFa = null;
    private zzbes<? extends Result> zzaFb = null;
    private volatile ResultCallbacks<? super R> zzaFc = null;
    private PendingResult<R> zzaFd = null;
    private final Object zzaBW = new Object();
    private Status zzaFe = null;
    private boolean zzaFg = false;

    public zzbes(WeakReference<GoogleApiClient> weakReference) {
        zzbo.zzb(weakReference, "GoogleApiClient reference must not be null");
        this.zzaBY = weakReference;
        GoogleApiClient googleApiClient = this.zzaBY.get();
        this.zzaFf = new zzbeu(this, googleApiClient != null ? googleApiClient.getLooper() : Looper.getMainLooper());
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static void zzc(Result result) {
        if (result instanceof Releasable) {
            try {
                ((Releasable) result).release();
            } catch (RuntimeException e) {
                String strValueOf = String.valueOf(result);
                Log.w("TransformedResultImpl", new StringBuilder(String.valueOf(strValueOf).length() + 18).append("Unable to release ").append(strValueOf).toString(), e);
            }
        }
    }

    private final void zzqJ() {
        if (this.zzaFa == null && this.zzaFc == null) {
            return;
        }
        GoogleApiClient googleApiClient = this.zzaBY.get();
        if (!this.zzaFg && this.zzaFa != null && googleApiClient != null) {
            googleApiClient.zza(this);
            this.zzaFg = true;
        }
        if (this.zzaFe != null) {
            zzw(this.zzaFe);
        } else if (this.zzaFd != null) {
            this.zzaFd.setResultCallback(this);
        }
    }

    private final boolean zzqL() {
        return (this.zzaFc == null || this.zzaBY.get() == null) ? false : true;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzv(Status status) {
        synchronized (this.zzaBW) {
            this.zzaFe = status;
            zzw(this.zzaFe);
        }
    }

    private final void zzw(Status status) {
        synchronized (this.zzaBW) {
            if (this.zzaFa != null) {
                Status statusOnFailure = this.zzaFa.onFailure(status);
                zzbo.zzb(statusOnFailure, "onFailure must not return null");
                this.zzaFb.zzv(statusOnFailure);
            } else if (zzqL()) {
                this.zzaFc.onFailure(status);
            }
        }
    }

    @Override // com.google.android.gms.common.api.TransformedResult
    public final void andFinally(@NonNull ResultCallbacks<? super R> resultCallbacks) {
        synchronized (this.zzaBW) {
            zzbo.zza(this.zzaFc == null, "Cannot call andFinally() twice.");
            zzbo.zza(this.zzaFa == null, "Cannot call then() and andFinally() on the same TransformedResult.");
            this.zzaFc = resultCallbacks;
            zzqJ();
        }
    }

    @Override // com.google.android.gms.common.api.ResultCallback
    public final void onResult(R r) {
        synchronized (this.zzaBW) {
            if (!r.getStatus().isSuccess()) {
                zzv(r.getStatus());
                zzc(r);
            } else if (this.zzaFa != null) {
                zzbeg.zzqj().submit(new zzbet(this, r));
            } else if (zzqL()) {
                this.zzaFc.onSuccess(r);
            }
        }
    }

    @Override // com.google.android.gms.common.api.TransformedResult
    @NonNull
    public final <S extends Result> TransformedResult<S> then(@NonNull ResultTransform<? super R, ? extends S> resultTransform) {
        zzbes<? extends Result> zzbesVar;
        synchronized (this.zzaBW) {
            zzbo.zza(this.zzaFa == null, "Cannot call then() twice.");
            zzbo.zza(this.zzaFc == null, "Cannot call then() and andFinally() on the same TransformedResult.");
            this.zzaFa = resultTransform;
            zzbesVar = new zzbes<>(this.zzaBY);
            this.zzaFb = zzbesVar;
            zzqJ();
        }
        return zzbesVar;
    }

    /* JADX WARN: Multi-variable type inference failed */
    public final void zza(PendingResult<?> pendingResult) {
        synchronized (this.zzaBW) {
            this.zzaFd = pendingResult;
            zzqJ();
        }
    }

    final void zzqK() {
        this.zzaFc = null;
    }
}
