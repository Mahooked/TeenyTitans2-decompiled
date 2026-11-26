package com.google.android.gms.ads.internal.js;

import android.content.Context;
import android.support.annotation.Keep;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zzajg;
import com.google.android.gms.internal.zzajm;
import com.google.android.gms.internal.zzcu;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class JavascriptEngineFactory {

    static class JSEngineSettableFuture extends zzajg<zza> {

        @Keep
        zza mEngineReference;

        private JSEngineSettableFuture() {
        }

        /* synthetic */ JSEngineSettableFuture(zzc zzcVar) {
            this();
        }
    }

    public final zzajm<zza> zza(Context context, zzaje zzajeVar, String str, zzcu zzcuVar, com.google.android.gms.ads.internal.zzv zzvVar) {
        JSEngineSettableFuture jSEngineSettableFuture = new JSEngineSettableFuture(null);
        zzagz.zzZr.post(new zzc(this, context, zzajeVar, zzcuVar, zzvVar, jSEngineSettableFuture, str));
        return jSEngineSettableFuture;
    }
}
