package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.app.ActivityManager;
import android.app.KeyguardManager;
import android.content.Context;
import android.graphics.Rect;
import android.os.PowerManager;
import android.os.Process;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebView;
import android.widget.EditText;
import android.widget.TextView;
import com.adobe.mobile.TargetLocationRequest;
import java.util.List;
import org.apache.commons.io.IOUtils;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
@TargetApi(14)
public final class zzgz extends Thread {
    private final zzgu zzyy;
    private final zzzl zzyz;
    private boolean mStarted = false;
    private boolean zzyx = false;
    private boolean zzak = false;
    private final Object mLock = new Object();
    private final int zzxW = ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCU)).intValue();
    private final int zzyB = ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCV)).intValue();
    private final int zzxY = ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCW)).intValue();
    private final int zzyC = ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCX)).intValue();
    private final int zzyD = ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDa)).intValue();
    private final int zzyE = ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDc)).intValue();
    private final int zzyF = ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDd)).intValue();
    private final int zzyA = ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCY)).intValue();
    private final String zzyG = (String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDf);
    private final boolean zzyH = ((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDh)).booleanValue();

    public zzgz(zzgu zzguVar, zzzl zzzlVar) {
        this.zzyy = zzguVar;
        this.zzyz = zzzlVar;
        setName("ContentFetchTask");
    }

    private final zzhd zza(@Nullable View view, zzgt zzgtVar) {
        boolean z;
        int i = 0;
        if (view == null) {
            return new zzhd(this, 0, 0);
        }
        Context context = com.google.android.gms.ads.internal.zzbs.zzbC().getContext();
        if (context != null) {
            String str = (String) view.getTag(context.getResources().getIdentifier((String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDe), TargetLocationRequest.TARGET_PARAMETER_ORDER_ID, context.getPackageName()));
            if (!TextUtils.isEmpty(this.zzyG) && str != null && str.equals(this.zzyG)) {
                return new zzhd(this, 0, 0);
            }
        }
        boolean globalVisibleRect = view.getGlobalVisibleRect(new Rect());
        if ((view instanceof TextView) && !(view instanceof EditText)) {
            CharSequence text = ((TextView) view).getText();
            if (TextUtils.isEmpty(text)) {
                return new zzhd(this, 0, 0);
            }
            zzgtVar.zzb(text.toString(), globalVisibleRect, view.getX(), view.getY(), view.getWidth(), view.getHeight());
            return new zzhd(this, 1, 0);
        }
        if ((view instanceof WebView) && !(view instanceof zzaka)) {
            zzgtVar.zzcI();
            WebView webView = (WebView) view;
            if (com.google.android.gms.common.util.zzq.zzsc()) {
                zzgtVar.zzcI();
                webView.post(new zzhb(this, zzgtVar, webView, globalVisibleRect));
                z = true;
            } else {
                z = false;
            }
            return z ? new zzhd(this, 0, 1) : new zzhd(this, 0, 0);
        }
        if (!(view instanceof ViewGroup)) {
            return new zzhd(this, 0, 0);
        }
        ViewGroup viewGroup = (ViewGroup) view;
        int i2 = 0;
        for (int i3 = 0; i3 < viewGroup.getChildCount(); i3++) {
            zzhd zzhdVarZza = zza(viewGroup.getChildAt(i3), zzgtVar);
            i2 += zzhdVarZza.zzyP;
            i += zzhdVarZza.zzyQ;
        }
        return new zzhd(this, i2, i);
    }

    private static boolean zzcN() {
        try {
            Context context = com.google.android.gms.ads.internal.zzbs.zzbC().getContext();
            if (context == null) {
                return false;
            }
            ActivityManager activityManager = (ActivityManager) context.getSystemService("activity");
            KeyguardManager keyguardManager = (KeyguardManager) context.getSystemService("keyguard");
            if (activityManager == null || keyguardManager == null) {
                return false;
            }
            List<ActivityManager.RunningAppProcessInfo> runningAppProcesses = activityManager.getRunningAppProcesses();
            if (runningAppProcesses == null) {
                return false;
            }
            for (ActivityManager.RunningAppProcessInfo runningAppProcessInfo : runningAppProcesses) {
                if (Process.myPid() == runningAppProcessInfo.pid) {
                    if (runningAppProcessInfo.importance != 100 || keyguardManager.inKeyguardRestrictedInputMode()) {
                        break;
                        break;
                    }
                    PowerManager powerManager = (PowerManager) context.getSystemService("power");
                    if (!(powerManager == null ? false : powerManager.isScreenOn())) {
                        break;
                    }
                    return true;
                }
            }
            return false;
        } catch (Throwable th) {
            com.google.android.gms.ads.internal.zzbs.zzbD().zza(th, "ContentFetchTask.isInForeground");
            return false;
        }
    }

    private final void zzcP() {
        synchronized (this.mLock) {
            this.zzyx = true;
            zzafr.zzaC(new StringBuilder(42).append("ContentFetchThread: paused, mPause = ").append(this.zzyx).toString());
        }
    }

    /* JADX WARN: Bottom block not found for handler: all -> 0x0094 */
    @Override // java.lang.Thread, java.lang.Runnable
    /*
        Code decompiled incorrectly, please refer to instructions dump.
        To view partially-correct add '--show-bad-code' argument
    */
    public final void run() {
        /*
            r4 = this;
        L0:
            boolean r0 = zzcN()     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L7a
            if (r0 == 0) goto L88
            com.google.android.gms.internal.zzgv r0 = com.google.android.gms.ads.internal.zzbs.zzbC()     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L7a
            android.app.Activity r1 = r0.getActivity()     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L7a
            if (r1 != 0) goto L34
            java.lang.String r0 = "ContentFetchThread: no activity. Sleeping."
            com.google.android.gms.internal.zzafr.zzaC(r0)     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L7a
            r4.zzcP()     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L7a
        L18:
            int r0 = r4.zzyA     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L7a
            int r0 = r0 * 1000
            long r0 = (long) r0     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L7a
            java.lang.Thread.sleep(r0)     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L7a
        L20:
            java.lang.Object r1 = r4.mLock
            monitor-enter(r1)
        L23:
            boolean r0 = r4.zzyx     // Catch: java.lang.Throwable -> L94
            if (r0 == 0) goto L91
            java.lang.String r0 = "ContentFetchTask: waiting"
            com.google.android.gms.internal.zzafr.zzaC(r0)     // Catch: java.lang.InterruptedException -> L32 java.lang.Throwable -> L94
            java.lang.Object r0 = r4.mLock     // Catch: java.lang.InterruptedException -> L32 java.lang.Throwable -> L94
            r0.wait()     // Catch: java.lang.InterruptedException -> L32 java.lang.Throwable -> L94
            goto L23
        L32:
            r0 = move-exception
            goto L23
        L34:
            if (r1 == 0) goto L18
            r0 = 0
            android.view.Window r2 = r1.getWindow()     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L6a
            if (r2 == 0) goto L56
            android.view.Window r2 = r1.getWindow()     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L6a
            android.view.View r2 = r2.getDecorView()     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L6a
            if (r2 == 0) goto L56
            android.view.Window r1 = r1.getWindow()     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L6a
            android.view.View r1 = r1.getDecorView()     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L6a
            r2 = 16908290(0x1020002, float:2.3877235E-38)
            android.view.View r0 = r1.findViewById(r2)     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L6a
        L56:
            if (r0 == 0) goto L18
            if (r0 == 0) goto L18
            com.google.android.gms.internal.zzha r1 = new com.google.android.gms.internal.zzha     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L7a
            r1.<init>(r4, r0)     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L7a
            r0.post(r1)     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L7a
            goto L18
        L63:
            r0 = move-exception
            java.lang.String r1 = "Error in ContentFetchTask"
            com.google.android.gms.internal.zzafr.zzb(r1, r0)
            goto L20
        L6a:
            r1 = move-exception
            com.google.android.gms.internal.zzafk r2 = com.google.android.gms.ads.internal.zzbs.zzbD()     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L7a
            java.lang.String r3 = "ContentFetchTask.extractContent"
            r2.zza(r1, r3)     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L7a
            java.lang.String r1 = "Failed getting root view of activity. Content not extracted."
            com.google.android.gms.internal.zzafr.zzaC(r1)     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L7a
            goto L56
        L7a:
            r0 = move-exception
            java.lang.String r1 = "Error in ContentFetchTask"
            com.google.android.gms.internal.zzafr.zzb(r1, r0)
            com.google.android.gms.internal.zzzl r1 = r4.zzyz
            java.lang.String r2 = "ContentFetchTask.run"
            r1.zza(r0, r2)
            goto L20
        L88:
            java.lang.String r0 = "ContentFetchTask: sleeping"
            com.google.android.gms.internal.zzafr.zzaC(r0)     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L7a
            r4.zzcP()     // Catch: java.lang.InterruptedException -> L63 java.lang.Throwable -> L7a
            goto L18
        L91:
            monitor-exit(r1)     // Catch: java.lang.Throwable -> L94
            goto L0
        L94:
            r0 = move-exception
            monitor-exit(r1)     // Catch: java.lang.Throwable -> L94
            throw r0
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzgz.run():void");
    }

    public final void wakeup() {
        synchronized (this.mLock) {
            this.zzyx = false;
            this.mLock.notifyAll();
            zzafr.zzaC("ContentFetchThread: wakeup");
        }
    }

    final void zza(zzgt zzgtVar, WebView webView, String str, boolean z) {
        zzgtVar.zzcH();
        try {
            if (!TextUtils.isEmpty(str)) {
                String strOptString = new JSONObject(str).optString("text");
                if (this.zzyH || TextUtils.isEmpty(webView.getTitle())) {
                    zzgtVar.zza(strOptString, z, webView.getX(), webView.getY(), webView.getWidth(), webView.getHeight());
                } else {
                    String strValueOf = String.valueOf(webView.getTitle());
                    zzgtVar.zza(new StringBuilder(String.valueOf(strValueOf).length() + 1 + String.valueOf(strOptString).length()).append(strValueOf).append(IOUtils.LINE_SEPARATOR_UNIX).append(strOptString).toString(), z, webView.getX(), webView.getY(), webView.getWidth(), webView.getHeight());
                }
            }
            if (zzgtVar.zzcC()) {
                this.zzyy.zzb(zzgtVar);
            }
        } catch (JSONException e) {
            zzafr.zzaC("Json string may be malformed.");
        } catch (Throwable th) {
            zzafr.zza("Failed to get webview content.", th);
            this.zzyz.zza(th, "ContentFetchTask.processWebViewContent");
        }
    }

    public final void zzcM() {
        synchronized (this.mLock) {
            if (this.mStarted) {
                zzafr.zzaC("Content hash thread already started, quiting...");
            } else {
                this.mStarted = true;
                start();
            }
        }
    }

    public final zzgt zzcO() {
        return this.zzyy.zzcL();
    }

    public final boolean zzcQ() {
        return this.zzyx;
    }

    final void zzf(View view) {
        try {
            zzgt zzgtVar = new zzgt(this.zzxW, this.zzyB, this.zzxY, this.zzyC, this.zzyD, this.zzyE, this.zzyF);
            zzhd zzhdVarZza = zza(view, zzgtVar);
            zzgtVar.zzcJ();
            if (zzhdVarZza.zzyP == 0 && zzhdVarZza.zzyQ == 0) {
                return;
            }
            if (zzhdVarZza.zzyQ == 0 && zzgtVar.zzcK() == 0) {
                return;
            }
            if (zzhdVarZza.zzyQ == 0 && this.zzyy.zza(zzgtVar)) {
                return;
            }
            this.zzyy.zzc(zzgtVar);
        } catch (Exception e) {
            zzafr.zzb("Exception in fetchContentOnUIThread", e);
            this.zzyz.zza(e, "ContentFetchTask.fetchContent");
        }
    }
}
