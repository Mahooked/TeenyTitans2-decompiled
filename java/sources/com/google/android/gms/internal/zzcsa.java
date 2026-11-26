package com.google.android.gms.internal;

import android.content.Context;
import android.os.SystemClock;
import android.text.TextUtils;
import android.util.SparseArray;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.safetynet.HarmfulAppsData;
import com.google.android.gms.safetynet.SafeBrowsingData;
import com.google.android.gms.safetynet.SafeBrowsingThreat;
import com.google.android.gms.safetynet.SafetyNet;
import com.google.android.gms.safetynet.SafetyNetApi;
import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;
import java.util.concurrent.TimeUnit;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
public class zzcsa implements SafetyNetApi {
    private static final String TAG = zzcsa.class.getSimpleName();
    protected static SparseArray<zzcsr> zzbBM;
    protected static long zzbBN;

    static class zza implements SafetyNetApi.AttestationResult {
        private final Status mStatus;
        private final com.google.android.gms.safetynet.zza zzbBV;

        public zza(Status status, com.google.android.gms.safetynet.zza zzaVar) {
            this.mStatus = status;
            this.zzbBV = zzaVar;
        }

        @Override // com.google.android.gms.safetynet.SafetyNetApi.AttestationResult
        public final String getJwsResult() {
            if (this.zzbBV == null) {
                return null;
            }
            return this.zzbBV.getJwsResult();
        }

        @Override // com.google.android.gms.common.api.Result
        public final Status getStatus() {
            return this.mStatus;
        }
    }

    static abstract class zzb extends zzcrv<SafetyNetApi.AttestationResult> {
        protected zzcrw zzbBW;

        public zzb(GoogleApiClient googleApiClient) {
            super(googleApiClient);
            this.zzbBW = new zzcsi(this);
        }

        @Override // com.google.android.gms.internal.zzbbe
        protected final /* synthetic */ Result zzb(Status status) {
            return new zza(status, null);
        }
    }

    static abstract class zzc extends zzcrv<SafetyNetApi.VerifyAppsUserResult> {
        protected zzcrw zzbBW;

        public zzc(GoogleApiClient googleApiClient) {
            super(googleApiClient);
            this.zzbBW = new zzcsj(this);
        }

        @Override // com.google.android.gms.internal.zzbbe
        protected final /* synthetic */ Result zzb(Status status) {
            return new zzj(status, false);
        }
    }

    static abstract class zzd extends zzcrv<SafetyNetApi.HarmfulAppsResult> {
        protected final zzcrw zzbBW;

        public zzd(GoogleApiClient googleApiClient) {
            super(googleApiClient);
            this.zzbBW = new zzcsk(this);
        }

        @Override // com.google.android.gms.internal.zzbbe
        protected final /* synthetic */ Result zzb(Status status) {
            return new zzg(status, null);
        }
    }

    static abstract class zze extends zzcrv<SafetyNetApi.RecaptchaTokenResult> {
        protected zzcrw zzbBW;

        public zze(GoogleApiClient googleApiClient) {
            super(googleApiClient);
            this.zzbBW = new zzcsl(this);
        }

        @Override // com.google.android.gms.internal.zzbbe
        protected final /* synthetic */ Result zzb(Status status) {
            return new zzh(status, null);
        }
    }

    static abstract class zzf extends zzcrv<SafetyNetApi.SafeBrowsingResult> {
        protected zzcrw zzbBW;

        public zzf(GoogleApiClient googleApiClient) {
            super(googleApiClient);
            this.zzbBW = new zzcsm(this);
        }

        @Override // com.google.android.gms.internal.zzbbe
        protected final /* synthetic */ Result zzb(Status status) {
            return new zzi(status, null);
        }
    }

    static class zzg implements SafetyNetApi.HarmfulAppsResult {
        private final Status mStatus;
        private final com.google.android.gms.safetynet.zzd zzbCc;

        public zzg(Status status, com.google.android.gms.safetynet.zzd zzdVar) {
            this.mStatus = status;
            this.zzbCc = zzdVar;
        }

        @Override // com.google.android.gms.safetynet.SafetyNetApi.HarmfulAppsResult
        public final List<HarmfulAppsData> getHarmfulAppsList() {
            return this.zzbCc == null ? Collections.emptyList() : Arrays.asList(this.zzbCc.zzbBH);
        }

        @Override // com.google.android.gms.safetynet.SafetyNetApi.HarmfulAppsResult
        public final long getLastScanTimeMs() {
            if (this.zzbCc == null) {
                return 0L;
            }
            return this.zzbCc.zzbBG;
        }

