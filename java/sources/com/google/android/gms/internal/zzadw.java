package com.google.android.gms.internal;

import android.content.Context;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.concurrent.Future;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzadw extends zzafp implements zzadv {
    private final Context mContext;
    private final Object mLock;
    private final zzafg zzQQ;
    private final long zzWC;
    private final ArrayList<Future> zzWN;
    private final ArrayList<String> zzWO;
    private final HashMap<String, zzadm> zzWP;
    private final List<zzadp> zzWQ;
    private final HashSet<String> zzWR;
    private final zzacs zzWS;

    public zzadw(Context context, zzafg zzafgVar, zzacs zzacsVar) {
        this(context, zzafgVar, zzacsVar, ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDI)).longValue());
    }

    private zzadw(Context context, zzafg zzafgVar, zzacs zzacsVar, long j) {
        this.zzWN = new ArrayList<>();
        this.zzWO = new ArrayList<>();
        this.zzWP = new HashMap<>();
        this.zzWQ = new ArrayList();
        this.zzWR = new HashSet<>();
        this.mLock = new Object();
        this.mContext = context;
        this.zzQQ = zzafgVar;
        this.zzWS = zzacsVar;
        this.zzWC = j;
    }

    private final zzaff zza(int i, @Nullable String str, @Nullable zzua zzuaVar) {
        return new zzaff(this.zzQQ.zzUj.zzSz, null, this.zzQQ.zzXY.zzMa, i, this.zzQQ.zzXY.zzMb, this.zzQQ.zzXY.zzTq, this.zzQQ.zzXY.orientation, this.zzQQ.zzXY.zzMg, this.zzQQ.zzUj.zzSC, this.zzQQ.zzXY.zzTo, zzuaVar, null, str, this.zzQQ.zzXN, null, this.zzQQ.zzXY.zzTp, this.zzQQ.zzvX, this.zzQQ.zzXY.zzTn, this.zzQQ.zzXR, this.zzQQ.zzXY.zzTs, this.zzQQ.zzXY.zzTt, this.zzQQ.zzXL, null, this.zzQQ.zzXY.zzTD, this.zzQQ.zzXY.zzTE, this.zzQQ.zzXY.zzTF, this.zzQQ.zzXY.zzTG, this.zzQQ.zzXY.zzTH, zzgV(), this.zzQQ.zzXY.zzMd, this.zzQQ.zzXY.zzTK, this.zzQQ.zzXX);
    }

    private final String zzgV() {
        int i;
        StringBuilder sb = new StringBuilder("");
        if (this.zzWQ == null) {
            return sb.toString();
        }
        for (zzadp zzadpVar : this.zzWQ) {
            if (zzadpVar != null && !TextUtils.isEmpty(zzadpVar.zzLK)) {
                String str = zzadpVar.zzLK;
                switch (zzadpVar.errorCode) {
                    case 3:
                        i = 1;
                        break;
                    case 4:
                        i = 2;
                        break;
                    case 5:
                        i = 4;
                        break;
                    case 6:
                        i = 0;
                        break;
                    case 7:
                        i = 3;
                        break;
                    default:
                        i = 6;
                        break;
                }
                sb.append(String.valueOf(new StringBuilder(String.valueOf(str).length() + 33).append(str).append(".").append(i).append(".").append(zzadpVar.zzML).toString()).concat("_"));
            }
        }
        return sb.substring(0, Math.max(0, sb.length() - 1));
    }

    @Override // com.google.android.gms.internal.zzafp
    public void onStop() {
    }

    @Override // com.google.android.gms.internal.zzadv
    public final void zza(String str, int i) {
    }

    @Override // com.google.android.gms.internal.zzadv
    public final void zzaw(String str) {
        synchronized (this.mLock) {
            this.zzWR.add(str);
        }
    }

    @Override // com.google.android.gms.internal.zzafp
    public final void zzbd() {
        zzadm zzadmVar;
        zzadm zzadmVar2;
        zzadm zzadmVar3;
        zzadm zzadmVar4;
        String string;
        for (zzua zzuaVar : this.zzQQ.zzXN.zzLY) {
            String str = zzuaVar.zzLP;
            for (String str2 : zzuaVar.zzLJ) {
                if ("com.google.android.gms.ads.mediation.customevent.CustomEventAdapter".equals(str2) || "com.google.ads.mediation.customevent.CustomEventAdapter".equals(str2)) {
                    try {
                        string = new JSONObject(str).getString("class_name");
                    } catch (JSONException e) {
                        zzafr.zzb("Unable to determine custom event class name, skipping...", e);
                    }
                } else {
                    string = str2;
                }
                synchronized (this.mLock) {
                    zzadz zzadzVarZzav = this.zzWS.zzav(string);
                    if (zzadzVarZzav == null || zzadzVarZzav.zzgX() == null || zzadzVarZzav.zzgW() == null) {
                        this.zzWQ.add(new zzadr().zzay(zzuaVar.zzLK).zzax(string).zzg(0L).zzw(7).zzgU());
                    } else {
                        zzadm zzadmVar5 = new zzadm(this.mContext, string, str, zzuaVar, this.zzQQ, zzadzVarZzav, this, this.zzWC);
                        this.zzWN.add((Future) zzadmVar5.zzgp());
                        this.zzWO.add(string);
                        this.zzWP.put(string, zzadmVar5);
                    }
                }
            }
        }
        for (int i = 0; i < this.zzWN.size(); i++) {
            try {
                try {
                    this.zzWN.get(i).get();
                    synchronized (this.mLock) {
                        String str3 = this.zzWO.get(i);
                        if (!TextUtils.isEmpty(str3) && (zzadmVar4 = this.zzWP.get(str3)) != null) {
                            this.zzWQ.add(zzadmVar4.zzgR());
                        }
                    }
                    synchronized (this.mLock) {
                        if (this.zzWR.contains(this.zzWO.get(i))) {
                            String str4 = this.zzWO.get(i);
                            zzaiy.zzaaH.post(new zzadx(this, zza(-2, str4, this.zzWP.get(str4) != null ? this.zzWP.get(str4).zzgS() : null)));
                            return;
                        }
                    }
                } catch (Throwable th) {
                    synchronized (this.mLock) {
                        String str5 = this.zzWO.get(i);
                        if (!TextUtils.isEmpty(str5) && (zzadmVar3 = this.zzWP.get(str5)) != null) {
                            this.zzWQ.add(zzadmVar3.zzgR());
                        }
                        throw th;
                    }
                }
            } catch (InterruptedException e2) {
                Thread.currentThread().interrupt();
                synchronized (this.mLock) {
                    String str6 = this.zzWO.get(i);
                    if (!TextUtils.isEmpty(str6) && (zzadmVar2 = this.zzWP.get(str6)) != null) {
                        this.zzWQ.add(zzadmVar2.zzgR());
                    }
                    zzaiy.zzaaH.post(new zzady(this, zza(3, null, null)));
                }
            } catch (Exception e3) {
                zzafr.zzc("Unable to resolve rewarded adapter.", e3);
                synchronized (this.mLock) {
                    String str7 = this.zzWO.get(i);
                    if (!TextUtils.isEmpty(str7) && (zzadmVar = this.zzWP.get(str7)) != null) {
                        this.zzWQ.add(zzadmVar.zzgR());
                    }
                }
            }
        }
        zzaiy.zzaaH.post(new zzady(this, zza(3, null, null)));
    }
}
