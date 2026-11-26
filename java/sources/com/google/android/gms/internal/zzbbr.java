package com.google.android.gms.internal;

import android.support.annotation.NonNull;
import android.support.v4.util.ArrayMap;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import java.util.Collections;
import java.util.Iterator;

/* JADX INFO: loaded from: classes.dex */
final class zzbbr implements OnCompleteListener<Void> {
    private /* synthetic */ zzbbp zzaCP;

    private zzbbr(zzbbp zzbbpVar) {
        this.zzaCP = zzbbpVar;
    }

    @Override // com.google.android.gms.tasks.OnCompleteListener
    public final void onComplete(@NonNull Task<Void> task) {
        this.zzaCP.zzaCv.lock();
        try {
            if (this.zzaCP.zzaCK) {
                if (task.isSuccessful()) {
                    this.zzaCP.zzaCL = new ArrayMap(this.zzaCP.zzaCB.size());
                    Iterator it = this.zzaCP.zzaCB.values().iterator();
                    while (it.hasNext()) {
                        this.zzaCP.zzaCL.put(((zzbbo) it.next()).zzph(), ConnectionResult.zzazX);
                    }
                } else if (task.getException() instanceof com.google.android.gms.common.api.zza) {
                    com.google.android.gms.common.api.zza zzaVar = (com.google.android.gms.common.api.zza) task.getException();
                    if (this.zzaCP.zzaCI) {
                        this.zzaCP.zzaCL = new ArrayMap(this.zzaCP.zzaCB.size());
                        for (zzbbo zzbboVar : this.zzaCP.zzaCB.values()) {
                            Object objZzph = zzbboVar.zzph();
                            ConnectionResult connectionResultZza = zzaVar.zza(zzbboVar);
                            if (this.zzaCP.zza((zzbbo<?>) zzbboVar, connectionResultZza)) {
                                this.zzaCP.zzaCL.put(objZzph, new ConnectionResult(16));
                            } else {
                                this.zzaCP.zzaCL.put(objZzph, connectionResultZza);
                            }
                        }
                    } else {
                        this.zzaCP.zzaCL = zzaVar.zzpf();
                    }
                    this.zzaCP.zzaCO = this.zzaCP.zzpN();
                } else {
                    Log.e("ConnectionlessGAC", "Unexpected availability exception", task.getException());
                    this.zzaCP.zzaCL = Collections.emptyMap();
                    this.zzaCP.zzaCO = new ConnectionResult(8);
                }
                if (this.zzaCP.zzaCM != null) {
                    this.zzaCP.zzaCL.putAll(this.zzaCP.zzaCM);
                    this.zzaCP.zzaCO = this.zzaCP.zzpN();
                }
                if (this.zzaCP.zzaCO == null) {
                    this.zzaCP.zzpL();
                    this.zzaCP.zzpM();
                } else {
                    zzbbp.zza(this.zzaCP, false);
                    this.zzaCP.zzaCE.zzc(this.zzaCP.zzaCO);
                }
                this.zzaCP.zzaCG.signalAll();
            }
        } finally {
            this.zzaCP.zzaCv.unlock();
        }
    }
}
