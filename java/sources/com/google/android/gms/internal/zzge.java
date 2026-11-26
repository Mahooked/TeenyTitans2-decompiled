package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.app.Activity;
import android.app.Application;
import android.app.KeyguardManager;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.IntentFilter;
import android.graphics.Rect;
import android.os.Bundle;
import android.os.PowerManager;
import android.support.annotation.Nullable;
import android.util.DisplayMetrics;
import android.view.View;
import android.view.ViewTreeObserver;
import android.view.Window;
import android.view.WindowManager;
import java.lang.ref.WeakReference;
import java.util.HashSet;
import java.util.Iterator;

/* JADX INFO: loaded from: classes.dex */
@zzzn
@TargetApi(14)
public final class zzge implements Application.ActivityLifecycleCallbacks, View.OnAttachStateChangeListener, ViewTreeObserver.OnGlobalLayoutListener, ViewTreeObserver.OnScrollChangedListener {
    private static final long zzxy = ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEr)).longValue();
    private final Context mApplicationContext;
    private final WindowManager zzwR;
    private final PowerManager zzwS;
    private final KeyguardManager zzwT;
    private WeakReference<ViewTreeObserver> zzxA;
    private WeakReference<View> zzxB;
    private zzgj zzxC;
    private DisplayMetrics zzxF;

    @Nullable
    private BroadcastReceiver zzxb;
    private Application zzxz;
    private zzair zzwx = new zzair(zzxy);
    private boolean zzxa = false;
    private int zzxD = -1;
    private HashSet<zzgi> zzxE = new HashSet<>();

    public zzge(Context context, View view) {
        this.mApplicationContext = context.getApplicationContext();
        this.zzwR = (WindowManager) context.getSystemService("window");
        this.zzwS = (PowerManager) this.mApplicationContext.getSystemService("power");
        this.zzwT = (KeyguardManager) context.getSystemService("keyguard");
        if (this.mApplicationContext instanceof Application) {
            this.zzxz = (Application) this.mApplicationContext;
            this.zzxC = new zzgj((Application) this.mApplicationContext, this);
        }
        this.zzxF = context.getResources().getDisplayMetrics();
        View view2 = this.zzxB != null ? this.zzxB.get() : null;
        if (view2 != null) {
            view2.removeOnAttachStateChangeListener(this);
            zze(view2);
        }
        this.zzxB = new WeakReference<>(view);
        if (view != null) {
            if (com.google.android.gms.ads.internal.zzbs.zzbB().isAttachedToWindow(view)) {
                zzd(view);
            }
            view.addOnAttachStateChangeListener(this);
        }
    }

    private final Rect zza(Rect rect) {
        return new Rect(zzi(rect.left), zzi(rect.top), zzi(rect.right), zzi(rect.bottom));
    }

    private final void zza(Activity activity, int i) {
        Window window;
        if (this.zzxB == null || (window = activity.getWindow()) == null) {
            return;
        }
        View viewPeekDecorView = window.peekDecorView();
        View view = this.zzxB.get();
        if (view == null || viewPeekDecorView == null || view.getRootView() != viewPeekDecorView.getRootView()) {
            return;
        }
        this.zzxD = i;
    }

    private final void zzcA() {
        com.google.android.gms.ads.internal.zzbs.zzbz();
        zzagz.zzZr.post(new zzgf(this));
    }

    private final void zzd(View view) {
        ViewTreeObserver viewTreeObserver = view.getViewTreeObserver();
        if (viewTreeObserver.isAlive()) {
            this.zzxA = new WeakReference<>(viewTreeObserver);
            viewTreeObserver.addOnScrollChangedListener(this);
            viewTreeObserver.addOnGlobalLayoutListener(this);
        }
        if (this.zzxb == null) {
            IntentFilter intentFilter = new IntentFilter();
            intentFilter.addAction("android.intent.action.SCREEN_ON");
            intentFilter.addAction("android.intent.action.SCREEN_OFF");
            intentFilter.addAction("android.intent.action.USER_PRESENT");
            this.zzxb = new zzgg(this);
            this.mApplicationContext.registerReceiver(this.zzxb, intentFilter);
        }
        if (this.zzxz != null) {
            try {
                this.zzxz.registerActivityLifecycleCallbacks(this.zzxC);
            } catch (Exception e) {
                zzafr.zzb("Error registering activity lifecycle callbacks.", e);
            }
        }
    }

    private final void zze(View view) {
        try {
            if (this.zzxA != null) {
                ViewTreeObserver viewTreeObserver = this.zzxA.get();
                if (viewTreeObserver != null && viewTreeObserver.isAlive()) {
                    viewTreeObserver.removeOnScrollChangedListener(this);
                    viewTreeObserver.removeGlobalOnLayoutListener(this);
                }
                this.zzxA = null;
            }
        } catch (Exception e) {
            zzafr.zzb("Error while unregistering listeners from the last ViewTreeObserver.", e);
        }
        try {
            ViewTreeObserver viewTreeObserver2 = view.getViewTreeObserver();
            if (viewTreeObserver2.isAlive()) {
                viewTreeObserver2.removeOnScrollChangedListener(this);
                viewTreeObserver2.removeGlobalOnLayoutListener(this);
            }
        } catch (Exception e2) {
            zzafr.zzb("Error while unregistering listeners from the ViewTreeObserver.", e2);
        }
        if (this.zzxb != null) {
            try {
                this.mApplicationContext.unregisterReceiver(this.zzxb);
            } catch (IllegalStateException e3) {
                zzafr.zzb("Failed trying to unregister the receiver", e3);
            } catch (Exception e4) {
                com.google.android.gms.ads.internal.zzbs.zzbD().zza(e4, "ActiveViewUnit.stopScreenStatusMonitoring");
            }
            this.zzxb = null;
        }
        if (this.zzxz != null) {
            try {
                this.zzxz.unregisterActivityLifecycleCallbacks(this.zzxC);
            } catch (Exception e5) {
                zzafr.zzb("Error registering activity lifecycle callbacks.", e5);
            }
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzh(int i) {
        if (this.zzxE.size() == 0 || this.zzxB == null) {
            return;
        }
        View view = this.zzxB.get();
        boolean z = i == 1;
        boolean z2 = view == null;
        Rect rect = new Rect();
        Rect rect2 = new Rect();
        boolean globalVisibleRect = false;
        Rect rect3 = new Rect();
        boolean localVisibleRect = false;
        Rect rect4 = new Rect();
        Rect rect5 = new Rect();
        rect5.right = this.zzwR.getDefaultDisplay().getWidth();
        rect5.bottom = this.zzwR.getDefaultDisplay().getHeight();
        int[] iArr = new int[2];
        int[] iArr2 = new int[2];
        if (view != null) {
            globalVisibleRect = view.getGlobalVisibleRect(rect2);
            localVisibleRect = view.getLocalVisibleRect(rect3);
            view.getHitRect(rect4);
            try {
                view.getLocationOnScreen(iArr);
                view.getLocationInWindow(iArr2);
            } catch (Exception e) {
                zzafr.zzb("Failure getting view location.", e);
            }
            rect.left = iArr[0];
            rect.top = iArr[1];
            rect.right = rect.left + view.getWidth();
            rect.bottom = rect.top + view.getHeight();
        }
        int windowVisibility = view != null ? view.getWindowVisibility() : 8;
        if (this.zzxD != -1) {
            windowVisibility = this.zzxD;
        }
        boolean z3 = !z2 && com.google.android.gms.ads.internal.zzbs.zzbz().zza(view, this.zzwS, this.zzwT) && globalVisibleRect && localVisibleRect && windowVisibility == 0;
        if (z && !this.zzwx.tryAcquire() && z3 == this.zzxa) {
            return;
        }
        if (z3 || this.zzxa || i != 1) {
            zzgh zzghVar = new zzgh(com.google.android.gms.ads.internal.zzbs.zzbF().elapsedRealtime(), this.zzwS.isScreenOn(), view != null ? com.google.android.gms.ads.internal.zzbs.zzbB().isAttachedToWindow(view) : false, view != null ? view.getWindowVisibility() : 8, zza(rect5), zza(rect), zza(rect2), globalVisibleRect, zza(rect3), localVisibleRect, zza(rect4), this.zzxF.density, z3);
            Iterator<zzgi> it = this.zzxE.iterator();
            while (it.hasNext()) {
                it.next().zza(zzghVar);
            }
            this.zzxa = z3;
        }
    }

    private final int zzi(int i) {
        return (int) (i / this.zzxF.density);
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivityCreated(Activity activity, Bundle bundle) {
        zza(activity, 0);
        zzh(3);
        zzcA();
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivityDestroyed(Activity activity) {
        zzh(3);
        zzcA();
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivityPaused(Activity activity) {
        zza(activity, 4);
        zzh(3);
        zzcA();
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivityResumed(Activity activity) {
        zza(activity, 0);
        zzh(3);
        zzcA();
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivitySaveInstanceState(Activity activity, Bundle bundle) {
        zzh(3);
        zzcA();
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivityStarted(Activity activity) {
        zza(activity, 0);
        zzh(3);
        zzcA();
    }

    @Override // android.app.Application.ActivityLifecycleCallbacks
    public final void onActivityStopped(Activity activity) {
        zzh(3);
        zzcA();
    }

    @Override // android.view.ViewTreeObserver.OnGlobalLayoutListener
    public final void onGlobalLayout() {
        zzh(2);
        zzcA();
    }

    @Override // android.view.ViewTreeObserver.OnScrollChangedListener
    public final void onScrollChanged() {
        zzh(1);
    }

    @Override // android.view.View.OnAttachStateChangeListener
    public final void onViewAttachedToWindow(View view) {
        this.zzxD = -1;
        zzd(view);
        zzh(3);
    }

    @Override // android.view.View.OnAttachStateChangeListener
    public final void onViewDetachedFromWindow(View view) {
        this.zzxD = -1;
        zzh(3);
        zzcA();
        zze(view);
    }

    public final void zza(zzgi zzgiVar) {
        this.zzxE.add(zzgiVar);
        zzh(3);
    }

    public final void zzb(zzgi zzgiVar) {
        this.zzxE.remove(zzgiVar);
    }

    public final void zzcB() {
        zzh(4);
    }
}