        @Override // com.google.android.gms.common.api.Result
        public final Status getStatus() {
            return this.mStatus;
        }
    }

    static class zzh implements SafetyNetApi.RecaptchaTokenResult {
        private final Status mStatus;
        private final com.google.android.gms.safetynet.zzf zzbCd;

        public zzh(Status status, com.google.android.gms.safetynet.zzf zzfVar) {
            this.mStatus = status;
            this.zzbCd = zzfVar;
        }

        @Override // com.google.android.gms.common.api.Result
        public final Status getStatus() {
            return this.mStatus;
        }

        @Override // com.google.android.gms.safetynet.SafetyNetApi.RecaptchaTokenResult
        public final String getTokenResult() {
            if (this.zzbCd == null) {
                return null;
            }
            return this.zzbCd.getTokenResult();
        }
    }

    static class zzi implements SafetyNetApi.SafeBrowsingResult {
        private Status mStatus;
        private String zzbBI;
        private final SafeBrowsingData zzbCe;

        public zzi(Status status, SafeBrowsingData safeBrowsingData) {
            this.mStatus = status;
            this.zzbCe = safeBrowsingData;
            this.zzbBI = null;
            if (this.zzbCe != null) {
                this.zzbBI = this.zzbCe.getMetadata();
            } else if (this.mStatus.isSuccess()) {
                this.mStatus = new Status(8);
            }
        }

        @Override // com.google.android.gms.safetynet.SafetyNetApi.SafeBrowsingResult
        public final List<SafeBrowsingThreat> getDetectedThreats() {
            ArrayList arrayList = new ArrayList();
            if (this.zzbBI == null) {
                return arrayList;
            }
            try {
                JSONArray jSONArray = new JSONObject(this.zzbBI).getJSONArray("matches");
                for (int i = 0; i < jSONArray.length(); i++) {
                    try {
                        arrayList.add(new SafeBrowsingThreat(Integer.parseInt(jSONArray.getJSONObject(i).getString("threat_type"))));
                    } catch (NumberFormatException e) {
                    } catch (JSONException e2) {
                    }
                }
                return arrayList;
            } catch (JSONException e3) {
                return arrayList;
            }
        }

        @Override // com.google.android.gms.safetynet.SafetyNetApi.SafeBrowsingResult
        public final String getMetadata() {
            return this.zzbBI;
        }

        @Override // com.google.android.gms.common.api.Result
        public final Status getStatus() {
            return this.mStatus;
        }
    }

    static class zzj implements SafetyNetApi.VerifyAppsUserResult {
        private Status mStatus;
        private boolean zzzE;

        public zzj() {
        }

        public zzj(Status status, boolean z) {
            this.mStatus = status;
            this.zzzE = z;
        }

        @Override // com.google.android.gms.common.api.Result
        public final Status getStatus() {
            return this.mStatus;
        }

        @Override // com.google.android.gms.safetynet.SafetyNetApi.VerifyAppsUserResult
        public final boolean isVerifyAppsEnabled() {
            if (this.mStatus == null || !this.mStatus.isSuccess()) {
                return false;
            }
            return this.zzzE;
        }
    }

    public static PendingResult<SafetyNetApi.SafeBrowsingResult> zza(GoogleApiClient googleApiClient, String str, int i, String str2, int... iArr) {
        if (iArr.length == 0) {
            throw new IllegalArgumentException("Null threatTypes in lookupUri");
        }
        if (TextUtils.isEmpty(str)) {
            throw new IllegalArgumentException("Null or empty uri in lookupUri");
        }
        return googleApiClient.zzd(new zzcsd(googleApiClient, iArr, i, str, str2));
    }

    public static PendingResult<SafetyNetApi.AttestationResult> zza(GoogleApiClient googleApiClient, byte[] bArr, String str) {
        return googleApiClient.zzd(new zzcsb(googleApiClient, bArr, str));
    }

    @Override // com.google.android.gms.safetynet.SafetyNetApi
    public PendingResult<SafetyNetApi.AttestationResult> attest(GoogleApiClient googleApiClient, byte[] bArr) {
        return zza(googleApiClient, bArr, null);
    }

    @Override // com.google.android.gms.safetynet.SafetyNetApi
    public PendingResult<SafetyNetApi.VerifyAppsUserResult> enableVerifyApps(GoogleApiClient googleApiClient) {
        return googleApiClient.zzd(new zzcsf(this, googleApiClient));
    }

