package com.google.android.gms.common.stats;

import android.os.Parcel;
import android.os.Parcelable;
import android.text.TextUtils;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public final class WakeLockEvent extends StatsEvent {
    public static final Parcelable.Creator<WakeLockEvent> CREATOR = new zzd();
    private final long mTimeout;
    private final long zzaJn;
    private int zzaJo;
    private final String zzaJp;
    private final String zzaJq;
    private final String zzaJr;
    private final int zzaJs;
    private final List<String> zzaJt;
    private final String zzaJu;
    private final long zzaJv;
    private int zzaJw;
    private final String zzaJx;
    private final float zzaJy;
    private long zzaJz;
    private int zzaku;

    WakeLockEvent(int i, long j, int i2, String str, int i3, List<String> list, String str2, long j2, int i4, String str3, String str4, float f, long j3, String str5) {
        this.zzaku = i;
        this.zzaJn = j;
        this.zzaJo = i2;
        this.zzaJp = str;
        this.zzaJq = str3;
        this.zzaJr = str5;
        this.zzaJs = i3;
        this.zzaJz = -1L;
        this.zzaJt = list;
        this.zzaJu = str2;
        this.zzaJv = j2;
        this.zzaJw = i4;
        this.zzaJx = str4;
        this.zzaJy = f;
        this.mTimeout = j3;
    }

    public WakeLockEvent(long j, int i, String str, int i2, List<String> list, String str2, long j2, int i3, String str3, String str4, float f, long j3, String str5) {
        this(2, j, i, str, i2, list, str2, j2, i3, str3, str4, f, j3, str5);
    }

    @Override // com.google.android.gms.common.stats.StatsEvent
    public final int getEventType() {
        return this.zzaJo;
    }

    @Override // com.google.android.gms.common.stats.StatsEvent
    public final long getTimeMillis() {
        return this.zzaJn;
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.zzaku);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.zzaJn);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 4, this.zzaJp, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 5, this.zzaJs);
        com.google.android.gms.common.internal.safeparcel.zzd.zzb(parcel, 6, this.zzaJt, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 8, this.zzaJv);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 10, this.zzaJq, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 11, this.zzaJo);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 12, this.zzaJu, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 13, this.zzaJx, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 14, this.zzaJw);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 15, this.zzaJy);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 16, this.mTimeout);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 17, this.zzaJr, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }

    @Override // com.google.android.gms.common.stats.StatsEvent
    public final long zzrV() {
        return this.zzaJz;
    }

    @Override // com.google.android.gms.common.stats.StatsEvent
    public final String zzrW() {
        String strValueOf = String.valueOf("\t");
        String strValueOf2 = String.valueOf(this.zzaJp);
        String strValueOf3 = String.valueOf("\t");
        int i = this.zzaJs;
        String strValueOf4 = String.valueOf("\t");
        String strJoin = this.zzaJt == null ? "" : TextUtils.join(",", this.zzaJt);
        String strValueOf5 = String.valueOf("\t");
        int i2 = this.zzaJw;
        String strValueOf6 = String.valueOf("\t");
        String str = this.zzaJq == null ? "" : this.zzaJq;
        String strValueOf7 = String.valueOf("\t");
        String str2 = this.zzaJx == null ? "" : this.zzaJx;
        String strValueOf8 = String.valueOf("\t");
        float f = this.zzaJy;
        String strValueOf9 = String.valueOf("\t");
        String str3 = this.zzaJr == null ? "" : this.zzaJr;
        return new StringBuilder(String.valueOf(strValueOf).length() + 37 + String.valueOf(strValueOf2).length() + String.valueOf(strValueOf3).length() + String.valueOf(strValueOf4).length() + String.valueOf(strJoin).length() + String.valueOf(strValueOf5).length() + String.valueOf(strValueOf6).length() + String.valueOf(str).length() + String.valueOf(strValueOf7).length() + String.valueOf(str2).length() + String.valueOf(strValueOf8).length() + String.valueOf(strValueOf9).length() + String.valueOf(str3).length()).append(strValueOf).append(strValueOf2).append(strValueOf3).append(i).append(strValueOf4).append(strJoin).append(strValueOf5).append(i2).append(strValueOf6).append(str).append(strValueOf7).append(str2).append(strValueOf8).append(f).append(strValueOf9).append(str3).toString();
    }
}
