package com.google.android.gms.ads.internal;

import android.content.Context;
import android.os.RemoteException;
import android.support.annotation.Nullable;
import android.support.v4.util.SimpleArrayMap;
import android.util.Log;
import com.google.ads.AdRequest;
import com.google.android.gms.internal.zzaff;
import com.google.android.gms.internal.zzafg;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzajc;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzfk;
import com.google.android.gms.internal.zzir;
import com.google.android.gms.internal.zziv;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zznb;
import com.google.android.gms.internal.zznh;
import com.google.android.gms.internal.zznq;
import com.google.android.gms.internal.zzns;
import com.google.android.gms.internal.zznu;
import com.google.android.gms.internal.zznw;
import com.google.android.gms.internal.zznx;
import com.google.android.gms.internal.zzny;
import com.google.android.gms.internal.zznz;
import com.google.android.gms.internal.zzoa;
import com.google.android.gms.internal.zzpj;
import com.google.android.gms.internal.zzpt;
import com.google.android.gms.internal.zzpw;
import com.google.android.gms.internal.zzuq;
import com.google.android.gms.internal.zzvc;
import com.google.android.gms.internal.zzvf;
import com.google.android.gms.internal.zzxg;
import com.google.android.gms.internal.zzxx;
import com.google.android.gms.internal.zzyh;
import com.google.android.gms.internal.zzzn;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzbb extends zzd implements zznz {
    private final Object mLock;
    private boolean zzta;
    private zzaka zzuO;
    private zzyh zzuP;

    public zzbb(Context context, zzv zzvVar, zziv zzivVar, String str, zzuq zzuqVar, zzaje zzajeVar) {
        super(context, zzivVar, str, zzuqVar, zzajeVar, zzvVar);
        this.mLock = new Object();
    }

    private final void zza(zznq zznqVar) {
        zzagz.zzZr.post(new zzbd(this, zznqVar));
    }

    private final void zza(zzns zznsVar) {
        zzagz.zzZr.post(new zzbe(this, zznsVar));
    }

    private final void zzbn() {
        zzagz.zzb(new zzbh(this));
    }

    @Override // com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public final String getAdUnitId() {
        return this.zzsP.zzvR;
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public final void pause() {
        throw new IllegalStateException("Native Ad DOES NOT support pause().");
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public final void resume() {
        throw new IllegalStateException("Native Ad DOES NOT support resume().");
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.internal.zzjz
    public final void showInterstitial() {
        throw new IllegalStateException("Interstitial is NOT supported by NativeAdManager.");
    }

    @Override // com.google.android.gms.ads.internal.zza
    public final void zza(zzafg zzafgVar, zznb zznbVar) {
        if (zzafgVar.zzvX != null) {
            this.zzsP.zzvX = zzafgVar.zzvX;
        }
        if (zzafgVar.errorCode != -2) {
            zzagz.zzZr.post(new zzbc(this, zzafgVar));
            return;
        }
        this.zzsP.zzwt = 0;
        zzbt zzbtVar = this.zzsP;
        zzbs.zzby();
        zzbtVar.zzvW = zzxx.zza(this.zzsP.zzqD, this, zzafgVar, this.zzsP.zzvS, null, this.zzsX, this, zznbVar);
        String strValueOf = String.valueOf(this.zzsP.zzvW.getClass().getName());
        zzafr.zzaC(strValueOf.length() != 0 ? "AdRenderer: ".concat(strValueOf) : new String("AdRenderer: "));
    }

    @Override // com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public final void zza(zznh zznhVar) {
        throw new IllegalStateException("CustomRendering is NOT supported by NativeAdManager.");
    }

    @Override // com.google.android.gms.internal.zznz
    public final void zza(zznw zznwVar) {
        if (this.zzuO != null) {
            this.zzuO.zzb(zznwVar);
        }
    }

    @Override // com.google.android.gms.internal.zznz
    public final void zza(zzny zznyVar) {
        if (this.zzsP.zzvY.zzXL != null) {
            zzbs.zzbD().zzhG().zza(this.zzsP.zzvX, this.zzsP.zzvY, new zzfk(zznyVar), (com.google.android.gms.ads.internal.js.zzai) null);
        }
    }

    @Override // com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public final void zza(zzxg zzxgVar) {
        throw new IllegalStateException("In App Purchase is NOT supported by NativeAdManager.");
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.ads.internal.zza
    protected final boolean zza(zzaff zzaffVar, zzaff zzaffVar2) {
        zzc(null);
        if (!this.zzsP.zzcc()) {
            throw new IllegalStateException("Native ad DOES NOT have custom rendering mode.");
        }
        if (zzaffVar2.zzTo) {
            if (((Boolean) zzbs.zzbL().zzd(zzmo.zzFu)).booleanValue()) {
                zzbn();
            }
            try {
                zzvc zzvcVarZzfq = zzaffVar2.zzMH != null ? zzaffVar2.zzMH.zzfq() : null;
                zzvf zzvfVarZzfr = zzaffVar2.zzMH != null ? zzaffVar2.zzMH.zzfr() : null;
                zzpj zzpjVarZzfv = zzaffVar2.zzMH != null ? zzaffVar2.zzMH.zzfv() : null;
                if (zzvcVarZzfq != null && this.zzsP.zzwf != null) {
                    zznq zznqVar = new zznq(zzvcVarZzfq.getHeadline(), zzvcVarZzfq.getImages(), zzvcVarZzfq.getBody(), zzvcVarZzfq.zzeh() != null ? zzvcVarZzfq.zzeh() : null, zzvcVarZzfq.getCallToAction(), zzvcVarZzfq.getStarRating(), zzvcVarZzfq.getStore(), zzvcVarZzfq.getPrice(), null, zzvcVarZzfq.getExtras(), zzvcVarZzfq.getVideoController(), null);
                    zznqVar.zzb(new zznx(this.zzsP.zzqD, this, this.zzsP.zzvS, zzvcVarZzfq, zznqVar));
                    zza(zznqVar);
                } else if (zzvfVarZzfr != null && this.zzsP.zzwg != null) {
                    zzns zznsVar = new zzns(zzvfVarZzfr.getHeadline(), zzvfVarZzfr.getImages(), zzvfVarZzfr.getBody(), zzvfVarZzfr.zzem() != null ? zzvfVarZzfr.zzem() : null, zzvfVarZzfr.getCallToAction(), zzvfVarZzfr.getAdvertiser(), null, zzvfVarZzfr.getExtras(), zzvfVarZzfr.getVideoController(), null);
                    zznsVar.zzb(new zznx(this.zzsP.zzqD, this, this.zzsP.zzvS, zzvfVarZzfr, zznsVar));
                    zza(zznsVar);
                } else {
                    if (zzpjVarZzfv == null || this.zzsP.zzwi == null || this.zzsP.zzwi.get(zzpjVarZzfv.getCustomTemplateId()) == null) {
                        zzafr.zzaT("No matching mapper/listener for retrieved native ad template.");
                        zze(0);
                        return false;
                    }
                    zzagz.zzZr.post(new zzbg(this, zzpjVarZzfv));
                }
            } catch (RemoteException e) {
                zzafr.zzc("Failed to get native ad mapper", e);
            }
        } else {
            zzoa zzoaVar = zzaffVar2.zzXT;
            if ((zzoaVar instanceof zzns) && this.zzsP.zzwg != null) {
                zza((zzns) zzaffVar2.zzXT);
            } else if ((zzoaVar instanceof zznq) && this.zzsP.zzwf != null) {
                zza((zznq) zzaffVar2.zzXT);
            } else {
                if (!(zzoaVar instanceof zznu) || this.zzsP.zzwi == null || this.zzsP.zzwi.get(((zznu) zzoaVar).getCustomTemplateId()) == null) {
                    zzafr.zzaT("No matching listener for retrieved native ad template.");
                    zze(0);
                    return false;
                }
                zzagz.zzZr.post(new zzbf(this, ((zznu) zzoaVar).getCustomTemplateId(), zzaffVar2));
            }
        }
        return super.zza(zzaffVar, zzaffVar2);
    }

    @Override // com.google.android.gms.ads.internal.zzd
    protected final boolean zza(zzir zzirVar, zzaff zzaffVar, boolean z) {
        return this.zzsO.zzbo();
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.ads.internal.zza
    public final boolean zza(zzir zzirVar, zznb zznbVar) {
        try {
            if (((Boolean) zzbs.zzbL().zzd(zzmo.zzFu)).booleanValue()) {
                synchronized (this.mLock) {
                    this.zzuP = new zzyh(this.zzsP.zzqD, this, this.zzsP.zzvS, this.zzsP.zzvT);
                    this.zzuP.zzgs();
                    this.zzuP.zzgt();
                }
            }
            return super.zza(zzirVar, zznbVar);
        } catch (Exception e) {
            if (zzajc.zzz(4)) {
                Log.i(AdRequest.LOGTAG, "Error initializing webview.", e);
            }
            return false;
        }
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.internal.zzuc
    public final void zzaC() {
        if (this.zzsP.zzvY == null || !"com.google.ads.mediation.admob.AdMobAdapter".equals(this.zzsP.zzvY.zzMI)) {
            super.zzaC();
        } else {
            zzau();
        }
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.internal.zzuc
    public final void zzaH() {
        if (this.zzsP.zzvY == null || !"com.google.ads.mediation.admob.AdMobAdapter".equals(this.zzsP.zzvY.zzMI)) {
            super.zzaH();
        } else {
            zzat();
        }
    }

    @Override // com.google.android.gms.internal.zznz
    public final void zzaO() {
        if (this.zzuO != null) {
            this.zzuO.destroy();
            this.zzuO = null;
        }
    }

    @Override // com.google.android.gms.internal.zznz
    public final boolean zzaP() {
        return this.zzsP.zzvY != null && this.zzsP.zzvY.zzTo && this.zzsP.zzvY.zzXN != null && this.zzsP.zzvY.zzXN.zzMm;
    }

    @Override // com.google.android.gms.ads.internal.zza
    protected final void zzas() {
        super.zzas();
        if (this.zzta) {
            if (((Boolean) zzbs.zzbL().zzd(zzmo.zzFC)).booleanValue()) {
                zzbj();
            }
        }
    }

    @Nullable
    public final zzyh zzbi() {
        zzyh zzyhVar;
        synchronized (this.mLock) {
            zzyhVar = this.zzuP;
        }
        return zzyhVar;
    }

    public final void zzbj() {
        if (this.zzsP.zzvY == null || this.zzuO == null) {
            this.zzta = true;
            zzafr.zzaT("Request to enable ActiveView before adState is available.");
        } else {
            zzbs.zzbD().zzhG().zza(this.zzsP.zzvX, this.zzsP.zzvY, this.zzuO.getView(), this.zzuO);
            this.zzta = false;
        }
    }

    public final void zzbk() {
        this.zzta = false;
        if (this.zzsP.zzvY == null || this.zzuO == null) {
            zzafr.zzaT("Request to enable ActiveView before adState is available.");
        } else {
            zzbs.zzbD().zzhG().zzg(this.zzsP.zzvY);
        }
    }

    public final SimpleArrayMap<String, zzpw> zzbl() {
        com.google.android.gms.common.internal.zzbo.zzcz("getOnCustomTemplateAdLoadedListeners must be called on the main UI thread.");
        return this.zzsP.zzwi;
    }

    public final void zzbm() {
        if (this.zzuO == null || this.zzuO.zziH() == null || this.zzsP.zzwj == null || this.zzsP.zzwj.zzIr == null) {
            return;
        }
        this.zzuO.zziH().zzb(this.zzsP.zzwj.zzIr);
    }

    public final void zzc(@Nullable List<String> list) {
        com.google.android.gms.common.internal.zzbo.zzcz("setNativeTemplates must be called on the main UI thread.");
        this.zzsP.zzwq = list;
    }

    public final void zzd(zzaka zzakaVar) {
        this.zzuO = zzakaVar;
    }

    @Override // com.google.android.gms.ads.internal.zza
    protected final void zze(int i) {
        if (((Boolean) zzbs.zzbL().zzd(zzmo.zzFu)).booleanValue()) {
            zzbn();
        }
        super.zze(i);
    }

    @Override // com.google.android.gms.internal.zznz
    @Nullable
    public final zzpt zzs(String str) {
        com.google.android.gms.common.internal.zzbo.zzcz("getOnCustomClickListener must be called on the main UI thread.");
        return this.zzsP.zzwh.get(str);
    }
}
