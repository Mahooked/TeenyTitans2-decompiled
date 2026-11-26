package com.google.android.gms.internal;

import java.io.DataOutputStream;
import java.io.IOException;
import java.io.OutputStream;

/* JADX INFO: loaded from: classes.dex */
final class zzaav implements Runnable {
    private /* synthetic */ OutputStream zzTS;
    private /* synthetic */ byte[] zzTT;

    zzaav(zzaau zzaauVar, OutputStream outputStream, byte[] bArr) {
        this.zzTS = outputStream;
        this.zzTT = bArr;
    }

    /* JADX WARN: Code duplicated, block: B:16:0x0037  */
    /* JADX WARN: Code duplicated, block: B:18:0x003d  */
    @Override // java.lang.Runnable
    public final void run() throws Throwable {
        DataOutputStream dataOutputStream;
        try {
            try {
                dataOutputStream = new DataOutputStream(this.zzTS);
                try {
                    dataOutputStream.writeInt(this.zzTT.length);
                    dataOutputStream.write(this.zzTT);
                    com.google.android.gms.common.util.zzn.closeQuietly(dataOutputStream);
                } catch (IOException e) {
                    e = e;
                    zzafr.zzb("Error transporting the ad response", e);
                    com.google.android.gms.ads.internal.zzbs.zzbD().zza(e, "LargeParcelTeleporter.pipeData.1");
                    if (dataOutputStream == null) {
                        com.google.android.gms.common.util.zzn.closeQuietly(this.zzTS);
                    } else {
                        com.google.android.gms.common.util.zzn.closeQuietly(dataOutputStream);
                    }
                }
            } catch (Throwable th) {
                th = th;
                if (dataOutputStream == null) {
                    com.google.android.gms.common.util.zzn.closeQuietly(this.zzTS);
                } else {
                    com.google.android.gms.common.util.zzn.closeQuietly(dataOutputStream);
                }
                throw th;
            }
        } catch (IOException e2) {
            e = e2;
            dataOutputStream = null;
        } catch (Throwable th2) {
            th = th2;
            dataOutputStream = null;
            if (dataOutputStream == null) {
                com.google.android.gms.common.util.zzn.closeQuietly(this.zzTS);
            } else {
                com.google.android.gms.common.util.zzn.closeQuietly(dataOutputStream);
            }
            throw th;
        }
    }
}
