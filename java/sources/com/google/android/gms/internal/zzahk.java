package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.content.Context;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.renderscript.Allocation;
import android.renderscript.Element;
import android.renderscript.RenderScript;
import android.renderscript.ScriptIntrinsicBlur;
import android.text.TextUtils;
import android.webkit.WebSettings;
import java.io.File;

/* JADX INFO: loaded from: classes.dex */
@TargetApi(17)
public class zzahk extends zzahm {
    @Override // com.google.android.gms.internal.zzahe
    public final String getDefaultUserAgent(Context context) {
        zzaiu zzaiuVarZzif = zzaiu.zzif();
        if (TextUtils.isEmpty(zzaiuVarZzif.zzJP)) {
            zzaiuVarZzif.zzJP = (String) zzait.zzb(new zzaiv(zzaiuVarZzif, com.google.android.gms.common.zzo.getRemoteContext(context), context));
        }
        return zzaiuVarZzif.zzJP;
    }

    @Override // com.google.android.gms.internal.zzahe
    public final void zzR(Context context) {
        zzaiu zzaiuVarZzif = zzaiu.zzif();
        zzafr.v("Updating user agent.");
        String defaultUserAgent = WebSettings.getDefaultUserAgent(context);
        if (!defaultUserAgent.equals(zzaiuVarZzif.zzJP)) {
            Context remoteContext = com.google.android.gms.common.zzo.getRemoteContext(context);
            if (remoteContext == null) {
                SharedPreferences.Editor editorPutString = context.getSharedPreferences("admob_user_agent", 0).edit().putString("user_agent", WebSettings.getDefaultUserAgent(context));
                if (remoteContext == null) {
                    editorPutString.apply();
                } else {
                    File file = new File(context.getApplicationInfo().dataDir, "shared_prefs");
                    File parentFile = file.getParentFile();
                    if (parentFile != null) {
                        parentFile.setExecutable(true, false);
                    }
                    file.setExecutable(true, false);
                    editorPutString.commit();
                    new File(file, String.valueOf("admob_user_agent").concat(".xml")).setReadable(true, false);
                }
            }
            zzaiuVarZzif.zzJP = defaultUserAgent;
        }
        zzafr.v("User agent is updated.");
        com.google.android.gms.ads.internal.zzbs.zzbD().zzhH();
    }

    @Override // com.google.android.gms.internal.zzahe
    public final Drawable zza(Context context, Bitmap bitmap, boolean z, float f) {
        if (!z || f <= 0.0f || f > 25.0f) {
            return new BitmapDrawable(context.getResources(), bitmap);
        }
        try {
            Bitmap bitmapCreateScaledBitmap = Bitmap.createScaledBitmap(bitmap, bitmap.getWidth(), bitmap.getHeight(), false);
            Bitmap bitmapCreateBitmap = Bitmap.createBitmap(bitmapCreateScaledBitmap);
            RenderScript renderScriptCreate = RenderScript.create(context);
            ScriptIntrinsicBlur scriptIntrinsicBlurCreate = ScriptIntrinsicBlur.create(renderScriptCreate, Element.U8_4(renderScriptCreate));
            Allocation allocationCreateFromBitmap = Allocation.createFromBitmap(renderScriptCreate, bitmapCreateScaledBitmap);
            Allocation allocationCreateFromBitmap2 = Allocation.createFromBitmap(renderScriptCreate, bitmapCreateBitmap);
            scriptIntrinsicBlurCreate.setRadius(f);
            scriptIntrinsicBlurCreate.setInput(allocationCreateFromBitmap);
            scriptIntrinsicBlurCreate.forEach(allocationCreateFromBitmap2);
            allocationCreateFromBitmap2.copyTo(bitmapCreateBitmap);
            return new BitmapDrawable(context.getResources(), bitmapCreateBitmap);
        } catch (RuntimeException e) {
            return new BitmapDrawable(context.getResources(), bitmap);
        }
    }

    @Override // com.google.android.gms.internal.zzahm, com.google.android.gms.internal.zzahh, com.google.android.gms.internal.zzahe
    public final boolean zza(Context context, WebSettings webSettings) {
        super.zza(context, webSettings);
        webSettings.setMediaPlaybackRequiresUserGesture(false);
        return true;
    }
}
