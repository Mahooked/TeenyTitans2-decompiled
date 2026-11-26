package com.google.android.gms.internal;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.view.View;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zznq extends zzpc implements zzob {
    private Bundle mExtras;
    private Object mLock = new Object();
    private String zzHB;
    private List<zznp> zzHC;
    private String zzHD;
    private zzos zzHE;
    private String zzHF;
    private double zzHG;
    private String zzHH;
    private String zzHI;

    @Nullable
    private zznn zzHJ;

    @Nullable
    private zzks zzHK;

    @Nullable
    private View zzHL;
    private zzny zzHM;

    public zznq(String str, List list, String str2, zzos zzosVar, String str3, double d, String str4, String str5, @Nullable zznn zznnVar, Bundle bundle, zzks zzksVar, View view) {
        this.zzHB = str;
        this.zzHC = list;
        this.zzHD = str2;
        this.zzHE = zzosVar;
        this.zzHF = str3;
        this.zzHG = d;
        this.zzHH = str4;
        this.zzHI = str5;
        this.zzHJ = zznnVar;
        this.mExtras = bundle;
        this.zzHK = zzksVar;
        this.zzHL = view;
    }

    static /* synthetic */ zzny zza(zznq zznqVar, zzny zznyVar) {
        zznqVar.zzHM = null;
        return null;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final void destroy() {
        zzagz.zzZr.post(new zznr(this));
        this.zzHB = null;
        this.zzHC = null;
        this.zzHD = null;
        this.zzHE = null;
        this.zzHF = null;
        this.zzHG = 0.0d;
        this.zzHH = null;
        this.zzHI = null;
        this.zzHJ = null;
        this.mExtras = null;
        this.mLock = null;
        this.zzHK = null;
        this.zzHL = null;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final String getBody() {
        return this.zzHD;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final String getCallToAction() {
        return this.zzHF;
    }

    @Override // com.google.android.gms.internal.zzoa
    public final String getCustomTemplateId() {
        return "";
    }

    @Override // com.google.android.gms.internal.zzpb
    public final Bundle getExtras() {
        return this.mExtras;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final String getHeadline() {
        return this.zzHB;
    }

    @Override // com.google.android.gms.internal.zzpb, com.google.android.gms.internal.zzob
    public final List getImages() {
        return this.zzHC;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final String getPrice() {
        return this.zzHI;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final double getStarRating() {
        return this.zzHG;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final String getStore() {
        return this.zzHH;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final zzks getVideoController() {
        return this.zzHK;
    }

    @Override // com.google.android.gms.internal.zzoa
    public final void zzb(zzny zznyVar) {
        synchronized (this.mLock) {
            this.zzHM = zznyVar;
        }
    }

    @Override // com.google.android.gms.internal.zzpb
    public final void zzc(Bundle bundle) {
        synchronized (this.mLock) {
            if (this.zzHM == null) {
                zzafr.e("Attempt to perform click before app install ad initialized.");
            } else {
                this.zzHM.zzc(bundle);
            }
        }
    }

    @Override // com.google.android.gms.internal.zzpb
    public final boolean zzd(Bundle bundle) {
        boolean zZzd;
        synchronized (this.mLock) {
            if (this.zzHM == null) {
                zzafr.e("Attempt to record impression before app install ad initialized.");
                zZzd = false;
            } else {
                zZzd = this.zzHM.zzd(bundle);
            }
        }
        return zZzd;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final void zze(Bundle bundle) {
        synchronized (this.mLock) {
            if (this.zzHM == null) {
                zzafr.e("Attempt to perform click before app install ad initialized.");
            } else {
                this.zzHM.zze(bundle);
            }
        }
    }

    @Override // com.google.android.gms.internal.zzpb
    public final zzos zzeh() {
        return this.zzHE;
    }

    @Override // com.google.android.gms.internal.zzpb
    public final IObjectWrapper zzei() {
        return com.google.android.gms.dynamic.zzn.zzw(this.zzHM);
    }

    @Override // com.google.android.gms.internal.zzoa
    public final String zzej() {
        return "2";
    }

    @Override // com.google.android.gms.internal.zzoa
    public final zznn zzek() {
        return this.zzHJ;
    }

    @Override // com.google.android.gms.internal.zzoa
    public final View zzel() {
        return this.zzHL;
    }
}
