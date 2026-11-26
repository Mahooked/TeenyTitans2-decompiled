package com.google.android.gms.internal;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.Locale;
import java.util.PriorityQueue;
import org.apache.commons.io.IOUtils;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzhg {
    private final int zzyY;
    private final zzhf zzza = new zzhk();
    private final int zzyX = 6;
    private final int zzyZ = 0;

    public zzhg(int i) {
        this.zzyY = i;
    }

    private final String zzz(String str) {
        String[] strArrSplit = str.split(IOUtils.LINE_SEPARATOR_UNIX);
        if (strArrSplit.length == 0) {
            return "";
        }
        zzhi zzhiVar = new zzhi();
        PriorityQueue priorityQueue = new PriorityQueue(this.zzyY, new zzhh(this));
        for (String str2 : strArrSplit) {
            String[] strArrZzd = zzhj.zzd(str2, false);
            if (strArrZzd.length != 0) {
                zzhm.zza(strArrZzd, this.zzyY, this.zzyX, priorityQueue);
            }
        }
        Iterator it = priorityQueue.iterator();
        while (it.hasNext()) {
            try {
                zzhiVar.write(this.zzza.zzy(((zzhn) it.next()).zzze));
            } catch (IOException e) {
                zzafr.zzb("Error while writing hash to byteStream", e);
            }
        }
        return zzhiVar.toString();
    }

    public final String zza(ArrayList<String> arrayList) {
        StringBuffer stringBuffer = new StringBuffer();
        ArrayList<String> arrayList2 = arrayList;
        int size = arrayList2.size();
        int i = 0;
        while (i < size) {
            String str = arrayList2.get(i);
            i++;
            stringBuffer.append(str.toLowerCase(Locale.US));
            stringBuffer.append('\n');
        }
        return zzz(stringBuffer.toString());
    }
}
