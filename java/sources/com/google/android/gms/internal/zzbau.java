package com.google.android.gms.internal;

import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.util.Log;
import android.util.SparseArray;
import com.fasterxml.jackson.core.util.MinimalPrettyPrinter;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.internal.zzbo;
import java.io.FileDescriptor;
import java.io.PrintWriter;

/* JADX INFO: loaded from: classes.dex */
public class zzbau extends zzbba {
    private final SparseArray<zza> zzaBB;

    class zza implements GoogleApiClient.OnConnectionFailedListener {
        public final int zzaBC;
        public final GoogleApiClient zzaBD;
        public final GoogleApiClient.OnConnectionFailedListener zzaBE;

        public zza(int i, GoogleApiClient googleApiClient, GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener) {
            this.zzaBC = i;
            this.zzaBD = googleApiClient;
            this.zzaBE = onConnectionFailedListener;
            googleApiClient.registerConnectionFailedListener(this);
        }

        @Override // com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener
        public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
            String strValueOf = String.valueOf(connectionResult);
            Log.d("AutoManageHelper", new StringBuilder(String.valueOf(strValueOf).length() + 27).append("beginFailureResolution for ").append(strValueOf).toString());
            zzbau.this.zzb(connectionResult, this.zzaBC);
        }
    }

    private zzbau(zzbdt zzbdtVar) {
        super(zzbdtVar);
        this.zzaBB = new SparseArray<>();
        this.zzaEG.zza("AutoManageHelper", this);
    }

    public static zzbau zza(zzbdr zzbdrVar) {
        zzbdt zzbdtVarZzb = zzb(zzbdrVar);
        zzbau zzbauVar = (zzbau) zzbdtVarZzb.zza("AutoManageHelper", zzbau.class);
        return zzbauVar != null ? zzbauVar : new zzbau(zzbdtVarZzb);
    }

    @Nullable
    private final zza zzam(int i) {
        if (this.zzaBB.size() <= i) {
            return null;
        }
        return this.zzaBB.get(this.zzaBB.keyAt(i));
    }

    @Override // com.google.android.gms.internal.zzbds
    public final void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
        for (int i = 0; i < this.zzaBB.size(); i++) {
            zza zzaVarZzam = zzam(i);
            if (zzaVarZzam != null) {
                printWriter.append((CharSequence) str).append("GoogleApiClient #").print(zzaVarZzam.zzaBC);
                printWriter.println(":");
                zzaVarZzam.zzaBD.dump(String.valueOf(str).concat("  "), fileDescriptor, printWriter, strArr);
            }
        }
    }

    @Override // com.google.android.gms.internal.zzbba, com.google.android.gms.internal.zzbds
    public void onStart() {
        super.onStart();
        boolean z = this.mStarted;
        String strValueOf = String.valueOf(this.zzaBB);
        Log.d("AutoManageHelper", new StringBuilder(String.valueOf(strValueOf).length() + 14).append("onStart ").append(z).append(MinimalPrettyPrinter.DEFAULT_ROOT_VALUE_SEPARATOR).append(strValueOf).toString());
        if (this.zzaBN.get() == null) {
            for (int i = 0; i < this.zzaBB.size(); i++) {
                zza zzaVarZzam = zzam(i);
                if (zzaVarZzam != null) {
                    zzaVarZzam.zzaBD.connect();
                }
            }
        }
    }

    @Override // com.google.android.gms.internal.zzbba, com.google.android.gms.internal.zzbds
    public void onStop() {
        super.onStop();
        for (int i = 0; i < this.zzaBB.size(); i++) {
            zza zzaVarZzam = zzam(i);
            if (zzaVarZzam != null) {
                zzaVarZzam.zzaBD.disconnect();
            }
        }
    }

    public final void zza(int i, GoogleApiClient googleApiClient, GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener) {
        zzbo.zzb(googleApiClient, "GoogleApiClient instance cannot be null");
        zzbo.zza(this.zzaBB.indexOfKey(i) < 0, new StringBuilder(54).append("Already managing a GoogleApiClient with id ").append(i).toString());
        zzbbb zzbbbVar = this.zzaBN.get();
        boolean z = this.mStarted;
        String strValueOf = String.valueOf(zzbbbVar);
        Log.d("AutoManageHelper", new StringBuilder(String.valueOf(strValueOf).length() + 49).append("starting AutoManage for client ").append(i).append(MinimalPrettyPrinter.DEFAULT_ROOT_VALUE_SEPARATOR).append(z).append(MinimalPrettyPrinter.DEFAULT_ROOT_VALUE_SEPARATOR).append(strValueOf).toString());
        this.zzaBB.put(i, new zza(i, googleApiClient, onConnectionFailedListener));
        if (this.mStarted && zzbbbVar == null) {
            String strValueOf2 = String.valueOf(googleApiClient);
            Log.d("AutoManageHelper", new StringBuilder(String.valueOf(strValueOf2).length() + 11).append("connecting ").append(strValueOf2).toString());
            googleApiClient.connect();
        }
    }

    @Override // com.google.android.gms.internal.zzbba
    protected final void zza(ConnectionResult connectionResult, int i) {
        Log.w("AutoManageHelper", "Unresolved error while connecting client. Stopping auto-manage.");
        if (i < 0) {
            Log.wtf("AutoManageHelper", "AutoManageLifecycleHelper received onErrorResolutionFailed callback but no failing client ID is set", new Exception());
            return;
        }
        zza zzaVar = this.zzaBB.get(i);
        if (zzaVar != null) {
            zzal(i);
            GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener = zzaVar.zzaBE;
            if (onConnectionFailedListener != null) {
                onConnectionFailedListener.onConnectionFailed(connectionResult);
            }
        }
    }

    public final void zzal(int i) {
        zza zzaVar = this.zzaBB.get(i);
        this.zzaBB.remove(i);
        if (zzaVar != null) {
            zzaVar.zzaBD.unregisterConnectionFailedListener(zzaVar);
            zzaVar.zzaBD.disconnect();
        }
    }

    @Override // com.google.android.gms.internal.zzbba
    protected final void zzps() {
        for (int i = 0; i < this.zzaBB.size(); i++) {
            zza zzaVarZzam = zzam(i);
            if (zzaVarZzam != null) {
                zzaVarZzam.zzaBD.connect();
            }
        }
    }
}
