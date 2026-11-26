package com.google.android.gms.internal;

import android.os.Parcel;
import android.util.Base64;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
@zzzn
final class zztr {
    final int zzKt;
    final String zztV;
    final zzir zzuT;

    private zztr(zzir zzirVar, String str, int i) {
        this.zzuT = zzirVar;
        this.zztV = str;
        this.zzKt = i;
    }

    zztr(zztn zztnVar) {
        this(zztnVar.zzeI(), zztnVar.getAdUnitId(), zztnVar.getNetworkType());
    }

    static zztr zzab(String str) throws IOException {
        String[] strArrSplit = str.split("\u0000");
        if (strArrSplit.length != 3) {
            throw new IOException("Incorrect field count for QueueSeed.");
        }
        Parcel parcelObtain = Parcel.obtain();
        try {
            try {
                String str2 = new String(Base64.decode(strArrSplit[0], 0), HTTP.UTF_8);
                int i = Integer.parseInt(strArrSplit[1]);
                byte[] bArrDecode = Base64.decode(strArrSplit[2], 0);
                parcelObtain.unmarshall(bArrDecode, 0, bArrDecode.length);
                parcelObtain.setDataPosition(0);
                zztr zztrVar = new zztr(zzir.CREATOR.createFromParcel(parcelObtain), str2, i);
                parcelObtain.recycle();
                return zztrVar;
            } catch (Throwable th) {
                com.google.android.gms.ads.internal.zzbs.zzbD().zza(th, "QueueSeed.decode");
                throw new IOException("Malformed QueueSeed encoding.", th);
            }
        } catch (Throwable th2) {
            parcelObtain.recycle();
            throw th2;
        }
    }

    final String zzeW() {
        String string;
        Parcel parcelObtain = Parcel.obtain();
        try {
            try {
                String strEncodeToString = Base64.encodeToString(this.zztV.getBytes(HTTP.UTF_8), 0);
                String string2 = Integer.toString(this.zzKt);
                this.zzuT.writeToParcel(parcelObtain, 0);
                String strEncodeToString2 = Base64.encodeToString(parcelObtain.marshall(), 0);
                string = new StringBuilder(String.valueOf(strEncodeToString).length() + 2 + String.valueOf(string2).length() + String.valueOf(strEncodeToString2).length()).append(strEncodeToString).append("\u0000").append(string2).append("\u0000").append(strEncodeToString2).toString();
                parcelObtain.recycle();
            } catch (UnsupportedEncodingException e) {
                zzafr.e("QueueSeed encode failed because UTF-8 is not available.");
                parcelObtain.recycle();
                string = "";
            }
            return string;
        } catch (Throwable th) {
            parcelObtain.recycle();
            throw th;
        }
    }
}
