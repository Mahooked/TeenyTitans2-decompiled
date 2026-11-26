package com.google.android.gms.internal;

import android.content.Context;
import android.support.annotation.Nullable;
import com.google.android.gms.ads.internal.js.JavascriptEngineFactory;
import java.util.concurrent.CancellationException;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzyh {
    private final Context mContext;
    private final Object mLock;
    private final zzcu zzIc;
    private final com.google.android.gms.ads.internal.zzbb zzRm;
    private JavascriptEngineFactory zzRn;
    private com.google.android.gms.ads.internal.js.zzah zzRo;
    private zzajm<com.google.android.gms.ads.internal.js.zza> zzRp;
    private boolean zzRq;
    private boolean zzRr;
    private final zzaje zzuK;
    private static final long zzRj = TimeUnit.SECONDS.toMillis(60);
    private static final Object zzuF = new Object();
    private static boolean zzRk = false;
    private static com.google.android.gms.ads.internal.js.zzl zzRl = null;

    public zzyh(Context context, com.google.android.gms.ads.internal.zzbb zzbbVar, zzcu zzcuVar, zzaje zzajeVar) {
        this.mLock = new Object();
        this.zzRq = false;
        this.zzRr = false;
        this.mContext = context;
        this.zzRm = zzbbVar;
        this.zzIc = zzcuVar;
        this.zzuK = zzajeVar;
        this.zzRq = ((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFt)).booleanValue();
    }

    public zzyh(Context context, zzafg zzafgVar, com.google.android.gms.ads.internal.zzbb zzbbVar, zzcu zzcuVar) {
        this(context, zzbbVar, zzcuVar, (zzafgVar == null || zzafgVar.zzUj == null) ? null : zzafgVar.zzUj.zzvT);
    }

    @Nullable
    private final com.google.android.gms.ads.internal.js.zza zzgu() throws ExecutionException, CancellationException, InterruptedException, TimeoutException {
        com.google.android.gms.ads.internal.js.zza zzaVar = null;
        if (this.zzRp != null) {
            zzaVar = this.zzRp.get(zzRj, TimeUnit.MILLISECONDS);
            synchronized (this.mLock) {
                if (!this.zzRr) {
                    zzaVar.zza(this.zzRm, this.zzRm, this.zzRm, this.zzRm, false, null, null, null);
                    this.zzRr = true;
                }
            }
        }
        return zzaVar;
    }

    public final void zza(zzym zzymVar) {
        if (this.zzRq) {
            com.google.android.gms.ads.internal.js.zzah zzahVar = this.zzRo;
            if (zzahVar == null) {
                zzafr.zzaT("SharedJavascriptEngine not initialized");
                return;
            } else {
                zzahVar.zza(new zzyi(this, zzymVar), new zzyj(this, zzymVar));
                return;
            }
        }
        try {
            com.google.android.gms.ads.internal.js.zza zzaVarZzgu = zzgu();
            if (zzaVarZzgu == null) {
                zzafr.zzaT("JavascriptEngine not initialized");
            } else {
                zzymVar.zzd(zzaVarZzgu);
            }
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
            zzafr.zzc("Exception occurred during execution", e);
        } catch (CancellationException e2) {
            e = e2;
            zzafr.zzc("Exception occurred during execution", e);
        } catch (ExecutionException e3) {
            e = e3;
            zzafr.zzc("Exception occurred during execution", e);
        } catch (TimeoutException e4) {
            e = e4;
            zzafr.zzc("Exception occurred during execution", e);
        }
    }

    public final void zzfd() {
        if (this.zzRq) {
            return;
        }
        try {
            com.google.android.gms.ads.internal.js.zza zzaVarZzgu = zzgu();
            if (zzaVarZzgu != null) {
                com.google.android.gms.ads.internal.zzbs.zzbz();
                zzagz.runOnUiThread(new zzyl(this, zzaVarZzgu));
            }
        } catch (InterruptedException | CancellationException | ExecutionException | TimeoutException e) {
            zzafr.zzc("Exception occurred while destroying engine", e);
        }
    }

    public final void zzgs() {
        if (!this.zzRq) {
            this.zzRn = new JavascriptEngineFactory();
            return;
        }
        synchronized (zzuF) {
            if (!zzRk) {
                zzRl = new com.google.android.gms.ads.internal.js.zzl(this.mContext.getApplicationContext() != null ? this.mContext.getApplicationContext() : this.mContext, this.zzuK, (String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFq), new zzyk(this), new com.google.android.gms.ads.internal.js.zzx());
                zzRk = true;
            }
        }
    }

    public final void zzgt() throws zzakm {
        if (this.zzRq) {
            this.zzRo = new com.google.android.gms.ads.internal.js.zzah(zzRl.zzb(this.zzIc));
            return;
        }
        this.zzRp = this.zzRn.zza(this.mContext, this.zzuK, (String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFq), this.zzIc, this.zzRm.zzak());
    }
}
