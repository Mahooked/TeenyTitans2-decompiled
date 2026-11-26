package com.google.android.gms.internal;

import android.util.Base64OutputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
final class zzhi {
    private ByteArrayOutputStream zzzb = new ByteArrayOutputStream(4096);
    private Base64OutputStream zzzc = new Base64OutputStream(this.zzzb, 10);

    /* JADX WARN: Multi-variable type inference failed */
    public final String toString() {
        String string;
        try {
            this.zzzc.close();
        } catch (IOException e) {
            zzafr.zzb("HashManager: Unable to convert to Base64.", e);
        }
        try {
            this.zzzb.close();
            string = this.zzzb.toString();
        } catch (IOException e2) {
            zzafr.zzb("HashManager: Unable to convert to Base64.", e2);
            string = "";
        } finally {
            this.zzzb = null;
            this.zzzc = null;
        }
        return string;
    }

    public final void write(byte[] bArr) throws IOException {
        this.zzzc.write(bArr);
    }
}
