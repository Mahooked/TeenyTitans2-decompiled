package com.google.android.gms.internal;

import android.location.Location;
import android.support.annotation.Nullable;
import com.google.android.gms.ads.mediation.MediationAdRequest;
import java.util.Date;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzvi implements MediationAdRequest {
    private final int zzAe;
    private final boolean zzAq;
    private final int zzMZ;
    private final Date zzda;
    private final Set<String> zzdc;
    private final boolean zzdd;
    private final Location zzde;

    public zzvi(@Nullable Date date, int i, @Nullable Set<String> set, @Nullable Location location, boolean z, int i2, boolean z2) {
        this.zzda = date;
        this.zzAe = i;
        this.zzdc = set;
        this.zzde = location;
        this.zzdd = z;
        this.zzMZ = i2;
        this.zzAq = z2;
    }

    @Override // com.google.android.gms.ads.mediation.MediationAdRequest
    public final Date getBirthday() {
        return this.zzda;
    }

    @Override // com.google.android.gms.ads.mediation.MediationAdRequest
    public final int getGender() {
        return this.zzAe;
    }

    @Override // com.google.android.gms.ads.mediation.MediationAdRequest
    public final Set<String> getKeywords() {
        return this.zzdc;
    }

    @Override // com.google.android.gms.ads.mediation.MediationAdRequest
    public final Location getLocation() {
        return this.zzde;
    }

    @Override // com.google.android.gms.ads.mediation.MediationAdRequest
    public final boolean isDesignedForFamilies() {
        return this.zzAq;
    }

    @Override // com.google.android.gms.ads.mediation.MediationAdRequest
    public final boolean isTesting() {
        return this.zzdd;
    }

    @Override // com.google.android.gms.ads.mediation.MediationAdRequest
    public final int taggedForChildDirectedTreatment() {
        return this.zzMZ;
    }
}
