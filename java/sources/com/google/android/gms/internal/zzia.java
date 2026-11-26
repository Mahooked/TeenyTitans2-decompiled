package com.google.android.gms.internal;

import android.net.Uri;
import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable;
import android.support.annotation.Nullable;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzia extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzia> CREATOR = new zzib();

    @Nullable
    public final String url;
    private long zzzu;
    private String zzzv;
    private String zzzw;
    private String zzzx;
    private Bundle zzzy;
    private boolean zzzz;

    zzia(@Nullable String str, long j, String str2, String str3, String str4, Bundle bundle, boolean z) {
        this.url = str;
        this.zzzu = j;
        this.zzzv = str2 == null ? "" : str2;
        this.zzzw = str3 == null ? "" : str3;
        this.zzzx = str4 == null ? "" : str4;
        this.zzzy = bundle == null ? new Bundle() : bundle;
        this.zzzz = z;
    }

    @Nullable
    public static zzia zzB(String str) {
        return zze(Uri.parse(str));
    }

    @Nullable
    public static zzia zze(Uri uri) {
        try {
            if (!"gcache".equals(uri.getScheme())) {
                return null;
            }
            List<String> pathSegments = uri.getPathSegments();
            if (pathSegments.size() != 2) {
                zzafr.zzaT(new StringBuilder(62).append("Expected 2 path parts for namespace and id, found :").append(pathSegments.size()).toString());
                return null;
            }
            String str = pathSegments.get(0);
            String str2 = pathSegments.get(1);
            String host = uri.getHost();
            String queryParameter = uri.getQueryParameter("url");
            boolean zEquals = "1".equals(uri.getQueryParameter("read_only"));
            String queryParameter2 = uri.getQueryParameter("expiration");
            long j = queryParameter2 == null ? 0L : Long.parseLong(queryParameter2);
            Bundle bundle = new Bundle();
            for (String str3 : com.google.android.gms.ads.internal.zzbs.zzbB().zzh(uri)) {
                if (str3.startsWith("tag.")) {
                    bundle.putString(str3.substring(4), uri.getQueryParameter(str3));
                }
            }
            return new zzia(queryParameter, j, host, str, str2, bundle, zEquals);
        } catch (NullPointerException | NumberFormatException e) {
            zzafr.zzc("Unable to parse Uri into cache offering.", e);
            return null;
        }
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.url, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, this.zzzu);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 4, this.zzzv, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 5, this.zzzw, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 6, this.zzzx, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 7, this.zzzy, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 8, this.zzzz);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
