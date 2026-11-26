package com.google.android.gms.common.images;

import android.graphics.drawable.Drawable;
import android.net.Uri;
import com.google.android.gms.common.internal.zzbe;
import java.lang.ref.WeakReference;
import java.util.Arrays;

/* JADX INFO: loaded from: classes.dex */
public final class zzd extends zza {
    private WeakReference<ImageManager.OnImageLoadedListener> zzaGn;

    public zzd(ImageManager.OnImageLoadedListener onImageLoadedListener, Uri uri) {
        super(uri, 0);
        com.google.android.gms.common.internal.zzc.zzr(onImageLoadedListener);
        this.zzaGn = new WeakReference<>(onImageLoadedListener);
    }

    public final boolean equals(Object obj) {
        if (!(obj instanceof zzd)) {
            return false;
        }
        if (this == obj) {
            return true;
        }
        zzd zzdVar = (zzd) obj;
        ImageManager.OnImageLoadedListener onImageLoadedListener = this.zzaGn.get();
        ImageManager.OnImageLoadedListener onImageLoadedListener2 = zzdVar.zzaGn.get();
        return onImageLoadedListener2 != null && onImageLoadedListener != null && zzbe.equal(onImageLoadedListener2, onImageLoadedListener) && zzbe.equal(zzdVar.zzaGf, this.zzaGf);
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzaGf});
    }

    @Override // com.google.android.gms.common.images.zza
    protected final void zza(Drawable drawable, boolean z, boolean z2, boolean z3) {
        ImageManager.OnImageLoadedListener onImageLoadedListener;
        if (z2 || (onImageLoadedListener = this.zzaGn.get()) == null) {
            return;
        }
        onImageLoadedListener.onImageLoaded(this.zzaGf.uri, drawable, z3);
    }
}
