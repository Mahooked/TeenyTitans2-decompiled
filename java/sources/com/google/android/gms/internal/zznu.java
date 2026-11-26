package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import android.support.v4.util.SimpleArrayMap;
import android.view.View;
import android.widget.FrameLayout;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.lang.ref.WeakReference;
import java.util.Arrays;
import java.util.List;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zznu extends zzpk implements zzoa {
    private final Object mLock = new Object();
    private final zznn zzHJ;

    @Nullable
    private zzks zzHK;

    @Nullable
    private View zzHL;
    private zzny zzHM;
    private final String zzHR;
    private final SimpleArrayMap<String, zznp> zzHS;
    private final SimpleArrayMap<String, String> zzHT;

    public zznu(String str, SimpleArrayMap<String, zznp> simpleArrayMap, SimpleArrayMap<String, String> simpleArrayMap2, zznn zznnVar, zzks zzksVar, View view) {
        this.zzHR = str;
        this.zzHS = simpleArrayMap;
        this.zzHT = simpleArrayMap2;
        this.zzHJ = zznnVar;
        this.zzHK = zzksVar;
        this.zzHL = view;
    }

    @Override // com.google.android.gms.internal.zzpj
    public final void destroy() {
        this.zzHM = null;
        this.zzHK = null;
        this.zzHL = null;
    }

    @Override // com.google.android.gms.internal.zzpj
    public final List<String> getAvailableAssetNames() {
        int i = 0;
        String[] strArr = new String[this.zzHS.size() + this.zzHT.size()];
        int i2 = 0;
        for (int i3 = 0; i3 < this.zzHS.size(); i3++) {
            strArr[i2] = this.zzHS.keyAt(i3);
            i2++;
        }
        while (i < this.zzHT.size()) {
            strArr[i2] = this.zzHT.keyAt(i);
            i++;
            i2++;
        }
        return Arrays.asList(strArr);
    }

    @Override // com.google.android.gms.internal.zzpj, com.google.android.gms.internal.zzoa
    public final String getCustomTemplateId() {
        return this.zzHR;
    }

    @Override // com.google.android.gms.internal.zzpj
    public final zzks getVideoController() {
        return this.zzHK;
    }

    @Override // com.google.android.gms.internal.zzpj
    public final void performClick(String str) {
        synchronized (this.mLock) {
            if (this.zzHM == null) {
                zzajc.e("Attempt to call performClick before ad initialized.");
            } else {
                this.zzHM.zza(null, str, null, null, null);
            }
        }
    }

    @Override // com.google.android.gms.internal.zzpj
    public final void recordImpression() {
        synchronized (this.mLock) {
            if (this.zzHM == null) {
                zzajc.e("Attempt to perform recordImpression before ad initialized.");
            } else {
                this.zzHM.zza((View) null, (Map<String, WeakReference<View>>) null);
            }
        }
    }

    @Override // com.google.android.gms.internal.zzpj
    public final String zzP(String str) {
        return this.zzHT.get(str);
    }

    @Override // com.google.android.gms.internal.zzpj
    public final zzos zzQ(String str) {
        return this.zzHS.get(str);
    }

    @Override // com.google.android.gms.internal.zzoa
    public final void zzb(zzny zznyVar) {
        synchronized (this.mLock) {
            this.zzHM = zznyVar;
        }
    }

    @Override // com.google.android.gms.internal.zzpj
    public final IObjectWrapper zzei() {
        return com.google.android.gms.dynamic.zzn.zzw(this.zzHM);
    }

    @Override // com.google.android.gms.internal.zzoa
    public final String zzej() {
        return "3";
    }

    @Override // com.google.android.gms.internal.zzoa
    public final zznn zzek() {
        return this.zzHJ;
    }

    @Override // com.google.android.gms.internal.zzoa
    public final View zzel() {
        return this.zzHL;
    }

    @Override // com.google.android.gms.internal.zzpj
    public final IObjectWrapper zzen() {
        return com.google.android.gms.dynamic.zzn.zzw(this.zzHM.getContext().getApplicationContext());
    }

    @Override // com.google.android.gms.internal.zzpj
    public final boolean zzj(IObjectWrapper iObjectWrapper) {
        if (this.zzHM == null) {
            zzajc.e("Attempt to call renderVideoInMediaView before ad initialized.");
            return false;
        }
        if (this.zzHL == null) {
            return false;
        }
        zznv zznvVar = new zznv(this);
        this.zzHM.zza((FrameLayout) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper), zznvVar);
        return true;
    }
}
