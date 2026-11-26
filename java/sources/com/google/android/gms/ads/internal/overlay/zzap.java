package com.google.android.gms.ads.internal.overlay;

import android.annotation.TargetApi;
import android.content.Context;
import android.graphics.SurfaceTexture;
import android.opengl.GLES20;
import android.opengl.GLUtils;
import android.util.Log;
import com.google.android.gms.ads.internal.zzbs;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzme;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zzzn;
import java.nio.Buffer;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.FloatBuffer;
import java.util.concurrent.CountDownLatch;
import javax.microedition.khronos.egl.EGL10;
import javax.microedition.khronos.egl.EGLConfig;
import javax.microedition.khronos.egl.EGLContext;
import javax.microedition.khronos.egl.EGLDisplay;
import javax.microedition.khronos.egl.EGLSurface;

/* JADX INFO: loaded from: classes.dex */
@zzzn
@TargetApi(14)
public final class zzap extends Thread implements SurfaceTexture.OnFrameAvailableListener, zzao {
    private static final float[] zzPU = {-1.0f, -1.0f, -1.0f, 1.0f, -1.0f, -1.0f, -1.0f, 1.0f, -1.0f, 1.0f, 1.0f, -1.0f};
    private final float[] zzPR;
    private final zzam zzPV;
    private final float[] zzPW;
    private final float[] zzPX;
    private final float[] zzPY;
    private final float[] zzPZ;
    private final float[] zzQa;
    private final float[] zzQb;
    private float zzQc;
    private float zzQd;
    private float zzQe;
    private SurfaceTexture zzQf;
    private SurfaceTexture zzQg;
    private int zzQh;
    private int zzQi;
    private int zzQj;
    private FloatBuffer zzQk;
    private final CountDownLatch zzQl;
    private final Object zzQm;
    private EGL10 zzQn;
    private EGLDisplay zzQo;
    private EGLContext zzQp;
    private EGLSurface zzQq;
    private volatile boolean zzQr;
    private volatile boolean zzQs;
    private int zzrW;
    private int zzrX;

    public zzap(Context context) {
        super("SphericalVideoProcessor");
        this.zzQk = ByteBuffer.allocateDirect(zzPU.length << 2).order(ByteOrder.nativeOrder()).asFloatBuffer();
        this.zzQk.put(zzPU).position(0);
        this.zzPR = new float[9];
        this.zzPW = new float[9];
        this.zzPX = new float[9];
        this.zzPY = new float[9];
        this.zzPZ = new float[9];
        this.zzQa = new float[9];
        this.zzQb = new float[9];
        this.zzQc = Float.NaN;
        this.zzPV = new zzam(context);
        this.zzPV.zza(this);
        this.zzQl = new CountDownLatch(1);
        this.zzQm = new Object();
    }

    private static void zza(float[] fArr, float f) {
        fArr[0] = 1.0f;
        fArr[1] = 0.0f;
        fArr[2] = 0.0f;
        fArr[3] = 0.0f;
        fArr[4] = (float) Math.cos(f);
        fArr[5] = (float) (-Math.sin(f));
        fArr[6] = 0.0f;
        fArr[7] = (float) Math.sin(f);
        fArr[8] = (float) Math.cos(f);
    }

    private static void zza(float[] fArr, float[] fArr2, float[] fArr3) {
        fArr[0] = (fArr2[0] * fArr3[0]) + (fArr2[1] * fArr3[3]) + (fArr2[2] * fArr3[6]);
        fArr[1] = (fArr2[0] * fArr3[1]) + (fArr2[1] * fArr3[4]) + (fArr2[2] * fArr3[7]);
        fArr[2] = (fArr2[0] * fArr3[2]) + (fArr2[1] * fArr3[5]) + (fArr2[2] * fArr3[8]);
        fArr[3] = (fArr2[3] * fArr3[0]) + (fArr2[4] * fArr3[3]) + (fArr2[5] * fArr3[6]);
        fArr[4] = (fArr2[3] * fArr3[1]) + (fArr2[4] * fArr3[4]) + (fArr2[5] * fArr3[7]);
        fArr[5] = (fArr2[3] * fArr3[2]) + (fArr2[4] * fArr3[5]) + (fArr2[5] * fArr3[8]);
        fArr[6] = (fArr2[6] * fArr3[0]) + (fArr2[7] * fArr3[3]) + (fArr2[8] * fArr3[6]);
        fArr[7] = (fArr2[6] * fArr3[1]) + (fArr2[7] * fArr3[4]) + (fArr2[8] * fArr3[7]);
        fArr[8] = (fArr2[6] * fArr3[2]) + (fArr2[7] * fArr3[5]) + (fArr2[8] * fArr3[8]);
    }

