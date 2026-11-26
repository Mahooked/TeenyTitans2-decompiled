package com.google.android.gms.internal;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.view.View;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzns extends zzpg implements zzob {
    private Bundle mExtras;
    private Object mLock = new Object();
    private String zzHB;
    private List<zznp> zzHC;
    private String zzHD;
    private String zzHF;

    @Nullable
    private zznn zzHJ;

    @Nullable
    private zzks zzHK;

    @Nullable
    private View zzHL;
    private zzny zzHM;
    private zzos zzHO;
    private String zzHP;

    public zzns(String str, List list, String str2, zzos zzosVar, String str3, String str4, @Nullable zznn zznnVar, Bundle bundle, zzks zzksVar, View view) {
        this.zzHB = str;
        this.zzHC = list;
        this.zzHD = str2;
        this.zzHO = zzosVar;
        this.zzHF = str3;
        this.zzHP = str4;
        this.zzHJ = zznnVar;
        this.mExtras = bundle;
        this.zzHK = zzksVar;
        this.zzHL = view;
    }

    static /* synthetic */ zzny zza(zzns zznsVar, zzny zznyVar) {
        zznsVar.zzHM = null;
        return null;
    }

    @Override // com.google.android.gms.internal.zzpf
    public final void destroy() {
        zzagz.zzZr.post(new zznt(this));
        this.zzHB = null;
        this.zzHC = null;
        this.zzHD = null;
        this.zzHO = null;
        this.zzHF = null;
        this.zzHP = null;
        this.zzHJ = null;
        this.mExtras = null;
        this.mLock = null;
        this.zzHK = null;
        this.zzHL = null;
    }

    @Override // com.google.android.gms.internal.zzpf
    public final String getAdvertiser() {
        return this.zzHP;
    }

    @Override // com.google.android.gms.internal.zzpf
    public final String getBody() {
        return this.zzHD;
    }

    @Override // com.google.android.gms.internal.zzpf
    public final String getCallToAction() {
        return this.zzHF;
    }

    @Override // com.google.android.gms.internal.zzoa
    public final String getCustomTemplateId() {
        return "";
    }

    @Override // com.google.android.gms.internal.zzpf
    public final Bundle getExtras() {
        return this.mExtras;
    }

    @Override // com.google.android.gms.internal.zzpf
    public final String getHeadline() {
        return this.zzHB;
    }

    @Override // com.google.android.gms.internal.zzpf, com.google.android.gms.internal.zzob
    public final List getImages() {
        return this.zzHC;
    }

    @Override // com.google.android.gms.internal.zzpf
    public final zzks getVideoController() {
        return this.zzHK;
    }

    @Override // com.google.android.gms.internal.zzoa
    public final void zzb(zzny zznyVar) {
        synchronized (this.mLock) {
            this.zzHM = zznyVar;
        }
    }

    @Override // com.google.android.gms.internal.zzpf
    public final void zzc(Bundle bundle) {
        synchronized (this.mLock) {
            if (this.zzHM == null) {
                zzafr.e("Attempt to perform click before content ad initialized.");
            } else {
                this.zzHM.zzc(bundle);
            }
        }
    }

    @Override // com.google.android.gms.internal.zzpf
    public final boolean zzd(Bundle bundle) {
        boolean zZzd;
        synchronized (this.mLock) {
            if (this.zzHM == null) {
                zzafr.e("Attempt to record impression before content ad initialized.");
                zZzd = false;
            } else {
                zZzd = this.zzHM.zzd(bundle);
            }
        }
        return zZzd;
    }

    @Override // com.google.android.gms.internal.zzpf
    public final void zze(Bundle bundle) {
        synchronized (this.mLock) {
            if (this.zzHM == null) {
                zzafr.e("Attempt to perform click before app install ad initialized.");
            } else {
                this.zzHM.zze(bundle);
            }
        }
    }

    @Override // com.google.android.gms.internal.zzpf
    public final IObjectWrapper zzei() {
        return com.google.android.gms.dynamic.zzn.zzw(this.zzHM);
    }

    @Override // com.google.android.gms.internal.zzoa
    public final String zzej() {
        return "1";
    }

    @Override // com.google.android.gms.internal.zzoa
    public final zznn zzek() {
        return this.zzHJ;
    }

    @Override // com.google.android.gms.internal.zzoa
    public final View zzel() {
        return this.zzHL;
    }

    @Override // com.google.android.gms.internal.zzpf
    public final zzos zzem() {
        return this.zzHO;
    }
}