    @Override // com.google.android.gms.safetynet.SafetyNetApi
    public PendingResult<SafetyNetApi.VerifyAppsUserResult> isVerifyAppsEnabled(GoogleApiClient googleApiClient) {
        return googleApiClient.zzd(new zzcse(this, googleApiClient));
    }

    @Override // com.google.android.gms.safetynet.SafetyNetApi
    public boolean isVerifyAppsEnabled(Context context) {
        GoogleApiClient googleApiClientBuild = new GoogleApiClient.Builder(context).addApi(SafetyNet.API).build();
        try {
            if (!googleApiClientBuild.blockingConnect(3L, TimeUnit.SECONDS).isSuccess()) {
                return false;
            }
            SafetyNetApi.VerifyAppsUserResult verifyAppsUserResult = (SafetyNetApi.VerifyAppsUserResult) isVerifyAppsEnabled(googleApiClientBuild).await(3L, TimeUnit.SECONDS);
            boolean z = verifyAppsUserResult != null && verifyAppsUserResult.isVerifyAppsEnabled();
        } finally {
            if (googleApiClientBuild != null) {
                googleApiClientBuild.disconnect();
            }
        }
    }

    @Override // com.google.android.gms.safetynet.SafetyNetApi
    public PendingResult<SafetyNetApi.HarmfulAppsResult> listHarmfulApps(GoogleApiClient googleApiClient) {
        return googleApiClient.zzd(new zzcsg(this, googleApiClient));
    }

    @Override // com.google.android.gms.safetynet.SafetyNetApi
    public PendingResult<SafetyNetApi.SafeBrowsingResult> lookupUri(GoogleApiClient googleApiClient, String str, String str2, int... iArr) {
        return zza(googleApiClient, str, 1, str2, iArr);
    }

    @Override // com.google.android.gms.safetynet.SafetyNetApi
    public PendingResult<SafetyNetApi.SafeBrowsingResult> lookupUri(GoogleApiClient googleApiClient, String str, int... iArr) {
        return zza(googleApiClient, str, 1, null, iArr);
    }

    @Override // com.google.android.gms.safetynet.SafetyNetApi
    public PendingResult<SafetyNetApi.SafeBrowsingResult> lookupUri(GoogleApiClient googleApiClient, List<Integer> list, String str) {
        return zza(googleApiClient, list, str, null);
    }

    @Override // com.google.android.gms.safetynet.SafetyNetApi
    public boolean lookupUriInLocalBlacklist(String str, int... iArr) throws NoSuchAlgorithmException {
        if (iArr == null) {
            throw new IllegalArgumentException("Null threatTypes in lookupUri");
        }
        if (TextUtils.isEmpty(str)) {
            throw new IllegalArgumentException("Null or empty uri in lookupUri");
        }
        if (zzbBM == null || zzbBN == 0 || SystemClock.elapsedRealtime() - zzbBN >= 1200000) {
            return true;
        }
        if (zzbBM == null || zzbBM.size() == 0) {
            return true;
        }
        List<zzcsp> listZzAk = new zzcss(str).zzAk();
        if (listZzAk == null || listZzAk.isEmpty()) {
            return true;
        }
        for (zzcsp zzcspVar : listZzAk) {
            for (int i : iArr) {
                zzcsr zzcsrVar = zzbBM.get(i);
                int i2 = (zzcsrVar == null || zzcsrVar.zzr(zzcspVar.zzbu(4).getBytes())) ? 0 : i2 + 1;
                return true;
            }
        }
        return false;
    }

    @Override // com.google.android.gms.safetynet.SafetyNetApi
    public PendingResult<SafetyNetApi.RecaptchaTokenResult> verifyWithRecaptcha(GoogleApiClient googleApiClient, String str) {
        if (TextUtils.isEmpty(str)) {
            throw new IllegalArgumentException("Null or empty site key in verifyWithRecaptcha");
        }
        return googleApiClient.zzd(new zzcsh(this, googleApiClient, str));
    }

    public final PendingResult<SafetyNetApi.SafeBrowsingResult> zza(GoogleApiClient googleApiClient, List<Integer> list, String str, String str2) {
        if (list == null) {
            throw new IllegalArgumentException("Null threatTypes in lookupUri");
        }
        if (TextUtils.isEmpty(str)) {
            throw new IllegalArgumentException("Null or empty uri in lookupUri");
        }
        return googleApiClient.zzd(new zzcsc(this, googleApiClient, list, str, str2));
    }
}
