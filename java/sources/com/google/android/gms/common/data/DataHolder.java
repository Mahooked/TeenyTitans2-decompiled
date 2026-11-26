package com.google.android.gms.common.data;

import android.content.ContentValues;
import android.database.CharArrayBuffer;
import android.database.CursorIndexOutOfBoundsException;
import android.database.CursorWindow;
import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable;
import android.util.Log;
import com.google.android.gms.common.annotation.KeepName;
import com.google.android.gms.common.internal.zzbo;
import java.io.Closeable;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@KeepName
public final class DataHolder extends com.google.android.gms.common.internal.safeparcel.zza implements Closeable {
    public static final Parcelable.Creator<DataHolder> CREATOR = new zzf();
    private static final zza zzaFI = new zze(new String[0], null);
    private boolean mClosed;
    private final String[] zzaFB;
    private Bundle zzaFC;
    private final CursorWindow[] zzaFD;
    private final Bundle zzaFE;
    private int[] zzaFF;
    int zzaFG;
    private boolean zzaFH;
    private int zzaku;
    private final int zzaxu;

    public static class zza {
        private final String[] zzaFB;
        private final ArrayList<HashMap<String, Object>> zzaFJ;
        private final String zzaFK;
        private final HashMap<Object, Integer> zzaFL;
        private boolean zzaFM;
        private String zzaFN;

        private zza(String[] strArr, String str) {
            this.zzaFB = (String[]) zzbo.zzu(strArr);
            this.zzaFJ = new ArrayList<>();
            this.zzaFK = str;
            this.zzaFL = new HashMap<>();
            this.zzaFM = false;
            this.zzaFN = null;
        }

        /* synthetic */ zza(String[] strArr, String str, zze zzeVar) {
            this(strArr, null);
        }

        public zza zza(ContentValues contentValues) {
            com.google.android.gms.common.internal.zzc.zzr(contentValues);
            HashMap<String, Object> map = new HashMap<>(contentValues.size());
            for (Map.Entry<String, Object> entry : contentValues.valueSet()) {
                map.put(entry.getKey(), entry.getValue());
            }
            return zza(map);
        }

        public zza zza(HashMap<String, Object> map) {
            Object obj;
            int iIntValue;
            com.google.android.gms.common.internal.zzc.zzr(map);
            if (this.zzaFK == null || (obj = map.get(this.zzaFK)) == null) {
                iIntValue = -1;
            } else {
                Integer num = this.zzaFL.get(obj);
                if (num == null) {
                    this.zzaFL.put(obj, Integer.valueOf(this.zzaFJ.size()));
                    iIntValue = -1;
                } else {
                    iIntValue = num.intValue();
                }
            }
            if (iIntValue == -1) {
                this.zzaFJ.add(map);
            } else {
                this.zzaFJ.remove(iIntValue);
                this.zzaFJ.add(iIntValue, map);
            }
            this.zzaFM = false;
            return this;
        }

        /* JADX WARN: Multi-variable type inference failed */
        public final DataHolder zzav(int i) {
            return new DataHolder(this, 0, (Bundle) null, (zze) (0 == true ? 1 : 0));
        }
    }

    public static class zzb extends RuntimeException {
        public zzb(String str) {
            super(str);
        }
    }

    DataHolder(int i, String[] strArr, CursorWindow[] cursorWindowArr, int i2, Bundle bundle) {
        this.mClosed = false;
        this.zzaFH = true;
        this.zzaku = i;
        this.zzaFB = strArr;
        this.zzaFD = cursorWindowArr;
        this.zzaxu = i2;
        this.zzaFE = bundle;
    }

    private DataHolder(zza zzaVar, int i, Bundle bundle) {
        this(zzaVar.zzaFB, zza(zzaVar, -1), i, (Bundle) null);
    }

    /* synthetic */ DataHolder(zza zzaVar, int i, Bundle bundle, zze zzeVar) {
        this(zzaVar, 0, null);
    }

    private DataHolder(String[] strArr, CursorWindow[] cursorWindowArr, int i, Bundle bundle) {
        this.mClosed = false;
        this.zzaFH = true;
        this.zzaku = 1;
        this.zzaFB = (String[]) zzbo.zzu(strArr);
        this.zzaFD = (CursorWindow[]) zzbo.zzu(cursorWindowArr);
        this.zzaxu = i;
        this.zzaFE = bundle;
        zzqR();
    }

    /* JADX WARN: Multi-variable type inference failed */
    public static zza zza(String[] strArr) {
        return new zza(strArr, null, 0 == true ? 1 : 0);
    }

