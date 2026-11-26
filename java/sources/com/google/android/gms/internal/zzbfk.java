package com.google.android.gms.internal;

import android.graphics.drawable.Drawable;

/* JADX INFO: loaded from: classes.dex */
final class zzbfk extends Drawable.ConstantState {
    int mChangingConfigurations;
    int zzaGD;

    zzbfk(zzbfk zzbfkVar) {
        if (zzbfkVar != null) {
            this.mChangingConfigurations = zzbfkVar.mChangingConfigurations;
            this.zzaGD = zzbfkVar.zzaGD;
        }
    }

    @Override // android.graphics.drawable.Drawable.ConstantState
    public final int getChangingConfigurations() {
        return this.mChangingConfigurations;
    }

    @Override // android.graphics.drawable.Drawable.ConstantState
    public final Drawable newDrawable() {
        return new zzbfg(this);
    }
}
