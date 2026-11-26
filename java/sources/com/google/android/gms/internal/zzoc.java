package com.google.android.gms.internal;

import android.content.Context;
import android.graphics.Rect;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.os.IBinder;
import android.os.RemoteException;
import android.support.annotation.Nullable;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewParent;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;
import com.google.android.gms.common.internal.zzbo;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.lang.ref.WeakReference;
import java.util.Iterator;
import java.util.Map;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public class zzoc implements zzny {
    private final Context mContext;
    private final zznz zzHX;

    @Nullable
    private final JSONObject zzIa;

    @Nullable
    private final zzoa zzIb;
    private final zzcu zzIc;
    boolean zzId;

    @Nullable
    private String zzIe;

    @Nullable
    private final zzaje zztW;

    @Nullable
    private zzyh zzuP;

    @Nullable
    private zzaev zzuk;
    private final Object mLock = new Object();
    private WeakReference<View> zzIf = null;

    public zzoc(Context context, zznz zznzVar, @Nullable zzyh zzyhVar, zzcu zzcuVar, @Nullable JSONObject jSONObject, @Nullable zzoa zzoaVar, @Nullable zzaje zzajeVar, @Nullable String str) {
        this.mContext = context;
        this.zzHX = zznzVar;
        this.zzuP = zzyhVar;
        this.zzIc = zzcuVar;
        this.zzIa = jSONObject;
        this.zzIb = zzoaVar;
        this.zztW = zzajeVar;
        this.zzIe = str;
    }

    private final JSONObject zza(Map<String, WeakReference<View>> map, View view) {
        JSONObject jSONObject;
        JSONObject jSONObject2 = new JSONObject();
        if (map == null || view == null) {
            return jSONObject2;
        }
        int[] iArrZzh = zzh(view);
        for (Map.Entry<String, WeakReference<View>> entry : map.entrySet()) {
            View view2 = entry.getValue().get();
            if (view2 != null) {
                int[] iArrZzh2 = zzh(view2);
                JSONObject jSONObject3 = new JSONObject();
                JSONObject jSONObject4 = new JSONObject();
                try {
                    jSONObject4.put("width", zzm(view2.getMeasuredWidth()));
                    jSONObject4.put("height", zzm(view2.getMeasuredHeight()));
                    jSONObject4.put("x", zzm(iArrZzh2[0] - iArrZzh[0]));
                    jSONObject4.put("y", zzm(iArrZzh2[1] - iArrZzh[1]));
                    jSONObject4.put("relative_to", "ad_view");
                    jSONObject3.put("frame", jSONObject4);
                    Rect rect = new Rect();
                    if (view2.getLocalVisibleRect(rect)) {
                        jSONObject = zzb(rect);
                    } else {
                        jSONObject = new JSONObject();
                        jSONObject.put("width", 0);
                        jSONObject.put("height", 0);
                        jSONObject.put("x", zzm(iArrZzh2[0] - iArrZzh[0]));
                        jSONObject.put("y", zzm(iArrZzh2[1] - iArrZzh[1]));
                        jSONObject.put("relative_to", "ad_view");
                    }
                    jSONObject3.put("visible_bounds", jSONObject);
                    if (view2 instanceof TextView) {
                        TextView textView = (TextView) view2;
                        jSONObject3.put("text_color", textView.getCurrentTextColor());
                        jSONObject3.put("font_size", textView.getTextSize());
                        jSONObject3.put("text", textView.getText());
                    }
                    jSONObject2.put(entry.getKey(), jSONObject3);
                } catch (JSONException e) {
                    zzafr.zzaT("Unable to get asset views information");
                }
            }
        }
        return jSONObject2;
    }

    private final void zza(View view, JSONObject jSONObject, JSONObject jSONObject2, JSONObject jSONObject3, String str, JSONObject jSONObject4, JSONObject jSONObject5) {
        zzbo.zzcz("performClick must be called on the main UI thread.");
        try {
            JSONObject jSONObject6 = new JSONObject();
            jSONObject6.put("ad", this.zzIa);
            if (jSONObject2 != null) {
                jSONObject6.put("asset_view_signal", jSONObject2);
            }
            if (jSONObject != null) {
                jSONObject6.put("ad_view_signal", jSONObject);
            }
            if (jSONObject4 != null) {
                jSONObject6.put("click_signal", jSONObject4);
            }
            if (jSONObject3 != null) {
                jSONObject6.put("scroll_view_signal", jSONObject3);
            }
            JSONObject jSONObject7 = new JSONObject();
            jSONObject7.put("asset_id", str);
            jSONObject7.put("template", this.zzIb.zzej());
            jSONObject7.put("has_custom_click_handler", this.zzHX.zzs(this.zzIb.getCustomTemplateId()) != null);
            jSONObject6.put("has_custom_click_handler", this.zzHX.zzs(this.zzIb.getCustomTemplateId()) != null);
            try {
                JSONObject jSONObjectOptJSONObject = this.zzIa.optJSONObject("tracking_urls_and_actions");
                if (jSONObjectOptJSONObject == null) {
                    jSONObjectOptJSONObject = new JSONObject();
                }
                jSONObject7.put("click_signals", this.zzIc.zzB().zza(this.mContext, jSONObjectOptJSONObject.optString("click_string"), view));
            } catch (Exception e) {
                zzafr.zzb("Exception obtaining click signals", e);
            }
            jSONObject6.put("click", jSONObject7);
            if (jSONObject5 != null) {
                jSONObject6.put("provided_signals", jSONObject5);
            }
            jSONObject6.put("ads_id", this.zzIe);
            this.zzuP.zza(new zzod(this, jSONObject6));
        } catch (JSONException e2) {
            zzafr.zzb("Unable to create click JSON.", e2);
        }
    }

    private final boolean zza(JSONObject jSONObject, JSONObject jSONObject2, JSONObject jSONObject3, JSONObject jSONObject4) {
        zzbo.zzcz("recordImpression must be called on the main UI thread.");
        this.zzId = true;
        try {
            JSONObject jSONObject5 = new JSONObject();
            jSONObject5.put("ad", this.zzIa);
            jSONObject5.put("ads_id", this.zzIe);
            if (jSONObject2 != null) {
                jSONObject5.put("asset_view_signal", jSONObject2);
            }
            if (jSONObject != null) {
                jSONObject5.put("ad_view_signal", jSONObject);
            }
            if (jSONObject3 != null) {
                jSONObject5.put("scroll_view_signal", jSONObject3);
            }
            if (jSONObject4 != null) {
                jSONObject5.put("provided_signals", jSONObject4);
            }
            this.zzuP.zza(new zzoe(this, jSONObject5));
            this.zzuP.zza(new zzof(this.zzHX, this.zzIe));
            this.zzHX.zza(this);
            this.zzHX.zzaw();
            return true;
        } catch (JSONException e) {
            zzafr.zzb("Unable to create impression JSON.", e);
            return false;
        }
    }

    private final JSONObject zzb(Rect rect) throws JSONException {
        JSONObject jSONObject = new JSONObject();
        jSONObject.put("width", zzm(rect.right - rect.left));
        jSONObject.put("height", zzm(rect.bottom - rect.top));
        jSONObject.put("x", zzm(rect.left));
        jSONObject.put("y", zzm(rect.top));
        jSONObject.put("relative_to", "self");
        return jSONObject;
    }

    private static int[] zzh(View view) {
        int[] iArr = new int[2];
        view.getLocationOnScreen(iArr);
        return iArr;
    }

    private final JSONObject zzi(View view) {
        JSONObject jSONObject;
        JSONObject jSONObject2 = new JSONObject();
        if (view != null) {
            try {
                int[] iArrZzh = zzh(view);
                JSONObject jSONObject3 = new JSONObject();
                jSONObject3.put("width", zzm(view.getMeasuredWidth()));
                jSONObject3.put("height", zzm(view.getMeasuredHeight()));
                jSONObject3.put("x", zzm(iArrZzh[0]));
                jSONObject3.put("y", zzm(iArrZzh[1]));
                jSONObject3.put("relative_to", "window");
                jSONObject2.put("frame", jSONObject3);
                Rect rect = new Rect();
                if (view.getGlobalVisibleRect(rect)) {
                    jSONObject = zzb(rect);
                } else {
                    jSONObject = new JSONObject();
                    jSONObject.put("width", 0);
                    jSONObject.put("height", 0);
                    jSONObject.put("x", zzm(iArrZzh[0]));
                    jSONObject.put("y", zzm(iArrZzh[1]));
                    jSONObject.put("relative_to", "window");
                }
                jSONObject2.put("visible_bounds", jSONObject);
            } catch (Exception e) {
                zzafr.zzaT("Unable to get native ad view bounding box");
            }
        }
        return jSONObject2;
    }

    private static JSONObject zzj(View view) {
        JSONObject jSONObject = new JSONObject();
        if (view != null) {
            try {
                com.google.android.gms.ads.internal.zzbs.zzbz();
                jSONObject.put("contained_in_scroll_view", zzagz.zzp(view) != -1);
            } catch (Exception e) {
            }
        }
        return jSONObject;
    }

    private final int zzm(int i) {
        zzji.zzds();
        return zzaiy.zzd(this.mContext, i);
    }

    @Override // com.google.android.gms.internal.zzny
    public final Context getContext() {
        return this.mContext;
    }

    @Override // com.google.android.gms.internal.zzny
    @Nullable
    public View zza(View.OnClickListener onClickListener, boolean z) {
        zznn zznnVarZzek = this.zzIb.zzek();
        if (zznnVarZzek == null) {
            return null;
        }
        RelativeLayout.LayoutParams layoutParams = new RelativeLayout.LayoutParams(-2, -2);
        if (!z) {
            switch (zznnVarZzek.zzee()) {
                case 0:
                    layoutParams.addRule(10);
                    layoutParams.addRule(9);
                    break;
                case 1:
                default:
                    layoutParams.addRule(10);
                    layoutParams.addRule(11);
                    break;
                case 2:
                    layoutParams.addRule(12);
                    layoutParams.addRule(11);
                    break;
                case 3:
                    layoutParams.addRule(12);
                    layoutParams.addRule(9);
                    break;
            }
        }
        zzno zznoVar = new zzno(this.mContext, zznnVarZzek, layoutParams);
        zznoVar.setOnClickListener(onClickListener);
        zznoVar.setContentDescription((CharSequence) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFA));
        return zznoVar;
    }

    @Override // com.google.android.gms.internal.zzny
    public final void zza(View view, zznw zznwVar) {
        FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(-1, -1);
        View viewZzel = this.zzIb.zzel();
        if (viewZzel != null) {
            ViewParent parent = viewZzel.getParent();
            if (parent instanceof ViewGroup) {
                ((ViewGroup) parent).removeView(viewZzel);
            }
            ((FrameLayout) view).addView(viewZzel, layoutParams);
            this.zzHX.zza(zznwVar);
            return;
        }
        if (this.zzIb instanceof zzob) {
            zzob zzobVar = (zzob) this.zzIb;
            if (zzobVar.getImages() == null || zzobVar.getImages().size() <= 0) {
                return;
            }
            Object obj = zzobVar.getImages().get(0);
            zzos zzosVarZzi = obj instanceof IBinder ? zzot.zzi((IBinder) obj) : null;
            if (zzosVarZzi != null) {
                try {
                    IObjectWrapper iObjectWrapperZzeg = zzosVarZzi.zzeg();
                    if (iObjectWrapperZzeg != null) {
                        Drawable drawable = (Drawable) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapperZzeg);
                        ImageView imageView = new ImageView(this.mContext);
                        imageView.setImageDrawable(drawable);
                        imageView.setScaleType(ImageView.ScaleType.CENTER_INSIDE);
                        ((FrameLayout) view).addView(imageView, layoutParams);
                    }
                } catch (RemoteException e) {
                    zzafr.zzaT("Could not get drawable from image");
                }
            }
        }
    }

    @Override // com.google.android.gms.internal.zzny
    public final void zza(View view, String str, @Nullable Bundle bundle, Map<String, WeakReference<View>> map, View view2) {
        JSONObject jSONObject;
        JSONObject jSONObjectZza = zza(map, view2);
        JSONObject jSONObjectZzi = zzi(view2);
        JSONObject jSONObjectZzj = zzj(view2);
        try {
            JSONObject jSONObjectZza2 = com.google.android.gms.ads.internal.zzbs.zzbz().zza(bundle, (JSONObject) null);
            jSONObject = new JSONObject();
            try {
                jSONObject.put("click_point", jSONObjectZza2);
                jSONObject.put("asset_id", str);
            } catch (Exception e) {
                e = e;
                zzafr.zzb("Error occured while grabbing click signals.", e);
            }
        } catch (Exception e2) {
            e = e2;
            jSONObject = null;
        }
        zza(view, jSONObjectZzi, jSONObjectZza, jSONObjectZzj, str, jSONObject, null);
    }

    @Override // com.google.android.gms.internal.zzny
    public void zza(View view, Map<String, WeakReference<View>> map) {
        zza(zzi(view), zza(map, view), zzj(view), (JSONObject) null);
    }

    @Override // com.google.android.gms.internal.zzny
    public void zza(View view, Map<String, WeakReference<View>> map, Bundle bundle, View view2) {
        zzbo.zzcz("performClick must be called on the main UI thread.");
        if (map != null) {
            for (Map.Entry<String, WeakReference<View>> entry : map.entrySet()) {
                if (view.equals(entry.getValue().get())) {
                    zza(view, entry.getKey(), bundle, map, view2);
                    return;
                }
            }
        }
        if ("2".equals(this.zzIb.zzej())) {
            zza(view, "2099", bundle, map, view2);
        } else if ("1".equals(this.zzIb.zzej())) {
            zza(view, "1099", bundle, map, view2);
        }
    }

    public void zza(View view, Map<String, WeakReference<View>> map, View.OnTouchListener onTouchListener, View.OnClickListener onClickListener) {
        if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFy)).booleanValue()) {
            view.setOnTouchListener(onTouchListener);
            view.setClickable(true);
            view.setOnClickListener(onClickListener);
            if (map != null) {
                Iterator<Map.Entry<String, WeakReference<View>>> it = map.entrySet().iterator();
                while (it.hasNext()) {
                    View view2 = it.next().getValue().get();
                    if (view2 != null) {
                        view2.setOnTouchListener(onTouchListener);
                        view2.setClickable(true);
                        view2.setOnClickListener(onClickListener);
                    }
                }
            }
        }
    }

    @Override // com.google.android.gms.internal.zzny
    public void zzb(View view, Map<String, WeakReference<View>> map) {
        if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFx)).booleanValue()) {
            return;
        }
        view.setOnTouchListener(null);
        view.setClickable(false);
        view.setOnClickListener(null);
        if (map != null) {
            Iterator<Map.Entry<String, WeakReference<View>>> it = map.entrySet().iterator();
            while (it.hasNext()) {
                View view2 = it.next().getValue().get();
                if (view2 != null) {
                    view2.setOnTouchListener(null);
                    view2.setClickable(false);
                    view2.setOnClickListener(null);
                }
            }
        }
    }

    @Override // com.google.android.gms.internal.zzny
    public final void zzc(Bundle bundle) {
        if (bundle == null) {
            zzafr.zzaC("Click data is null. No click is reported.");
        } else {
            zza(null, null, null, null, bundle.getBundle("click_signal").getString("asset_id"), null, com.google.android.gms.ads.internal.zzbs.zzbz().zza(bundle, (JSONObject) null));
        }
    }

    @Override // com.google.android.gms.internal.zzny
    public final void zzc(View view, Map<String, WeakReference<View>> map) {
        synchronized (this.mLock) {
            if (this.zzId) {
                return;
            }
            if (view.isShown()) {
                if (view.getGlobalVisibleRect(new Rect(), null)) {
                    zza(view, map);
                }
            }
        }
    }

    @Override // com.google.android.gms.internal.zzny
    public final void zzd(MotionEvent motionEvent) {
        this.zzIc.zza(motionEvent);
    }

    public final void zzd(Map<String, WeakReference<View>> map) {
        if (this.zzIb.zzel() != null) {
            if ("2".equals(this.zzIb.zzej())) {
                com.google.android.gms.ads.internal.zzbs.zzbD().zzb(this.mContext, this.zzHX.getAdUnitId(), this.zzIb.zzej(), map.containsKey("2011"));
            } else if ("1".equals(this.zzIb.zzej())) {
                com.google.android.gms.ads.internal.zzbs.zzbD().zzb(this.mContext, this.zzHX.getAdUnitId(), this.zzIb.zzej(), map.containsKey("1009"));
            }
        }
    }

    @Override // com.google.android.gms.internal.zzny
    public final boolean zzd(Bundle bundle) {
        return zza((JSONObject) null, (JSONObject) null, (JSONObject) null, com.google.android.gms.ads.internal.zzbs.zzbz().zza(bundle, (JSONObject) null));
    }

    @Override // com.google.android.gms.internal.zzny
    public final void zze(Bundle bundle) {
        if (bundle == null) {
            zzafr.zzaC("Touch event data is null. No touch event is reported.");
            return;
        }
        this.zzIc.zzB().zza((int) bundle.getFloat("x"), (int) bundle.getFloat("y"), bundle.getInt("duration_ms"));
    }

    @Override // com.google.android.gms.internal.zzny
    public boolean zzep() {
        zznn zznnVarZzek = this.zzIb.zzek();
        return zznnVarZzek != null && zznnVarZzek.zzef();
    }

    public zzaka zzes() throws zzakm {
        if (this.zzIa == null || this.zzIa.optJSONObject("overlay") == null) {
            return null;
        }
        zzaka zzakaVarZza = com.google.android.gms.ads.internal.zzbs.zzbA().zza(this.mContext, zziv.zzg(this.mContext), false, false, this.zzIc, this.zztW, null, null, null, zzig.zzde());
        zzakaVarZza.getView().setVisibility(8);
        this.zzuP.zza(new zzom(new zzog(zzakaVarZza)));
        return zzakaVarZza;
    }

    @Override // com.google.android.gms.internal.zzny
    public void zzet() {
        this.zzuP.zzfd();
    }

    @Override // com.google.android.gms.internal.zzny
    public final View zzeu() {
        if (this.zzIf != null) {
            return this.zzIf.get();
        }
        return null;
    }

    @Override // com.google.android.gms.internal.zzny
    public final void zzev() {
        this.zzHX.zzaO();
    }

    @Nullable
    public final zzaev zzew() {
        if (!com.google.android.gms.ads.internal.zzbs.zzbY().zzr(this.mContext)) {
            return null;
        }
        if (this.zzuk == null) {
            this.zzuk = new zzaev(this.mContext, this.zzHX.getAdUnitId());
        }
        return this.zzuk;
    }

    @Override // com.google.android.gms.internal.zzny
    public final void zzg(View view) {
        this.zzIf = new WeakReference<>(view);
    }
}
