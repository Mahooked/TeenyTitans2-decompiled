package com.google.android.gms.ads.internal;

import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.os.Bundle;
import android.os.IBinder;
import android.os.RemoteException;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import android.util.Base64;
import android.view.View;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.internal.zzaff;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zznq;
import com.google.android.gms.internal.zzns;
import com.google.android.gms.internal.zzos;
import com.google.android.gms.internal.zzot;
import com.google.android.gms.internal.zzrd;
import com.google.android.gms.internal.zzuh;
import com.google.android.gms.internal.zzvc;
import com.google.android.gms.internal.zzvf;
import com.google.android.gms.internal.zzzn;
import java.io.ByteArrayOutputStream;
import java.util.Iterator;
import java.util.List;
import java.util.concurrent.CountDownLatch;
import org.apache.http.protocol.HTTP;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzar {
    static zzrd zza(@Nullable zzvc zzvcVar, @Nullable zzvf zzvfVar, zzab zzabVar) {
        return new zzaw(zzvcVar, zzabVar, zzvfVar);
    }

    private static String zza(@Nullable Bitmap bitmap) {
        ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
        if (bitmap == null) {
            zzafr.zzaT("Bitmap is null. Returning empty string");
            return "";
        }
        bitmap.compress(Bitmap.CompressFormat.PNG, 100, byteArrayOutputStream);
        String strEncodeToString = Base64.encodeToString(byteArrayOutputStream.toByteArray(), 0);
        String strValueOf = String.valueOf("data:image/png;base64,");
        String strValueOf2 = String.valueOf(strEncodeToString);
        return strValueOf2.length() != 0 ? strValueOf.concat(strValueOf2) : new String(strValueOf);
    }

    static String zza(@Nullable zzos zzosVar) {
        if (zzosVar == null) {
            zzafr.zzaT("Image is null. Returning empty string");
            return "";
        }
        try {
            Uri uri = zzosVar.getUri();
            if (uri != null) {
                return uri.toString();
            }
        } catch (RemoteException e) {
            zzafr.zzaT("Unable to get image uri. Trying data uri next");
        }
        return zzb(zzosVar);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static JSONObject zza(@Nullable Bundle bundle, String str) throws JSONException {
        JSONObject jSONObject = new JSONObject();
        if (bundle == null || TextUtils.isEmpty(str)) {
            return jSONObject;
        }
        JSONObject jSONObject2 = new JSONObject(str);
        Iterator<String> itKeys = jSONObject2.keys();
        while (itKeys.hasNext()) {
            String next = itKeys.next();
            if (bundle.containsKey(next)) {
                if ("image".equals(jSONObject2.getString(next))) {
                    Object obj = bundle.get(next);
                    if (obj instanceof Bitmap) {
                        jSONObject.put(next, zza((Bitmap) obj));
                    } else {
                        zzafr.zzaT("Invalid type. An image type extra should return a bitmap");
                    }
                } else if (bundle.get(next) instanceof Bitmap) {
                    zzafr.zzaT("Invalid asset type. Bitmap should be returned only for image type");
                } else {
                    jSONObject.put(next, String.valueOf(bundle.get(next)));
                }
            }
        }
        return jSONObject;
    }

    public static boolean zza(zzaka zzakaVar, zzuh zzuhVar, CountDownLatch countDownLatch) {
        boolean z;
        try {
            View view = zzakaVar.getView();
            if (view == null) {
                zzafr.zzaT("AdWebView is null");
                z = false;
            } else {
                view.setVisibility(4);
                List<String> list = zzuhVar.zzMG.zzLV;
                if (list == null || list.isEmpty()) {
                    zzafr.zzaT("No template ids present in mediation response");
                    z = false;
                } else {
                    zzakaVar.zziw().zza("/nativeExpressAssetsLoaded", new zzau(countDownLatch));
                    zzakaVar.zziw().zza("/nativeExpressAssetsLoadingFailed", new zzav(countDownLatch));
                    zzvc zzvcVarZzfq = zzuhVar.zzMH.zzfq();
                    zzvf zzvfVarZzfr = zzuhVar.zzMH.zzfr();
                    if (list.contains("2") && zzvcVarZzfq != null) {
                        zzakaVar.zziw().zza(new zzas(new zznq(zzvcVarZzfq.getHeadline(), zzvcVarZzfq.getImages(), zzvcVarZzfq.getBody(), zzvcVarZzfq.zzeh(), zzvcVarZzfq.getCallToAction(), zzvcVarZzfq.getStarRating(), zzvcVarZzfq.getStore(), zzvcVarZzfq.getPrice(), null, zzvcVarZzfq.getExtras(), null, null), zzuhVar.zzMG.zzLU, zzakaVar));
                    } else if (!list.contains("1") || zzvfVarZzfr == null) {
                        zzafr.zzaT("No matching template id and mapper");
                        z = false;
                    } else {
                        zzakaVar.zziw().zza(new zzat(new zzns(zzvfVarZzfr.getHeadline(), zzvfVarZzfr.getImages(), zzvfVarZzfr.getBody(), zzvfVarZzfr.zzem(), zzvfVarZzfr.getCallToAction(), zzvfVarZzfr.getAdvertiser(), null, zzvfVarZzfr.getExtras(), null, null), zzuhVar.zzMG.zzLU, zzakaVar));
                    }
                    String str = zzuhVar.zzMG.zzLS;
                    String str2 = zzuhVar.zzMG.zzLT;
                    if (str2 != null) {
                        zzakaVar.loadDataWithBaseURL(str2, str, "text/html", HTTP.UTF_8, null);
                    } else {
                        zzakaVar.loadData(str, "text/html", HTTP.UTF_8);
                    }
                    z = true;
                }
            }
        } catch (RemoteException e) {
            zzafr.zzc("Unable to invoke load assets", e);
            z = false;
        } catch (RuntimeException e2) {
            countDownLatch.countDown();
            throw e2;
        }
        if (!z) {
            countDownLatch.countDown();
        }
        return z;
    }

    private static String zzb(zzos zzosVar) {
        String strZza;
        try {
            IObjectWrapper iObjectWrapperZzeg = zzosVar.zzeg();
            if (iObjectWrapperZzeg == null) {
                zzafr.zzaT("Drawable is null. Returning empty string");
                strZza = "";
            } else {
                Drawable drawable = (Drawable) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapperZzeg);
                if (drawable instanceof BitmapDrawable) {
                    strZza = zza(((BitmapDrawable) drawable).getBitmap());
                } else {
                    zzafr.zzaT("Drawable is not an instance of BitmapDrawable. Returning empty string");
                    strZza = "";
                }
            }
            return strZza;
        } catch (RemoteException e) {
            zzafr.zzaT("Unable to get drawable. Returning empty string");
            return "";
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static void zzb(zzaka zzakaVar) {
        View.OnClickListener onClickListenerZziL = zzakaVar.zziL();
        if (onClickListenerZziL != null) {
            onClickListenerZziL.onClick(zzakaVar.getView());
        }
    }

    @Nullable
    public static View zzd(@Nullable zzaff zzaffVar) {
        if (zzaffVar == null) {
            zzafr.e("AdState is null");
            return null;
        }
        if (zze(zzaffVar) && zzaffVar.zzPg != null) {
            return zzaffVar.zzPg.getView();
        }
        try {
            IObjectWrapper view = zzaffVar.zzMH != null ? zzaffVar.zzMH.getView() : null;
            if (view != null) {
                return (View) com.google.android.gms.dynamic.zzn.zzE(view);
            }
            zzafr.zzaT("View in mediation adapter is null.");
            return null;
        } catch (RemoteException e) {
            zzafr.zzc("Could not get View from mediation adapter.", e);
            return null;
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Nullable
    public static zzos zzd(Object obj) {
        if (obj instanceof IBinder) {
            return zzot.zzi((IBinder) obj);
        }
        return null;
    }

    public static boolean zze(@Nullable zzaff zzaffVar) {
        return (zzaffVar == null || !zzaffVar.zzTo || zzaffVar.zzMG == null || zzaffVar.zzMG.zzLS == null) ? false : true;
    }
}
