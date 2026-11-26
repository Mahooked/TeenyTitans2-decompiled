package com.google.android.gms.internal;

import android.app.AlertDialog;
import android.content.Context;
import android.content.res.Resources;
import android.net.Uri;
import android.text.TextUtils;
import android.webkit.URLUtil;
import com.google.android.gms.R;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzwn extends zzwu {
    private final Context mContext;
    private final Map<String, String> zzHa;

    public zzwn(zzaka zzakaVar, Map<String, String> map) {
        super(zzakaVar, "storePicture");
        this.zzHa = map;
        this.mContext = zzakaVar.zzis();
    }

    public final void execute() {
        if (this.mContext == null) {
            zzan("Activity context is not available");
            return;
        }
        com.google.android.gms.ads.internal.zzbs.zzbz();
        if (!zzagz.zzH(this.mContext).zzdG()) {
            zzan("Feature is not supported by the device.");
            return;
        }
        String str = this.zzHa.get("iurl");
        if (TextUtils.isEmpty(str)) {
            zzan("Image url cannot be empty.");
            return;
        }
        if (!URLUtil.isValidUrl(str)) {
            String strValueOf = String.valueOf(str);
            zzan(strValueOf.length() != 0 ? "Invalid image url: ".concat(strValueOf) : new String("Invalid image url: "));
            return;
        }
        String lastPathSegment = Uri.parse(str).getLastPathSegment();
        com.google.android.gms.ads.internal.zzbs.zzbz();
        if (!zzagz.zzaK(lastPathSegment)) {
            String strValueOf2 = String.valueOf(lastPathSegment);
            zzan(strValueOf2.length() != 0 ? "Image type not recognized: ".concat(strValueOf2) : new String("Image type not recognized: "));
            return;
        }
        Resources resources = com.google.android.gms.ads.internal.zzbs.zzbD().getResources();
        com.google.android.gms.ads.internal.zzbs.zzbz();
        AlertDialog.Builder builderZzG = zzagz.zzG(this.mContext);
        builderZzG.setTitle(resources != null ? resources.getString(R.string.s1) : "Save image");
        builderZzG.setMessage(resources != null ? resources.getString(R.string.s2) : "Allow Ad to store image in Picture gallery?");
        builderZzG.setPositiveButton(resources != null ? resources.getString(R.string.s3) : "Accept", new zzwo(this, str, lastPathSegment));
        builderZzG.setNegativeButton(resources != null ? resources.getString(R.string.s4) : "Decline", new zzwp(this));
        builderZzG.create().show();
    }
}
