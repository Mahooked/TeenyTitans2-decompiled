package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.view.View;

/* JADX INFO: loaded from: classes.dex */
@TargetApi(18)
public class zzahl extends zzahk {
    @Override // com.google.android.gms.internal.zzahe
    public boolean isAttachedToWindow(View view) {
        return super.isAttachedToWindow(view) || view.getWindowId() != null;
    }

    @Override // com.google.android.gms.internal.zzahe
    public final int zzhV() {
        return 14;
    }
}
