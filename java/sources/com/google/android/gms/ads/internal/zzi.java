package com.google.android.gms.ads.internal;

import android.content.Context;
import android.support.annotation.Nullable;
import android.view.View;
import com.google.android.gms.internal.zzaet;
import com.google.android.gms.internal.zzaff;
import com.google.android.gms.internal.zzafg;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzakm;
import com.google.android.gms.internal.zziv;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zznb;
import com.google.android.gms.internal.zznh;
import com.google.android.gms.internal.zzuq;
import com.google.android.gms.internal.zzwv;
import com.google.android.gms.internal.zzxx;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public class zzi extends zzd implements zzag, zzwv {
    private boolean zzta;

    public zzi(Context context, zziv zzivVar, String str, zzuq zzuqVar, zzaje zzajeVar, zzv zzvVar) {
        super(context, zzivVar, str, zzuqVar, zzajeVar, zzvVar);
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
    protected zzaka zza(zzafg zzafgVar, @Nullable zzw zzwVar, @Nullable zzaet zzaetVar) throws zzakm {
        zzaka zzakaVarZza = null;
        View nextView = this.zzsP.zzvU.getNextView();
        if (nextView instanceof zzaka) {
            zzakaVarZza = (zzaka) nextView;
            if (((Boolean) zzbs.zzbL().zzd(zzmo.zzDE)).booleanValue()) {
                zzafr.zzaC("Reusing webview...");
                zzakaVarZza.zza(this.zzsP.zzqD, this.zzsP.zzvX, this.zzsK);
            } else {
                zzakaVarZza.destroy();
                zzakaVarZza = null;
            }
        }
        if (zzakaVarZza == null) {
            if (nextView != 0) {
                this.zzsP.zzvU.removeView(nextView);
            }
            zzakaVarZza = zzbs.zzbA().zza(this.zzsP.zzqD, this.zzsP.zzvX, false, false, this.zzsP.zzvS, this.zzsP.zzvT, this.zzsK, this, this.zzsS, zzafgVar.zzXX);
            if (this.zzsP.zzvX.zzAu == null) {
                zzb(zzakaVarZza.getView());
            }
        }
        zzaka zzakaVar = zzakaVarZza;
        zzakaVar.zziw().zza(this, this, this, this, false, null, zzwVar, this, zzaetVar);
        zza((com.google.android.gms.ads.internal.js.zzai) zzakaVar);
        zzakaVar.zzaV(zzafgVar.zzUj.zzSM);
        return zzakaVar;
    }

    @Override // com.google.android.gms.internal.zzwv
    public final void zza(int i, int i2, int i3, int i4) {
        zzar();
    }

    protected final void zza(com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        zzaiVar.zza("/trackActiveViewUnit", new zzj(this));
    }

    @Override // com.google.android.gms.ads.internal.zza
    protected void zza(zzafg zzafgVar, zznb zznbVar) {
        if (zzafgVar.errorCode != -2) {
            zzagz.zzZr.post(new zzk(this, zzafgVar));
            return;
        }
        if (zzafgVar.zzvX != null) {
            this.zzsP.zzvX = zzafgVar.zzvX;
        }
        if (!zzafgVar.zzXY.zzTo || zzafgVar.zzXY.zzAx) {
            zzagz.zzZr.post(new zzl(this, zzafgVar, this.zzsS.zzto.zza(this.zzsP.zzqD, this.zzsP.zzvT, zzafgVar.zzXY), zznbVar));
        } else {
            this.zzsP.zzwt = 0;
            zzbt zzbtVar = this.zzsP;
            zzbs.zzby();
            zzbtVar.zzvW = zzxx.zza(this.zzsP.zzqD, this, zzafgVar, this.zzsP.zzvS, null, this.zzsX, this, zznbVar);
        }
    }

    final void zza(zzaka zzakaVar) {
        if (this.zzsP.zzvY != null) {
            this.zzsR.zza(this.zzsP.zzvX, this.zzsP.zzvY, zzakaVar.getView(), zzakaVar);
            this.zzta = false;
        } else {
            this.zzta = true;
            zzafr.zzaT("Request to enable ActiveView before adState is available.");
        }
    }

    @Override // com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public final void zza(zznh zznhVar) {
        com.google.android.gms.common.internal.zzbo.zzcz("setOnCustomRenderedAdLoadedListener must be called on the main UI thread.");
        this.zzsP.zzwo = zznhVar;
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.ads.internal.zza
    protected boolean zza(@Nullable zzaff zzaffVar, zzaff zzaffVar2) {
        if (this.zzsP.zzcc() && this.zzsP.zzvU != null) {
            this.zzsP.zzvU.zzcf().zzaP(zzaffVar2.zzTt);
        }
        return super.zza(zzaffVar, zzaffVar2);
    }

    @Override // com.google.android.gms.ads.internal.zzag
    public final void zzaL() {
        onAdClicked();
    }

    @Override // com.google.android.gms.ads.internal.zzag
    public final void zzaM() {
        recordImpression();
        zzao();
    }

    @Override // com.google.android.gms.internal.zzwv
    public final void zzaN() {
        zzap();
    }

    @Override // com.google.android.gms.ads.internal.zza
    protected void zzas() {
        super.zzas();
        if (this.zzta) {
            if (((Boolean) zzbs.zzbL().zzd(zzmo.zzFC)).booleanValue()) {
                zza(this.zzsP.zzvY.zzPg);
            }
        }
    }

    @Override // com.google.android.gms.ads.internal.zzag
    public final void zzc(View view) {
        this.zzsP.zzws = view;
        zzb(new zzaff(this.zzsP.zzvZ, null, null, null, null, null, null, null));
    }
}
