package com.google.android.gms.ads.internal.overlay;

import android.annotation.TargetApi;
import android.content.Context;
import android.media.AudioManager;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@zzzn
@TargetApi(14)
public final class zzat implements AudioManager.OnAudioFocusChangeListener {
    private final AudioManager mAudioManager;
    private boolean zzPw;
    private final zzau zzQL;
    private boolean zzQM;
    private boolean zzQN;
    private float zzQO = 1.0f;

    public zzat(Context context, zzau zzauVar) {
        this.mAudioManager = (AudioManager) context.getSystemService("audio");
        this.zzQL = zzauVar;
    }

    private final void zzgn() {
        boolean z = this.zzPw && !this.zzQN && this.zzQO > 0.0f;
        if (z && !this.zzQM) {
            if (this.mAudioManager != null && !this.zzQM) {
                this.zzQM = this.mAudioManager.requestAudioFocus(this, 3, 2) == 1;
            }
            this.zzQL.zzfH();
            return;
        }
        if (z || !this.zzQM) {
            return;
        }
        if (this.mAudioManager != null && this.zzQM) {
            this.zzQM = this.mAudioManager.abandonAudioFocus(this) == 0;
        }
        this.zzQL.zzfH();
    }

    @Override // android.media.AudioManager.OnAudioFocusChangeListener
    public final void onAudioFocusChange(int i) {
        this.zzQM = i > 0;
        this.zzQL.zzfH();
    }

    public final void setMuted(boolean z) {
        this.zzQN = z;
        zzgn();
    }

    public final void zzb(float f) {
        this.zzQO = f;
        zzgn();
    }

    public final void zzgj() {
        this.zzPw = true;
        zzgn();
    }

    public final void zzgk() {
        this.zzPw = false;
        zzgn();
    }

    public final float zzgm() {
        float f = this.zzQN ? 0.0f : this.zzQO;
        if (this.zzQM) {
            return f;
        }
        return 0.0f;
    }
}
