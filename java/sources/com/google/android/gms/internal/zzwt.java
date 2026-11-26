package com.google.android.gms.internal;

import android.app.Activity;
import android.content.Context;
import android.util.DisplayMetrics;
import android.view.Display;
import android.view.WindowManager;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzwt extends zzwu implements zzrd {
    private final Context mContext;
    private final zzaka zzJH;
    private final zzlz zzNW;
    private float zzNX;
    private int zzNY;
    private int zzNZ;
    private int zzOa;
    private int zzOb;
    private int zzOc;
    private int zzOd;
    private int zzOe;
    private final WindowManager zzwR;
    private DisplayMetrics zzxF;

    public zzwt(zzaka zzakaVar, Context context, zzlz zzlzVar) {
        super(zzakaVar);
        this.zzNY = -1;
        this.zzNZ = -1;
        this.zzOb = -1;
        this.zzOc = -1;
        this.zzOd = -1;
        this.zzOe = -1;
        this.zzJH = zzakaVar;
        this.mContext = context;
        this.zzNW = zzlzVar;
        this.zzwR = (WindowManager) context.getSystemService("window");
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        this.zzxF = new DisplayMetrics();
        Display defaultDisplay = this.zzwR.getDefaultDisplay();
        defaultDisplay.getMetrics(this.zzxF);
        this.zzNX = this.zzxF.density;
        this.zzOa = defaultDisplay.getRotation();
        zzji.zzds();
        this.zzNY = zzaiy.zzb(this.zzxF, this.zzxF.widthPixels);
        zzji.zzds();
        this.zzNZ = zzaiy.zzb(this.zzxF, this.zzxF.heightPixels);
        Activity activityZzis = this.zzJH.zzis();
        if (activityZzis == null || activityZzis.getWindow() == null) {
            this.zzOb = this.zzNY;
            this.zzOc = this.zzNZ;
        } else {
            com.google.android.gms.ads.internal.zzbs.zzbz();
            int[] iArrZzf = zzagz.zzf(activityZzis);
            zzji.zzds();
            this.zzOb = zzaiy.zzb(this.zzxF, iArrZzf[0]);
            zzji.zzds();
            this.zzOc = zzaiy.zzb(this.zzxF, iArrZzf[1]);
        }
        if (this.zzJH.zzam().zzAt) {
            this.zzOd = this.zzNY;
            this.zzOe = this.zzNZ;
        } else {
            this.zzJH.measure(0, 0);
        }
        zza(this.zzNY, this.zzNZ, this.zzOb, this.zzOc, this.zzNX, this.zzOa);
        this.zzJH.zzb("onDeviceFeaturesReceived", new zzwq(new zzws().zzm(this.zzNW.zzdE()).zzl(this.zzNW.zzdF()).zzn(this.zzNW.zzdH()).zzo(this.zzNW.zzdG()).zzp(true)).toJson());
        int[] iArr = new int[2];
        this.zzJH.getLocationOnScreen(iArr);
        zzji.zzds();
        int iZzd = zzaiy.zzd(this.mContext, iArr[0]);
        zzji.zzds();
        zzc(iZzd, zzaiy.zzd(this.mContext, iArr[1]));
        if (zzafr.zzz(2)) {
            zzafr.zzaS("Dispatching Ready Event.");
        }
        zzao(this.zzJH.zziz().zzaP);
    }

    public final void zzc(int i, int i2) {
        int i3 = this.mContext instanceof Activity ? com.google.android.gms.ads.internal.zzbs.zzbz().zzh((Activity) this.mContext)[0] : 0;
        if (this.zzJH.zzam() == null || !this.zzJH.zzam().zzAt) {
            zzji.zzds();
            this.zzOd = zzaiy.zzd(this.mContext, this.zzJH.getWidth());
            zzji.zzds();
            this.zzOe = zzaiy.zzd(this.mContext, this.zzJH.getHeight());
        }
        zzc(i, i2 - i3, this.zzOd, this.zzOe);
        this.zzJH.zziw().zzb(i, i2);
    }
}
