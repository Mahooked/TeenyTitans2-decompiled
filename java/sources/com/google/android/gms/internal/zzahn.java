package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.view.View;
import android.view.ViewGroup;

/* JADX INFO: loaded from: classes.dex */
@TargetApi(19)
public class zzahn extends zzahl {
    @Override // com.google.android.gms.internal.zzahl, com.google.android.gms.internal.zzahe
    public final boolean isAttachedToWindow(View view) {
        return view.isAttachedToWindow();
    }

    @Override // com.google.android.gms.internal.zzahe
    public final ViewGroup.LayoutParams zzhW() {
        return new ViewGroup.LayoutParams(-1, -1);
    }
}
