package com.google.android.gms.internal;

import android.content.Context;
import android.location.Location;
import android.net.ConnectivityManager;
import android.os.Bundle;
import android.text.TextUtils;
import com.google.android.gms.ads.identifier.AdvertisingIdClient;
import java.io.BufferedOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.UUID;
import java.util.concurrent.Future;
import java.util.concurrent.TimeUnit;
import org.apache.http.cookie.SM;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzabm extends zzaan {
    private static zzabm zzUx;
    private static final Object zzuF = new Object();
    private final Context mContext;
    private final com.google.android.gms.ads.internal.js.zzl zzLG;
    private final zzabl zzUy;
    private final zzmb zzUz;

    private zzabm(Context context, zzmb zzmbVar, zzabl zzablVar) {
        this(context, zzmbVar, zzablVar, com.google.android.gms.ads.internal.zzbs.zzbO().zzb(context, new zzaje(11020208, 11020208, true)).zzff());
    }

    private zzabm(Context context, zzmb zzmbVar, zzabl zzablVar, com.google.android.gms.ads.internal.js.zzl zzlVar) {
        this.mContext = context;
        this.zzUy = zzablVar;
        this.zzUz = zzmbVar;
        this.zzLG = zzlVar;
    }

    private static zzaai zza(Context context, com.google.android.gms.ads.internal.js.zzl zzlVar, zzmb zzmbVar, zzabl zzablVar, zzaae zzaaeVar) {
        String string;
        zzafr.zzaC("Starting ad request from service using: AFMA_getAd");
        zznb zznbVar = new zznb(((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCQ)).booleanValue(), "load_ad", zzaaeVar.zzvX.zzAs);
        if (zzaaeVar.versionCode > 10 && zzaaeVar.zzSP != -1) {
            zznbVar.zza(zznbVar.zzc(zzaaeVar.zzSP), "cts");
        }
        zzmz zzmzVarZzdS = zznbVar.zzdS();
        zzajm<Bundle> zzajmVarZzj = zzablVar.zzUv.zzj(context);
        Future<zzacn> futureZzo = zzablVar.zzUu.zzo(context);
        zzajm<String> zzajmVarZzaD = zzablVar.zzUp.zzaD(zzaaeVar.zzSA.packageName);
        zzajm<String> zzajmVarZze = zzablVar.zzUw.zze(zzaaeVar);
        Future<zzacb> futureZzn = com.google.android.gms.ads.internal.zzbs.zzbI().zzn(context);
        zzajh zzajhVar = new zzajh(null);
        Bundle bundle = zzaaeVar.zzSz.extras;
        zzajm<Location> zzajmVarZza = (!zzaaeVar.zzSV || (bundle != null && bundle.getString("_ad") != null)) ? zzajhVar : zzablVar.zzUs.zza(zzaaeVar.applicationInfo);
        zzajm<AdvertisingIdClient.Info> zzajmVarZzz = ((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDM)).booleanValue() ? zzablVar.zzUw.zzz(context) : new zzajh(null);
        Bundle bundle2 = (zzaaeVar.versionCode < 4 || zzaaeVar.zzSG == null) ? null : zzaaeVar.zzSG;
        ((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDg)).booleanValue();
        com.google.android.gms.ads.internal.zzbs.zzbz();
        if (zzagz.zzc(context, context.getPackageName(), "android.permission.ACCESS_NETWORK_STATE") && ((ConnectivityManager) context.getSystemService("connectivity")).getActiveNetworkInfo() == null) {
            zzafr.zzaC("Device is offline.");
        }
        String string2 = zzaaeVar.versionCode >= 7 ? zzaaeVar.zzSM : UUID.randomUUID().toString();
        zzabu zzabuVar = new zzabu(string2, zzaaeVar.applicationInfo.packageName);
        if (zzaaeVar.zzSz.extras != null && (string = zzaaeVar.zzSz.extras.getString("_ad")) != null) {
            return zzabt.zza(context, zzaaeVar, string);
        }
        List<String> listZza = zzablVar.zzUq.zza(zzaaeVar);
        Bundle bundle3 = (Bundle) zzaji.zza(zzajmVarZzj, null, ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGb)).longValue(), TimeUnit.MILLISECONDS);
        zzacn zzacnVar = (zzacn) zzaji.zza(futureZzo, null, ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEF)).longValue(), TimeUnit.MILLISECONDS);
        Location location = (Location) zzaji.zza(zzajmVarZza, null, ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFK)).longValue(), TimeUnit.MILLISECONDS);
        AdvertisingIdClient.Info info = (AdvertisingIdClient.Info) zzaji.zza(zzajmVarZzz, null, ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDN)).longValue(), TimeUnit.MILLISECONDS);
        String str = (String) zzaji.zza(zzajmVarZze, (Object) null);
        String str2 = (String) zzaji.zza(zzajmVarZzaD, (Object) null);
        zzacb zzacbVar = (zzacb) zzaji.zza(futureZzn, (Object) null);
        if (zzacbVar == null) {
            zzafr.zzaT("Error fetching device info. This is not recoverable.");
            return new zzaai(0);
        }
        zzabk zzabkVar = new zzabk();
        zzabkVar.zzUj = zzaaeVar;
        zzabkVar.zzUk = zzacbVar;
        zzabkVar.zzUh = zzacnVar;
        zzabkVar.zzzV = location;
        zzabkVar.zzUg = bundle3;
        zzabkVar.zzSB = str;
        zzabkVar.zzqi = info;
        if (listZza == null) {
            zzabkVar.zzSN.clear();
        }
        zzabkVar.zzSN = listZza;
        zzabkVar.zzSG = bundle2;
        zzabkVar.zzUi = str2;
        zzabkVar.zzUl = zzablVar.zzUo.zzf(context);
        zzabkVar.zzUm = zzablVar.zzUm;
        JSONObject jSONObjectZza = zzabt.zza(context, zzabkVar);
        if (jSONObjectZza == null) {
            return new zzaai(0);
        }
        if (zzaaeVar.versionCode < 7) {
            try {
                jSONObjectZza.put("request_id", string2);
            } catch (JSONException e) {
            }
        }
        String string3 = jSONObjectZza.toString();
        zznbVar.zza(zzmzVarZzdS, "arc");
        zzagz.zzZr.post(new zzabn(zzlVar, zzabuVar, zznbVar, zznbVar.zzdS(), string3));
        try {
            zzaca zzacaVar = zzabuVar.zzgG().get(10L, TimeUnit.SECONDS);
            if (zzacaVar == null) {
                return new zzaai(0);
            }
            if (zzacaVar.getErrorCode() != -2) {
                return new zzaai(zzacaVar.getErrorCode());
            }
            if (zznbVar.zzdW() != null) {
                zznbVar.zza(zznbVar.zzdW(), "rur");
            }
            zzaai zzaaiVarZza = TextUtils.isEmpty(zzacaVar.zzgL()) ? null : zzabt.zza(context, zzaaeVar, zzacaVar.zzgL());
            if (zzaaiVarZza == null && !TextUtils.isEmpty(zzacaVar.getUrl())) {
                zzaaiVarZza = zza(zzaaeVar, context, zzaaeVar.zzvT.zzaP, zzacaVar.getUrl(), str2, zzacaVar, zznbVar, zzablVar);
            }
            if (zzaaiVarZza == null) {
                zzaaiVarZza = new zzaai(0);
            }
            zznbVar.zza(zzmzVarZzdS, "tts");
            zzaaiVarZza.zzTB = zznbVar.zzdU();
            return zzaaiVarZza;
        } catch (Exception e2) {
            return new zzaai(0);
        } finally {
            zzagz.zzZr.post(new zzabq(zzablVar, context, zzabuVar, zzaaeVar));
        }
    }

    public static zzaai zza(zzaae zzaaeVar, Context context, String str, String str2, String str3, zzaca zzacaVar, zznb zznbVar, zzabl zzablVar) {
        InputStreamReader inputStreamReader;
        BufferedOutputStream bufferedOutputStream;
        zzmz zzmzVarZzdS = zznbVar != null ? zznbVar.zzdS() : null;
        try {
            zzaby zzabyVar = new zzaby(zzaaeVar, zzacaVar.zzgI());
            String strValueOf = String.valueOf(str2);
            zzafr.zzaC(strValueOf.length() != 0 ? "AdRequestServiceImpl: Sending request: ".concat(strValueOf) : new String("AdRequestServiceImpl: Sending request: "));
            URL url = new URL(str2);
            long jElapsedRealtime = com.google.android.gms.ads.internal.zzbs.zzbF().elapsedRealtime();
            URL url2 = url;
            int i = 0;
            while (true) {
                HttpURLConnection httpURLConnection = (HttpURLConnection) url2.openConnection();
                try {
                    com.google.android.gms.ads.internal.zzbs.zzbz().zza(context, str, false, httpURLConnection);
                    if (!TextUtils.isEmpty(str3) && zzacaVar.zzgK()) {
                        httpURLConnection.addRequestProperty("x-afma-drt-cookie", str3);
                    }
                    String str4 = zzaaeVar.zzSW;
                    if (!TextUtils.isEmpty(str4)) {
                        zzafr.zzaC("Sending webview cookie in ad request header.");
                        httpURLConnection.addRequestProperty(SM.COOKIE, str4);
                    }
                    if (zzacaVar != null && !TextUtils.isEmpty(zzacaVar.zzgJ())) {
                        httpURLConnection.setDoOutput(true);
                        byte[] bytes = zzacaVar.zzgJ().getBytes();
                        httpURLConnection.setFixedLengthStreamingMode(bytes.length);
                        try {
                            BufferedOutputStream bufferedOutputStream2 = new BufferedOutputStream(httpURLConnection.getOutputStream());
                            try {
                                bufferedOutputStream2.write(bytes);
                                com.google.android.gms.common.util.zzn.closeQuietly(bufferedOutputStream2);
                            } catch (Throwable th) {
                                th = th;
                                bufferedOutputStream = bufferedOutputStream2;
                                com.google.android.gms.common.util.zzn.closeQuietly(bufferedOutputStream);
                                throw th;
                            }
                        } catch (Throwable th2) {
                            th = th2;
                            bufferedOutputStream = null;
                        }
                    }
                    int responseCode = httpURLConnection.getResponseCode();
                    Map<String, List<String>> headerFields = httpURLConnection.getHeaderFields();
                    if (responseCode < 200 || responseCode >= 300) {
                        zza(url2.toString(), headerFields, (String) null, responseCode);
                        if (responseCode < 300 || responseCode >= 400) {
                            zzafr.zzaT(new StringBuilder(46).append("Received error HTTP response code: ").append(responseCode).toString());
                            zzaai zzaaiVar = new zzaai(0);
                            httpURLConnection.disconnect();
                            return zzaaiVar;
                        }
                        String headerField = httpURLConnection.getHeaderField("Location");
                        if (TextUtils.isEmpty(headerField)) {
                            zzafr.zzaT("No location header to follow redirect.");
                            zzaai zzaaiVar2 = new zzaai(0);
                            httpURLConnection.disconnect();
                            return zzaaiVar2;
                        }
                        url2 = new URL(headerField);
                        i++;
                        if (i > ((Integer) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGG)).intValue()) {
                            zzafr.zzaT("Too many redirects.");
                            zzaai zzaaiVar3 = new zzaai(0);
                            httpURLConnection.disconnect();
                            return zzaaiVar3;
                        }
                        zzabyVar.zzh(headerFields);
                        httpURLConnection.disconnect();
                        if (zzablVar != null) {
                        }
                    } else {
                        String string = url2.toString();
                        try {
                            InputStreamReader inputStreamReader2 = new InputStreamReader(httpURLConnection.getInputStream());
                            try {
                                com.google.android.gms.ads.internal.zzbs.zzbz();
                                String strZza = zzagz.zza(inputStreamReader2);
                                com.google.android.gms.common.util.zzn.closeQuietly(inputStreamReader2);
                                zza(string, headerFields, strZza, responseCode);
                                zzabyVar.zza(string, headerFields, strZza);
                                if (zznbVar != null) {
                                    zznbVar.zza(zzmzVarZzdS, "ufe");
                                }
                                zzaai zzaaiVarZze = zzabyVar.zze(jElapsedRealtime);
                                httpURLConnection.disconnect();
                                return zzaaiVarZze;
                            } catch (Throwable th3) {
                                th = th3;
                                inputStreamReader = inputStreamReader2;
                                com.google.android.gms.common.util.zzn.closeQuietly(inputStreamReader);
                                throw th;
                            }
                        } catch (Throwable th4) {
                            th = th4;
                            inputStreamReader = null;
                        }
                    }
                } catch (Throwable th5) {
                    httpURLConnection.disconnect();
                    throw th5;
                }
            }
        } catch (IOException e) {
            String strValueOf2 = String.valueOf(e.getMessage());
            zzafr.zzaT(strValueOf2.length() != 0 ? "Error while connecting to ad server: ".concat(strValueOf2) : new String("Error while connecting to ad server: "));
            return new zzaai(2);
        }
    }

    public static zzabm zza(Context context, zzmb zzmbVar, zzabl zzablVar) {
        zzabm zzabmVar;
        synchronized (zzuF) {
            if (zzUx == null) {
                if (context.getApplicationContext() != null) {
                    context = context.getApplicationContext();
                }
                zzmo.initialize(context);
                zzUx = new zzabm(context, zzmbVar, zzablVar);
            }
            zzabmVar = zzUx;
        }
        return zzabmVar;
    }

    private static void zza(String str, Map<String, List<String>> map, String str2, int i) {
        if (zzafr.zzz(2)) {
            zzafr.v(new StringBuilder(String.valueOf(str).length() + 39).append("Http Response: {\n  URL:\n    ").append(str).append("\n  Headers:").toString());
            if (map != null) {
                for (String str3 : map.keySet()) {
                    zzafr.v(new StringBuilder(String.valueOf(str3).length() + 5).append("    ").append(str3).append(":").toString());
                    Iterator<String> it = map.get(str3).iterator();
                    while (it.hasNext()) {
                        String strValueOf = String.valueOf(it.next());
                        zzafr.v(strValueOf.length() != 0 ? "      ".concat(strValueOf) : new String("      "));
                    }
                }
            }
            zzafr.v("  Body:");
            if (str2 != null) {
                for (int i2 = 0; i2 < Math.min(str2.length(), 100000); i2 += 1000) {
                    zzafr.v(str2.substring(i2, Math.min(str2.length(), i2 + 1000)));
                }
            } else {
                zzafr.v("    null");
            }
            zzafr.v(new StringBuilder(34).append("  Response Code:\n    ").append(i).append("\n}").toString());
        }
    }

    @Override // com.google.android.gms.internal.zzaam
    public final void zza(zzaae zzaaeVar, zzaap zzaapVar) {
        com.google.android.gms.ads.internal.zzbs.zzbD().zzd(this.mContext, zzaaeVar.zzvT);
        zzajm<Void> zzajmVarZza = zzagt.zza(new zzabr(this, zzaaeVar, zzaapVar));
        com.google.android.gms.ads.internal.zzbs.zzbP().zzie();
        com.google.android.gms.ads.internal.zzbs.zzbP().getHandler().postDelayed(new zzabs(this, zzajmVarZza), 60000L);
    }

    @Override // com.google.android.gms.internal.zzaam
    public final void zza(zzaax zzaaxVar, zzaas zzaasVar) {
        zzafr.v("Nonagon code path entered in octagon");
        throw new IllegalArgumentException();
    }

    @Override // com.google.android.gms.internal.zzaam
    public final zzaai zzc(zzaae zzaaeVar) {
        return zza(this.mContext, this.zzLG, this.zzUz, this.zzUy, zzaaeVar);
    }
}
