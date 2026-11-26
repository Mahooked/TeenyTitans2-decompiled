package com.google.android.gms.internal;

import android.app.PendingIntent;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.Nullable;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GoogleApiAvailability;
import java.util.concurrent.atomic.AtomicReference;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzbba extends zzbds implements DialogInterface.OnCancelListener {
    protected volatile boolean mStarted;
    protected final AtomicReference<zzbbb> zzaBN;
    private final Handler zzaBO;
    protected final GoogleApiAvailability zzaBd;

    protected zzbba(zzbdt zzbdtVar) {
        this(zzbdtVar, GoogleApiAvailability.getInstance());
    }

    private zzbba(zzbdt zzbdtVar, GoogleApiAvailability googleApiAvailability) {
        super(zzbdtVar);
        this.zzaBN = new AtomicReference<>(null);
        this.zzaBO = new Handler(Looper.getMainLooper());
        this.zzaBd = googleApiAvailability;
    }

    private static int zza(@Nullable zzbbb zzbbbVar) {
        if (zzbbbVar == null) {
            return -1;
        }
        return zzbbbVar.zzpy();
    }

    /* JADX WARN: Can't fix incorrect switch cases order, some code will duplicate */
    /* JADX WARN: Code duplicated, block: B:23:0x005a A[DONT_INVERT] */
    /* JADX WARN: Code duplicated, block: B:24:0x005c  */
    /* JADX WARN: Code duplicated, block: B:4:0x0011  */
    /* JADX WARN: Code duplicated, block: B:6:0x0014  */
    @Override // com.google.android.gms.internal.zzbds
    public void onActivityResult(int i, int i2, Intent intent) {
        boolean z;
        zzbbb zzbbbVar = this.zzaBN.get();
        switch (i) {
            case 1:
                if (i2 == -1) {
                    z = true;
                } else if (i2 == 0) {
                    zzbbb zzbbbVar2 = new zzbbb(new ConnectionResult(intent != null ? intent.getIntExtra("<<ResolutionFailureErrorDetail>>", 13) : 13, null), zza(zzbbbVar));
                    this.zzaBN.set(zzbbbVar2);
                    zzbbbVar = zzbbbVar2;
                    z = false;
                } else {
                    z = false;
                }
                if (z) {
                    zzpx();
                } else if (zzbbbVar != null) {
                    zza(zzbbbVar.zzpz(), zzbbbVar.zzpy());
                }
                break;
            case 2:
                int iIsGooglePlayServicesAvailable = this.zzaBd.isGooglePlayServicesAvailable(getActivity());
                z = iIsGooglePlayServicesAvailable == 0;
                if (zzbbbVar != null) {
                    if (zzbbbVar.zzpz().getErrorCode() == 18 && iIsGooglePlayServicesAvailable == 18) {
                    }
                    if (z) {
                        zzpx();
                    } else if (zzbbbVar != null) {
                        zza(zzbbbVar.zzpz(), zzbbbVar.zzpy());
                    }
                }
                break;
            default:
                z = false;
                if (z) {
                    zzpx();
                } else if (zzbbbVar != null) {
                    zza(zzbbbVar.zzpz(), zzbbbVar.zzpy());
                }
                break;
        }
    }

    @Override // android.content.DialogInterface.OnCancelListener
    public void onCancel(DialogInterface dialogInterface) {
        zza(new ConnectionResult(13, null), zza(this.zzaBN.get()));
        zzpx();
    }

    @Override // com.google.android.gms.internal.zzbds
    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        if (bundle != null) {
            this.zzaBN.set(bundle.getBoolean("resolving_error", false) ? new zzbbb(new ConnectionResult(bundle.getInt("failed_status"), (PendingIntent) bundle.getParcelable("failed_resolution")), bundle.getInt("failed_client_id", -1)) : null);
        }
    }

    @Override // com.google.android.gms.internal.zzbds
    public final void onSaveInstanceState(Bundle bundle) {
        super.onSaveInstanceState(bundle);
        zzbbb zzbbbVar = this.zzaBN.get();
        if (zzbbbVar != null) {
            bundle.putBoolean("resolving_error", true);
            bundle.putInt("failed_client_id", zzbbbVar.zzpy());
            bundle.putInt("failed_status", zzbbbVar.zzpz().getErrorCode());
            bundle.putParcelable("failed_resolution", zzbbbVar.zzpz().getResolution());
        }
    }

    @Override // com.google.android.gms.internal.zzbds
    public void onStart() {
        super.onStart();
        this.mStarted = true;
    }

    @Override // com.google.android.gms.internal.zzbds
    public void onStop() {
        super.onStop();
        this.mStarted = false;
    }

    protected abstract void zza(ConnectionResult connectionResult, int i);

    public final void zzb(ConnectionResult connectionResult, int i) {
        zzbbb zzbbbVar = new zzbbb(connectionResult, i);
        if (this.zzaBN.compareAndSet(null, zzbbbVar)) {
            this.zzaBO.post(new zzbbc(this, zzbbbVar));
        }
    }

    protected abstract void zzps();

    protected final void zzpx() {
        this.zzaBN.set(null);
        zzps();
    }
}
