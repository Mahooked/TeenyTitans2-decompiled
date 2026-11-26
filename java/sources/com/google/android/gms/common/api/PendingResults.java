package com.google.android.gms.common.api;

import android.os.Looper;
import com.google.android.gms.common.internal.zzbo;
import com.google.android.gms.internal.zzbbe;
import com.google.android.gms.internal.zzbec;
import com.google.android.gms.internal.zzben;

/* JADX INFO: loaded from: classes.dex */
public final class PendingResults {

    static final class zza<R extends Result> extends zzbbe<R> {
        private final R zzaBi;

        public zza(R r) {
            super(Looper.getMainLooper());
            this.zzaBi = r;
        }

        @Override // com.google.android.gms.internal.zzbbe
        protected final R zzb(Status status) {
            if (status.getStatusCode() != this.zzaBi.getStatus().getStatusCode()) {
                throw new UnsupportedOperationException("Creating failed results is not supported");
            }
            return this.zzaBi;
        }
    }

    static final class zzb<R extends Result> extends zzbbe<R> {
        private final R zzaBj;

        public zzb(GoogleApiClient googleApiClient, R r) {
            super(googleApiClient);
            this.zzaBj = r;
        }

        @Override // com.google.android.gms.internal.zzbbe
        protected final R zzb(Status status) {
            return this.zzaBj;
        }
    }

    static final class zzc<R extends Result> extends zzbbe<R> {
        public zzc(GoogleApiClient googleApiClient) {
            super(googleApiClient);
        }

        @Override // com.google.android.gms.internal.zzbbe
        protected final R zzb(Status status) {
            throw new UnsupportedOperationException("Creating failed results is not supported");
        }
    }

    private PendingResults() {
    }

    public static PendingResult<Status> canceledPendingResult() {
        zzben zzbenVar = new zzben(Looper.getMainLooper());
        zzbenVar.cancel();
        return zzbenVar;
    }

    public static <R extends Result> PendingResult<R> canceledPendingResult(R r) {
        zzbo.zzb(r, "Result must not be null");
        zzbo.zzb(r.getStatus().getStatusCode() == 16, "Status code must be CommonStatusCodes.CANCELED");
        zza zzaVar = new zza(r);
        zzaVar.cancel();
        return zzaVar;
    }

    public static <R extends Result> OptionalPendingResult<R> immediatePendingResult(R r) {
        zzbo.zzb(r, "Result must not be null");
        zzc zzcVar = new zzc(null);
        zzcVar.setResult(r);
        return new zzbec(zzcVar);
    }

    public static PendingResult<Status> immediatePendingResult(Status status) {
        zzbo.zzb(status, "Result must not be null");
        zzben zzbenVar = new zzben(Looper.getMainLooper());
        zzbenVar.setResult(status);
        return zzbenVar;
    }

    public static <R extends Result> PendingResult<R> zza(R r, GoogleApiClient googleApiClient) {
        zzbo.zzb(r, "Result must not be null");
        zzbo.zzb(!r.getStatus().isSuccess(), "Status code must not be SUCCESS");
        zzb zzbVar = new zzb(googleApiClient, r);
        zzbVar.setResult(r);
        return zzbVar;
    }

    public static PendingResult<Status> zza(Status status, GoogleApiClient googleApiClient) {
        zzbo.zzb(status, "Result must not be null");
        zzben zzbenVar = new zzben(googleApiClient);
        zzbenVar.setResult(status);
        return zzbenVar;
    }

    public static <R extends Result> OptionalPendingResult<R> zzb(R r, GoogleApiClient googleApiClient) {
        zzbo.zzb(r, "Result must not be null");
        zzc zzcVar = new zzc(googleApiClient);
        zzcVar.setResult(r);
        return new zzbec(zzcVar);
    }
}
