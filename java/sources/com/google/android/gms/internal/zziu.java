package com.google.android.gms.internal;

import android.content.Context;
import android.location.Location;
import android.os.Bundle;
import com.google.ads.mediation.admob.AdMobAdapter;
import com.google.android.gms.ads.search.SearchAdRequest;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Date;
import java.util.List;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zziu {
    public static final zziu zzAr = new zziu();

    protected zziu() {
    }

    public static zzir zza(Context context, zzla zzlaVar) {
        Date birthday = zzlaVar.getBirthday();
        long time = birthday != null ? birthday.getTime() : -1L;
        String contentUrl = zzlaVar.getContentUrl();
        int gender = zzlaVar.getGender();
        Set<String> keywords = zzlaVar.getKeywords();
        List listUnmodifiableList = !keywords.isEmpty() ? Collections.unmodifiableList(new ArrayList(keywords)) : null;
        boolean zIsTestDevice = zzlaVar.isTestDevice(context);
        int iZzdB = zzlaVar.zzdB();
        Location location = zzlaVar.getLocation();
        Bundle networkExtrasBundle = zzlaVar.getNetworkExtrasBundle(AdMobAdapter.class);
        boolean manualImpressionsEnabled = zzlaVar.getManualImpressionsEnabled();
        String publisherProvidedId = zzlaVar.getPublisherProvidedId();
        SearchAdRequest searchAdRequestZzdy = zzlaVar.zzdy();
        zzlt zzltVar = searchAdRequestZzdy != null ? new zzlt(searchAdRequestZzdy) : null;
        String strZza = null;
        Context applicationContext = context.getApplicationContext();
        if (applicationContext != null) {
            String packageName = applicationContext.getPackageName();
            zzji.zzds();
            strZza = zzaiy.zza(Thread.currentThread().getStackTrace(), packageName);
        }
        return new zzir(7, time, networkExtrasBundle, gender, listUnmodifiableList, zIsTestDevice, iZzdB, manualImpressionsEnabled, publisherProvidedId, zzltVar, location, contentUrl, zzlaVar.zzdA(), zzlaVar.getCustomTargeting(), Collections.unmodifiableList(new ArrayList(zzlaVar.zzdC())), zzlaVar.zzdx(), strZza, zzlaVar.isDesignedForFamilies());
    }
}
