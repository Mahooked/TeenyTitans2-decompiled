package com.google.android.gms.ads.internal.overlay;

import android.content.Intent;
import android.os.Bundle;
import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzim;
import com.google.android.gms.internal.zzqk;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class AdOverlayInfoParcel extends com.google.android.gms.common.internal.safeparcel.zza implements ReflectedParcelable {
    public static final Parcelable.Creator<AdOverlayInfoParcel> CREATOR = new zzv();
    public final int orientation;
    public final String url;
    public final zzc zzPd;
    public final zzim zzPe;
    public final zzw zzPf;
    public final zzaka zzPg;
    public final zzqk zzPh;
    public final String zzPi;
    public final boolean zzPj;
    public final String zzPk;
    public final zzag zzPl;
    public final int zzPm;
    public final String zzPn;
    public final com.google.android.gms.ads.internal.zzap zzPo;
    public final zzaje zzvT;

    AdOverlayInfoParcel(zzc zzcVar, IBinder iBinder, IBinder iBinder2, IBinder iBinder3, IBinder iBinder4, String str, boolean z, String str2, IBinder iBinder5, int i, int i2, String str3, zzaje zzajeVar, String str4, com.google.android.gms.ads.internal.zzap zzapVar) {
        this.zzPd = zzcVar;
        this.zzPe = (zzim) com.google.android.gms.dynamic.zzn.zzE(IObjectWrapper.zza.zzM(iBinder));
        this.zzPf = (zzw) com.google.android.gms.dynamic.zzn.zzE(IObjectWrapper.zza.zzM(iBinder2));
        this.zzPg = (zzaka) com.google.android.gms.dynamic.zzn.zzE(IObjectWrapper.zza.zzM(iBinder3));
        this.zzPh = (zzqk) com.google.android.gms.dynamic.zzn.zzE(IObjectWrapper.zza.zzM(iBinder4));
        this.zzPi = str;
        this.zzPj = z;
        this.zzPk = str2;
        this.zzPl = (zzag) com.google.android.gms.dynamic.zzn.zzE(IObjectWrapper.zza.zzM(iBinder5));
        this.orientation = i;
        this.zzPm = i2;
        this.url = str3;
        this.zzvT = zzajeVar;
        this.zzPn = str4;
        this.zzPo = zzapVar;
    }

    public AdOverlayInfoParcel(zzc zzcVar, zzim zzimVar, zzw zzwVar, zzag zzagVar, zzaje zzajeVar) {
        this.zzPd = zzcVar;
        this.zzPe = zzimVar;
        this.zzPf = zzwVar;
        this.zzPg = null;
        this.zzPh = null;
        this.zzPi = null;
        this.zzPj = false;
        this.zzPk = null;
        this.zzPl = zzagVar;
        this.orientation = -1;
        this.zzPm = 4;
        this.url = null;
        this.zzvT = zzajeVar;
        this.zzPn = null;
        this.zzPo = null;
    }

    public AdOverlayInfoParcel(zzim zzimVar, zzw zzwVar, zzag zzagVar, zzaka zzakaVar, int i, zzaje zzajeVar, String str, com.google.android.gms.ads.internal.zzap zzapVar) {
        this.zzPd = null;
        this.zzPe = zzimVar;
        this.zzPf = zzwVar;
        this.zzPg = zzakaVar;
        this.zzPh = null;
        this.zzPi = null;
        this.zzPj = false;
        this.zzPk = null;
        this.zzPl = zzagVar;
        this.orientation = i;
        this.zzPm = 1;
        this.url = null;
        this.zzvT = zzajeVar;
        this.zzPn = str;
        this.zzPo = zzapVar;
    }

    public AdOverlayInfoParcel(zzim zzimVar, zzw zzwVar, zzag zzagVar, zzaka zzakaVar, boolean z, int i, zzaje zzajeVar) {
        this.zzPd = null;
        this.zzPe = zzimVar;
        this.zzPf = zzwVar;
        this.zzPg = zzakaVar;
        this.zzPh = null;
        this.zzPi = null;
        this.zzPj = z;
        this.zzPk = null;
        this.zzPl = zzagVar;
        this.orientation = i;
        this.zzPm = 2;
        this.url = null;
        this.zzvT = zzajeVar;
        this.zzPn = null;
        this.zzPo = null;
    }

    public AdOverlayInfoParcel(zzim zzimVar, zzw zzwVar, zzqk zzqkVar, zzag zzagVar, zzaka zzakaVar, boolean z, int i, String str, zzaje zzajeVar) {
        this.zzPd = null;
        this.zzPe = zzimVar;
        this.zzPf = zzwVar;
        this.zzPg = zzakaVar;
        this.zzPh = zzqkVar;
        this.zzPi = null;
        this.zzPj = z;
        this.zzPk = null;
        this.zzPl = zzagVar;
        this.orientation = i;
        this.zzPm = 3;
        this.url = str;
        this.zzvT = zzajeVar;
        this.zzPn = null;
        this.zzPo = null;
    }

    public AdOverlayInfoParcel(zzim zzimVar, zzw zzwVar, zzqk zzqkVar, zzag zzagVar, zzaka zzakaVar, boolean z, int i, String str, String str2, zzaje zzajeVar) {
        this.zzPd = null;
        this.zzPe = zzimVar;
        this.zzPf = zzwVar;
        this.zzPg = zzakaVar;
        this.zzPh = zzqkVar;
        this.zzPi = str2;
        this.zzPj = z;
        this.zzPk = str;
        this.zzPl = zzagVar;
        this.orientation = i;
        this.zzPm = 3;
        this.url = null;
        this.zzvT = zzajeVar;
        this.zzPn = null;
        this.zzPo = null;
    }

    public static void zza(Intent intent, AdOverlayInfoParcel adOverlayInfoParcel) {
        Bundle bundle = new Bundle(1);
        bundle.putParcelable("com.google.android.gms.ads.inernal.overlay.AdOverlayInfo", adOverlayInfoParcel);
        intent.putExtra("com.google.android.gms.ads.inernal.overlay.AdOverlayInfo", bundle);
    }

    public static AdOverlayInfoParcel zzb(Intent intent) {
        try {
            Bundle bundleExtra = intent.getBundleExtra("com.google.android.gms.ads.inernal.overlay.AdOverlayInfo");
            bundleExtra.setClassLoader(AdOverlayInfoParcel.class.getClassLoader());
            return (AdOverlayInfoParcel) bundleExtra.getParcelable("com.google.android.gms.ads.inernal.overlay.AdOverlayInfo");
        } catch (Exception e) {
            return null;
        }
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, (Parcelable) this.zzPd, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, com.google.android.gms.dynamic.zzn.zzw(this.zzPe).asBinder(), false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 4, com.google.android.gms.dynamic.zzn.zzw(this.zzPf).asBinder(), false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 5, com.google.android.gms.dynamic.zzn.zzw(this.zzPg).asBinder(), false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 6, com.google.android.gms.dynamic.zzn.zzw(this.zzPh).asBinder(), false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 7, this.zzPi, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 8, this.zzPj);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 9, this.zzPk, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 10, com.google.android.gms.dynamic.zzn.zzw(this.zzPl).asBinder(), false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 11, this.orientation);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 12, this.zzPm);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 13, this.url, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 14, (Parcelable) this.zzvT, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 16, this.zzPn, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 17, (Parcelable) this.zzPo, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
