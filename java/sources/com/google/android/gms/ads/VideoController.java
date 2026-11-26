package com.google.android.gms.ads;

import android.os.RemoteException;
import android.support.annotation.Nullable;
import com.google.android.gms.common.annotation.KeepForSdk;
import com.google.android.gms.common.internal.zzbo;
import com.google.android.gms.internal.zzajc;
import com.google.android.gms.internal.zzks;
import com.google.android.gms.internal.zzlw;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class VideoController {

    @KeepForSdk
    public static final int PLAYBACK_STATE_ENDED = 3;

    @KeepForSdk
    public static final int PLAYBACK_STATE_PAUSED = 2;

    @KeepForSdk
    public static final int PLAYBACK_STATE_PLAYING = 1;

    @KeepForSdk
    public static final int PLAYBACK_STATE_READY = 5;

    @KeepForSdk
    public static final int PLAYBACK_STATE_UNKNOWN = 0;
    private final Object mLock = new Object();

    @Nullable
    private zzks zzsd;

    @Nullable
    private VideoLifecycleCallbacks zzse;

    public static abstract class VideoLifecycleCallbacks {
        public void onVideoEnd() {
        }

        public void onVideoMute(boolean z) {
        }

        public void onVideoPause() {
        }

        public void onVideoPlay() {
        }

        public void onVideoStart() {
        }
    }

    public final float getAspectRatio() {
        float aspectRatio = 0.0f;
        synchronized (this.mLock) {
            try {
                if (this.zzsd != null) {
                    try {
                        aspectRatio = this.zzsd.getAspectRatio();
                    } catch (RemoteException e) {
                        zzajc.zzb("Unable to call getAspectRatio on video controller.", e);
                    }
                }
            } catch (Throwable th) {
                throw th;
            }
        }
        return aspectRatio;
    }

    @KeepForSdk
    public final int getPlaybackState() {
        int playbackState = 0;
        synchronized (this.mLock) {
            try {
                if (this.zzsd != null) {
                    try {
                        playbackState = this.zzsd.getPlaybackState();
                    } catch (RemoteException e) {
                        zzajc.zzb("Unable to call getPlaybackState on video controller.", e);
                    }
                }
            } catch (Throwable th) {
                throw th;
            }
        }
        return playbackState;
    }

    @Nullable
    public final VideoLifecycleCallbacks getVideoLifecycleCallbacks() {
        VideoLifecycleCallbacks videoLifecycleCallbacks;
        synchronized (this.mLock) {
            videoLifecycleCallbacks = this.zzse;
        }
        return videoLifecycleCallbacks;
    }

    public final boolean hasVideoContent() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzsd != null;
        }
        return z;
    }

    @KeepForSdk
    public final boolean isCustomControlsEnabled() {
        boolean zIsCustomControlsEnabled = false;
        synchronized (this.mLock) {
            try {
                if (this.zzsd != null) {
                    try {
                        zIsCustomControlsEnabled = this.zzsd.isCustomControlsEnabled();
                    } catch (RemoteException e) {
                        zzajc.zzb("Unable to call isUsingCustomPlayerControls.", e);
                    }
                }
            } catch (Throwable th) {
                throw th;
            }
        }
        return zIsCustomControlsEnabled;
    }

    @KeepForSdk
    public final boolean isMuted() {
        boolean zIsMuted = true;
        synchronized (this.mLock) {
            try {
                if (this.zzsd != null) {
                    try {
                        zIsMuted = this.zzsd.isMuted();
                    } catch (RemoteException e) {
                        zzajc.zzb("Unable to call isMuted on video controller.", e);
                    }
                }
            } catch (Throwable th) {
                throw th;
            }
        }
        return zIsMuted;
    }

    @KeepForSdk
    public final void mute(boolean z) {
        synchronized (this.mLock) {
            if (this.zzsd == null) {
                return;
            }
            try {
                this.zzsd.mute(z);
            } catch (RemoteException e) {
                zzajc.zzb("Unable to call mute on video controller.", e);
            }
        }
    }

    @KeepForSdk
    public final void pause() {
        synchronized (this.mLock) {
            if (this.zzsd == null) {
                return;
            }
            try {
                this.zzsd.pause();
            } catch (RemoteException e) {
                zzajc.zzb("Unable to call pause on video controller.", e);
            }
        }
    }

    @KeepForSdk
    public final void play() {
        synchronized (this.mLock) {
            if (this.zzsd == null) {
                return;
            }
            try {
                this.zzsd.play();
            } catch (RemoteException e) {
                zzajc.zzb("Unable to call play on video controller.", e);
            }
        }
    }

    public final void setVideoLifecycleCallbacks(VideoLifecycleCallbacks videoLifecycleCallbacks) {
        zzbo.zzb(videoLifecycleCallbacks, "VideoLifecycleCallbacks may not be null.");
        synchronized (this.mLock) {
            this.zzse = videoLifecycleCallbacks;
            if (this.zzsd == null) {
                return;
            }
            try {
                this.zzsd.zza(new zzlw(videoLifecycleCallbacks));
            } catch (RemoteException e) {
                zzajc.zzb("Unable to call setVideoLifecycleCallbacks on video controller.", e);
            }
        }
    }

    public final void zza(zzks zzksVar) {
        synchronized (this.mLock) {
            this.zzsd = zzksVar;
            if (this.zzse != null) {
                setVideoLifecycleCallbacks(this.zzse);
            }
        }
    }

    public final zzks zzae() {
        zzks zzksVar;
        synchronized (this.mLock) {
            zzksVar = this.zzsd;
        }
        return zzksVar;
    }
}
