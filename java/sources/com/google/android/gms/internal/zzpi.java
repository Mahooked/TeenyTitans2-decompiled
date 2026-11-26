package com.google.android.gms.internal;

import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.RemoteException;
import com.google.android.gms.ads.VideoController;
import com.google.android.gms.ads.formats.NativeAd;
import com.google.android.gms.ads.formats.NativeContentAd;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.util.ArrayList;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzpi extends NativeContentAd {
    private final zzpf zzIH;
    private final zzov zzII;
    private final List<NativeAd.Image> zzIF = new ArrayList();
    private final VideoController zzBd = new VideoController();

    public zzpi(zzpf zzpfVar) {
        zzov zzovVar;
        zzos zzouVar;
        IBinder iBinder;
        this.zzIH = zzpfVar;
        try {
            List images = this.zzIH.getImages();
            if (images != null) {
                for (Object obj : images) {
                    if (!(obj instanceof IBinder) || (iBinder = (IBinder) obj) == null) {
                        zzouVar = null;
                    } else {
                        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.ads.internal.formats.client.INativeAdImage");
                        zzouVar = iInterfaceQueryLocalInterface instanceof zzos ? (zzos) iInterfaceQueryLocalInterface : new zzou(iBinder);
                    }
                    if (zzouVar != null) {
                        this.zzIF.add(new zzov(zzouVar));
                    }
                }
            }
        } catch (RemoteException e) {
            zzajc.zzb("Failed to get image.", e);
        }
        try {
            zzos zzosVarZzem = this.zzIH.zzem();
            zzovVar = zzosVarZzem != null ? new zzov(zzosVarZzem) : null;
        } catch (RemoteException e2) {
            zzajc.zzb("Failed to get icon.", e2);
        }
        this.zzII = zzovVar;
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Override // com.google.android.gms.ads.formats.NativeAd
    /* JADX INFO: renamed from: zzei, reason: merged with bridge method [inline-methods] */
    public final IObjectWrapper zzag() {
        try {
            return this.zzIH.zzei();
        } catch (RemoteException e) {
            zzajc.zzb("Failed to retrieve native ad engine.", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeContentAd
    public final void destroy() {
        try {
            this.zzIH.destroy();
        } catch (RemoteException e) {
            zzajc.zzb("Failed to destroy", e);
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeContentAd
    public final CharSequence getAdvertiser() {
        try {
            return this.zzIH.getAdvertiser();
        } catch (RemoteException e) {
            zzajc.zzb("Failed to get attribution.", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeContentAd
    public final CharSequence getBody() {
        try {
            return this.zzIH.getBody();
        } catch (RemoteException e) {
            zzajc.zzb("Failed to get body.", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeContentAd
    public final CharSequence getCallToAction() {
        try {
            return this.zzIH.getCallToAction();
        } catch (RemoteException e) {
            zzajc.zzb("Failed to get call to action.", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeContentAd
    public final Bundle getExtras() {
        try {
            return this.zzIH.getExtras();
        } catch (RemoteException e) {
            zzajc.zzc("Failed to get extras", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeContentAd
    public final CharSequence getHeadline() {
        try {
            return this.zzIH.getHeadline();
        } catch (RemoteException e) {
            zzajc.zzb("Failed to get headline.", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeContentAd
    public final List<NativeAd.Image> getImages() {
        return this.zzIF;
    }

    @Override // com.google.android.gms.ads.formats.NativeContentAd
    public final NativeAd.Image getLogo() {
        return this.zzII;
    }

    @Override // com.google.android.gms.ads.formats.NativeContentAd
    public final VideoController getVideoController() {
        try {
            if (this.zzIH.getVideoController() != null) {
                this.zzBd.zza(this.zzIH.getVideoController());
            }
        } catch (RemoteException e) {
            zzajc.zzb("Exception occurred while getting video controller", e);
        }
        return this.zzBd;
    }
}
