package com.google.android.gms.internal;

import android.content.Context;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.Callable;

/* JADX INFO: loaded from: classes.dex */
public final class zzct extends zzcs {
    private zzct(Context context, String str, boolean z) {
        super(context, str, z);
    }

    public static zzct zza(String str, Context context, boolean z) {
        zza(context, z);
        return new zzct(context, str, z);
    }

    @Override // com.google.android.gms.internal.zzcs
    protected final List<Callable<Void>> zzb(zzdb zzdbVar, zzax zzaxVar, zzau zzauVar) {
        if (zzdbVar.zzC() == null || !this.zzqk) {
            return super.zzb(zzdbVar, zzaxVar, zzauVar);
        }
        int iZzy = zzdbVar.zzy();
        ArrayList arrayList = new ArrayList();
        arrayList.addAll(super.zzb(zzdbVar, zzaxVar, zzauVar));
        arrayList.add(new zzdp(zzdbVar, "Ob9vkrYwqwLnJveTtaSWm/WWJCjo/9DRtOCY3btkKa6pJtjMu6sI0iK41HSh10io", "UrT94Dq3ubetC7rQ64nVjqMQ53po9X61geGgrP+ILCU=", zzaxVar, iZzy, 24));
        return arrayList;
    }
}
