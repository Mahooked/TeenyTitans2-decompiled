package com.google.android.gms.internal;

import android.content.Context;
import android.content.pm.PackageManager;
import android.util.Log;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.internal.zzbo;
import java.util.ArrayList;

/* JADX INFO: loaded from: classes.dex */
public final class zzazn {
    private final String packageName;
    private final int zzazj;
    private String zzazk;
    private int zzazl;
    private String zzazm;
    private String zzazn;
    private final boolean zzazo;
    private int zzazp;
    private final zzazt zzazq;
    private final com.google.android.gms.common.util.zze zzazr;
    private zzazs zzazs;
    private final zzazq zzazt;
    private static Api.zzf<zzbab> zzajR = new Api.zzf<>();
    private static Api.zza<zzbab, Api.ApiOptions.NoOptions> zzajS = new zzazo();

    @Deprecated
    public static final Api<Api.ApiOptions.NoOptions> API = new Api<>("ClearcutLogger.API", zzajS, zzajR);
    private static final zzcqn[] zzazg = new zzcqn[0];
    private static final String[] zzazh = new String[0];
    private static final byte[][] zzazi = new byte[0][];

    public zzazn(Context context, int i, String str, String str2, String str3, boolean z, zzazt zzaztVar, com.google.android.gms.common.util.zze zzeVar, zzazs zzazsVar, zzazq zzazqVar) {
        this.zzazl = -1;
        this.zzazp = 0;
        this.packageName = context.getPackageName();
        this.zzazj = zzap(context);
        this.zzazl = -1;
        this.zzazk = str;
        this.zzazm = null;
        this.zzazn = null;
        this.zzazo = z;
        this.zzazq = zzaztVar;
        this.zzazr = zzeVar;
        this.zzazs = new zzazs();
        this.zzazp = 0;
        this.zzazt = zzazqVar;
        if (z) {
            zzbo.zzb(true, (Object) "can't be anonymous with an upload account");
        }
    }

    public zzazn(Context context, String str, String str2) {
        this(context, -1, str, null, null, false, zzazw.zzaq(context), com.google.android.gms.common.util.zzi.zzrY(), null, new zzbah(context));
    }

    private static int zzap(Context context) {
        try {
            return context.getPackageManager().getPackageInfo(context.getPackageName(), 0).versionCode;
        } catch (PackageManager.NameNotFoundException e) {
            Log.wtf("ClearcutLogger", "This can't happen.");
            return 0;
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static int[] zzb(ArrayList<Integer> arrayList) {
        if (arrayList == null) {
            return null;
        }
        int[] iArr = new int[arrayList.size()];
        ArrayList<Integer> arrayList2 = arrayList;
        int size = arrayList2.size();
        int i = 0;
        int i2 = 0;
        while (i < size) {
            Integer num = arrayList2.get(i);
            i++;
            iArr[i2] = num.intValue();
            i2++;
        }
        return iArr;
    }

    public final zzazp zze(byte[] bArr) {
        return new zzazp(this, bArr, (zzazo) null);
    }
}
