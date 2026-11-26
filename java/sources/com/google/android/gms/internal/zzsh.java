package com.google.android.gms.internal;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.nio.ByteBuffer;
import java.nio.channels.Channels;
import java.nio.channels.FileChannel;
import java.nio.channels.ReadableByteChannel;
import java.text.DecimalFormat;
import java.util.Collections;
import java.util.HashSet;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzsh extends zzsb {
    private static final Set<String> zzJX = Collections.synchronizedSet(new HashSet());
    private static final DecimalFormat zzJY = new DecimalFormat("#,###");
    private File zzJZ;
    private boolean zzKa;

    public zzsh(zzaka zzakaVar) {
        super(zzakaVar);
        File cacheDir = this.mContext.getCacheDir();
        if (cacheDir == null) {
            zzafr.zzaT("Context.getCacheDir() returned null");
            return;
        }
        this.zzJZ = new File(cacheDir, "admobVideoStreams");
        if (!this.zzJZ.isDirectory() && !this.zzJZ.mkdirs()) {
            String strValueOf = String.valueOf(this.zzJZ.getAbsolutePath());
            zzafr.zzaT(strValueOf.length() != 0 ? "Could not create preload cache directory at ".concat(strValueOf) : new String("Could not create preload cache directory at "));
            this.zzJZ = null;
        } else {
            if (this.zzJZ.setReadable(true, false) && this.zzJZ.setExecutable(true, false)) {
                return;
            }
            String strValueOf2 = String.valueOf(this.zzJZ.getAbsolutePath());
            zzafr.zzaT(strValueOf2.length() != 0 ? "Could not set cache file permissions at ".concat(strValueOf2) : new String("Could not set cache file permissions at "));
            this.zzJZ = null;
        }
    }

    private final File zzb(File file) {
        return new File(this.zzJZ, String.valueOf(file.getName()).concat(".done"));
    }

    @Override // com.google.android.gms.internal.zzsb
    public final void abort() {
        this.zzKa = true;
    }

    /* JADX WARN: Code duplicated, block: B:180:0x050b  */
    /* JADX WARN: Multi-variable type inference failed */
    /* JADX WARN: Type inference failed for: r3v18 */
    /* JADX WARN: Type inference failed for: r3v27 */
    /* JADX WARN: Type inference failed for: r3v28 */
    /* JADX WARN: Type inference failed for: r3v41 */
    /* JADX WARN: Type inference failed for: r3v42, types: [com.google.android.gms.internal.zzsb] */
    /* JADX WARN: Type inference failed for: r3v51 */
    /* JADX WARN: Type inference failed for: r3v52 */
    /* JADX WARN: Type inference failed for: r3v56 */
    /* JADX WARN: Type inference failed for: r3v64 */
    /* JADX WARN: Type inference failed for: r3v65 */
    /* JADX WARN: Type inference failed for: r4v17, types: [long] */
    /* JADX WARN: Type inference failed for: r4v21, types: [java.lang.String] */
    @Override // com.google.android.gms.internal.zzsb
    public final boolean zzU(String str) {
        int i;
        zzmm zzmmVarZzbL;
        String str2;
        int responseCode;
        boolean zDelete;
        long jLastModified;
        File file;
        if (this.zzJZ == null) {
            zza(str, null, "noCacheDir", null);
            return false;
        }
        do {
            if (this.zzJZ == null) {
                i = 0;
            } else {
                int i2 = 0;
                for (File file2 : this.zzJZ.listFiles()) {
                    if (!file2.getName().endsWith(".done")) {
                        i2++;
                    }
                }
                i = i2;
            }
            if (i <= ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCm)).intValue()) {
                zzji.zzds();
                File file3 = new File(this.zzJZ, zzaiy.zzaR(str));
                File fileZzb = zzb(file3);
                if (file3.isFile() && fileZzb.isFile()) {
                    int length = (int) file3.length();
                    String strValueOf = String.valueOf(str);
                    zzafr.zzaC(strValueOf.length() != 0 ? "Stream cache hit at ".concat(strValueOf) : new String("Stream cache hit at "));
                    zza(str, file3.getAbsolutePath(), length);
                    return true;
                }
                String strValueOf2 = String.valueOf(this.zzJZ.getAbsolutePath());
                String strValueOf3 = String.valueOf(str);
                String strConcat = strValueOf3.length() != 0 ? strValueOf2.concat(strValueOf3) : new String(strValueOf2);
                synchronized (zzJX) {
                    if (zzJX.contains(strConcat)) {
                        String strValueOf4 = String.valueOf(str);
                        zzafr.zzaT(strValueOf4.length() != 0 ? "Stream cache already in progress at ".concat(strValueOf4) : new String("Stream cache already in progress at "));
                        zza(str, file3.getAbsolutePath(), "inProgress", null);
                        return false;
                    }
                    zzJX.add(strConcat);
                    FileOutputStream fileOutputStream = null;
                    try {
                        com.google.android.gms.ads.internal.zzbs.zzbM();
                        HttpURLConnection httpURLConnectionZzb = zzajo.zzb(str, ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCs)).intValue());
                        if (!(httpURLConnectionZzb instanceof HttpURLConnection) || (responseCode = httpURLConnectionZzb.getResponseCode()) < 400) {
                            int contentLength = httpURLConnectionZzb.getContentLength();
                            if (contentLength < 0) {
                                String strValueOf5 = String.valueOf(str);
                                zzafr.zzaT(strValueOf5.length() != 0 ? "Stream cache aborted, missing content-length header at ".concat(strValueOf5) : new String("Stream cache aborted, missing content-length header at "));
                                zza(str, file3.getAbsolutePath(), "contentLengthMissing", null);
                                zzJX.remove(strConcat);
                                return false;
                            }
                            String str3 = zzJY.format(contentLength);
                            int iIntValue = ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCn)).intValue();
                            if (contentLength > iIntValue) {
                                zzafr.zzaT(new StringBuilder(String.valueOf(str3).length() + 33 + String.valueOf(str).length()).append("Content length ").append(str3).append(" exceeds limit at ").append(str).toString());
                                String strValueOf6 = String.valueOf(str3);
                                zza(str, file3.getAbsolutePath(), "sizeExceeded", strValueOf6.length() != 0 ? "File too big for full file cache. Size: ".concat(strValueOf6) : new String("File too big for full file cache. Size: "));
                                zzJX.remove(strConcat);
                                return false;
                            }
                            StringBuilder sbAppend = new StringBuilder(String.valueOf(str3).length() + 20 + String.valueOf(str).length()).append("Caching ").append(str3);
                            str2 = " bytes from ";
                            zzafr.zzaC(sbAppend.append(" bytes from ").append(str).toString());
                            ReadableByteChannel readableByteChannelNewChannel = Channels.newChannel(httpURLConnectionZzb.getInputStream());
                            FileOutputStream fileOutputStream2 = new FileOutputStream(file3);
                            try {
                                try {
                                    FileChannel channel = fileOutputStream2.getChannel();
                                    ByteBuffer byteBufferAllocate = ByteBuffer.allocate(1048576);
                                    com.google.android.gms.common.util.zze zzeVarZzbF = com.google.android.gms.ads.internal.zzbs.zzbF();
                                    int i3 = 0;
                                    long jCurrentTimeMillis = zzeVarZzbF.currentTimeMillis();
                                    zzair zzairVar = new zzair(((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCr)).longValue());
                                    zzme<Long> zzmeVar = zzmo.zzCq;
                                    zzmmVarZzbL = com.google.android.gms.ads.internal.zzbs.zzbL();
                                    long jLongValue = ((Long) zzmmVarZzbL.zzd(zzmeVar)).longValue();
                                    while (true) {
                                        int i4 = readableByteChannelNewChannel.read(byteBufferAllocate);
                                        if (i4 < 0) {
                                            fileOutputStream2.close();
                                            if (zzafr.zzz(3)) {
                                                String str4 = zzJY.format(i3);
                                                zzafr.zzaC(new StringBuilder(String.valueOf(str4).length() + 22 + String.valueOf(str).length()).append("Preloaded ").append(str4).append(" bytes from ").append(str).toString());
                                            }
                                            file3.setReadable(true, false);
                                            if (fileZzb.isFile()) {
                                                fileZzb.setLastModified(System.currentTimeMillis());
                                            } else {
                                                try {
                                                    fileZzb.createNewFile();
                                                } catch (IOException e) {
                                                }
                                            }
                                            zza(str, file3.getAbsolutePath(), i3);
                                            zzJX.remove(strConcat);
                                            return true;
                                        }
                                        i3 += i4;
                                        try {
                                            try {
                                                if (i3 > iIntValue) {
                                                    String strValueOf7 = String.valueOf(Integer.toString(i3));
                                                    if (strValueOf7.length() != 0) {
                                                        "File too big for full file cache. Size: ".concat(strValueOf7);
                                                    } else {
                                                        new String("File too big for full file cache. Size: ");
                                                    }
                                                    throw new IOException("stream cache file size limit exceeded");
                                                }
                                                byteBufferAllocate.flip();
                                                while (channel.write(byteBufferAllocate) > 0) {
                                                }
                                                byteBufferAllocate.clear();
                                                str2 = 1000 * jLongValue;
                                                if (zzeVarZzbF.currentTimeMillis() - jCurrentTimeMillis > str2) {
                                                    String strValueOf8 = String.valueOf(Long.toString(jLongValue));
                                                    new StringBuilder(String.valueOf(strValueOf8).length() + 29).append("Timeout exceeded. Limit: ").append(strValueOf8).append(" sec").toString();
                                                    throw new IOException("stream cache time limit exceeded");
                                                }
                                                if (this.zzKa) {
                                                    throw new IOException("abort requested");
                                                }
                                                if (zzairVar.tryAcquire()) {
                                                    zzmmVarZzbL = this;
                                                    str2 = str;
                                                    zzaiy.zzaaH.post(new zzsc(zzmmVarZzbL, str2, file3.getAbsolutePath(), i3, contentLength, false));
                                                }
                                            } catch (IOException e2) {
                                                e = e2;
                                                zzmmVarZzbL = 0;
                                                fileOutputStream = fileOutputStream2;
                                            } catch (RuntimeException e3) {
                                                e = e3;
                                                zzmmVarZzbL = 0;
                                                fileOutputStream = fileOutputStream2;
                                            }
                                        } catch (IOException e4) {
                                            e = e4;
                                            fileOutputStream = fileOutputStream2;
                                        } catch (RuntimeException e5) {
                                            e = e5;
                                            fileOutputStream = fileOutputStream2;
                                        }
                                    }
                                } catch (IOException e6) {
                                    e = e6;
                                    zzmmVarZzbL = 0;
                                    str2 = "error";
                                    fileOutputStream = fileOutputStream2;
                                }
                            } catch (RuntimeException e7) {
                                e = e7;
                                zzmmVarZzbL = 0;
                                str2 = "error";
                                fileOutputStream = fileOutputStream2;
                            }
                        } else {
                            str2 = "badUrl";
                            try {
                                String strValueOf9 = String.valueOf(Integer.toString(responseCode));
                                zzmmVarZzbL = strValueOf9.length() != 0 ? "HTTP request failed. Code: ".concat(strValueOf9) : new String("HTTP request failed. Code: ");
                                try {
                                    throw new IOException(new StringBuilder(String.valueOf(str).length() + 32).append("HTTP status code ").append(responseCode).append(" at ").append(str).toString());
                                } catch (IOException e8) {
                                    e = e8;
                                } catch (RuntimeException e9) {
                                    e = e9;
                                }
                            } catch (IOException e10) {
                                e = e10;
                                zzmmVarZzbL = 0;
                            } catch (RuntimeException e11) {
                                e = e11;
                                zzmmVarZzbL = 0;
                            }
                        }
                    } catch (IOException e12) {
                        e = e12;
                        zzmmVarZzbL = 0;
                        str2 = "error";
                    } catch (RuntimeException e13) {
                        e = e13;
                        zzmmVarZzbL = 0;
                        str2 = "error";
                    }
                    if (e instanceof RuntimeException) {
                        com.google.android.gms.ads.internal.zzbs.zzbD().zza(e, "VideoStreamFullFileCache.preload");
                    }
                    try {
                        fileOutputStream.close();
                    } catch (IOException e14) {
                    } catch (NullPointerException e15) {
                    }
                    if (this.zzKa) {
                        zzafr.zzaS(new StringBuilder(String.valueOf(str).length() + 26).append("Preload aborted for URL \"").append(str).append("\"").toString());
                    } else {
                        zzafr.zzc(new StringBuilder(String.valueOf(str).length() + 25).append("Preload failed for URL \"").append(str).append("\"").toString(), e);
                    }
                    if (file3.exists() && !file3.delete()) {
                        String strValueOf10 = String.valueOf(file3.getAbsolutePath());
                        zzafr.zzaT(strValueOf10.length() != 0 ? "Could not delete partial cache file at ".concat(strValueOf10) : new String("Could not delete partial cache file at "));
                    }
                    zza(str, file3.getAbsolutePath(), str2, zzmmVarZzbL);
                    zzJX.remove(strConcat);
                    return false;
                }
            }
            if (this.zzJZ == null) {
                zDelete = false;
            } else {
                File file4 = null;
                long j = Long.MAX_VALUE;
                File[] fileArrListFiles = this.zzJZ.listFiles();
                int length2 = fileArrListFiles.length;
                int i5 = 0;
                while (i5 < length2) {
                    File file5 = fileArrListFiles[i5];
                    if (file5.getName().endsWith(".done")) {
                        jLastModified = j;
                        file = file4;
                    } else {
                        jLastModified = file5.lastModified();
                        if (jLastModified < j) {
                            file = file5;
                        } else {
                            jLastModified = j;
                            file = file4;
                        }
                    }
                    i5++;
                    file4 = file;
                    j = jLastModified;
                }
                zDelete = false;
                if (file4 != null) {
                    zDelete = file4.delete();
                    File fileZzb2 = zzb(file4);
                    if (fileZzb2.isFile()) {
                        zDelete &= fileZzb2.delete();
                    }
                }
            }
        } while (zDelete);
        zzafr.zzaT("Unable to expire stream cache");
        zza(str, null, "expireFailed", null);
        return false;
    }
}
