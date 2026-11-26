package com.google.android.gms.ads.internal;

import android.os.AsyncTask;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzeu;
import com.google.android.gms.internal.zzmo;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;

/* JADX INFO: loaded from: classes.dex */
final class zzbq extends AsyncTask<Void, Void, String> {
    private /* synthetic */ zzbm zzvf;

    private zzbq(zzbm zzbmVar) {
        this.zzvf = zzbmVar;
    }

    /* synthetic */ zzbq(zzbm zzbmVar, zzbn zzbnVar) {
        this(zzbmVar);
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Override // android.os.AsyncTask
    /* JADX INFO: renamed from: zza, reason: merged with bridge method [inline-methods] */
    public final String doInBackground(Void... voidArr) {
        try {
            this.zzvf.zzvd = (zzeu) this.zzvf.zzva.get(((Long) zzbs.zzbL().zzd(zzmo.zzFX)).longValue(), TimeUnit.MILLISECONDS);
        } catch (InterruptedException e) {
            e = e;
            zzafr.zzc("Failed to load ad data", e);
        } catch (ExecutionException e2) {
            e = e2;
            zzafr.zzc("Failed to load ad data", e);
        } catch (TimeoutException e3) {
            zzafr.zzaT("Timed out waiting for ad data");
        }
        return this.zzvf.zzbp();
    }

    @Override // android.os.AsyncTask
    protected final /* synthetic */ void onPostExecute(String str) {
        String str2 = str;
        if (this.zzvf.zzvc == null || str2 == null) {
            return;
        }
        this.zzvf.zzvc.loadUrl(str2);
    }
}
