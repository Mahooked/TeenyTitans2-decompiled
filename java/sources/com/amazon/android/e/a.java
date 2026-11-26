package com.amazon.android.e;

import android.app.Activity;
import android.content.Intent;

/* JADX INFO: loaded from: classes.dex */
final class a {
    final int a;
    Activity b;
    private final Intent c;

    public a(Intent intent, int i) {
        this.c = intent;
        this.a = i;
    }

    public final void a(Activity activity) {
        c.a.trace("Calling startActivityForResult from: " + activity);
        activity.startActivityForResult(this.c, this.a);
        this.b = activity;
    }
}
