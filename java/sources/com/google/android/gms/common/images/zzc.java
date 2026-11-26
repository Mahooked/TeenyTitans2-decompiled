package com.google.android.gms.common.images;

import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.widget.ImageView;
import com.google.android.gms.common.internal.zzbe;
import com.google.android.gms.internal.zzbfg;
import com.google.android.gms.internal.zzbfl;
import java.lang.ref.WeakReference;

/* JADX INFO: loaded from: classes.dex */
public final class zzc extends zza {
    private WeakReference<ImageView> zzaGm;

    public zzc(ImageView imageView, int i) {
        super(null, i);
        com.google.android.gms.common.internal.zzc.zzr(imageView);
        this.zzaGm = new WeakReference<>(imageView);
    }

    public zzc(ImageView imageView, Uri uri) {
        super(uri, 0);
        com.google.android.gms.common.internal.zzc.zzr(imageView);
        this.zzaGm = new WeakReference<>(imageView);
    }

    public final boolean equals(Object obj) {
        if (!(obj instanceof zzc)) {
            return false;
        }
        if (this == obj) {
            return true;
        }
        ImageView imageView = this.zzaGm.get();
        ImageView imageView2 = ((zzc) obj).zzaGm.get();
        return (imageView2 == null || imageView == null || !zzbe.equal(imageView2, imageView)) ? false : true;
    }

    public final int hashCode() {
        return 0;
    }

    @Override // com.google.android.gms.common.images.zza
    protected final void zza(Drawable drawable, boolean z, boolean z2, boolean z3) {
        Drawable zzbfgVar;
        ImageView imageView = this.zzaGm.get();
        if (imageView != null) {
            boolean z4 = (z2 || z3) ? false : true;
            if (z4 && (imageView instanceof zzbfl)) {
                int iZzqY = ((zzbfl) imageView).zzqY();
                if (this.zzaGh != 0 && iZzqY == this.zzaGh) {
                    return;
                }
            }
            boolean zZzc = zzc(z, z2);
            if (zZzc) {
                Drawable drawable2 = imageView.getDrawable();
                if (drawable2 == null) {
                    drawable2 = null;
                } else if (drawable2 instanceof zzbfg) {
                    drawable2 = ((zzbfg) drawable2).zzqW();
                }
                zzbfgVar = new zzbfg(drawable2, drawable);
            } else {
                zzbfgVar = drawable;
            }
            imageView.setImageDrawable(zzbfgVar);
            if (imageView instanceof zzbfl) {
                zzbfl zzbflVar = (zzbfl) imageView;
                zzbflVar.zzo(z3 ? this.zzaGf.uri : null);
                zzbflVar.zzax(z4 ? this.zzaGh : 0);
            }
            if (zZzc) {
                ((zzbfg) zzbfgVar).startTransition(250);
            }
        }
    }
}
