package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.internal.zzbr;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.Future;
import java.util.concurrent.locks.Lock;

/* JADX INFO: loaded from: classes.dex */
public final class zzbcd implements zzbcw {
    private final Context mContext;
    private final Api.zza<? extends zzctk, zzctl> zzaBe;
    private final com.google.android.gms.common.internal.zzq zzaCA;
    private final Map<Api<?>, Boolean> zzaCD;
    private final com.google.android.gms.common.zze zzaCF;
    private ConnectionResult zzaCO;
    private final zzbcx zzaCZ;
    private final Lock zzaCv;
    private int zzaDc;
    private int zzaDe;
    private zzctk zzaDh;
    private boolean zzaDi;
    private boolean zzaDj;
    private boolean zzaDk;
    private com.google.android.gms.common.internal.zzal zzaDl;
    private boolean zzaDm;
    private boolean zzaDn;
    private int zzaDd = 0;
    private final Bundle zzaDf = new Bundle();
    private final Set<Api.zzc> zzaDg = new HashSet();
    private ArrayList<Future<?>> zzaDo = new ArrayList<>();

    public zzbcd(zzbcx zzbcxVar, com.google.android.gms.common.internal.zzq zzqVar, Map<Api<?>, Boolean> map, com.google.android.gms.common.zze zzeVar, Api.zza<? extends zzctk, zzctl> zzaVar, Lock lock, Context context) {
        this.zzaCZ = zzbcxVar;
        this.zzaCA = zzqVar;
        this.zzaCD = map;
        this.zzaCF = zzeVar;
        this.zzaBe = zzaVar;
        this.zzaCv = lock;
        this.mContext = context;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zza(zzctx zzctxVar) {
        if (zzan(0)) {
            ConnectionResult connectionResultZzpz = zzctxVar.zzpz();
            if (!connectionResultZzpz.isSuccess()) {
                if (!zzd(connectionResultZzpz)) {
                    zze(connectionResultZzpz);
                    return;
                } else {
                    zzpZ();
                    zzpX();
                    return;
                }
            }
            zzbr zzbrVarZzAx = zzctxVar.zzAx();
            ConnectionResult connectionResultZzpz2 = zzbrVarZzAx.zzpz();
            if (!connectionResultZzpz2.isSuccess()) {
                String strValueOf = String.valueOf(connectionResultZzpz2);
                Log.wtf("GoogleApiClientConnecting", new StringBuilder(String.valueOf(strValueOf).length() + 48).append("Sign-in succeeded with resolve account failure: ").append(strValueOf).toString(), new Exception());
                zze(connectionResultZzpz2);
            } else {
                this.zzaDk = true;
                this.zzaDl = zzbrVarZzAx.zzrH();
                this.zzaDm = zzbrVarZzAx.zzrI();
                this.zzaDn = zzbrVarZzAx.zzrJ();
                zzpX();
            }
        }
    }

    private final void zzad(boolean z) {
        if (this.zzaDh != null) {
            if (this.zzaDh.isConnected() && z) {
                this.zzaDh.zzAq();
            }
            this.zzaDh.disconnect();
            this.zzaDl = null;
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final boolean zzan(int i) {
        if (this.zzaDd == i) {
            return true;
        }
        Log.w("GoogleApiClientConnecting", this.zzaCZ.zzaCl.zzqg());
        String strValueOf = String.valueOf(this);
        Log.w("GoogleApiClientConnecting", new StringBuilder(String.valueOf(strValueOf).length() + 23).append("Unexpected callback in ").append(strValueOf).toString());
        Log.w("GoogleApiClientConnecting", new StringBuilder(33).append("mRemainingConnections=").append(this.zzaDe).toString());
        String strValueOf2 = String.valueOf(zzao(this.zzaDd));
        String strValueOf3 = String.valueOf(zzao(i));
        Log.wtf("GoogleApiClientConnecting", new StringBuilder(String.valueOf(strValueOf2).length() + 70 + String.valueOf(strValueOf3).length()).append("GoogleApiClient connecting is in step ").append(strValueOf2).append(" but received callback for step ").append(strValueOf3).toString(), new Exception());
        zze(new ConnectionResult(8, null));
        return false;
    }

    private static String zzao(int i) {
        switch (i) {
            case 0:
                return "STEP_SERVICE_BINDINGS_AND_SIGN_IN";
            case 1:
                return "STEP_GETTING_REMOTE_SERVICE";
            default:
                return "UNKNOWN";
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    /* JADX WARN: Code duplicated, block: B:20:0x003f  */
    /* JADX WARN: Code duplicated, block: B:8:0x0015  */
    public final void zzb(ConnectionResult connectionResult, Api<?> api, boolean z) {
        boolean z2 = true;
        int priority = api.zzpb().getPriority();
        if (z) {
            boolean z3 = connectionResult.hasResolution() || this.zzaCF.zzak(connectionResult.getErrorCode()) != null;
            if (!z3) {
                z2 = false;
            } else if (this.zzaCO != null) {
                z2 = false;
            }
        } else if (this.zzaCO != null && priority >= this.zzaDc) {
            z2 = false;
        }
        if (z2) {
            this.zzaCO = connectionResult;
            this.zzaDc = priority;
        }
        this.zzaCZ.zzaDU.put(api.zzpd(), connectionResult);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final boolean zzd(ConnectionResult connectionResult) {
        return this.zzaDi && !connectionResult.hasResolution();
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zze(ConnectionResult connectionResult) {
        zzqa();
        zzad(!connectionResult.hasResolution());
        this.zzaCZ.zzg(connectionResult);
        this.zzaCZ.zzaDY.zzc(connectionResult);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final boolean zzpW() {
        this.zzaDe--;
        if (this.zzaDe > 0) {
            return false;
        }
        if (this.zzaDe < 0) {
            Log.w("GoogleApiClientConnecting", this.zzaCZ.zzaCl.zzqg());
            Log.wtf("GoogleApiClientConnecting", "GoogleApiClient received too many callbacks for the given step. Clients may be in an unexpected state; GoogleApiClient will now disconnect.", new Exception());
            zze(new ConnectionResult(8, null));
            return false;
        }
        if (this.zzaCO == null) {
            return true;
        }
        this.zzaCZ.zzaDX = this.zzaDc;
        zze(this.zzaCO);
        return false;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzpX() {
        if (this.zzaDe != 0) {
            return;
        }
        if (!this.zzaDj || this.zzaDk) {
            ArrayList arrayList = new ArrayList();
            this.zzaDd = 1;
            this.zzaDe = this.zzaCZ.zzaDF.size();
            for (Api.zzc<?> zzcVar : this.zzaCZ.zzaDF.keySet()) {
                if (!this.zzaCZ.zzaDU.containsKey(zzcVar)) {
                    arrayList.add(this.zzaCZ.zzaDF.get(zzcVar));
                } else if (zzpW()) {
                    zzpY();
                }
            }
            if (arrayList.isEmpty()) {
                return;
            }
            this.zzaDo.add(zzbda.zzqj().submit(new zzbcj(this, arrayList)));
        }
    }

    private final void zzpY() {
        this.zzaCZ.zzqi();
        zzbda.zzqj().execute(new zzbce(this));
        if (this.zzaDh != null) {
            if (this.zzaDm) {
                this.zzaDh.zza(this.zzaDl, this.zzaDn);
            }
            zzad(false);
        }
        Iterator<Api.zzc<?>> it = this.zzaCZ.zzaDU.keySet().iterator();
        while (it.hasNext()) {
            this.zzaCZ.zzaDF.get(it.next()).disconnect();
        }
        this.zzaCZ.zzaDY.zzm(this.zzaDf.isEmpty() ? null : this.zzaDf);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzpZ() {
        this.zzaDj = false;
        this.zzaCZ.zzaCl.zzaDG = Collections.emptySet();
        for (Api.zzc<?> zzcVar : this.zzaDg) {
            if (!this.zzaCZ.zzaDU.containsKey(zzcVar)) {
                this.zzaCZ.zzaDU.put(zzcVar, new ConnectionResult(17, null));
            }
        }
    }

    private final void zzqa() {
        ArrayList<Future<?>> arrayList = this.zzaDo;
        int size = arrayList.size();
        int i = 0;
        while (i < size) {
            Future<?> future = arrayList.get(i);
            i++;
            future.cancel(true);
        }
        this.zzaDo.clear();
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final Set<Scope> zzqb() {
        if (this.zzaCA == null) {
            return Collections.emptySet();
        }
        HashSet hashSet = new HashSet(this.zzaCA.zzrn());
        Map<Api<?>, com.google.android.gms.common.internal.zzr> mapZzrp = this.zzaCA.zzrp();
        for (Api<?> api : mapZzrp.keySet()) {
            if (!this.zzaCZ.zzaDU.containsKey(api.zzpd())) {
                hashSet.addAll(mapZzrp.get(api).zzame);
            }
        }
        return hashSet;
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final void begin() {
        zzbce zzbceVar = null;
        this.zzaCZ.zzaDU.clear();
        this.zzaDj = false;
        this.zzaCO = null;
        this.zzaDd = 0;
        this.zzaDi = true;
        this.zzaDk = false;
        this.zzaDm = false;
        HashMap map = new HashMap();
        boolean z = false;
        for (Api<?> api : this.zzaCD.keySet()) {
            Api.zze zzeVar = this.zzaCZ.zzaDF.get(api.zzpd());
            boolean z2 = (api.zzpb().getPriority() == 1) | z;
            boolean zBooleanValue = this.zzaCD.get(api).booleanValue();
            if (zzeVar.zzmv()) {
                this.zzaDj = true;
                if (zBooleanValue) {
                    this.zzaDg.add(api.zzpd());
                } else {
                    this.zzaDi = false;
                }
            }
            map.put(zzeVar, new zzbcf(this, api, zBooleanValue));
            z = z2;
        }
        if (z) {
            this.zzaDj = false;
        }
        if (this.zzaDj) {
            this.zzaCA.zzc(Integer.valueOf(System.identityHashCode(this.zzaCZ.zzaCl)));
            zzbcm zzbcmVar = new zzbcm(this, zzbceVar);
            this.zzaDh = (zzctk) this.zzaBe.zza(this.mContext, this.zzaCZ.zzaCl.getLooper(), this.zzaCA, this.zzaCA.zzrt(), zzbcmVar, zzbcmVar);
        }
        this.zzaDe = this.zzaCZ.zzaDF.size();
        this.zzaDo.add(zzbda.zzqj().submit(new zzbcg(this, map)));
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final void connect() {
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final boolean disconnect() {
        zzqa();
        zzad(true);
        this.zzaCZ.zzg(null);
        return true;
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final void onConnected(Bundle bundle) {
        if (zzan(1)) {
            if (bundle != null) {
                this.zzaDf.putAll(bundle);
            }
            if (zzpW()) {
                zzpY();
            }
        }
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final void onConnectionSuspended(int i) {
        zze(new ConnectionResult(8, null));
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final void zza(ConnectionResult connectionResult, Api<?> api, boolean z) {
        if (zzan(1)) {
            zzb(connectionResult, api, z);
            if (zzpW()) {
                zzpY();
            }
        }
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final <A extends Api.zzb, R extends Result, T extends zzbay<R, A>> T zzd(T t) {
        this.zzaCZ.zzaCl.zzaCJ.add(t);
        return t;
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final <A extends Api.zzb, T extends zzbay<? extends Result, A>> T zze(T t) {
        throw new IllegalStateException("GoogleApiClient is not connected yet.");
    }
}
