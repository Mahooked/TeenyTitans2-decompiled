package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.app.DownloadManager;

/* JADX INFO: loaded from: classes.dex */
@TargetApi(9)
public class zzahg extends zzahe {
    public zzahg() {
        super();
    }

    @Override // com.google.android.gms.internal.zzahe
    public boolean zza(DownloadManager.Request request) {
        request.setShowRunningNotification(true);
        return true;
    }

    @Override // com.google.android.gms.internal.zzahe
    public final int zzhT() {
        return 6;
    }

    @Override // com.google.android.gms.internal.zzahe
    public final int zzhU() {
        return 7;
    }
}
