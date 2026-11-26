package com.google.android.gms.internal;

import com.google.ads.AdRequest;

/* JADX INFO: loaded from: classes.dex */
final /* synthetic */ class zzwc {
    private static /* synthetic */ int[] zzNm;
    static final /* synthetic */ int[] zzNn = new int[AdRequest.ErrorCode.values().length];

    static {
        try {
            zzNn[AdRequest.ErrorCode.INTERNAL_ERROR.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            zzNn[AdRequest.ErrorCode.INVALID_REQUEST.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            zzNn[AdRequest.ErrorCode.NETWORK_ERROR.ordinal()] = 3;
        } catch (NoSuchFieldError e3) {
        }
        try {
            zzNn[AdRequest.ErrorCode.NO_FILL.ordinal()] = 4;
        } catch (NoSuchFieldError e4) {
        }
        zzNm = new int[AdRequest.Gender.values().length];
        try {
            zzNm[AdRequest.Gender.FEMALE.ordinal()] = 1;
        } catch (NoSuchFieldError e5) {
        }
        try {
            zzNm[AdRequest.Gender.MALE.ordinal()] = 2;
        } catch (NoSuchFieldError e6) {
        }
        try {
            zzNm[AdRequest.Gender.UNKNOWN.ordinal()] = 3;
        } catch (NoSuchFieldError e7) {
        }
    }
}
