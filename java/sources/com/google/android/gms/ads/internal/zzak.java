package com.google.android.gms.ads.internal;

import android.content.Context;
import android.support.v4.util.SimpleArrayMap;
import android.text.TextUtils;
import com.google.android.gms.ads.formats.PublisherAdViewOptions;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zziv;
import com.google.android.gms.internal.zzjo;
import com.google.android.gms.internal.zzjr;
import com.google.android.gms.internal.zzjv;
import com.google.android.gms.internal.zzkk;
import com.google.android.gms.internal.zzon;
import com.google.android.gms.internal.zzpn;
import com.google.android.gms.internal.zzpq;
import com.google.android.gms.internal.zzpt;
import com.google.android.gms.internal.zzpw;
import com.google.android.gms.internal.zzpz;
import com.google.android.gms.internal.zzuq;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzak extends zzjv {
    private final Context mContext;
    private final zzv zzsS;
    private final zzuq zzsX;
    private zzjo zztK;
    private zziv zztO;
    private PublisherAdViewOptions zztP;
    private zzon zztS;
    private zzkk zztU;
    private final String zztV;
    private final zzaje zztW;
    private zzpn zzua;
    private zzpq zzub;
    private zzpz zzue;
    private SimpleArrayMap<String, zzpw> zzud = new SimpleArrayMap<>();
    private SimpleArrayMap<String, zzpt> zzuc = new SimpleArrayMap<>();

    public zzak(Context context, String str, zzuq zzuqVar, zzaje zzajeVar, zzv zzvVar) {
        this.mContext = context;
        this.zztV = str;
        this.zzsX = zzuqVar;
        this.zztW = zzajeVar;
        this.zzsS = zzvVar;
    }

    @Override // com.google.android.gms.internal.zzju
    public final void zza(PublisherAdViewOptions publisherAdViewOptions) {
        this.zztP = publisherAdViewOptions;
    }

    @Override // com.google.android.gms.internal.zzju
    public final void zza(zzon zzonVar) {
        this.zztS = zzonVar;
    }

    @Override // com.google.android.gms.internal.zzju
    public final void zza(zzpn zzpnVar) {
        this.zzua = zzpnVar;
    }

    @Override // com.google.android.gms.internal.zzju
    public final void zza(zzpq zzpqVar) {
        this.zzub = zzpqVar;
    }

    @Override // com.google.android.gms.internal.zzju
    public final void zza(zzpz zzpzVar, zziv zzivVar) {
        this.zzue = zzpzVar;
        this.zztO = zzivVar;
    }

    @Override // com.google.android.gms.internal.zzju
    public final void zza(String str, zzpw zzpwVar, zzpt zzptVar) {
        if (TextUtils.isEmpty(str)) {
            throw new IllegalArgumentException("Custom template ID for native custom template ad is empty. Please provide a valid template id.");
        }
        this.zzud.put(str, zzpwVar);
        this.zzuc.put(str, zzptVar);
    }

    @Override // com.google.android.gms.internal.zzju
    public final zzjr zzaZ() {
        return new zzai(this.mContext, this.zztV, this.zzsX, this.zztW, this.zztK, this.zzua, this.zzub, this.zzud, this.zzuc, this.zztS, this.zztU, this.zzsS, this.zzue, this.zztO, this.zztP);
    }

    @Override // com.google.android.gms.internal.zzju
    public final void zzb(zzjo zzjoVar) {
        this.zztK = zzjoVar;
    }

    @Override // com.google.android.gms.internal.zzju
    public final void zzb(zzkk zzkkVar) {
        this.zztU = zzkkVar;
    }
}
