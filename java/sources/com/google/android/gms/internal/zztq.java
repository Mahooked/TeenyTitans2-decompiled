package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;
import android.support.annotation.Nullable;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zztq extends zzka {
    private final zzti zzKI;
    private final zzsi zzKq;

    @Nullable
    private com.google.android.gms.ads.internal.zzal zzKv;
    private final String zztV;
    private boolean zzuj;

    public zztq(Context context, String str, zzuq zzuqVar, zzaje zzajeVar, com.google.android.gms.ads.internal.zzv zzvVar) {
        this(str, new zzsi(context, zzuqVar, zzajeVar, zzvVar));
    }

    private zztq(String str, zzsi zzsiVar) {
        this.zztV = str;
        this.zzKq = zzsiVar;
        this.zzKI = new zzti();
        com.google.android.gms.ads.internal.zzbs.zzbN().zza(zzsiVar);
    }

    private final void abort() {
        if (this.zzKv != null) {
            return;
        }
        this.zzKv = this.zzKq.zzW(this.zztV);
        this.zzKI.zzd(this.zzKv);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void destroy() throws RemoteException {
        if (this.zzKv != null) {
            this.zzKv.destroy();
        }
    }

    @Override // com.google.android.gms.internal.zzjz
    public final String getAdUnitId() {
        throw new IllegalStateException("getAdUnitId not implemented");
    }

    @Override // com.google.android.gms.internal.zzjz
    @Nullable
    public final String getMediationAdapterClassName() throws RemoteException {
        if (this.zzKv != null) {
            return this.zzKv.getMediationAdapterClassName();
        }
        return null;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final zzks getVideoController() {
        throw new IllegalStateException("getVideoController not implemented for interstitials");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final boolean isLoading() throws RemoteException {
        return this.zzKv != null && this.zzKv.isLoading();
    }

    @Override // com.google.android.gms.internal.zzjz
    public final boolean isReady() throws RemoteException {
        return this.zzKv != null && this.zzKv.isReady();
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void pause() throws RemoteException {
        if (this.zzKv != null) {
            this.zzKv.pause();
        }
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void resume() throws RemoteException {
        if (this.zzKv != null) {
            this.zzKv.resume();
        }
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void setImmersiveMode(boolean z) {
        this.zzuj = z;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void setManualImpressionsEnabled(boolean z) throws RemoteException {
        abort();
        if (this.zzKv != null) {
            this.zzKv.setManualImpressionsEnabled(z);
        }
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void setUserId(String str) {
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void showInterstitial() throws RemoteException {
        if (this.zzKv == null) {
            zzafr.zzaT("Interstitial ad must be loaded before showInterstitial().");
        } else {
            this.zzKv.setImmersiveMode(this.zzuj);
            this.zzKv.showInterstitial();
        }
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void stopLoading() throws RemoteException {
        if (this.zzKv != null) {
            this.zzKv.stopLoading();
        }
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzadd zzaddVar) {
        this.zzKI.zzKl = zzaddVar;
        if (this.zzKv != null) {
            this.zzKI.zzd(this.zzKv);
        }
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zziv zzivVar) throws RemoteException {
        if (this.zzKv != null) {
            this.zzKv.zza(zzivVar);
        }
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzjl zzjlVar) throws RemoteException {
        this.zzKI.zzKk = zzjlVar;
        if (this.zzKv != null) {
            this.zzKI.zzd(this.zzKv);
        }
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzjo zzjoVar) throws RemoteException {
        this.zzKI.zztK = zzjoVar;
        if (this.zzKv != null) {
            this.zzKI.zzd(this.zzKv);
        }
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzke zzkeVar) throws RemoteException {
        this.zzKI.zzKi = zzkeVar;
        if (this.zzKv != null) {
            this.zzKI.zzd(this.zzKv);
        }
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzkk zzkkVar) throws RemoteException {
        abort();
        if (this.zzKv != null) {
            this.zzKv.zza(zzkkVar);
        }
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzky zzkyVar) {
        throw new IllegalStateException("Unused method");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzlx zzlxVar) {
        throw new IllegalStateException("getVideoController not implemented for interstitials");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zznh zznhVar) throws RemoteException {
        this.zzKI.zzKj = zznhVar;
        if (this.zzKv != null) {
            this.zzKI.zzd(this.zzKv);
        }
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzxg zzxgVar) throws RemoteException {
        zzafr.zzaT("setInAppPurchaseListener is deprecated and should not be called.");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzxo zzxoVar, String str) throws RemoteException {
        zzafr.zzaT("setPlayStorePurchaseParams is deprecated and should not be called.");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final boolean zza(zzir zzirVar) throws RemoteException {
        if (!zztl.zzi(zzirVar).contains("gw")) {
            abort();
        }
        if (zztl.zzi(zzirVar).contains("_skipMediation")) {
            abort();
        }
        if (zzirVar.zzzU != null) {
            abort();
        }
        if (this.zzKv != null) {
            return this.zzKv.zza(zzirVar);
        }
        zztl zztlVarZzbN = com.google.android.gms.ads.internal.zzbs.zzbN();
        if (zztl.zzi(zzirVar).contains("_ad")) {
            zztlVarZzbN.zzb(zzirVar, this.zztV);
        }
        zzto zztoVarZza = zztlVarZzbN.zza(zzirVar, this.zztV);
        if (zztoVarZza == null) {
            abort();
            zztp.zzeN().zzeR();
            return this.zzKv.zza(zzirVar);
        }
        if (zztoVarZza.zzKz) {
            zztp.zzeN().zzeQ();
        } else {
            zztoVarZza.load();
            zztp.zzeN().zzeR();
        }
        this.zzKv = zztoVarZza.zzKv;
        zztoVarZza.zzKx.zza(this.zzKI);
        this.zzKI.zzd(this.zzKv);
        return zztoVarZza.zzKA;
    }

    @Override // com.google.android.gms.internal.zzjz
    @Nullable
    public final String zzaI() throws RemoteException {
        if (this.zzKv != null) {
            return this.zzKv.zzaI();
        }
        return null;
    }

    @Override // com.google.android.gms.internal.zzjz
    @Nullable
    public final IObjectWrapper zzal() throws RemoteException {
        if (this.zzKv != null) {
            return this.zzKv.zzal();
        }
        return null;
    }

    @Override // com.google.android.gms.internal.zzjz
    @Nullable
    public final zziv zzam() throws RemoteException {
        if (this.zzKv != null) {
            return this.zzKv.zzam();
        }
        return null;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zzao() throws RemoteException {
        if (this.zzKv != null) {
            this.zzKv.zzao();
        } else {
            zzafr.zzaT("Interstitial ad must be loaded before pingManualTrackingUrl().");
        }
    }

    @Override // com.google.android.gms.internal.zzjz
    public final zzke zzax() {
        throw new IllegalStateException("getIAppEventListener not implemented");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final zzjo zzay() {
        throw new IllegalStateException("getIAdListener not implemented");
    }
}
