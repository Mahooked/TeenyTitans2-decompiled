package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import java.util.Collections;
import java.util.List;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaff {
    public final int errorCode;
    public final int orientation;

    @Nullable
    public final zzua zzMG;

    @Nullable
    public final zzut zzMH;

    @Nullable
    public final String zzMI;

    @Nullable
    public final zzud zzMJ;
    public final List<String> zzMa;
    public final List<String> zzMb;

    @Nullable
    public final List<String> zzMd;
    public final long zzMg;

    @Nullable
    public final zzaka zzPg;
    public final String zzSC;
    public final zzir zzSz;

    @Nullable
    public final zzaee zzTD;

    @Nullable
    public final List<String> zzTF;
    public final boolean zzTG;
    private zzaak zzTH;
    public final String zzTK;
    private long zzTn;
    public final boolean zzTo;
    private long zzTp;
    public final List<String> zzTq;
    public final String zzTt;
    public final JSONObject zzXL;
    public boolean zzXM;
    public final zzub zzXN;

    @Nullable
    public final String zzXO;
    public final zziv zzXP;

    @Nullable
    public final List<String> zzXQ;
    public final long zzXR;
    public final long zzXS;

    @Nullable
    public final zzoa zzXT;
    public boolean zzXU;
    public boolean zzXV;
    public boolean zzXW;
    private zzig zzXX;

    public zzaff(zzafg zzafgVar, @Nullable zzaka zzakaVar, @Nullable zzua zzuaVar, @Nullable zzut zzutVar, @Nullable String str, @Nullable zzud zzudVar, @Nullable zzoa zzoaVar, @Nullable String str2) {
        this(zzafgVar.zzUj.zzSz, null, zzafgVar.zzXY.zzMa, zzafgVar.errorCode, zzafgVar.zzXY.zzMb, zzafgVar.zzXY.zzTq, zzafgVar.zzXY.orientation, zzafgVar.zzXY.zzMg, zzafgVar.zzUj.zzSC, zzafgVar.zzXY.zzTo, null, null, null, zzafgVar.zzXN, null, zzafgVar.zzXY.zzTp, zzafgVar.zzvX, zzafgVar.zzXY.zzTn, zzafgVar.zzXR, zzafgVar.zzXS, zzafgVar.zzXY.zzTt, zzafgVar.zzXL, null, zzafgVar.zzXY.zzTD, zzafgVar.zzXY.zzTE, zzafgVar.zzXY.zzTE, zzafgVar.zzXY.zzTG, zzafgVar.zzXY.zzTH, null, zzafgVar.zzXY.zzMd, zzafgVar.zzXY.zzTK, zzafgVar.zzXX);
    }

    public zzaff(zzir zzirVar, @Nullable zzaka zzakaVar, List<String> list, int i, List<String> list2, List<String> list3, int i2, long j, String str, boolean z, @Nullable zzua zzuaVar, @Nullable zzut zzutVar, @Nullable String str2, zzub zzubVar, @Nullable zzud zzudVar, long j2, zziv zzivVar, long j3, long j4, long j5, String str3, JSONObject jSONObject, @Nullable zzoa zzoaVar, zzaee zzaeeVar, List<String> list4, List<String> list5, boolean z2, zzaak zzaakVar, @Nullable String str4, List<String> list6, String str5, zzig zzigVar) {
        this.zzXU = false;
        this.zzXV = false;
        this.zzXW = false;
        this.zzSz = zzirVar;
        this.zzPg = zzakaVar;
        this.zzMa = zzn(list);
        this.errorCode = i;
        this.zzMb = zzn(list2);
        this.zzTq = zzn(list3);
        this.orientation = i2;
        this.zzMg = j;
        this.zzSC = str;
        this.zzTo = z;
        this.zzMG = zzuaVar;
        this.zzMH = zzutVar;
        this.zzMI = str2;
        this.zzXN = zzubVar;
        this.zzMJ = zzudVar;
        this.zzTp = j2;
        this.zzXP = zzivVar;
        this.zzTn = j3;
        this.zzXR = j4;
        this.zzXS = j5;
        this.zzTt = str3;
        this.zzXL = jSONObject;
        this.zzXT = zzoaVar;
        this.zzTD = zzaeeVar;
        this.zzXQ = zzn(list4);
        this.zzTF = zzn(list5);
        this.zzTG = z2;
        this.zzTH = zzaakVar;
        this.zzXO = str4;
        this.zzMd = zzn(list6);
        this.zzTK = str5;
        this.zzXX = zzigVar;
    }

    @Nullable
    private static <T> List<T> zzn(@Nullable List<T> list) {
        if (list == null) {
            return null;
        }
        return Collections.unmodifiableList(list);
    }

    public final boolean zzcn() {
        if (this.zzPg == null || this.zzPg.zziw() == null) {
            return false;
        }
        return this.zzPg.zziw().zzcn();
    }
}
