package com.google.android.gms.ads.internal.overlay;

import android.annotation.TargetApi;
import android.content.Context;
import android.graphics.SurfaceTexture;
import android.media.MediaPlayer;
import android.net.Uri;
import android.os.Build;
import android.view.Surface;
import android.view.TextureView;
import android.view.View;
import com.google.android.gms.ads.internal.zzbs;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzia;
import com.google.android.gms.internal.zzzn;
import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
@TargetApi(14)
public final class zzd extends zzy implements MediaPlayer.OnBufferingUpdateListener, MediaPlayer.OnCompletionListener, MediaPlayer.OnErrorListener, MediaPlayer.OnInfoListener, MediaPlayer.OnPreparedListener, MediaPlayer.OnVideoSizeChangedListener, TextureView.SurfaceTextureListener {
    private static final Map<Integer, String> zzOk = new HashMap();
    private final zzar zzOl;
    private final boolean zzOm;
    private int zzOn;
    private int zzOo;
    private MediaPlayer zzOp;
    private Uri zzOq;
    private int zzOr;
    private int zzOs;
    private int zzOt;
    private int zzOu;
    private int zzOv;
    private zzap zzOw;
    private boolean zzOx;
    private int zzOy;
    private zzx zzOz;

    static {
        if (Build.VERSION.SDK_INT >= 17) {
            zzOk.put(-1004, "MEDIA_ERROR_IO");
            zzOk.put(-1007, "MEDIA_ERROR_MALFORMED");
            zzOk.put(-1010, "MEDIA_ERROR_UNSUPPORTED");
            zzOk.put(-110, "MEDIA_ERROR_TIMED_OUT");
            zzOk.put(3, "MEDIA_INFO_VIDEO_RENDERING_START");
        }
        zzOk.put(100, "MEDIA_ERROR_SERVER_DIED");
        zzOk.put(1, "MEDIA_ERROR_UNKNOWN");
        zzOk.put(1, "MEDIA_INFO_UNKNOWN");
        zzOk.put(700, "MEDIA_INFO_VIDEO_TRACK_LAGGING");
        zzOk.put(701, "MEDIA_INFO_BUFFERING_START");
        zzOk.put(702, "MEDIA_INFO_BUFFERING_END");
        zzOk.put(800, "MEDIA_INFO_BAD_INTERLEAVING");
        zzOk.put(801, "MEDIA_INFO_NOT_SEEKABLE");
        zzOk.put(802, "MEDIA_INFO_METADATA_UPDATE");
        if (Build.VERSION.SDK_INT >= 19) {
            zzOk.put(901, "MEDIA_INFO_UNSUPPORTED_SUBTITLE");
            zzOk.put(902, "MEDIA_INFO_SUBTITLE_TIMED_OUT");
        }
    }

    public zzd(Context context, boolean z, boolean z2, zzaq zzaqVar, zzar zzarVar) {
        super(context);
        this.zzOn = 0;
        this.zzOo = 0;
        setSurfaceTextureListener(this);
        this.zzOl = zzarVar;
        this.zzOx = z;
        this.zzOm = z2;
        this.zzOl.zza(this);
    }

    private final void zza(float f) {
        if (this.zzOp == null) {
            zzafr.zzaT("AdMediaPlayerView setMediaPlayerVolume() called before onPrepared().");
        } else {
            try {
                this.zzOp.setVolume(f, f);
            } catch (IllegalStateException e) {
            }
        }
    }

