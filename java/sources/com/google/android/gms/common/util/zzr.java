package com.google.android.gms.common.util;

import android.os.Process;
import android.os.StrictMode;
import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class zzr {
    private static String zzaJW = null;
    private static final int zzaJX = Process.myPid();

    private static String zzaD(int i) throws Throwable {
        BufferedReader bufferedReader;
        Throwable th;
        String strTrim = null;
        if (i > 0) {
            try {
                StrictMode.ThreadPolicy threadPolicyAllowThreadDiskReads = StrictMode.allowThreadDiskReads();
                try {
                    bufferedReader = new BufferedReader(new FileReader(new StringBuilder(25).append("/proc/").append(i).append("/cmdline").toString()));
                    try {
                        StrictMode.setThreadPolicy(threadPolicyAllowThreadDiskReads);
                        strTrim = bufferedReader.readLine().trim();
                        zzn.closeQuietly(bufferedReader);
                    } catch (IOException e) {
                        zzn.closeQuietly(bufferedReader);
                    } catch (Throwable th2) {
                        th = th2;
                        zzn.closeQuietly(bufferedReader);
                        throw th;
                    }
                } catch (Throwable th3) {
                    StrictMode.setThreadPolicy(threadPolicyAllowThreadDiskReads);
                    throw th3;
                }
            } catch (IOException e2) {
                bufferedReader = null;
            } catch (Throwable th4) {
                bufferedReader = null;
                th = th4;
            }
        }
        return strTrim;
    }

    public static String zzsf() {
        if (zzaJW == null) {
            zzaJW = zzaD(zzaJX);
        }
        return zzaJW;
    }
}
