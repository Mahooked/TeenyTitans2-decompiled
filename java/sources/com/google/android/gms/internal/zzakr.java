package com.google.android.gms.internal;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.MutableContextWrapper;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzakr extends MutableContextWrapper {
    private Context mApplicationContext;
    private Activity zzaaC;
    private Context zzacs;

    public zzakr(Context context) {
        super(context);
        setBaseContext(context);
    }

    @Override // android.content.ContextWrapper, android.content.Context
    public final Object getSystemService(String str) {
        return this.zzacs.getSystemService(str);
    }

    @Override // android.content.MutableContextWrapper
    public final void setBaseContext(Context context) {
        this.mApplicationContext = context.getApplicationContext();
        this.zzaaC = context instanceof Activity ? (Activity) context : null;
        this.zzacs = context;
        super.setBaseContext(this.mApplicationContext);
    }

    @Override // android.content.ContextWrapper, android.content.Context
    public final void startActivity(Intent intent) {
        if (this.zzaaC != null) {
            this.zzaaC.startActivity(intent);
        } else {
            intent.setFlags(268435456);
            this.mApplicationContext.startActivity(intent);
        }
    }

    public final Activity zzis() {
        return this.zzaaC;
    }

    public final Context zzit() {
        return this.zzacs;
    }
}
