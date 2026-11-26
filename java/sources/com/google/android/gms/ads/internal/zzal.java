package com.google.android.gms.ads.internal;

import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Rect;
import android.os.Bundle;
import android.os.RemoteException;
import android.support.annotation.Nullable;
import android.view.Window;
import com.google.ads.mediation.AbstractAdViewAdapter;
import com.google.android.gms.ads.internal.overlay.AdOverlayInfoParcel;
import com.google.android.gms.internal.zzaai;
import com.google.android.gms.internal.zzabt;
import com.google.android.gms.internal.zzaee;
import com.google.android.gms.internal.zzaet;
import com.google.android.gms.internal.zzaev;
import com.google.android.gms.internal.zzaff;
import com.google.android.gms.internal.zzafg;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzahe;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzakb;
import com.google.android.gms.internal.zzakm;
import com.google.android.gms.internal.zzge;
import com.google.android.gms.internal.zzir;
import com.google.android.gms.internal.zziv;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zznb;
import com.google.android.gms.internal.zzrm;
import com.google.android.gms.internal.zzru;
import com.google.android.gms.internal.zzrv;
import com.google.android.gms.internal.zzua;
import com.google.android.gms.internal.zzub;
import com.google.android.gms.internal.zzuq;
import com.google.android.gms.internal.zzzn;
import java.util.Collections;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzal extends zzi implements zzrm, zzrv {
    private transient boolean zzuf;
    private int zzug;
    private boolean zzuh;
    private float zzui;
    private boolean zzuj;
    private final zzaev zzuk;
    private String zzul;
    private final String zzum;

    public zzal(Context context, zziv zzivVar, String str, zzuq zzuqVar, zzaje zzajeVar, zzv zzvVar) {
        super(context, zzivVar, str, zzuqVar, zzajeVar, zzvVar);
        this.zzug = -1;
        this.zzuf = false;
        this.zzuk = zzbs.zzbY().zzs(context) ? new zzaev(context, str) : null;
        this.zzum = (zzivVar == null || !"reward_mb".equals(zzivVar.zzAs)) ? "/Interstitial" : "/Rewarded";
    }

    private static zzafg zzb(zzafg zzafgVar) {
        try {
            String string = zzabt.zzb(zzafgVar.zzXY).toString();
            JSONObject jSONObject = new JSONObject();
            jSONObject.put(AbstractAdViewAdapter.AD_UNIT_ID_PARAMETER, zzafgVar.zzUj.zzvR);
            zzua zzuaVar = new zzua(string, null, Collections.singletonList("com.google.ads.mediation.admob.AdMobAdapter"), null, null, Collections.emptyList(), Collections.emptyList(), jSONObject.toString(), null, Collections.emptyList(), Collections.emptyList(), null, null, null, null, null, Collections.emptyList(), null);
            zzaai zzaaiVar = zzafgVar.zzXY;
            zzub zzubVar = new zzub(Collections.singletonList(zzuaVar), ((Long) zzbs.zzbL().zzd(zzmo.zzEL)).longValue(), Collections.emptyList(), Collections.emptyList(), Collections.emptyList(), zzaaiVar.zzMd, zzaaiVar.zzMe, "", -1L, 0, 1, null, 0, -1, -1L, false);
            return new zzafg(zzafgVar.zzUj, new zzaai(zzafgVar.zzUj, zzaaiVar.zzPi, zzaaiVar.body, Collections.emptyList(), Collections.emptyList(), zzaaiVar.zzTn, true, zzaaiVar.zzTp, Collections.emptyList(), zzaaiVar.zzMg, zzaaiVar.orientation, zzaaiVar.zzTr, zzaaiVar.zzTs, zzaaiVar.zzTt, zzaaiVar.zzTu, zzaaiVar.zzTv, null, zzaaiVar.zzTx, zzaaiVar.zzAv, zzaaiVar.zzSH, zzaaiVar.zzTy, zzaaiVar.zzTz, zzaaiVar.zzTC, zzaaiVar.zzAw, zzaaiVar.zzAx, null, Collections.emptyList(), Collections.emptyList(), zzaaiVar.zzTG, zzaaiVar.zzTH, zzaaiVar.zzSV, zzaaiVar.zzSW, zzaaiVar.zzMd, zzaaiVar.zzMe, zzaaiVar.zzTI, null, zzaaiVar.zzTK, zzaaiVar.zzTL, zzaaiVar.zzTh), zzubVar, zzafgVar.zzvX, zzafgVar.errorCode, zzafgVar.zzXR, zzafgVar.zzXS, (JSONObject) null, zzafgVar.zzXX);
        } catch (JSONException e) {
            zzafr.zzb("Unable to generate ad state for an interstitial ad with pooling.", e);
            return zzafgVar;
        }
    }

    private final void zzb(Bundle bundle) {
        zzbs.zzbz().zzb(this.zzsP.zzqD, this.zzsP.zzvT.zzaP, "gmob-apps", bundle, false);
    }

    @Override // com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public final void setImmersiveMode(boolean z) {
        com.google.android.gms.common.internal.zzbo.zzcz("setImmersiveMode must be called on the main UI thread.");
        this.zzuj = z;
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.internal.zzjz
    public final void showInterstitial() {
        Bitmap bitmapZzK;
        com.google.android.gms.common.internal.zzbo.zzcz("showInterstitial must be called on the main UI thread.");
        if (zzbs.zzbY().zzs(this.zzsP.zzqD)) {
            this.zzul = zzbs.zzbY().zzt(this.zzsP.zzqD);
            String strValueOf = String.valueOf(this.zzul);
            String strValueOf2 = String.valueOf(this.zzum);
            this.zzul = strValueOf2.length() != 0 ? strValueOf.concat(strValueOf2) : new String(strValueOf);
        }
        if (this.zzsP.zzvY == null) {
            zzafr.zzaT("The interstitial has not loaded.");
            return;
        }
        if (((Boolean) zzbs.zzbL().zzd(zzmo.zzEv)).booleanValue()) {
            String packageName = this.zzsP.zzqD.getApplicationContext() != null ? this.zzsP.zzqD.getApplicationContext().getPackageName() : this.zzsP.zzqD.getPackageName();
            if (!this.zzuf) {
                zzafr.zzaT("It is not recommended to show an interstitial before onAdLoaded completes.");
                Bundle bundle = new Bundle();
                bundle.putString("appid", packageName);
                bundle.putString("action", "show_interstitial_before_load_finish");
                zzb(bundle);
            }
            zzbs.zzbz();
            if (!zzagz.zzJ(this.zzsP.zzqD)) {
                zzafr.zzaT("It is not recommended to show an interstitial when app is not in foreground.");
                Bundle bundle2 = new Bundle();
                bundle2.putString("appid", packageName);
                bundle2.putString("action", "show_interstitial_app_not_in_foreground");
                zzb(bundle2);
            }
        }
        if (this.zzsP.zzcd()) {
            return;
        }
        if (this.zzsP.zzvY.zzTo && this.zzsP.zzvY.zzMH != null) {
            try {
                if (((Boolean) zzbs.zzbL().zzd(zzmo.zzDT)).booleanValue()) {
                    this.zzsP.zzvY.zzMH.setImmersiveMode(this.zzuj);
                }
                this.zzsP.zzvY.zzMH.showInterstitial();
                return;
            } catch (RemoteException e) {
                zzafr.zzc("Could not show interstitial.", e);
                zzbb();
                return;
            }
        }
        if (this.zzsP.zzvY.zzPg == null) {
            zzafr.zzaT("The interstitial failed to load.");
            return;
        }
        if (this.zzsP.zzvY.zzPg.zziA()) {
            zzafr.zzaT("The interstitial is already showing.");
            return;
        }
        this.zzsP.zzvY.zzPg.zzA(true);
        if (this.zzsP.zzvY.zzXL != null) {
            this.zzsR.zza(this.zzsP.zzvX, this.zzsP.zzvY);
        }
        zzaff zzaffVar = this.zzsP.zzvY;
        if (zzaffVar.zzcn()) {
            new zzge(this.zzsP.zzqD, zzaffVar.zzPg.getView()).zza(zzaffVar.zzPg);
        } else {
            zzaffVar.zzPg.zziw().zza(new zzam(this, zzaffVar));
        }
        if (this.zzsP.zzur) {
            zzbs.zzbz();
            bitmapZzK = zzagz.zzK(this.zzsP.zzqD);
        } else {
            bitmapZzK = null;
        }
        this.zzug = zzbs.zzbU().zzb(bitmapZzK);
        if (((Boolean) zzbs.zzbL().zzd(zzmo.zzFh)).booleanValue() && bitmapZzK != null) {
            new zzan(this, this.zzug).zzhL();
            return;
        }
        zzap zzapVar = new zzap(this.zzsP.zzur, zzba(), false, 0.0f, -1, this.zzuj);
        int requestedOrientation = this.zzsP.zzvY.zzPg.getRequestedOrientation();
        if (requestedOrientation == -1) {
            requestedOrientation = this.zzsP.zzvY.orientation;
        }
        AdOverlayInfoParcel adOverlayInfoParcel = new AdOverlayInfoParcel(this, this, this, this.zzsP.zzvY.zzPg, requestedOrientation, this.zzsP.zzvT, this.zzsP.zzvY.zzTt, zzapVar);
        zzbs.zzbx();
        com.google.android.gms.ads.internal.overlay.zzu.zza(this.zzsP.zzqD, adOverlayInfoParcel, true);
    }

    @Override // com.google.android.gms.ads.internal.zzi
    protected final zzaka zza(zzafg zzafgVar, @Nullable zzw zzwVar, @Nullable zzaet zzaetVar) throws zzakm {
        zzaka zzakaVarZza = zzbs.zzbA().zza(this.zzsP.zzqD, this.zzsP.zzvX, false, false, this.zzsP.zzvS, this.zzsP.zzvT, this.zzsK, this, this.zzsS, zzafgVar.zzXX);
        zzakaVarZza.zziw().zza(this, null, this, this, ((Boolean) zzbs.zzbL().zzd(zzmo.zzDn)).booleanValue(), this, zzwVar, null, zzaetVar);
        zza((com.google.android.gms.ads.internal.js.zzai) zzakaVarZza);
        zzakaVarZza.zzaV(zzafgVar.zzUj.zzSM);
        zzakaVarZza.zziw().zza("/reward", new zzru(this));
        return zzakaVarZza;
    }

    @Override // com.google.android.gms.ads.internal.zzi, com.google.android.gms.ads.internal.zza
    public final void zza(zzafg zzafgVar, zznb zznbVar) {
        if (!((Boolean) zzbs.zzbL().zzd(zzmo.zzDY)).booleanValue()) {
            super.zza(zzafgVar, zznbVar);
            return;
        }
        if (zzafgVar.errorCode != -2) {
            super.zza(zzafgVar, zznbVar);
            return;
        }
        Bundle bundle = zzafgVar.zzUj.zzSz.zzzX.getBundle("com.google.ads.mediation.admob.AdMobAdapter");
        boolean z = bundle == null || !bundle.containsKey("gw");
        boolean z2 = zzafgVar.zzXY.zzTo ? false : true;
        if (z && z2) {
            this.zzsP.zzvZ = zzb(zzafgVar);
        }
        super.zza(this.zzsP.zzvZ, zznbVar);
    }

    @Override // com.google.android.gms.internal.zzrm
    public final void zza(boolean z, float f) {
        this.zzuh = z;
        this.zzui = f;
    }

    @Override // com.google.android.gms.ads.internal.zzi, com.google.android.gms.ads.internal.zzd, com.google.android.gms.ads.internal.zza
    public final boolean zza(@Nullable zzaff zzaffVar, zzaff zzaffVar2) {
        if (!super.zza(zzaffVar, zzaffVar2)) {
            return false;
        }
        if (!this.zzsP.zzcc() && this.zzsP.zzws != null && zzaffVar2.zzXL != null) {
            this.zzsR.zza(this.zzsP.zzvX, zzaffVar2, this.zzsP.zzws);
        }
        return true;
    }

    @Override // com.google.android.gms.ads.internal.zzd
    protected final boolean zza(zzir zzirVar, zzaff zzaffVar, boolean z) {
        if (this.zzsP.zzcc() && zzaffVar.zzPg != null) {
            zzbs.zzbB();
            zzahe.zzk(zzaffVar.zzPg);
        }
        return this.zzsO.zzbo();
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.ads.internal.zza
    public final boolean zza(zzir zzirVar, zznb zznbVar) {
        if (this.zzsP.zzvY == null) {
            return super.zza(zzirVar, zznbVar);
        }
        zzafr.zzaT("An interstitial is already loading. Aborting.");
        return false;
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.ads.internal.overlay.zzw
    public final void zzaA() {
        super.zzaA();
        this.zzsR.zzg(this.zzsP.zzvY);
        if (zzbs.zzbY().zzs(this.zzsP.zzqD)) {
            this.zzuk.zzu(false);
        }
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.ads.internal.overlay.zzw
    public final void zzaB() {
        zzakb zzakbVarZziw;
        recordImpression();
        super.zzaB();
        if (this.zzsP.zzvY != null && this.zzsP.zzvY.zzPg != null && (zzakbVarZziw = this.zzsP.zzvY.zzPg.zziw()) != null) {
            zzakbVarZziw.zziV();
        }
        if (zzbs.zzbY().zzs(this.zzsP.zzqD)) {
            if (this.zzsP.zzvY != null && this.zzsP.zzvY.zzPg != null) {
                zzbs.zzbY().zze(this.zzsP.zzvY.zzPg.getContext(), this.zzul);
            }
            this.zzuk.zzu(true);
        }
    }

    @Override // com.google.android.gms.ads.internal.zza
    protected final void zzap() {
        zzbb();
        super.zzap();
    }

    @Override // com.google.android.gms.ads.internal.zzi, com.google.android.gms.ads.internal.zza
    protected final void zzas() {
        super.zzas();
        this.zzuf = true;
    }

    @Override // com.google.android.gms.internal.zzrv
    public final void zzb(zzaee zzaeeVar) {
        if (this.zzsP.zzvY != null) {
            if (this.zzsP.zzvY.zzTF != null) {
                zzbs.zzbz();
                zzagz.zza(this.zzsP.zzqD, this.zzsP.zzvT.zzaP, this.zzsP.zzvY.zzTF);
            }
            if (this.zzsP.zzvY.zzTD != null) {
                zzaeeVar = this.zzsP.zzvY.zzTD;
            }
        }
        zza(zzaeeVar);
    }

    protected final boolean zzba() {
        if (!(this.zzsP.zzqD instanceof Activity)) {
            return false;
        }
        Window window = ((Activity) this.zzsP.zzqD).getWindow();
        if (window == null || window.getDecorView() == null) {
            return false;
        }
        Rect rect = new Rect();
        Rect rect2 = new Rect();
        window.getDecorView().getGlobalVisibleRect(rect, null);
        window.getDecorView().getWindowVisibleDisplayFrame(rect2);
        return (rect.bottom == 0 || rect2.bottom == 0 || rect.top != rect2.top) ? false : true;
    }

    public final void zzbb() {
        zzbs.zzbU().zzb(Integer.valueOf(this.zzug));
        if (this.zzsP.zzcc()) {
            this.zzsP.zzca();
            this.zzsP.zzvY = null;
            this.zzsP.zzur = false;
            this.zzuf = false;
        }
    }

    @Override // com.google.android.gms.internal.zzrv
    public final void zzbc() {
        if (this.zzsP.zzvY != null && this.zzsP.zzvY.zzXQ != null) {
            zzbs.zzbz();
            zzagz.zza(this.zzsP.zzqD, this.zzsP.zzvT.zzaP, this.zzsP.zzvY.zzXQ);
        }
        zzav();
    }

    @Override // com.google.android.gms.internal.zzrm
    public final void zzc(boolean z) {
        this.zzsP.zzur = z;
    }
}
