package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import java.util.HashMap;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaks extends zzkt {
    private final zzaka zzJH;
    private float zzacA;
    private final boolean zzact;
    private final float zzacu;
    private int zzacv;
    private zzkv zzacw;
    private boolean zzacx;
    private float zzacz;
    private boolean zzsg;
    private final Object mLock = new Object();
    private boolean zzacy = true;
    private boolean zzsf = true;

    public zzaks(zzaka zzakaVar, float f, boolean z) {
        this.zzJH = zzakaVar;
        this.zzacu = f;
        this.zzact = z;
    }

    private final void zzc(String str, @Nullable Map<String, String> map) {
        HashMap map2 = map == null ? new HashMap() : new HashMap(map);
        map2.put("action", str);
        com.google.android.gms.ads.internal.zzbs.zzbz();
        zzagz.runOnUiThread(new zzakt(this, map2));
    }

    @Override // com.google.android.gms.internal.zzks
    public final float getAspectRatio() {
        float f;
        synchronized (this.mLock) {
            f = this.zzacA;
        }
        return f;
    }

    @Override // com.google.android.gms.internal.zzks
    public final int getPlaybackState() {
        int i;
        synchronized (this.mLock) {
            i = this.zzacv;
        }
        return i;
    }

    @Override // com.google.android.gms.internal.zzks
    public final boolean isCustomControlsEnabled() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzact && this.zzsg;
        }
        return z;
    }

    @Override // com.google.android.gms.internal.zzks
    public final boolean isMuted() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzacy;
        }
        return z;
    }

    @Override // com.google.android.gms.internal.zzks
    public final void mute(boolean z) {
        zzc(z ? "mute" : "unmute", null);
    }

    @Override // com.google.android.gms.internal.zzks
    public final void pause() {
        zzc("pause", null);
    }

    @Override // com.google.android.gms.internal.zzks
    public final void play() {
        zzc("play", null);
    }

    public final void zza(float f, int i, boolean z, float f2) {
        boolean z2;
        int i2;
        synchronized (this.mLock) {
            this.zzacz = f;
            z2 = this.zzacy;
            this.zzacy = z;
            i2 = this.zzacv;
            this.zzacv = i;
            this.zzacA = f2;
        }
        com.google.android.gms.ads.internal.zzbs.zzbz();
        zzagz.runOnUiThread(new zzaku(this, i2, i, z2, z));
    }

    @Override // com.google.android.gms.internal.zzks
    public final void zza(zzkv zzkvVar) {
        synchronized (this.mLock) {
            this.zzacw = zzkvVar;
        }
    }

    public final void zzb(zzlx zzlxVar) {
        synchronized (this.mLock) {
            this.zzsf = zzlxVar.zzBJ;
            this.zzsg = zzlxVar.zzBK;
        }
        zzc("initialState", com.google.android.gms.common.util.zzf.zza("muteStart", zzlxVar.zzBJ ? "1" : "0", "customControlsRequested", zzlxVar.zzBK ? "1" : "0"));
    }

    @Override // com.google.android.gms.internal.zzks
    public final float zzdv() {
        return this.zzacu;
    }

    @Override // com.google.android.gms.internal.zzks
    public final float zzdw() {
        float f;
        synchronized (this.mLock) {
            f = this.zzacz;
        }
        return f;
    }
}
