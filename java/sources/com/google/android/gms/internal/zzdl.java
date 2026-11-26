package com.google.android.gms.internal;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.concurrent.ExecutionException;

/* JADX INFO: loaded from: classes.dex */
public final class zzdl extends zzec {
    private zzau zzrn;
    private static volatile zzbu zzrm = null;
    private static final Object zzrl = new Object();

    public zzdl(zzdb zzdbVar, String str, String str2, zzax zzaxVar, int i, int i2, zzau zzauVar) {
        super(zzdbVar, str, str2, zzaxVar, i, 27);
        this.zzrn = null;
        this.zzrn = zzauVar;
    }

    private final String zzU() {
        try {
            if (this.zzpJ.zzL() != null) {
                this.zzpJ.zzL().get();
            }
            zzax zzaxVarZzK = this.zzpJ.zzK();
            if (zzaxVarZzK != null && zzaxVarZzK.zzaT != null) {
                return zzaxVarZzK.zzaT;
            }
        } catch (InterruptedException e) {
        } catch (ExecutionException e2) {
        }
        return null;
    }

    private static String zza(zzau zzauVar) {
        if (zzauVar == null || zzauVar.zzaR == null || zzdg.zzo(zzauVar.zzaR.zzaT)) {
            return null;
        }
        return zzauVar.zzaR.zzaT;
    }

    /* JADX WARN: Code duplicated, block: B:52:0x010a  */
    /* JADX WARN: Code duplicated, block: B:54:0x010f  */
    @Override // com.google.android.gms.internal.zzec
    protected final void zzT() throws IllegalAccessException, InvocationTargetException {
        boolean z;
        char c = 3;
        if (zzrm == null || zzdg.zzo(zzrm.zzaT) || zzrm.zzaT.equals("E") || zzrm.zzaT.equals("0000000000000000000000000000000000000000000000000000000000000000")) {
            synchronized (zzrl) {
                if (zzdg.zzo(zza(this.zzrn))) {
                    zzau zzauVar = this.zzrn;
                    if (Boolean.valueOf(zzdg.zzo(zza(zzauVar)) && zzauVar != null && zzauVar.zzaQ != null && zzauVar.zzaQ.zzaS.intValue() == 3).booleanValue()) {
                        if (!this.zzpJ.zzJ()) {
                            z = false;
                        } else if (!((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFc)).booleanValue()) {
                            z = false;
                        } else if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFd)).booleanValue()) {
                            z = true;
                        } else {
                            z = false;
                        }
                        if (!z) {
                            c = 2;
                        }
                    } else {
                        c = 2;
                    }
                } else {
                    c = 4;
                }
                Method method = this.zzrx;
                Object[] objArr = new Object[2];
                objArr[0] = this.zzpJ.getContext();
                objArr[1] = Boolean.valueOf(c == 2);
                zzbu zzbuVar = new zzbu((String) method.invoke(null, objArr));
                zzrm = zzbuVar;
                if (zzdg.zzo(zzbuVar.zzaT) || zzrm.zzaT.equals("E")) {
                    switch (c) {
                        case 3:
                            String strZzU = zzU();
                            if (!zzdg.zzo(strZzU)) {
                                zzrm.zzaT = strZzU;
                            }
                            break;
                        case 4:
                            zzrm.zzaT = this.zzrn.zzaR.zzaT;
                            break;
                    }
                }
            }
        }
        synchronized (this.zzro) {
            if (zzrm != null) {
                this.zzro.zzaT = zzrm.zzaT;
                this.zzro.zzbx = Long.valueOf(zzrm.zzlO);
                this.zzro.zzaV = zzrm.zzaV;
                this.zzro.zzaW = zzrm.zzaW;
                this.zzro.zzaX = zzrm.zzaX;
            }
        }
    }
}
