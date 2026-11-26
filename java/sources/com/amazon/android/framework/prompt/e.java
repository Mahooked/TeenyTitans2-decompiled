package com.amazon.android.framework.prompt;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.net.Uri;
import android.os.Build;
import android.telephony.TelephonyManager;
import android.text.TextUtils;
import com.amazon.android.framework.context.ContextManager;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.util.KiwiLogger;
import java.io.UnsupportedEncodingException;
import java.net.URLEncoder;
import java.util.Locale;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
public final class e extends SimplePrompt implements com.amazon.android.framework.resource.b {
    private static final KiwiLogger a = new KiwiLogger("ShutdownPrompt");

    @Resource
    private ContextManager b;

    @Resource
    private com.amazon.android.n.a c;

    public e(PromptContent promptContent) {
        super(promptContent, b.EXTENDED);
        a.trace("new ShutdownPrompt called!");
    }

    private final Intent a(Context context) {
        String simOperator;
        String packageName = context.getPackageName();
        StringBuilder sb = new StringBuilder("https://www.amazon.com/appstore-error-help");
        PackageManager packageManager = context.getPackageManager();
        a(sb, "?k=", "1.1");
        a(sb, "&m=", this.content.getTitle());
        a(sb, "&ec=", String.valueOf(this.content.getErrorCode()));
        String str = (String) this.c.a("PACKAGE");
        if (str != null) {
            a(sb, "&t=", str);
            a(sb, "&tv=", str, packageManager);
        }
        String str2 = (String) this.c.a("COMMAND");
        if (str2 != null) {
            a(sb, "&c=", str2);
        }
        String str3 = (String) this.c.a("LICENSE_FAILURE_RAW_EXCEPTION");
        if (str3 != null) {
            a(sb, "&e=", str3);
        }
        String str4 = Build.PRODUCT;
        if (str4 != null) {
            a(sb, "&d=", str4);
        }
        String str5 = Build.MANUFACTURER;
        if (str5 != null) {
            a(sb, "&ma=", str5);
        }
        String str6 = Build.MODEL;
        if (str6 != null) {
            a(sb, "&mo=", str6);
        }
        String str7 = Build.ID;
        if (str7 != null) {
            a(sb, "&bn=", str7);
        }
        a(sb, "&sz=", String.valueOf(context.getResources().getConfiguration().screenLayout & 15));
        a(sb, "&s=", String.valueOf(Build.VERSION.SDK_INT));
        a(sb, "&p=", packageName);
        a(sb, "&pv=", packageName, packageManager);
        a(sb, "&l=", Locale.getDefault().getLanguage() + "_" + Locale.getDefault().getCountry());
        TelephonyManager telephonyManager = (TelephonyManager) context.getSystemService("phone");
        if (telephonyManager == null) {
            a.trace("tm was null!");
            simOperator = null;
        } else {
            a.trace("tm was not null.");
            int simState = telephonyManager.getSimState();
            if (simState != 5) {
                a.trace("SIM not ready, returning null.  State was " + simState);
                simOperator = null;
            } else {
                simOperator = telephonyManager.getSimOperator();
            }
        }
        if (!TextUtils.isEmpty(simOperator)) {
            a(sb, "&mc=", simOperator);
        }
        String string = sb.toString();
        a.trace("Starting browser for uri " + string);
        return new Intent("android.intent.action.VIEW", Uri.parse(string));
    }

    private static Intent a(Context context, String str) {
        return new Intent("android.intent.action.VIEW", Uri.parse("amzn://apps/android?p=" + context.getPackageName() + "&ref=" + str));
    }

    private void a(Context context, Intent intent) {
        if (b(context, intent)) {
            new g(context, this.b).execute(intent);
        } else {
            a.error("Could not start activity, nothing resolves for the given data.");
            b();
        }
    }

    private static void a(StringBuilder sb, String str, String str2) {
        try {
            String strEncode = URLEncoder.encode(str2, HTTP.UTF_8);
            sb.append(str);
            sb.append(strEncode);
        } catch (UnsupportedEncodingException e) {
            a.error("Failed to URL-encode argument", e);
        }
    }

    private static void a(StringBuilder sb, String str, String str2, PackageManager packageManager) {
        try {
            a(sb, str, String.valueOf(packageManager.getPackageInfo(str2, 0).versionCode));
        } catch (PackageManager.NameNotFoundException e) {
            a.error("Could not find information for package " + str2);
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static void b() {
        a.trace("doShutdown()");
        a.test("Killing application");
        System.exit(0);
    }

    private static boolean b(Context context, Intent intent) {
        intent.addFlags(268435456);
        return intent.resolveActivity(context.getPackageManager()) != null;
    }

    @Override // com.amazon.android.framework.prompt.SimplePrompt
    protected final boolean canHandleAction(a aVar) {
        if (aVar == a.DEFAULT) {
            return true;
        }
        Intent intentA = null;
        Activity root = this.b.getRoot();
        if (aVar == a.DEEPLINK) {
            intentA = a(root, "badcert");
        } else if (aVar == a.HELP) {
            intentA = a(root);
        }
        if (intentA != null) {
            return b(root, intentA);
        }
        a.error("Unexpected action " + aVar + ", could not create intent.");
        return false;
    }

    @Override // com.amazon.android.framework.prompt.SimplePrompt
    protected final void doAction() {
        doAction(0);
    }

    @Override // com.amazon.android.framework.prompt.SimplePrompt
    protected final void doAction(int i) {
        a.trace("doAction(" + i + ")");
        a[] buttonActions = this.content.getButtonActions();
        a aVar = i < buttonActions.length ? buttonActions[i] : a.DEFAULT;
        if (aVar != a.HELP) {
            if (aVar != a.DEEPLINK) {
                if (aVar == a.DEFAULT) {
                    b();
                    return;
                }
                return;
            } else {
                a.trace("doAction() deeplink");
                a.trace("Attempting to deep link to appstore.");
                Activity root = this.b.getRoot();
                a(root, a(root, "badcert"));
                return;
            }
        }
        a.trace("doAction() help");
        a.trace("Store contains " + this.c.toString());
        if (!this.content.shouldShowFixup()) {
            a.trace("Should not show fixup");
            new p(this.b).execute(new Void[0]);
        } else {
            a.trace("Should show fixup");
            Activity root2 = this.b.getRoot();
            a(root2, a(root2));
            a.trace("Attempted to start browser.");
        }
    }

    @Override // com.amazon.android.h.c
    protected final long getExpirationDurationInSeconds() {
        return 31536000L;
    }

    @Override // com.amazon.android.h.b
    public final void onResourcesPopulatedImpl() {
        this.b.stopServices();
    }

    public final String toString() {
        return "ShutdownPrompt";
    }
}
