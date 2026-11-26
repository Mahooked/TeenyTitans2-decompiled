package com.google.android.gms.internal;

import android.content.Context;
import android.text.TextUtils;
import android.view.View;
import com.google.android.gms.ads.identifier.AdvertisingIdClient;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.Callable;

/* JADX INFO: loaded from: classes.dex */
public class zzcr extends zzcs {
    private static final String TAG = zzcr.class.getSimpleName();
    private AdvertisingIdClient.Info zzqi;

    private zzcr(Context context) {
        super(context, "");
    }

    public static String zza(String str, String str2) {
        return zzbv.zza(str, str2, true);
    }

    public static zzcr zzb(Context context) {
        zza(context, true);
        return new zzcr(context);
    }

    @Override // com.google.android.gms.internal.zzcs, com.google.android.gms.internal.zzcq
    protected final zzax zza(Context context, View view) {
        return null;
    }

    public final void zza(AdvertisingIdClient.Info info) {
        this.zzqi = info;
    }

    @Override // com.google.android.gms.internal.zzcs
    protected final void zza(zzdb zzdbVar, zzax zzaxVar, zzau zzauVar) {
        if (!zzdbVar.zzqP) {
            zza(zzb(zzdbVar, zzaxVar, zzauVar));
            return;
        }
        if (this.zzqi != null) {
            String id = this.zzqi.getId();
            if (!TextUtils.isEmpty(id)) {
                zzaxVar.zzbU = zzdg.zzn(id);
                zzaxVar.zzbV = 5;
                zzaxVar.zzbW = Boolean.valueOf(this.zzqi.isLimitAdTrackingEnabled());
            }
            this.zzqi = null;
        }
    }

    @Override // com.google.android.gms.internal.zzcs
    protected final List<Callable<Void>> zzb(zzdb zzdbVar, zzax zzaxVar, zzau zzauVar) {
        ArrayList arrayList = new ArrayList();
        if (zzdbVar.zzC() == null) {
            return arrayList;
        }
        arrayList.add(new zzdp(zzdbVar, "Ob9vkrYwqwLnJveTtaSWm/WWJCjo/9DRtOCY3btkKa6pJtjMu6sI0iK41HSh10io", "UrT94Dq3ubetC7rQ64nVjqMQ53po9X61geGgrP+ILCU=", zzaxVar, zzdbVar.zzy(), 24));
        return arrayList;
    }
}
