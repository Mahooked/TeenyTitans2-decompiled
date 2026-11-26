package com.google.android.gms.internal;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.pm.ResolveInfo;
import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.customtabs.CustomTabsCallback;
import android.support.customtabs.CustomTabsClient;
import android.support.customtabs.CustomTabsServiceConnection;
import android.support.customtabs.CustomTabsSession;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zznl implements aeq {

    @Nullable
    private CustomTabsSession zzHi;

    @Nullable
    private CustomTabsClient zzHj;

    @Nullable
    private CustomTabsServiceConnection zzHk;

    @Nullable
    private zznm zzHl;

    public static boolean zzi(Context context) {
        PackageManager packageManager = context.getPackageManager();
        if (packageManager == null) {
            return false;
        }
        Intent intent = new Intent("android.intent.action.VIEW", Uri.parse("http://www.example.com"));
        ResolveInfo resolveInfoResolveActivity = packageManager.resolveActivity(intent, 0);
        List<ResolveInfo> listQueryIntentActivities = packageManager.queryIntentActivities(intent, 65536);
        if (listQueryIntentActivities == null || resolveInfoResolveActivity == null) {
            return false;
        }
        for (int i = 0; i < listQueryIntentActivities.size(); i++) {
            if (resolveInfoResolveActivity.activityInfo.name.equals(listQueryIntentActivities.get(i).activityInfo.name)) {
                return resolveInfoResolveActivity.activityInfo.packageName.equals(aeo.zzbU(context));
            }
        }
        return false;
    }

    public final boolean mayLaunchUrl(Uri uri, Bundle bundle, List<Bundle> list) {
        CustomTabsSession customTabsSessionZzdY;
        if (this.zzHj == null || (customTabsSessionZzdY = zzdY()) == null) {
            return false;
        }
        return customTabsSessionZzdY.mayLaunchUrl(uri, (Bundle) null, (List) null);
    }

    @Override // com.google.android.gms.internal.aeq
    public final void zza(CustomTabsClient customTabsClient) {
        this.zzHj = customTabsClient;
        this.zzHj.warmup(0L);
        if (this.zzHl != null) {
            this.zzHl.zzea();
        }
    }

    public final void zza(zznm zznmVar) {
        this.zzHl = zznmVar;
    }

    public final void zzc(Activity activity) {
        if (this.zzHk == null) {
            return;
        }
        activity.unbindService(this.zzHk);
        this.zzHj = null;
        this.zzHi = null;
        this.zzHk = null;
    }

    public final void zzd(Activity activity) {
        String strZzbU;
        if (this.zzHj == null && (strZzbU = aeo.zzbU(activity)) != null) {
            this.zzHk = new aep(this);
            CustomTabsClient.bindCustomTabsService(activity, strZzbU, this.zzHk);
        }
    }

    @Nullable
    public final CustomTabsSession zzdY() {
        if (this.zzHj == null) {
            this.zzHi = null;
        } else if (this.zzHi == null) {
            this.zzHi = this.zzHj.newSession((CustomTabsCallback) null);
        }
        return this.zzHi;
    }

    @Override // com.google.android.gms.internal.aeq
    public final void zzdZ() {
        this.zzHj = null;
        this.zzHi = null;
        if (this.zzHl != null) {
            this.zzHl.zzeb();
        }
    }
}