    private static void zzar(String str) {
        int iGlGetError = GLES20.glGetError();
        if (iGlGetError != 0) {
            Log.e("SphericalVideoRenderer", new StringBuilder(String.valueOf(str).length() + 21).append(str).append(": glError ").append(iGlGetError).toString());
        }
    }

    private static void zzb(float[] fArr, float f) {
        fArr[0] = (float) Math.cos(f);
        fArr[1] = (float) (-Math.sin(f));
        fArr[2] = 0.0f;
        fArr[3] = (float) Math.sin(f);
        fArr[4] = (float) Math.cos(f);
        fArr[5] = 0.0f;
        fArr[6] = 0.0f;
        fArr[7] = 0.0f;
        fArr[8] = 1.0f;
    }

    private static int zzc(int i, String str) {
        int iGlCreateShader = GLES20.glCreateShader(i);
        zzar("createShader");
        if (iGlCreateShader != 0) {
            GLES20.glShaderSource(iGlCreateShader, str);
            zzar("shaderSource");
            GLES20.glCompileShader(iGlCreateShader);
            zzar("compileShader");
            int[] iArr = new int[1];
            GLES20.glGetShaderiv(iGlCreateShader, 35713, iArr, 0);
            zzar("getShaderiv");
            if (iArr[0] == 0) {
                Log.e("SphericalVideoRenderer", new StringBuilder(37).append("Could not compile shader ").append(i).append(":").toString());
                Log.e("SphericalVideoRenderer", GLES20.glGetShaderInfoLog(iGlCreateShader));
                GLES20.glDeleteShader(iGlCreateShader);
                zzar("deleteShader");
                return 0;
            }
        }
        return iGlCreateShader;
    }

    private final void zzgh() {
        while (this.zzQj > 0) {
            this.zzQf.updateTexImage();
            this.zzQj--;
        }
        if (this.zzPV.zza(this.zzPR)) {
            if (Float.isNaN(this.zzQc)) {
                float[] fArr = this.zzPR;
                float[] fArr2 = {0.0f, 1.0f, 0.0f};
                float[] fArr3 = {(fArr[0] * fArr2[0]) + (fArr[1] * fArr2[1]) + (fArr[2] * fArr2[2]), (fArr[3] * fArr2[0]) + (fArr[4] * fArr2[1]) + (fArr[5] * fArr2[2]), (fArr[8] * fArr2[2]) + (fArr[6] * fArr2[0]) + (fArr[7] * fArr2[1])};
                this.zzQc = -(((float) Math.atan2(fArr3[1], fArr3[0])) - 1.5707964f);
            }
            zzb(this.zzQa, this.zzQc + this.zzQd);
        } else {
            zza(this.zzPR, -1.5707964f);
            zzb(this.zzQa, this.zzQd);
        }
        zza(this.zzPW, 1.5707964f);
        zza(this.zzPX, this.zzQa, this.zzPW);
        zza(this.zzPY, this.zzPR, this.zzPX);
        zza(this.zzPZ, this.zzQe);
        zza(this.zzQb, this.zzPZ, this.zzPY);
        GLES20.glUniformMatrix3fv(this.zzQi, 1, false, this.zzQb, 0);
        GLES20.glDrawArrays(5, 0, 4);
        zzar("drawArrays");
        GLES20.glFinish();
        this.zzQn.eglSwapBuffers(this.zzQo, this.zzQq);
    }

