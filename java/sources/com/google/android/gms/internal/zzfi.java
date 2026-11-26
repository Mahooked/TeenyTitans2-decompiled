package com.google.android.gms.internal;

import android.app.KeyguardManager;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.IntentFilter;
import android.graphics.Rect;
import android.os.Build;
import android.os.Handler;
import android.os.PowerManager;
import android.provider.Settings;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import android.util.DisplayMetrics;
import android.view.Display;
import android.view.View;
import android.view.ViewTreeObserver;
import android.view.WindowManager;
import java.lang.ref.WeakReference;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Map;
import java.util.UUID;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzfi implements ViewTreeObserver.OnGlobalLayoutListener, ViewTreeObserver.OnScrollChangedListener {
    private final Context mApplicationContext;
    private final WeakReference<zzaff> zzwN;
    private final zzgs zzwP;
    protected final zzfg zzwQ;
    private final WindowManager zzwR;
    private final PowerManager zzwS;
    private final KeyguardManager zzwT;
    private final DisplayMetrics zzwU;

    @Nullable
    private zzfp zzwV;
    private boolean zzwW;
    private boolean zzwZ;

    @Nullable
    private BroadcastReceiver zzxb;
    private float zzxg;
    private Object mLock = new Object();
    private boolean zzuV = false;
    private boolean zzwX = false;
    private final HashSet<Object> zzxc = new HashSet<>();
    private final HashSet<zzgd> zzxd = new HashSet<>();
    private final Rect zzxe = new Rect();
    private WeakReference<ViewTreeObserver> zzwO = new WeakReference<>(null);
    private boolean zzwY = true;
    private boolean zzxa = false;
    private zzair zzwx = new zzair(200);
    private final zzfl zzxf = new zzfl(this, new Handler());

    public zzfi(Context context, zziv zzivVar, zzaff zzaffVar, zzaje zzajeVar, zzgs zzgsVar) {
        this.zzwN = new WeakReference<>(zzaffVar);
        this.zzwP = zzgsVar;
        this.zzwQ = new zzfg(UUID.randomUUID().toString(), zzajeVar, zzivVar.zzAs, zzaffVar.zzXL, zzaffVar.zzcn(), zzivVar.zzAv);
        this.zzwR = (WindowManager) context.getSystemService("window");
        this.zzwS = (PowerManager) context.getApplicationContext().getSystemService("power");
        this.zzwT = (KeyguardManager) context.getSystemService("keyguard");
        this.mApplicationContext = context;
        this.mApplicationContext.getContentResolver().registerContentObserver(Settings.System.CONTENT_URI, true, this.zzxf);
        this.zzwU = context.getResources().getDisplayMetrics();
        Display defaultDisplay = this.zzwR.getDefaultDisplay();
        this.zzxe.right = defaultDisplay.getWidth();
        this.zzxe.bottom = defaultDisplay.getHeight();
        zzcp();
    }

    private final boolean isScreenOn() {
        return Build.VERSION.SDK_INT >= 20 ? this.zzwS.isInteractive() : this.zzwS.isScreenOn();
    }

    private static int zza(int i, DisplayMetrics displayMetrics) {
        return (int) (i / displayMetrics.density);
    }

    private final JSONObject zza(@Nullable View view, @Nullable Boolean bool) throws JSONException {
        if (view == null) {
            return zzcu().put("isAttachedToWindow", false).put("isScreenOn", isScreenOn()).put("isVisible", false);
        }
        boolean zIsAttachedToWindow = com.google.android.gms.ads.internal.zzbs.zzbB().isAttachedToWindow(view);
        int[] iArr = new int[2];
        int[] iArr2 = new int[2];
        try {
            view.getLocationOnScreen(iArr);
            view.getLocationInWindow(iArr2);
        } catch (Exception e) {
            zzafr.zzb("Failure getting view location.", e);
        }
        Rect rect = new Rect();
        rect.left = iArr[0];
        rect.top = iArr[1];
        rect.right = rect.left + view.getWidth();
        rect.bottom = rect.top + view.getHeight();
        Rect rect2 = new Rect();
        boolean globalVisibleRect = view.getGlobalVisibleRect(rect2, null);
        Rect rect3 = new Rect();
        boolean localVisibleRect = view.getLocalVisibleRect(rect3);
        Rect rect4 = new Rect();
        view.getHitRect(rect4);
        JSONObject jSONObjectZzcu = zzcu();
        jSONObjectZzcu.put("windowVisibility", view.getWindowVisibility()).put("isAttachedToWindow", zIsAttachedToWindow).put("viewBox", new JSONObject().put("top", zza(this.zzxe.top, this.zzwU)).put("bottom", zza(this.zzxe.bottom, this.zzwU)).put("left", zza(this.zzxe.left, this.zzwU)).put("right", zza(this.zzxe.right, this.zzwU))).put("adBox", new JSONObject().put("top", zza(rect.top, this.zzwU)).put("bottom", zza(rect.bottom, this.zzwU)).put("left", zza(rect.left, this.zzwU)).put("right", zza(rect.right, this.zzwU))).put("globalVisibleBox", new JSONObject().put("top", zza(rect2.top, this.zzwU)).put("bottom", zza(rect2.bottom, this.zzwU)).put("left", zza(rect2.left, this.zzwU)).put("right", zza(rect2.right, this.zzwU))).put("globalVisibleBoxVisible", globalVisibleRect).put("localVisibleBox", new JSONObject().put("top", zza(rect3.top, this.zzwU)).put("bottom", zza(rect3.bottom, this.zzwU)).put("left", zza(rect3.left, this.zzwU)).put("right", zza(rect3.right, this.zzwU))).put("localVisibleBoxVisible", localVisibleRect).put("hitBox", new JSONObject().put("top", zza(rect4.top, this.zzwU)).put("bottom", zza(rect4.bottom, this.zzwU)).put("left", zza(rect4.left, this.zzwU)).put("right", zza(rect4.right, this.zzwU))).put("screenDensity", this.zzwU.density);
        if (bool == null) {
            bool = Boolean.valueOf(com.google.android.gms.ads.internal.zzbs.zzbz().zza(view, this.zzwS, this.zzwT));
        }
        jSONObjectZzcu.put("isVisible", bool.booleanValue());
        return jSONObjectZzcu;
    }

    private static JSONObject zza(JSONObject jSONObject) throws JSONException {
        JSONArray jSONArray = new JSONArray();
        JSONObject jSONObject2 = new JSONObject();
        jSONArray.put(jSONObject);
        jSONObject2.put("units", jSONArray);
        return jSONObject2;
    }

    private final void zza(JSONObject jSONObject, boolean z) {
        try {
            JSONObject jSONObjectZza = zza(jSONObject);
            ArrayList arrayList = new ArrayList(this.zzxd);
            int size = arrayList.size();
            int i = 0;
            while (i < size) {
                Object obj = arrayList.get(i);
                i++;
                ((zzgd) obj).zzb(jSONObjectZza, z);
            }
        } catch (Throwable th) {
            zzafr.zzb("Skipping active view message.", th);
        }
    }

    private final void zzcr() {
        if (this.zzwV != null) {
            this.zzwV.zza(this);
        }
    }

    private final void zzct() {
        ViewTreeObserver viewTreeObserver = this.zzwO.get();
        if (viewTreeObserver == null || !viewTreeObserver.isAlive()) {
            return;
        }
        viewTreeObserver.removeOnScrollChangedListener(this);
        viewTreeObserver.removeGlobalOnLayoutListener(this);
    }

    private final JSONObject zzcu() throws JSONException {
        JSONObject jSONObject = new JSONObject();
        JSONObject jSONObjectPut = jSONObject.put("afmaVersion", this.zzwQ.zzck()).put("activeViewJSON", this.zzwQ.zzcl()).put("timestamp", com.google.android.gms.ads.internal.zzbs.zzbF().elapsedRealtime()).put("adFormat", this.zzwQ.zzcj()).put("hashCode", this.zzwQ.zzcm()).put("isMraid", this.zzwQ.zzcn()).put("isStopped", this.zzwX).put("isPaused", this.zzuV).put("isNative", this.zzwQ.zzco()).put("isScreenOn", isScreenOn());
        com.google.android.gms.ads.internal.zzbs.zzbz();
        JSONObject jSONObjectPut2 = jSONObjectPut.put("appMuted", zzagz.zzbh());
        com.google.android.gms.ads.internal.zzbs.zzbz();
        jSONObjectPut2.put("appVolume", zzagz.zzbf()).put("deviceVolume", this.zzxg);
        return jSONObject;
    }

    @Override // android.view.ViewTreeObserver.OnGlobalLayoutListener
    public final void onGlobalLayout() {
        zzg(2);
    }

    @Override // android.view.ViewTreeObserver.OnScrollChangedListener
    public final void onScrollChanged() {
        zzg(1);
    }

    public final void pause() {
        synchronized (this.mLock) {
            this.zzuV = true;
            zzg(3);
        }
    }

    public final void resume() {
        synchronized (this.mLock) {
            this.zzuV = false;
            zzg(3);
        }
    }

    public final void stop() {
        synchronized (this.mLock) {
            this.zzwX = true;
            zzg(3);
        }
    }

    public final void zza(zzfp zzfpVar) {
        synchronized (this.mLock) {
            this.zzwV = zzfpVar;
        }
    }

    public final void zza(zzgd zzgdVar) {
        if (this.zzxd.isEmpty()) {
            synchronized (this.mLock) {
                if (this.zzxb == null) {
                    IntentFilter intentFilter = new IntentFilter();
                    intentFilter.addAction("android.intent.action.SCREEN_ON");
                    intentFilter.addAction("android.intent.action.SCREEN_OFF");
                    this.zzxb = new zzfj(this);
                    this.mApplicationContext.registerReceiver(this.zzxb, intentFilter);
                }
            }
            zzg(3);
        }
        this.zzxd.add(zzgdVar);
        try {
            zzgdVar.zzb(zza(zza(this.zzwP.zzcv(), (Boolean) null)), false);
        } catch (JSONException e) {
            zzafr.zzb("Skipping measurement update for new client.", e);
        }
    }

    final void zza(zzgd zzgdVar, Map<String, String> map) {
        String strValueOf = String.valueOf(this.zzwQ.zzcm());
        zzafr.zzaC(strValueOf.length() != 0 ? "Received request to untrack: ".concat(strValueOf) : new String("Received request to untrack: "));
        zzb(zzgdVar);
    }

    final void zzb(zzaka zzakaVar, Map<String, String> map) {
        zzg(3);
    }

    public final void zzb(zzgd zzgdVar) {
        this.zzxd.remove(zzgdVar);
        zzgdVar.zzcz();
        if (this.zzxd.isEmpty()) {
            synchronized (this.mLock) {
                zzct();
                synchronized (this.mLock) {
                    if (this.zzxb != null) {
                        try {
                            try {
                                this.mApplicationContext.unregisterReceiver(this.zzxb);
                            } catch (IllegalStateException e) {
                                zzafr.zzb("Failed trying to unregister the receiver", e);
                            }
                        } catch (Exception e2) {
                            com.google.android.gms.ads.internal.zzbs.zzbD().zza(e2, "ActiveViewUnit.stopScreenStatusMonitoring");
                        }
                        this.zzxb = null;
                    }
                }
                this.mApplicationContext.getContentResolver().unregisterContentObserver(this.zzxf);
                this.zzwY = false;
                zzcr();
                ArrayList arrayList = new ArrayList(this.zzxd);
                int size = arrayList.size();
                int i = 0;
                while (i < size) {
                    Object obj = arrayList.get(i);
                    i++;
                    zzb((zzgd) obj);
                }
            }
        }
    }

    final boolean zzb(@Nullable Map<String, String> map) {
        if (map == null) {
            return false;
        }
        String str = map.get("hashCode");
        return !TextUtils.isEmpty(str) && str.equals(this.zzwQ.zzcm());
    }

    final void zzc(Map<String, String> map) {
        if (map.containsKey("isVisible")) {
            if (!"1".equals(map.get("isVisible"))) {
                "true".equals(map.get("isVisible"));
            }
            Iterator<Object> it = this.zzxc.iterator();
            while (it.hasNext()) {
                it.next();
            }
        }
    }

    public final void zzcp() {
        com.google.android.gms.ads.internal.zzbs.zzbz();
        this.zzxg = zzagz.zzM(this.mApplicationContext);
    }

    public final void zzcq() {
        synchronized (this.mLock) {
            if (this.zzwY) {
                this.zzwZ = true;
                try {
                    JSONObject jSONObjectZzcu = zzcu();
                    jSONObjectZzcu.put("doneReasonCode", "u");
                    zza(jSONObjectZzcu, true);
                } catch (RuntimeException e) {
                    zzafr.zzb("Failure while processing active view data.", e);
                } catch (JSONException e2) {
                    zzafr.zzb("JSON failure while processing active view data.", e2);
                }
                String strValueOf = String.valueOf(this.zzwQ.zzcm());
                zzafr.zzaC(strValueOf.length() != 0 ? "Untracking ad unit: ".concat(strValueOf) : new String("Untracking ad unit: "));
            }
        }
    }

    public final boolean zzcs() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzwY;
        }
        return z;
    }

    protected final void zzg(int i) {
        boolean z;
        ViewTreeObserver viewTreeObserver;
        ViewTreeObserver viewTreeObserver2;
        synchronized (this.mLock) {
            Iterator<zzgd> it = this.zzxd.iterator();
            while (true) {
                if (!it.hasNext()) {
                    z = false;
                    break;
                } else if (it.next().zzcy()) {
                    z = true;
                    break;
                }
            }
            if (z && this.zzwY) {
                View viewZzcv = this.zzwP.zzcv();
                boolean z2 = viewZzcv != null && com.google.android.gms.ads.internal.zzbs.zzbz().zza(viewZzcv, this.zzwS, this.zzwT);
                boolean z3 = viewZzcv != null && z2 && viewZzcv.getGlobalVisibleRect(new Rect(), null);
                if (this.zzwP.zzcw()) {
                    zzcq();
                    return;
                }
                if (i == 1 && !this.zzwx.tryAcquire() && z3 == this.zzxa) {
                    return;
                }
                if (z3 || this.zzxa || i != 1) {
                    try {
                        zza(zza(viewZzcv, Boolean.valueOf(z2)), false);
                        this.zzxa = z3;
                    } catch (RuntimeException | JSONException e) {
                        zzafr.zza("Active view update failed.", e);
                    }
                    View viewZzcv2 = this.zzwP.zzcx().zzcv();
                    if (viewZzcv2 != null && (viewTreeObserver2 = viewZzcv2.getViewTreeObserver()) != (viewTreeObserver = this.zzwO.get())) {
                        zzct();
                        if (!this.zzwW || (viewTreeObserver != null && viewTreeObserver.isAlive())) {
                            this.zzwW = true;
                            viewTreeObserver2.addOnScrollChangedListener(this);
                            viewTreeObserver2.addOnGlobalLayoutListener(this);
                        }
                        this.zzwO = new WeakReference<>(viewTreeObserver2);
                    }
                    zzcr();
                }
            }
        }
    }
}
