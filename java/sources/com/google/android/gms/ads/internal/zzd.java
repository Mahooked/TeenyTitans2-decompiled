package com.google.android.gms.ads.internal;

import android.app.Activity;
import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.os.RemoteException;
import android.support.annotation.Nullable;
import android.util.DisplayMetrics;
import com.google.android.gms.internal.zzaaf;
import com.google.android.gms.internal.zzaaz;
import com.google.android.gms.internal.zzaff;
import com.google.android.gms.internal.zzafh;
import com.google.android.gms.internal.zzafj;
import com.google.android.gms.internal.zzafp;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagt;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzahe;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zzajm;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzbha;
import com.google.android.gms.internal.zzgz;
import com.google.android.gms.internal.zzij;
import com.google.android.gms.internal.zzir;
import com.google.android.gms.internal.zziv;
import com.google.android.gms.internal.zzky;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zznb;
import com.google.android.gms.internal.zzon;
import com.google.android.gms.internal.zzpj;
import com.google.android.gms.internal.zzpt;
import com.google.android.gms.internal.zztp;
import com.google.android.gms.internal.zzuc;
import com.google.android.gms.internal.zzuj;
import com.google.android.gms.internal.zzuq;
import com.google.android.gms.internal.zzzn;
import com.google.android.gms.internal.zzzq;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.UUID;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public abstract class zzd extends zza implements com.google.android.gms.ads.internal.overlay.zzw, zzbl, zzuc {
    protected final zzuq zzsX;
    private transient boolean zzsY;

    public zzd(Context context, zziv zzivVar, String str, zzuq zzuqVar, zzaje zzajeVar, zzv zzvVar) {
        this(new zzbt(context, zzivVar, str, zzajeVar), zzuqVar, null, zzvVar);
    }

    private zzd(zzbt zzbtVar, zzuq zzuqVar, @Nullable zzbi zzbiVar, zzv zzvVar) {
        super(zzbtVar, null, zzvVar);
        this.zzsX = zzuqVar;
        this.zzsY = false;
    }

    private final zzaaf zza(zzir zzirVar, Bundle bundle, zzafj zzafjVar) {
        PackageInfo packageInfo;
        ArrayList arrayList;
        ArrayList arrayList2;
        int i;
        JSONArray jSONArrayOptJSONArray;
        ApplicationInfo applicationInfo = this.zzsP.zzqD.getApplicationInfo();
        try {
            packageInfo = zzbha.zzaP(this.zzsP.zzqD).getPackageInfo(applicationInfo.packageName, 0);
            while (true) {
                int i2 = i;
                if (i2 >= this.zzsP.zzwi.size()) {
                    break;
                }
                String strKeyAt = this.zzsP.zzwi.keyAt(i2);
                arrayList.add(strKeyAt);
                if (this.zzsP.zzwh.containsKey(strKeyAt) && this.zzsP.zzwh.get(strKeyAt) != null) {
                    arrayList2.add(strKeyAt);
                }
                i = i2 + 1;
            }
        } catch (PackageManager.NameNotFoundException e) {
            packageInfo = null;
        }
        DisplayMetrics displayMetrics = this.zzsP.zzqD.getResources().getDisplayMetrics();
        Bundle bundle2 = null;
        if (this.zzsP.zzvU != null && this.zzsP.zzvU.getParent() != null) {
            int[] iArr = new int[2];
            this.zzsP.zzvU.getLocationOnScreen(iArr);
            int i3 = iArr[0];
            int i4 = iArr[1];
            int width = this.zzsP.zzvU.getWidth();
            int height = this.zzsP.zzvU.getHeight();
            int i5 = 0;
            if (this.zzsP.zzvU.isShown() && i3 + width > 0 && i4 + height > 0 && i3 <= displayMetrics.widthPixels && i4 <= displayMetrics.heightPixels) {
                i5 = 1;
            }
            bundle2 = new Bundle(5);
            bundle2.putInt("x", i3);
            bundle2.putInt("y", i4);
            bundle2.putInt("width", width);
            bundle2.putInt("height", height);
            bundle2.putInt("visible", i5);
        }
        String strZzhp = zzbs.zzbD().zzhp();
        this.zzsP.zzwa = new zzafh(strZzhp, this.zzsP.zzvR);
        this.zzsP.zzwa.zzo(zzirVar);
        zzbs.zzbz();
        String strZza = zzagz.zza(this.zzsP.zzqD, this.zzsP.zzvU, this.zzsP.zzvX);
        long value = 0;
        if (this.zzsP.zzwe != null) {
            try {
                value = this.zzsP.zzwe.getValue();
            } catch (RemoteException e2) {
                zzafr.zzaT("Cannot get correlation id, default to 0.");
            }
        }
        String string = UUID.randomUUID().toString();
        Bundle bundleZza = zzbs.zzbD().zza(this.zzsP.zzqD, this, strZzhp);
        arrayList = new ArrayList();
        arrayList2 = new ArrayList();
        i = 0;
        zzajm zzajmVarZza = zzagt.zza(new zze(this));
        zzajm zzajmVarZza2 = zzagt.zza(new zzf(this));
        String strZzhk = zzafjVar != null ? zzafjVar.zzhk() : null;
        String string2 = null;
        if (this.zzsP.zzwq != null && this.zzsP.zzwq.size() > 0) {
            int i6 = packageInfo != null ? packageInfo.versionCode : 0;
            if (i6 > zzbs.zzbD().zzhA()) {
                zzbs.zzbD().zzhF();
                zzbs.zzbD().zzx(i6);
            } else {
                JSONObject jSONObjectZzhE = zzbs.zzbD().zzhE();
                if (jSONObjectZzhE != null && (jSONArrayOptJSONArray = jSONObjectZzhE.optJSONArray(this.zzsP.zzvR)) != null) {
                    string2 = jSONArrayOptJSONArray.toString();
                }
            }
        }
        zziv zzivVar = this.zzsP.zzvX;
        String str = this.zzsP.zzvR;
        String sessionId = zzbs.zzbD().getSessionId();
        zzaje zzajeVar = this.zzsP.zzvT;
        List<String> list = this.zzsP.zzwq;
        boolean zZzhs = zzbs.zzbD().zzhs();
        int i7 = displayMetrics.widthPixels;
        int i8 = displayMetrics.heightPixels;
        float f = displayMetrics.density;
        List<String> listZzdJ = zzmo.zzdJ();
        String str2 = this.zzsP.zzvQ;
        zzon zzonVar = this.zzsP.zzwj;
        String strZzce = this.zzsP.zzce();
        zzbs.zzbz();
        float fZzbf = zzagz.zzbf();
        zzbs.zzbz();
        boolean zZzbh = zzagz.zzbh();
        zzbs.zzbz();
        int iZzN = zzagz.zzN(this.zzsP.zzqD);
        zzbs.zzbz();
        int iZzp = zzagz.zzp(this.zzsP.zzvU);
        boolean z = this.zzsP.zzqD instanceof Activity;
        boolean zZzhx = zzbs.zzbD().zzhx();
        boolean zZzhC = zzbs.zzbD().zzhC();
        int iZzeE = zzbs.zzbW().zzeE();
        zzbs.zzbz();
        Bundle bundleZzhS = zzagz.zzhS();
        String strZzib = zzbs.zzbH().zzib();
        zzky zzkyVar = this.zzsP.zzwl;
        boolean zZzic = zzbs.zzbH().zzic();
        Bundle bundleAsBundle = zztp.zzeN().asBundle();
        zzbs.zzbD();
        return new zzaaf(bundle2, zzirVar, zzivVar, str, applicationInfo, packageInfo, strZzhp, sessionId, zzajeVar, bundleZza, list, arrayList, bundle, zZzhs, i7, i8, f, strZza, value, string, listZzdJ, str2, zzonVar, strZzce, fZzbf, zZzbh, iZzN, iZzp, z, zZzhx, zzajmVarZza, strZzhk, zZzhC, iZzeE, bundleZzhS, strZzib, zzkyVar, zZzic, bundleAsBundle, this.zzsP.zzqD.getSharedPreferences("admob", 0).getStringSet("never_pool_slots", Collections.emptySet()).contains(this.zzsP.zzvR), zzajmVarZza2, this.zzsP.zzwn, string2, arrayList2);
    }

    @Override // com.google.android.gms.internal.zzjz
    public final String getMediationAdapterClassName() {
        if (this.zzsP.zzvY == null) {
            return null;
        }
        return this.zzsP.zzvY.zzMI;
    }

    @Override // com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzim
    public void onAdClicked() {
        if (this.zzsP.zzvY == null) {
            zzafr.zzaT("Ad state was null when trying to ping click URLs.");
            return;
        }
        if (this.zzsP.zzvY.zzXN != null && this.zzsP.zzvY.zzXN.zzMa != null) {
            zzbs.zzbS();
            zzuj.zza(this.zzsP.zzqD, this.zzsP.zzvT.zzaP, this.zzsP.zzvY, this.zzsP.zzvR, false, zzb(this.zzsP.zzvY.zzXN.zzMa));
        }
        if (this.zzsP.zzvY.zzMG != null && this.zzsP.zzvY.zzMG.zzLM != null) {
            zzbs.zzbS();
            zzuj.zza(this.zzsP.zzqD, this.zzsP.zzvT.zzaP, this.zzsP.zzvY, this.zzsP.zzvR, false, this.zzsP.zzvY.zzMG.zzLM);
        }
        super.onAdClicked();
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzw
    public void onPause() {
        this.zzsR.zzi(this.zzsP.zzvY);
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzw
    public void onResume() {
        this.zzsR.zzj(this.zzsP.zzvY);
    }

    @Override // com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public void pause() {
        com.google.android.gms.common.internal.zzbo.zzcz("pause must be called on the main UI thread.");
        if (this.zzsP.zzvY != null && this.zzsP.zzvY.zzPg != null && this.zzsP.zzcc()) {
            zzbs.zzbB();
            zzahe.zzk(this.zzsP.zzvY.zzPg);
        }
        if (this.zzsP.zzvY != null && this.zzsP.zzvY.zzMH != null) {
            try {
                this.zzsP.zzvY.zzMH.pause();
            } catch (RemoteException e) {
                zzafr.zzaT("Could not pause mediation adapter.");
            }
        }
        this.zzsR.zzi(this.zzsP.zzvY);
        this.zzsO.pause();
    }

    public final void recordImpression() {
        zza(this.zzsP.zzvY, false);
    }

    @Override // com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzjz
    public void resume() {
        com.google.android.gms.common.internal.zzbo.zzcz("resume must be called on the main UI thread.");
        zzaka zzakaVar = null;
        if (this.zzsP.zzvY != null && this.zzsP.zzvY.zzPg != null) {
            zzakaVar = this.zzsP.zzvY.zzPg;
        }
        if (zzakaVar != null && this.zzsP.zzcc()) {
            zzbs.zzbB();
            zzahe.zzl(this.zzsP.zzvY.zzPg);
        }
        if (this.zzsP.zzvY != null && this.zzsP.zzvY.zzMH != null) {
            try {
                this.zzsP.zzvY.zzMH.resume();
            } catch (RemoteException e) {
                zzafr.zzaT("Could not resume mediation adapter.");
            }
        }
        if (zzakaVar == null || !zzakaVar.zziD()) {
            this.zzsO.resume();
        }
        this.zzsR.zzj(this.zzsP.zzvY);
    }

    public void showInterstitial() {
        throw new IllegalStateException("showInterstitial is not supported for current ad type");
    }

    protected void zza(@Nullable zzaff zzaffVar, boolean z) {
        if (zzaffVar == null) {
            zzafr.zzaT("Ad state was null when trying to ping impression URLs.");
            return;
        }
        if (zzaffVar == null) {
            zzafr.zzaT("Ad state was null when trying to ping impression URLs.");
        } else {
            zzafr.zzaC("Pinging Impression URLs.");
            if (this.zzsP.zzwa != null) {
                this.zzsP.zzwa.zzhc();
            }
            if (zzaffVar.zzMb != null && !zzaffVar.zzXU) {
                zzbs.zzbz();
                zzagz.zza(this.zzsP.zzqD, this.zzsP.zzvT.zzaP, zzb(zzaffVar.zzMb));
                zzaffVar.zzXU = true;
            }
        }
        if (zzaffVar.zzXN != null && zzaffVar.zzXN.zzMb != null) {
            zzbs.zzbS();
            zzuj.zza(this.zzsP.zzqD, this.zzsP.zzvT.zzaP, zzaffVar, this.zzsP.zzvR, z, zzb(zzaffVar.zzXN.zzMb));
        }
        if (zzaffVar.zzMG == null || zzaffVar.zzMG.zzLN == null) {
            return;
        }
        zzbs.zzbS();
        zzuj.zza(this.zzsP.zzqD, this.zzsP.zzvT.zzaP, zzaffVar, this.zzsP.zzvR, z, zzaffVar.zzMG.zzLN);
    }

    @Override // com.google.android.gms.internal.zzuc
    public final void zza(zzpj zzpjVar, String str) {
        String customTemplateId;
        zzpt zzptVar = null;
        if (zzpjVar != null) {
            try {
                customTemplateId = zzpjVar.getCustomTemplateId();
            } catch (RemoteException e) {
                zzafr.zzc("Unable to call onCustomClick.", e);
                return;
            }
        } else {
            customTemplateId = null;
        }
        if (this.zzsP.zzwh != null && customTemplateId != null) {
            zzptVar = this.zzsP.zzwh.get(customTemplateId);
        }
        if (zzptVar == null) {
            zzafr.zzaT("Mediation adapter invoked onCustomClick but no listeners were set.");
        } else {
            zzptVar.zzb(zzpjVar, str);
        }
    }

    @Override // com.google.android.gms.ads.internal.zza
    final boolean zza(zzaff zzaffVar) {
        zzir zzirVar;
        boolean z = false;
        if (this.zzsQ != null) {
            zzirVar = this.zzsQ;
            this.zzsQ = null;
        } else {
            zzirVar = zzaffVar.zzSz;
            if (zzirVar.extras != null) {
                z = zzirVar.extras.getBoolean("_noRefresh", false);
            }
        }
        return zza(zzirVar, zzaffVar, z);
    }

    @Override // com.google.android.gms.ads.internal.zza
    protected boolean zza(@Nullable zzaff zzaffVar, zzaff zzaffVar2) {
        int i;
        int i2 = 0;
        if (zzaffVar != null && zzaffVar.zzMJ != null) {
            zzaffVar.zzMJ.zza((zzuc) null);
        }
        if (zzaffVar2.zzMJ != null) {
            zzaffVar2.zzMJ.zza(this);
        }
        if (zzaffVar2.zzXN != null) {
            i = zzaffVar2.zzXN.zzMn;
            i2 = zzaffVar2.zzXN.zzMo;
        } else {
            i = 0;
        }
        this.zzsP.zzwr.zzg(i, i2);
        return true;
    }

    protected boolean zza(zzir zzirVar, zzaff zzaffVar, boolean z) {
        if (!z && this.zzsP.zzcc()) {
            if (zzaffVar.zzMg > 0) {
                this.zzsO.zza(zzirVar, zzaffVar.zzMg);
            } else if (zzaffVar.zzXN != null && zzaffVar.zzXN.zzMg > 0) {
                this.zzsO.zza(zzirVar, zzaffVar.zzXN.zzMg);
            } else if (!zzaffVar.zzTo && zzaffVar.errorCode == 2) {
                this.zzsO.zzg(zzirVar);
            }
        }
        return this.zzsO.zzbo();
    }

    @Override // com.google.android.gms.ads.internal.zza
    public boolean zza(zzir zzirVar, zznb zznbVar) {
        zzafj zzafjVarZzhD;
        if (!zzaz()) {
            return false;
        }
        zzbs.zzbz();
        zzgz zzgzVarZzA = zzbs.zzbD().zzA(this.zzsP.zzqD);
        Bundle bundleZza = zzgzVarZzA == null ? null : zzagz.zza(zzgzVarZzA);
        this.zzsO.cancel();
        this.zzsP.zzwt = 0;
        if (((Boolean) zzbs.zzbL().zzd(zzmo.zzFO)).booleanValue()) {
            zzafjVarZzhD = zzbs.zzbD().zzhD();
            zzbs.zzbV().zza(this.zzsP.zzqD, this.zzsP.zzvT, false, zzafjVarZzhD, zzafjVarZzhD != null ? zzafjVarZzhD.zzhl() : null, this.zzsP.zzvR, null);
        } else {
            zzafjVarZzhD = null;
        }
        zzaaf zzaafVarZza = zza(zzirVar, bundleZza, zzafjVarZzhD);
        zznbVar.zzh("seq_num", zzaafVarZza.zzSC);
        zznbVar.zzh("request_id", zzaafVarZza.zzSM);
        zznbVar.zzh("session_id", zzaafVarZza.zzSD);
        if (zzaafVarZza.zzSA != null) {
            zznbVar.zzh("app_version", String.valueOf(zzaafVarZza.zzSA.versionCode));
        }
        zzbt zzbtVar = this.zzsP;
        zzbs.zzbv();
        Context context = this.zzsP.zzqD;
        zzij zzijVar = this.zzsS.zztp;
        zzafp zzaazVar = zzaafVarZza.zzSz.extras.getBundle("sdk_less_server_data") != null ? new zzaaz(context, zzaafVarZza, this, zzijVar) : new zzzq(context, zzaafVarZza, this, zzijVar);
        zzaazVar.zzhL();
        zzbtVar.zzvV = zzaazVar;
        return true;
    }

    public void zzaA() {
        this.zzsY = false;
        zzap();
        this.zzsP.zzwa.zzhe();
    }

    public void zzaB() {
        this.zzsY = true;
        zzar();
    }

    public void zzaC() {
        onAdClicked();
    }

    @Override // com.google.android.gms.internal.zzuc
    public final void zzaD() {
        zzaA();
    }

    @Override // com.google.android.gms.internal.zzuc
    public final void zzaE() {
        zzaq();
    }

    @Override // com.google.android.gms.internal.zzuc
    public final void zzaF() {
        zzaB();
    }

    @Override // com.google.android.gms.internal.zzuc
    public final void zzaG() {
        if (this.zzsP.zzvY != null) {
            String str = this.zzsP.zzvY.zzMI;
            zzafr.zzaT(new StringBuilder(String.valueOf(str).length() + 74).append("Mediation adapter ").append(str).append(" refreshed, but mediation adapters should never refresh.").toString());
        }
        zza(this.zzsP.zzvY, true);
        zzas();
    }

    public void zzaH() {
        recordImpression();
    }

    @Override // com.google.android.gms.internal.zzjz
    public final String zzaI() {
        if (this.zzsP.zzvY == null) {
            return null;
        }
        String str = this.zzsP.zzvY.zzMI;
        if (!("com.google.android.gms.ads.mediation.customevent.CustomEventAdapter".equals(str) || "com.google.ads.mediation.customevent.CustomEventAdapter".equals(str)) || this.zzsP.zzvY.zzMG == null) {
            return str;
        }
        try {
            return new JSONObject(this.zzsP.zzvY.zzMG.zzLP).getString("class_name");
        } catch (NullPointerException e) {
            return str;
        } catch (JSONException e2) {
            return str;
        }
    }

    @Override // com.google.android.gms.ads.internal.zzbl
    public final void zzaJ() {
        zzbs.zzbz();
        zzagz.runOnUiThread(new zzg(this));
    }

    @Override // com.google.android.gms.ads.internal.zzbl
    public final void zzaK() {
        zzbs.zzbz();
        zzagz.runOnUiThread(new zzh(this));
    }

    protected boolean zzaz() {
        zzbs.zzbz();
        if (zzagz.zzc(this.zzsP.zzqD, this.zzsP.zzqD.getPackageName(), "android.permission.INTERNET")) {
            zzbs.zzbz();
            if (zzagz.zzD(this.zzsP.zzqD)) {
                return true;
            }
        }
        return false;
    }

    @Override // com.google.android.gms.ads.internal.zza, com.google.android.gms.internal.zzxy
    public final void zzb(zzaff zzaffVar) {
        super.zzb(zzaffVar);
        if (zzaffVar.zzMG != null) {
            zzafr.zzaC("Disable the debug gesture detector on the mediation ad frame.");
            if (this.zzsP.zzvU != null) {
                this.zzsP.zzvU.zzci();
            }
            zzafr.zzaC("Pinging network fill URLs.");
            zzbs.zzbS();
            zzuj.zza(this.zzsP.zzqD, this.zzsP.zzvT.zzaP, zzaffVar, this.zzsP.zzvR, false, zzaffVar.zzMG.zzLO);
            if (zzaffVar.zzXN != null && zzaffVar.zzXN.zzMd != null && zzaffVar.zzXN.zzMd.size() > 0) {
                zzafr.zzaC("Pinging urls remotely");
                zzbs.zzbz().zza(this.zzsP.zzqD, zzaffVar.zzXN.zzMd);
            }
        } else {
            zzafr.zzaC("Enable the debug gesture detector on the admob ad frame.");
            if (this.zzsP.zzvU != null) {
                this.zzsP.zzvU.zzch();
            }
        }
        if (zzaffVar.errorCode != 3 || zzaffVar.zzXN == null || zzaffVar.zzXN.zzMc == null) {
            return;
        }
        zzafr.zzaC("Pinging no fill URLs.");
        zzbs.zzbS();
        zzuj.zza(this.zzsP.zzqD, this.zzsP.zzvT.zzaP, zzaffVar, this.zzsP.zzvR, false, zzaffVar.zzXN.zzMc);
    }

    @Override // com.google.android.gms.ads.internal.zza
    protected final boolean zzb(zzir zzirVar) {
        return super.zzb(zzirVar) && !this.zzsY;
    }

    @Override // com.google.android.gms.internal.zzuc
    public final void zze(String str, String str2) {
        onAppEvent(str, str2);
    }
}
