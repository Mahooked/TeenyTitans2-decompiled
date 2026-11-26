package com.unity3d.player;

import android.app.Activity;
import android.content.Context;
import android.os.IBinder;

/* JADX INFO: loaded from: classes.dex */
class GoogleARProxy extends b {
    private boolean f;

    GoogleARProxy(d dVar) {
        super("Google AR", dVar);
        this.f = false;
    }

    private boolean a(ClassLoader classLoader) {
        if (this.f) {
            return true;
        }
        try {
            Class<?> clsLoadClass = classLoader.loadClass("com.unity3d.unitygar.GoogleAR");
            m mVar = new m(clsLoadClass, clsLoadClass.getConstructor(new Class[0]).newInstance(new Object[0]));
            mVar.a("initialize", new Class[]{Activity.class});
            mVar.a("create", new Class[0]);
            mVar.a("pause", new Class[0]);
            mVar.a("resume", new Class[0]);
            this.a = mVar;
            this.f = true;
            return true;
        } catch (Exception e) {
            this.b.reportError("Google AR Error", e.toString() + e.getLocalizedMessage());
            return false;
        }
    }

    private final native void tangoOnCreate(Activity activity);

    private final native void tangoOnServiceConnected(IBinder iBinder);

    private final native void tangoOnStop();

    final void a() {
        runOnUiThread(new Runnable() { // from class: com.unity3d.player.GoogleARProxy.2
            @Override // java.lang.Runnable
            public final void run() {
                try {
                    if (GoogleARProxy.this.a != null) {
                        GoogleARProxy.this.a.a("create", new Object[0]);
                    }
                } catch (Exception e) {
                    GoogleARProxy.this.reportError("Exception creating " + GoogleARProxy.this.e + " VR on UI Thread. " + e.getLocalizedMessage());
                }
            }
        });
    }

    final void a(final Activity activity, Context context) {
        if (a(UnityPlayer.class.getClassLoader())) {
            this.c = context;
            runOnUiThread(new Runnable() { // from class: com.unity3d.player.GoogleARProxy.1
                @Override // java.lang.Runnable
                public final void run() {
                    try {
                        if (GoogleARProxy.this.a != null) {
                            GoogleARProxy.this.a.a("initialize", activity);
                        }
                    } catch (Exception e) {
                        GoogleARProxy.this.reportError("Exception creating " + GoogleARProxy.this.e + " VR on UI Thread. " + e.getLocalizedMessage());
                    }
                }
            });
        }
    }

    final void b() {
        runOnUiThread(new Runnable() { // from class: com.unity3d.player.GoogleARProxy.3
            @Override // java.lang.Runnable
            public final void run() {
                try {
                    if (GoogleARProxy.this.a != null) {
                        GoogleARProxy.this.a.a("pause", new Object[0]);
                    }
                } catch (Exception e) {
                    GoogleARProxy.this.reportError("Exception pausing " + GoogleARProxy.this.e + " VR on UI Thread. " + e.getLocalizedMessage());
                }
            }
        });
    }

    final void c() {
        runOnUiThread(new Runnable() { // from class: com.unity3d.player.GoogleARProxy.4
            @Override // java.lang.Runnable
            public final void run() {
                try {
                    if (GoogleARProxy.this.a != null) {
                        GoogleARProxy.this.a.a("resume", new Object[0]);
                    }
                } catch (Exception e) {
                    GoogleARProxy.this.reportError("Exception resuming " + GoogleARProxy.this.e + " VR on UI Thread. " + e.getLocalizedMessage());
                }
            }
        });
    }

    public final boolean d() {
        return this.f;
    }
}
