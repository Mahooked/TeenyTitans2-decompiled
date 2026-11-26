package com.google.android.gms.internal;

import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.RemoteException;
import com.google.android.gms.ads.VideoController;
import com.google.android.gms.ads.formats.NativeAd;
import com.google.android.gms.ads.formats.NativeAppInstallAd;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.util.ArrayList;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzpe extends NativeAppInstallAd {
    private final zzpb zzIE;
    private final zzov zzIG;
    private final List<NativeAd.Image> zzIF = new ArrayList();
    private final VideoController zzBd = new VideoController();

    public zzpe(zzpb zzpbVar) {
        zzov zzovVar;
        zzos zzouVar;
        IBinder iBinder;
        this.zzIE = zzpbVar;
        try {
            List images = this.zzIE.getImages();
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
            zzos zzosVarZzeh = this.zzIE.zzeh();
            zzovVar = zzosVarZzeh != null ? new zzov(zzosVarZzeh) : null;
        } catch (RemoteException e2) {
            zzajc.zzb("Failed to get icon.", e2);
        }
        this.zzIG = zzovVar;
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Override // com.google.android.gms.ads.formats.NativeAd
    /* JADX INFO: renamed from: zzei, reason: merged with bridge method [inline-methods] */
    public final IObjectWrapper zzag() {
        try {
            return this.zzIE.zzei();
        } catch (RemoteException e) {
            zzajc.zzb("Failed to retrieve native ad engine.", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeAppInstallAd
    public final void destroy() {
        try {
            this.zzIE.destroy();
        } catch (RemoteException e) {
            zzajc.zzb("Failed to destroy", e);
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeAppInstallAd
    public final CharSequence getBody() {
        try {
            return this.zzIE.getBody();
        } catch (RemoteException e) {
            zzajc.zzb("Failed to get body.", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeAppInstallAd
    public final CharSequence getCallToAction() {
        try {
            return this.zzIE.getCallToAction();
        } catch (RemoteException e) {
            zzajc.zzb("Failed to get call to action.", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeAppInstallAd
    public final Bundle getExtras() {
        try {
            return this.zzIE.getExtras();
        } catch (RemoteException e) {
            zzajc.zzb("Failed to get extras", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeAppInstallAd
    public final CharSequence getHeadline() {
        try {
            return this.zzIE.getHeadline();
        } catch (RemoteException e) {
            zzajc.zzb("Failed to get headline.", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeAppInstallAd
    public final NativeAd.Image getIcon() {
        return this.zzIG;
    }

    @Override // com.google.android.gms.ads.formats.NativeAppInstallAd
    public final List<NativeAd.Image> getImages() {
        return this.zzIF;
    }

    @Override // com.google.android.gms.ads.formats.NativeAppInstallAd
    public final CharSequence getPrice() {
        try {
            return this.zzIE.getPrice();
        } catch (RemoteException e) {
            zzajc.zzb("Failed to get price.", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeAppInstallAd
    public final Double getStarRating() {
        try {
            double starRating = this.zzIE.getStarRating();
            if (starRating == -1.0d) {
                return null;
            }
            return Double.valueOf(starRating);
        } catch (RemoteException e) {
            zzajc.zzb("Failed to get star rating.", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeAppInstallAd
    public final CharSequence getStore() {
        try {
            return this.zzIE.getStore();
        } catch (RemoteException e) {
            zzajc.zzb("Failed to get store", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeAppInstallAd
    public final VideoController getVideoController() {
        try {
            if (this.zzIE.getVideoController() != null) {
                this.zzBd.zza(this.zzIE.getVideoController());
            }
        } catch (RemoteException e) {
            zzajc.zzb("Exception occurred while getting video controller", e);
        }
        return this.zzBd;
    }
}
