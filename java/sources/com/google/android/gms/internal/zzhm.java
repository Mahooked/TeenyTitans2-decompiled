package com.google.android.gms.internal;

import java.util.PriorityQueue;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzhm {
    private static long zza(long j, int i) {
        if (i == 0) {
            return 1L;
        }
        if (i != 1) {
            return i % 2 == 0 ? zza((j * j) % 1073807359, i / 2) % 1073807359 : ((zza((j * j) % 1073807359, i / 2) % 1073807359) * j) % 1073807359;
        }
        return j;
    }

    private static String zza(String[] strArr, int i, int i2) {
        if (strArr.length < i + i2) {
            zzafr.e("Unable to construct shingle");
            return "";
        }
        StringBuffer stringBuffer = new StringBuffer();
        for (int i3 = i; i3 < (i + i2) - 1; i3++) {
            stringBuffer.append(strArr[i3]);
            stringBuffer.append(' ');
        }
        stringBuffer.append(strArr[(i + i2) - 1]);
        return stringBuffer.toString();
    }

    private static void zza(int i, long j, String str, int i2, PriorityQueue<zzhn> priorityQueue) {
        zzhn zzhnVar = new zzhn(j, str, i2);
        if ((priorityQueue.size() != i || (priorityQueue.peek().zzzf <= zzhnVar.zzzf && priorityQueue.peek().value <= zzhnVar.value)) && !priorityQueue.contains(zzhnVar)) {
            priorityQueue.add(zzhnVar);
            if (priorityQueue.size() > i) {
                priorityQueue.poll();
            }
        }
    }

    public static void zza(String[] strArr, int i, int i2, PriorityQueue<zzhn> priorityQueue) {
        if (strArr.length < i2) {
            zza(i, zzb(strArr, 0, strArr.length), zza(strArr, 0, strArr.length), strArr.length, priorityQueue);
            return;
        }
        long jZzb = zzb(strArr, 0, i2);
        zza(i, jZzb, zza(strArr, 0, i2), i2, priorityQueue);
        long jZza = zza(16785407L, i2 - 1);
        for (int i3 = 1; i3 < (strArr.length - i2) + 1; i3++) {
            jZzb = ((((((jZzb + 1073807359) - ((((((long) zzhj.zzA(strArr[i3 - 1])) + 2147483647L) % 1073807359) * jZza) % 1073807359)) % 1073807359) * 16785407) % 1073807359) + ((((long) zzhj.zzA(strArr[(i3 + i2) - 1])) + 2147483647L) % 1073807359)) % 1073807359;
            zza(i, jZzb, zza(strArr, i3, i2), strArr.length, priorityQueue);
        }
    }

    private static long zzb(String[] strArr, int i, int i2) {
        long jZzA = (((long) zzhj.zzA(strArr[0])) + 2147483647L) % 1073807359;
        for (int i3 = 1; i3 < i2; i3++) {
            jZzA = (((jZzA * 16785407) % 1073807359) + ((((long) zzhj.zzA(strArr[i3])) + 2147483647L) % 1073807359)) % 1073807359;
        }
        return jZzA;
    }
}
