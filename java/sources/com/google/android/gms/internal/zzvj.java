package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import android.os.RemoteException;
import com.google.ads.mediation.admob.AdMobAdapter;
import com.google.android.gms.ads.formats.NativeCustomTemplateAd;
import com.google.android.gms.ads.mediation.MediationAdapter;
import com.google.android.gms.ads.mediation.MediationBannerAdapter;
import com.google.android.gms.ads.mediation.MediationInterstitialAdapter;
import com.google.android.gms.ads.mediation.MediationNativeAdapter;
import com.google.android.gms.ads.mediation.NativeAdMapper;
import com.google.android.gms.ads.mediation.NativeAppInstallAdMapper;
import com.google.android.gms.ads.mediation.NativeContentAdMapper;
import com.google.android.gms.ads.mediation.OnContextChangedListener;
import com.google.android.gms.ads.mediation.OnImmersiveModeUpdatedListener;
import com.google.android.gms.ads.reward.mediation.InitializableMediationRewardedVideoAdAdapter;
import com.google.android.gms.ads.reward.mediation.MediationRewardedVideoAdAdapter;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashSet;
import java.util.Iterator;
import java.util.List;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzvj extends zzuu {
    private final MediationAdapter zzNa;
    private zzvk zzNb;

    public zzvj(MediationAdapter mediationAdapter) {
        this.zzNa = mediationAdapter;
    }

    private final Bundle zza(String str, zzir zzirVar, String str2) throws RemoteException {
        String strValueOf = String.valueOf(str);
        zzajc.zzaT(strValueOf.length() != 0 ? "Server parameters: ".concat(strValueOf) : new String("Server parameters: "));
        try {
            Bundle bundle = new Bundle();
            if (str != null) {
                JSONObject jSONObject = new JSONObject(str);
                Bundle bundle2 = new Bundle();
                Iterator<String> itKeys = jSONObject.keys();
                while (itKeys.hasNext()) {
                    String next = itKeys.next();
                    bundle2.putString(next, jSONObject.getString(next));
                }
                bundle = bundle2;
            }
            if (this.zzNa instanceof AdMobAdapter) {
                bundle.putString("adJson", str2);
                if (zzirVar != null) {
                    bundle.putInt("tagForChildDirectedTreatment", zzirVar.zzzR);
                }
            }
            return bundle;
        } catch (Throwable th) {
            zzajc.zzc("Could not get Server Parameters Bundle.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final void destroy() throws RemoteException {
        try {
            this.zzNa.onDestroy();
        } catch (Throwable th) {
            zzajc.zzc("Could not destroy adapter.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final Bundle getInterstitialAdapterInfo() {
        if (this.zzNa instanceof zzalj) {
            return ((zzalj) this.zzNa).getInterstitialAdapterInfo();
        }
        String strValueOf = String.valueOf(this.zzNa.getClass().getCanonicalName());
        zzajc.zzaT(strValueOf.length() != 0 ? "MediationAdapter is not a v2 MediationInterstitialAdapter: ".concat(strValueOf) : new String("MediationAdapter is not a v2 MediationInterstitialAdapter: "));
        return new Bundle();
    }

    @Override // com.google.android.gms.internal.zzut
    public final zzks getVideoController() {
        if (!(this.zzNa instanceof com.google.android.gms.ads.mediation.zza)) {
            return null;
        }
        try {
            return ((com.google.android.gms.ads.mediation.zza) this.zzNa).getVideoController();
        } catch (Throwable th) {
            zzajc.zzc("Could not get video controller.", th);
            return null;
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final IObjectWrapper getView() throws RemoteException {
        if (!(this.zzNa instanceof MediationBannerAdapter)) {
            String strValueOf = String.valueOf(this.zzNa.getClass().getCanonicalName());
            zzajc.zzaT(strValueOf.length() != 0 ? "MediationAdapter is not a MediationBannerAdapter: ".concat(strValueOf) : new String("MediationAdapter is not a MediationBannerAdapter: "));
            throw new RemoteException();
        }
        try {
            return com.google.android.gms.dynamic.zzn.zzw(((MediationBannerAdapter) this.zzNa).getBannerView());
        } catch (Throwable th) {
            zzajc.zzc("Could not get banner view from adapter.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final boolean isInitialized() throws RemoteException {
        if (!(this.zzNa instanceof MediationRewardedVideoAdAdapter)) {
            String strValueOf = String.valueOf(this.zzNa.getClass().getCanonicalName());
            zzajc.zzaT(strValueOf.length() != 0 ? "MediationAdapter is not a MediationRewardedVideoAdAdapter: ".concat(strValueOf) : new String("MediationAdapter is not a MediationRewardedVideoAdAdapter: "));
            throw new RemoteException();
        }
        zzajc.zzaC("Check if adapter is initialized.");
        try {
            return ((MediationRewardedVideoAdAdapter) this.zzNa).isInitialized();
        } catch (Throwable th) {
            zzajc.zzc("Could not check if adapter is initialized.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final void pause() throws RemoteException {
        try {
            this.zzNa.onPause();
        } catch (Throwable th) {
            zzajc.zzc("Could not pause adapter.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final void resume() throws RemoteException {
        try {
            this.zzNa.onResume();
        } catch (Throwable th) {
            zzajc.zzc("Could not resume adapter.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final void setImmersiveMode(boolean z) throws RemoteException {
        if (!(this.zzNa instanceof OnImmersiveModeUpdatedListener)) {
            String strValueOf = String.valueOf(this.zzNa.getClass().getCanonicalName());
            zzajc.zzaS(strValueOf.length() != 0 ? "MediationAdapter is not an OnImmersiveModeUpdatedListener: ".concat(strValueOf) : new String("MediationAdapter is not an OnImmersiveModeUpdatedListener: "));
        } else {
            try {
                ((OnImmersiveModeUpdatedListener) this.zzNa).onImmersiveModeUpdated(z);
            } catch (Throwable th) {
                zzajc.zzc("Could not set immersive mode.", th);
            }
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final void showInterstitial() throws RemoteException {
        if (!(this.zzNa instanceof MediationInterstitialAdapter)) {
            String strValueOf = String.valueOf(this.zzNa.getClass().getCanonicalName());
            zzajc.zzaT(strValueOf.length() != 0 ? "MediationAdapter is not a MediationInterstitialAdapter: ".concat(strValueOf) : new String("MediationAdapter is not a MediationInterstitialAdapter: "));
            throw new RemoteException();
        }
        zzajc.zzaC("Showing interstitial from adapter.");
        try {
            ((MediationInterstitialAdapter) this.zzNa).showInterstitial();
        } catch (Throwable th) {
            zzajc.zzc("Could not show interstitial from adapter.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final void showVideo() throws RemoteException {
        if (!(this.zzNa instanceof MediationRewardedVideoAdAdapter)) {
            String strValueOf = String.valueOf(this.zzNa.getClass().getCanonicalName());
            zzajc.zzaT(strValueOf.length() != 0 ? "MediationAdapter is not a MediationRewardedVideoAdAdapter: ".concat(strValueOf) : new String("MediationAdapter is not a MediationRewardedVideoAdAdapter: "));
            throw new RemoteException();
        }
        zzajc.zzaC("Show rewarded video ad from adapter.");
        try {
            ((MediationRewardedVideoAdAdapter) this.zzNa).showVideo();
        } catch (Throwable th) {
            zzajc.zzc("Could not show rewarded video ad from adapter.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zzaea zzaeaVar, List<String> list) throws RemoteException {
        if (!(this.zzNa instanceof InitializableMediationRewardedVideoAdAdapter)) {
            String strValueOf = String.valueOf(this.zzNa.getClass().getCanonicalName());
            zzajc.zzaT(strValueOf.length() != 0 ? "MediationAdapter is not an InitializableMediationRewardedVideoAdAdapter: ".concat(strValueOf) : new String("MediationAdapter is not an InitializableMediationRewardedVideoAdAdapter: "));
            throw new RemoteException();
        }
        zzajc.zzaC("Initialize rewarded video adapter.");
        try {
            InitializableMediationRewardedVideoAdAdapter initializableMediationRewardedVideoAdAdapter = (InitializableMediationRewardedVideoAdAdapter) this.zzNa;
            ArrayList arrayList = new ArrayList();
            Iterator<String> it = list.iterator();
            while (it.hasNext()) {
                arrayList.add(zza(it.next(), (zzir) null, (String) null));
            }
            initializableMediationRewardedVideoAdAdapter.initialize((Context) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper), new zzaed(zzaeaVar), arrayList);
        } catch (Throwable th) {
            zzajc.zzc("Could not initialize rewarded video adapter.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zzir zzirVar, String str, zzaea zzaeaVar, String str2) throws RemoteException {
        Bundle bundle;
        zzvi zzviVar;
        if (!(this.zzNa instanceof MediationRewardedVideoAdAdapter)) {
            String strValueOf = String.valueOf(this.zzNa.getClass().getCanonicalName());
            zzajc.zzaT(strValueOf.length() != 0 ? "MediationAdapter is not a MediationRewardedVideoAdAdapter: ".concat(strValueOf) : new String("MediationAdapter is not a MediationRewardedVideoAdAdapter: "));
            throw new RemoteException();
        }
        zzajc.zzaC("Initialize rewarded video adapter.");
        try {
            MediationRewardedVideoAdAdapter mediationRewardedVideoAdAdapter = (MediationRewardedVideoAdAdapter) this.zzNa;
            Bundle bundleZza = zza(str2, zzirVar, (String) null);
            if (zzirVar != null) {
                zzvi zzviVar2 = new zzvi(zzirVar.zzzN == -1 ? null : new Date(zzirVar.zzzN), zzirVar.zzzO, zzirVar.zzzP != null ? new HashSet(zzirVar.zzzP) : null, zzirVar.zzzV, zzirVar.zzzQ, zzirVar.zzzR, zzirVar.zzAc);
                if (zzirVar.zzzX != null) {
                    bundle = zzirVar.zzzX.getBundle(mediationRewardedVideoAdAdapter.getClass().getName());
                    zzviVar = zzviVar2;
                } else {
                    bundle = null;
                    zzviVar = zzviVar2;
                }
            } else {
                bundle = null;
                zzviVar = null;
            }
            mediationRewardedVideoAdAdapter.initialize((Context) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper), zzviVar, str, new zzaed(zzaeaVar), bundleZza, bundle);
        } catch (Throwable th) {
            zzajc.zzc("Could not initialize rewarded video adapter.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zzir zzirVar, String str, zzuw zzuwVar) throws RemoteException {
        zza(iObjectWrapper, zzirVar, str, (String) null, zzuwVar);
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zzir zzirVar, String str, String str2, zzuw zzuwVar) throws RemoteException {
        if (!(this.zzNa instanceof MediationInterstitialAdapter)) {
            String strValueOf = String.valueOf(this.zzNa.getClass().getCanonicalName());
            zzajc.zzaT(strValueOf.length() != 0 ? "MediationAdapter is not a MediationInterstitialAdapter: ".concat(strValueOf) : new String("MediationAdapter is not a MediationInterstitialAdapter: "));
            throw new RemoteException();
        }
        zzajc.zzaC("Requesting interstitial ad from adapter.");
        try {
            MediationInterstitialAdapter mediationInterstitialAdapter = (MediationInterstitialAdapter) this.zzNa;
            mediationInterstitialAdapter.requestInterstitialAd((Context) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper), new zzvk(zzuwVar), zza(str, zzirVar, str2), new zzvi(zzirVar.zzzN == -1 ? null : new Date(zzirVar.zzzN), zzirVar.zzzO, zzirVar.zzzP != null ? new HashSet(zzirVar.zzzP) : null, zzirVar.zzzV, zzirVar.zzzQ, zzirVar.zzzR, zzirVar.zzAc), zzirVar.zzzX != null ? zzirVar.zzzX.getBundle(mediationInterstitialAdapter.getClass().getName()) : null);
        } catch (Throwable th) {
            zzajc.zzc("Could not request interstitial ad from adapter.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zzir zzirVar, String str, String str2, zzuw zzuwVar, zzon zzonVar, List<String> list) throws RemoteException {
        if (!(this.zzNa instanceof MediationNativeAdapter)) {
            String strValueOf = String.valueOf(this.zzNa.getClass().getCanonicalName());
            zzajc.zzaT(strValueOf.length() != 0 ? "MediationAdapter is not a MediationNativeAdapter: ".concat(strValueOf) : new String("MediationAdapter is not a MediationNativeAdapter: "));
            throw new RemoteException();
        }
        try {
            MediationNativeAdapter mediationNativeAdapter = (MediationNativeAdapter) this.zzNa;
            zzvn zzvnVar = new zzvn(zzirVar.zzzN == -1 ? null : new Date(zzirVar.zzzN), zzirVar.zzzO, zzirVar.zzzP != null ? new HashSet(zzirVar.zzzP) : null, zzirVar.zzzV, zzirVar.zzzQ, zzirVar.zzzR, zzonVar, list, zzirVar.zzAc);
            Bundle bundle = zzirVar.zzzX != null ? zzirVar.zzzX.getBundle(mediationNativeAdapter.getClass().getName()) : null;
            this.zzNb = new zzvk(zzuwVar);
            mediationNativeAdapter.requestNativeAd((Context) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper), this.zzNb, zza(str, zzirVar, str2), zzvnVar, bundle);
        } catch (Throwable th) {
            zzajc.zzc("Could not request native ad from adapter.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zziv zzivVar, zzir zzirVar, String str, zzuw zzuwVar) throws RemoteException {
        zza(iObjectWrapper, zzivVar, zzirVar, str, null, zzuwVar);
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(IObjectWrapper iObjectWrapper, zziv zzivVar, zzir zzirVar, String str, String str2, zzuw zzuwVar) throws RemoteException {
        if (!(this.zzNa instanceof MediationBannerAdapter)) {
            String strValueOf = String.valueOf(this.zzNa.getClass().getCanonicalName());
            zzajc.zzaT(strValueOf.length() != 0 ? "MediationAdapter is not a MediationBannerAdapter: ".concat(strValueOf) : new String("MediationAdapter is not a MediationBannerAdapter: "));
            throw new RemoteException();
        }
        zzajc.zzaC("Requesting banner ad from adapter.");
        try {
            MediationBannerAdapter mediationBannerAdapter = (MediationBannerAdapter) this.zzNa;
            mediationBannerAdapter.requestBannerAd((Context) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper), new zzvk(zzuwVar), zza(str, zzirVar, str2), com.google.android.gms.ads.zzb.zza(zzivVar.width, zzivVar.height, zzivVar.zzAs), new zzvi(zzirVar.zzzN == -1 ? null : new Date(zzirVar.zzzN), zzirVar.zzzO, zzirVar.zzzP != null ? new HashSet(zzirVar.zzzP) : null, zzirVar.zzzV, zzirVar.zzzQ, zzirVar.zzzR, zzirVar.zzAc), zzirVar.zzzX != null ? zzirVar.zzzX.getBundle(mediationBannerAdapter.getClass().getName()) : null);
        } catch (Throwable th) {
            zzajc.zzc("Could not request banner ad from adapter.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zza(zzir zzirVar, String str, String str2) throws RemoteException {
        if (!(this.zzNa instanceof MediationRewardedVideoAdAdapter)) {
            String strValueOf = String.valueOf(this.zzNa.getClass().getCanonicalName());
            zzajc.zzaT(strValueOf.length() != 0 ? "MediationAdapter is not a MediationRewardedVideoAdAdapter: ".concat(strValueOf) : new String("MediationAdapter is not a MediationRewardedVideoAdAdapter: "));
            throw new RemoteException();
        }
        zzajc.zzaC("Requesting rewarded video ad from adapter.");
        try {
            MediationRewardedVideoAdAdapter mediationRewardedVideoAdAdapter = (MediationRewardedVideoAdAdapter) this.zzNa;
            mediationRewardedVideoAdAdapter.loadAd(new zzvi(zzirVar.zzzN == -1 ? null : new Date(zzirVar.zzzN), zzirVar.zzzO, zzirVar.zzzP != null ? new HashSet(zzirVar.zzzP) : null, zzirVar.zzzV, zzirVar.zzzQ, zzirVar.zzzR, zzirVar.zzAc), zza(str, zzirVar, str2), zzirVar.zzzX != null ? zzirVar.zzzX.getBundle(mediationRewardedVideoAdAdapter.getClass().getName()) : null);
        } catch (Throwable th) {
            zzajc.zzc("Could not load rewarded video ad from adapter.", th);
            throw new RemoteException();
        }
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zzc(zzir zzirVar, String str) throws RemoteException {
        zza(zzirVar, str, (String) null);
    }

    @Override // com.google.android.gms.internal.zzut
    public final zzvc zzfq() {
        NativeAdMapper nativeAdMapperZzfx = this.zzNb.zzfx();
        if (nativeAdMapperZzfx instanceof NativeAppInstallAdMapper) {
            return new zzvl((NativeAppInstallAdMapper) nativeAdMapperZzfx);
        }
        return null;
    }

    @Override // com.google.android.gms.internal.zzut
    public final zzvf zzfr() {
        NativeAdMapper nativeAdMapperZzfx = this.zzNb.zzfx();
        if (nativeAdMapperZzfx instanceof NativeContentAdMapper) {
            return new zzvm((NativeContentAdMapper) nativeAdMapperZzfx);
        }
        return null;
    }

    @Override // com.google.android.gms.internal.zzut
    public final Bundle zzfs() {
        if (this.zzNa instanceof zzali) {
            return ((zzali) this.zzNa).zzfs();
        }
        String strValueOf = String.valueOf(this.zzNa.getClass().getCanonicalName());
        zzajc.zzaT(strValueOf.length() != 0 ? "MediationAdapter is not a v2 MediationBannerAdapter: ".concat(strValueOf) : new String("MediationAdapter is not a v2 MediationBannerAdapter: "));
        return new Bundle();
    }

    @Override // com.google.android.gms.internal.zzut
    public final Bundle zzft() {
        return new Bundle();
    }

    @Override // com.google.android.gms.internal.zzut
    public final boolean zzfu() {
        return this.zzNa instanceof InitializableMediationRewardedVideoAdAdapter;
    }

    @Override // com.google.android.gms.internal.zzut
    public final zzpj zzfv() {
        NativeCustomTemplateAd nativeCustomTemplateAdZzfy = this.zzNb.zzfy();
        if (nativeCustomTemplateAdZzfy instanceof zzpm) {
            return ((zzpm) nativeCustomTemplateAdZzfy).zzex();
        }
        return null;
    }

    @Override // com.google.android.gms.internal.zzut
    public final void zzk(IObjectWrapper iObjectWrapper) throws RemoteException {
        try {
            ((OnContextChangedListener) this.zzNa).onContextChanged((Context) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper));
        } catch (Throwable th) {
            zzajc.zza("Could not inform adapter of changed context", th);
        }
    }
}
