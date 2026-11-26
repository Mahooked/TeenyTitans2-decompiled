package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.URL;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzajd implements zzajb {

    @Nullable
    private final String zzJP;

    public zzajd() {
        this(null);
    }

    public zzajd(@Nullable String str) {
        this.zzJP = str;
    }

    @Override // com.google.android.gms.internal.zzajb
    @WorkerThread
    public final void zzaN(String str) {
        try {
            String strValueOf = String.valueOf(str);
            zzajc.zzaC(strValueOf.length() != 0 ? "Pinging URL: ".concat(strValueOf) : new String("Pinging URL: "));
            HttpURLConnection httpURLConnection = (HttpURLConnection) new URL(str).openConnection();
            try {
                zzji.zzds();
                zzaiy.zza(true, httpURLConnection, this.zzJP);
                int responseCode = httpURLConnection.getResponseCode();
                if (responseCode < 200 || responseCode >= 300) {
                    zzajc.zzaT(new StringBuilder(String.valueOf(str).length() + 65).append("Received non-success response code ").append(responseCode).append(" from pinging URL: ").append(str).toString());
                }
            } finally {
                httpURLConnection.disconnect();
            }
        } catch (IOException e) {
            String strValueOf2 = String.valueOf(e.getMessage());
            zzajc.zzaT(new StringBuilder(String.valueOf(str).length() + 27 + String.valueOf(strValueOf2).length()).append("Error while pinging URL: ").append(str).append(". ").append(strValueOf2).toString());
        } catch (IndexOutOfBoundsException e2) {
            String strValueOf3 = String.valueOf(e2.getMessage());
            zzajc.zzaT(new StringBuilder(String.valueOf(str).length() + 32 + String.valueOf(strValueOf3).length()).append("Error while parsing ping URL: ").append(str).append(". ").append(strValueOf3).toString());
        } catch (RuntimeException e3) {
            String strValueOf4 = String.valueOf(e3.getMessage());
            zzajc.zzaT(new StringBuilder(String.valueOf(str).length() + 27 + String.valueOf(strValueOf4).length()).append("Error while pinging URL: ").append(str).append(". ").append(strValueOf4).toString());
        }
    }
}
