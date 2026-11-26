package com.google.android.gms.ads.internal;

import android.os.RemoteException;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import android.view.View;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.internal.zzacq;
import com.google.android.gms.internal.zzadd;
import com.google.android.gms.internal.zzaee;
import com.google.android.gms.internal.zzaez;
import com.google.android.gms.internal.zzaff;
import com.google.android.gms.internal.zzafg;
import com.google.android.gms.internal.zzafh;
import com.google.android.gms.internal.zzafm;
import com.google.android.gms.internal.zzafn;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzaiy;
import com.google.android.gms.internal.zzfh;
import com.google.android.gms.internal.zzim;
import com.google.android.gms.internal.zzir;
import com.google.android.gms.internal.zzis;
import com.google.android.gms.internal.zziv;
import com.google.android.gms.internal.zzji;
import com.google.android.gms.internal.zzjl;
import com.google.android.gms.internal.zzjo;
import com.google.android.gms.internal.zzka;
import com.google.android.gms.internal.zzke;
import com.google.android.gms.internal.zzkk;
import com.google.android.gms.internal.zzks;
import com.google.android.gms.internal.zzky;
import com.google.android.gms.internal.zzlv;
import com.google.android.gms.internal.zzlx;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zzmz;
import com.google.android.gms.internal.zznb;
import com.google.android.gms.internal.zznh;
import com.google.android.gms.internal.zzqk;
import com.google.android.gms.internal.zzxg;
import com.google.android.gms.internal.zzxo;
import com.google.android.gms.internal.zzxy;
import com.google.android.gms.internal.zzzn;
import com.google.android.gms.internal.zzzp;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.Iterator;
import java.util.List;
import java.util.Timer;
import java.util.concurrent.CountDownLatch;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public abstract class zza extends zzka implements com.google.android.gms.ads.internal.overlay.zzag, zzafm, zzim, zzqk, zzxy, zzzp {
    protected zznb zzsK;
    private zzmz zzsL;
    private zzmz zzsM;
    protected boolean zzsN = false;
    protected final zzbi zzsO = new zzbi(this);
    protected final zzbt zzsP;

    @Nullable
    protected transient zzir zzsQ;
    protected final zzfh zzsR;
    protected final zzv zzsS;

    zza(zzbt zzbtVar, @Nullable zzbi zzbiVar, zzv zzvVar) {
        this.zzsP = zzbtVar;
        this.zzsS = zzvVar;
        zzbs.zzbz().zzE(this.zzsP.zzqD);
        zzbs.zzbD().zzd(this.zzsP.zzqD, this.zzsP.zzvT);
        zzbs.zzbE().initialize(this.zzsP.zzqD);
        this.zzsR = zzbs.zzbD().zzhG();
        zzbs.zzbC().initialize(this.zzsP.zzqD);
        if (((Boolean) zzbs.zzbL().zzd(zzmo.zzFE)).booleanValue()) {
            Timer timer = new Timer();
            timer.schedule(new zzb(this, new CountDownLatch(((Integer) zzbs.zzbL().zzd(zzmo.zzFG)).intValue()), timer), 0L, ((Long) zzbs.zzbL().zzd(zzmo.zzFF)).longValue());
        }
    }

    private static long zzr(String str) {
        int iIndexOf = str.indexOf("ufe");
        int iIndexOf2 = str.indexOf(44, iIndexOf);
        if (iIndexOf2 == -1) {
            iIndexOf2 = str.length();
        }
        try {
            return Long.parseLong(str.substring(iIndexOf + 4, iIndexOf2));
        } catch (IndexOutOfBoundsException e) {
            zzafr.zzaT("Invalid index for Url fetch time in CSI latency info.");
            return -1L;
        } catch (NumberFormatException e2) {
            zzafr.zzaT("Cannot find valid format of Url fetch time in CSI latency info.");
            return -1L;
        }
    }

    @Override // com.google.android.gms.internal.zzjz
    public void destroy() {
        com.google.android.gms.common.internal.zzbo.zzcz("destroy must be called on the main UI thread.");
        this.zzsO.cancel();
        this.zzsR.zzh(this.zzsP.zzvY);
        zzbt zzbtVar = this.zzsP;
        if (zzbtVar.zzvU != null) {
            zzbtVar.zzvU.zzcg();
        }
        zzbtVar.zzwc = null;
        zzbtVar.zzwd = null;
        zzbtVar.zzwo = null;
        zzbtVar.zzwe = null;
        zzbtVar.zze(false);
        if (zzbtVar.zzvU != null) {
            zzbtVar.zzvU.removeAllViews();
        }
        zzbtVar.zzca();
        zzbtVar.zzcb();
        zzbtVar.zzvY = null;
    }

    @Override // com.google.android.gms.internal.zzjz
    public String getAdUnitId() {
        return this.zzsP.zzvR;
    }

    @Override // com.google.android.gms.internal.zzjz
    public zzks getVideoController() {
        return null;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final boolean isLoading() {
        return this.zzsN;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final boolean isReady() {
        com.google.android.gms.common.internal.zzbo.zzcz("isLoaded must be called on the main UI thread.");
        return this.zzsP.zzvV == null && this.zzsP.zzvW == null && this.zzsP.zzvY != null;
    }

    @Override // com.google.android.gms.internal.zzim
    public void onAdClicked() {
        if (this.zzsP.zzvY == null) {
            zzafr.zzaT("Ad state was null when trying to ping click URLs.");
            return;
        }
        zzafr.zzaC("Pinging click URLs.");
        if (this.zzsP.zzwa != null) {
            this.zzsP.zzwa.zzhd();
        }
        if (this.zzsP.zzvY.zzMa != null) {
            zzbs.zzbz();
            zzagz.zza(this.zzsP.zzqD, this.zzsP.zzvT.zzaP, zzb(this.zzsP.zzvY.zzMa));
        }
        if (this.zzsP.zzwb != null) {
            try {
                this.zzsP.zzwb.onAdClicked();
            } catch (RemoteException e) {
                zzafr.zzc("Could not notify onAdClicked event.", e);
            }
        }
    }

    @Override // com.google.android.gms.internal.zzqk
    public final void onAppEvent(String str, @Nullable String str2) {
        if (this.zzsP.zzwd != null) {
            try {
                this.zzsP.zzwd.onAppEvent(str, str2);
            } catch (RemoteException e) {
                zzafr.zzc("Could not call the AppEventListener.", e);
            }
        }
    }

    @Override // com.google.android.gms.internal.zzjz
    public void pause() {
        com.google.android.gms.common.internal.zzbo.zzcz("pause must be called on the main UI thread.");
    }

    @Override // com.google.android.gms.internal.zzjz
    public void resume() {
        com.google.android.gms.common.internal.zzbo.zzcz("resume must be called on the main UI thread.");
    }

    @Override // com.google.android.gms.internal.zzjz
    public void setImmersiveMode(boolean z) {
        throw new IllegalStateException("onImmersiveModeUpdated is not supported for current ad type");
    }

    @Override // com.google.android.gms.internal.zzjz
    public void setManualImpressionsEnabled(boolean z) {
        throw new UnsupportedOperationException("Attempt to call setManualImpressionsEnabled for an unsupported ad type.");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void setUserId(String str) {
        zzafr.zzaT("RewardedVideoAd.setUserId() is deprecated. Please do not call this method.");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void stopLoading() {
        com.google.android.gms.common.internal.zzbo.zzcz("stopLoading must be called on the main UI thread.");
        this.zzsN = false;
        this.zzsP.zze(true);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzadd zzaddVar) {
        com.google.android.gms.common.internal.zzbo.zzcz("setRewardedVideoAdListener can only be called from the UI thread.");
        this.zzsP.zzwp = zzaddVar;
    }

    protected final void zza(@Nullable zzaee zzaeeVar) {
        if (this.zzsP.zzwp == null) {
            return;
        }
        String str = "";
        int i = 1;
        if (zzaeeVar != null) {
            try {
                str = zzaeeVar.type;
                i = zzaeeVar.zzWW;
            } catch (RemoteException e) {
                zzafr.zzc("Could not call RewardedVideoAdListener.onRewarded().", e);
                return;
            }
        }
        this.zzsP.zzwp.zza(new zzacq(str, i));
    }

    @Override // com.google.android.gms.internal.zzzp
    public final void zza(zzafg zzafgVar) {
        if (zzafgVar.zzXY.zzTs != -1 && !TextUtils.isEmpty(zzafgVar.zzXY.zzTB)) {
            long jZzr = zzr(zzafgVar.zzXY.zzTB);
            if (jZzr != -1) {
                this.zzsK.zza(this.zzsK.zzc(jZzr + zzafgVar.zzXY.zzTs), "stc");
            }
        }
        this.zzsK.zzO(zzafgVar.zzXY.zzTB);
        this.zzsK.zza(this.zzsL, "arf");
        this.zzsM = this.zzsK.zzdS();
        this.zzsK.zzh("gqi", zzafgVar.zzXY.zzTC);
        this.zzsP.zzvV = null;
        this.zzsP.zzvZ = zzafgVar;
        zzafgVar.zzXX.zza(new zzc(this, zzafgVar));
        zzafgVar.zzXX.zzdf();
        zza(zzafgVar, this.zzsK);
    }

    protected abstract void zza(zzafg zzafgVar, zznb zznbVar);

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zziv zzivVar) {
        com.google.android.gms.common.internal.zzbo.zzcz("setAdSize must be called on the main UI thread.");
        this.zzsP.zzvX = zzivVar;
        if (this.zzsP.zzvY != null && this.zzsP.zzvY.zzPg != null && this.zzsP.zzwt == 0) {
            this.zzsP.zzvY.zzPg.zza(zzivVar);
        }
        if (this.zzsP.zzvU == null) {
            return;
        }
        if (this.zzsP.zzvU.getChildCount() > 1) {
            this.zzsP.zzvU.removeView(this.zzsP.zzvU.getNextView());
        }
        this.zzsP.zzvU.setMinimumWidth(zzivVar.widthPixels);
        this.zzsP.zzvU.setMinimumHeight(zzivVar.heightPixels);
        this.zzsP.zzvU.requestLayout();
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzjl zzjlVar) {
        com.google.android.gms.common.internal.zzbo.zzcz("setAdListener must be called on the main UI thread.");
        this.zzsP.zzwb = zzjlVar;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzjo zzjoVar) {
        com.google.android.gms.common.internal.zzbo.zzcz("setAdListener must be called on the main UI thread.");
        this.zzsP.zzwc = zzjoVar;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzke zzkeVar) {
        com.google.android.gms.common.internal.zzbo.zzcz("setAppEventListener must be called on the main UI thread.");
        this.zzsP.zzwd = zzkeVar;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzkk zzkkVar) {
        com.google.android.gms.common.internal.zzbo.zzcz("setCorrelationIdProvider must be called on the main UI thread");
        this.zzsP.zzwe = zzkkVar;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(@Nullable zzky zzkyVar) {
        com.google.android.gms.common.internal.zzbo.zzcz("setIconAdOptions must be called on the main UI thread.");
        this.zzsP.zzwl = zzkyVar;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(@Nullable zzlx zzlxVar) {
        com.google.android.gms.common.internal.zzbo.zzcz("setVideoOptions must be called on the main UI thread.");
        this.zzsP.zzwk = zzlxVar;
    }

    @Override // com.google.android.gms.internal.zzjz
    public void zza(zznh zznhVar) {
        throw new IllegalStateException("setOnCustomRenderedAdLoadedListener is not supported for current ad type");
    }

    @Override // com.google.android.gms.internal.zzjz
    public void zza(zzxg zzxgVar) {
        zzafr.zzaT("setInAppPurchaseListener is deprecated and should not be called.");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzxo zzxoVar, String str) {
        zzafr.zzaT("setPlayStorePurchaseParams is deprecated and should not be called.");
    }

    @Override // com.google.android.gms.internal.zzafm
    public final void zza(HashSet<zzafh> hashSet) {
        this.zzsP.zza(hashSet);
    }

    boolean zza(zzaff zzaffVar) {
        return false;
    }

    protected abstract boolean zza(@Nullable zzaff zzaffVar, zzaff zzaffVar2);

    @Override // com.google.android.gms.internal.zzjz
    public boolean zza(zzir zzirVar) {
        com.google.android.gms.common.internal.zzbo.zzcz("loadAd must be called on the main UI thread.");
        zzbs.zzbE().zzcX();
        if (((Boolean) zzbs.zzbL().zzd(zzmo.zzDR)).booleanValue()) {
            zzir.zzh(zzirVar);
        }
        if (com.google.android.gms.common.util.zzj.zzaI(this.zzsP.zzqD) && zzirVar.zzzV != null) {
            zzirVar = new zzis(zzirVar).zza(null).zzdj();
        }
        if (this.zzsP.zzvV != null || this.zzsP.zzvW != null) {
            if (this.zzsQ != null) {
                zzafr.zzaT("Aborting last ad request since another ad request is already in progress. The current request object will still be cached for future refreshes.");
            } else {
                zzafr.zzaT("Loading already in progress, saving this object for future refreshes.");
            }
            this.zzsQ = zzirVar;
            return false;
        }
        zzafr.zzaS("Starting ad request.");
        this.zzsK = new zznb(((Boolean) zzbs.zzbL().zzd(zzmo.zzCQ)).booleanValue(), "load_ad", this.zzsP.zzvX.zzAs);
        this.zzsL = new zzmz(-1L, null, null);
        this.zzsM = new zzmz(-1L, null, null);
        this.zzsL = this.zzsK.zzdS();
        if (zzirVar.zzzQ) {
            zzafr.zzaS("This request is sent from a test device.");
        } else {
            zzji.zzds();
            String strValueOf = String.valueOf(zzaiy.zzV(this.zzsP.zzqD));
            zzafr.zzaS(new StringBuilder(String.valueOf(strValueOf).length() + 71).append("Use AdRequest.Builder.addTestDevice(\"").append(strValueOf).append("\") to get test ads on this device.").toString());
        }
        this.zzsO.zzf(zzirVar);
        this.zzsN = zza(zzirVar, this.zzsK);
        return this.zzsN;
    }

    protected abstract boolean zza(zzir zzirVar, zznb zznbVar);

    public final zzv zzak() {
        return this.zzsS;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final IObjectWrapper zzal() {
        com.google.android.gms.common.internal.zzbo.zzcz("getAdFrame must be called on the main UI thread.");
        return com.google.android.gms.dynamic.zzn.zzw(this.zzsP.zzvU);
    }

    @Override // com.google.android.gms.internal.zzjz
    @Nullable
    public final zziv zzam() {
        com.google.android.gms.common.internal.zzbo.zzcz("getAdSize must be called on the main UI thread.");
        if (this.zzsP.zzvX == null) {
            return null;
        }
        return new zzlv(this.zzsP.zzvX);
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzag
    public final void zzan() {
        zzaq();
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zzao() {
        com.google.android.gms.common.internal.zzbo.zzcz("recordManualImpression must be called on the main UI thread.");
        if (this.zzsP.zzvY == null) {
            zzafr.zzaT("Ad state was null when trying to ping manual tracking URLs.");
            return;
        }
        zzafr.zzaC("Pinging manual tracking URLs.");
        if (this.zzsP.zzvY.zzTq == null || this.zzsP.zzvY.zzXV) {
            return;
        }
        zzbs.zzbz();
        zzagz.zza(this.zzsP.zzqD, this.zzsP.zzvT.zzaP, this.zzsP.zzvY.zzTq);
        this.zzsP.zzvY.zzXV = true;
    }

    protected void zzap() {
        zzafr.zzaS("Ad closing.");
        if (this.zzsP.zzwc != null) {
            try {
                this.zzsP.zzwc.onAdClosed();
            } catch (RemoteException e) {
                zzafr.zzc("Could not call AdListener.onAdClosed().", e);
            }
        }
        if (this.zzsP.zzwp != null) {
            try {
                this.zzsP.zzwp.onRewardedVideoAdClosed();
            } catch (RemoteException e2) {
                zzafr.zzc("Could not call RewardedVideoAdListener.onRewardedVideoAdClosed().", e2);
            }
        }
    }

    protected final void zzaq() {
        zzafr.zzaS("Ad leaving application.");
        if (this.zzsP.zzwc != null) {
            try {
                this.zzsP.zzwc.onAdLeftApplication();
            } catch (RemoteException e) {
                zzafr.zzc("Could not call AdListener.onAdLeftApplication().", e);
            }
        }
        if (this.zzsP.zzwp != null) {
            try {
                this.zzsP.zzwp.onRewardedVideoAdLeftApplication();
            } catch (RemoteException e2) {
                zzafr.zzc("Could not call  RewardedVideoAdListener.onRewardedVideoAdLeftApplication().", e2);
            }
        }
    }

    protected final void zzar() {
        zzafr.zzaS("Ad opening.");
        if (this.zzsP.zzwc != null) {
            try {
                this.zzsP.zzwc.onAdOpened();
            } catch (RemoteException e) {
                zzafr.zzc("Could not call AdListener.onAdOpened().", e);
            }
        }
        if (this.zzsP.zzwp != null) {
            try {
                this.zzsP.zzwp.onRewardedVideoAdOpened();
            } catch (RemoteException e2) {
                zzafr.zzc("Could not call RewardedVideoAdListener.onRewardedVideoAdOpened().", e2);
            }
        }
    }

    protected void zzas() {
        zzafr.zzaS("Ad finished loading.");
        this.zzsN = false;
        if (this.zzsP.zzwc != null) {
            try {
                this.zzsP.zzwc.onAdLoaded();
            } catch (RemoteException e) {
                zzafr.zzc("Could not call AdListener.onAdLoaded().", e);
            }
        }
        if (this.zzsP.zzwp != null) {
            try {
                this.zzsP.zzwp.onRewardedVideoAdLoaded();
            } catch (RemoteException e2) {
                zzafr.zzc("Could not call RewardedVideoAdListener.onRewardedVideoAdLoaded().", e2);
            }
        }
    }

    public final void zzat() {
        zzafr.zzaS("Ad impression.");
        if (this.zzsP.zzwc != null) {
            try {
                this.zzsP.zzwc.onAdImpression();
            } catch (RemoteException e) {
                zzafr.zzc("Could not call AdListener.onAdImpression().", e);
            }
        }
    }

    public final void zzau() {
        zzafr.zzaS("Ad clicked.");
        if (this.zzsP.zzwc != null) {
            try {
                this.zzsP.zzwc.onAdClicked();
            } catch (RemoteException e) {
                zzafr.zzc("Could not call AdListener.onAdClicked().", e);
            }
        }
    }

    protected final void zzav() {
        if (this.zzsP.zzwp == null) {
            return;
        }
        try {
            this.zzsP.zzwp.onRewardedVideoStarted();
        } catch (RemoteException e) {
            zzafr.zzc("Could not call RewardedVideoAdListener.onVideoStarted().", e);
        }
    }

    public final void zzaw() {
        zzaff zzaffVar = this.zzsP.zzvY;
        if (zzaffVar == null || TextUtils.isEmpty(zzaffVar.zzTK) || zzaffVar.zzXW || !zzbs.zzbH().zzic()) {
            return;
        }
        zzafr.zzaC("Sending troubleshooting signals to the server.");
        zzbs.zzbH().zza(this.zzsP.zzqD, this.zzsP.zzvT.zzaP, zzaffVar.zzTK, this.zzsP.zzvR);
        zzaffVar.zzXW = true;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final zzke zzax() {
        return this.zzsP.zzwd;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final zzjo zzay() {
        return this.zzsP.zzwc;
    }

    protected final List<String> zzb(List<String> list) {
        ArrayList arrayList = new ArrayList(list.size());
        Iterator<String> it = list.iterator();
        while (it.hasNext()) {
            arrayList.add(zzaez.zzb(it.next(), this.zzsP.zzqD));
        }
        return arrayList;
    }

    protected final void zzb(View view) {
        zzbu zzbuVar = this.zzsP.zzvU;
        if (zzbuVar != null) {
            zzbuVar.addView(view, zzbs.zzbB().zzhW());
        }
    }

    @Override // com.google.android.gms.internal.zzxy
    public void zzb(zzaff zzaffVar) {
        this.zzsK.zza(this.zzsM, "awr");
        this.zzsP.zzvW = null;
        if (zzaffVar.errorCode != -2 && zzaffVar.errorCode != 3) {
            zzbs.zzbD().zzb(this.zzsP.zzbZ());
        }
        if (zzaffVar.errorCode == -1) {
            this.zzsN = false;
            return;
        }
        if (zza(zzaffVar)) {
            zzafr.zzaC("Ad refresh scheduled.");
        }
        if (zzaffVar.errorCode != -2) {
            zze(zzaffVar.errorCode);
            return;
        }
        if (this.zzsP.zzwr == null) {
            this.zzsP.zzwr = new zzafn(this.zzsP.zzvR);
        }
        this.zzsR.zzg(this.zzsP.zzvY);
        if (zza(this.zzsP.zzvY, zzaffVar)) {
            this.zzsP.zzvY = zzaffVar;
            zzbt zzbtVar = this.zzsP;
            if (zzbtVar.zzwa != null) {
                if (zzbtVar.zzvY != null) {
                    zzbtVar.zzwa.zzh(zzbtVar.zzvY.zzXR);
                    zzbtVar.zzwa.zzi(zzbtVar.zzvY.zzXS);
                    zzbtVar.zzwa.zzw(zzbtVar.zzvY.zzTo);
                }
                zzbtVar.zzwa.zzv(zzbtVar.zzvX.zzAt);
            }
            this.zzsK.zzh("is_mraid", this.zzsP.zzvY.zzcn() ? "1" : "0");
            this.zzsK.zzh("is_mediation", this.zzsP.zzvY.zzTo ? "1" : "0");
            if (this.zzsP.zzvY.zzPg != null && this.zzsP.zzvY.zzPg.zziw() != null) {
                this.zzsK.zzh("is_delay_pl", this.zzsP.zzvY.zzPg.zziw().zziS() ? "1" : "0");
            }
            this.zzsK.zza(this.zzsL, "ttc");
            if (zzbs.zzbD().zzhr() != null) {
                zzbs.zzbD().zzhr().zza(this.zzsK);
            }
            zzaw();
            if (this.zzsP.zzcc()) {
                zzas();
            }
        }
        if (zzaffVar.zzMd != null) {
            zzbs.zzbz().zza(this.zzsP.zzqD, zzaffVar.zzMd);
        }
    }

    protected boolean zzb(zzir zzirVar) {
        if (this.zzsP.zzvU == null) {
            return false;
        }
        Object parent = this.zzsP.zzvU.getParent();
        if (!(parent instanceof View)) {
            return false;
        }
        View view = (View) parent;
        return zzbs.zzbz().zza(view, view.getContext());
    }

    /* JADX INFO: Access modifiers changed from: protected */
    public void zze(int i) {
        zzafr.zzaT(new StringBuilder(30).append("Failed to load ad: ").append(i).toString());
        this.zzsN = false;
        if (this.zzsP.zzwc != null) {
            try {
                this.zzsP.zzwc.onAdFailedToLoad(i);
            } catch (RemoteException e) {
                zzafr.zzc("Could not call AdListener.onAdFailedToLoad().", e);
            }
        }
        if (this.zzsP.zzwp != null) {
            try {
                this.zzsP.zzwp.onRewardedVideoAdFailedToLoad(i);
            } catch (RemoteException e2) {
                zzafr.zzc("Could not call RewardedVideoAdListener.onRewardedVideoAdFailedToLoad().", e2);
            }
        }
    }
}
