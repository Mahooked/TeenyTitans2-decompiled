package com.google.android.gms.internal;

import android.content.Context;
import android.net.Uri;
import android.os.Environment;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.HashMap;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.atomic.AtomicBoolean;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzmr {
    private Context mContext;
    private String zzGJ;
    private BlockingQueue<zznb> zzGL;
    private ExecutorService zzGM;
    private LinkedHashMap<String, String> zzGN = new LinkedHashMap<>();
    private Map<String, zzmv> zzGO = new HashMap();
    private AtomicBoolean zzGP = new AtomicBoolean(false);
    private File zzGQ;
    private String zzwH;

    public zzmr(Context context, String str, String str2, Map<String, String> map) {
        File externalStorageDirectory;
        this.mContext = context;
        this.zzwH = str;
        this.zzGJ = str2;
        this.zzGP.set(((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCS)).booleanValue());
        if (this.zzGP.get() && (externalStorageDirectory = Environment.getExternalStorageDirectory()) != null) {
            this.zzGQ = new File(externalStorageDirectory, "sdk_csi_data.txt");
        }
        for (Map.Entry<String, String> entry : map.entrySet()) {
            this.zzGN.put(entry.getKey(), entry.getValue());
        }
        this.zzGL = new ArrayBlockingQueue(30);
        this.zzGM = Executors.newSingleThreadExecutor();
        this.zzGM.execute(new zzms(this));
        this.zzGO.put("action", zzmv.zzGT);
        this.zzGO.put("ad_format", zzmv.zzGT);
        this.zzGO.put("e", zzmv.zzGU);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzdO() throws Throwable {
        FileOutputStream fileOutputStream;
        while (true) {
            try {
                zznb zznbVarTake = this.zzGL.take();
                String strZzdU = zznbVarTake.zzdU();
                if (!TextUtils.isEmpty(strZzdU)) {
                    Map<String, String> mapZza = zza(this.zzGN, zznbVarTake.zzdV());
                    Uri.Builder builderBuildUpon = Uri.parse(this.zzGJ).buildUpon();
                    for (Map.Entry<String, String> entry : mapZza.entrySet()) {
                        builderBuildUpon.appendQueryParameter(entry.getKey(), entry.getValue());
                    }
                    StringBuilder sb = new StringBuilder(builderBuildUpon.build().toString());
                    sb.append("&it=").append(strZzdU);
                    String string = sb.toString();
                    if (this.zzGP.get()) {
                        File file = this.zzGQ;
                        if (file != null) {
                            try {
                                fileOutputStream = new FileOutputStream(file, true);
                                try {
                                    try {
                                        fileOutputStream.write(string.getBytes());
                                        fileOutputStream.write(10);
                                        try {
                                            fileOutputStream.close();
                                        } catch (IOException e) {
                                            zzafr.zzc("CsiReporter: Cannot close file: sdk_csi_data.txt.", e);
                                        }
                                    } catch (IOException e2) {
                                        e = e2;
                                        zzafr.zzc("CsiReporter: Cannot write to file: sdk_csi_data.txt.", e);
                                        if (fileOutputStream != null) {
                                            try {
                                                fileOutputStream.close();
                                            } catch (IOException e3) {
                                                zzafr.zzc("CsiReporter: Cannot close file: sdk_csi_data.txt.", e3);
                                            }
                                        }
                                    }
                                } catch (Throwable th) {
                                    th = th;
                                    if (fileOutputStream != null) {
                                        try {
                                            fileOutputStream.close();
                                        } catch (IOException e4) {
                                            zzafr.zzc("CsiReporter: Cannot close file: sdk_csi_data.txt.", e4);
                                        }
                                    }
                                    throw th;
                                }
                            } catch (IOException e5) {
                                e = e5;
                                fileOutputStream = null;
                            } catch (Throwable th2) {
                                th = th2;
                                fileOutputStream = null;
                            }
                        } else {
                            zzafr.zzaT("CsiReporter: File doesn't exists. Cannot write CSI data to file.");
                        }
                    } else {
                        com.google.android.gms.ads.internal.zzbs.zzbz();
                        zzagz.zzd(this.mContext, this.zzwH, string);
                    }
                }
            } catch (InterruptedException e6) {
                zzafr.zzc("CsiReporter:reporter interrupted", e6);
                return;
            }
        }
    }

    public final zzmv zzM(String str) {
        zzmv zzmvVar = this.zzGO.get(str);
        return zzmvVar != null ? zzmvVar : zzmv.zzGS;
    }

    final Map<String, String> zza(Map<String, String> map, @Nullable Map<String, String> map2) {
        LinkedHashMap linkedHashMap = new LinkedHashMap(map);
        if (map2 == null) {
            return linkedHashMap;
        }
        for (Map.Entry<String, String> entry : map2.entrySet()) {
            String key = entry.getKey();
            String value = entry.getValue();
            linkedHashMap.put(key, zzM(key).zzg((String) linkedHashMap.get(key), value));
        }
        return linkedHashMap;
    }

    public final boolean zza(zznb zznbVar) {
        return this.zzGL.offer(zznbVar);
    }

    public final void zze(@Nullable List<String> list) {
        if (list == null || list.isEmpty()) {
            return;
        }
        this.zzGN.put("e", TextUtils.join(",", list));
    }
}
