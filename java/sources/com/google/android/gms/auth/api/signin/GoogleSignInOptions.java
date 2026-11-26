package com.google.android.gms.auth.api.signin;

import android.accounts.Account;
import android.os.Parcel;
import android.os.Parcelable;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.google.android.gms.auth.api.signin.internal.zzn;
import com.google.android.gms.auth.api.signin.internal.zzo;
import com.google.android.gms.common.Scopes;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.zzbo;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
public class GoogleSignInOptions extends com.google.android.gms.common.internal.safeparcel.zza implements Api.ApiOptions.Optional, ReflectedParcelable {
    private int versionCode;
    private Account zzajb;
    private final ArrayList<Scope> zzalY;
    private final boolean zzalZ;
    private boolean zzalh;
    private String zzali;
    private final boolean zzama;
    private String zzamb;
    private ArrayList<zzn> zzamc;
    private Map<Integer, zzn> zzamd;
    public static final Scope zzalV = new Scope(Scopes.PROFILE);
    public static final Scope zzalW = new Scope("email");
    public static final Scope zzalX = new Scope("openid");
    private static Scope SCOPE_GAMES = new Scope(Scopes.GAMES);
    public static final GoogleSignInOptions DEFAULT_SIGN_IN = new Builder().requestId().requestProfile().build();
    public static final GoogleSignInOptions DEFAULT_GAMES_SIGN_IN = new Builder().requestScopes(SCOPE_GAMES, new Scope[0]).build();
    public static final Parcelable.Creator<GoogleSignInOptions> CREATOR = new zzd();
    private static Comparator<Scope> zzalU = new zzc();

    public static final class Builder {
        private Account zzajb;
        private boolean zzalZ;
        private boolean zzalh;
        private String zzali;
        private boolean zzama;
        private String zzamb;
        private Set<Scope> zzame;
        private Map<Integer, zzn> zzamf;

        public Builder() {
            this.zzame = new HashSet();
            this.zzamf = new HashMap();
        }

        public Builder(@NonNull GoogleSignInOptions googleSignInOptions) {
            this.zzame = new HashSet();
            this.zzamf = new HashMap();
            zzbo.zzu(googleSignInOptions);
            this.zzame = new HashSet(googleSignInOptions.zzalY);
            this.zzalZ = googleSignInOptions.zzalZ;
            this.zzama = googleSignInOptions.zzama;
            this.zzalh = googleSignInOptions.zzalh;
            this.zzali = googleSignInOptions.zzali;
            this.zzajb = googleSignInOptions.zzajb;
            this.zzamb = googleSignInOptions.zzamb;
            this.zzamf = GoogleSignInOptions.zzw(googleSignInOptions.zzamc);
        }

        private final String zzbR(String str) {
            zzbo.zzcF(str);
            zzbo.zzb(this.zzali == null || this.zzali.equals(str), "two different server client ids provided");
            return str;
        }

        public final Builder addExtension(GoogleSignInOptionsExtension googleSignInOptionsExtension) {
            if (this.zzamf.containsKey(1)) {
                throw new IllegalStateException("Only one extension per type may be added");
            }
            this.zzamf.put(1, new zzn(googleSignInOptionsExtension));
            return this;
        }

        public final GoogleSignInOptions build() {
            if (this.zzalh && (this.zzajb == null || !this.zzame.isEmpty())) {
                requestId();
            }
            return new GoogleSignInOptions(3, new ArrayList(this.zzame), this.zzajb, this.zzalh, this.zzalZ, this.zzama, this.zzali, this.zzamb, this.zzamf, null);
        }

        public final Builder requestEmail() {
            this.zzame.add(GoogleSignInOptions.zzalW);
            return this;
        }

        public final Builder requestId() {
            this.zzame.add(GoogleSignInOptions.zzalX);
            return this;
        }

        public final Builder requestIdToken(String str) {
            this.zzalh = true;
            this.zzali = zzbR(str);
            return this;
        }

        public final Builder requestProfile() {
            this.zzame.add(GoogleSignInOptions.zzalV);
            return this;
        }

        public final Builder requestScopes(Scope scope, Scope... scopeArr) {
            this.zzame.add(scope);
            this.zzame.addAll(Arrays.asList(scopeArr));
            return this;
        }

        public final Builder requestServerAuthCode(String str) {
            return requestServerAuthCode(str, false);
        }

        public final Builder requestServerAuthCode(String str, boolean z) {
            this.zzalZ = true;
            this.zzali = zzbR(str);
            this.zzama = z;
            return this;
        }

        public final Builder setAccountName(String str) {
            this.zzajb = new Account(zzbo.zzcF(str), "com.google");
            return this;
        }

        public final Builder setHostedDomain(String str) {
            this.zzamb = zzbo.zzcF(str);
            return this;
        }
    }

    GoogleSignInOptions(int i, ArrayList<Scope> arrayList, Account account, boolean z, boolean z2, boolean z3, String str, String str2, ArrayList<zzn> arrayList2) {
        this(i, arrayList, account, z, z2, z3, str, str2, zzw(arrayList2));
    }

    private GoogleSignInOptions(int i, ArrayList<Scope> arrayList, Account account, boolean z, boolean z2, boolean z3, String str, String str2, Map<Integer, zzn> map) {
        this.versionCode = i;
        this.zzalY = arrayList;
        this.zzajb = account;
        this.zzalh = z;
        this.zzalZ = z2;
        this.zzama = z3;
        this.zzali = str;
        this.zzamb = str2;
        this.zzamc = new ArrayList<>(map.values());
        this.zzamd = map;
    }

