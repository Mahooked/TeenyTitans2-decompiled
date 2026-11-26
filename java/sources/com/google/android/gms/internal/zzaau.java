package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.ParcelFileDescriptor;
import android.os.Parcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaau extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzaau> CREATOR = new zzaaw();
    private ParcelFileDescriptor zzTP;
    private Parcelable zzTQ;
    private boolean zzTR;

    zzaau(ParcelFileDescriptor parcelFileDescriptor) {
        this.zzTP = parcelFileDescriptor;
        this.zzTQ = null;
        this.zzTR = true;
    }

    public zzaau(SafeParcelable safeParcelable) {
        this.zzTP = null;
        this.zzTQ = safeParcelable;
        this.zzTR = false;
    }

    private final <T> ParcelFileDescriptor zzc(byte[] bArr) {
        ParcelFileDescriptor.AutoCloseOutputStream autoCloseOutputStream;
        try {
            ParcelFileDescriptor[] parcelFileDescriptorArrCreatePipe = ParcelFileDescriptor.createPipe();
            autoCloseOutputStream = new ParcelFileDescriptor.AutoCloseOutputStream(parcelFileDescriptorArrCreatePipe[1]);
            try {
                new Thread(new zzaav(this, autoCloseOutputStream, bArr)).start();
                return parcelFileDescriptorArrCreatePipe[0];
            } catch (IOException e) {
                e = e;
                zzafr.zzb("Error transporting the ad response", e);
                com.google.android.gms.ads.internal.zzbs.zzbD().zza(e, "LargeParcelTeleporter.pipeData.2");
                com.google.android.gms.common.util.zzn.closeQuietly(autoCloseOutputStream);
                return null;
            }
        } catch (IOException e2) {
            e = e2;
            autoCloseOutputStream = null;
        }
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        if (this.zzTP == null) {
            Parcel parcelObtain = Parcel.obtain();
            try {
                this.zzTQ.writeToParcel(parcelObtain, 0);
                byte[] bArrMarshall = parcelObtain.marshall();
                parcelObtain.recycle();
                this.zzTP = zzc(bArrMarshall);
            } catch (Throwable th) {
                parcelObtain.recycle();
                throw th;
            }
        }
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, (Parcelable) this.zzTP, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }

    /* JADX WARN: Bottom block not found for handler: all -> 0x0052 */
    /*
        Code decompiled incorrectly, please refer to instructions dump.
        To view partially-correct add '--show-bad-code' argument
    */
    public final <T extends com.google.android.gms.common.internal.safeparcel.SafeParcelable> T zza(android.os.Parcelable.Creator<T> r6) {
        /*
            r5 = this;
            r4 = 0
            boolean r0 = r5.zzTR
            if (r0 == 0) goto L44
            android.os.ParcelFileDescriptor r0 = r5.zzTP
            if (r0 != 0) goto L10
            java.lang.String r0 = "File descriptor is empty, returning null."
            com.google.android.gms.internal.zzafr.e(r0)
            r0 = 0
        Lf:
            return r0
        L10:
            java.io.DataInputStream r1 = new java.io.DataInputStream
            android.os.ParcelFileDescriptor$AutoCloseInputStream r0 = new android.os.ParcelFileDescriptor$AutoCloseInputStream
            android.os.ParcelFileDescriptor r2 = r5.zzTP
            r0.<init>(r2)
            r1.<init>(r0)
            int r0 = r1.readInt()     // Catch: java.io.IOException -> L49 java.lang.Throwable -> L52
            byte[] r0 = new byte[r0]     // Catch: java.io.IOException -> L49 java.lang.Throwable -> L52
            r2 = 0
            int r3 = r0.length     // Catch: java.io.IOException -> L49 java.lang.Throwable -> L52
            r1.readFully(r0, r2, r3)     // Catch: java.io.IOException -> L49 java.lang.Throwable -> L52
            com.google.android.gms.common.util.zzn.closeQuietly(r1)
            android.os.Parcel r1 = android.os.Parcel.obtain()
            r2 = 0
            int r3 = r0.length     // Catch: java.lang.Throwable -> L57
            r1.unmarshall(r0, r2, r3)     // Catch: java.lang.Throwable -> L57
            r0 = 0
            r1.setDataPosition(r0)     // Catch: java.lang.Throwable -> L57
            java.lang.Object r0 = r6.createFromParcel(r1)     // Catch: java.lang.Throwable -> L57
            com.google.android.gms.common.internal.safeparcel.SafeParcelable r0 = (com.google.android.gms.common.internal.safeparcel.SafeParcelable) r0     // Catch: java.lang.Throwable -> L57
            r5.zzTQ = r0     // Catch: java.lang.Throwable -> L57
            r1.recycle()
            r5.zzTR = r4
        L44:
            android.os.Parcelable r0 = r5.zzTQ
            com.google.android.gms.common.internal.safeparcel.SafeParcelable r0 = (com.google.android.gms.common.internal.safeparcel.SafeParcelable) r0
            goto Lf
        L49:
            r0 = move-exception
            java.lang.IllegalStateException r2 = new java.lang.IllegalStateException     // Catch: java.lang.Throwable -> L52
            java.lang.String r3 = "Could not read from parcel file descriptor"
            r2.<init>(r3, r0)     // Catch: java.lang.Throwable -> L52
            throw r2     // Catch: java.lang.Throwable -> L52
        L52:
            r0 = move-exception
            com.google.android.gms.common.util.zzn.closeQuietly(r1)
            throw r0
        L57:
            r0 = move-exception
            r1.recycle()
            throw r0
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzaau.zza(android.os.Parcelable$Creator):com.google.android.gms.common.internal.safeparcel.SafeParcelable");
    }
}
