package com.google.android.gms.internal;

import com.fasterxml.jackson.core.util.MinimalPrettyPrinter;
import java.io.IOException;
import java.text.Normalizer;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Locale;
import org.apache.commons.io.IOUtils;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzhp {
    private final int zzyY;
    private final zzhf zzza;
    private String zzzi;
    private String zzzj;
    private final boolean zzzk = false;
    private final int zzzl;
    private final int zzzm;

    public zzhp(int i, int i2, int i3) {
        this.zzyY = i;
        if (i2 > 64 || i2 < 0) {
            this.zzzl = 64;
        } else {
            this.zzzl = i2;
        }
        if (i3 <= 0) {
            this.zzzm = 1;
        } else {
            this.zzzm = i3;
        }
        this.zzza = new zzho(this.zzzl);
    }

    /* JADX WARN: Code duplicated, block: B:62:0x00e2  */
    private final boolean zza(String str, HashSet<String> hashSet) {
        String string;
        boolean z;
        String[] strArrSplit = str.split(IOUtils.LINE_SEPARATOR_UNIX);
        if (strArrSplit.length == 0) {
            return true;
        }
        for (String str2 : strArrSplit) {
            if (str2.indexOf("'") != -1) {
                StringBuilder sb = new StringBuilder(str2);
                int i = 1;
                boolean z2 = false;
                while (i + 2 <= sb.length()) {
                    if (sb.charAt(i) == '\'') {
                        if (sb.charAt(i - 1) == ' ' || !((sb.charAt(i + 1) == 's' || sb.charAt(i + 1) == 'S') && (i + 2 == sb.length() || sb.charAt(i + 2) == ' '))) {
                            sb.setCharAt(i, ' ');
                        } else {
                            sb.insert(i, ' ');
                            i += 2;
                        }
                        z2 = true;
                    }
                    i++;
                }
                string = z2 ? sb.toString() : null;
                if (string != null) {
                    this.zzzj = string;
                } else {
                    string = str2;
                }
            } else {
                string = str2;
            }
            String[] strArrZzd = zzhj.zzd(string, true);
            if (strArrZzd.length >= this.zzzm) {
                for (int i2 = 0; i2 < strArrZzd.length; i2++) {
                    String strConcat = "";
                    int i3 = 0;
                    while (true) {
                        if (i3 >= this.zzzm) {
                            z = true;
                            break;
                        }
                        if (i2 + i3 >= strArrZzd.length) {
                            z = false;
                            break;
                        }
                        if (i3 > 0) {
                            strConcat = String.valueOf(strConcat).concat(MinimalPrettyPrinter.DEFAULT_ROOT_VALUE_SEPARATOR);
                        }
                        String strValueOf = String.valueOf(strConcat);
                        String strValueOf2 = String.valueOf(strArrZzd[i2 + i3]);
                        strConcat = strValueOf2.length() != 0 ? strValueOf.concat(strValueOf2) : new String(strValueOf);
                        i3++;
                    }
                    if (!z) {
                        break;
                    }
                    hashSet.add(strConcat);
                    if (hashSet.size() >= this.zzyY) {
                        return false;
                    }
                }
                if (hashSet.size() >= this.zzyY) {
                    return false;
                }
            }
        }
        return true;
    }

    public final String zza(ArrayList<String> arrayList, ArrayList<zzhe> arrayList2) {
        Collections.sort(arrayList2, new zzhq(this));
        HashSet<String> hashSet = new HashSet<>();
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 >= arrayList2.size() || !zza(Normalizer.normalize(arrayList.get(arrayList2.get(i2).zzcV()), Normalizer.Form.NFKC).toLowerCase(Locale.US), hashSet)) {
                break;
            }
            i = i2 + 1;
        }
        zzhi zzhiVar = new zzhi();
        this.zzzi = "";
        Iterator<String> it = hashSet.iterator();
        while (it.hasNext()) {
            try {
                zzhiVar.write(this.zzza.zzy(it.next()));
            } catch (IOException e) {
                zzafr.zzb("Error while writing hash to byteStream", e);
            }
        }
        return zzhiVar.toString();
    }
}
