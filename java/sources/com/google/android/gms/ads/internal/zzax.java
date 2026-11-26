package com.google.android.gms.ads.internal;

import android.content.Context;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzahq;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zzko;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzax extends zzko {
    private static final Object zzuF = new Object();

    @Nullable
    private static zzax zzuG;
    private final Context mContext;
    private boolean zzuI;
    private zzaje zzuK;
    private final Object mLock = new Object();
    private float zzuJ = -1.0f;
    private boolean zzuH = false;

    private zzax(Context context, zzaje zzajeVar) {
        this.mContext = context;
        this.zzuK = zzajeVar;
    }

    public static zzax zza(Context context, zzaje zzajeVar) {
        zzax zzaxVar;
        synchronized (zzuF) {
            if (zzuG == null) {
                zzuG = new zzax(context.getApplicationContext(), zzajeVar);
            }
            zzaxVar = zzuG;
        }
        return zzaxVar;
    }

    @Nullable
    public static zzax zzbe() {
        zzax zzaxVar;
        synchronized (zzuF) {
            zzaxVar = zzuG;
        }
        return zzaxVar;
    }

    @Override // com.google.android.gms.internal.zzkn
    public final void initialize() {
        synchronized (zzuF) {
            if (this.zzuH) {
                zzafr.zzaT("Mobile ads is initialized already.");
                return;
            }
            this.zzuH = true;
            zzmo.initialize(this.mContext);
            zzbs.zzbD().zzd(this.mContext, this.zzuK);
            zzbs.zzbE().initialize(this.mContext);
        }
    }

    @Override // com.google.android.gms.internal.zzkn
    public final void setAppMuted(boolean z) {
        synchronized (this.mLock) {
            this.zzuI = z;
        }
    }

    @Override // com.google.android.gms.internal.zzkn
    public final void setAppVolume(float f) {
        synchronized (this.mLock) {
            this.zzuJ = f;
        }
    }

    @Override // com.google.android.gms.internal.zzkn
    public final void zzb(IObjectWrapper iObjectWrapper, String str) {
        if (iObjectWrapper == null) {
            zzafr.e("Wrapped context is null. Failed to open debug menu.");
            return;
        }
        Context context = (Context) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper);
        if (context == null) {
            zzafr.e("Context is null. Failed to open debug menu.");
            return;
        }
        zzahq zzahqVar = new zzahq(context);
        zzahqVar.setAdUnitId(str);
        zzahqVar.zzaO(this.zzuK.zzaP);
        zzahqVar.showDialog();
    }

    public final float zzbf() {
        float f;
        synchronized (this.mLock) {
            f = this.zzuJ;
        }
        return f;
    }

    public final boolean zzbg() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzuJ >= 0.0f;
        }
        return z;
    }

    public final boolean zzbh() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzuI;
        }
        return z;
    }

    @Override // com.google.android.gms.internal.zzkn
    public final void zzc(String str, IObjectWrapper iObjectWrapper) {
        zzay zzayVar;
        boolean z;
        if (TextUtils.isEmpty(str)) {
            return;
        }
        zzmo.initialize(this.mContext);
        boolean zBooleanValue = ((Boolean) zzbs.zzbL().zzd(zzmo.zzFO)).booleanValue() | ((Boolean) zzbs.zzbL().zzd(zzmo.zzDH)).booleanValue();
        if (((Boolean) zzbs.zzbL().zzd(zzmo.zzDH)).booleanValue()) {
            zzayVar = new zzay(this, (Runnable) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper));
            z = true;
        } else {
            zzayVar = null;
            z = zBooleanValue;
        }
        if (z) {
            zzbs.zzbV().zza(this.mContext, this.zzuK, str, zzayVar);
        }
    }

    @Override // com.google.android.gms.internal.zzkn
    public final void zzu(String str) {
        zzmo.initialize(this.mContext);
        if (TextUtils.isEmpty(str)) {
            return;
        }
        if (((Boolean) zzbs.zzbL().zzd(zzmo.zzFO)).booleanValue()) {
            zzbs.zzbV().zza(this.mContext, this.zzuK, str, null);
        }
    }
}
