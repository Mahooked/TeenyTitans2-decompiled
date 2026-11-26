package com.google.android.gms.internal;

import com.google.android.gms.ads.identifier.AdvertisingIdClient;
import java.io.IOException;
import java.lang.reflect.InvocationTargetException;

/* JADX INFO: loaded from: classes.dex */
public final class zzdp extends zzec {
    public zzdp(zzdb zzdbVar, String str, String str2, zzax zzaxVar, int i, int i2) {
        super(zzdbVar, str, str2, zzaxVar, i, 24);
    }

    private final void zzW() {
        AdvertisingIdClient advertisingIdClientZzO = this.zzpJ.zzO();
        if (advertisingIdClientZzO == null) {
            return;
        }
        try {
            AdvertisingIdClient.Info info = advertisingIdClientZzO.getInfo();
            String strZzn = zzdg.zzn(info.getId());
            if (strZzn != null) {
                synchronized (this.zzro) {
                    this.zzro.zzbU = strZzn;
                    this.zzro.zzbW = Boolean.valueOf(info.isLimitAdTrackingEnabled());
                    this.zzro.zzbV = 5;
                }
            }
        } catch (IOException e) {
        }
    }

    @Override // com.google.android.gms.internal.zzec
    protected final void zzT() throws IllegalAccessException, InvocationTargetException {
        if (this.zzpJ.zzH()) {
            zzW();
            return;
        }
        synchronized (this.zzro) {
            this.zzro.zzbU = (String) this.zzrx.invoke(null, this.zzpJ.getApplicationContext());
        }
    }

    @Override // com.google.android.gms.internal.zzec, java.util.concurrent.Callable
    /* JADX INFO: renamed from: zzV */
    public final Void call() throws Exception {
        if (this.zzpJ.isInitialized()) {
            return super.call();
        }
        if (this.zzpJ.zzH()) {
            zzW();
        }
        return null;
    }
}
