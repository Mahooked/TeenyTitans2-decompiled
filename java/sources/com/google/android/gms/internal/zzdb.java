package com.google.android.gms.internal;

import android.content.Context;
import android.os.Build;
import android.support.annotation.VisibleForTesting;
import android.support.v4.widget.ExploreByTouchHelper;
import android.util.Log;
import android.util.Pair;
import com.google.android.gms.ads.identifier.AdvertisingIdClient;
import com.google.android.gms.common.GooglePlayServicesNotAvailableException;
import com.google.android.gms.common.GooglePlayServicesRepairableException;
import com.google.android.gms.common.api.GoogleApiClient;
import dalvik.system.DexClassLoader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.lang.reflect.Method;
import java.security.NoSuchAlgorithmException;
import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.Future;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;

/* JADX INFO: loaded from: classes.dex */
public class zzdb {
    private static final String TAG = zzdb.class.getSimpleName();
    private static Object zzqQ = new Object();
    private static com.google.android.gms.common.zze zzqS = null;
    protected Context zzqD;
    private Context zzqE;
    private ExecutorService zzqF;
    private DexClassLoader zzqG;
    private zzcw zzqH;
    private byte[] zzqI;
    private zzcn zzqN;
    private volatile AdvertisingIdClient zzqJ = null;
    private volatile boolean zzqk = false;
    private Future zzqK = null;
    private volatile zzax zzqL = null;
    private Future zzqM = null;
    private GoogleApiClient zzqO = null;
    protected boolean zzqP = false;
    private boolean zzqR = false;
    protected boolean zzqT = false;
    private boolean zzqV = false;
    private Map<Pair<String, String>, zzea> zzqU = new HashMap();

