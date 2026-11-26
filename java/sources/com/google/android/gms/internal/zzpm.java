package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;
import com.google.android.gms.ads.VideoController;
import com.google.android.gms.ads.formats.MediaView;
import com.google.android.gms.ads.formats.NativeAd;
import com.google.android.gms.ads.formats.NativeCustomTemplateAd;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzpm implements NativeCustomTemplateAd {
    private final VideoController zzBd = new VideoController();
    private final zzpj zzIJ;
    private final MediaView zzIK;

    public zzpm(zzpj zzpjVar) {
        Context context;
        MediaView mediaView = null;
        this.zzIJ = zzpjVar;
        try {
            context = (Context) com.google.android.gms.dynamic.zzn.zzE(zzpjVar.zzen());
        } catch (RemoteException | NullPointerException e) {
            zzajc.zzb("Unable to inflate MediaView.", e);
            context = null;
        }
        if (context != null) {
            MediaView mediaView2 = new MediaView(context);
            try {
                mediaView = this.zzIJ.zzj(com.google.android.gms.dynamic.zzn.zzw(mediaView2)) ? mediaView2 : null;
            } catch (RemoteException e2) {
                zzajc.zzb("Unable to render video in MediaView.", e2);
            }
        }
        this.zzIK = mediaView;
    }

    @Override // com.google.android.gms.ads.formats.NativeCustomTemplateAd
    public final void destroy() {
        try {
            this.zzIJ.destroy();
        } catch (RemoteException e) {
            zzajc.zzb("Failed to destroy ad.", e);
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeCustomTemplateAd
    public final List<String> getAvailableAssetNames() {
        try {
            return this.zzIJ.getAvailableAssetNames();
        } catch (RemoteException e) {
            zzajc.zzb("Failed to get available asset names.", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeCustomTemplateAd
    public final String getCustomTemplateId() {
        try {
            return this.zzIJ.getCustomTemplateId();
        } catch (RemoteException e) {
            zzajc.zzb("Failed to get custom template id.", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeCustomTemplateAd
    public final NativeAd.Image getImage(String str) {
        try {
            zzos zzosVarZzQ = this.zzIJ.zzQ(str);
            if (zzosVarZzQ != null) {
                return new zzov(zzosVarZzQ);
            }
        } catch (RemoteException e) {
            zzajc.zzb("Failed to get image.", e);
        }
        return null;
    }

    @Override // com.google.android.gms.ads.formats.NativeCustomTemplateAd
    public final CharSequence getText(String str) {
        try {
            return this.zzIJ.zzP(str);
        } catch (RemoteException e) {
            zzajc.zzb("Failed to get string.", e);
            return null;
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeCustomTemplateAd
    public final VideoController getVideoController() {
        try {
            zzks videoController = this.zzIJ.getVideoController();
            if (videoController != null) {
                this.zzBd.zza(videoController);
            }
        } catch (RemoteException e) {
            zzajc.zzb("Exception occurred while getting video controller", e);
        }
        return this.zzBd;
    }

    @Override // com.google.android.gms.ads.formats.NativeCustomTemplateAd
    public final MediaView getVideoMediaView() {
        return this.zzIK;
    }

    @Override // com.google.android.gms.ads.formats.NativeCustomTemplateAd
    public final void performClick(String str) {
        try {
            this.zzIJ.performClick(str);
        } catch (RemoteException e) {
            zzajc.zzb("Failed to perform click.", e);
        }
    }

    @Override // com.google.android.gms.ads.formats.NativeCustomTemplateAd
    public final void recordImpression() {
        try {
            this.zzIJ.recordImpression();
        } catch (RemoteException e) {
            zzajc.zzb("Failed to record impression.", e);
        }
    }

    public final zzpj zzex() {
        return this.zzIJ;
    }
}