    private static CursorWindow[] zza(zza zzaVar, int i) {
        int i2;
        boolean z;
        CursorWindow cursorWindow;
        if (zzaVar.zzaFB.length == 0) {
            return new CursorWindow[0];
        }
        ArrayList arrayList = zzaVar.zzaFJ;
        int size = arrayList.size();
        CursorWindow cursorWindow2 = new CursorWindow(false);
        ArrayList arrayList2 = new ArrayList();
        arrayList2.add(cursorWindow2);
        cursorWindow2.setNumColumns(zzaVar.zzaFB.length);
        int i3 = 0;
        boolean z2 = false;
        while (i3 < size) {
            try {
                if (!cursorWindow2.allocRow()) {
                    Log.d("DataHolder", new StringBuilder(72).append("Allocating additional cursor window for large data set (row ").append(i3).append(")").toString());
                    cursorWindow2 = new CursorWindow(false);
                    cursorWindow2.setStartPosition(i3);
                    cursorWindow2.setNumColumns(zzaVar.zzaFB.length);
                    arrayList2.add(cursorWindow2);
                    if (!cursorWindow2.allocRow()) {
                        Log.e("DataHolder", "Unable to allocate row to hold data.");
                        arrayList2.remove(cursorWindow2);
                        return (CursorWindow[]) arrayList2.toArray(new CursorWindow[arrayList2.size()]);
                    }
                }
                Map map = (Map) arrayList.get(i3);
                boolean zPutDouble = true;
                for (int i4 = 0; i4 < zzaVar.zzaFB.length && zPutDouble; i4++) {
                    String str = zzaVar.zzaFB[i4];
                    Object obj = map.get(str);
                    if (obj == null) {
                        zPutDouble = cursorWindow2.putNull(i3, i4);
                    } else if (obj instanceof String) {
                        zPutDouble = cursorWindow2.putString((String) obj, i3, i4);
                    } else if (obj instanceof Long) {
                        zPutDouble = cursorWindow2.putLong(((Long) obj).longValue(), i3, i4);
                    } else if (obj instanceof Integer) {
                        zPutDouble = cursorWindow2.putLong(((Integer) obj).intValue(), i3, i4);
                    } else if (obj instanceof Boolean) {
                        zPutDouble = cursorWindow2.putLong(((Boolean) obj).booleanValue() ? 1L : 0L, i3, i4);
                    } else if (obj instanceof byte[]) {
                        zPutDouble = cursorWindow2.putBlob((byte[]) obj, i3, i4);
                    } else if (obj instanceof Double) {
                        zPutDouble = cursorWindow2.putDouble(((Double) obj).doubleValue(), i3, i4);
                    } else {
                        if (!(obj instanceof Float)) {
                            String strValueOf = String.valueOf(obj);
                            throw new IllegalArgumentException(new StringBuilder(String.valueOf(str).length() + 32 + String.valueOf(strValueOf).length()).append("Unsupported object for column ").append(str).append(": ").append(strValueOf).toString());
                        }
                        zPutDouble = cursorWindow2.putDouble(((Float) obj).floatValue(), i3, i4);
                    }
                }
                if (zPutDouble) {
                    i2 = i3;
                    z = false;
                    cursorWindow = cursorWindow2;
                } else {
                    if (z2) {
                        throw new zzb("Could not add the value to a new CursorWindow. The size of value may be larger than what a CursorWindow can handle.");
                    }
                    Log.d("DataHolder", new StringBuilder(74).append("Couldn't populate window data for row ").append(i3).append(" - allocating new window.").toString());
                    cursorWindow2.freeLastRow();
                    CursorWindow cursorWindow3 = new CursorWindow(false);
                    cursorWindow3.setStartPosition(i3);
                    cursorWindow3.setNumColumns(zzaVar.zzaFB.length);
                    arrayList2.add(cursorWindow3);
                    i2 = i3 - 1;
                    cursorWindow = cursorWindow3;
                    z = true;
                }
                z2 = z;
                cursorWindow2 = cursorWindow;
                i3 = i2 + 1;
            } catch (RuntimeException e) {
                int size2 = arrayList2.size();
                for (int i5 = 0; i5 < size2; i5++) {
                    ((CursorWindow) arrayList2.get(i5)).close();
                }
                throw e;
            }
        }
        return (CursorWindow[]) arrayList2.toArray(new CursorWindow[arrayList2.size()]);
    }

