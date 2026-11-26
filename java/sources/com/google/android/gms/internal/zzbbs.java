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
final class zzbbs implements OnCompleteListener<Void> {
    private /* synthetic */ zzbbp zzaCP;
    private zzbei zzaCQ;

    zzbbs(zzbbp zzbbpVar, zzbei zzbeiVar) {
        this.zzaCP = zzbbpVar;
        this.zzaCQ = zzbeiVar;
    }

    final void cancel() {
        this.zzaCQ.zzmF();
    }

    @Override // com.google.android.gms.tasks.OnCompleteListener
    public final void onComplete(@NonNull Task<Void> task) {
        this.zzaCP.zzaCv.lock();
        try {
            if (!this.zzaCP.zzaCK) {
                this.zzaCQ.zzmF();
                this.zzaCP.zzaCv.unlock();
                return;
            }
            if (task.isSuccessful()) {
                this.zzaCP.zzaCM = new ArrayMap(this.zzaCP.zzaCC.size());
                Iterator it = this.zzaCP.zzaCC.values().iterator();
                while (it.hasNext()) {
                    this.zzaCP.zzaCM.put(((zzbbo) it.next()).zzph(), ConnectionResult.zzazX);
                }
            } else if (task.getException() instanceof com.google.android.gms.common.api.zza) {
                com.google.android.gms.common.api.zza zzaVar = (com.google.android.gms.common.api.zza) task.getException();
                if (this.zzaCP.zzaCI) {
                    this.zzaCP.zzaCM = new ArrayMap(this.zzaCP.zzaCC.size());
                    for (zzbbo zzbboVar : this.zzaCP.zzaCC.values()) {
                        Object objZzph = zzbboVar.zzph();
                        ConnectionResult connectionResultZza = zzaVar.zza(zzbboVar);
                        if (this.zzaCP.zza((zzbbo<?>) zzbboVar, connectionResultZza)) {
                            this.zzaCP.zzaCM.put(objZzph, new ConnectionResult(16));
                        } else {
                            this.zzaCP.zzaCM.put(objZzph, connectionResultZza);
                        }
                    }
                } else {
                    this.zzaCP.zzaCM = zzaVar.zzpf();
                }
            } else {
                Log.e("ConnectionlessGAC", "Unexpected availability exception", task.getException());
                this.zzaCP.zzaCM = Collections.emptyMap();
            }
            if (this.zzaCP.isConnected()) {
                this.zzaCP.zzaCL.putAll(this.zzaCP.zzaCM);
                if (this.zzaCP.zzpN() == null) {
                    this.zzaCP.zzpL();
                    this.zzaCP.zzpM();
                    this.zzaCP.zzaCG.signalAll();
                }
            }
            this.zzaCQ.zzmF();
            this.zzaCP.zzaCv.unlock();
        } catch (Throwable th) {
            this.zzaCP.zzaCv.unlock();
            throw th;
        }
    }
}
