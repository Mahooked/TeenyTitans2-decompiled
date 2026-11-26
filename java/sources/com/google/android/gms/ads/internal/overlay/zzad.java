package com.google.android.gms.ads.internal.overlay;

import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.support.annotation.Nullable;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zznb;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzad extends zzz {
    @Override // com.google.android.gms.ads.internal.overlay.zzz
    @Nullable
    public final zzy zza(Context context, zzaka zzakaVar, int i, boolean z, zznb zznbVar, zzaq zzaqVar) {
        ApplicationInfo applicationInfo = context.getApplicationInfo();
        if (applicationInfo == null || applicationInfo.targetSdkVersion >= 11) {
            return new zzd(context, z, zzakaVar.zzam().zzAt, zzaqVar, new zzar(context, zzakaVar.zziz(), zzakaVar.getRequestId(), zznbVar, zzakaVar.zziF()));
        }
        return null;
    }
}
