package com.google.android.gms.internal;

import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.os.RemoteException;
import android.text.TextUtils;
import com.google.android.gms.common.api.GoogleApiClient;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public final class zzcsn extends com.google.android.gms.common.internal.zzz<zzcry> {
    private final Context mContext;

    public zzcsn(Context context, Looper looper, com.google.android.gms.common.internal.zzq zzqVar, GoogleApiClient.ConnectionCallbacks connectionCallbacks, GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener) {
        super(context, looper, 45, zzqVar, connectionCallbacks, onConnectionFailedListener);
        this.mContext = context;
    }

    private final String zzeH(String str) {
        ApplicationInfo applicationInfo;
        Bundle bundle;
        try {
            PackageManager packageManager = this.mContext.getPackageManager();
            if (packageManager != null && (applicationInfo = packageManager.getApplicationInfo(this.mContext.getPackageName(), 128)) != null && (bundle = applicationInfo.metaData) != null) {
                return (String) bundle.get(str);
            }
            return null;
        } catch (PackageManager.NameNotFoundException e) {
            return null;
        }
    }

    public final void zza(zzcrw zzcrwVar, List<Integer> list, int i, String str, String str2) throws RemoteException {
        String strZzeH = str2 == null ? zzeH("com.google.android.safetynet.API_KEY") : str2;
        int[] iArr = new int[list.size()];
        int i2 = 0;
        while (true) {
            int i3 = i2;
            if (i3 >= list.size()) {
                ((zzcry) zzrf()).zza(zzcrwVar, strZzeH, iArr, i, str);
                return;
            } else {
                iArr[i3] = list.get(i3).intValue();
                i2 = i3 + 1;
            }
        }
    }

    public final void zzb(zzcrw zzcrwVar, byte[] bArr, String str) throws RemoteException {
        if (TextUtils.isEmpty(str)) {
            str = zzeH("com.google.android.safetynet.ATTEST_API_KEY");
        }
        ((zzcry) zzrf()).zza(zzcrwVar, bArr, str);
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final /* synthetic */ IInterface zzd(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.safetynet.internal.ISafetyNetService");
        return iInterfaceQueryLocalInterface instanceof zzcry ? (zzcry) iInterfaceQueryLocalInterface : new zzcrz(iBinder);
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final String zzdb() {
        return "com.google.android.gms.safetynet.service.START";
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final String zzdc() {
        return "com.google.android.gms.safetynet.internal.ISafetyNetService";
    }
}
