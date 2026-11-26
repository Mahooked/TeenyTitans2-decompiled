package com.google.android.gms.internal;

import android.content.Context;
import android.view.ViewTreeObserver;
import java.lang.ref.WeakReference;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzyx {
    private final Context mContext;
    private final zzcu zzIc;
    private final zzafg zzQQ;
    private ViewTreeObserver.OnGlobalLayoutListener zzRX;
    private ViewTreeObserver.OnScrollChangedListener zzRY;
    private final com.google.android.gms.ads.internal.zzbb zzRm;
    private final zznb zzsK;
    private final Object mLock = new Object();
    private int zzwv = -1;
    private int zzww = -1;
    private zzair zzwx = new zzair(200);

    public zzyx(Context context, zzcu zzcuVar, zzafg zzafgVar, zznb zznbVar, com.google.android.gms.ads.internal.zzbb zzbbVar) {
        this.mContext = context;
        this.zzIc = zzcuVar;
        this.zzQQ = zzafgVar;
        this.zzsK = zznbVar;
        this.zzRm = zzbbVar;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final ViewTreeObserver.OnGlobalLayoutListener zza(WeakReference<zzaka> weakReference) {
        if (this.zzRX == null) {
            this.zzRX = new zzzd(this, weakReference);
        }
        return this.zzRX;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zza(WeakReference<zzaka> weakReference, boolean z) {
        zzaka zzakaVar;
        if (weakReference == null || (zzakaVar = weakReference.get()) == null || zzakaVar.getView() == null) {
            return;
        }
        if (!z || this.zzwx.tryAcquire()) {
            int[] iArr = new int[2];
            zzakaVar.getView().getLocationOnScreen(iArr);
            zzji.zzds();
            int iZzd = zzaiy.zzd(this.mContext, iArr[0]);
            zzji.zzds();
            int iZzd2 = zzaiy.zzd(this.mContext, iArr[1]);
            synchronized (this.mLock) {
                if (this.zzwv != iZzd || this.zzww != iZzd2) {
                    this.zzwv = iZzd;
                    this.zzww = iZzd2;
                    zzakaVar.zziw().zza(this.zzwv, this.zzww, !z);
                }
            }
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final ViewTreeObserver.OnScrollChangedListener zzb(WeakReference<zzaka> weakReference) {
        if (this.zzRY == null) {
            this.zzRY = new zzze(this, weakReference);
        }
        return this.zzRY;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzi(zzaka zzakaVar) {
        zzakb zzakbVarZziw = zzakaVar.zziw();
        zzakbVarZziw.zza("/video", zzqn.zzJg);
        zzakbVarZziw.zza("/videoMeta", zzqn.zzJh);
        zzakbVarZziw.zza("/precache", zzqn.zzJj);
        zzakbVarZziw.zza("/delayPageLoaded", zzqn.zzJm);
        zzakbVarZziw.zza("/instrument", zzqn.zzJk);
        zzakbVarZziw.zza("/log", zzqn.zzJb);
        zzakbVarZziw.zza("/videoClicked", zzqn.zzJc);
        zzakbVarZziw.zza("/trackActiveViewUnit", new zzzb(this));
        zzakbVarZziw.zza("/untrackActiveViewUnit", new zzzc(this));
    }

    final zzaka zzgz() throws zzakm {
        return com.google.android.gms.ads.internal.zzbs.zzbA().zza(this.mContext, zziv.zzg(this.mContext), false, false, this.zzIc, this.zzQQ.zzUj.zzvT, this.zzsK, null, this.zzRm.zzak(), this.zzQQ.zzXX);
    }
}