    /* synthetic */ GoogleSignInOptions(int i, ArrayList arrayList, Account account, boolean z, boolean z2, boolean z3, String str, String str2, Map map, zzc zzcVar) {
        this(3, (ArrayList<Scope>) arrayList, account, z, z2, z3, str, str2, (Map<Integer, zzn>) map);
    }

    @Nullable
    public static GoogleSignInOptions zzbQ(@Nullable String str) throws JSONException {
        if (TextUtils.isEmpty(str)) {
            return null;
        }
        JSONObject jSONObject = new JSONObject(str);
        HashSet hashSet = new HashSet();
        JSONArray jSONArray = jSONObject.getJSONArray("scopes");
        int length = jSONArray.length();
        for (int i = 0; i < length; i++) {
            hashSet.add(new Scope(jSONArray.getString(i)));
        }
        String strOptString = jSONObject.optString("accountName", null);
        return new GoogleSignInOptions(3, (ArrayList<Scope>) new ArrayList(hashSet), !TextUtils.isEmpty(strOptString) ? new Account(strOptString, "com.google") : null, jSONObject.getBoolean("idTokenRequested"), jSONObject.getBoolean("serverAuthRequested"), jSONObject.getBoolean("forceCodeForRefreshToken"), jSONObject.optString("serverClientId", null), jSONObject.optString("hostedDomain", null), new HashMap());
    }

    private final JSONObject zzmz() {
        JSONObject jSONObject = new JSONObject();
        try {
            JSONArray jSONArray = new JSONArray();
            Collections.sort(this.zzalY, zzalU);
            ArrayList<Scope> arrayList = this.zzalY;
            int size = arrayList.size();
            int i = 0;
            while (i < size) {
                Scope scope = arrayList.get(i);
                i++;
                jSONArray.put(scope.zzpp());
            }
            jSONObject.put("scopes", jSONArray);
            if (this.zzajb != null) {
                jSONObject.put("accountName", this.zzajb.name);
            }
            jSONObject.put("idTokenRequested", this.zzalh);
            jSONObject.put("forceCodeForRefreshToken", this.zzama);
            jSONObject.put("serverAuthRequested", this.zzalZ);
            if (!TextUtils.isEmpty(this.zzali)) {
                jSONObject.put("serverClientId", this.zzali);
            }
            if (!TextUtils.isEmpty(this.zzamb)) {
                jSONObject.put("hostedDomain", this.zzamb);
            }
            return jSONObject;
        } catch (JSONException e) {
            throw new RuntimeException(e);
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static Map<Integer, zzn> zzw(@Nullable List<zzn> list) {
        HashMap map = new HashMap();
        if (list == null) {
            return map;
        }
        for (zzn zznVar : list) {
            map.put(Integer.valueOf(zznVar.getType()), zznVar);
        }
        return map;
    }

    public boolean equals(Object obj) {
        if (obj == null) {
            return false;
        }
        try {
            GoogleSignInOptions googleSignInOptions = (GoogleSignInOptions) obj;
            if (this.zzamc.size() > 0 || googleSignInOptions.zzamc.size() > 0 || this.zzalY.size() != googleSignInOptions.zzmA().size() || !this.zzalY.containsAll(googleSignInOptions.zzmA())) {
                return false;
            }
            if (this.zzajb == null) {
                if (googleSignInOptions.zzajb != null) {
                    return false;
                }
            } else if (!this.zzajb.equals(googleSignInOptions.zzajb)) {
                return false;
            }
            if (TextUtils.isEmpty(this.zzali)) {
                if (!TextUtils.isEmpty(googleSignInOptions.zzali)) {
                    return false;
                }
            } else if (!this.zzali.equals(googleSignInOptions.zzali)) {
                return false;
            }
            return this.zzama == googleSignInOptions.zzama && this.zzalh == googleSignInOptions.zzalh && this.zzalZ == googleSignInOptions.zzalZ;
        } catch (ClassCastException e) {
            return false;
        }
    }

    public final Account getAccount() {
        return this.zzajb;
    }

    public Scope[] getScopeArray() {
        return (Scope[]) this.zzalY.toArray(new Scope[this.zzalY.size()]);
    }

    public final String getServerClientId() {
        return this.zzali;
    }

    public int hashCode() {
        ArrayList arrayList = new ArrayList();
        ArrayList<Scope> arrayList2 = this.zzalY;
        int size = arrayList2.size();
        int i = 0;
        while (i < size) {
            Scope scope = arrayList2.get(i);
            i++;
            arrayList.add(scope.zzpp());
        }
        Collections.sort(arrayList);
        return new zzo().zzo(arrayList).zzo(this.zzajb).zzo(this.zzali).zzP(this.zzama).zzP(this.zzalh).zzP(this.zzalZ).zzmJ();
    }

    public final boolean isIdTokenRequested() {
        return this.zzalh;
    }

    @Override // android.os.Parcelable
    public void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.versionCode);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 2, zzmA(), false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, (Parcelable) this.zzajb, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 4, this.zzalh);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 5, this.zzalZ);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 6, this.zzama);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 7, this.zzali, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 8, this.zzamb, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 9, this.zzamc, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }

    public final ArrayList<Scope> zzmA() {
        return new ArrayList<>(this.zzalY);
    }

    public final boolean zzmB() {
        return this.zzalZ;
    }

    public final String zzmC() {
        return zzmz().toString();
    }
}
