package com.amazon.android.e;

import android.app.Activity;
import android.content.Intent;

/* JADX INFO: loaded from: classes.dex */
public final class f {
    final int a;
    public final int b;
    private final Activity c;
    private final Intent d;

    public f(Activity activity, int i, int i2, Intent intent) {
        this.c = activity;
        this.a = i;
        this.b = i2;
        this.d = intent;
    }

    public final String toString() {
        return "ActivtyResult: [ requestCode: " + this.a + ", resultCode: " + this.b + ", activity: " + this.c + ", intent: " + this.d + "]";
    }
}
