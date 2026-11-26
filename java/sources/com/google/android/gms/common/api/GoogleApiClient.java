package com.google.android.gms.common.api;

import android.accounts.Account;
import android.content.Context;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.FragmentActivity;
import android.support.v4.util.ArrayMap;
import android.view.View;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.android.gms.common.internal.zzbo;
import com.google.android.gms.common.internal.zzq;
import com.google.android.gms.common.internal.zzr;
import com.google.android.gms.internal.zzbau;
import com.google.android.gms.internal.zzbay;
import com.google.android.gms.internal.zzbbi;
import com.google.android.gms.internal.zzbcp;
import com.google.android.gms.internal.zzbdr;
import com.google.android.gms.internal.zzbdw;
import com.google.android.gms.internal.zzbei;
import com.google.android.gms.internal.zzbes;
import com.google.android.gms.internal.zzctg;
import com.google.android.gms.internal.zzctk;
import com.google.android.gms.internal.zzctl;
import java.io.FileDescriptor;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.WeakHashMap;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.locks.ReentrantLock;

/* JADX INFO: loaded from: classes.dex */
public abstract class GoogleApiClient {
    public static final int SIGN_IN_MODE_OPTIONAL = 2;
    public static final int SIGN_IN_MODE_REQUIRED = 1;
    private static final Set<GoogleApiClient> zzaAS = Collections.newSetFromMap(new WeakHashMap());

    public static final class Builder {
        private final Context mContext;
        private final Set<Scope> zzaAT;
        private final Set<Scope> zzaAU;
        private int zzaAV;
        private View zzaAW;
        private String zzaAX;
        private final Map<Api<?>, zzr> zzaAY;
        private final Map<Api<?>, Api.ApiOptions> zzaAZ;
        private zzbdr zzaBa;
        private int zzaBb;
        private OnConnectionFailedListener zzaBc;
        private GoogleApiAvailability zzaBd;
        private Api.zza<? extends zzctk, zzctl> zzaBe;
        private final ArrayList<ConnectionCallbacks> zzaBf;
        private final ArrayList<OnConnectionFailedListener> zzaBg;
        private boolean zzaBh;
        private Account zzajb;
        private String zzake;
        private Looper zzrM;

        public Builder(@NonNull Context context) {
            this.zzaAT = new HashSet();
            this.zzaAU = new HashSet();
            this.zzaAY = new ArrayMap();
            this.zzaAZ = new ArrayMap();
            this.zzaBb = -1;
            this.zzaBd = GoogleApiAvailability.getInstance();
            this.zzaBe = zzctg.zzajS;
            this.zzaBf = new ArrayList<>();
            this.zzaBg = new ArrayList<>();
            this.zzaBh = false;
            this.mContext = context;
            this.zzrM = context.getMainLooper();
            this.zzake = context.getPackageName();
            this.zzaAX = context.getClass().getName();
        }

        public Builder(@NonNull Context context, @NonNull ConnectionCallbacks connectionCallbacks, @NonNull OnConnectionFailedListener onConnectionFailedListener) {
            this(context);
            zzbo.zzb(connectionCallbacks, "Must provide a connected listener");
            this.zzaBf.add(connectionCallbacks);
            zzbo.zzb(onConnectionFailedListener, "Must provide a connection failed listener");
            this.zzaBg.add(onConnectionFailedListener);
        }

        private final <O extends Api.ApiOptions> void zza(Api<O> api, O o, Scope... scopeArr) {
            HashSet hashSet = new HashSet(api.zzpb().zzn(o));
            for (Scope scope : scopeArr) {
                hashSet.add(scope);
            }
            this.zzaAY.put(api, new zzr(hashSet));
        }

        public final Builder addApi(@NonNull Api<? extends Api.ApiOptions.NotRequiredOptions> api) {
            zzbo.zzb(api, "Api must not be null");
            this.zzaAZ.put(api, null);
            List<Scope> listZzn = api.zzpb().zzn(null);
            this.zzaAU.addAll(listZzn);
            this.zzaAT.addAll(listZzn);
            return this;
        }

        public final <O extends Api.ApiOptions.HasOptions> Builder addApi(@NonNull Api<O> api, @NonNull O o) {
            zzbo.zzb(api, "Api must not be null");
            zzbo.zzb(o, "Null options are not permitted for this Api");
            this.zzaAZ.put(api, o);
            List<Scope> listZzn = api.zzpb().zzn(o);
            this.zzaAU.addAll(listZzn);
            this.zzaAT.addAll(listZzn);
            return this;
        }

        public final <O extends Api.ApiOptions.HasOptions> Builder addApiIfAvailable(@NonNull Api<O> api, @NonNull O o, Scope... scopeArr) {
            zzbo.zzb(api, "Api must not be null");
            zzbo.zzb(o, "Null options are not permitted for this Api");
            this.zzaAZ.put(api, o);
            zza(api, o, scopeArr);
            return this;
        }

