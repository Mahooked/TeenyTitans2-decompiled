package com.google.android.gms.common.internal;

import android.accounts.Account;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.DeadObjectException;
import android.os.Handler;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.os.RemoteException;
import android.support.annotation.CallSuper;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import android.text.TextUtils;
import android.util.Log;
import com.fasterxml.jackson.core.util.MinimalPrettyPrinter;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.CommonStatusCodes;
import com.google.android.gms.common.api.Scope;
import java.io.FileDescriptor;
import java.io.PrintWriter;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Date;
import java.util.Locale;
import java.util.Set;
import java.util.concurrent.atomic.AtomicInteger;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzd<T extends IInterface> {
    private static String[] zzaHc = {"service_esmobile", "service_googleme"};
    private final Context mContext;
    final Handler mHandler;
    private final Object mLock;
    private final com.google.android.gms.common.zze zzaCF;
    private int zzaGH;
    private long zzaGI;
    private long zzaGJ;
    private int zzaGK;
    private long zzaGL;
    private zzak zzaGM;
    private final zzae zzaGN;
    private final Object zzaGO;
    private zzaw zzaGP;
    protected zzj zzaGQ;
    private T zzaGR;
    private final ArrayList<zzi<?>> zzaGS;
    private zzl zzaGT;
    private int zzaGU;
    private final zzf zzaGV;
    private final zzg zzaGW;
    private final int zzaGX;
    private final String zzaGY;
    private ConnectionResult zzaGZ;
    private boolean zzaHa;
    protected AtomicInteger zzaHb;
    private final Looper zzrM;

    protected zzd(Context context, Looper looper, int i, zzf zzfVar, zzg zzgVar, String str) {
        this(context, looper, zzae.zzaC(context), com.google.android.gms.common.zze.zzoW(), i, (zzf) zzbo.zzu(zzfVar), (zzg) zzbo.zzu(zzgVar), null);
    }

    protected zzd(Context context, Looper looper, zzae zzaeVar, com.google.android.gms.common.zze zzeVar, int i, zzf zzfVar, zzg zzgVar, String str) {
        this.mLock = new Object();
        this.zzaGO = new Object();
        this.zzaGS = new ArrayList<>();
        this.zzaGU = 1;
        this.zzaGZ = null;
        this.zzaHa = false;
        this.zzaHb = new AtomicInteger(0);
        this.mContext = (Context) zzbo.zzb(context, "Context must not be null");
        this.zzrM = (Looper) zzbo.zzb(looper, "Looper must not be null");
        this.zzaGN = (zzae) zzbo.zzb(zzaeVar, "Supervisor must not be null");
        this.zzaCF = (com.google.android.gms.common.zze) zzbo.zzb(zzeVar, "API availability must not be null");
        this.mHandler = new zzh(this, looper);
        this.zzaGX = i;
        this.zzaGV = zzfVar;
        this.zzaGW = zzgVar;
        this.zzaGY = str;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zza(int i, T t) {
        zzbo.zzaf((i == 4) == (t != null));
        synchronized (this.mLock) {
            this.zzaGU = i;
            this.zzaGR = t;
            switch (i) {
                case 1:
                    if (this.zzaGT != null) {
                        this.zzaGN.zza(zzdb(), zzqZ(), this.zzaGT, zzra());
                        this.zzaGT = null;
                    }
                    break;
                case 2:
                case 3:
                    if (this.zzaGT != null && this.zzaGM != null) {
                        String strValueOf = String.valueOf(this.zzaGM.zzrE());
                        String strValueOf2 = String.valueOf(this.zzaGM.getPackageName());
                        Log.e("GmsClient", new StringBuilder(String.valueOf(strValueOf).length() + 70 + String.valueOf(strValueOf2).length()).append("Calling connect() while still connected, missing disconnect() for ").append(strValueOf).append(" on ").append(strValueOf2).toString());
                        this.zzaGN.zza(this.zzaGM.zzrE(), this.zzaGM.getPackageName(), this.zzaGT, zzra());
                        this.zzaHb.incrementAndGet();
                    }
                    this.zzaGT = new zzl(this, this.zzaHb.get());
                    this.zzaGM = new zzak(zzqZ(), zzdb(), false);
                    if (!this.zzaGN.zza(new zzaf(this.zzaGM.zzrE(), this.zzaGM.getPackageName()), this.zzaGT, zzra())) {
                        String strValueOf3 = String.valueOf(this.zzaGM.zzrE());
                        String strValueOf4 = String.valueOf(this.zzaGM.getPackageName());
                        Log.e("GmsClient", new StringBuilder(String.valueOf(strValueOf3).length() + 34 + String.valueOf(strValueOf4).length()).append("unable to connect to service: ").append(strValueOf3).append(" on ").append(strValueOf4).toString());
                        zza(16, (Bundle) null, this.zzaHb.get());
                    }
                    break;
                case 4:
                    zza(t);
                    break;
            }
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final boolean zza(int i, int i2, T t) {
        boolean z;
        synchronized (this.mLock) {
            if (this.zzaGU != i) {
                z = false;
            } else {
                zza(i2, t);
                z = true;
            }
        }
        return z;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zzaz(int i) {
        int i2;
        if (zzrc()) {
            i2 = 5;
            this.zzaHa = true;
        } else {
            i2 = 4;
        }
        this.mHandler.sendMessage(this.mHandler.obtainMessage(i2, this.zzaHb.get(), 16));
    }

    @Nullable
    private final String zzra() {
        return this.zzaGY == null ? this.mContext.getClass().getName() : this.zzaGY;
    }

    private final boolean zzrc() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzaGU == 3;
        }
        return z;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final boolean zzri() {
        if (this.zzaHa || TextUtils.isEmpty(zzdc()) || TextUtils.isEmpty(null)) {
            return false;
        }
        try {
            Class.forName(zzdc());
            return true;
        } catch (ClassNotFoundException e) {
            return false;
        }
    }

    public void disconnect() {
        this.zzaHb.incrementAndGet();
        synchronized (this.zzaGS) {
            int size = this.zzaGS.size();
            for (int i = 0; i < size; i++) {
                this.zzaGS.get(i).removeListener();
            }
            this.zzaGS.clear();
        }
        synchronized (this.zzaGO) {
            this.zzaGP = null;
        }
        zza(1, (IInterface) null);
    }

    public final void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
        int i;
        T t;
        zzaw zzawVar;
        synchronized (this.mLock) {
            i = this.zzaGU;
            t = this.zzaGR;
        }
        synchronized (this.zzaGO) {
            zzawVar = this.zzaGP;
        }
        printWriter.append((CharSequence) str).append("mConnectState=");
        switch (i) {
            case 1:
                printWriter.print("DISCONNECTED");
                break;
            case 2:
                printWriter.print("REMOTE_CONNECTING");
                break;
            case 3:
                printWriter.print("LOCAL_CONNECTING");
                break;
            case 4:
                printWriter.print("CONNECTED");
                break;
            case 5:
                printWriter.print("DISCONNECTING");
                break;
            default:
                printWriter.print("UNKNOWN");
                break;
        }
        printWriter.append(" mService=");
        if (t == null) {
            printWriter.append("null");
        } else {
            printWriter.append((CharSequence) zzdc()).append("@").append((CharSequence) Integer.toHexString(System.identityHashCode(t.asBinder())));
        }
        printWriter.append(" mServiceBroker=");
        if (zzawVar == null) {
            printWriter.println("null");
        } else {
            printWriter.append("IGmsServiceBroker@").println(Integer.toHexString(System.identityHashCode(zzawVar.asBinder())));
        }
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss.SSS", Locale.US);
        if (this.zzaGJ > 0) {
            PrintWriter printWriterAppend = printWriter.append((CharSequence) str).append("lastConnectedTime=");
            long j = this.zzaGJ;
            String strValueOf = String.valueOf(simpleDateFormat.format(new Date(this.zzaGJ)));
            printWriterAppend.println(new StringBuilder(String.valueOf(strValueOf).length() + 21).append(j).append(MinimalPrettyPrinter.DEFAULT_ROOT_VALUE_SEPARATOR).append(strValueOf).toString());
        }
        if (this.zzaGI > 0) {
            printWriter.append((CharSequence) str).append("lastSuspendedCause=");
            switch (this.zzaGH) {
                case 1:
                    printWriter.append("CAUSE_SERVICE_DISCONNECTED");
                    break;
                case 2:
                    printWriter.append("CAUSE_NETWORK_LOST");
                    break;
                default:
                    printWriter.append((CharSequence) String.valueOf(this.zzaGH));
                    break;
            }
            PrintWriter printWriterAppend2 = printWriter.append(" lastSuspendedTime=");
            long j2 = this.zzaGI;
            String strValueOf2 = String.valueOf(simpleDateFormat.format(new Date(this.zzaGI)));
            printWriterAppend2.println(new StringBuilder(String.valueOf(strValueOf2).length() + 21).append(j2).append(MinimalPrettyPrinter.DEFAULT_ROOT_VALUE_SEPARATOR).append(strValueOf2).toString());
        }
        if (this.zzaGL > 0) {
            printWriter.append((CharSequence) str).append("lastFailedStatus=").append((CharSequence) CommonStatusCodes.getStatusCodeString(this.zzaGK));
            PrintWriter printWriterAppend3 = printWriter.append(" lastFailedTime=");
            long j3 = this.zzaGL;
            String strValueOf3 = String.valueOf(simpleDateFormat.format(new Date(this.zzaGL)));
            printWriterAppend3.println(new StringBuilder(String.valueOf(strValueOf3).length() + 21).append(j3).append(MinimalPrettyPrinter.DEFAULT_ROOT_VALUE_SEPARATOR).append(strValueOf3).toString());
        }
    }

    public Account getAccount() {
        return null;
    }

    public final Context getContext() {
        return this.mContext;
    }

    public final Looper getLooper() {
        return this.zzrM;
    }

    public final boolean isConnected() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzaGU == 4;
        }
        return z;
    }

    public final boolean isConnecting() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzaGU == 2 || this.zzaGU == 3;
        }
        return z;
    }

    @CallSuper
    protected void onConnectionFailed(ConnectionResult connectionResult) {
        this.zzaGK = connectionResult.getErrorCode();
        this.zzaGL = System.currentTimeMillis();
    }

    @CallSuper
    protected final void onConnectionSuspended(int i) {
        this.zzaGH = i;
        this.zzaGI = System.currentTimeMillis();
    }

    protected final void zza(int i, @Nullable Bundle bundle, int i2) {
        this.mHandler.sendMessage(this.mHandler.obtainMessage(7, i2, -1, new zzo(this, i, null)));
    }

    protected void zza(int i, IBinder iBinder, Bundle bundle, int i2) {
        this.mHandler.sendMessage(this.mHandler.obtainMessage(1, i2, -1, new zzn(this, i, iBinder, bundle)));
    }

    @CallSuper
    protected void zza(@NonNull T t) {
        this.zzaGJ = System.currentTimeMillis();
    }

    @WorkerThread
    public final void zza(zzal zzalVar, Set<Scope> set) {
        Bundle bundleZzmo = zzmo();
        zzx zzxVar = new zzx(this.zzaGX);
        zzxVar.zzaHw = this.mContext.getPackageName();
        zzxVar.zzaHz = bundleZzmo;
        if (set != null) {
            zzxVar.zzaHy = (Scope[]) set.toArray(new Scope[set.size()]);
        }
        if (zzmv()) {
            zzxVar.zzaHA = getAccount() != null ? getAccount() : new Account("<<default account>>", "com.google");
            if (zzalVar != null) {
                zzxVar.zzaHx = zzalVar.asBinder();
            }
        } else if (zzrg()) {
            zzxVar.zzaHA = getAccount();
        }
        zzxVar.zzaHB = zzrd();
        try {
            synchronized (this.zzaGO) {
                try {
                    if (this.zzaGP != null) {
                        this.zzaGP.zza(new zzk(this, this.zzaHb.get()), zzxVar);
                    } else {
                        Log.w("GmsClient", "mServiceBroker is null, client disconnected");
                    }
                } catch (Throwable th) {
                    throw th;
                }
            }
        } catch (DeadObjectException e) {
            Log.w("GmsClient", "IGmsServiceBroker.getService failed", e);
            zzay(1);
        } catch (RemoteException e2) {
            e = e2;
            Log.w("GmsClient", "IGmsServiceBroker.getService failed", e);
            zza(8, (IBinder) null, (Bundle) null, this.zzaHb.get());
        } catch (SecurityException e3) {
            throw e3;
        } catch (RuntimeException e4) {
            e = e4;
            Log.w("GmsClient", "IGmsServiceBroker.getService failed", e);
            zza(8, (IBinder) null, (Bundle) null, this.zzaHb.get());
        }
    }

    public void zza(@NonNull zzj zzjVar) {
        this.zzaGQ = (zzj) zzbo.zzb(zzjVar, "Connection progress callbacks cannot be null.");
        zza(2, (IInterface) null);
    }

    protected final void zza(@NonNull zzj zzjVar, int i, @Nullable PendingIntent pendingIntent) {
        this.zzaGQ = (zzj) zzbo.zzb(zzjVar, "Connection progress callbacks cannot be null.");
        this.mHandler.sendMessage(this.mHandler.obtainMessage(3, this.zzaHb.get(), i, pendingIntent));
    }

    public final void zzay(int i) {
        this.mHandler.sendMessage(this.mHandler.obtainMessage(6, this.zzaHb.get(), i));
    }

    @Nullable
    protected abstract T zzd(IBinder iBinder);

    @NonNull
    protected abstract String zzdb();

    @NonNull
    protected abstract String zzdc();

    public boolean zzmG() {
        return false;
    }

    public Intent zzmH() {
        throw new UnsupportedOperationException("Not a sign in API");
    }

    protected Bundle zzmo() {
        return new Bundle();
    }

    public boolean zzmv() {
        return false;
    }

    public Bundle zzoC() {
        return null;
    }

    public boolean zzpe() {
        return true;
    }

    protected String zzqZ() {
        return "com.google.android.gms";
    }

    public final void zzrb() {
        int iIsGooglePlayServicesAvailable = this.zzaCF.isGooglePlayServicesAvailable(this.mContext);
        if (iIsGooglePlayServicesAvailable == 0) {
            zza(new zzm(this));
        } else {
            zza(1, (IInterface) null);
            zza(new zzm(this), iIsGooglePlayServicesAvailable, (PendingIntent) null);
        }
    }

    public com.google.android.gms.common.zzc[] zzrd() {
        return new com.google.android.gms.common.zzc[0];
    }

    protected final void zzre() {
        if (!isConnected()) {
            throw new IllegalStateException("Not connected. Call connect() and wait for onConnected() to be called.");
        }
    }

    public final T zzrf() throws DeadObjectException {
        T t;
        synchronized (this.mLock) {
            if (this.zzaGU == 5) {
                throw new DeadObjectException();
            }
            zzre();
            zzbo.zza(this.zzaGR != null, "Client is connected but service is null");
            t = this.zzaGR;
        }
        return t;
    }

    public boolean zzrg() {
        return false;
    }

    protected Set<Scope> zzrh() {
        return Collections.EMPTY_SET;
    }
}
