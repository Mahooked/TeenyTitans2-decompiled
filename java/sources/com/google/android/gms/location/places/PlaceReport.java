package com.google.android.gms.location.places;

import android.os.Parcel;
import android.os.Parcelable;
import android.support.v4.os.EnvironmentCompat;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbe;
import com.google.android.gms.common.internal.zzbg;
import com.google.android.gms.common.internal.zzbo;
import java.util.Arrays;

/* JADX INFO: loaded from: classes.dex */
public class PlaceReport extends zza implements ReflectedParcelable {
    public static final Parcelable.Creator<PlaceReport> CREATOR = new zzl();
    private final String mTag;
    private final String zzaeK;
    private int zzaku;
    private final String zzbjI;

    PlaceReport(int i, String str, String str2, String str3) {
        this.zzaku = i;
        this.zzbjI = str;
        this.mTag = str2;
        this.zzaeK = str3;
    }

    public static PlaceReport create(String str, String str2) {
        boolean z = false;
        zzbo.zzu(str);
        zzbo.zzcF(str2);
        zzbo.zzcF(EnvironmentCompat.MEDIA_UNKNOWN);
        byte b = -1;
        switch (EnvironmentCompat.MEDIA_UNKNOWN.hashCode()) {
            case -1436706272:
                if (EnvironmentCompat.MEDIA_UNKNOWN.equals("inferredGeofencing")) {
                    b = 2;
                }
                break;
            case -1194968642:
                if (EnvironmentCompat.MEDIA_UNKNOWN.equals("userReported")) {
                    b = 1;
                }
                break;
            case -284840886:
                if (EnvironmentCompat.MEDIA_UNKNOWN.equals(EnvironmentCompat.MEDIA_UNKNOWN)) {
                    b = 0;
                }
                break;
            case -262743844:
                if (EnvironmentCompat.MEDIA_UNKNOWN.equals("inferredReverseGeocoding")) {
                    b = 4;
                }
                break;
            case 1164924125:
                if (EnvironmentCompat.MEDIA_UNKNOWN.equals("inferredSnappedToRoad")) {
                    b = 5;
                }
                break;
            case 1287171955:
                if (EnvironmentCompat.MEDIA_UNKNOWN.equals("inferredRadioSignals")) {
                    b = 3;
                }
                break;
        }
        switch (b) {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                z = true;
                break;
        }
        zzbo.zzb(z, "Invalid source");
        return new PlaceReport(1, str, str2, EnvironmentCompat.MEDIA_UNKNOWN);
    }

    public boolean equals(Object obj) {
        if (!(obj instanceof PlaceReport)) {
            return false;
        }
        PlaceReport placeReport = (PlaceReport) obj;
        return zzbe.equal(this.zzbjI, placeReport.zzbjI) && zzbe.equal(this.mTag, placeReport.mTag) && zzbe.equal(this.zzaeK, placeReport.zzaeK);
    }

    public String getPlaceId() {
        return this.zzbjI;
    }

    public String getTag() {
        return this.mTag;
    }

    public int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzbjI, this.mTag, this.zzaeK});
    }

    public String toString() {
        zzbg zzbgVarZzt = zzbe.zzt(this);
        zzbgVarZzt.zzg("placeId", this.zzbjI);
        zzbgVarZzt.zzg("tag", this.mTag);
        if (!EnvironmentCompat.MEDIA_UNKNOWN.equals(this.zzaeK)) {
            zzbgVarZzt.zzg("source", this.zzaeK);
        }
        return zzbgVarZzt.toString();
    }

    @Override // android.os.Parcelable
    public void writeToParcel(Parcel parcel, int i) {
        int iZze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzaku);
        zzd.zza(parcel, 2, getPlaceId(), false);
        zzd.zza(parcel, 3, getTag(), false);
        zzd.zza(parcel, 4, this.zzaeK, false);
        zzd.zzI(parcel, iZze);
    }
}