        public final Builder addApiIfAvailable(@NonNull Api<? extends Api.ApiOptions.NotRequiredOptions> api, Scope... scopeArr) {
            zzbo.zzb(api, "Api must not be null");
            this.zzaAZ.put(api, null);
            zza(api, null, scopeArr);
            return this;
        }

        public final Builder addConnectionCallbacks(@NonNull ConnectionCallbacks connectionCallbacks) {
            zzbo.zzb(connectionCallbacks, "Listener must not be null");
            this.zzaBf.add(connectionCallbacks);
            return this;
        }

        public final Builder addOnConnectionFailedListener(@NonNull OnConnectionFailedListener onConnectionFailedListener) {
            zzbo.zzb(onConnectionFailedListener, "Listener must not be null");
            this.zzaBg.add(onConnectionFailedListener);
            return this;
        }

        public final Builder addScope(@NonNull Scope scope) {
            zzbo.zzb(scope, "Scope must not be null");
            this.zzaAT.add(scope);
            return this;
        }

        public final GoogleApiClient build() {
            zzbo.zzb(!this.zzaAZ.isEmpty(), "must call addApi() to add at least one API");
            zzq zzqVarZzpn = zzpn();
            Api<?> api = null;
            Map<Api<?>, zzr> mapZzrp = zzqVarZzpn.zzrp();
            ArrayMap arrayMap = new ArrayMap();
            ArrayMap arrayMap2 = new ArrayMap();
            ArrayList arrayList = new ArrayList();
            boolean z = false;
            for (Api<?> api2 : this.zzaAZ.keySet()) {
                Api.ApiOptions apiOptions = this.zzaAZ.get(api2);
                boolean z2 = mapZzrp.get(api2) != null;
                arrayMap.put(api2, Boolean.valueOf(z2));
                zzbbi zzbbiVar = new zzbbi(api2, z2);
                arrayList.add(zzbbiVar);
                Api.zza<?, O> zzaVarZzpc = api2.zzpc();
                Api.zze zzeVarZza = zzaVarZzpc.zza(this.mContext, this.zzrM, zzqVarZzpn, apiOptions, zzbbiVar, zzbbiVar);
                arrayMap2.put(api2.zzpd(), zzeVarZza);
                boolean z3 = zzaVarZzpc.getPriority() == 1 ? apiOptions != null : z;
                if (!zzeVarZza.zzmG()) {
                    api2 = api;
                } else if (api != null) {
                    String strValueOf = String.valueOf(api2.getName());
                    String strValueOf2 = String.valueOf(api.getName());
                    throw new IllegalStateException(new StringBuilder(String.valueOf(strValueOf).length() + 21 + String.valueOf(strValueOf2).length()).append(strValueOf).append(" cannot be used with ").append(strValueOf2).toString());
                }
                z = z3;
                api = api2;
            }
            if (api != null) {
                if (z) {
                    String strValueOf3 = String.valueOf(api.getName());
                    throw new IllegalStateException(new StringBuilder(String.valueOf(strValueOf3).length() + 82).append("With using ").append(strValueOf3).append(", GamesOptions can only be specified within GoogleSignInOptions.Builder").toString());
                }
                zzbo.zza(this.zzajb == null, "Must not set an account in GoogleApiClient.Builder when using %s. Set account in GoogleSignInOptions.Builder instead", api.getName());
                zzbo.zza(this.zzaAT.equals(this.zzaAU), "Must not set scopes in GoogleApiClient.Builder when using %s. Set account in GoogleSignInOptions.Builder instead.", api.getName());
            }
            zzbcp zzbcpVar = new zzbcp(this.mContext, new ReentrantLock(), this.zzrM, zzqVarZzpn, this.zzaBd, this.zzaBe, arrayMap, this.zzaBf, this.zzaBg, arrayMap2, this.zzaBb, zzbcp.zza(arrayMap2.values(), true), arrayList, false);
            synchronized (GoogleApiClient.zzaAS) {
                GoogleApiClient.zzaAS.add(zzbcpVar);
            }
            if (this.zzaBb >= 0) {
                zzbau.zza(this.zzaBa).zza(this.zzaBb, zzbcpVar, this.zzaBc);
            }
            return zzbcpVar;
        }

        public final Builder enableAutoManage(@NonNull FragmentActivity fragmentActivity, int i, @Nullable OnConnectionFailedListener onConnectionFailedListener) {
            zzbdr zzbdrVar = new zzbdr(fragmentActivity);
            zzbo.zzb(i >= 0, "clientId must be non-negative");
            this.zzaBb = i;
            this.zzaBc = onConnectionFailedListener;
            this.zzaBa = zzbdrVar;
            return this;
        }

        public final Builder enableAutoManage(@NonNull FragmentActivity fragmentActivity, @Nullable OnConnectionFailedListener onConnectionFailedListener) {
            return enableAutoManage(fragmentActivity, 0, onConnectionFailedListener);
        }