    private final void zzfE() {
        SurfaceTexture surfaceTextureZzgg;
        zzafr.v("AdMediaPlayerView init MediaPlayer");
        SurfaceTexture surfaceTexture = getSurfaceTexture();
        if (this.zzOq == null || surfaceTexture == null) {
            return;
        }
        zzq(false);
        try {
            zzbs.zzbQ();
            this.zzOp = new MediaPlayer();
            this.zzOp.setOnBufferingUpdateListener(this);
            this.zzOp.setOnCompletionListener(this);
            this.zzOp.setOnErrorListener(this);
            this.zzOp.setOnInfoListener(this);
            this.zzOp.setOnPreparedListener(this);
            this.zzOp.setOnVideoSizeChangedListener(this);
            this.zzOt = 0;
            if (this.zzOx) {
                this.zzOw = new zzap(getContext());
                this.zzOw.zza(surfaceTexture, getWidth(), getHeight());
                this.zzOw.start();
                surfaceTextureZzgg = this.zzOw.zzgg();
                if (surfaceTextureZzgg == null) {
                    this.zzOw.zzgf();
                    this.zzOw = null;
                    surfaceTextureZzgg = surfaceTexture;
                }
            } else {
                surfaceTextureZzgg = surfaceTexture;
            }
            this.zzOp.setDataSource(getContext(), this.zzOq);
            zzbs.zzbR();
            this.zzOp.setSurface(new Surface(surfaceTextureZzgg));
            this.zzOp.setAudioStreamType(3);
            this.zzOp.setScreenOnWhilePlaying(true);
            this.zzOp.prepareAsync();
            zzq(1);
        } catch (IOException | IllegalArgumentException | IllegalStateException e) {
            String strValueOf = String.valueOf(this.zzOq);
            zzafr.zzc(new StringBuilder(String.valueOf(strValueOf).length() + 36).append("Failed to initialize MediaPlayer at ").append(strValueOf).toString(), e);
            onError(this.zzOp, 1, 0);
        }
    }

    private final void zzfF() {
        if (this.zzOm && zzfG() && this.zzOp.getCurrentPosition() > 0 && this.zzOo != 3) {
            zzafr.v("AdMediaPlayerView nudging MediaPlayer");
            zza(0.0f);
            this.zzOp.start();
            int currentPosition = this.zzOp.getCurrentPosition();
            long jCurrentTimeMillis = zzbs.zzbF().currentTimeMillis();
            while (zzfG() && this.zzOp.getCurrentPosition() == currentPosition && zzbs.zzbF().currentTimeMillis() - jCurrentTimeMillis <= 250) {
            }
            this.zzOp.pause();
            zzfH();
        }
    }

    private final boolean zzfG() {
        return (this.zzOp == null || this.zzOn == -1 || this.zzOn == 0 || this.zzOn == 1) ? false : true;
    }

    private final void zzq(int i) {
        if (i == 3) {
            this.zzOl.zzgj();
            this.zzPq.zzgj();
        } else if (this.zzOn == 3) {
            this.zzOl.zzgk();
            this.zzPq.zzgk();
        }
        this.zzOn = i;
    }

