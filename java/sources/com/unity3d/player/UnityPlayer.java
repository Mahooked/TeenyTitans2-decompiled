package com.unity3d.player;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.res.Configuration;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.os.Process;
import android.telephony.PhoneStateListener;
import android.telephony.TelephonyManager;
import android.view.InputEvent;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.Surface;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewParent;
import android.widget.FrameLayout;
import com.comscore.utils.Constants;
import java.util.concurrent.ConcurrentLinkedQueue;
import java.util.concurrent.Semaphore;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicInteger;

/* JADX INFO: loaded from: classes.dex */
public class UnityPlayer extends FrameLayout implements com.unity3d.player.d {
    public static Activity currentActivity = null;
    private static boolean o;
    c a;
    i b;
    private boolean c;
    private boolean d;
    private l e;
    private final ConcurrentLinkedQueue f;
    private BroadcastReceiver g;
    private boolean h;
    private a i;
    private TelephonyManager j;
    private j k;
    private GoogleARProxy l;
    private Context m;
    private SurfaceView n;
    private boolean p;
    private n q;
    private boolean r;

    /* JADX INFO: renamed from: com.unity3d.player.UnityPlayer$3, reason: invalid class name */
    class AnonymousClass3 extends BroadcastReceiver {
        final /* synthetic */ UnityPlayer a;

        @Override // android.content.BroadcastReceiver
        public void onReceive(Context context, Intent intent) {
            this.a.c();
        }
    }

    private class a extends PhoneStateListener {
        private a() {
        }

        /* synthetic */ a(UnityPlayer unityPlayer, byte b) {
            this();
        }

        @Override // android.telephony.PhoneStateListener
        public final void onCallStateChanged(int i, String str) {
            UnityPlayer.this.nativeMuteMasterAudio(i == 1);
        }
    }

    enum b {
        PAUSE,
        RESUME,
        QUIT,
        FOCUS_GAINED,
        FOCUS_LOST,
        NEXT_FRAME
    }

    private class c extends Thread {
        Handler a;
        boolean b;
        int c;

        private c() {
            this.b = false;
            this.c = 5;
        }

        /* synthetic */ c(UnityPlayer unityPlayer, byte b) {
            this();
        }

        private void a(b bVar) {
            Message.obtain(this.a, 2269, bVar).sendToTarget();
        }

        public final void a() {
            a(b.QUIT);
        }

        public final void a(boolean z) {
            a(z ? b.FOCUS_GAINED : b.FOCUS_LOST);
        }

        public final void b() {
            a(b.RESUME);
        }

        public final void c() {
            a(b.PAUSE);
        }

        @Override // java.lang.Thread, java.lang.Runnable
        public final void run() {
            setName("UnityMain");
            Looper.prepare();
            this.a = new Handler(new Handler.Callback() { // from class: com.unity3d.player.UnityPlayer.c.1
                @Override // android.os.Handler.Callback
                public final boolean handleMessage(Message message) {
                    if (message.what != 2269) {
                        return false;
                    }
                    b bVar = (b) message.obj;
                    if (bVar == b.QUIT) {
                        Looper.myLooper().quit();
                    } else if (bVar == b.RESUME) {
                        c.this.b = true;
                    } else if (bVar == b.PAUSE) {
                        c.this.b = false;
                        UnityPlayer.this.executeGLThreadJobs();
                    } else if (bVar == b.FOCUS_LOST) {
                        if (!c.this.b) {
                            UnityPlayer.this.executeGLThreadJobs();
                        }
                    } else if (bVar == b.NEXT_FRAME) {
                        if (c.this.c >= 0) {
                            if (c.this.c == 0 && UnityPlayer.this.i()) {
                                UnityPlayer.this.a();
                            }
                            c.this.c--;
                        }
                        UnityPlayer.this.executeGLThreadJobs();
                        if (!UnityPlayer.this.isFinishing() && !UnityPlayer.this.nativeRender()) {
                            UnityPlayer.this.c();
                        }
                    }
                    if (c.this.b) {
                        Message.obtain(c.this.a, 2269, b.NEXT_FRAME).sendToTarget();
                    }
                    return true;
                }
            });
            Looper.loop();
        }
    }