        public final Builder setAccountName(String str) {
            this.zzajb = str == null ? null : new Account(str, "com.google");
            return this;
        }

        public final Builder setGravityForPopups(int i) {
            this.zzaAV = i;
            return this;
        }

        public final Builder setHandler(@NonNull Handler handler) {
            zzbo.zzb(handler, "Handler must not be null");
            this.zzrM = handler.getLooper();
            return this;
        }

        public final Builder setViewForPopups(@NonNull View view) {
            zzbo.zzb(view, "View must not be null");
            this.zzaAW = view;
            return this;
        }

        public final Builder useDefaultAccount() {
            return setAccountName("<<default account>>");
        }

        public final Builder zze(Account account) {
            this.zzajb = account;
            return this;
        }

        public final zzq zzpn() {
            zzctl zzctlVar = zzctl.zzbCM;
            if (this.zzaAZ.containsKey(zzctg.API)) {
                zzctlVar = (zzctl) this.zzaAZ.get(zzctg.API);
            }
            return new zzq(this.zzajb, this.zzaAT, this.zzaAY, this.zzaAV, this.zzaAW, this.zzake, this.zzaAX, zzctlVar);
        }
    }

    public interface ConnectionCallbacks {
        public static final int CAUSE_NETWORK_LOST = 2;
        public static final int CAUSE_SERVICE_DISCONNECTED = 1;

        void onConnected(@Nullable Bundle bundle);

        void onConnectionSuspended(int i);
    }

    public interface OnConnectionFailedListener {
        void onConnectionFailed(@NonNull ConnectionResult connectionResult);
    }

    public static void dumpAll(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
        synchronized (zzaAS) {
            String strConcat = String.valueOf(str).concat("  ");
            int i = 0;
            for (GoogleApiClient googleApiClient : zzaAS) {
                printWriter.append((CharSequence) str).append("GoogleApiClient#").println(i);
                googleApiClient.dump(strConcat, fileDescriptor, printWriter, strArr);
                i++;
            }
        }
    }

    public static Set<GoogleApiClient> zzpk() {
        Set<GoogleApiClient> set;
        synchronized (zzaAS) {
            set = zzaAS;
        }
        return set;
    }

    public abstract ConnectionResult blockingConnect();

    public abstract ConnectionResult blockingConnect(long j, @NonNull TimeUnit timeUnit);

    public abstract PendingResult<Status> clearDefaultAccountAndReconnect();

    public abstract void connect();

    public void connect(int i) {
        throw new UnsupportedOperationException();
    }

    public abstract void disconnect();

    public abstract void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr);

    @NonNull
    public abstract ConnectionResult getConnectionResult(@NonNull Api<?> api);

    public Context getContext() {
        throw new UnsupportedOperationException();
    }

    public Looper getLooper() {
        throw new UnsupportedOperationException();
    }

    public abstract boolean hasConnectedApi(@NonNull Api<?> api);

    public abstract boolean isConnected();

    public abstract boolean isConnecting();

    public abstract boolean isConnectionCallbacksRegistered(@NonNull ConnectionCallbacks connectionCallbacks);

    public abstract boolean isConnectionFailedListenerRegistered(@NonNull OnConnectionFailedListener onConnectionFailedListener);

    public abstract void reconnect();

    public abstract void registerConnectionCallbacks(@NonNull ConnectionCallbacks connectionCallbacks);

    public abstract void registerConnectionFailedListener(@NonNull OnConnectionFailedListener onConnectionFailedListener);

    public abstract void stopAutoManage(@NonNull FragmentActivity fragmentActivity);

    public abstract void unregisterConnectionCallbacks(@NonNull ConnectionCallbacks connectionCallbacks);

    public abstract void unregisterConnectionFailedListener(@NonNull OnConnectionFailedListener onConnectionFailedListener);

    @NonNull
    public <C extends Api.zze> C zza(@NonNull Api.zzc<C> zzcVar) {
        throw new UnsupportedOperationException();
    }

    public void zza(zzbes zzbesVar) {
        throw new UnsupportedOperationException();
    }

    public boolean zza(@NonNull Api<?> api) {
        throw new UnsupportedOperationException();
    }

    public boolean zza(zzbei zzbeiVar) {
        throw new UnsupportedOperationException();
    }

    public void zzb(zzbes zzbesVar) {
        throw new UnsupportedOperationException();
    }

    public <A extends Api.zzb, R extends Result, T extends zzbay<R, A>> T zzd(@NonNull T t) {
        throw new UnsupportedOperationException();
    }

    public <A extends Api.zzb, T extends zzbay<? extends Result, A>> T zze(@NonNull T t) {
        throw new UnsupportedOperationException();
    }

    public <L> zzbdw<L> zzp(@NonNull L l) {
        throw new UnsupportedOperationException();
    }

    public void zzpl() {
        throw new UnsupportedOperationException();
    }
}
