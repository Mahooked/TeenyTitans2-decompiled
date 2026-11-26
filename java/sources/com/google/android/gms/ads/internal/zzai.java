package com.google.android.gms.ads.internal;

import android.content.Context;
import android.support.annotation.Nullable;
import android.support.v4.util.SimpleArrayMap;
import com.google.android.gms.ads.formats.PublisherAdViewOptions;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zzir;
import com.google.android.gms.internal.zziv;
import com.google.android.gms.internal.zzjo;
import com.google.android.gms.internal.zzjs;
import com.google.android.gms.internal.zzkk;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zzon;
import com.google.android.gms.internal.zzpn;
import com.google.android.gms.internal.zzpq;
import com.google.android.gms.internal.zzpt;
import com.google.android.gms.internal.zzpw;
import com.google.android.gms.internal.zzpz;
import com.google.android.gms.internal.zzuq;
import com.google.android.gms.internal.zzzn;
import java.lang.ref.WeakReference;
import java.util.ArrayList;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzai extends zzjs {
    private final Context mContext;
    private final zzv zzsS;
    private final zzuq zzsX;
    private final zzjo zztK;

    @Nullable
    private final zzpn zztL;

    @Nullable
    private final zzpq zztM;

    @Nullable
    private final zzpz zztN;

    @Nullable
    private final zziv zztO;

    @Nullable
    private final PublisherAdViewOptions zztP;
    private final SimpleArrayMap<String, zzpw> zztQ;
    private final SimpleArrayMap<String, zzpt> zztR;
    private final zzon zztS;
    private final zzkk zztU;
    private final String zztV;
    private final zzaje zztW;

    @Nullable
    private WeakReference<zzd> zztX;
    private final Object mLock = new Object();
    private final List<String> zztT = zzaY();

    zzai(Context context, String str, zzuq zzuqVar, zzaje zzajeVar, zzjo zzjoVar, zzpn zzpnVar, zzpq zzpqVar, SimpleArrayMap<String, zzpw> simpleArrayMap, SimpleArrayMap<String, zzpt> simpleArrayMap2, zzon zzonVar, zzkk zzkkVar, zzv zzvVar, zzpz zzpzVar, zziv zzivVar, PublisherAdViewOptions publisherAdViewOptions) {
        this.mContext = context;
        this.zztV = str;
        this.zzsX = zzuqVar;
        this.zztW = zzajeVar;
        this.zztK = zzjoVar;
        this.zztM = zzpqVar;
        this.zztL = zzpnVar;
        this.zztQ = simpleArrayMap;
        this.zztR = simpleArrayMap2;
        this.zztS = zzonVar;
        this.zztU = zzkkVar;
        this.zzsS = zzvVar;
        this.zztN = zzpzVar;
        this.zztO = zzivVar;
        this.zztP = publisherAdViewOptions;
        zzmo.initialize(this.mContext);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final boolean zzaW() {
        return ((Boolean) zzbs.zzbL().zzd(zzmo.zzDP)).booleanValue() && this.zztN != null;
    }

    private final boolean zzaX() {
        return (this.zztL == null && this.zztM == null && (this.zztQ == null || this.zztQ.size() <= 0)) ? false : true;
    }

    private final List<String> zzaY() {
        ArrayList arrayList = new ArrayList();
        if (this.zztM != null) {
            arrayList.add("1");
        }
        if (this.zztL != null) {
            arrayList.add("2");
        }
        if (this.zztQ.size() > 0) {
            arrayList.add("3");
        }
        return arrayList;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzd(zzir zzirVar) {
        zzq zzqVar = new zzq(this.mContext, this.zzsS, this.zztO, this.zztV, this.zzsX, this.zztW);
        this.zztX = new WeakReference<>(zzqVar);
        zzpz zzpzVar = this.zztN;
        com.google.android.gms.common.internal.zzbo.zzcz("setOnPublisherAdViewLoadedListener must be called on the main UI thread.");
        zzqVar.zzsP.zzwm = zzpzVar;
        if (this.zztP != null) {
            if (this.zztP.zzai() != null) {
                zzqVar.zza(this.zztP.zzai());
            }
            zzqVar.setManualImpressionsEnabled(this.zztP.getManualImpressionsEnabled());
        }
        zzpn zzpnVar = this.zztL;
        com.google.android.gms.common.internal.zzbo.zzcz("setOnAppInstallAdLoadedListener must be called on the main UI thread.");
        zzqVar.zzsP.zzwf = zzpnVar;
        zzpq zzpqVar = this.zztM;
        com.google.android.gms.common.internal.zzbo.zzcz("setOnContentAdLoadedListener must be called on the main UI thread.");
        zzqVar.zzsP.zzwg = zzpqVar;
        SimpleArrayMap<String, zzpw> simpleArrayMap = this.zztQ;
        com.google.android.gms.common.internal.zzbo.zzcz("setOnCustomTemplateAdLoadedListeners must be called on the main UI thread.");
        zzqVar.zzsP.zzwi = simpleArrayMap;
        SimpleArrayMap<String, zzpt> simpleArrayMap2 = this.zztR;
        com.google.android.gms.common.internal.zzbo.zzcz("setOnCustomClickListener must be called on the main UI thread.");
        zzqVar.zzsP.zzwh = simpleArrayMap2;
        zzon zzonVar = this.zztS;
        com.google.android.gms.common.internal.zzbo.zzcz("setNativeAdOptions must be called on the main UI thread.");
        zzqVar.zzsP.zzwj = zzonVar;
        zzqVar.zzc(zzaY());
        zzqVar.zza(this.zztK);
        zzqVar.zza(this.zztU);
        ArrayList arrayList = new ArrayList();
        if (zzaX()) {
            arrayList.add(1);
        }
        if (this.zztN != null) {
            arrayList.add(2);
        }
        zzqVar.zzd(arrayList);
        if (zzaX()) {
            zzirVar.extras.putBoolean("ina", true);
        }
        if (this.zztN != null) {
            zzirVar.extras.putBoolean("iba", true);
        }
        zzqVar.zza(zzirVar);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zze(zzir zzirVar) {
        zzbb zzbbVar = new zzbb(this.mContext, this.zzsS, zziv.zzg(this.mContext), this.zztV, this.zzsX, this.zztW);
        this.zztX = new WeakReference<>(zzbbVar);
        zzpn zzpnVar = this.zztL;
        com.google.android.gms.common.internal.zzbo.zzcz("setOnAppInstallAdLoadedListener must be called on the main UI thread.");
        zzbbVar.zzsP.zzwf = zzpnVar;
        zzpq zzpqVar = this.zztM;
        com.google.android.gms.common.internal.zzbo.zzcz("setOnContentAdLoadedListener must be called on the main UI thread.");
        zzbbVar.zzsP.zzwg = zzpqVar;
        SimpleArrayMap<String, zzpw> simpleArrayMap = this.zztQ;
        com.google.android.gms.common.internal.zzbo.zzcz("setOnCustomTemplateAdLoadedListeners must be called on the main UI thread.");
        zzbbVar.zzsP.zzwi = simpleArrayMap;
        zzbbVar.zza(this.zztK);
        SimpleArrayMap<String, zzpt> simpleArrayMap2 = this.zztR;
        com.google.android.gms.common.internal.zzbo.zzcz("setOnCustomClickListener must be called on the main UI thread.");
        zzbbVar.zzsP.zzwh = simpleArrayMap2;
        zzbbVar.zzc(zzaY());
        zzon zzonVar = this.zztS;
        com.google.android.gms.common.internal.zzbo.zzcz("setNativeAdOptions must be called on the main UI thread.");
        zzbbVar.zzsP.zzwj = zzonVar;
        zzbbVar.zza(this.zztU);
        zzbbVar.zza(zzirVar);
    }

    @Override // com.google.android.gms.internal.zzjr
    @Nullable
    public final String getMediationAdapterClassName() {
        synchronized (this.mLock) {
            if (this.zztX == null) {
                return null;
            }
            zzd zzdVar = this.zztX.get();
            return zzdVar != null ? zzdVar.getMediationAdapterClassName() : null;
        }
    }

    @Override // com.google.android.gms.internal.zzjr
    public final boolean isLoading() {
        synchronized (this.mLock) {
            if (this.zztX == null) {
                return false;
            }
            zzd zzdVar = this.zztX.get();
            return zzdVar != null ? zzdVar.isLoading() : false;
        }
    }

    @Override // com.google.android.gms.internal.zzjr
    @Nullable
    public final String zzaI() {
        synchronized (this.mLock) {
            if (this.zztX == null) {
                return null;
            }
            zzd zzdVar = this.zztX.get();
            return zzdVar != null ? zzdVar.zzaI() : null;
        }
    }

    @Override // com.google.android.gms.internal.zzjr
    public final void zzc(zzir zzirVar) {
        zzagz.zzZr.post(new zzaj(this, zzirVar));
    }
}
