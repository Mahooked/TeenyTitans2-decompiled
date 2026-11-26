package com.google.android.gms.internal;

import android.content.Context;
import android.net.Uri;
import android.os.RemoteException;
import android.view.MotionEvent;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzeu {
    private final zzey zzsh;

    public zzeu(String str, Context context, boolean z) {
        this.zzsh = zzex.zzb(str, context, false);
    }

    public final void zza(MotionEvent motionEvent) throws RemoteException {
        this.zzsh.zzd(com.google.android.gms.dynamic.zzn.zzw(motionEvent));
    }

    public final Uri zzb(Uri uri, Context context) throws zzev, RemoteException {
        IObjectWrapper iObjectWrapperZza = this.zzsh.zza(com.google.android.gms.dynamic.zzn.zzw(uri), com.google.android.gms.dynamic.zzn.zzw(context));
        if (iObjectWrapperZza == null) {
            throw new zzev();
        }
        return (Uri) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapperZza);
    }

    public final Uri zzc(Uri uri, Context context) throws zzev, RemoteException {
        IObjectWrapper iObjectWrapperZzb = this.zzsh.zzb(com.google.android.gms.dynamic.zzn.zzw(uri), com.google.android.gms.dynamic.zzn.zzw(context));
        if (iObjectWrapperZzb == null) {
            throw new zzev();
        }
        return (Uri) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapperZzb);
    }
}
