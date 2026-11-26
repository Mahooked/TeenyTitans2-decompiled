package com.google.android.gms.internal;

import android.app.Activity;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.TaskCompletionSource;
import java.util.concurrent.CancellationException;

/* JADX INFO: loaded from: classes.dex */
public class zzbeb extends zzbba {
    private TaskCompletionSource<Void> zzalE;

    private zzbeb(zzbdt zzbdtVar) {
        super(zzbdtVar);
        this.zzalE = new TaskCompletionSource<>();
        this.zzaEG.zza("GmsAvailabilityHelper", this);
    }

    public static zzbeb zzp(Activity activity) {
        zzbdt zzbdtVarZzn = zzn(activity);
        zzbeb zzbebVar = (zzbeb) zzbdtVarZzn.zza("GmsAvailabilityHelper", zzbeb.class);
        if (zzbebVar == null) {
            return new zzbeb(zzbdtVarZzn);
        }
        if (!zzbebVar.zzalE.getTask().isComplete()) {
            return zzbebVar;
        }
        zzbebVar.zzalE = new TaskCompletionSource<>();
        return zzbebVar;
    }

    public final Task<Void> getTask() {
        return this.zzalE.getTask();
    }

    @Override // com.google.android.gms.internal.zzbds
    public void onDestroy() {
        super.onDestroy();
        this.zzalE.setException(new CancellationException("Host activity was destroyed before Google Play services could be made available."));
    }

    @Override // com.google.android.gms.internal.zzbba
    protected final void zza(ConnectionResult connectionResult, int i) {
        this.zzalE.setException(com.google.android.gms.common.internal.zzb.zzx(new Status(connectionResult.getErrorCode(), connectionResult.getErrorMessage(), connectionResult.getResolution())));
    }

    @Override // com.google.android.gms.internal.zzbba
    protected final void zzps() {
        int iIsGooglePlayServicesAvailable = this.zzaBd.isGooglePlayServicesAvailable(this.zzaEG.zzqF());
        if (iIsGooglePlayServicesAvailable == 0) {
            this.zzalE.setResult(null);
        } else {
            if (this.zzalE.getTask().isComplete()) {
                return;
            }
            zzb(new ConnectionResult(iIsGooglePlayServicesAvailable, null), 0);
        }
    }
}
