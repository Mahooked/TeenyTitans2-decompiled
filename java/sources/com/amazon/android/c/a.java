package com.amazon.android.c;

import android.app.Application;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.util.KiwiLogger;
import com.amazon.android.m.g;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Random;

/* JADX INFO: loaded from: classes.dex */
public final class a implements d, com.amazon.android.framework.resource.b, Thread.UncaughtExceptionHandler {
    private static final KiwiLogger a = new KiwiLogger("CrashManagerImpl");

    @Resource
    private g b;

    @Resource
    private Application c;
    private Thread.UncaughtExceptionHandler d;
    private Map e = new HashMap();

    private synchronized void a(String str) {
        FileOutputStream fileOutputStream = null;
        try {
            try {
                FileOutputStream fileOutputStreamOpenFileOutput = this.c.openFileOutput("s-" + new Random().nextInt(99999) + ".amzst", 0);
                try {
                    fileOutputStreamOpenFileOutput.write(str.getBytes());
                    com.amazon.android.framework.util.a.a(fileOutputStreamOpenFileOutput);
                } catch (Exception e) {
                    fileOutputStream = fileOutputStreamOpenFileOutput;
                    e = e;
                    if (KiwiLogger.ERROR_ON) {
                        a.error("Coud not save crash report to file", e);
                    }
                    com.amazon.android.framework.util.a.a(fileOutputStream);
                } catch (Throwable th) {
                    fileOutputStream = fileOutputStreamOpenFileOutput;
                    th = th;
                    com.amazon.android.framework.util.a.a(fileOutputStream);
                    throw th;
                }
            } catch (Exception e2) {
                e = e2;
            }
        } catch (Throwable th2) {
            th = th2;
        }
    }

    private static b b(String str) {
        try {
            return (b) com.amazon.android.t.a.a(c(str));
        } catch (Exception e) {
            if (KiwiLogger.ERROR_ON) {
                a.error("Failed to load crash report: " + str);
            }
            return null;
        }
    }

    private boolean b() {
        return this.e.size() >= 5;
    }

    private static String c(String str) throws Throwable {
        StringBuilder sb = new StringBuilder();
        BufferedReader bufferedReader = null;
        try {
            BufferedReader bufferedReader2 = new BufferedReader(new FileReader(str));
            while (bufferedReader2.ready()) {
                try {
                    sb.append(bufferedReader2.readLine());
                } catch (Throwable th) {
                    th = th;
                    bufferedReader = bufferedReader2;
                    com.amazon.android.framework.util.a.a(bufferedReader);
                    throw th;
                }
            }
            com.amazon.android.framework.util.a.a(bufferedReader2);
            return sb.toString();
        } catch (Throwable th2) {
            th = th2;
        }
    }

    private static void d(String str) {
        try {
            new File(str).delete();
        } catch (Exception e) {
            if (KiwiLogger.ERROR_ON) {
                a.error("Cannot delete file: " + str, e);
            }
        }
    }

    @Override // com.amazon.android.c.d
    public final List a() {
        if (b()) {
            return Collections.emptyList();
        }
        ArrayList arrayList = new ArrayList();
        String[] list = new File(this.c.getFilesDir().getAbsolutePath() + "/").list(new c(this));
        for (int i = 0; i < list.length && !b(); i++) {
            String str = this.c.getFilesDir().getAbsolutePath() + "/" + list[i];
            b bVarB = b(str);
            if (bVarB != null) {
                this.e.put(bVarB, str);
                arrayList.add(bVarB);
            } else {
                d(str);
            }
        }
        return arrayList;
    }

    @Override // com.amazon.android.c.d
    public final void a(List list) {
        Iterator it = list.iterator();
        while (it.hasNext()) {
            b bVar = (b) it.next();
            d((String) this.e.get(bVar));
            this.e.remove(bVar);
        }
    }

    @Override // com.amazon.android.framework.resource.b
    public final void onResourcesPopulated() {
        com.amazon.android.d.a.a();
        if (Thread.getDefaultUncaughtExceptionHandler() instanceof d) {
            return;
        }
        if (KiwiLogger.TRACE_ON) {
            a.trace("Registering Crash Handler");
        }
        this.d = Thread.getDefaultUncaughtExceptionHandler();
        Thread.setDefaultUncaughtExceptionHandler(this);
    }

    @Override // java.lang.Thread.UncaughtExceptionHandler
    public final void uncaughtException(Thread thread, Throwable th) {
        if (KiwiLogger.TRACE_ON) {
            a.trace("Crash detected", th);
        }
        try {
            try {
                a(com.amazon.android.t.a.a(new b(this.c, th)));
            } catch (Throwable th2) {
                if (KiwiLogger.ERROR_ON) {
                    a.error("Could not handle uncaught exception", th2);
                }
            }
            this.b.a(new com.amazon.android.a.b());
        } catch (Throwable th3) {
            if (KiwiLogger.TRACE_ON) {
                a.trace("Error occured while handling exception", th3);
            }
        }
        if (KiwiLogger.TRACE_ON) {
            a.trace("Calling previous handler");
        }
        if (this.d != null) {
            this.d.uncaughtException(thread, th);
        }
    }
}