    private final boolean zzgi() {
        boolean zEglDestroyContext = false;
        if (this.zzQq != null && this.zzQq != EGL10.EGL_NO_SURFACE) {
            zEglDestroyContext = this.zzQn.eglMakeCurrent(this.zzQo, EGL10.EGL_NO_SURFACE, EGL10.EGL_NO_SURFACE, EGL10.EGL_NO_CONTEXT) | false | this.zzQn.eglDestroySurface(this.zzQo, this.zzQq);
            this.zzQq = null;
        }
        if (this.zzQp != null) {
            zEglDestroyContext |= this.zzQn.eglDestroyContext(this.zzQo, this.zzQp);
            this.zzQp = null;
        }
        if (this.zzQo == null) {
            return zEglDestroyContext;
        }
        boolean zEglTerminate = zEglDestroyContext | this.zzQn.eglTerminate(this.zzQo);
        this.zzQo = null;
        return zEglTerminate;
    }

    @Override // android.graphics.SurfaceTexture.OnFrameAvailableListener
    public final void onFrameAvailable(SurfaceTexture surfaceTexture) {
        this.zzQj++;
        synchronized (this.zzQm) {
            this.zzQm.notifyAll();
        }
    }

    @Override // java.lang.Thread, java.lang.Runnable
    public final void run() {
        boolean z;
        int iGlCreateProgram;
        if (this.zzQg == null) {
            zzafr.e("SphericalVideoProcessor started with no output texture.");
            this.zzQl.countDown();
            return;
        }
        this.zzQn = (EGL10) EGLContext.getEGL();
        this.zzQo = this.zzQn.eglGetDisplay(EGL10.EGL_DEFAULT_DISPLAY);
        if (this.zzQo == EGL10.EGL_NO_DISPLAY) {
            z = false;
        } else {
            if (this.zzQn.eglInitialize(this.zzQo, new int[2])) {
                int[] iArr = new int[1];
                EGLConfig[] eGLConfigArr = new EGLConfig[1];
                EGLConfig eGLConfig = (!this.zzQn.eglChooseConfig(this.zzQo, new int[]{12352, 4, 12324, 8, 12323, 8, 12322, 8, 12325, 16, 12344}, eGLConfigArr, 1, iArr) || iArr[0] <= 0) ? null : eGLConfigArr[0];
                if (eGLConfig == null) {
                    z = false;
                } else {
                    this.zzQp = this.zzQn.eglCreateContext(this.zzQo, eGLConfig, EGL10.EGL_NO_CONTEXT, new int[]{12440, 2, 12344});
                    if (this.zzQp == null || this.zzQp == EGL10.EGL_NO_CONTEXT) {
                        z = false;
                    } else {
                        this.zzQq = this.zzQn.eglCreateWindowSurface(this.zzQo, eGLConfig, this.zzQg, null);
                        z = (this.zzQq == null || this.zzQq == EGL10.EGL_NO_SURFACE || !this.zzQn.eglMakeCurrent(this.zzQo, this.zzQq, this.zzQq, this.zzQp)) ? false : true;
                    }
                }
            } else {
                z = false;
            }
        }
        zzme<String> zzmeVar = zzmo.zzEn;
        int iZzc = zzc(35633, !((String) zzbs.zzbL().zzd(zzmeVar)).equals(zzmeVar.zzdI()) ? (String) zzbs.zzbL().zzd(zzmeVar) : "attribute highp vec3 aPosition;varying vec3 pos;void main() {  gl_Position = vec4(aPosition, 1.0);  pos = aPosition;}");
        if (iZzc == 0) {
            iGlCreateProgram = 0;
        } else {
            zzme<String> zzmeVar2 = zzmo.zzEo;
            int iZzc2 = zzc(35632, !((String) zzbs.zzbL().zzd(zzmeVar2)).equals(zzmeVar2.zzdI()) ? (String) zzbs.zzbL().zzd(zzmeVar2) : "#extension GL_OES_EGL_image_external : require\n#define INV_PI 0.3183\nprecision highp float;varying vec3 pos;uniform samplerExternalOES uSplr;uniform mat3 uVMat;uniform float uFOVx;uniform float uFOVy;void main() {  vec3 ray = vec3(pos.x * tan(uFOVx), pos.y * tan(uFOVy), -1);  ray = (uVMat * ray).xyz;  ray = normalize(ray);  vec2 texCrd = vec2(    0.5 + atan(ray.x, - ray.z) * INV_PI * 0.5, acos(ray.y) * INV_PI);  gl_FragColor = vec4(texture2D(uSplr, texCrd).xyz, 1.0);}");
            if (iZzc2 == 0) {
                iGlCreateProgram = 0;
            } else {
                iGlCreateProgram = GLES20.glCreateProgram();
                zzar("createProgram");
                if (iGlCreateProgram != 0) {
                    GLES20.glAttachShader(iGlCreateProgram, iZzc);
                    zzar("attachShader");
                    GLES20.glAttachShader(iGlCreateProgram, iZzc2);
                    zzar("attachShader");
                    GLES20.glLinkProgram(iGlCreateProgram);
                    zzar("linkProgram");
                    int[] iArr2 = new int[1];
                    GLES20.glGetProgramiv(iGlCreateProgram, 35714, iArr2, 0);
                    zzar("getProgramiv");
                    if (iArr2[0] != 1) {
                        Log.e("SphericalVideoRenderer", "Could not link program: ");
                        Log.e("SphericalVideoRenderer", GLES20.glGetProgramInfoLog(iGlCreateProgram));
                        GLES20.glDeleteProgram(iGlCreateProgram);
                        zzar("deleteProgram");
                        iGlCreateProgram = 0;
                    } else {
                        GLES20.glValidateProgram(iGlCreateProgram);
                        zzar("validateProgram");
                    }
                }
            }
        }
        this.zzQh = iGlCreateProgram;
        GLES20.glUseProgram(this.zzQh);
        zzar("useProgram");
        int iGlGetAttribLocation = GLES20.glGetAttribLocation(this.zzQh, "aPosition");
        GLES20.glVertexAttribPointer(iGlGetAttribLocation, 3, 5126, false, 12, (Buffer) this.zzQk);
        zzar("vertexAttribPointer");
        GLES20.glEnableVertexAttribArray(iGlGetAttribLocation);
        zzar("enableVertexAttribArray");
        int[] iArr3 = new int[1];
        GLES20.glGenTextures(1, iArr3, 0);
        zzar("genTextures");
        int i = iArr3[0];
        GLES20.glBindTexture(36197, i);
        zzar("bindTextures");
        GLES20.glTexParameteri(36197, 10240, 9729);
        zzar("texParameteri");
        GLES20.glTexParameteri(36197, 10241, 9729);
        zzar("texParameteri");
        GLES20.glTexParameteri(36197, 10242, 33071);
        zzar("texParameteri");
        GLES20.glTexParameteri(36197, 10243, 33071);
        zzar("texParameteri");
        this.zzQi = GLES20.glGetUniformLocation(this.zzQh, "uVMat");
        GLES20.glUniformMatrix3fv(this.zzQi, 1, false, new float[]{1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f}, 0);
        boolean z2 = this.zzQh != 0;
        if (!z || !z2) {
            String strValueOf = String.valueOf(GLUtils.getEGLErrorString(this.zzQn.eglGetError()));
            String strConcat = strValueOf.length() != 0 ? "EGL initialization failed: ".concat(strValueOf) : new String("EGL initialization failed: ");
            zzafr.e(strConcat);
            zzbs.zzbD().zza(new Throwable(strConcat), "SphericalVideoProcessor.run.1");
            zzgi();
            this.zzQl.countDown();
            return;
        }
        this.zzQf = new SurfaceTexture(i);
        this.zzQf.setOnFrameAvailableListener(this);
        this.zzQl.countDown();
        this.zzPV.start();
        try {
            try {
                this.zzQr = true;
                while (!this.zzQs) {
                    zzgh();
                    if (this.zzQr) {
                        GLES20.glViewport(0, 0, this.zzrW, this.zzrX);
                        zzar("viewport");
                        int iGlGetUniformLocation = GLES20.glGetUniformLocation(this.zzQh, "uFOVx");
                        int iGlGetUniformLocation2 = GLES20.glGetUniformLocation(this.zzQh, "uFOVy");
                        if (this.zzrW > this.zzrX) {
                            GLES20.glUniform1f(iGlGetUniformLocation, 0.87266463f);
                            GLES20.glUniform1f(iGlGetUniformLocation2, (this.zzrX * 0.87266463f) / this.zzrW);
                        } else {
                            GLES20.glUniform1f(iGlGetUniformLocation, (this.zzrW * 0.87266463f) / this.zzrX);
                            GLES20.glUniform1f(iGlGetUniformLocation2, 0.87266463f);
                        }
                        this.zzQr = false;
                    }
                    try {
                        synchronized (this.zzQm) {
                            try {
                                if (!this.zzQs && !this.zzQr && this.zzQj == 0) {
                                    this.zzQm.wait();
                                }
                            } catch (Throwable th) {
                                throw th;
                            }
                        }
                    } catch (InterruptedException e) {
                    }
                }
                this.zzPV.stop();
                this.zzQf.setOnFrameAvailableListener(null);
                this.zzQf = null;
                zzgi();
            } catch (Throwable th2) {
                this.zzPV.stop();
                this.zzQf.setOnFrameAvailableListener(null);
                this.zzQf = null;
                zzgi();
                throw th2;
            }
        } catch (IllegalStateException e2) {
            zzafr.zzaT("SphericalVideoProcessor halted unexpectedly.");
            this.zzPV.stop();
            this.zzQf.setOnFrameAvailableListener(null);
            this.zzQf = null;
            zzgi();
        } catch (Throwable th3) {
            zzafr.zzb("SphericalVideoProcessor died.", th3);
            zzbs.zzbD().zza(th3, "SphericalVideoProcessor.run.2");
            this.zzPV.stop();
            this.zzQf.setOnFrameAvailableListener(null);
            this.zzQf = null;
            zzgi();
        }
    }

