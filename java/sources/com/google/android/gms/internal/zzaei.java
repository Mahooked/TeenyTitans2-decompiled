package com.google.android.gms.internal;

import java.util.Map;
import java.util.concurrent.ExecutionException;
import org.json.JSONException;

/* JADX INFO: loaded from: classes.dex */
final class zzaei implements Runnable {
    private /* synthetic */ zzaeg zzXf;
    private /* synthetic */ zzajm zzXg;

    zzaei(zzaeg zzaegVar, zzajm zzajmVar) {
        this.zzXf = zzaegVar;
        this.zzXg = zzajmVar;
    }

    /* JADX WARN: Code duplicated, block: B:17:0x0048  */
    /* JADX WARN: Code duplicated, block: B:25:? A[RETURN, SYNTHETIC] */
    @Override // java.lang.Runnable
    public final void run() {
        Throwable th;
        try {
            this.zzXf.zzi((Map) this.zzXg.get());
            if (this.zzXf.zzXa) {
                synchronized (this.zzXf.mLock) {
                    this.zzXf.zzWX.zzcsJ = 9;
                }
            }
            this.zzXf.send();
        } catch (InterruptedException e) {
            th = e;
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGe)).booleanValue()) {
                zzafr.zza("Failed to get SafeBrowsing metadata", th);
            }
        } catch (ExecutionException e2) {
            th = e2;
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGe)).booleanValue()) {
                zzafr.zza("Failed to get SafeBrowsing metadata", th);
            }
        } catch (JSONException e3) {
            th = e3;
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGe)).booleanValue()) {
                zzafr.zza("Failed to get SafeBrowsing metadata", th);
            }
        }
    }
}
