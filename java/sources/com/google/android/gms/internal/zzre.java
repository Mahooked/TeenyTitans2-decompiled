package com.google.android.gms.internal;

import android.content.Context;
import android.text.TextUtils;
import java.io.BufferedOutputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzre implements zzrd {
    private final Context mContext;
    private final zzaje zztW;

    public zzre(Context context, zzaje zzajeVar) {
        this.mContext = context;
        this.zztW = zzajeVar;
    }

    /* JADX WARN: Code duplicated, block: B:30:0x00c5  */
    private final zzrj zza(zzri zzriVar) throws Throwable {
        HttpURLConnection httpURLConnection;
        Exception e;
        HttpURLConnection httpURLConnection2 = null;
        try {
            HttpURLConnection httpURLConnection3 = (HttpURLConnection) zzriVar.zzez().openConnection();
            try {
                com.google.android.gms.ads.internal.zzbs.zzbz().zza(this.mContext, this.zztW.zzaP, false, httpURLConnection3);
                ArrayList<zzrh> arrayListZzeA = zzriVar.zzeA();
                int size = arrayListZzeA.size();
                int i = 0;
                while (i < size) {
                    zzrh zzrhVar = arrayListZzeA.get(i);
                    i++;
                    zzrh zzrhVar2 = zzrhVar;
                    httpURLConnection3.addRequestProperty(zzrhVar2.getKey(), zzrhVar2.getValue());
                }
                if (!TextUtils.isEmpty(zzriVar.zzeB())) {
                    httpURLConnection3.setDoOutput(true);
                    byte[] bytes = zzriVar.zzeB().getBytes();
                    httpURLConnection3.setFixedLengthStreamingMode(bytes.length);
                    BufferedOutputStream bufferedOutputStream = new BufferedOutputStream(httpURLConnection3.getOutputStream());
                    bufferedOutputStream.write(bytes);
                    bufferedOutputStream.close();
                }
                ArrayList arrayList = new ArrayList();
                if (httpURLConnection3.getHeaderFields() != null) {
                    for (Map.Entry<String, List<String>> entry : httpURLConnection3.getHeaderFields().entrySet()) {
                        Iterator<String> it = entry.getValue().iterator();
                        while (it.hasNext()) {
                            arrayList.add(new zzrh(entry.getKey(), it.next()));
                        }
                    }
                }
                String strZzey = zzriVar.zzey();
                int responseCode = httpURLConnection3.getResponseCode();
                com.google.android.gms.ads.internal.zzbs.zzbz();
                zzrj zzrjVar = new zzrj(this, true, new zzrk(strZzey, responseCode, arrayList, zzagz.zza(new InputStreamReader(httpURLConnection3.getInputStream()))), null);
                if (httpURLConnection3 != null) {
                    httpURLConnection3.disconnect();
                }
                return zzrjVar;
            } catch (Exception e2) {
                e = e2;
                httpURLConnection = httpURLConnection3;
                try {
                    zzrj zzrjVar2 = new zzrj(this, false, null, e.toString());
                    if (httpURLConnection == null) {
                        return zzrjVar2;
                    }
                    httpURLConnection.disconnect();
                    return zzrjVar2;
                } catch (Throwable th) {
                    th = th;
                    httpURLConnection2 = httpURLConnection;
                    if (httpURLConnection2 != null) {
                        httpURLConnection2.disconnect();
                    }
                    throw th;
                }
            } catch (Throwable th2) {
                httpURLConnection2 = httpURLConnection3;
                th = th2;
                if (httpURLConnection2 != null) {
                    httpURLConnection2.disconnect();
                }
                throw th;
            }
        } catch (Exception e3) {
            httpURLConnection = null;
            e = e3;
        } catch (Throwable th3) {
            th = th3;
        }
    }

    private static JSONObject zza(zzrk zzrkVar) {
        JSONObject jSONObject = new JSONObject();
        try {
            jSONObject.put("http_request_id", zzrkVar.zzey());
            if (zzrkVar.getBody() != null) {
                jSONObject.put("body", zzrkVar.getBody());
            }
            JSONArray jSONArray = new JSONArray();
            for (zzrh zzrhVar : zzrkVar.zzeD()) {
                jSONArray.put(new JSONObject().put("key", zzrhVar.getKey()).put("value", zzrhVar.getValue()));
            }
            jSONObject.put("headers", jSONArray);
            jSONObject.put("response_code", zzrkVar.getResponseCode());
        } catch (JSONException e) {
            zzafr.zzb("Error constructing JSON for http response.", e);
        }
        return jSONObject;
    }

    private static zzri zzc(JSONObject jSONObject) {
        URL url;
        String strOptString = jSONObject.optString("http_request_id");
        String strOptString2 = jSONObject.optString("url");
        String strOptString3 = jSONObject.optString("post_body", null);
        try {
            url = new URL(strOptString2);
        } catch (MalformedURLException e) {
            zzafr.zzb("Error constructing http request.", e);
            url = null;
        }
        ArrayList arrayList = new ArrayList();
        JSONArray jSONArrayOptJSONArray = jSONObject.optJSONArray("headers");
        if (jSONArrayOptJSONArray == null) {
            jSONArrayOptJSONArray = new JSONArray();
        }
        for (int i = 0; i < jSONArrayOptJSONArray.length(); i++) {
            JSONObject jSONObjectOptJSONObject = jSONArrayOptJSONArray.optJSONObject(i);
            if (jSONObjectOptJSONObject != null) {
                arrayList.add(new zzrh(jSONObjectOptJSONObject.optString("key"), jSONObjectOptJSONObject.optString("value")));
            }
        }
        return new zzri(strOptString, url, arrayList, strOptString3);
    }

    public final JSONObject zzR(String str) {
        try {
            JSONObject jSONObject = new JSONObject(str);
            JSONObject jSONObject2 = new JSONObject();
            String strOptString = "";
            try {
                strOptString = jSONObject.optString("http_request_id");
                zzrj zzrjVarZza = zza(zzc(jSONObject));
                if (zzrjVarZza.isSuccess()) {
                    jSONObject2.put("response", zza(zzrjVarZza.zzeC()));
                    jSONObject2.put("success", true);
                } else {
                    jSONObject2.put("response", new JSONObject().put("http_request_id", strOptString));
                    jSONObject2.put("success", false);
                    jSONObject2.put("reason", zzrjVarZza.getReason());
                }
                return jSONObject2;
            } catch (Exception e) {
                try {
                    jSONObject2.put("response", new JSONObject().put("http_request_id", strOptString));
                    jSONObject2.put("success", false);
                    jSONObject2.put("reason", e.toString());
                    return jSONObject2;
                } catch (JSONException e2) {
                    return jSONObject2;
                }
            }
        } catch (JSONException e3) {
            zzafr.e("The request is not a valid JSON.");
            try {
                return new JSONObject().put("success", false);
            } catch (JSONException e4) {
                return new JSONObject();
            }
        }
    }

    @Override // com.google.android.gms.internal.zzrd
    public final void zza(zzaka zzakaVar, Map<String, String> map) {
        zzagt.zza(new zzrf(this, map, zzakaVar));
    }
}