    private final void zzq(boolean z) {
        zzafr.v("AdMediaPlayerView release");
        if (this.zzOw != null) {
            this.zzOw.zzgf();
            this.zzOw = null;
        }
        if (this.zzOp != null) {
            this.zzOp.reset();
            this.zzOp.release();
            this.zzOp = null;
            zzq(0);
            if (z) {
                this.zzOo = 0;
                this.zzOo = 0;
            }
        }
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzy
    public final int getCurrentPosition() {
        if (zzfG()) {
            return this.zzOp.getCurrentPosition();
        }
        return 0;
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzy
    public final int getDuration() {
        if (zzfG()) {
            return this.zzOp.getDuration();
        }
        return -1;
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzy
    public final int getVideoHeight() {
        if (this.zzOp != null) {
            return this.zzOp.getVideoHeight();
        }
        return 0;
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzy
    public final int getVideoWidth() {
        if (this.zzOp != null) {
            return this.zzOp.getVideoWidth();
        }
        return 0;
    }

    @Override // android.media.MediaPlayer.OnBufferingUpdateListener
    public final void onBufferingUpdate(MediaPlayer mediaPlayer, int i) {
        this.zzOt = i;
    }

    @Override // android.media.MediaPlayer.OnCompletionListener
    public final void onCompletion(MediaPlayer mediaPlayer) {
        zzafr.v("AdMediaPlayerView completion");
        zzq(5);
        this.zzOo = 5;
        zzagz.zzZr.post(new zzf(this));
    }

    @Override // android.media.MediaPlayer.OnErrorListener
    public final boolean onError(MediaPlayer mediaPlayer, int i, int i2) {
        String str = zzOk.get(Integer.valueOf(i));
        String str2 = zzOk.get(Integer.valueOf(i2));
        zzafr.zzaT(new StringBuilder(String.valueOf(str).length() + 38 + String.valueOf(str2).length()).append("AdMediaPlayerView MediaPlayer error: ").append(str).append(":").append(str2).toString());
        zzq(-1);
        this.zzOo = -1;
        zzagz.zzZr.post(new zzg(this, str, str2));
        return true;
    }

    @Override // android.media.MediaPlayer.OnInfoListener
    public final boolean onInfo(MediaPlayer mediaPlayer, int i, int i2) {
        String str = zzOk.get(Integer.valueOf(i));
        String str2 = zzOk.get(Integer.valueOf(i2));
        zzafr.v(new StringBuilder(String.valueOf(str).length() + 37 + String.valueOf(str2).length()).append("AdMediaPlayerView MediaPlayer info: ").append(str).append(":").append(str2).toString());
        return true;
    }

    @Override // android.view.View
    protected final void onMeasure(int i, int i2) {
        int defaultSize = getDefaultSize(this.zzOr, i);
        int defaultSize2 = getDefaultSize(this.zzOs, i2);
        if (this.zzOr > 0 && this.zzOs > 0 && this.zzOw == null) {
            int mode = View.MeasureSpec.getMode(i);
            int size = View.MeasureSpec.getSize(i);
            int mode2 = View.MeasureSpec.getMode(i2);
            defaultSize2 = View.MeasureSpec.getSize(i2);
            if (mode == 1073741824 && mode2 == 1073741824) {
                if (this.zzOr * defaultSize2 < this.zzOs * size) {
                    defaultSize = (this.zzOr * defaultSize2) / this.zzOs;
                } else if (this.zzOr * defaultSize2 > this.zzOs * size) {
                    defaultSize2 = (this.zzOs * size) / this.zzOr;
                    defaultSize = size;
                } else {
                    defaultSize = size;
                }
            } else if (mode == 1073741824) {
                int i3 = (this.zzOs * size) / this.zzOr;
                if (mode2 != Integer.MIN_VALUE || i3 <= defaultSize2) {
                    defaultSize2 = i3;
                    defaultSize = size;
                } else {
                    defaultSize = size;
                }
            } else if (mode2 == 1073741824) {
                defaultSize = (this.zzOr * defaultSize2) / this.zzOs;
                if (mode == Integer.MIN_VALUE && defaultSize > size) {
                    defaultSize = size;
                }
            } else {
                int i4 = this.zzOr;
                int i5 = this.zzOs;
                if (mode2 != Integer.MIN_VALUE || i5 <= defaultSize2) {
                    defaultSize2 = i5;
                    defaultSize = i4;
                } else {
                    defaultSize = (this.zzOr * defaultSize2) / this.zzOs;
                }
                if (mode == Integer.MIN_VALUE && defaultSize > size) {
                    defaultSize2 = (this.zzOs * size) / this.zzOr;
                    defaultSize = size;
                }
            }
        }
        setMeasuredDimension(defaultSize, defaultSize2);
        if (this.zzOw != null) {
            this.zzOw.zzf(defaultSize, defaultSize2);
        }
        if (Build.VERSION.SDK_INT == 16) {
            if ((this.zzOu > 0 && this.zzOu != defaultSize) || (this.zzOv > 0 && this.zzOv != defaultSize2)) {
                zzfF();
            }
            this.zzOu = defaultSize;
            this.zzOv = defaultSize2;
        }
    }

    @Override // android.media.MediaPlayer.OnPreparedListener
    public final void onPrepared(MediaPlayer mediaPlayer) {
        zzafr.v("AdMediaPlayerView prepared");
        zzq(2);
        this.zzOl.zzfT();
        zzagz.zzZr.post(new zze(this));
        this.zzOr = mediaPlayer.getVideoWidth();
        this.zzOs = mediaPlayer.getVideoHeight();
        if (this.zzOy != 0) {
            seekTo(this.zzOy);
        }
        zzfF();
        int i = this.zzOr;
        zzafr.zzaS(new StringBuilder(62).append("AdMediaPlayerView stream dimensions: ").append(i).append(" x ").append(this.zzOs).toString());
        if (this.zzOo == 3) {
            play();
        }
        zzfH();
    }

    @Override // android.view.TextureView.SurfaceTextureListener
    public final void onSurfaceTextureAvailable(SurfaceTexture surfaceTexture, int i, int i2) {
        zzafr.v("AdMediaPlayerView surface created");
        zzfE();
        zzagz.zzZr.post(new zzh(this));
    }

    @Override // android.view.TextureView.SurfaceTextureListener
    public final boolean onSurfaceTextureDestroyed(SurfaceTexture surfaceTexture) {
        zzafr.v("AdMediaPlayerView surface destroyed");
        if (this.zzOp != null && this.zzOy == 0) {
            this.zzOy = this.zzOp.getCurrentPosition();
        }
        if (this.zzOw != null) {
            this.zzOw.zzgf();
        }
        zzagz.zzZr.post(new zzj(this));
        zzq(true);
        return true;
    }

    @Override // android.view.TextureView.SurfaceTextureListener
    public final void onSurfaceTextureSizeChanged(SurfaceTexture surfaceTexture, int i, int i2) {
        zzafr.v("AdMediaPlayerView surface changed");
        boolean z = this.zzOo == 3;
        boolean z2 = this.zzOr == i && this.zzOs == i2;
        if (this.zzOp != null && z && z2) {
            if (this.zzOy != 0) {
                seekTo(this.zzOy);
            }
            play();
        }
        if (this.zzOw != null) {
            this.zzOw.zzf(i, i2);
        }
        zzagz.zzZr.post(new zzi(this, i, i2));
    }

    @Override // android.view.TextureView.SurfaceTextureListener
    public final void onSurfaceTextureUpdated(SurfaceTexture surfaceTexture) {
        this.zzOl.zzb(this);
        this.zzPp.zza(surfaceTexture, this.zzOz);
    }

    @Override // android.media.MediaPlayer.OnVideoSizeChangedListener
    public final void onVideoSizeChanged(MediaPlayer mediaPlayer, int i, int i2) {
        zzafr.v(new StringBuilder(57).append("AdMediaPlayerView size changed: ").append(i).append(" x ").append(i2).toString());
        this.zzOr = mediaPlayer.getVideoWidth();
        this.zzOs = mediaPlayer.getVideoHeight();
        if (this.zzOr == 0 || this.zzOs == 0) {
            return;
        }
        requestLayout();
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzy
    public final void pause() {
        zzafr.v("AdMediaPlayerView pause");
        if (zzfG() && this.zzOp.isPlaying()) {
            this.zzOp.pause();
            zzq(4);
            zzagz.zzZr.post(new zzl(this));
        }
        this.zzOo = 4;
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzy
    public final void play() {
        zzafr.v("AdMediaPlayerView play");
        if (zzfG()) {
            this.zzOp.start();
            zzq(3);
            this.zzPp.zzfU();
            zzagz.zzZr.post(new zzk(this));
        }
        this.zzOo = 3;
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzy
    public final void seekTo(int i) {
        zzafr.v(new StringBuilder(34).append("AdMediaPlayerView seek ").append(i).toString());
        if (!zzfG()) {
            this.zzOy = i;
        } else {
            this.zzOp.seekTo(i);
            this.zzOy = 0;
        }
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzy
    public final void setVideoPath(String str) {
        Uri uri = Uri.parse(str);
        zzia zziaVarZze = zzia.zze(uri);
        if (zziaVarZze != null) {
            uri = Uri.parse(zziaVarZze.url);
        }
        this.zzOq = uri;
        this.zzOy = 0;
        zzfE();
        requestLayout();
        invalidate();
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzy
    public final void stop() {
        zzafr.v("AdMediaPlayerView stop");
        if (this.zzOp != null) {
            this.zzOp.stop();
            this.zzOp.release();
            this.zzOp = null;
            zzq(0);
            this.zzOo = 0;
        }
        this.zzOl.onStop();
    }

    @Override // android.view.View
    public final String toString() {
        String strValueOf = String.valueOf(getClass().getName());
        String strValueOf2 = String.valueOf(Integer.toHexString(hashCode()));
        return new StringBuilder(String.valueOf(strValueOf).length() + 1 + String.valueOf(strValueOf2).length()).append(strValueOf).append("@").append(strValueOf2).toString();
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzy
    public final void zza(float f, float f2) {
        if (this.zzOw != null) {
            this.zzOw.zzb(f, f2);
        }
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzy
    public final void zza(zzx zzxVar) {
        this.zzOz = zzxVar;
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzy
    public final String zzfD() {
        String strValueOf = String.valueOf(this.zzOx ? " spherical" : "");
        return strValueOf.length() != 0 ? "MediaPlayer".concat(strValueOf) : new String("MediaPlayer");
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzy, com.google.android.gms.ads.internal.overlay.zzau
    public final void zzfH() {
        zza(this.zzPq.zzgm());
    }
}
