package com.google.android.gms.internal;

import android.os.SystemClock;
import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.EOFException;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.Collections;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.Map;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
public final class zzag implements zzb {
    private final Map<String, zzai> zzav;
    private long zzaw;
    private final File zzax;
    private final int zzay;

    public zzag(File file) {
        this(file, 5242880);
    }

    private zzag(File file, int i) {
        this.zzav = new LinkedHashMap(16, 0.75f, true);
        this.zzaw = 0L;
        this.zzax = file;
        this.zzay = 5242880;
    }

    private final synchronized void remove(String str) {
        boolean zDelete = zze(str).delete();
        zzai zzaiVar = this.zzav.get(str);
        if (zzaiVar != null) {
            this.zzaw -= zzaiVar.size;
            this.zzav.remove(str);
        }
        if (!zDelete) {
            zzab.zzb("Could not delete cache entry for key=%s, filename=%s", str, zzd(str));
        }
    }

    private static int zza(InputStream inputStream) throws IOException {
        int i = inputStream.read();
        if (i == -1) {
            throw new EOFException();
        }
        return i;
    }

    static void zza(OutputStream outputStream, int i) throws IOException {
        outputStream.write(i & 255);
        outputStream.write((i >> 8) & 255);
        outputStream.write((i >> 16) & 255);
        outputStream.write(i >>> 24);
    }

    static void zza(OutputStream outputStream, long j) throws IOException {
        outputStream.write((byte) j);
        outputStream.write((byte) (j >>> 8));
        outputStream.write((byte) (j >>> 16));
        outputStream.write((byte) (j >>> 24));
        outputStream.write((byte) (j >>> 32));
        outputStream.write((byte) (j >>> 40));
        outputStream.write((byte) (j >>> 48));
        outputStream.write((byte) (j >>> 56));
    }

    static void zza(OutputStream outputStream, String str) throws IOException {
        byte[] bytes = str.getBytes(HTTP.UTF_8);
        zza(outputStream, bytes.length);
        outputStream.write(bytes, 0, bytes.length);
    }

    private final void zza(String str, zzai zzaiVar) {
        if (this.zzav.containsKey(str)) {
            this.zzaw = (zzaiVar.size - this.zzav.get(str).size) + this.zzaw;
        } else {
            this.zzaw += zzaiVar.size;
        }
        this.zzav.put(str, zzaiVar);
    }

    private static byte[] zza(InputStream inputStream, int i) throws IOException {
        byte[] bArr = new byte[i];
        int i2 = 0;
        while (i2 < i) {
            int i3 = inputStream.read(bArr, i2, i - i2);
            if (i3 == -1) {
                break;
            }
            i2 += i3;
        }
        if (i2 != i) {
            throw new IOException(new StringBuilder(50).append("Expected ").append(i).append(" bytes, read ").append(i2).append(" bytes").toString());
        }
        return bArr;
    }

    static int zzb(InputStream inputStream) throws IOException {
        return zza(inputStream) | 0 | (zza(inputStream) << 8) | (zza(inputStream) << 16) | (zza(inputStream) << 24);
    }

    static long zzc(InputStream inputStream) throws IOException {
        return 0 | (((long) zza(inputStream)) & 255) | ((((long) zza(inputStream)) & 255) << 8) | ((((long) zza(inputStream)) & 255) << 16) | ((((long) zza(inputStream)) & 255) << 24) | ((((long) zza(inputStream)) & 255) << 32) | ((((long) zza(inputStream)) & 255) << 40) | ((((long) zza(inputStream)) & 255) << 48) | ((((long) zza(inputStream)) & 255) << 56);
    }

    static String zzd(InputStream inputStream) throws IOException {
        return new String(zza(inputStream, (int) zzc(inputStream)), HTTP.UTF_8);
    }

    private static String zzd(String str) {
        int length = str.length() / 2;
        String strValueOf = String.valueOf(String.valueOf(str.substring(0, length).hashCode()));
        String strValueOf2 = String.valueOf(String.valueOf(str.substring(length).hashCode()));
        return strValueOf2.length() != 0 ? strValueOf.concat(strValueOf2) : new String(strValueOf);
    }

    private final File zze(String str) {
        return new File(this.zzax, zzd(str));
    }

    static Map<String, String> zze(InputStream inputStream) throws IOException {
        int iZzb = zzb(inputStream);
        Map<String, String> mapEmptyMap = iZzb == 0 ? Collections.emptyMap() : new HashMap<>(iZzb);
        for (int i = 0; i < iZzb; i++) {
            mapEmptyMap.put(zzd(inputStream).intern(), zzd(inputStream).intern());
        }
        return mapEmptyMap;
    }

    @Override // com.google.android.gms.internal.zzb
    public final synchronized void initialize() {
        BufferedInputStream bufferedInputStream;
        if (this.zzax.exists()) {
            File[] fileArrListFiles = this.zzax.listFiles();
            if (fileArrListFiles != null) {
                for (File file : fileArrListFiles) {
                    BufferedInputStream bufferedInputStream2 = null;
                    try {
                        bufferedInputStream = new BufferedInputStream(new FileInputStream(file));
                        try {
                            try {
                                zzai zzaiVarZzf = zzai.zzf(bufferedInputStream);
                                zzaiVarZzf.size = file.length();
                                zza(zzaiVarZzf.key, zzaiVarZzf);
                                try {
                                    bufferedInputStream.close();
                                } catch (IOException e) {
                                }
                            } catch (Throwable th) {
                                bufferedInputStream2 = bufferedInputStream;
                                th = th;
                                if (bufferedInputStream2 != null) {
                                    try {
                                        bufferedInputStream2.close();
                                    } catch (IOException e2) {
                                    }
                                }
                                throw th;
                            }
                        } catch (IOException e3) {
                            if (file != null) {
                                file.delete();
                            }
                            if (bufferedInputStream != null) {
                                try {
                                    bufferedInputStream.close();
                                } catch (IOException e4) {
                                }
                            }
                        }
                    } catch (IOException e5) {
                        bufferedInputStream = null;
                    } catch (Throwable th2) {
                        th = th2;
                    }
                }
            }
        } else if (!this.zzax.mkdirs()) {
            zzab.zzc("Unable to create cache dir %s", this.zzax.getAbsolutePath());
        }
    }