    public static DataHolder zzau(int i) {
        return new DataHolder(zzaFI, i, null);
    }

    private final void zzh(String str, int i) {
        if (this.zzaFC == null || !this.zzaFC.containsKey(str)) {
            String strValueOf = String.valueOf(str);
            throw new IllegalArgumentException(strValueOf.length() != 0 ? "No such column: ".concat(strValueOf) : new String("No such column: "));
        }
        if (isClosed()) {
            throw new IllegalArgumentException("Buffer is closed.");
        }
        if (i < 0 || i >= this.zzaFG) {
            throw new CursorIndexOutOfBoundsException(i, this.zzaFG);
        }
    }

    @Override // java.io.Closeable, java.lang.AutoCloseable
    public final void close() {
        synchronized (this) {
            if (!this.mClosed) {
                this.mClosed = true;
                for (int i = 0; i < this.zzaFD.length; i++) {
                    this.zzaFD[i].close();
                }
            }
        }
    }

    protected final void finalize() throws Throwable {
        try {
            if (this.zzaFH && this.zzaFD.length > 0 && !isClosed()) {
                close();
                String strValueOf = String.valueOf(toString());
                Log.e("DataBuffer", new StringBuilder(String.valueOf(strValueOf).length() + 178).append("Internal data leak within a DataBuffer object detected!  Be sure to explicitly call release() on all DataBuffer extending objects when you are done with them. (internal object: ").append(strValueOf).append(")").toString());
            }
        } finally {
            super.finalize();
        }
    }

    public final int getCount() {
        return this.zzaFG;
    }

    public final int getStatusCode() {
        return this.zzaxu;
    }

    public final boolean isClosed() {
        boolean z;
        synchronized (this) {
            z = this.mClosed;
        }
        return z;
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 1, this.zzaFB, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, (Parcelable[]) this.zzaFD, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 3, this.zzaxu);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 4, this.zzaFE, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1000, this.zzaku);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }

    public final void zza(String str, int i, int i2, CharArrayBuffer charArrayBuffer) {
        zzh(str, i);
        this.zzaFD[i2].copyStringToBuffer(i, this.zzaFC.getInt(str), charArrayBuffer);
    }

    public final int zzat(int i) {
        int i2 = 0;
        zzbo.zzae(i >= 0 && i < this.zzaFG);
        while (i2 < this.zzaFF.length) {
            if (i < this.zzaFF[i2]) {
                i2--;
                break;
            }
            i2++;
        }
        return i2 == this.zzaFF.length ? i2 - 1 : i2;
    }

    public final long zzb(String str, int i, int i2) {
        zzh(str, i);
        return this.zzaFD[i2].getLong(i, this.zzaFC.getInt(str));
    }

    public final int zzc(String str, int i, int i2) {
        zzh(str, i);
        return this.zzaFD[i2].getInt(i, this.zzaFC.getInt(str));
    }

    public final boolean zzcv(String str) {
        return this.zzaFC.containsKey(str);
    }

    public final String zzd(String str, int i, int i2) {
        zzh(str, i);
        return this.zzaFD[i2].getString(i, this.zzaFC.getInt(str));
    }

    public final boolean zze(String str, int i, int i2) {
        zzh(str, i);
        return Long.valueOf(this.zzaFD[i2].getLong(i, this.zzaFC.getInt(str))).longValue() == 1;
    }

    public final float zzf(String str, int i, int i2) {
        zzh(str, i);
        return this.zzaFD[i2].getFloat(i, this.zzaFC.getInt(str));
    }

    public final byte[] zzg(String str, int i, int i2) {
        zzh(str, i);
        return this.zzaFD[i2].getBlob(i, this.zzaFC.getInt(str));
    }

    public final boolean zzh(String str, int i, int i2) {
        zzh(str, i);
        return this.zzaFD[i2].isNull(i, this.zzaFC.getInt(str));
    }

    public final Bundle zzqN() {
        return this.zzaFE;
    }

    public final void zzqR() {
        this.zzaFC = new Bundle();
        for (int i = 0; i < this.zzaFB.length; i++) {
            this.zzaFC.putInt(this.zzaFB[i], i);
        }
        this.zzaFF = new int[this.zzaFD.length];
        int numRows = 0;
        for (int i2 = 0; i2 < this.zzaFD.length; i2++) {
            this.zzaFF[i2] = numRows;
            numRows += this.zzaFD[i2].getNumRows() - (numRows - this.zzaFD[i2].getStartPosition());
        }
        this.zzaFG = numRows;
    }
}
