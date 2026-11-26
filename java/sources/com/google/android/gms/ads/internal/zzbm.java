package com.google.android.gms.ads.internal;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.RemoteException;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import android.view.ViewGroup;
import android.webkit.WebView;
import com.google.android.gms.actions.SearchIntents;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.internal.zzadd;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagt;
import com.google.android.gms.internal.zzaiy;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zzeu;
import com.google.android.gms.internal.zzev;
import com.google.android.gms.internal.zzir;
import com.google.android.gms.internal.zziv;
import com.google.android.gms.internal.zzji;
import com.google.android.gms.internal.zzjl;
import com.google.android.gms.internal.zzjo;
import com.google.android.gms.internal.zzka;
import com.google.android.gms.internal.zzke;
import com.google.android.gms.internal.zzkk;
import com.google.android.gms.internal.zzks;
import com.google.android.gms.internal.zzky;
import com.google.android.gms.internal.zzlx;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zznh;
import com.google.android.gms.internal.zzxg;
import com.google.android.gms.internal.zzxo;
import com.google.android.gms.internal.zzzn;
import java.util.Map;
import java.util.concurrent.Future;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzbm extends zzka {
    private final Context mContext;

    @Nullable
    private zzjo zztK;
    private final zzaje zztW;
    private final zziv zzuZ;
    private final Future<zzeu> zzva = zzagt.zza(new zzbp(this));
    private final zzbr zzvb;

    @Nullable
    private WebView zzvc;

    @Nullable
    private zzeu zzvd;
    private AsyncTask<Void, Void, String> zzve;

    public zzbm(Context context, zziv zzivVar, String str, zzaje zzajeVar) {
        this.mContext = context;
        this.zztW = zzajeVar;
        this.zzuZ = zzivVar;
        this.zzvc = new WebView(this.mContext);
        this.zzvb = new zzbr(str);
        zzf(0);
        this.zzvc.setVerticalScrollBarEnabled(false);
        this.zzvc.getSettings().setJavaScriptEnabled(true);
        this.zzvc.setWebViewClient(new zzbn(this));
        this.zzvc.setOnTouchListener(new zzbo(this));
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final String zzw(String str) {
        if (this.zzvd == null) {
            return str;
        }
        Uri uriZzc = Uri.parse(str);
        try {
            uriZzc = this.zzvd.zzc(uriZzc, this.mContext);
        } catch (RemoteException e) {
            zzafr.zzc("Unable to process ad data", e);
        } catch (zzev e2) {
            zzafr.zzc("Unable to parse ad click url", e2);
        }
        return uriZzc.toString();
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzx(String str) {
        Intent intent = new Intent("android.intent.action.VIEW");
        intent.setData(Uri.parse(str));
        this.mContext.startActivity(intent);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void destroy() throws RemoteException {
        com.google.android.gms.common.internal.zzbo.zzcz("destroy must be called on the main UI thread.");
        this.zzve.cancel(true);
        this.zzva.cancel(true);
        this.zzvc.destroy();
        this.zzvc = null;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final String getAdUnitId() {
        throw new IllegalStateException("getAdUnitId not implemented");
    }

    @Override // com.google.android.gms.internal.zzjz
    @Nullable
    public final String getMediationAdapterClassName() throws RemoteException {
        return null;
    }

    @Override // com.google.android.gms.internal.zzjz
    @Nullable
    public final zzks getVideoController() {
        return null;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final boolean isLoading() throws RemoteException {
        return false;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final boolean isReady() throws RemoteException {
        return false;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void pause() throws RemoteException {
        com.google.android.gms.common.internal.zzbo.zzcz("pause must be called on the main UI thread.");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void resume() throws RemoteException {
        com.google.android.gms.common.internal.zzbo.zzcz("resume must be called on the main UI thread.");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void setImmersiveMode(boolean z) {
        throw new IllegalStateException("Unused method");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void setManualImpressionsEnabled(boolean z) throws RemoteException {
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void setUserId(String str) throws RemoteException {
        throw new IllegalStateException("Unused method");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void showInterstitial() throws RemoteException {
        throw new IllegalStateException("Unused method");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void stopLoading() throws RemoteException {
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzadd zzaddVar) throws RemoteException {
        throw new IllegalStateException("Unused method");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zziv zzivVar) throws RemoteException {
        throw new IllegalStateException("AdSize must be set before initialization");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzjl zzjlVar) throws RemoteException {
        throw new IllegalStateException("Unused method");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzjo zzjoVar) throws RemoteException {
        this.zztK = zzjoVar;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzke zzkeVar) throws RemoteException {
        throw new IllegalStateException("Unused method");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzkk zzkkVar) throws RemoteException {
        throw new IllegalStateException("Unused method");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzky zzkyVar) {
        throw new IllegalStateException("Unused method");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzlx zzlxVar) {
        throw new IllegalStateException("Unused method");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zznh zznhVar) throws RemoteException {
        throw new IllegalStateException("Unused method");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzxg zzxgVar) throws RemoteException {
        throw new IllegalStateException("Unused method");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zza(zzxo zzxoVar, String str) throws RemoteException {
        throw new IllegalStateException("Unused method");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final boolean zza(zzir zzirVar) throws RemoteException {
        com.google.android.gms.common.internal.zzbo.zzb(this.zzvc, "This Search Ad has already been torn down");
        this.zzvb.zza(zzirVar, this.zztW);
        this.zzve = new zzbq(this, null).execute(new Void[0]);
        return true;
    }

    @Override // com.google.android.gms.internal.zzjz
    @Nullable
    public final String zzaI() throws RemoteException {
        return null;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final IObjectWrapper zzal() throws RemoteException {
        com.google.android.gms.common.internal.zzbo.zzcz("getAdFrame must be called on the main UI thread.");
        return com.google.android.gms.dynamic.zzn.zzw(this.zzvc);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final zziv zzam() throws RemoteException {
        return this.zzuZ;
    }

    @Override // com.google.android.gms.internal.zzjz
    public final void zzao() throws RemoteException {
        throw new IllegalStateException("Unused method");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final zzke zzax() {
        throw new IllegalStateException("getIAppEventListener not implemented");
    }

    @Override // com.google.android.gms.internal.zzjz
    public final zzjo zzay() {
        throw new IllegalStateException("getIAdListener not implemented");
    }

    final String zzbp() {
        Uri uriZzb;
        Uri.Builder builder = new Uri.Builder();
        builder.scheme("https://").appendEncodedPath((String) zzbs.zzbL().zzd(zzmo.zzFV));
        builder.appendQueryParameter(SearchIntents.EXTRA_QUERY, this.zzvb.getQuery());
        builder.appendQueryParameter("pubId", this.zzvb.zzbs());
        Map<String, String> mapZzbt = this.zzvb.zzbt();
        for (String str : mapZzbt.keySet()) {
            builder.appendQueryParameter(str, mapZzbt.get(str));
        }
        Uri uriBuild = builder.build();
        if (this.zzvd != null) {
            try {
                uriZzb = this.zzvd.zzb(uriBuild, this.mContext);
            } catch (RemoteException | zzev e) {
                zzafr.zzc("Unable to process ad data", e);
                uriZzb = uriBuild;
            }
        } else {
            uriZzb = uriBuild;
        }
        String strValueOf = String.valueOf(zzbq());
        String strValueOf2 = String.valueOf(uriZzb.getEncodedQuery());
        return new StringBuilder(String.valueOf(strValueOf).length() + 1 + String.valueOf(strValueOf2).length()).append(strValueOf).append("#").append(strValueOf2).toString();
    }

    final String zzbq() {
        String strZzbr = this.zzvb.zzbr();
        String str = TextUtils.isEmpty(strZzbr) ? "www.google.com" : strZzbr;
        String strValueOf = String.valueOf("https://");
        String str2 = (String) zzbs.zzbL().zzd(zzmo.zzFV);
        return new StringBuilder(String.valueOf(strValueOf).length() + String.valueOf(str).length() + String.valueOf(str2).length()).append(strValueOf).append(str).append(str2).toString();
    }

    final void zzf(int i) {
        if (this.zzvc == null) {
            return;
        }
        this.zzvc.setLayoutParams(new ViewGroup.LayoutParams(-1, i));
    }

    final int zzv(String str) {
        String queryParameter = Uri.parse(str).getQueryParameter("height");
        if (TextUtils.isEmpty(queryParameter)) {
            return 0;
        }
        try {
            zzji.zzds();
            return zzaiy.zzc(this.mContext, Integer.parseInt(queryParameter));
        } catch (NumberFormatException e) {
            return 0;
        }
    }
}
