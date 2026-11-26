package com.google.android.gms.ads.internal;

import android.net.http.Headers;
import android.os.Bundle;
import android.support.annotation.Nullable;
import com.google.android.gms.internal.zzir;
import com.google.android.gms.internal.zziv;
import com.google.android.gms.internal.zzzn;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashSet;
import java.util.Iterator;
import java.util.TreeSet;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzp {
    private static String bundleToString(@Nullable Bundle bundle) {
        String strBundleToString;
        if (bundle == null) {
            return null;
        }
        StringBuilder sb = new StringBuilder();
        Iterator it = new TreeSet(bundle.keySet()).iterator();
        while (it.hasNext()) {
            Object obj = bundle.get((String) it.next());
            if (obj == null) {
                strBundleToString = "null";
            } else {
                strBundleToString = obj instanceof Bundle ? bundleToString((Bundle) obj) : obj.toString();
            }
            sb.append(strBundleToString);
        }
        return sb.toString();
    }

    public static Object[] zza(String str, zzir zzirVar, String str2, int i, @Nullable zziv zzivVar) {
        HashSet hashSet = new HashSet(Arrays.asList(str.split(",")));
        ArrayList arrayList = new ArrayList();
        arrayList.add(str);
        arrayList.add(str2);
        if (hashSet.contains("formatString")) {
            arrayList.add(null);
        }
        if (hashSet.contains("networkType")) {
            arrayList.add(Integer.valueOf(i));
        }
        if (hashSet.contains("birthday")) {
            arrayList.add(Long.valueOf(zzirVar.zzzN));
        }
        if (hashSet.contains("extras")) {
            arrayList.add(bundleToString(zzirVar.extras));
        }
        if (hashSet.contains("gender")) {
            arrayList.add(Integer.valueOf(zzirVar.zzzO));
        }
        if (hashSet.contains("keywords")) {
            if (zzirVar.zzzP != null) {
                arrayList.add(zzirVar.zzzP.toString());
            } else {
                arrayList.add(null);
            }
        }
        if (hashSet.contains("isTestDevice")) {
            arrayList.add(Boolean.valueOf(zzirVar.zzzQ));
        }
        if (hashSet.contains("tagForChildDirectedTreatment")) {
            arrayList.add(Integer.valueOf(zzirVar.zzzR));
        }
        if (hashSet.contains("manualImpressionsEnabled")) {
            arrayList.add(Boolean.valueOf(zzirVar.zzzS));
        }
        if (hashSet.contains("publisherProvidedId")) {
            arrayList.add(zzirVar.zzzT);
        }
        if (hashSet.contains(Headers.LOCATION)) {
            if (zzirVar.zzzV != null) {
                arrayList.add(zzirVar.zzzV.toString());
            } else {
                arrayList.add(null);
            }
        }
        if (hashSet.contains("contentUrl")) {
            arrayList.add(zzirVar.zzzW);
        }
        if (hashSet.contains("networkExtras")) {
            arrayList.add(bundleToString(zzirVar.zzzX));
        }
        if (hashSet.contains("customTargeting")) {
            arrayList.add(bundleToString(zzirVar.zzzY));
        }
        if (hashSet.contains("categoryExclusions")) {
            if (zzirVar.zzzZ != null) {
                arrayList.add(zzirVar.zzzZ.toString());
            } else {
                arrayList.add(null);
            }
        }
        if (hashSet.contains("requestAgent")) {
            arrayList.add(zzirVar.zzAa);
        }
        if (hashSet.contains("requestPackage")) {
            arrayList.add(zzirVar.zzAb);
        }
        if (hashSet.contains("isDesignedForFamilies")) {
            arrayList.add(Boolean.valueOf(zzirVar.zzAc));
        }
        return arrayList.toArray();
    }
}