    /* JADX WARN: Code duplicated, block: B:53:0x00ac A[EXC_TOP_SPLITTER, SYNTHETIC] */
    @Override // com.google.android.gms.internal.zzb
    public final synchronized zzc zza(String str) {
        zzaj zzajVar;
        zzc zzcVar;
        zzaj zzajVar2;
        zzai zzaiVar = this.zzav.get(str);
        if (zzaiVar == null) {
            zzcVar = null;
        } else {
            try {
                File fileZze = zze(str);
                try {
                    zzajVar = new zzaj(new BufferedInputStream(new FileInputStream(fileZze)));
                    try {
                        zzai.zzf(zzajVar);
                        byte[] bArrZza = zza(zzajVar, (int) (fileZze.length() - ((long) zzajVar.zzaz)));
                        zzc zzcVar2 = new zzc();
                        zzcVar2.data = bArrZza;
                        zzcVar2.zza = zzaiVar.zza;
                        zzcVar2.zzb = zzaiVar.zzb;
                        zzcVar2.zzc = zzaiVar.zzc;
                        zzcVar2.zzd = zzaiVar.zzd;
                        zzcVar2.zze = zzaiVar.zze;
                        zzcVar2.zzf = zzaiVar.zzf;
                        try {
                            zzajVar.close();
                            zzcVar = zzcVar2;
                        } catch (IOException e) {
                            zzcVar = null;
                        }
                    } catch (IOException e2) {
                        e = e2;
                        zzajVar2 = zzajVar;
                        try {
                            zzab.zzb("%s: %s", fileZze.getAbsolutePath(), e.toString());
                            remove(str);
                            if (zzajVar2 != null) {
                                try {
                                    zzajVar2.close();
                                } catch (IOException e3) {
                                    zzcVar = null;
                                }
                            }
                            zzcVar = null;
                        } catch (Throwable th) {
                            th = th;
                            zzajVar = zzajVar2;
                            if (zzajVar != null) {
                                try {
                                    zzajVar.close();
                                } catch (IOException e4) {
                                    zzcVar = null;
                                }
                            }
                            throw th;
                        }
                    } catch (NegativeArraySizeException e5) {
                        e = e5;
                        zzab.zzb("%s: %s", fileZze.getAbsolutePath(), e.toString());
                        remove(str);
                        if (zzajVar != null) {
                            try {
                                zzajVar.close();
                            } catch (IOException e6) {
                                zzcVar = null;
                            }
                        }
                        zzcVar = null;
                    }
                } catch (IOException e7) {
                    e = e7;
                    zzajVar2 = null;
                } catch (NegativeArraySizeException e8) {
                    e = e8;
                    zzajVar = null;
                } catch (Throwable th2) {
                    th = th2;
                    zzajVar = null;
                    if (zzajVar != null) {
                        zzajVar.close();
                    }
                    throw th;
                }
            } catch (Throwable th3) {
                th = th3;
            }
        }
        return zzcVar;
    }

    @Override // com.google.android.gms.internal.zzb
    public final synchronized void zza(String str, zzc zzcVar) {
        int i;
        int i2 = 0;
        synchronized (this) {
            int length = zzcVar.data.length;
            if (this.zzaw + ((long) length) >= this.zzay) {
                if (zzab.DEBUG) {
                    zzab.zza("Pruning old cache entries.", new Object[0]);
                }
                long j = this.zzaw;
                long jElapsedRealtime = SystemClock.elapsedRealtime();
                Iterator<Map.Entry<String, zzai>> it = this.zzav.entrySet().iterator();
                while (true) {
                    if (!it.hasNext()) {
                        i = i2;
                        break;
                    }
                    zzai value = it.next().getValue();
                    if (zze(value.key).delete()) {
                        this.zzaw -= value.size;
                    } else {
                        zzab.zzb("Could not delete cache entry for key=%s, filename=%s", value.key, zzd(value.key));
                    }
                    it.remove();
                    i = i2 + 1;
                    if (this.zzaw + ((long) length) < this.zzay * 0.9f) {
                        break;
                    } else {
                        i2 = i;
                    }
                }
                if (zzab.DEBUG) {
                    zzab.zza("pruned %d files, %d bytes, %d ms", Integer.valueOf(i), Long.valueOf(this.zzaw - j), Long.valueOf(SystemClock.elapsedRealtime() - jElapsedRealtime));
                }
            }
            File fileZze = zze(str);
            try {
                BufferedOutputStream bufferedOutputStream = new BufferedOutputStream(new FileOutputStream(fileZze));
                zzai zzaiVar = new zzai(str, zzcVar);
                if (!zzaiVar.zza(bufferedOutputStream)) {
                    bufferedOutputStream.close();
                    zzab.zzb("Failed to write header for %s", fileZze.getAbsolutePath());
                    throw new IOException();
                }
                bufferedOutputStream.write(zzcVar.data);
                bufferedOutputStream.close();
                zza(str, zzaiVar);
            } catch (IOException e) {
                if (!fileZze.delete()) {
                    zzab.zzb("Could not clean up file %s", fileZze.getAbsolutePath());
                }
            }
        }
    }
}
