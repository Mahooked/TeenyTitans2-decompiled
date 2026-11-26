package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzaku implements Runnable {
    private /* synthetic */ zzaks zzacC;
    private /* synthetic */ int zzacD;
    private /* synthetic */ int zzacE;
    private /* synthetic */ boolean zzacF;
    private /* synthetic */ boolean zzacG;

    zzaku(zzaks zzaksVar, int i, int i2, boolean z, boolean z2) {
        this.zzacC = zzaksVar;
        this.zzacD = i;
        this.zzacE = i2;
        this.zzacF = z;
        this.zzacG = z2;
    }

    /* JADX WARN: Code duplicated, block: B:45:0x0077 A[DONT_INVERT] */
    /* JADX WARN: Code duplicated, block: B:47:0x0082 A[DONT_INVERT] */
    /* JADX WARN: Code duplicated, block: B:49:0x008d A[DONT_INVERT] */
    /* JADX WARN: Code duplicated, block: B:67:0x0079 A[EXC_TOP_SPLITTER, SYNTHETIC] */
    /* JADX WARN: Code duplicated, block: B:71:0x008f A[EXC_TOP_SPLITTER, SYNTHETIC] */
    /* JADX WARN: Code duplicated, block: B:74:0x0084 A[EXC_TOP_SPLITTER, SYNTHETIC] */
    /* JADX WARN: Code duplicated, block: B:76:0x006e A[EXC_TOP_SPLITTER, SYNTHETIC] */
    @Override // java.lang.Runnable
    public final void run() {
        synchronized (this.zzacC.mLock) {
            boolean z = this.zzacD != this.zzacE;
            boolean z2 = !this.zzacC.zzacx && this.zzacE == 1;
            boolean z3 = z && this.zzacE == 1;
            boolean z4 = z && this.zzacE == 2;
            boolean z5 = z && this.zzacE == 3;
            boolean z6 = this.zzacF != this.zzacG;
            this.zzacC.zzacx = this.zzacC.zzacx || z2;
            if (this.zzacC.zzacw == null) {
                return;
            }
            if (!z2) {
                if (z3) {
                    if (z4) {
                        if (z5) {
                            if (z6) {
                                this.zzacC.zzacw.onVideoMute(this.zzacG);
                            }
                            return;
                        } else {
                            this.zzacC.zzacw.onVideoEnd();
                            if (z6) {
                                this.zzacC.zzacw.onVideoMute(this.zzacG);
                            }
                            return;
                        }
                    }
                    this.zzacC.zzacw.onVideoPause();
                    if (z5) {
                        if (z6) {
                            this.zzacC.zzacw.onVideoMute(this.zzacG);
                        }
                        return;
                    } else {
                        this.zzacC.zzacw.onVideoEnd();
                        if (z6) {
                            this.zzacC.zzacw.onVideoMute(this.zzacG);
                        }
                        return;
                    }
                }
                this.zzacC.zzacw.onVideoPlay();
                if (z4) {
                    if (z5) {
                        if (z6) {
                            this.zzacC.zzacw.onVideoMute(this.zzacG);
                        }
                        return;
                    } else {
                        this.zzacC.zzacw.onVideoEnd();
                        if (z6) {
                            this.zzacC.zzacw.onVideoMute(this.zzacG);
                        }
                        return;
                    }
                }
                this.zzacC.zzacw.onVideoPause();
                if (z5) {
                    if (z6) {
                        this.zzacC.zzacw.onVideoMute(this.zzacG);
                    }
                    return;
                } else {
                    this.zzacC.zzacw.onVideoEnd();
                    if (z6) {
                        this.zzacC.zzacw.onVideoMute(this.zzacG);
                    }
                    return;
                }
            }
            try {
                this.zzacC.zzacw.onVideoStart();
            } catch (RemoteException e) {
                zzafr.zzc("Unable to call onVideoStart()", e);
            }
            if (z3) {
                if (z4) {
                    if (z5) {
                        if (z6) {
                            this.zzacC.zzacw.onVideoMute(this.zzacG);
                        }
                        return;
                    } else {
                        this.zzacC.zzacw.onVideoEnd();
                        if (z6) {
                            this.zzacC.zzacw.onVideoMute(this.zzacG);
                        }
                        return;
                    }
                }
                this.zzacC.zzacw.onVideoPause();
                if (z5) {
                    if (z6) {
                        this.zzacC.zzacw.onVideoMute(this.zzacG);
                    }
                    return;
                } else {
                    this.zzacC.zzacw.onVideoEnd();
                    if (z6) {
                        this.zzacC.zzacw.onVideoMute(this.zzacG);
                    }
                    return;
                }
            }
            try {
                this.zzacC.zzacw.onVideoPlay();
            } catch (RemoteException e2) {
                zzafr.zzc("Unable to call onVideoPlay()", e2);
            }
            if (z4) {
                if (z5) {
                    if (z6) {
                        this.zzacC.zzacw.onVideoMute(this.zzacG);
                    }
                    return;
                } else {
                    this.zzacC.zzacw.onVideoEnd();
                    if (z6) {
                        this.zzacC.zzacw.onVideoMute(this.zzacG);
                    }
                    return;
                }
            }
            try {
                this.zzacC.zzacw.onVideoPause();
            } catch (RemoteException e3) {
                zzafr.zzc("Unable to call onVideoPause()", e3);
            }
            if (z5) {
                if (z6) {
                    this.zzacC.zzacw.onVideoMute(this.zzacG);
                }
                return;
            }
            try {
                this.zzacC.zzacw.onVideoEnd();
            } catch (RemoteException e4) {
                zzafr.zzc("Unable to call onVideoEnd()", e4);
            }
            if (z6) {
                try {
                    this.zzacC.zzacw.onVideoMute(this.zzacG);
                } catch (RemoteException e5) {
                    zzafr.zzc("Unable to call onVideoMute()", e5);
                }
            }
            return;
            throw th;
        }
    }
}
