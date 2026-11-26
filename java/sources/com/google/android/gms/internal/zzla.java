package com.google.android.gms.internal;

import android.content.Context;
import android.location.Location;
import android.os.Bundle;
import com.google.android.gms.ads.mediation.MediationAdapter;
import com.google.android.gms.ads.mediation.NetworkExtras;
import com.google.android.gms.ads.mediation.customevent.CustomEvent;
import com.google.android.gms.ads.search.SearchAdRequest;
import java.util.Collections;
import java.util.Date;
import java.util.Map;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzla {
    private final Bundle zzAS;
    private final Map<Class<? extends NetworkExtras>, NetworkExtras> zzAT;
    private final SearchAdRequest zzAU;
    private final Set<String> zzAV;
    private final Set<String> zzAW;
    private final int zzAe;
    private final int zzAh;
    private final String zzAi;
    private final String zzAk;
    private final Bundle zzAm;
    private final String zzAo;
    private final boolean zzAq;
    private final Date zzda;
    private final Set<String> zzdc;
    private final Location zzde;
    private final boolean zzsu;

    public zzla(zzlb zzlbVar) {
        this(zzlbVar, null);
    }

    public zzla(zzlb zzlbVar, SearchAdRequest searchAdRequest) {
        this.zzda = zzlbVar.zzda;
        this.zzAk = zzlbVar.zzAk;
        this.zzAe = zzlbVar.zzAe;
        this.zzdc = Collections.unmodifiableSet(zzlbVar.zzAX);
        this.zzde = zzlbVar.zzde;
        this.zzsu = zzlbVar.zzsu;
        this.zzAS = zzlbVar.zzAS;
        this.zzAT = Collections.unmodifiableMap(zzlbVar.zzAY);
        this.zzAi = zzlbVar.zzAi;
        this.zzAo = zzlbVar.zzAo;
        this.zzAU = searchAdRequest;
        this.zzAh = zzlbVar.zzAh;
        this.zzAV = Collections.unmodifiableSet(zzlbVar.zzAZ);
        this.zzAm = zzlbVar.zzAm;
        this.zzAW = Collections.unmodifiableSet(zzlbVar.zzBa);
        this.zzAq = zzlbVar.zzAq;
    }

    public final Date getBirthday() {
        return this.zzda;
    }

    public final String getContentUrl() {
        return this.zzAk;
    }

    public final Bundle getCustomEventExtrasBundle(Class<? extends CustomEvent> cls) {
        Bundle bundle = this.zzAS.getBundle("com.google.android.gms.ads.mediation.customevent.CustomEventAdapter");
        if (bundle != null) {
            return bundle.getBundle(cls.getName());
        }
        return null;
    }

    public final Bundle getCustomTargeting() {
        return this.zzAm;
    }

    public final int getGender() {
        return this.zzAe;
    }

    public final Set<String> getKeywords() {
        return this.zzdc;
    }

    public final Location getLocation() {
        return this.zzde;
    }

    public final boolean getManualImpressionsEnabled() {
        return this.zzsu;
    }

    @Deprecated
    public final <T extends NetworkExtras> T getNetworkExtras(Class<T> cls) {
        return (T) this.zzAT.get(cls);
    }

    public final Bundle getNetworkExtrasBundle(Class<? extends MediationAdapter> cls) {
        return this.zzAS.getBundle(cls.getName());
    }

    public final String getPublisherProvidedId() {
        return this.zzAi;
    }

    public final boolean isDesignedForFamilies() {
        return this.zzAq;
    }

    public final boolean isTestDevice(Context context) {
        Set<String> set = this.zzAV;
        zzji.zzds();
        return set.contains(zzaiy.zzV(context));
    }

    public final Bundle zzdA() {
        return this.zzAS;
    }

    public final int zzdB() {
        return this.zzAh;
    }

    public final Set<String> zzdC() {
        return this.zzAW;
    }

    public final String zzdx() {
        return this.zzAo;
    }

    public final SearchAdRequest zzdy() {
        return this.zzAU;
    }

    public final Map<Class<? extends NetworkExtras>, NetworkExtras> zzdz() {
        return this.zzAT;
    }
}
