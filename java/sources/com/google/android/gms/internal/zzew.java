package com.google.android.gms.internal;

import android.content.Context;
import android.net.Uri;
import android.view.MotionEvent;
import com.google.android.gms.ads.identifier.AdvertisingIdClient;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzew extends zzez {
    private final zzct zzsi;
    private final zzcu zzsj;
    private final zzcr zzsk;
    private boolean zzsl = false;

    public zzew(String str, Context context, boolean z) {
        this.zzsi = zzct.zza(str, context, z);
        this.zzsj = new zzcu(this.zzsi);
        this.zzsk = z ? null : zzcr.zzb(context);
    }

    private final IObjectWrapper zza(IObjectWrapper iObjectWrapper, IObjectWrapper iObjectWrapper2, boolean z) {
        try {
            Uri uri = (Uri) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper);
            Context context = (Context) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper2);
            return com.google.android.gms.dynamic.zzn.zzw(z ? this.zzsj.zza(uri, context) : this.zzsj.zza(uri, context, null));
        } catch (zzcv e) {
            return null;
        }
    }

    @Override // com.google.android.gms.internal.zzey
    public final IObjectWrapper zza(IObjectWrapper iObjectWrapper, IObjectWrapper iObjectWrapper2) {
        return zza(iObjectWrapper, iObjectWrapper2, true);
    }

    @Override // com.google.android.gms.internal.zzey
    public final String zza(IObjectWrapper iObjectWrapper, String str) {
        return this.zzsi.zza((Context) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper), str);
    }

    @Override // com.google.android.gms.internal.zzey
    public final String zza(IObjectWrapper iObjectWrapper, byte[] bArr) {
        Context context = (Context) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper);
        String strZza = this.zzsi.zza(context, bArr);
        if (this.zzsk == null || !this.zzsl) {
            return strZza;
        }
        String strZza2 = zzcr.zza(strZza, this.zzsk.zza(context, bArr));
        this.zzsl = false;
        return strZza2;
    }

    @Override // com.google.android.gms.internal.zzey
    public final boolean zza(IObjectWrapper iObjectWrapper) {
        return this.zzsj.zza((Uri) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper));
    }

    @Override // com.google.android.gms.internal.zzey
    public final String zzaf() {
        return "ms";
    }

    @Override // com.google.android.gms.internal.zzey
    public final IObjectWrapper zzb(IObjectWrapper iObjectWrapper, IObjectWrapper iObjectWrapper2) {
        return zza(iObjectWrapper, iObjectWrapper2, false);
    }

    @Override // com.google.android.gms.internal.zzey
    public final void zzb(String str, String str2) {
        this.zzsj.zzb(str, str2);
    }

    @Override // com.google.android.gms.internal.zzey
    public final boolean zzb(IObjectWrapper iObjectWrapper) {
        return this.zzsj.zzc((Uri) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper));
    }

    @Override // com.google.android.gms.internal.zzey
    public final boolean zzb(String str, boolean z) {
        if (this.zzsk == null) {
            return false;
        }
        this.zzsk.zza(new AdvertisingIdClient.Info(str, z));
        this.zzsl = true;
        return true;
    }

    @Override // com.google.android.gms.internal.zzey
    public final String zzc(IObjectWrapper iObjectWrapper) {
        return zza(iObjectWrapper, (byte[]) null);
    }

    @Override // com.google.android.gms.internal.zzey
    public final void zzd(IObjectWrapper iObjectWrapper) {
        this.zzsj.zza((MotionEvent) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper));
    }

    @Override // com.google.android.gms.internal.zzey
    public final void zzk(String str) {
        this.zzsj.zzk(str);
    }
}
