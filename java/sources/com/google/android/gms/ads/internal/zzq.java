package com.google.android.gms.ads.internal;

import android.content.Context;
import android.os.RemoteException;
import android.support.annotation.Nullable;
import android.view.View;
import com.google.android.gms.internal.zzaff;
import com.google.android.gms.internal.zzafg;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzir;
import com.google.android.gms.internal.zziv;
import com.google.android.gms.internal.zzks;
import com.google.android.gms.internal.zznb;
import com.google.android.gms.internal.zznh;
import com.google.android.gms.internal.zznq;
import com.google.android.gms.internal.zzns;
import com.google.android.gms.internal.zznw;
import com.google.android.gms.internal.zznx;
import com.google.android.gms.internal.zzny;
import com.google.android.gms.internal.zznz;
import com.google.android.gms.internal.zzpj;
import com.google.android.gms.internal.zzpt;
import com.google.android.gms.internal.zzuq;
import com.google.android.gms.internal.zzvc;
import com.google.android.gms.internal.zzvf;
import com.google.android.gms.internal.zzxx;
import com.google.android.gms.internal.zzzn;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzq extends zzd implements zznz {
    private boolean zzsu;
    private zzaff zztg;
    private boolean zzth;

    public zzq(Context context, zzv zzvVar, zziv zzivVar, String str, zzuq zzuqVar, zzaje zzajeVar) {
        super(context, zzivVar, str, zzuqVar, zzajeVar, zzvVar);
        this.zzth = false;
    }

    private static zzaff zza(zzafg zzafgVar, int i) {
        return new zzaff(zzafgVar.zzUj.zzSz, null, zzafgVar.zzXY.zzMa, i, zzafgVar.zzXY.zzMb, zzafgVar.zzXY.zzTq, zzafgVar.zzXY.orientation, zzafgVar.zzXY.zzMg, zzafgVar.zzUj.zzSC, zzafgVar.zzXY.zzTo, null, null, null, zzafgVar.zzXN, null, zzafgVar.zzXY.zzTp, zzafgVar.zzvX, zzafgVar.zzXY.zzTn, zzafgVar.zzXR, zzafgVar.zzXS, zzafgVar.zzXY.zzTt, zzafgVar.zzXL, null, zzafgVar.zzXY.zzTD, zzafgVar.zzXY.zzTE, zzafgVar.zzXY.zzTE, zzafgVar.zzXY.zzTG, zzafgVar.zzXY.zzTH, null, zzafgVar.zzXY.zzMd, zzafgVar.zzXY.zzTK, zzafgVar.zzXX);
    }

    private final boolean zzb(zzaff zzaffVar, zzaff zzaffVar2) {
        zzc(null);
        if (!this.zzsP.zzcc()) {
            zzafr.zzaT("Native ad does not have custom rendering mode.");
            zze(0);
            return false;
        }
        try {
            zzvc zzvcVarZzfq = zzaffVar2.zzMH != null ? zzaffVar2.zzMH.zzfq() : null;
            zzvf zzvfVarZzfr = zzaffVar2.zzMH != null ? zzaffVar2.zzMH.zzfr() : null;
            zzpj zzpjVarZzfv = zzaffVar2.zzMH != null ? zzaffVar2.zzMH.zzfv() : null;
            if (zzvcVarZzfq != null && this.zzsP.zzwf != null) {
                zznq zznqVar = new zznq(zzvcVarZzfq.getHeadline(), zzvcVarZzfq.getImages(), zzvcVarZzfq.getBody(), zzvcVarZzfq.zzeh() != null ? zzvcVarZzfq.zzeh() : null, zzvcVarZzfq.getCallToAction(), zzvcVarZzfq.getStarRating(), zzvcVarZzfq.getStore(), zzvcVarZzfq.getPrice(), null, zzvcVarZzfq.getExtras(), zzvcVarZzfq.getVideoController(), null);
                zznqVar.zzb(new zznx(this.zzsP.zzqD, this, this.zzsP.zzvS, zzvcVarZzfq, zznqVar));
                zzagz.zzZr.post(new zzs(this, zznqVar));
            } else if (zzvfVarZzfr != null && this.zzsP.zzwg != null) {
                zzns zznsVar = new zzns(zzvfVarZzfr.getHeadline(), zzvfVarZzfr.getImages(), zzvfVarZzfr.getBody(), zzvfVarZzfr.zzem() != null ? zzvfVarZzfr.zzem() : null, zzvfVarZzfr.getCallToAction(), zzvfVarZzfr.getAdvertiser(), null, zzvfVarZzfr.getExtras(), zzvfVarZzfr.getVideoController(), null);
                zznsVar.zzb(new zznx(this.zzsP.zzqD, this, this.zzsP.zzvS, zzvfVarZzfr, zznsVar));
                zzagz.zzZr.post(new zzt(this, zznsVar));
            } else {
                if (zzpjVarZzfv == null || this.zzsP.zzwi == null || this.zzsP.zzwi.get(zzpjVarZzfv.getCustomTemplateId()) == null) {
                    zzafr.zzaT("No matching mapper/listener for retrieved native ad template.");
                    zze(0);
                    return false;
                }
                zzagz.zzZr.post(new zzu(this, zzpjVarZzfv));
            }
            return super.zza(zzaffVar, zzaffVar2);
        } catch (RemoteException e) {
            zzafr.zzc("Failed to get native ad mapper", e);
            zze(0);
            return false;
        }
    }

    /* JADX WARN: Multi-variable type inference failed */
    /* JADX WARN: Type inference fix 'apply assigned field type' failed
    java.lang.UnsupportedOperationException: ArgType.getObject(), call class: class jadx.core.dex.instructions.args.ArgType$UnknownArg
    	at jadx.core.dex.instructions.args.ArgType.getObject(ArgType.java:596)
    	at jadx.core.dex.attributes.nodes.ClassTypeVarsAttr.getTypeVarsMapFor(ClassTypeVarsAttr.java:35)
    	at jadx.core.dex.nodes.utils.TypeUtils.replaceClassGenerics(TypeUtils.java:177)
    	at jadx.core.dex.visitors.typeinference.FixTypesVisitor.insertExplicitUseCast(FixTypesVisitor.java:397)
    	at jadx.core.dex.visitors.typeinference.FixTypesVisitor.tryFieldTypeWithNewCasts(FixTypesVisitor.java:359)
    	at jadx.core.dex.visitors.typeinference.FixTypesVisitor.applyFieldType(FixTypesVisitor.java:309)
    	at jadx.core.dex.visitors.typeinference.FixTypesVisitor.visit(FixTypesVisitor.java:94)
     */
    private final boolean zzc(zzaff zzaffVar, zzaff zzaffVar2) {
        View viewZzd = zzar.zzd(zzaffVar2);
        if (viewZzd == null) {
            return false;
        }
        View nextView = this.zzsP.zzvU.getNextView();
        if (nextView != 0) {
            if (nextView instanceof zzaka) {
                ((zzaka) nextView).destroy();
            }
            this.zzsP.zzvU.removeView(nextView);
        }
        if (!zzar.zze(zzaffVar2)) {
            try {
                zzb(viewZzd);
            } catch (Throwable th) {
                zzbs.zzbD().zza(th, "AdLoaderManager.swapBannerViews");
                zzafr.zzc("Could not add mediation view to view hierarchy.", th);
                return false;
            }
        }
        if (this.zzsP.zzvU.getChildCount() > 1) {
            this.zzsP.zzvU.showNext();
        }
        if (zzaffVar != null) {
            View nextView2 = this.zzsP.zzvU.getNextView();
            if (nextView2 != null) {
                this.zzsP.zzvU.removeView(nextView2);
            }
            this.zzsP.zzcb();
        }
        this.zzsP.zzvU.setMinimumWidth(zzam().widthPixels);
        this.zzsP.zzvU.setMinimumHeight(zzam().heightPixels);
        this.zzsP.zzvU.requestLayout();
        this.zzsP.zzvU.setVisibility(0);
        return true;
    }

    @Override // com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    @Nullable
    public final zzks getVideoController() {
        return null;
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public final void pause() {
        if (!this.zzth) {
            throw new IllegalStateException("Native Ad does not support pause().");
        }
        super.pause();
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public final void resume() {
        if (!this.zzth) {
            throw new IllegalStateException("Native Ad does not support resume().");
        }
        super.resume();
    }

    @Override // com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public final void setManualImpressionsEnabled(boolean z) {
        com.google.android.gms.common.internal.zzbo.zzcz("setManualImpressionsEnabled must be called from the main thread.");
        this.zzsu = z;
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.internal.zzjz
    public final void showInterstitial() {
        throw new IllegalStateException("Interstitial is not supported by AdLoaderManager.");
    }

    @Override // com.google.android.gms.ads.internal.zza
    public final void zza(zzafg zzafgVar, zznb zznbVar) {
        this.zztg = null;
        if (zzafgVar.errorCode != -2) {
            this.zztg = zza(zzafgVar, zzafgVar.errorCode);
        } else if (!zzafgVar.zzXY.zzTo) {
            zzafr.zzaT("partialAdState is not mediation");
            this.zztg = zza(zzafgVar, 0);
        }
        if (this.zztg != null) {
            zzagz.zzZr.post(new zzr(this));
            return;
        }
        if (zzafgVar.zzvX != null) {
            this.zzsP.zzvX = zzafgVar.zzvX;
        }
        this.zzsP.zzwt = 0;
        zzbt zzbtVar = this.zzsP;
        zzbs.zzby();
        zzbtVar.zzvW = zzxx.zza(this.zzsP.zzqD, this, zzafgVar, this.zzsP.zzvS, null, this.zzsX, this, zznbVar);
    }

    @Override // com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public final void zza(zznh zznhVar) {
        throw new IllegalStateException("CustomRendering is not supported by AdLoaderManager.");
    }

    @Override // com.google.android.gms.internal.zznz
    public final void zza(zznw zznwVar) {
        zzafr.zzaT("Unexpected call to AdLoaderManager method");
    }

    @Override // com.google.android.gms.internal.zznz
    public final void zza(zzny zznyVar) {
        zzafr.zzaT("Unexpected call to AdLoaderManager method");
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.ads.internal.zza
    protected final boolean zza(@Nullable zzaff zzaffVar, zzaff zzaffVar2) {
        boolean z;
        if (!this.zzsP.zzcc()) {
            throw new IllegalStateException("AdLoader API does not support custom rendering.");
        }
        if (!zzaffVar2.zzTo) {
            zze(0);
            zzafr.zzaT("newState is not mediation.");
            return false;
        }
        if (zzaffVar2.zzMG != null && zzaffVar2.zzMG.zzfh()) {
            if (this.zzsP.zzcc() && this.zzsP.zzvU != null) {
                this.zzsP.zzvU.zzcf().zzaP(zzaffVar2.zzTt);
            }
            if (!super.zza(zzaffVar, zzaffVar2)) {
                z = false;
            } else if (!this.zzsP.zzcc() || zzc(zzaffVar, zzaffVar2)) {
                if (!this.zzsP.zzcd()) {
                    super.zza(zzaffVar2, false);
                }
                z = true;
            } else {
                zze(0);
                z = false;
            }
            if (!z) {
                return false;
            }
            this.zzth = true;
        } else {
            if (zzaffVar2.zzMG == null || !zzaffVar2.zzMG.zzfi()) {
                zze(0);
                zzafr.zzaT("Response is neither banner nor native.");
                return false;
            }
            if (!zzb(zzaffVar, zzaffVar2)) {
                return false;
            }
        }
        zzd(new ArrayList(Arrays.asList(2)));
        return true;
    }

    @Override // com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public final boolean zza(zzir zzirVar) {
        if (this.zzsP.zzwn != null && this.zzsP.zzwn.size() == 1 && this.zzsP.zzwn.get(0).intValue() == 2) {
            zzafr.e("Requesting only banner Ad from AdLoader or calling loadAd on returned banner is not yet supported");
            zze(0);
            return false;
        }
        if (this.zzsP.zzwm == null) {
            return super.zza(zzirVar);
        }
        if (zzirVar.zzzS != this.zzsu) {
            zzirVar = new zzir(zzirVar.versionCode, zzirVar.zzzN, zzirVar.extras, zzirVar.zzzO, zzirVar.zzzP, zzirVar.zzzQ, zzirVar.zzzR, zzirVar.zzzS || this.zzsu, zzirVar.zzzT, zzirVar.zzzU, zzirVar.zzzV, zzirVar.zzzW, zzirVar.zzzX, zzirVar.zzzY, zzirVar.zzzZ, zzirVar.zzAa, zzirVar.zzAb, zzirVar.zzAc);
        }
        return super.zza(zzirVar);
    }

    @Override // com.google.android.gms.ads.internal.zzd
    protected final boolean zza(zzir zzirVar, zzaff zzaffVar, boolean z) {
        return false;
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.internal.zzuc
    public final void zzaC() {
        if (this.zzsP.zzvY == null || !"com.google.ads.mediation.admob.AdMobAdapter".equals(this.zzsP.zzvY.zzMI) || this.zzsP.zzvY.zzMG == null || !this.zzsP.zzvY.zzMG.zzfi()) {
            super.zzaC();
        } else {
            zzau();
        }
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.internal.zzuc
    public final void zzaH() {
        if (this.zzsP.zzvY == null || !"com.google.ads.mediation.admob.AdMobAdapter".equals(this.zzsP.zzvY.zzMI) || this.zzsP.zzvY.zzMG == null || !this.zzsP.zzvY.zzMG.zzfi()) {
            super.zzaH();
        } else {
            zzat();
        }
    }

    @Override // com.google.android.gms.internal.zznz
    public final void zzaO() {
        zzafr.zzaT("Unexpected call to AdLoaderManager method");
    }

    @Override // com.google.android.gms.internal.zznz
    public final boolean zzaP() {
        return this.zzsP.zzvY != null && this.zzsP.zzvY.zzTo && this.zzsP.zzvY.zzXN != null && this.zzsP.zzvY.zzXN.zzMm;
    }

    @Override // com.google.android.gms.ads.internal.zza
    protected final void zzas() {
        super.zzas();
        zzaff zzaffVar = this.zzsP.zzvY;
        if (zzaffVar == null || zzaffVar.zzMG == null || !zzaffVar.zzMG.zzfh() || this.zzsP.zzwm == null) {
            return;
        }
        try {
            this.zzsP.zzwm.zza(this, com.google.android.gms.dynamic.zzn.zzw(this.zzsP.zzqD));
        } catch (RemoteException e) {
            zzafr.zzc("Could not call PublisherAdViewLoadedListener.onPublisherAdViewLoaded().", e);
        }
    }

    public final void zzc(@Nullable List<String> list) {
        com.google.android.gms.common.internal.zzbo.zzcz("setNativeTemplates must be called on the main UI thread.");
        this.zzsP.zzwq = list;
    }

    public final void zzd(List<Integer> list) {
        com.google.android.gms.common.internal.zzbo.zzcz("setAllowedAdTypes must be called on the main UI thread.");
        this.zzsP.zzwn = list;
    }

    @Override // com.google.android.gms.internal.zznz
    @Nullable
    public final zzpt zzs(String str) {
        com.google.android.gms.common.internal.zzbo.zzcz("getOnCustomClickListener must be called on the main UI thread.");
        return this.zzsP.zzwh.get(str);
    }
}
