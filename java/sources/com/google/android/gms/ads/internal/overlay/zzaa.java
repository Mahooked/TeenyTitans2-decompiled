package com.google.android.gms.ads.internal.overlay;

import android.annotation.TargetApi;
import android.content.Context;
import android.graphics.Bitmap;
import android.support.annotation.Nullable;
import android.support.v4.internal.view.SupportMenu;
import android.support.v4.view.InputDeviceCompat;
import android.text.TextUtils;
import android.view.MotionEvent;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.TextView;
import com.google.android.gms.ads.internal.zzbs;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zznb;
import com.google.android.gms.internal.zzzn;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaa extends FrameLayout implements zzx {
    private final zzaka zzJH;
    private String zzJM;
    private long zzPA;
    private long zzPB;
    private Bitmap zzPC;
    private ImageView zzPD;
    private boolean zzPE;
    private final FrameLayout zzPr;
    private final zznb zzPs;
    private final zzas zzPt;
    private final long zzPu;

    @Nullable
    private zzy zzPv;
    private boolean zzPw;
    private boolean zzPx;
    private boolean zzPy;
    private boolean zzPz;

    public zzaa(Context context, zzaka zzakaVar, int i, boolean z, zznb zznbVar, zzaq zzaqVar) {
        super(context);
        this.zzJH = zzakaVar;
        this.zzPs = zznbVar;
        this.zzPr = new FrameLayout(context);
        addView(this.zzPr, new FrameLayout.LayoutParams(-1, -1));
        com.google.android.gms.common.internal.zzc.zzr(zzakaVar.zzak());
        this.zzPv = zzakaVar.zzak().zztn.zza(context, zzakaVar, i, z, zznbVar, zzaqVar);
        if (this.zzPv != null) {
            this.zzPr.addView(this.zzPv, new FrameLayout.LayoutParams(-1, -1, 17));
            if (((Boolean) zzbs.zzbL().zzd(zzmo.zzCw)).booleanValue()) {
                zzgb();
            }
        }
        this.zzPD = new ImageView(context);
        this.zzPu = ((Long) zzbs.zzbL().zzd(zzmo.zzCA)).longValue();
        this.zzPz = ((Boolean) zzbs.zzbL().zzd(zzmo.zzCy)).booleanValue();
        if (this.zzPs != null) {
            this.zzPs.zzh("spinner_used", this.zzPz ? "1" : "0");
        }
        this.zzPt = new zzas(this);
        if (this.zzPv != null) {
            this.zzPv.zza(this);
        }
        if (this.zzPv == null) {
            zzj("AdVideoUnderlay Error", "Allocating player failed.");
        }
    }

    public static void zza(zzaka zzakaVar, String str) {
        HashMap map = new HashMap();
        map.put("event", "decoderProps");
        map.put("error", str);
        zzakaVar.zza("onVideoEvent", map);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zza(String str, String... strArr) {
        HashMap map = new HashMap();
        map.put("event", str);
        int length = strArr.length;
        int i = 0;
        String str2 = null;
        while (i < length) {
            String str3 = strArr[i];
            if (str2 != null) {
                map.put(str2, str3);
                str3 = null;
            }
            i++;
            str2 = str3;
        }
        this.zzJH.zza("onVideoEvent", map);
    }

    public static void zzc(zzaka zzakaVar, Map<String, List<Map<String, Object>>> map) {
        HashMap map2 = new HashMap();
        map2.put("event", "decoderProps");
        map2.put("mimeTypes", map);
        zzakaVar.zza("onVideoEvent", map2);
    }

    private final boolean zzgd() {
        return this.zzPD.getParent() != null;
    }

    private final void zzge() {
        if (this.zzJH.zzis() == null || !this.zzPx || this.zzPy) {
            return;
        }
        this.zzJH.zzis().getWindow().clearFlags(128);
        this.zzPx = false;
    }

    public static void zzh(zzaka zzakaVar) {
        HashMap map = new HashMap();
        map.put("event", "no_video_view");
        zzakaVar.zza("onVideoEvent", map);
    }

    public final void destroy() {
        this.zzPt.pause();
        if (this.zzPv != null) {
            this.zzPv.stop();
        }
        zzge();
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzx
    public final void onPaused() {
        zza("pause", new String[0]);
        zzge();
        this.zzPw = false;
    }

    public final void pause() {
        if (this.zzPv == null) {
            return;
        }
        this.zzPv.pause();
    }

    public final void play() {
        if (this.zzPv == null) {
            return;
        }
        this.zzPv.play();
    }

    public final void seekTo(int i) {
        if (this.zzPv == null) {
            return;
        }
        this.zzPv.seekTo(i);
    }

    public final void zza(float f, float f2) {
        if (this.zzPv != null) {
            this.zzPv.zza(f, f2);
        }
    }

    public final void zzaq(String str) {
        this.zzJM = str;
    }

    public final void zzb(float f) {
        if (this.zzPv == null) {
            return;
        }
        zzy zzyVar = this.zzPv;
        zzyVar.zzPq.zzb(f);
        zzyVar.zzfH();
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzx
    public final void zzd(int i, int i2) {
        if (this.zzPz) {
            int iMax = Math.max(i / ((Integer) zzbs.zzbL().zzd(zzmo.zzCz)).intValue(), 1);
            int iMax2 = Math.max(i2 / ((Integer) zzbs.zzbL().zzd(zzmo.zzCz)).intValue(), 1);
            if (this.zzPC != null && this.zzPC.getWidth() == iMax && this.zzPC.getHeight() == iMax2) {
                return;
            }
            this.zzPC = Bitmap.createBitmap(iMax, iMax2, Bitmap.Config.ARGB_8888);
            this.zzPE = false;
        }
    }

    public final void zzd(int i, int i2, int i3, int i4) {
        if (i3 == 0 || i4 == 0) {
            return;
        }
        FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(i3, i4);
        layoutParams.setMargins(i, i2, 0, 0);
        this.zzPr.setLayoutParams(layoutParams);
        requestLayout();
    }

    @TargetApi(14)
    public final void zze(MotionEvent motionEvent) {
        if (this.zzPv == null) {
            return;
        }
        this.zzPv.dispatchTouchEvent(motionEvent);
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzx
    public final void zzfS() {
        this.zzPt.resume();
        zzagz.zzZr.post(new zzab(this));
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzx
    public final void zzfT() {
        if (this.zzPv != null && this.zzPB == 0) {
            zza("canplaythrough", "duration", String.valueOf(this.zzPv.getDuration() / 1000.0f), "videoWidth", String.valueOf(this.zzPv.getVideoWidth()), "videoHeight", String.valueOf(this.zzPv.getVideoHeight()));
        }
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzx
    public final void zzfU() {
        if (this.zzJH.zzis() != null && !this.zzPx) {
            this.zzPy = (this.zzJH.zzis().getWindow().getAttributes().flags & 128) != 0;
            if (!this.zzPy) {
                this.zzJH.zzis().getWindow().addFlags(128);
                this.zzPx = true;
            }
        }
        this.zzPw = true;
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzx
    public final void zzfV() {
        zza("ended", new String[0]);
        zzge();
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzx
    public final void zzfW() {
        if (this.zzPE && this.zzPC != null && !zzgd()) {
            this.zzPD.setImageBitmap(this.zzPC);
            this.zzPD.invalidate();
            this.zzPr.addView(this.zzPD, new FrameLayout.LayoutParams(-1, -1));
            this.zzPr.bringChildToFront(this.zzPD);
        }
        this.zzPt.pause();
        this.zzPB = this.zzPA;
        zzagz.zzZr.post(new zzac(this));
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzx
    public final void zzfX() {
        if (this.zzPw && zzgd()) {
            this.zzPr.removeView(this.zzPD);
        }
        if (this.zzPC != null) {
            long jElapsedRealtime = zzbs.zzbF().elapsedRealtime();
            if (this.zzPv.getBitmap(this.zzPC) != null) {
                this.zzPE = true;
            }
            long jElapsedRealtime2 = zzbs.zzbF().elapsedRealtime() - jElapsedRealtime;
            if (zzafr.zzhM()) {
                zzafr.v(new StringBuilder(46).append("Spinner frame grab took ").append(jElapsedRealtime2).append("ms").toString());
            }
            if (jElapsedRealtime2 > this.zzPu) {
                zzafr.zzaT("Spinner frame grab crossed jank threshold! Suspending spinner.");
                this.zzPz = false;
                this.zzPC = null;
                if (this.zzPs != null) {
                    this.zzPs.zzh("spinner_jank", Long.toString(jElapsedRealtime2));
                }
            }
        }
    }

    public final void zzfY() {
        if (this.zzPv == null) {
            return;
        }
        if (TextUtils.isEmpty(this.zzJM)) {
            zza("no_src", new String[0]);
        } else {
            this.zzPv.setVideoPath(this.zzJM);
        }
    }

    public final void zzfZ() {
        if (this.zzPv == null) {
            return;
        }
        zzy zzyVar = this.zzPv;
        zzyVar.zzPq.setMuted(true);
        zzyVar.zzfH();
    }

    public final void zzga() {
        if (this.zzPv == null) {
            return;
        }
        zzy zzyVar = this.zzPv;
        zzyVar.zzPq.setMuted(false);
        zzyVar.zzfH();
    }

    @TargetApi(14)
    public final void zzgb() {
        if (this.zzPv == null) {
            return;
        }
        TextView textView = new TextView(this.zzPv.getContext());
        String strValueOf = String.valueOf(this.zzPv.zzfD());
        textView.setText(strValueOf.length() != 0 ? "AdMob - ".concat(strValueOf) : new String("AdMob - "));
        textView.setTextColor(SupportMenu.CATEGORY_MASK);
        textView.setBackgroundColor(InputDeviceCompat.SOURCE_ANY);
        this.zzPr.addView(textView, new FrameLayout.LayoutParams(-2, -2, 17));
        this.zzPr.bringChildToFront(textView);
    }

    final void zzgc() {
        if (this.zzPv == null) {
            return;
        }
        long currentPosition = this.zzPv.getCurrentPosition();
        if (this.zzPA == currentPosition || currentPosition <= 0) {
            return;
        }
        zza("timeupdate", "time", String.valueOf(currentPosition / 1000.0f));
        this.zzPA = currentPosition;
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzx
    public final void zzj(String str, @Nullable String str2) {
        zza("error", "what", str, "extra", str2);
    }
}
