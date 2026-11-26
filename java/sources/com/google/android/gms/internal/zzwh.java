package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.app.AlertDialog;
import android.content.Context;
import android.content.Intent;
import android.content.res.Resources;
import android.net.http.Headers;
import android.provider.CalendarContract;
import android.text.TextUtils;
import com.google.android.gms.R;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzwh extends zzwu {
    private final Context mContext;
    private final Map<String, String> zzHa;
    private String zzNs;
    private long zzNt;
    private long zzNu;
    private String zzNv;
    private String zzNw;

    public zzwh(zzaka zzakaVar, Map<String, String> map) {
        super(zzakaVar, "createCalendarEvent");
        this.zzHa = map;
        this.mContext = zzakaVar.zzis();
        this.zzNs = zzal("description");
        this.zzNv = zzal("summary");
        this.zzNt = zzam("start_ticks");
        this.zzNu = zzam("end_ticks");
        this.zzNw = zzal(Headers.LOCATION);
    }

    private final String zzal(String str) {
        return TextUtils.isEmpty(this.zzHa.get(str)) ? "" : this.zzHa.get(str);
    }

    private final long zzam(String str) {
        String str2 = this.zzHa.get(str);
        if (str2 == null) {
            return -1L;
        }
        try {
            return Long.parseLong(str2);
        } catch (NumberFormatException e) {
            return -1L;
        }
    }

    @TargetApi(14)
    final Intent createIntent() {
        Intent data = new Intent("android.intent.action.EDIT").setData(CalendarContract.Events.CONTENT_URI);
        data.putExtra("title", this.zzNs);
        data.putExtra("eventLocation", this.zzNw);
        data.putExtra("description", this.zzNv);
        if (this.zzNt > -1) {
            data.putExtra("beginTime", this.zzNt);
        }
        if (this.zzNu > -1) {
            data.putExtra("endTime", this.zzNu);
        }
        data.setFlags(268435456);
        return data;
    }

    public final void execute() {
        if (this.mContext == null) {
            zzan("Activity context is not available.");
            return;
        }
        com.google.android.gms.ads.internal.zzbs.zzbz();
        if (!zzagz.zzH(this.mContext).zzdH()) {
            zzan("This feature is not available on the device.");
            return;
        }
        com.google.android.gms.ads.internal.zzbs.zzbz();
        AlertDialog.Builder builderZzG = zzagz.zzG(this.mContext);
        Resources resources = com.google.android.gms.ads.internal.zzbs.zzbD().getResources();
        builderZzG.setTitle(resources != null ? resources.getString(R.string.s5) : "Create calendar event");
        builderZzG.setMessage(resources != null ? resources.getString(R.string.s6) : "Allow Ad to create a calendar event?");
        builderZzG.setPositiveButton(resources != null ? resources.getString(R.string.s3) : "Accept", new zzwi(this));
        builderZzG.setNegativeButton(resources != null ? resources.getString(R.string.s4) : "Decline", new zzwj(this));
        builderZzG.create().show();
    }
}
