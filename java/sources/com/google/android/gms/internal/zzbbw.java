package com.google.android.gms.internal;

import android.app.Activity;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.internal.zzbo;

/* JADX INFO: loaded from: classes.dex */
public class zzbbw extends zzbba {
    private zzbdb zzaAN;
    private final com.google.android.gms.common.util.zza<zzbat<?>> zzaCW;

    private zzbbw(zzbdt zzbdtVar) {
        super(zzbdtVar);
        this.zzaCW = new com.google.android.gms.common.util.zza<>();
        this.zzaEG.zza("ConnectionlessLifecycleHelper", this);
    }

    public static void zza(Activity activity, zzbdb zzbdbVar, zzbat<?> zzbatVar) {
        zzn(activity);
        zzbdt zzbdtVarZzn = zzn(activity);
        zzbbw zzbbwVar = (zzbbw) zzbdtVarZzn.zza("ConnectionlessLifecycleHelper", zzbbw.class);
        if (zzbbwVar == null) {
            zzbbwVar = new zzbbw(zzbdtVarZzn);
        }
        zzbbwVar.zzaAN = zzbdbVar;
        zzbo.zzb(zzbatVar, "ApiKey cannot be null");
        zzbbwVar.zzaCW.add(zzbatVar);
        zzbdbVar.zza(zzbbwVar);
    }

    private final void zzpS() {
        if (this.zzaCW.isEmpty()) {
            return;
        }
        this.zzaAN.zza(this);
    }

    @Override // com.google.android.gms.internal.zzbds
    public void onResume() {
        super.onResume();
        zzpS();
    }

    @Override // com.google.android.gms.internal.zzbba, com.google.android.gms.internal.zzbds
    public void onStart() {
        super.onStart();
        zzpS();
    }

    @Override // com.google.android.gms.internal.zzbba, com.google.android.gms.internal.zzbds
    public void onStop() {
        super.onStop();
        this.zzaAN.zzb(this);
    }

    @Override // com.google.android.gms.internal.zzbba
    protected final void zza(ConnectionResult connectionResult, int i) {
        this.zzaAN.zza(connectionResult, i);
    }

    final com.google.android.gms.common.util.zza<zzbat<?>> zzpR() {
        return this.zzaCW;
    }

    @Override // com.google.android.gms.internal.zzbba
    protected final void zzps() {
        this.zzaAN.zzps();
    }
}