    private zzdb(Context context) {
        this.zzqD = context;
        this.zzqE = context.getApplicationContext();
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzM() {
        try {
            if (this.zzqJ != null || this.zzqE == null) {
                return;
            }
            AdvertisingIdClient advertisingIdClient = new AdvertisingIdClient(this.zzqE);
            advertisingIdClient.start();
            this.zzqJ = advertisingIdClient;
        } catch (GooglePlayServicesNotAvailableException | GooglePlayServicesRepairableException | IOException e) {
            this.zzqJ = null;
        }
    }

    @VisibleForTesting
    private final zzax zzN() {
        try {
            return zzcaq.zzn(this.zzqD, this.zzqD.getPackageName(), Integer.toString(this.zzqD.getPackageManager().getPackageInfo(this.zzqD.getPackageName(), 0).versionCode));
        } catch (Throwable th) {
            return null;
        }
    }

    public static zzdb zza(Context context, String str, String str2, boolean z) throws Throwable {
        zzdb zzdbVar = new zzdb(context);
        try {
            zzdbVar.zzqF = Executors.newCachedThreadPool();
            zzdbVar.zzqk = z;
            if (z) {
                zzdbVar.zzqK = zzdbVar.zzqF.submit(new zzdc(zzdbVar));
            }
            zzdbVar.zzqF.execute(new zzde(zzdbVar));
            try {
                zzqS = com.google.android.gms.common.zze.zzoW();
                zzdbVar.zzqP = com.google.android.gms.common.zze.zzau(zzdbVar.zzqD) > 0;
                zzdbVar.zzqR = zzqS.isGooglePlayServicesAvailable(zzdbVar.zzqD) == 0;
                if (zzdbVar.zzqD.getApplicationContext() != null) {
                    zzdbVar.zzqO = new GoogleApiClient.Builder(zzdbVar.zzqD).addApi(zzazn.API).build();
                }
            } catch (Throwable th) {
            }
            zzdbVar.zza(0, true);
            if (zzdg.zzS() && ((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFa)).booleanValue()) {
                throw new IllegalStateException("Task Context initialization must not be called from the UI thread.");
            }
            zzdbVar.zzqH = new zzcw(null);
            try {
                zzdbVar.zzqI = zzdbVar.zzqH.zzl(str);
                try {
                    try {
                        try {
                            File cacheDir = zzdbVar.zzqD.getCacheDir();
                            if (cacheDir == null && (cacheDir = zzdbVar.zzqD.getDir("dex", 0)) == null) {
                                throw new zzcy();
                            }
                            File file = cacheDir;
                            File file2 = new File(String.format("%s/%s.jar", file, "1489418796403"));
                            if (!file2.exists()) {
                                byte[] bArrZza = zzdbVar.zzqH.zza(zzdbVar.zzqI, str2);
                                file2.createNewFile();
                                FileOutputStream fileOutputStream = new FileOutputStream(file2);
                                fileOutputStream.write(bArrZza, 0, bArrZza.length);
                                fileOutputStream.close();
                            }
                            zzdbVar.zzb(file, "1489418796403");
                            try {
                                zzdbVar.zzqG = new DexClassLoader(file2.getAbsolutePath(), file.getAbsolutePath(), null, zzdbVar.zzqD.getClassLoader());
                                zza(file2);
                                zzdbVar.zza(file, "1489418796403");
                                zzm(String.format("%s/%s.dex", file, "1489418796403"));
                                zzdbVar.zzqN = new zzcn(zzdbVar);
                                zzdbVar.zzqV = true;
                            } catch (Throwable th2) {
                                zza(file2);
                                zzdbVar.zza(file, "1489418796403");
                                zzm(String.format("%s/%s.dex", file, "1489418796403"));
                                throw th2;
                            }
                        } catch (FileNotFoundException e) {
                            throw new zzcy(e);
                        }
                    } catch (IOException e2) {
                        throw new zzcy(e2);
                    }
                } catch (zzcx e3) {
                    throw new zzcy(e3);
                } catch (NullPointerException e4) {
                    throw new zzcy(e4);
                }
            } catch (zzcx e5) {
                throw new zzcy(e5);
            }
        } catch (zzcy e6) {
        }
        return zzdbVar;
    }

    private static void zza(File file) {
        if (file.exists()) {
            file.delete();
        } else {
            Log.d(TAG, String.format("File %s not found. No need for deletion", file.getAbsolutePath()));
        }
    }

    private final void zza(File file, String str) throws Throwable {
        FileInputStream fileInputStream;
        FileOutputStream fileOutputStream;
        FileOutputStream fileOutputStream2 = null;
        fileOutputStream2 = null;
        fileInputStream = null;
        fileInputStream = null;
        FileInputStream fileInputStream2 = null;
        File file2 = new File(String.format("%s/%s.tmp", file, str));
        if (file2.exists()) {
            return;
        }
        File file3 = new File(String.format("%s/%s.dex", file, str));
        if (file3.exists()) {
            long length = file3.length();
            if (length > 0) {
                byte[] bArr = new byte[(int) length];
                try {
                    fileInputStream = new FileInputStream(file3);
                    try {
                        if (fileInputStream.read(bArr) <= 0) {
                            try {
                                fileInputStream.close();
                            } catch (IOException e) {
                            }
                            zza(file3);
                            return;
                        }
                        zzbc zzbcVar = new zzbc();
                        zzbcVar.zzcG = Build.VERSION.SDK.getBytes();
                        zzbcVar.zzcF = str.getBytes();
                        byte[] bytes = this.zzqH.zzc(this.zzqI, bArr).getBytes();
                        zzbcVar.data = bytes;
                        zzbcVar.zzcE = zzbv.zzb(bytes);
                        file2.createNewFile();
                        fileOutputStream = new FileOutputStream(file2);
                        try {
                            byte[] bArrZzc = adp.zzc(zzbcVar);
                            fileOutputStream.write(bArrZzc, 0, bArrZzc.length);
                            fileOutputStream.close();
                            try {
                                fileInputStream.close();
                            } catch (IOException e2) {
                            }
                            try {
                                fileOutputStream.close();
                            } catch (IOException e3) {
                            }
                            zza(file3);
                            return;
                        } catch (zzcx e4) {
                            fileInputStream2 = fileInputStream;
                        } catch (IOException e5) {
                            fileInputStream2 = fileInputStream;
                        } catch (NoSuchAlgorithmException e6) {
                            fileInputStream2 = fileInputStream;
                        } catch (Throwable th) {
                            fileOutputStream2 = fileOutputStream;
                            th = th;
                            if (fileInputStream != null) {
                                try {
                                    fileInputStream.close();
                                } catch (IOException e7) {
                                }
                            }
                            if (fileOutputStream2 != null) {
                                try {
                                    fileOutputStream2.close();
                                } catch (IOException e8) {
                                }
                            }
                            zza(file3);
                            throw th;
                        }
                    } catch (zzcx e9) {
                        fileOutputStream = null;
                        fileInputStream2 = fileInputStream;
                    } catch (IOException e10) {
                        fileOutputStream = null;
                        fileInputStream2 = fileInputStream;
                    } catch (NoSuchAlgorithmException e11) {
                        fileOutputStream = null;
                        fileInputStream2 = fileInputStream;
                    } catch (Throwable th2) {
                        th = th2;
                    }
                } catch (zzcx e12) {
                    fileOutputStream = null;
                } catch (IOException e13) {
                    fileOutputStream = null;
                } catch (NoSuchAlgorithmException e14) {
                    fileOutputStream = null;
                } catch (Throwable th3) {
                    th = th3;
                    fileInputStream = null;
                }
                if (fileInputStream2 != null) {
                    try {
                        fileInputStream2.close();
                    } catch (IOException e15) {
                    }
                }
                if (fileOutputStream != null) {
                    try {
                        fileOutputStream.close();
                    } catch (IOException e16) {
                    }
                }
                zza(file3);
            }
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static boolean zza(int i, zzax zzaxVar) {
        if (i < 4) {
            if (zzaxVar == null) {
                return true;
            }
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFd)).booleanValue() && (zzaxVar.zzaT == null || zzaxVar.zzaT.equals("0000000000000000000000000000000000000000000000000000000000000000"))) {
                return true;
            }
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFe)).booleanValue() && (zzaxVar.zzbZ == null || zzaxVar.zzbZ.zzcx == null || zzaxVar.zzbZ.zzcx.longValue() == -2)) {
                return true;
            }
        }
        return false;
    }

    /* JADX WARN: Code duplicated, block: B:87:0x00ce A[EXC_TOP_SPLITTER, SYNTHETIC] */
    /* JADX WARN: Code duplicated, block: B:89:0x00c9 A[EXC_TOP_SPLITTER, SYNTHETIC] */
    /* JADX WARN: Multi-variable type inference failed */
    /* JADX WARN: Type inference failed for: r0v16, types: [boolean] */
    /* JADX WARN: Type inference failed for: r1v1 */
    /* JADX WARN: Type inference failed for: r1v10 */
    /* JADX WARN: Type inference failed for: r1v13 */
    /* JADX WARN: Type inference failed for: r1v15 */
    /* JADX WARN: Type inference failed for: r1v17 */
    /* JADX WARN: Type inference failed for: r1v2 */
    /* JADX WARN: Type inference failed for: r1v3 */
    /* JADX WARN: Type inference failed for: r1v6, types: [java.io.FileInputStream] */
    /* JADX WARN: Type inference failed for: r1v8 */
    /* JADX WARN: Type inference failed for: r1v9 */
    /* JADX WARN: Type inference failed for: r4v2 */
    /* JADX WARN: Type inference failed for: r4v3, types: [java.io.FileInputStream] */
    /* JADX WARN: Type inference failed for: r4v4, types: [java.io.FileInputStream] */
    private final boolean zzb(File file, String str) throws Throwable {
        ?? fileInputStream;
        FileOutputStream fileOutputStream;
        ?? r1;
        boolean z;
        FileOutputStream fileOutputStream2 = null;
        IOException e = null;
        File file2 = new File(String.format("%s/%s.tmp", file, str));
        if (!file2.exists()) {
            return false;
        }
        File file3 = new File(String.format("%s/%s.dex", file, str));
        if (file3.exists()) {
            return false;
        }
        try {
            long length = file2.length();
            if (length <= 0) {
                zza(file2);
                z = false;
            } else {
                byte[] bArr = new byte[(int) length];
                fileInputStream = new FileInputStream(file2);
                try {
                    if (fileInputStream.read(bArr) <= 0) {
                        Log.d(TAG, "Cannot read the cache data.");
                        zza(file2);
                        try {
                            fileInputStream.close();
                        } catch (IOException e2) {
                        }
                        z = false;
                    } else {
                        zzbc zzbcVar = (zzbc) adp.zza(new zzbc(), bArr);
                        if (str.equals(new String(zzbcVar.zzcF)) && Arrays.equals(zzbcVar.zzcE, zzbv.zzb(zzbcVar.data)) && Arrays.equals(zzbcVar.zzcG, Build.VERSION.SDK.getBytes())) {
                            byte[] bArrZza = this.zzqH.zza(this.zzqI, new String(zzbcVar.data));
                            file3.createNewFile();
                            fileOutputStream = new FileOutputStream(file3);
                            fileOutputStream2 = null;
                            try {
                                fileOutputStream.write(bArrZza, 0, bArrZza.length);
                                try {
                                    fileInputStream.close();
                                } catch (IOException e3) {
                                    e = e3;
                                }
                                try {
                                    fileOutputStream.close();
                                } catch (IOException e4) {
                                }
                                z = true;
                            } catch (zzcx e5) {
                                r1 = fileInputStream;
                                if (r1 != 0) {
                                    try {
                                        r1.close();
                                    } catch (IOException e6) {
                                    }
                                }
                                if (fileOutputStream != null) {
                                    try {
                                        fileOutputStream.close();
                                    } catch (IOException e7) {
                                    }
                                }
                                return e;
                            } catch (IOException e8) {
                                r1 = fileInputStream;
                                if (r1 != 0) {
                                    r1.close();
                                }
                                if (fileOutputStream != null) {
                                    fileOutputStream.close();
                                }
                                return e;
                            } catch (NoSuchAlgorithmException e9) {
                                r1 = fileInputStream;
                                if (r1 != 0) {
                                    r1.close();
                                }
                                if (fileOutputStream != null) {
                                    fileOutputStream.close();
                                }
                                return e;
                            } catch (Throwable th) {
                                fileOutputStream2 = fileOutputStream;
                                th = th;
                                if (fileInputStream != 0) {
                                    try {
                                        fileInputStream.close();
                                    } catch (IOException e10) {
                                    }
                                }
                                if (fileOutputStream2 == null) {
                                    throw th;
                                }
                                try {
                                    fileOutputStream2.close();
                                    throw th;
                                } catch (IOException e11) {
                                    throw th;
                                }
                            }
                        } else {
                            zza(file2);
                            try {
                                fileInputStream.close();
                            } catch (IOException e12) {
                            }
                            z = false;
                        }
                    }
                } catch (zzcx e13) {
                    fileOutputStream = fileOutputStream2;
                    r1 = fileInputStream;
                } catch (IOException e14) {
                    fileOutputStream = fileOutputStream2;
                    r1 = fileInputStream;
                } catch (NoSuchAlgorithmException e15) {
                    fileOutputStream = fileOutputStream2;
                    r1 = fileInputStream;
                } catch (Throwable th2) {
                    th = th2;
                }
            }
            return z;
        } catch (zzcx e16) {
            fileOutputStream = fileOutputStream2;
            r1 = fileOutputStream2;
        } catch (IOException e17) {
            fileOutputStream = fileOutputStream2;
            r1 = fileOutputStream2;
        } catch (NoSuchAlgorithmException e18) {
            fileOutputStream = fileOutputStream2;
            r1 = fileOutputStream2;
        } catch (Throwable th3) {
            th = th3;
            fileInputStream = fileOutputStream2;
        }
    }

    private static void zzm(String str) {
        zza(new File(str));
    }

    public final Context getApplicationContext() {
        return this.zzqE;
    }

    public final Context getContext() {
        return this.zzqD;
    }

    public final boolean isInitialized() {
        return this.zzqV;
    }

    public final ExecutorService zzC() {
        return this.zzqF;
    }

    public final DexClassLoader zzD() {
        return this.zzqG;
    }

    public final zzcw zzE() {
        return this.zzqH;
    }

    public final byte[] zzF() {
        return this.zzqI;
    }

    public final GoogleApiClient zzG() {
        return this.zzqO;
    }

    public final boolean zzH() {
        return this.zzqP;
    }

    public final zzcn zzI() {
        return this.zzqN;
    }

    public final boolean zzJ() {
        return this.zzqR;
    }

    public final zzax zzK() {
        return this.zzqL;
    }

    public final Future zzL() {
        return this.zzqM;
    }

    public final AdvertisingIdClient zzO() {
        if (!this.zzqk) {
            return null;
        }
        if (this.zzqJ != null) {
            return this.zzqJ;
        }
        if (this.zzqK != null) {
            try {
                this.zzqK.get(2000L, TimeUnit.MILLISECONDS);
                this.zzqK = null;
            } catch (InterruptedException e) {
            } catch (ExecutionException e2) {
            } catch (TimeoutException e3) {
                this.zzqK.cancel(true);
            }
        }
        return this.zzqJ;
    }

    public final void zzP() {
        try {
            synchronized (zzqQ) {
                if (this.zzqT) {
                    return;
                }
                if (!this.zzqR || this.zzqO == null) {
                    this.zzqT = false;
                } else {
                    this.zzqO.connect();
                    this.zzqT = true;
                }
            }
        } catch (Throwable th) {
        }
    }

    public final void zzQ() {
        synchronized (zzqQ) {
            if (this.zzqT && this.zzqO != null) {
                this.zzqO.disconnect();
                this.zzqT = false;
            }
        }
    }

    @VisibleForTesting
    final void zza(int i, boolean z) {
        if (this.zzqR) {
            Future<?> futureSubmit = this.zzqF.submit(new zzdd(this, i, z));
            if (i == 0) {
                this.zzqM = futureSubmit;
            }
        }
    }

    public final boolean zza(String str, String str2, Class<?>... clsArr) {
        if (this.zzqU.containsKey(new Pair(str, str2))) {
            return false;
        }
        this.zzqU.put(new Pair<>(str, str2), new zzea(this, str, str2, clsArr));
        return true;
    }

    @VisibleForTesting
    final zzax zzb(int i, boolean z) {
        if (i > 0 && z) {
            try {
                Thread.sleep(i * 1000);
            } catch (InterruptedException e) {
            }
        }
        return zzN();
    }

    public final Method zzc(String str, String str2) {
        zzea zzeaVar = this.zzqU.get(new Pair(str, str2));
        if (zzeaVar == null) {
            return null;
        }
        return zzeaVar.zzY();
    }

    public final int zzy() {
        return this.zzqN != null ? zzcn.zzy() : ExploreByTouchHelper.INVALID_ID;
    }
}
