package com.google.android.gms.internal;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.net.Uri;
import android.text.TextUtils;
import android.view.MotionEvent;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzahq {
    private final Context mContext;
    private int mState;
    private final float zzNX;
    private String zzZB;
    private float zzZC;
    private float zzZD;
    private float zzZE;
    private String zztV;
    private String zzwH;

    public zzahq(Context context) {
        this.mState = 0;
        this.mContext = context;
        this.zzNX = context.getResources().getDisplayMetrics().density;
    }

    public zzahq(Context context, String str) {
        this(context);
        this.zzZB = str;
    }

    private static int zza(List<String> list, String str, boolean z) {
        if (!z) {
            return -1;
        }
        list.add(str);
        return list.size() - 1;
    }

    private final void zza(int i, float f, float f2) {
        if (i == 0) {
            this.mState = 0;
            this.zzZC = f;
            this.zzZD = f2;
            this.zzZE = f2;
            return;
        }
        if (this.mState != -1) {
            if (i != 2) {
                if (i == 1 && this.mState == 4) {
                    showDialog();
                    return;
                }
                return;
            }
            if (f2 > this.zzZD) {
                this.zzZD = f2;
            } else if (f2 < this.zzZE) {
                this.zzZE = f2;
            }
            if (this.zzZD - this.zzZE > 30.0f * this.zzNX) {
                this.mState = -1;
                return;
            }
            if (this.mState == 0 || this.mState == 2) {
                if (f - this.zzZC >= 50.0f * this.zzNX) {
                    this.zzZC = f;
                    this.mState++;
                }
            } else if ((this.mState == 1 || this.mState == 3) && f - this.zzZC <= (-50.0f) * this.zzNX) {
                this.zzZC = f;
                this.mState++;
            }
            if (this.mState == 1 || this.mState == 3) {
                if (f > this.zzZC) {
                    this.zzZC = f;
                }
            } else {
                if (this.mState != 2 || f >= this.zzZC) {
                    return;
                }
                this.zzZC = f;
            }
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    /* JADX WARN: Code duplicated, block: B:15:0x009d  */
    public final void zzhY() {
        String strTrim;
        if (!(this.mContext instanceof Activity)) {
            zzafr.zzaS("Can not create dialog without Activity Context");
            return;
        }
        String str = this.zzZB;
        if (TextUtils.isEmpty(str)) {
            strTrim = "No debug information";
        } else {
            Uri uriBuild = new Uri.Builder().encodedQuery(str.replaceAll("\\+", "%20")).build();
            StringBuilder sb = new StringBuilder();
            com.google.android.gms.ads.internal.zzbs.zzbz();
            Map<String, String> mapZzg = zzagz.zzg(uriBuild);
            for (String str2 : mapZzg.keySet()) {
                sb.append(str2).append(" = ").append(mapZzg.get(str2)).append("\n\n");
            }
            strTrim = sb.toString().trim();
            if (TextUtils.isEmpty(strTrim)) {
                strTrim = "No debug information";
            }
        }
        AlertDialog.Builder builder = new AlertDialog.Builder(this.mContext);
        builder.setMessage(strTrim);
        builder.setTitle("Ad Information");
        builder.setPositiveButton("Share", new zzahs(this, strTrim));
        builder.setNegativeButton(HTTP.CONN_CLOSE, new zzaht(this));
        builder.create().show();
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzhZ() {
        zzafr.zzaC("Debug mode [Creative Preview] selected.");
        zzagt.zza(new zzahu(this));
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzia() {
        zzafr.zzaC("Debug mode [Troubleshooting] selected.");
        zzagt.zza(new zzahv(this));
    }

    public final void setAdUnitId(String str) {
        this.zztV = str;
    }

    public final void showDialog() {
        if (!((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGs)).booleanValue()) {
            if (!((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGr)).booleanValue()) {
                zzhY();
                return;
            }
        }
        if (!(this.mContext instanceof Activity)) {
            zzafr.zzaS("Can not create dialog without Activity Context");
            return;
        }
        ArrayList arrayList = new ArrayList();
        int iZza = zza((List<String>) arrayList, "Ad Information", true);
        int iZza2 = zza(arrayList, "Creative Preview", ((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGr)).booleanValue());
        int iZza3 = zza(arrayList, "Troubleshooting", ((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGs)).booleanValue());
        AlertDialog.Builder builder = new AlertDialog.Builder(this.mContext, com.google.android.gms.ads.internal.zzbs.zzbB().zzhX());
        builder.setTitle("Select a Debug Mode").setItems((CharSequence[]) arrayList.toArray(new String[0]), new zzahr(this, iZza, iZza2, iZza3));
        builder.create().show();
    }

    public final void zzaO(String str) {
        this.zzwH = str;
    }

    public final void zzaP(String str) {
        this.zzZB = str;
    }

    public final void zzf(MotionEvent motionEvent) {
        int historySize = motionEvent.getHistorySize();
        for (int i = 0; i < historySize; i++) {
            zza(motionEvent.getActionMasked(), motionEvent.getHistoricalX(0, i), motionEvent.getHistoricalY(0, i));
        }
        zza(motionEvent.getActionMasked(), motionEvent.getX(), motionEvent.getY());
    }
}
