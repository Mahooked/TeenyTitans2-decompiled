package com.google.android.gms.ads.internal;

import android.os.RemoteException;
import android.view.MotionEvent;
import android.view.View;
import com.google.android.gms.internal.zzafr;

/* JADX INFO: loaded from: classes.dex */
final class zzbo implements View.OnTouchListener {
    private /* synthetic */ zzbm zzvf;

    zzbo(zzbm zzbmVar) {
        this.zzvf = zzbmVar;
    }

    @Override // android.view.View.OnTouchListener
    public final boolean onTouch(View view, MotionEvent motionEvent) {
        if (this.zzvf.zzvd == null) {
            return false;
        }
        try {
            this.zzvf.zzvd.zza(motionEvent);
            return false;
        } catch (RemoteException e) {
            zzafr.zzc("Unable to process ad data", e);
            return false;
        }
    }
}