    private abstract class d implements Runnable {
        private d() {
        }

        /* synthetic */ d(UnityPlayer unityPlayer, byte b) {
            this();
        }

        public abstract void a();

        @Override // java.lang.Runnable
        public final void run() {
            if (UnityPlayer.this.isFinishing()) {
                return;
            }
            a();
        }
    }

    static {
        new k().a();
        o = false;
        o = loadLibraryStatic("main");
    }

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    public UnityPlayer(Context context) {
        super(context);
        byte b2 = 0;
        this.c = false;
        this.d = true;
        this.e = new l();
        this.f = new ConcurrentLinkedQueue();
        this.g = null;
        this.a = new c(this, b2);
        this.h = false;
        this.i = new a(this, b2);
        this.l = null;
        this.r = false;
        this.b = null;
        if (context instanceof Activity) {
            currentActivity = (Activity) context;
        }
        a(currentActivity);
        this.m = context;
        if (currentActivity != null && i()) {
            this.k = new j(this.m, j.a.a()[getSplashMode()]);
            addView(this.k);
        }
        if (h.c) {
            if (currentActivity != null) {
                h.d.a(currentActivity, new Runnable() { // from class: com.unity3d.player.UnityPlayer.1
                    @Override // java.lang.Runnable
                    public final void run() {
                        UnityPlayer.this.a(new Runnable() { // from class: com.unity3d.player.UnityPlayer.1.1
                            @Override // java.lang.Runnable
                            public final void run() {
                                UnityPlayer.this.e.d();
                                UnityPlayer.this.f();
                            }
                        });
                    }
                });
            } else {
                this.e.d();
            }
        }
        a(this.m.getApplicationInfo());
        if (!l.c()) {
            AlertDialog alertDialogCreate = new AlertDialog.Builder(this.m).setTitle("Failure to initialize!").setPositiveButton(Constants.RESPONSE_MASK, new DialogInterface.OnClickListener() { // from class: com.unity3d.player.UnityPlayer.13
                @Override // android.content.DialogInterface.OnClickListener
                public final void onClick(DialogInterface dialogInterface, int i) {
                    UnityPlayer.this.c();
                }
            }).setMessage("Your hardware does not support this application, sorry!").create();
            alertDialogCreate.setCancelable(false);
            alertDialogCreate.show();
            return;
        }
        initJni(context);
        this.n = b();
        addView(this.n);
        bringChildToFront(this.k);
        this.p = false;
        nativeInitWWW(WWW.class);
        nativeInitWebRequest(UnityWebRequest.class);
        k();
        this.j = (TelephonyManager) this.m.getSystemService("phone");
        this.a.start();
    }