    public final void zza(SurfaceTexture surfaceTexture, int i, int i2) {
        this.zzrW = i;
        this.zzrX = i2;
        this.zzQg = surfaceTexture;
    }

    public final void zzb(float f, float f2) {
        float f3;
        float f4;
        if (this.zzrW > this.zzrX) {
            f3 = (1.7453293f * f) / this.zzrW;
            f4 = (1.7453293f * f2) / this.zzrW;
        } else {
            f3 = (1.7453293f * f) / this.zzrX;
            f4 = (1.7453293f * f2) / this.zzrX;
        }
        this.zzQd -= f3;
        this.zzQe -= f4;
        if (this.zzQe < -1.5707964f) {
            this.zzQe = -1.5707964f;
        }
        if (this.zzQe > 1.5707964f) {
            this.zzQe = 1.5707964f;
        }
    }

    public final void zzf(int i, int i2) {
        synchronized (this.zzQm) {
            this.zzrW = i;
            this.zzrX = i2;
            this.zzQr = true;
            this.zzQm.notifyAll();
        }
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzao
    public final void zzfO() {
        synchronized (this.zzQm) {
            this.zzQm.notifyAll();
        }
    }

    public final void zzgf() {
        synchronized (this.zzQm) {
            this.zzQs = true;
            this.zzQg = null;
            this.zzQm.notifyAll();
        }
    }

    public final SurfaceTexture zzgg() {
        if (this.zzQg == null) {
            return null;
        }
        try {
            this.zzQl.await();
        } catch (InterruptedException e) {
        }
        return this.zzQf;
    }
}
