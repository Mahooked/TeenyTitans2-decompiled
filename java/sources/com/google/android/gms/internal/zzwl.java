package com.google.android.gms.internal;

import android.view.View;

/* JADX INFO: loaded from: classes.dex */
final class zzwl implements View.OnClickListener {
    private /* synthetic */ zzwk zzNL;

    zzwl(zzwk zzwkVar) {
        this.zzNL = zzwkVar;
    }

    @Override // android.view.View.OnClickListener
    public final void onClick(View view) {
        this.zzNL.zzk(true);
    }
}
