package com.google.android.gms.ads.internal;

import android.R;
import android.content.Context;
import android.graphics.Rect;
import android.os.RemoteException;
import android.support.annotation.Nullable;
import android.support.v4.util.SimpleArrayMap;
import android.view.View;
import android.view.ViewTreeObserver;
import com.google.android.gms.internal.zzadd;
import com.google.android.gms.internal.zzaff;
import com.google.android.gms.internal.zzafg;
import com.google.android.gms.internal.zzafh;
import com.google.android.gms.internal.zzafn;
import com.google.android.gms.internal.zzafp;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzahp;
import com.google.android.gms.internal.zzair;
import com.google.android.gms.internal.zzaiy;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zzcu;
import com.google.android.gms.internal.zziv;
import com.google.android.gms.internal.zzji;
import com.google.android.gms.internal.zzjl;
import com.google.android.gms.internal.zzjo;
import com.google.android.gms.internal.zzke;
import com.google.android.gms.internal.zzkk;
import com.google.android.gms.internal.zzky;
import com.google.android.gms.internal.zzlx;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zznh;
import com.google.android.gms.internal.zzon;
import com.google.android.gms.internal.zzpn;
import com.google.android.gms.internal.zzpq;
import com.google.android.gms.internal.zzpt;
import com.google.android.gms.internal.zzpw;
import com.google.android.gms.internal.zzpz;
import com.google.android.gms.internal.zzzn;
import java.util.HashSet;
import java.util.List;
import java.util.UUID;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzbt implements ViewTreeObserver.OnGlobalLayoutListener, ViewTreeObserver.OnScrollChangedListener {
    public final Context zzqD;
    boolean zzur;
    final String zzvQ;
    public String zzvR;
    final zzcu zzvS;
    public final zzaje zzvT;

    @Nullable
    zzbu zzvU;

    @Nullable
    public zzafp zzvV;

    @Nullable
    public zzahp zzvW;
    public zziv zzvX;

    @Nullable
    public zzaff zzvY;
    public zzafg zzvZ;
    private boolean zzwA;

    @Nullable
    public zzafh zzwa;

    @Nullable
    zzjl zzwb;

    @Nullable
    zzjo zzwc;

    @Nullable
    zzke zzwd;

    @Nullable
    zzkk zzwe;

    @Nullable
    zzpn zzwf;

    @Nullable
    zzpq zzwg;
    SimpleArrayMap<String, zzpt> zzwh;
    SimpleArrayMap<String, zzpw> zzwi;
    zzon zzwj;

    @Nullable
    zzlx zzwk;

    @Nullable
    zzky zzwl;

    @Nullable
    zzpz zzwm;

    @Nullable
    List<Integer> zzwn;

    @Nullable
    zznh zzwo;

    @Nullable
    zzadd zzwp;

    @Nullable
    List<String> zzwq;

    @Nullable
    public zzafn zzwr;

    @Nullable
    View zzws;
    public int zzwt;
    private HashSet<zzafh> zzwu;
    private int zzwv;
    private int zzww;
    private zzair zzwx;
    private boolean zzwy;
    private boolean zzwz;

    public zzbt(Context context, zziv zzivVar, String str, zzaje zzajeVar) {
        this(context, zzivVar, str, zzajeVar, null);
    }

    private zzbt(Context context, zziv zzivVar, String str, zzaje zzajeVar, zzcu zzcuVar) {
        this.zzwr = null;
        this.zzws = null;
        this.zzwt = 0;
        this.zzur = false;
        this.zzwu = null;
        this.zzwv = -1;
        this.zzww = -1;
        this.zzwy = true;
        this.zzwz = true;
        this.zzwA = false;
        zzmo.initialize(context);
        if (zzbs.zzbD().zzhr() != null) {
            List<String> listZzdK = zzmo.zzdK();
            if (zzajeVar.zzaaO != 0) {
                listZzdK.add(Integer.toString(zzajeVar.zzaaO));
            }
            zzbs.zzbD().zzhr().zze(listZzdK);
        }
        this.zzvQ = UUID.randomUUID().toString();
        if (zzivVar.zzAt || zzivVar.zzAv) {
            this.zzvU = null;
        } else {
            this.zzvU = new zzbu(context, str, zzajeVar.zzaP, this, this);
            this.zzvU.setMinimumWidth(zzivVar.widthPixels);
            this.zzvU.setMinimumHeight(zzivVar.heightPixels);
            this.zzvU.setVisibility(4);
        }
        this.zzvX = zzivVar;
        this.zzvR = str;
        this.zzqD = context;
        this.zzvT = zzajeVar;
        this.zzvS = new zzcu(new zzah(this));
        this.zzwx = new zzair(200L);
        this.zzwi = new SimpleArrayMap<>();
    }

    private final void zzd(boolean z) {
        View viewFindViewById;
        if (this.zzvU == null || this.zzvY == null || this.zzvY.zzPg == null || this.zzvY.zzPg.zziw() == null) {
            return;
        }
        if (!z || this.zzwx.tryAcquire()) {
            if (this.zzvY.zzPg.zziw().zzcn()) {
                int[] iArr = new int[2];
                this.zzvU.getLocationOnScreen(iArr);
                zzji.zzds();
                int iZzd = zzaiy.zzd(this.zzqD, iArr[0]);
                zzji.zzds();
                int iZzd2 = zzaiy.zzd(this.zzqD, iArr[1]);
                if (iZzd != this.zzwv || iZzd2 != this.zzww) {
                    this.zzwv = iZzd;
                    this.zzww = iZzd2;
                    this.zzvY.zzPg.zziw().zza(this.zzwv, this.zzww, z ? false : true);
                }
            }
            if (this.zzvU == null || (viewFindViewById = this.zzvU.getRootView().findViewById(R.id.content)) == null) {
                return;
            }
            Rect rect = new Rect();
            Rect rect2 = new Rect();
            this.zzvU.getGlobalVisibleRect(rect);
            viewFindViewById.getGlobalVisibleRect(rect2);
            if (rect.top != rect2.top) {
                this.zzwy = false;
            }
            if (rect.bottom != rect2.bottom) {
                this.zzwz = false;
            }
        }
    }

    @Override // android.view.ViewTreeObserver.OnGlobalLayoutListener
    public final void onGlobalLayout() {
        zzd(false);
    }

    @Override // android.view.ViewTreeObserver.OnScrollChangedListener
    public final void onScrollChanged() {
        zzd(true);
        this.zzwA = true;
    }

    public final void zza(HashSet<zzafh> hashSet) {
        this.zzwu = hashSet;
    }

    public final HashSet<zzafh> zzbZ() {
        return this.zzwu;
    }

    public final void zzca() {
        if (this.zzvY == null || this.zzvY.zzPg == null) {
            return;
        }
        this.zzvY.zzPg.destroy();
    }

    public final void zzcb() {
        if (this.zzvY == null || this.zzvY.zzMH == null) {
            return;
        }
        try {
            this.zzvY.zzMH.destroy();
        } catch (RemoteException e) {
            zzafr.zzaT("Could not destroy mediation adapter.");
        }
    }

    public final boolean zzcc() {
        return this.zzwt == 0;
    }

    public final boolean zzcd() {
        return this.zzwt == 1;
    }

    public final String zzce() {
        if (this.zzwy && this.zzwz) {
            return "";
        }
        if (this.zzwy) {
            return this.zzwA ? "top-scrollable" : "top-locked";
        }
        if (this.zzwz) {
            return this.zzwA ? "bottom-scrollable" : "bottom-locked";
        }
        return "";
    }

    public final void zze(boolean z) {
        if (this.zzwt == 0 && this.zzvY != null && this.zzvY.zzPg != null) {
            this.zzvY.zzPg.stopLoading();
        }
        if (this.zzvV != null) {
            this.zzvV.cancel();
        }
        if (this.zzvW != null) {
            this.zzvW.cancel();
        }
        if (z) {
            this.zzvY = null;
        }
    }
}
