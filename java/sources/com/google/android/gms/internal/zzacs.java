package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.google.ads.mediation.AbstractAdViewAdapter;
import com.google.android.gms.common.internal.zzbo;
import java.util.Arrays;
import java.util.Collections;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzacs extends com.google.android.gms.ads.internal.zzd implements zzadt {
    private static zzacs zzWr;
    private static final zzup zzWs = new zzup();
    private final Map<String, zzadz> zzWt;
    private boolean zzWu;
    private boolean zzuj;

    public zzacs(Context context, com.google.android.gms.ads.internal.zzv zzvVar, zziv zzivVar, zzuq zzuqVar, zzaje zzajeVar) {
        super(context, zzivVar, null, zzuqVar, zzajeVar, zzvVar);
        this.zzWt = new HashMap();
        zzWr = this;
    }

    private static zzafg zzc(zzafg zzafgVar) {
        zzafr.v("Creating mediation ad response for non-mediated rewarded ad.");
        try {
            String string = zzabt.zzb(zzafgVar.zzXY).toString();
            JSONObject jSONObject = new JSONObject();
            jSONObject.put(AbstractAdViewAdapter.AD_UNIT_ID_PARAMETER, zzafgVar.zzUj.zzvR);
            return new zzafg(zzafgVar.zzUj, zzafgVar.zzXY, new zzub(Arrays.asList(new zzua(string, null, Arrays.asList("com.google.ads.mediation.admob.AdMobAdapter"), null, null, Collections.emptyList(), Collections.emptyList(), jSONObject.toString(), null, Collections.emptyList(), Collections.emptyList(), null, null, null, null, null, Collections.emptyList(), null)), ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEL)).longValue(), Collections.emptyList(), Collections.emptyList(), Collections.emptyList(), Collections.emptyList(), false, "", -1L, 0, 1, null, 0, -1, -1L, false), zzafgVar.zzvX, zzafgVar.errorCode, zzafgVar.zzXR, zzafgVar.zzXS, zzafgVar.zzXL, zzafgVar.zzXX);
        } catch (JSONException e) {
            zzafr.zzb("Unable to generate ad state for non-mediated rewarded video.", e);
            return new zzafg(zzafgVar.zzUj, zzafgVar.zzXY, (zzub) null, zzafgVar.zzvX, 0, zzafgVar.zzXR, zzafgVar.zzXS, zzafgVar.zzXL, zzafgVar.zzXX);
        }
    }

    public static zzacs zzgO() {
        return zzWr;
    }

    @Override // com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public final void destroy() {
        zzbo.zzcz("destroy must be called on the main UI thread.");
        for (String str : this.zzWt.keySet()) {
            try {
                zzadz zzadzVar = this.zzWt.get(str);
                if (zzadzVar != null && zzadzVar.zzgW() != null) {
                    zzadzVar.zzgW().destroy();
                }
            } catch (RemoteException e) {
                String strValueOf = String.valueOf(str);
                zzafr.zzaT(strValueOf.length() != 0 ? "Fail to destroy adapter: ".concat(strValueOf) : new String("Fail to destroy adapter: "));
            }
        }
    }

    public final boolean isLoaded() {
        zzbo.zzcz("isLoaded must be called on the main UI thread.");
        return this.zzsP.zzvV == null && this.zzsP.zzvW == null && this.zzsP.zzvY != null && !this.zzWu;
    }

    public final void onContextChanged(@NonNull Context context) {
        Iterator<zzadz> it = this.zzWt.values().iterator();
        while (it.hasNext()) {
            try {
                it.next().zzgW().zzk(com.google.android.gms.dynamic.zzn.zzw(context));
            } catch (RemoteException e) {
                zzafr.zzb("Unable to call Adapter.onContextChanged.", e);
            }
        }
    }

    @Override // com.google.android.gms.internal.zzadt
    public final void onRewardedVideoAdClosed() {
        zzap();
    }

    @Override // com.google.android.gms.internal.zzadt
    public final void onRewardedVideoAdLeftApplication() {
        zzaq();
    }

    @Override // com.google.android.gms.internal.zzadt
    public final void onRewardedVideoAdOpened() {
        zza(this.zzsP.zzvY, false);
        zzar();
    }

    @Override // com.google.android.gms.internal.zzadt
    public final void onRewardedVideoStarted() {
        if (this.zzsP.zzvY != null && this.zzsP.zzvY.zzMG != null) {
            com.google.android.gms.ads.internal.zzbs.zzbS();
            zzuj.zza(this.zzsP.zzqD, this.zzsP.zzvT.zzaP, this.zzsP.zzvY, this.zzsP.zzvR, false, this.zzsP.zzvY.zzMG.zzLQ);
        }
        zzav();
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public final void pause() {
        zzbo.zzcz("pause must be called on the main UI thread.");
        for (String str : this.zzWt.keySet()) {
            try {
                zzadz zzadzVar = this.zzWt.get(str);
                if (zzadzVar != null && zzadzVar.zzgW() != null) {
                    zzadzVar.zzgW().pause();
                }
            } catch (RemoteException e) {
                String strValueOf = String.valueOf(str);
                zzafr.zzaT(strValueOf.length() != 0 ? "Fail to pause adapter: ".concat(strValueOf) : new String("Fail to pause adapter: "));
            }
        }
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public final void resume() {
        zzbo.zzcz("resume must be called on the main UI thread.");
        for (String str : this.zzWt.keySet()) {
            try {
                zzadz zzadzVar = this.zzWt.get(str);
                if (zzadzVar != null && zzadzVar.zzgW() != null) {
                    zzadzVar.zzgW().resume();
                }
            } catch (RemoteException e) {
                String strValueOf = String.valueOf(str);
                zzafr.zzaT(strValueOf.length() != 0 ? "Fail to resume adapter: ".concat(strValueOf) : new String("Fail to resume adapter: "));
            }
        }
    }

    @Override // com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public final void setImmersiveMode(boolean z) {
        zzbo.zzcz("setImmersiveMode must be called on the main UI thread.");
        this.zzuj = z;
    }

    public final void zza(zzadj zzadjVar) {
        zzbo.zzcz("loadAd must be called on the main UI thread.");
        if (TextUtils.isEmpty(zzadjVar.zzvR)) {
            zzafr.zzaT("Invalid ad unit id. Aborting.");
            zzagz.zzZr.post(new zzact(this));
        } else {
            this.zzWu = false;
            this.zzsP.zzvR = zzadjVar.zzvR;
            super.zza(zzadjVar.zzSz);
        }
    }

    @Override // com.google.android.gms.ads.internal.zza
    public final void zza(zzafg zzafgVar, zznb zznbVar) {
        if (zzafgVar.errorCode != -2) {
            zzagz.zzZr.post(new zzacu(this, zzafgVar));
            return;
        }
        this.zzsP.zzvZ = zzafgVar;
        if (zzafgVar.zzXN == null) {
            this.zzsP.zzvZ = zzc(zzafgVar);
        }
        this.zzsP.zzwt = 0;
        com.google.android.gms.ads.internal.zzbt zzbtVar = this.zzsP;
        com.google.android.gms.ads.internal.zzbs.zzby();
        zzadw zzadwVar = new zzadw(this.zzsP.zzqD, this.zzsP.zzvZ, this);
        String strValueOf = String.valueOf(zzadwVar.getClass().getName());
        zzafr.zzaC(strValueOf.length() != 0 ? "AdRenderer: ".concat(strValueOf) : new String("AdRenderer: "));
        zzadwVar.zzgp();
        zzbtVar.zzvW = zzadwVar;
    }

    @Override // com.google.android.gms.ads.internal.zzd, com.google.android.gms.ads.internal.zza
    public final boolean zza(zzaff zzaffVar, zzaff zzaffVar2) {
        return true;
    }

    @Override // com.google.android.gms.ads.internal.zzd
    protected final boolean zza(zzir zzirVar, zzaff zzaffVar, boolean z) {
        return false;
    }

    @Override // com.google.android.gms.ads.internal.zza
    protected final void zzap() {
        this.zzsP.zzvY = null;
        super.zzap();
    }

    @Nullable
    public final zzadz zzav(String str) {
        Exception exc;
        zzadz zzadzVar;
        zzadz zzadzVar2 = this.zzWt.get(str);
        if (zzadzVar2 != null) {
            return zzadzVar2;
        }
        try {
            zzadzVar = new zzadz(("com.google.ads.mediation.admob.AdMobAdapter".equals(str) ? zzWs : this.zzsX).zzah(str), this);
            try {
                this.zzWt.put(str, zzadzVar);
                return zzadzVar;
            } catch (Exception e) {
                exc = e;
                String strValueOf = String.valueOf(str);
                zzafr.zzc(strValueOf.length() != 0 ? "Fail to instantiate adapter ".concat(strValueOf) : new String("Fail to instantiate adapter "), exc);
                return zzadzVar;
            }
        } catch (Exception e2) {
            exc = e2;
            zzadzVar = zzadzVar2;
        }
    }

    @Override // com.google.android.gms.internal.zzadt
    public final void zzc(@Nullable zzaee zzaeeVar) {
        if (this.zzsP.zzvY != null && this.zzsP.zzvY.zzMG != null) {
            com.google.android.gms.ads.internal.zzbs.zzbS();
            zzuj.zza(this.zzsP.zzqD, this.zzsP.zzvT.zzaP, this.zzsP.zzvY, this.zzsP.zzvR, false, this.zzsP.zzvY.zzMG.zzLR);
        }
        if (this.zzsP.zzvY != null && this.zzsP.zzvY.zzXN != null && !TextUtils.isEmpty(this.zzsP.zzvY.zzXN.zzMh)) {
            zzaeeVar = new zzaee(this.zzsP.zzvY.zzXN.zzMh, this.zzsP.zzvY.zzXN.zzMi);
        }
        zza(zzaeeVar);
    }

    public final void zzgP() {
        zzbo.zzcz("showAd must be called on the main UI thread.");
        if (!isLoaded()) {
            zzafr.zzaT("The reward video has not loaded.");
            return;
        }
        this.zzWu = true;
        zzadz zzadzVarZzav = zzav(this.zzsP.zzvY.zzMI);
        if (zzadzVarZzav == null || zzadzVarZzav.zzgW() == null) {
            return;
        }
        try {
            zzadzVarZzav.zzgW().setImmersiveMode(this.zzuj);
            zzadzVarZzav.zzgW().showVideo();
        } catch (RemoteException e) {
            zzafr.zzc("Could not call showVideo.", e);
        }
    }

    @Override // com.google.android.gms.internal.zzadt
    public final void zzgQ() {
        onAdClicked();
    }
}
