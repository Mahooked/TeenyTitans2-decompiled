package com.google.android.gms.common.data;

import android.content.ContentValues;
import android.os.Parcel;
import android.os.Parcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable;

/* JADX INFO: loaded from: classes.dex */
public class zzd<T extends SafeParcelable> extends AbstractDataBuffer<T> {
    private static final String[] zzaFz = {"data"};
    private final Parcelable.Creator<T> zzaFA;

    public zzd(DataHolder dataHolder, Parcelable.Creator<T> creator) {
        super(dataHolder);
        this.zzaFA = creator;
    }

    public static <T extends SafeParcelable> void zza(DataHolder.zza zzaVar, T t) {
        Parcel parcelObtain = Parcel.obtain();
        t.writeToParcel(parcelObtain, 0);
        ContentValues contentValues = new ContentValues();
        contentValues.put("data", parcelObtain.marshall());
        zzaVar.zza(contentValues);
        parcelObtain.recycle();
    }

    public static DataHolder.zza zzqQ() {
        return DataHolder.zza(zzaFz);
    }

    @Override // com.google.android.gms.common.data.AbstractDataBuffer, com.google.android.gms.common.data.DataBuffer
    /* JADX INFO: renamed from: zzas, reason: merged with bridge method [inline-methods] */
    public T get(int i) {
        byte[] bArrZzg = this.zzaCX.zzg("data", i, this.zzaCX.zzat(i));
        Parcel parcelObtain = Parcel.obtain();
        parcelObtain.unmarshall(bArrZzg, 0, bArrZzg.length);
        parcelObtain.setDataPosition(0);
        T tCreateFromParcel = this.zzaFA.createFromParcel(parcelObtain);
        parcelObtain.recycle();
        return tCreateFromParcel;
    }
}
