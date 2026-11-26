package com.google.android.gms.ads.internal;

import android.content.Context;
import android.graphics.Rect;
import android.os.RemoteException;
import android.support.annotation.Nullable;
import android.view.View;
import android.view.ViewTreeObserver;
import com.google.android.gms.ads.AdSize;
import com.google.android.gms.internal.zzaet;
import com.google.android.gms.internal.zzaev;
import com.google.android.gms.internal.zzaff;
import com.google.android.gms.internal.zzafg;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zzajv;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzakb;
import com.google.android.gms.internal.zzakj;
import com.google.android.gms.internal.zzakm;
import com.google.android.gms.internal.zzaks;
import com.google.android.gms.internal.zzge;
import com.google.android.gms.internal.zzir;
import com.google.android.gms.internal.zziv;
import com.google.android.gms.internal.zzji;
import com.google.android.gms.internal.zzks;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zzuq;
import com.google.android.gms.internal.zzvc;
import com.google.android.gms.internal.zzvf;
import com.google.android.gms.internal.zzzn;
import java.lang.ref.WeakReference;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzx extends zzi implements ViewTreeObserver.OnGlobalLayoutListener, ViewTreeObserver.OnScrollChangedListener {
    private boolean zzsu;
    private WeakReference<Object> zzts;

    public zzx(Context context, zziv zzivVar, String str, zzuq zzuqVar, zzaje zzajeVar, zzv zzvVar) {
        super(context, zzivVar, str, zzuqVar, zzajeVar, zzvVar);
        this.zzts = new WeakReference<>(null);
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
    private final boolean zzd(@Nullable zzaff zzaffVar, zzaff zzaffVar2) {
        if (zzaffVar2.zzTo) {
            View viewZzd = zzar.zzd(zzaffVar2);
            if (viewZzd == null) {
                zzafr.zzaT("Could not get mediation view");
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
                    if (zzbs.zzbY().zzq(this.zzsP.zzqD)) {
                        new zzge(this.zzsP.zzqD, viewZzd).zza(new zzaev(this.zzsP.zzqD, this.zzsP.zzvR));
                    }
                    zzb(viewZzd);
                } catch (Throwable th) {
                    zzbs.zzbD().zza(th, "BannerAdManager.swapViews");
                    zzafr.zzc("Could not add mediation view to view hierarchy.", th);
                    return false;
                }
            }
        } else if (zzaffVar2.zzXP != null && zzaffVar2.zzPg != null) {
            zzaffVar2.zzPg.zza(zzaffVar2.zzXP);
            this.zzsP.zzvU.removeAllViews();
            this.zzsP.zzvU.setMinimumWidth(zzaffVar2.zzXP.widthPixels);
            this.zzsP.zzvU.setMinimumHeight(zzaffVar2.zzXP.heightPixels);
            zzb(zzaffVar2.zzPg.getView());
        }
        if (this.zzsP.zzvU.getChildCount() > 1) {
            this.zzsP.zzvU.showNext();
        }
        if (zzaffVar != null) {
            View nextView2 = this.zzsP.zzvU.getNextView();
            if (nextView2 instanceof zzaka) {
                ((zzaka) nextView2).zza(this.zzsP.zzqD, this.zzsP.zzvX, this.zzsK);
            } else if (nextView2 != 0) {
                this.zzsP.zzvU.removeView(nextView2);
            }
            this.zzsP.zzcb();
        }
        this.zzsP.zzvU.setVisibility(0);
        return true;
    }

    @Override // com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    @Nullable
    public final zzks getVideoController() {
        com.google.android.gms.common.internal.zzbo.zzcz("getVideoController must be called from the main thread.");
        if (this.zzsP.zzvY == null || this.zzsP.zzvY.zzPg == null) {
            return null;
        }
        return this.zzsP.zzvY.zzPg.zziH();
    }

    @Override // android.view.ViewTreeObserver.OnGlobalLayoutListener
    public final void onGlobalLayout() {
        zzc(this.zzsP.zzvY);
    }

    @Override // android.view.ViewTreeObserver.OnScrollChangedListener
    public final void onScrollChanged() {
        zzc(this.zzsP.zzvY);
    }

    @Override // com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public final void setManualImpressionsEnabled(boolean z) {
        com.google.android.gms.common.internal.zzbo.zzcz("setManualImpressionsEnabled must be called from the main thread.");
        this.zzsu = z;
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.internal.zzjz
    public final void showInterstitial() {
        throw new IllegalStateException("Interstitial is NOT supported by BannerAdManager.");
    }

    @Override // com.google.android.gms.ads.internal.zzi
    protected final zzaka zza(zzafg zzafgVar, @Nullable zzw zzwVar, @Nullable zzaet zzaetVar) throws zzakm {
        AdSize adSizeZzdl;
        zziv zzivVar;
        if (this.zzsP.zzvX.zzAu == null && this.zzsP.zzvX.zzAw) {
            zzbt zzbtVar = this.zzsP;
            if (zzafgVar.zzXY.zzAw) {
                zzivVar = this.zzsP.zzvX;
            } else {
                String str = zzafgVar.zzXY.zzTr;
                if (str != null) {
                    String[] strArrSplit = str.split("[xX]");
                    strArrSplit[0] = strArrSplit[0].trim();
                    strArrSplit[1] = strArrSplit[1].trim();
                    adSizeZzdl = new AdSize(Integer.parseInt(strArrSplit[0]), Integer.parseInt(strArrSplit[1]));
                } else {
                    adSizeZzdl = this.zzsP.zzvX.zzdl();
                }
                zzivVar = new zziv(this.zzsP.zzqD, adSizeZzdl);
            }
            zzbtVar.zzvX = zzivVar;
        }
        return super.zza(zzafgVar, zzwVar, zzaetVar);
    }

    @Override // com.google.android.gms.ads.internal.zzd
    protected final void zza(@Nullable zzaff zzaffVar, boolean z) {
        super.zza(zzaffVar, z);
        if (zzar.zze(zzaffVar)) {
            zzab zzabVar = new zzab(this);
            if (zzaffVar == null || !zzar.zze(zzaffVar)) {
                return;
            }
            zzaka zzakaVar = zzaffVar.zzPg;
            View view = zzakaVar != null ? zzakaVar.getView() : null;
            if (view == null) {
                zzafr.zzaT("AdWebView is null");
                return;
            }
            try {
                List<String> list = zzaffVar.zzMG != null ? zzaffVar.zzMG.zzLV : null;
                if (list == null || list.isEmpty()) {
                    zzafr.zzaT("No template ids present in mediation response");
                    return;
                }
                zzvc zzvcVarZzfq = zzaffVar.zzMH != null ? zzaffVar.zzMH.zzfq() : null;
                zzvf zzvfVarZzfr = zzaffVar.zzMH != null ? zzaffVar.zzMH.zzfr() : null;
                if (list.contains("2") && zzvcVarZzfq != null) {
                    zzvcVarZzfq.zzm(com.google.android.gms.dynamic.zzn.zzw(view));
                    if (!zzvcVarZzfq.getOverrideImpressionRecording()) {
                        zzvcVarZzfq.recordImpression();
                    }
                    zzakaVar.zziw().zza("/nativeExpressViewClicked", zzar.zza(zzvcVarZzfq, (zzvf) null, zzabVar));
                    return;
                }
                if (!list.contains("1") || zzvfVarZzfr == null) {
                    zzafr.zzaT("No matching template id and mapper");
                    return;
                }
                zzvfVarZzfr.zzm(com.google.android.gms.dynamic.zzn.zzw(view));
                if (!zzvfVarZzfr.getOverrideImpressionRecording()) {
                    zzvfVarZzfr.recordImpression();
                }
                zzakaVar.zziw().zza("/nativeExpressViewClicked", zzar.zza((zzvc) null, zzvfVarZzfr, zzabVar));
            } catch (RemoteException e) {
                zzafr.zzc("Error occurred while recording impression and registering for clicks", e);
            }
        }
    }

    /* JADX WARN: Code duplicated, block: B:48:0x00e9  */
    @Override // com.google.android.gms.ads.internal.zzi, com.google.android.gms.ads.internal.zzd, com.google.android.gms.ads.internal.zza
    public final boolean zza(@Nullable zzaff zzaffVar, zzaff zzaffVar2) {
        if (!super.zza(zzaffVar, zzaffVar2)) {
            return false;
        }
        if (this.zzsP.zzcc() && !zzd(zzaffVar, zzaffVar2)) {
            zze(0);
            return false;
        }
        if (zzaffVar2.zzTG) {
            zzc(zzaffVar2);
            zzbs.zzbX();
            zzajv.zza((View) this.zzsP.zzvU, (ViewTreeObserver.OnGlobalLayoutListener) this);
            zzbs.zzbX();
            zzajv.zza((View) this.zzsP.zzvU, (ViewTreeObserver.OnScrollChangedListener) this);
            if (!zzaffVar2.zzXM) {
                zzy zzyVar = new zzy(this);
                zzakb zzakbVarZziw = zzaffVar2.zzPg != null ? zzaffVar2.zzPg.zziw() : null;
                if (zzakbVarZziw != null) {
                    zzakbVarZziw.zza(new zzz(this, zzaffVar2, zzyVar));
                }
            }
        } else if (this.zzsP.zzcd()) {
            if (((Boolean) zzbs.zzbL().zzd(zzmo.zzFo)).booleanValue()) {
                zza(zzaffVar2, false);
            }
        } else {
            zza(zzaffVar2, false);
        }
        if (zzaffVar2.zzPg != null) {
            zzaks zzaksVarZziH = zzaffVar2.zzPg.zziH();
            zzakb zzakbVarZziw2 = zzaffVar2.zzPg.zziw();
            if (zzakbVarZziw2 != null) {
                zzakbVarZziw2.zziV();
            }
            if (this.zzsP.zzwk != null && zzaksVarZziH != null) {
                zzaksVarZziH.zzb(this.zzsP.zzwk);
            }
        }
        if (this.zzsP.zzcc()) {
            if (zzaffVar2.zzPg != null) {
                if (zzaffVar2.zzXL != null) {
                    this.zzsR.zza(this.zzsP.zzvX, zzaffVar2);
                }
                zzge zzgeVar = new zzge(this.zzsP.zzqD, zzaffVar2.zzPg.getView());
                if (zzbs.zzbY().zzq(this.zzsP.zzqD)) {
                    zzgeVar.zza(new zzaev(this.zzsP.zzqD, this.zzsP.zzvR));
                }
                if (zzaffVar2.zzcn()) {
                    zzgeVar.zza(zzaffVar2.zzPg);
                } else {
                    zzaffVar2.zzPg.zziw().zza(new zzaa(this, zzgeVar, zzaffVar2));
                }
            }
        } else if (this.zzsP.zzws != null && zzaffVar2.zzXL != null) {
            this.zzsR.zza(this.zzsP.zzvX, zzaffVar2, this.zzsP.zzws);
        }
        return true;
    }

    @Override // com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public final boolean zza(zzir zzirVar) {
        if (zzirVar.zzzS != this.zzsu) {
            zzirVar = new zzir(zzirVar.versionCode, zzirVar.zzzN, zzirVar.extras, zzirVar.zzzO, zzirVar.zzzP, zzirVar.zzzQ, zzirVar.zzzR, zzirVar.zzzS || this.zzsu, zzirVar.zzzT, zzirVar.zzzU, zzirVar.zzzV, zzirVar.zzzW, zzirVar.zzzX, zzirVar.zzzY, zzirVar.zzzZ, zzirVar.zzAa, zzirVar.zzAb, zzirVar.zzAc);
        }
        return super.zza(zzirVar);
    }

    @Override // com.google.android.gms.ads.internal.zzd
    protected final boolean zzaz() {
        boolean z = true;
        zzbs.zzbz();
        if (!zzagz.zzc(this.zzsP.zzqD, this.zzsP.zzqD.getPackageName(), "android.permission.INTERNET")) {
            zzji.zzds().zza(this.zzsP.zzvU, this.zzsP.zzvX, "Missing internet permission in AndroidManifest.xml.", "Missing internet permission in AndroidManifest.xml. You must have the following declaration: <uses-permission android:name=\"android.permission.INTERNET\" />");
            z = false;
        }
        zzbs.zzbz();
        if (!zzagz.zzD(this.zzsP.zzqD)) {
            zzji.zzds().zza(this.zzsP.zzvU, this.zzsP.zzvX, "Missing AdActivity with android:configChanges in AndroidManifest.xml.", "Missing AdActivity with android:configChanges in AndroidManifest.xml. You must have the following declaration within the <application> element: <activity android:name=\"com.google.android.gms.ads.AdActivity\" android:configChanges=\"keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize\" />");
            z = false;
        }
        if (!z && this.zzsP.zzvU != null) {
            this.zzsP.zzvU.setVisibility(0);
        }
        return z;
    }

    final void zzc(@Nullable zzaff zzaffVar) {
        if (zzaffVar == null || zzaffVar.zzXM || this.zzsP.zzvU == null || !zzbs.zzbz().zza(this.zzsP.zzvU, this.zzsP.zzqD) || !this.zzsP.zzvU.getGlobalVisibleRect(new Rect(), null)) {
            return;
        }
        if (zzaffVar != null && zzaffVar.zzPg != null && zzaffVar.zzPg.zziw() != null) {
            zzaffVar.zzPg.zziw().zza((zzakj) null);
        }
        zza(zzaffVar, false);
        zzaffVar.zzXM = true;
    }
}
