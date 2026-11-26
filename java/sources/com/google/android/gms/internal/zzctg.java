package com.google.android.gms.internal;

import com.google.android.gms.common.Scopes;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Scope;

/* JADX INFO: loaded from: classes.dex */
public final class zzctg {
    private static Api.zzf<zzctu> zzajR = new Api.zzf<>();
    private static Api.zzf<zzctu> zzbCJ = new Api.zzf<>();
    public static final Api.zza<zzctu, zzctl> zzajS = new zzcth();
    private static Api.zza<zzctu, zzctj> zzbCK = new zzcti();
    private static Scope zzalV = new Scope(Scopes.PROFILE);
    private static Scope zzalW = new Scope("email");
    public static final Api<zzctl> API = new Api<>("SignIn.API", zzajS, zzajR);
    private static Api<zzctj> zzaMc = new Api<>("SignIn.INTERNAL_API", zzbCK, zzbCJ);
}
