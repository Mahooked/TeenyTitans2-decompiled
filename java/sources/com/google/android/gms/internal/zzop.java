package com.google.android.gms.internal;

import android.graphics.Point;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewTreeObserver;
import android.widget.FrameLayout;
import com.google.android.gms.ads.formats.AdChoicesView;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.lang.ref.WeakReference;
import java.util.HashMap;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzop extends zzox implements View.OnClickListener, View.OnTouchListener, ViewTreeObserver.OnGlobalLayoutListener, ViewTreeObserver.OnScrollChangedListener {
    private static String[] zzIs = {"2011", "1009"};

    @Nullable
    private zzny zzHM;
    private final FrameLayout zzIt;

    @Nullable
    private View zzIv;

    @Nullable
    FrameLayout zzss;
    private final Object mLock = new Object();
    private Map<String, WeakReference<View>> zzIu = new HashMap();
    private boolean zzIw = false;
    private Point zzIx = new Point();
    private Point zzIy = new Point();
    private WeakReference<zzge> zzIz = new WeakReference<>(null);

    public zzop(FrameLayout frameLayout, FrameLayout frameLayout2) {
        this.zzIt = frameLayout;
        this.zzss = frameLayout2;
        com.google.android.gms.ads.internal.zzbs.zzbX();
        zzajv.zza((View) this.zzIt, (ViewTreeObserver.OnGlobalLayoutListener) this);
        com.google.android.gms.ads.internal.zzbs.zzbX();
        zzajv.zza((View) this.zzIt, (ViewTreeObserver.OnScrollChangedListener) this);
        this.zzIt.setOnTouchListener(this);
        this.zzIt.setOnClickListener(this);
        zzmo.initialize(this.zzIt.getContext());
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zza(zzoc zzocVar) {
        View view;
        synchronized (this.mLock) {
            zzocVar.zzd(this.zzIu);
            if (this.zzIu == null) {
                view = null;
                break;
            }
            String[] strArr = zzIs;
            int length = strArr.length;
            int i = 0;
            while (true) {
                if (i >= length) {
                    view = null;
                    break;
                }
                WeakReference<View> weakReference = this.zzIu.get(strArr[i]);
                if (weakReference != null) {
                    view = weakReference.get();
                    break;
                }
                i++;
            }
            if (view instanceof FrameLayout) {
                zzocVar.zza(view, new zzor(this, view));
            } else {
                zzocVar.zzev();
            }
        }
    }

    private final void zzg(@Nullable View view) {
        if (this.zzHM != null) {
            zzny zznyVarZzer = this.zzHM instanceof zznx ? ((zznx) this.zzHM).zzer() : this.zzHM;
            if (zznyVarZzer != null) {
                zznyVarZzer.zzg(view);
            }
        }
    }

    private final int zzm(int i) {
        zzji.zzds();
        return zzaiy.zzd(this.zzHM.getContext(), i);
    }

    @Override // com.google.android.gms.internal.zzow
    public final void destroy() {
        synchronized (this.mLock) {
            if (this.zzss != null) {
                this.zzss.removeAllViews();
            }
            this.zzss = null;
            this.zzIu = null;
            this.zzIv = null;
            this.zzHM = null;
            this.zzIx = null;
            this.zzIy = null;
            this.zzIz = null;
        }
    }

    @Override // android.view.View.OnClickListener
    public final void onClick(View view) {
        synchronized (this.mLock) {
            if (this.zzHM == null) {
                return;
            }
            Bundle bundle = new Bundle();
            bundle.putFloat("x", zzm(this.zzIx.x));
            bundle.putFloat("y", zzm(this.zzIx.y));
            bundle.putFloat("start_x", zzm(this.zzIy.x));
            bundle.putFloat("start_y", zzm(this.zzIy.y));
            if (this.zzIv == null || !this.zzIv.equals(view)) {
                this.zzHM.zza(view, this.zzIu, bundle, this.zzIt);
            } else if (!(this.zzHM instanceof zznx)) {
                this.zzHM.zza(view, "1007", bundle, this.zzIu, this.zzIt);
            } else if (((zznx) this.zzHM).zzer() != null) {
                ((zznx) this.zzHM).zzer().zza(view, "1007", bundle, this.zzIu, this.zzIt);
            }
        }
    }

    @Override // android.view.ViewTreeObserver.OnGlobalLayoutListener
    public final void onGlobalLayout() {
        synchronized (this.mLock) {
            if (this.zzIw) {
                int measuredWidth = this.zzIt.getMeasuredWidth();
                int measuredHeight = this.zzIt.getMeasuredHeight();
                if (measuredWidth != 0 && measuredHeight != 0 && this.zzss != null) {
                    this.zzss.setLayoutParams(new FrameLayout.LayoutParams(measuredWidth, measuredHeight));
                    this.zzIw = false;
                }
            }
            if (this.zzHM != null) {
                this.zzHM.zzc(this.zzIt, this.zzIu);
            }
        }
    }

    @Override // android.view.ViewTreeObserver.OnScrollChangedListener
    public final void onScrollChanged() {
        synchronized (this.mLock) {
            if (this.zzHM != null) {
                this.zzHM.zzc(this.zzIt, this.zzIu);
            }
        }
    }

    @Override // android.view.View.OnTouchListener
    public final boolean onTouch(View view, MotionEvent motionEvent) {
        synchronized (this.mLock) {
            if (this.zzHM != null) {
                int[] iArr = new int[2];
                this.zzIt.getLocationOnScreen(iArr);
                Point point = new Point((int) (motionEvent.getRawX() - iArr[0]), (int) (motionEvent.getRawY() - iArr[1]));
                this.zzIx = point;
                if (motionEvent.getAction() == 0) {
                    this.zzIy = point;
                }
                MotionEvent motionEventObtain = MotionEvent.obtain(motionEvent);
                motionEventObtain.setLocation(point.x, point.y);
                this.zzHM.zzd(motionEventObtain);
                motionEventObtain.recycle();
            }
        }
        return false;
    }

    @Override // com.google.android.gms.internal.zzow
    public final IObjectWrapper zzL(String str) {
        synchronized (this.mLock) {
            if (this.zzIu == null) {
                return null;
            }
            WeakReference<View> weakReference = this.zzIu.get(str);
            return com.google.android.gms.dynamic.zzn.zzw(weakReference != null ? weakReference.get() : null);
        }
    }

    @Override // com.google.android.gms.internal.zzow
    public final void zzb(IObjectWrapper iObjectWrapper, int i) {
        zzge zzgeVar;
        if (!com.google.android.gms.ads.internal.zzbs.zzbY().zzr(this.zzIt.getContext()) || this.zzIz == null || (zzgeVar = this.zzIz.get()) == null) {
            return;
        }
        zzgeVar.zzcB();
    }

    @Override // com.google.android.gms.internal.zzow
    public final void zzd(String str, IObjectWrapper iObjectWrapper) {
        View view = (View) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper);
        synchronized (this.mLock) {
            if (this.zzIu == null) {
                return;
            }
            if (view == null) {
                this.zzIu.remove(str);
            } else {
                this.zzIu.put(str, new WeakReference<>(view));
                if ("1098".equals(str)) {
                    return;
                }
                view.setOnTouchListener(this);
                view.setClickable(true);
                view.setOnClickListener(this);
            }
        }
    }

    @Override // com.google.android.gms.internal.zzow
    public final void zze(IObjectWrapper iObjectWrapper) {
        zzoc zzocVar;
        zzoc zzocVar2;
        ViewGroup viewGroup = null;
        synchronized (this.mLock) {
            zzg(null);
            Object objZzE = com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper);
            if (!(objZzE instanceof zzoc)) {
                zzafr.zzaT("Not an instance of native engine. This is most likely a transient error");
                return;
            }
            if (this.zzss != null) {
                this.zzss.setLayoutParams(new FrameLayout.LayoutParams(0, 0));
                this.zzIt.requestLayout();
            }
            this.zzIw = true;
            zzoc zzocVar3 = (zzoc) objZzE;
            if (this.zzHM != null) {
                if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFw)).booleanValue()) {
                    this.zzHM.zzb(this.zzIt, this.zzIu);
                }
            }
            if ((this.zzHM instanceof zzoc) && (zzocVar2 = (zzoc) this.zzHM) != null && zzocVar2.getContext() != null && com.google.android.gms.ads.internal.zzbs.zzbY().zzr(this.zzIt.getContext())) {
                zzaev zzaevVarZzew = zzocVar2.zzew();
                if (zzaevVarZzew != null) {
                    zzaevVarZzew.zzu(false);
                }
                zzge zzgeVar = this.zzIz.get();
                if (zzgeVar != null && zzaevVarZzew != null) {
                    zzgeVar.zzb(zzaevVarZzew);
                }
            }
            if ((this.zzHM instanceof zznx) && ((zznx) this.zzHM).zzeq()) {
                ((zznx) this.zzHM).zzc(zzocVar3);
            } else {
                this.zzHM = zzocVar3;
                if (zzocVar3 instanceof zznx) {
                    ((zznx) zzocVar3).zzc((zzny) null);
                }
            }
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFw)).booleanValue()) {
                this.zzss.setClickable(false);
            }
            this.zzss.removeAllViews();
            boolean zZzep = zzocVar3.zzep();
            if (zZzep && this.zzIu != null) {
                WeakReference<View> weakReference = this.zzIu.get("1098");
                View view = weakReference != null ? weakReference.get() : null;
                if (view instanceof ViewGroup) {
                    viewGroup = (ViewGroup) view;
                }
            }
            boolean z = zZzep && viewGroup != null;
            this.zzIv = zzocVar3.zza(this, z);
            if (this.zzIv != null) {
                if (this.zzIu != null) {
                    this.zzIu.put("1007", new WeakReference<>(this.zzIv));
                }
                if (z) {
                    viewGroup.removeAllViews();
                    viewGroup.addView(this.zzIv);
                } else {
                    AdChoicesView adChoicesView = new AdChoicesView(zzocVar3.getContext());
                    adChoicesView.setLayoutParams(new FrameLayout.LayoutParams(-1, -1));
                    adChoicesView.addView(this.zzIv);
                    if (this.zzss != null) {
                        this.zzss.addView(adChoicesView);
                    }
                }
            }
            zzocVar3.zza(this.zzIt, this.zzIu, this, this);
            zzagz.zzZr.post(new zzoq(this, zzocVar3));
            zzg(this.zzIt);
            if ((this.zzHM instanceof zzoc) && (zzocVar = (zzoc) this.zzHM) != null && zzocVar.getContext() != null && com.google.android.gms.ads.internal.zzbs.zzbY().zzr(this.zzIt.getContext())) {
                zzge zzgeVar2 = this.zzIz.get();
                if (zzgeVar2 == null) {
                    zzgeVar2 = new zzge(this.zzIt.getContext(), this.zzIt);
                    this.zzIz = new WeakReference<>(zzgeVar2);
                }
                zzgeVar2.zza(zzocVar.zzew());
            }
        }
    }
}
