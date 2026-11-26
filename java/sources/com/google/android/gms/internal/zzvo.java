package com.google.android.gms.internal;

import android.app.Activity;
import android.os.Bundle;
import android.os.RemoteException;
import com.google.ads.mediation.MediationAdapter;
import com.google.ads.mediation.MediationBannerAdapter;
import com.google.ads.mediation.MediationInterstitialAdapter;
import com.google.ads.mediation.MediationServerParameters;
import com.google.ads.mediation.NetworkExtras;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzvo<NETWORK_EXTRAS extends NetworkExtras, SERVER_PARAMETERS extends MediationServerParameters> extends zzuu {
    private final MediationAdapter<NETWORK_EXTRAS, SERVER_PARAMETERS> zzNi;
    private final NETWORK_EXTRAS zzNj;

    public zzvo(MediationAdapter<NETWORK_EXTRAS, SERVER_PARAMETERS> mediationAdapter, NETWORK_EXTRAS network_extras) {
        this.zzNi = mediationAdapter;
        this.zzNj = network_extras;
    }

    private final SERVER_PARAMETERS zza(String str, int i, String str2) throws RemoteException {
        HashMap map;
        try {
            if (str != null) {
                JSONObject jSONObject = new JSONObject(str);
                map = new HashMap(jSONObject.length());
                Iterator<String> itKeys = jSONObject.keys();
                while (itKeys.hasNext()) {
                    String next = itKeys.next();
                    map.put(next, jSONObject.getString(next));
                }
            } else {
                map = new HashMap(0);
            }
            Class<SERVER_PARAMETERS> serverParametersType = this.zzNi.getServerParametersType();
            if (serverParametersType == null) {
                return null;
            }
            SERVER_PARAMETERS server_parametersNewInstance = serverParametersType.newInstance();
            server_parametersNewInstance.load(map);
            return server_parametersNewInstance;
        } catch (Throwable th) {
            zzajc.zzc("Could not get MediationServerParameters.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final void destroy() throws RemoteException {
        try {
            this.zzNi.destroy();
        } catch (Throwable th) {
            zzajc.zzc("Could not destroy adapter.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final Bundle getInterstitialAdapterInfo() {
        return new Bundle();
    }

    @Override // com.google.android.gms.internal.zzut
    public final zzks getVideoController() {
        return null;
    }

    @Override // com.google.android.gms.internal.zzut
    public final IObjectWrapper getView() throws RemoteException {
        if (!(this.zzNi instanceof MediationBannerAdapter)) {
            String strValueOf = String.valueOf(this.zzNi.getClass().getCanonicalName());
            zzajc.zzaT(strValueOf.length() != 0 ? "MediationAdapter is not a MediationBannerAdapter: ".concat(strValueOf) : new String("MediationAdapter is not a MediationBannerAdapter: "));
            throw new RemoteException();
        }
        try {
            return com.google.android.gms.dynamic.zzn.zzw(((MediationBannerAdapter) this.zzNi).getBannerView());
        } catch (Throwable th) {
            zzajc.zzc("Could not get banner view from adapter.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final boolean isInitialized() {
        return true;
    }

    @Override // com.google.android.gms.internal.zzut
    public final void pause() throws RemoteException {
        throw new RemoteException();
    }

    @Override // com.google.android.gms.internal.zzut
    public final void resume() throws RemoteException {
        throw new RemoteException();
    }

    @Override // com.google.android.gms.internal.zzut
    public final void setImmersiveMode(boolean z) {
    }

    @Override // com.google.android.gms.internal.zzut
    public final void showInterstitial() throws RemoteException {
        if (!(this.zzNi instanceof MediationInterstitialAdapter)) {
            String strValueOf = String.valueOf(this.zzNi.getClass().getCanonicalName());
            zzajc.zzaT(strValueOf.length() != 0 ? "MediationAdapter is not a MediationInterstitialAdapter: ".concat(strValueOf) : new String("MediationAdapter is not a MediationInterstitialAdapter: "));
            throw new RemoteException();
        }
        zzajc.zzaC("Showing interstitial from adapter.");
        try {
            ((MediationInterstitialAdapter) this.zzNi).showInterstitial();
        } catch (Throwable th) {
            zzajc.zzc("Could not show interstitial from adapter.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final void showVideo() {
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zzaea zzaeaVar, List<String> list) {
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zzir zzirVar, String str, zzaea zzaeaVar, String str2) throws RemoteException {
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zzir zzirVar, String str, zzuw zzuwVar) throws RemoteException {
        zza(iObjectWrapper, zzirVar, str, (String) null, zzuwVar);
    }

    /* JADX WARN: Multi-variable type inference failed */
    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zzir zzirVar, String str, String str2, zzuw zzuwVar) throws RemoteException {
        if (!(this.zzNi instanceof MediationInterstitialAdapter)) {
            String strValueOf = String.valueOf(this.zzNi.getClass().getCanonicalName());
            zzajc.zzaT(strValueOf.length() != 0 ? "MediationAdapter is not a MediationInterstitialAdapter: ".concat(strValueOf) : new String("MediationAdapter is not a MediationInterstitialAdapter: "));
            throw new RemoteException();
        }
        zzajc.zzaC("Requesting interstitial ad from adapter.");
        try {
            ((MediationInterstitialAdapter) this.zzNi).requestInterstitialAd(new zzvp(zzuwVar), (Activity) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper), zza(str, zzirVar.zzzR, str2), zzwb.zzn(zzirVar), this.zzNj);
        } catch (Throwable th) {
            zzajc.zzc("Could not request interstitial ad from adapter.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zzir zzirVar, String str, String str2, zzuw zzuwVar, zzon zzonVar, List<String> list) {
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zziv zzivVar, zzir zzirVar, String str, zzuw zzuwVar) throws RemoteException {
        zza(iObjectWrapper, zzivVar, zzirVar, str, null, zzuwVar);
    }

    /* JADX WARN: Multi-variable type inference failed */
    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zziv zzivVar, zzir zzirVar, String str, String str2, zzuw zzuwVar) throws RemoteException {
        if (!(this.zzNi instanceof MediationBannerAdapter)) {
            String strValueOf = String.valueOf(this.zzNi.getClass().getCanonicalName());
            zzajc.zzaT(strValueOf.length() != 0 ? "MediationAdapter is not a MediationBannerAdapter: ".concat(strValueOf) : new String("MediationAdapter is not a MediationBannerAdapter: "));
            throw new RemoteException();
        }
        zzajc.zzaC("Requesting banner ad from adapter.");
        try {
            ((MediationBannerAdapter) this.zzNi).requestBannerAd(new zzvp(zzuwVar), (Activity) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper), zza(str, zzirVar.zzzR, str2), zzwb.zzb(zzivVar), zzwb.zzn(zzirVar), this.zzNj);
        } catch (Throwable th) {
            zzajc.zzc("Could not request banner ad from adapter.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(zzir zzirVar, String str, String str2) {
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zzc(zzir zzirVar, String str) {
    }

    @Override // com.google.android.gms.internal.zzut
    public final zzvc zzfq() {
        return null;
    }

    @Override // com.google.android.gms.internal.zzut
    public final zzvf zzfr() {
        return null;
    }

    @Override // com.google.android.gms.internal.zzut
    public final Bundle zzfs() {
        return new Bundle();
    }

    @Override // com.google.android.gms.internal.zzut
    public final Bundle zzft() {
        return new Bundle();
    }

    @Override // com.google.android.gms.internal.zzut
    public final boolean zzfu() {
        return false;
    }

    @Override // com.google.android.gms.internal.zzut
    public final zzpj zzfv() {
        return null;
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zzk(IObjectWrapper iObjectWrapper) throws RemoteException {
    }
}
