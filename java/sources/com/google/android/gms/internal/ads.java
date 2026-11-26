package com.google.android.gms.internal;

import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class ads {
    private static int zzcsy = 11;
    private static int zzcsz = 12;
    private static int zzcsA = 16;
    private static int zzcsB = 26;
    public static final int[] zzcsC = new int[0];
    public static final long[] zzcsD = new long[0];
    public static final float[] zzcsE = new float[0];
    private static double[] zzcsF = new double[0];
    public static final boolean[] zzcsG = new boolean[0];
    public static final String[] EMPTY_STRING_ARRAY = new String[0];
    public static final byte[][] zzcsH = new byte[0][];
    public static final byte[] zzcsI = new byte[0];

    public static final int zzb(adg adgVar, int i) throws IOException {
        int i2 = 1;
        int position = adgVar.getPosition();
        adgVar.zzcm(i);
        while (adgVar.zzLA() == i) {
            adgVar.zzcm(i);
            i2++;
        }
        adgVar.zzq(position, i);
        return i2;
    }
}