    public static void UnitySendMessage(String str, String str2, String str3) {
        if (l.c()) {
            nativeUnitySendMessage(str, str2, str3);
        } else {
            e.Log(5, "Native libraries not loaded - dropping message for " + str + "." + str2);
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public void a() {
        a(new Runnable() { // from class: com.unity3d.player.UnityPlayer.16
            @Override // java.lang.Runnable
            public final void run() {
                UnityPlayer.this.removeView(UnityPlayer.this.k);
                UnityPlayer.this.k.a();
                UnityPlayer.h(UnityPlayer.this);
            }
        });
    }

    /* JADX INFO: Access modifiers changed from: private */
    public void a(int i, Surface surface) {
        if (this.c) {
            return;
        }
        b(0, surface);
    }

    private static void a(Activity activity) {
        View decorView;
        if (activity == null || !activity.getIntent().getBooleanExtra("android.intent.extra.VR_LAUNCH", false) || activity.getWindow() == null || (decorView = activity.getWindow().getDecorView()) == null) {
            return;
        }
        decorView.setSystemUiVisibility(7);
    }

    private static void a(ApplicationInfo applicationInfo) {
        if (o && NativeLoader.load(applicationInfo.nativeLibraryDir)) {
            l.a();
        }
    }

    private void a(View view, View view2) {
        boolean z;
        if (this.e.e()) {
            z = false;
        } else {
            pause();
            z = true;
        }
        if (view != null) {
            ViewParent parent = view.getParent();
            if (!(parent instanceof UnityPlayer) || ((UnityPlayer) parent) != this) {
                if (parent instanceof ViewGroup) {
                    ((ViewGroup) parent).removeView(view);
                }
                addView(view);
                bringChildToFront(view);
                view.setVisibility(0);
            }
        }
        if (view2 != null && view2.getParent() == this) {
            view2.setVisibility(8);
            removeView(view2);
        }
        if (z) {
            resume();
        }
    }

    private void a(d dVar) {
        if (isFinishing()) {
            return;
        }
        b(dVar);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public SurfaceView b() {
        SurfaceView surfaceView = new SurfaceView(this.m);
        surfaceView.getHolder().setFormat(2);
        surfaceView.getHolder().addCallback(new SurfaceHolder.Callback() { // from class: com.unity3d.player.UnityPlayer.17
            @Override // android.view.SurfaceHolder.Callback
            public final void surfaceChanged(SurfaceHolder surfaceHolder, int i, int i2, int i3) {
                UnityPlayer.this.a(0, surfaceHolder.getSurface());
            }

            @Override // android.view.SurfaceHolder.Callback
            public final void surfaceCreated(SurfaceHolder surfaceHolder) {
                UnityPlayer.this.a(0, surfaceHolder.getSurface());
            }

            @Override // android.view.SurfaceHolder.Callback
            public final void surfaceDestroyed(SurfaceHolder surfaceHolder) {
                UnityPlayer.this.a(0, (Surface) null);
            }
        });
        surfaceView.setFocusable(true);
        surfaceView.setFocusableInTouchMode(true);
        return surfaceView;
    }

    private void b(Runnable runnable) {
        if (l.c()) {
            if (Thread.currentThread() == this.a) {
                runnable.run();
            } else {
                this.f.add(runnable);
            }
        }
    }

    private boolean b(int i, Surface surface) {
        if (!l.c()) {
            return false;
        }
        nativeRecreateGfxState(i, surface);
        return true;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public void c() {
        if (!(this.m instanceof Activity) || ((Activity) this.m).isFinishing()) {
            return;
        }
        ((Activity) this.m).finish();
    }

    /* JADX INFO: Access modifiers changed from: private */
    public void d() {
        reportSoftInputStr(null, 1, true);
        if (this.e.g()) {
            if (l.c()) {
                final Semaphore semaphore = new Semaphore(0);
                if (isFinishing()) {
                    b(new Runnable() { // from class: com.unity3d.player.UnityPlayer.19
                        @Override // java.lang.Runnable
                        public final void run() {
                            UnityPlayer.this.e();
                            semaphore.release();
                        }
                    });
                } else {
                    b(new Runnable() { // from class: com.unity3d.player.UnityPlayer.20
                        @Override // java.lang.Runnable
                        public final void run() {
                            if (!UnityPlayer.this.nativePause()) {
                                semaphore.release();
                                return;
                            }
                            UnityPlayer.m(UnityPlayer.this);
                            UnityPlayer.this.e();
                            semaphore.release(2);
                        }
                    });
                }
                try {
                    if (!semaphore.tryAcquire(4L, TimeUnit.SECONDS)) {
                        e.Log(5, "Timeout while trying to pause the Unity Engine.");
                    }
                } catch (InterruptedException e) {
                    e.Log(5, "UI thread got interrupted while trying to pause the Unity Engine.");
                }
                if (semaphore.drainPermits() > 0) {
                    quit();
                }
            }
            this.e.c(false);
            this.e.b(true);
            if (this.h) {
                this.j.listen(this.i, 0);
            }
            this.a.c();
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public void e() {
        nativeDone();
    }

    /* JADX INFO: Access modifiers changed from: private */
    public void f() {
        if (this.e.f()) {
            this.e.c(true);
            b(new Runnable() { // from class: com.unity3d.player.UnityPlayer.2
                @Override // java.lang.Runnable
                public final void run() {
                    UnityPlayer.this.nativeResume();
                }
            });
            this.a.b();
        }
    }

    private static void g() {
        if (l.c()) {
            if (!NativeLoader.unload()) {
                throw new UnsatisfiedLinkError("Unable to unload libraries from libmain.so");
            }
            l.b();
        }
    }

    private ApplicationInfo h() {
        return this.m.getPackageManager().getApplicationInfo(this.m.getPackageName(), 128);
    }

    static /* synthetic */ j h(UnityPlayer unityPlayer) {
        unityPlayer.k = null;
        return null;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public boolean i() {
        try {
            return h().metaData.getBoolean("unity.splash-enable");
        } catch (Exception e) {
            return false;
        }
    }

    private final native void initJni(Context context);

    private boolean j() {
        try {
            return h().metaData.getBoolean("unity.tango-enable");
        } catch (Exception e) {
            return false;
        }
    }

    private void k() {
        if (this.m instanceof Activity) {
            ((Activity) this.m).getWindow().setFlags(1024, 1024);
        }
    }

    protected static boolean loadLibraryStatic(String str) {
        try {
            System.loadLibrary(str);
            return true;
        } catch (Exception e) {
            e.Log(6, "Unknown error " + e);
            return false;
        } catch (UnsatisfiedLinkError e2) {
            e.Log(6, "Unable to find " + str);
            return false;
        }
    }

    static /* synthetic */ boolean m(UnityPlayer unityPlayer) {
        unityPlayer.p = true;
        return true;
    }

    private final native void nativeDone();

    /* JADX INFO: Access modifiers changed from: private */
    public final native void nativeFocusChanged(boolean z);

    private final native void nativeInitWWW(Class cls);

    private final native void nativeInitWebRequest(Class cls);

    private final native boolean nativeInjectEvent(InputEvent inputEvent);

    /* JADX INFO: Access modifiers changed from: private */
    public final native void nativeLowMemory();

    /* JADX INFO: Access modifiers changed from: private */
    public final native void nativeMuteMasterAudio(boolean z);

    /* JADX INFO: Access modifiers changed from: private */
    public final native boolean nativePause();

    private final native void nativeRecreateGfxState(int i, Surface surface);

    /* JADX INFO: Access modifiers changed from: private */
    public final native boolean nativeRender();

    /* JADX INFO: Access modifiers changed from: private */
    public final native void nativeResume();

    /* JADX INFO: Access modifiers changed from: private */
    public final native void nativeSetInputString(String str);

    /* JADX INFO: Access modifiers changed from: private */
    public final native void nativeSoftInputCanceled();

    /* JADX INFO: Access modifiers changed from: private */
    public final native void nativeSoftInputClosed();

    private final native void nativeSoftInputLostFocus();

    private static native void nativeUnitySendMessage(String str, String str2, String str3);

    final void a(Runnable runnable) {
        if (this.m instanceof Activity) {
            ((Activity) this.m).runOnUiThread(runnable);
        } else {
            e.Log(5, "Not running Unity from an Activity; ignored...");
        }
    }

    protected void addPhoneCallListener() {
        this.h = true;
        this.j.listen(this.i, 32);
    }

    @Override // com.unity3d.player.d
    public boolean addViewToPlayer(View view, boolean z) {
        boolean z2 = true;
        a(view, z ? this.n : null);
        boolean z3 = view.getParent() == this;
        boolean z4 = z && this.n.getParent() == null;
        boolean z5 = this.n.getParent() == this;
        if (!z3 || (!z4 && !z5)) {
            z2 = false;
        }
        if (!z2) {
            if (!z3) {
                e.Log(6, "addViewToPlayer: Failure adding view to hierarchy");
            }
            if (!z4 && !z5) {
                e.Log(6, "addViewToPlayer: Failure removing old view from hierarchy");
            }
        }
        return z2;
    }

    public void configurationChanged(Configuration configuration) {
        if (this.n instanceof SurfaceView) {
            this.n.getHolder().setSizeFromLayout();
        }
        if (this.q != null) {
            this.q.updateVideoLayout();
        }
        GoogleVrProxy googleVrProxyB = GoogleVrApi.b();
        if (googleVrProxyB != null) {
            googleVrProxyB.d();
        }
    }

    protected void disableLogger() {
        e.a = true;
    }

    public boolean displayChanged(int i, Surface surface) {
        if (i == 0) {
            this.c = surface != null;
            a(new Runnable() { // from class: com.unity3d.player.UnityPlayer.18
                @Override // java.lang.Runnable
                public final void run() {
                    if (UnityPlayer.this.c) {
                        UnityPlayer.this.removeView(UnityPlayer.this.n);
                    } else {
                        UnityPlayer.this.addView(UnityPlayer.this.n);
                    }
                }
            });
        }
        return b(i, surface);
    }

    protected void executeGLThreadJobs() {
        while (true) {
            Runnable runnable = (Runnable) this.f.poll();
            if (runnable == null) {
                return;
            } else {
                runnable.run();
            }
        }
    }

    public Bundle getSettings() {
        return Bundle.EMPTY;
    }

    protected int getSplashMode() {
        try {
            return h().metaData.getInt("unity.splash-mode");
        } catch (Exception e) {
            return 0;
        }
    }

    public View getView() {
        return this;
    }

    protected void hideSoftInput() {
        final Runnable runnable = new Runnable() { // from class: com.unity3d.player.UnityPlayer.6
            @Override // java.lang.Runnable
            public final void run() {
                if (UnityPlayer.this.b != null) {
                    UnityPlayer.this.b.dismiss();
                    UnityPlayer.this.b = null;
                }
            }
        };
        if (h.b) {
            a(new d() { // from class: com.unity3d.player.UnityPlayer.7
                /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
                {
                    super(UnityPlayer.this, (byte) 0);
                }

                @Override // com.unity3d.player.UnityPlayer.d
                public final void a() {
                    UnityPlayer.this.a(runnable);
                }
            });
        } else {
            a(runnable);
        }
    }

    public void init(int i, boolean z) {
    }

    protected boolean initializeGoogleAr() {
        if (this.l != null || currentActivity == null || !j()) {
            return false;
        }
        this.l = new GoogleARProxy(this);
        this.l.a(currentActivity, this.m);
        this.l.a();
        if (!this.e.e()) {
            this.l.c();
        }
        return this.l.d();
    }

    protected boolean initializeGoogleVr() {
        final GoogleVrProxy googleVrProxyB = GoogleVrApi.b();
        if (googleVrProxyB == null) {
            GoogleVrApi.a(this);
            googleVrProxyB = GoogleVrApi.b();
            if (googleVrProxyB == null) {
                e.Log(6, "Unable to create Google VR subsystem.");
                return false;
            }
        }
        final Semaphore semaphore = new Semaphore(0);
        final Runnable runnable = new Runnable() { // from class: com.unity3d.player.UnityPlayer.10
            @Override // java.lang.Runnable
            public final void run() {
                UnityPlayer.this.injectEvent(new KeyEvent(0, 4));
                UnityPlayer.this.injectEvent(new KeyEvent(1, 4));
            }
        };
        a(new Runnable() { // from class: com.unity3d.player.UnityPlayer.11
            @Override // java.lang.Runnable
            public final void run() {
                if (!googleVrProxyB.a(UnityPlayer.currentActivity, UnityPlayer.this.m, UnityPlayer.this.b(), runnable)) {
                    e.Log(6, "Unable to initialize Google VR subsystem.");
                }
                if (UnityPlayer.currentActivity != null) {
                    googleVrProxyB.a(UnityPlayer.currentActivity.getIntent());
                }
                semaphore.release();
            }
        });
        try {
            if (semaphore.tryAcquire(4L, TimeUnit.SECONDS)) {
                return googleVrProxyB.a();
            }
            e.Log(5, "Timeout while trying to initialize Google VR.");
            return false;
        } catch (InterruptedException e) {
            e.Log(5, "UI thread was interrupted while initializing Google VR. " + e.getLocalizedMessage());
            return false;
        }
    }

    public boolean injectEvent(InputEvent inputEvent) {
        return nativeInjectEvent(inputEvent);
    }

    protected boolean isFinishing() {
        if (!this.p) {
            boolean z = (this.m instanceof Activity) && ((Activity) this.m).isFinishing();
            this.p = z;
            if (!z) {
                return false;
            }
        }
        return true;
    }

    protected void kill() {
        Process.killProcess(Process.myPid());
    }

    protected boolean loadLibrary(String str) {
        return loadLibraryStatic(str);
    }

    public void lowMemory() {
        b(new Runnable() { // from class: com.unity3d.player.UnityPlayer.21
            @Override // java.lang.Runnable
            public final void run() {
                UnityPlayer.this.nativeLowMemory();
            }
        });
    }

    @Override // android.view.View
    public boolean onGenericMotionEvent(MotionEvent motionEvent) {
        return injectEvent(motionEvent);
    }

    @Override // android.view.View, android.view.KeyEvent.Callback
    public boolean onKeyDown(int i, KeyEvent keyEvent) {
        return injectEvent(keyEvent);
    }

    @Override // android.view.View, android.view.KeyEvent.Callback
    public boolean onKeyLongPress(int i, KeyEvent keyEvent) {
        return injectEvent(keyEvent);
    }

    @Override // android.view.View, android.view.KeyEvent.Callback
    public boolean onKeyMultiple(int i, int i2, KeyEvent keyEvent) {
        return injectEvent(keyEvent);
    }

    @Override // android.view.View, android.view.KeyEvent.Callback
    public boolean onKeyUp(int i, KeyEvent keyEvent) {
        return injectEvent(keyEvent);
    }

    @Override // android.view.View
    public boolean onTouchEvent(MotionEvent motionEvent) {
        return injectEvent(motionEvent);
    }

    public void pause() {
        if (this.q == null) {
            if (this.l != null) {
                this.l.b();
            }
            d();
        } else {
            this.r = this.q.a();
            if (this.r) {
                return;
            }
            this.q.pause();
        }
    }

    public void quit() {
        GoogleVrProxy googleVrProxyB = GoogleVrApi.b();
        if (googleVrProxyB != null) {
            GoogleVrApi.a();
            googleVrProxyB.b();
        }
        this.p = true;
        if (!this.e.e()) {
            pause();
        }
        this.a.a();
        try {
            this.a.join(4000L);
        } catch (InterruptedException e) {
            this.a.interrupt();
        }
        if (this.g != null) {
            this.m.unregisterReceiver(this.g);
        }
        this.g = null;
        if (l.c()) {
            removeAllViews();
        }
        kill();
        g();
    }

    @Override // com.unity3d.player.d
    public void removeViewFromPlayer(View view) {
        a(this.n, view);
        boolean z = view.getParent() == null;
        boolean z2 = this.n.getParent() == this;
        if (z && z2) {
            return;
        }
        if (!z) {
            e.Log(6, "removeViewFromPlayer: Failure removing view from hierarchy");
        }
        if (z2) {
            return;
        }
        e.Log(6, "removeVireFromPlayer: Failure agging old view to hierarchy");
    }

    @Override // com.unity3d.player.d
    public void reportError(String str, String str2) {
        e.Log(6, str + ": " + str2);
    }

    protected void reportSoftInputStr(final String str, final int i, final boolean z) {
        if (i == 1) {
            hideSoftInput();
        }
        a(new d() { // from class: com.unity3d.player.UnityPlayer.9
            /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
            {
                super(UnityPlayer.this, (byte) 0);
            }

            @Override // com.unity3d.player.UnityPlayer.d
            public final void a() {
                if (z) {
                    UnityPlayer.this.nativeSoftInputCanceled();
                } else if (str != null) {
                    UnityPlayer.this.nativeSetInputString(str);
                }
                if (i == 1) {
                    UnityPlayer.this.nativeSoftInputClosed();
                }
            }
        });
    }

    public void resume() {
        this.e.b(false);
        if (this.q != null) {
            if (this.r) {
                return;
            }
            this.q.start();
            return;
        }
        f();
        GoogleVrProxy googleVrProxyB = GoogleVrApi.b();
        if (googleVrProxyB != null) {
            googleVrProxyB.c();
        }
        if (this.l != null) {
            this.l.c();
        }
    }

    protected void setSoftInputStr(final String str) {
        a(new Runnable() { // from class: com.unity3d.player.UnityPlayer.8
            @Override // java.lang.Runnable
            public final void run() {
                if (UnityPlayer.this.b == null || str == null) {
                    return;
                }
                UnityPlayer.this.b.a(str);
            }
        });
    }

    protected void showSoftInput(final String str, final int i, final boolean z, final boolean z2, final boolean z3, final boolean z4, final String str2) {
        a(new Runnable() { // from class: com.unity3d.player.UnityPlayer.5
            @Override // java.lang.Runnable
            public final void run() {
                UnityPlayer.this.b = new i(UnityPlayer.this.m, this, str, i, z, z2, z3, str2);
                UnityPlayer.this.b.show();
            }
        });
    }

    protected boolean showVideoPlayer(final String str, final int i, final int i2, final int i3, final boolean z, final int i4, final int i5) {
        final Semaphore semaphore = new Semaphore(0);
        final AtomicInteger atomicInteger = new AtomicInteger(-1);
        a(new Runnable() { // from class: com.unity3d.player.UnityPlayer.12
            @Override // java.lang.Runnable
            public final void run() {
                if (UnityPlayer.this.q != null) {
                    e.Log(5, "Video already playing");
                    atomicInteger.set(2);
                    semaphore.release();
                } else {
                    UnityPlayer.this.q = new n(UnityPlayer.this.m, str, i, i2, i3, z, i4, i5, new n.a() { // from class: com.unity3d.player.UnityPlayer.12.1
                        @Override // com.unity3d.player.n.a
                        public final void a(int i6) {
                            atomicInteger.set(i6);
                            if (i6 == 3) {
                                if (UnityPlayer.this.n.getParent() == null) {
                                    UnityPlayer.this.addView(UnityPlayer.this.n);
                                }
                                if (UnityPlayer.this.q != null) {
                                    UnityPlayer.this.q.destroyPlayer();
                                    UnityPlayer.this.removeView(UnityPlayer.this.q);
                                    UnityPlayer.this.q = null;
                                }
                                UnityPlayer.this.resume();
                            }
                            if (i6 != 0) {
                                semaphore.release();
                            }
                        }
                    });
                    UnityPlayer.this.addView(UnityPlayer.this.q);
                }
            }
        });
        boolean z2 = false;
        try {
            semaphore.acquire();
            z2 = atomicInteger.get() != 2;
        } catch (InterruptedException e) {
        }
        if (z2) {
            if (this.q != null) {
                a(new Runnable() { // from class: com.unity3d.player.UnityPlayer.14
                    @Override // java.lang.Runnable
                    public final void run() {
                        if (UnityPlayer.this.q != null) {
                            UnityPlayer.this.d();
                            UnityPlayer.this.q.requestFocus();
                            UnityPlayer.this.removeView(UnityPlayer.this.n);
                        }
                    }
                });
            }
        } else if (this.q != null) {
            a(new Runnable() { // from class: com.unity3d.player.UnityPlayer.15
                @Override // java.lang.Runnable
                public final void run() {
                    if (UnityPlayer.this.n.getParent() == null) {
                        UnityPlayer.this.addView(UnityPlayer.this.n);
                    }
                    if (UnityPlayer.this.q != null) {
                        UnityPlayer.this.q.destroyPlayer();
                        UnityPlayer.this.removeView(UnityPlayer.this.q);
                        UnityPlayer.this.q = null;
                    }
                    UnityPlayer.this.resume();
                }
            });
        }
        return z2;
    }

    public void start() {
    }

    public void stop() {
    }

    public void windowFocusChanged(final boolean z) {
        this.e.a(z);
        if (z && this.b != null) {
            nativeSoftInputLostFocus();
            reportSoftInputStr(null, 1, false);
        }
        b(new Runnable() { // from class: com.unity3d.player.UnityPlayer.4
            @Override // java.lang.Runnable
            public final void run() {
                UnityPlayer.this.nativeFocusChanged(z);
            }
        });
        this.a.a(z);
        f();
    }
}
