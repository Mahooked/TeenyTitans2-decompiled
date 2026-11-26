package com.google.android.gms.common.internal;

import android.content.Context;
import android.content.res.Resources;
import com.google.android.gms.R;

/* JADX INFO: loaded from: classes.dex */
public final class zzby {
    private final Resources zzaIw;
    private final String zzaIx;

    public zzby(Context context) {
        zzbo.zzu(context);
        this.zzaIw = context.getResources();
        this.zzaIx = this.zzaIw.getResourcePackageName(R.string.common_google_play_services_unknown_issue);
    }

    public final String getString(String str) {
        int identifier = this.zzaIw.getIdentifier(str, "string", this.zzaIx);
        if (identifier == 0) {
            return null;
        }
        return this.zzaIw.getString(identifier);
    }
}
