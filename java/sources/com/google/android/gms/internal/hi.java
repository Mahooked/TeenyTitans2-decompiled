package com.google.android.gms.internal;

import android.content.ContentResolver;
import android.database.Cursor;
import android.net.Uri;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.TreeMap;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.regex.Pattern;

/* JADX INFO: loaded from: classes.dex */
public class hi {
    private static HashMap<String, String> zzbUe;
    private static Object zzbUj;
    private static boolean zzbUk;
    private static Uri CONTENT_URI = Uri.parse("content://com.google.android.gsf.gservices");
    private static Uri zzbUa = Uri.parse("content://com.google.android.gsf.gservices/prefix");
    private static Pattern zzbUb = Pattern.compile("^(1|true|t|on|yes|y)$", 2);
    private static Pattern zzbUc = Pattern.compile("^(0|false|f|off|no|n)$", 2);
    private static final AtomicBoolean zzbUd = new AtomicBoolean();
    private static HashMap<String, Boolean> zzbUf = new HashMap<>();
    private static HashMap<String, Integer> zzbUg = new HashMap<>();
    private static HashMap<String, Long> zzbUh = new HashMap<>();
    private static HashMap<String, Float> zzbUi = new HashMap<>();
    private static String[] zzbUl = new String[0];

    public static long getLong(ContentResolver contentResolver, String str, long j) {
        Long lValueOf;
        long j2;
        Object objZzb = zzb(contentResolver);
        Long l = (Long) zza((HashMap<String, long>) zzbUh, str, 0L);
        if (l != null) {
            return l.longValue();
        }
        String strZza = zza(contentResolver, str, (String) null);
        if (strZza == null) {
            lValueOf = l;
            j2 = 0;
        } else {
            try {
                long j3 = Long.parseLong(strZza);
                lValueOf = Long.valueOf(j3);
                j2 = j3;
            } catch (NumberFormatException e) {
                lValueOf = l;
                j2 = 0;
            }
        }
        HashMap<String, Long> map = zzbUh;
        synchronized (hi.class) {
            if (objZzb == zzbUj) {
                map.put(str, lValueOf);
                zzbUe.remove(str);
            }
        }
        return j2;
    }

    private static <T> T zza(HashMap<String, T> map, String str, T t) {
        synchronized (hi.class) {
            if (!map.containsKey(str)) {
                return null;
            }
            T t2 = map.get(str);
            if (t2 == null) {
                t2 = t;
            }
            return t2;
        }
    }

    /* JADX WARN: Code duplicated, block: B:29:0x0057 A[Catch: all -> 0x0054, DONT_GENERATE, TRY_ENTER, TryCatch #0 {, blocks: (B:4:0x0006, B:6:0x0013, B:9:0x001e, B:11:0x0020, B:13:0x0026, B:15:0x002e, B:17:0x0032, B:29:0x0057, B:19:0x003a, B:21:0x0047, B:24:0x0052, B:31:0x0059, B:32:0x005c), top: B:55:0x0006 }] */
    /* JADX WARN: Code duplicated, block: B:37:0x0072 A[Catch: all -> 0x0097, TRY_LEAVE, TryCatch #1 {all -> 0x0097, blocks: (B:35:0x006c, B:41:0x007d, B:43:0x0083, B:46:0x008b, B:37:0x0072), top: B:56:0x006c }] */
    /* JADX WARN: Code duplicated, block: B:39:0x0078 A[DONT_GENERATE] */
    public static String zza(ContentResolver contentResolver, String str, String str2) {
        String str3 = null;
        synchronized (hi.class) {
            zza(contentResolver);
            Object obj = zzbUj;
            if (zzbUe.containsKey(str)) {
                String str4 = zzbUe.get(str);
                str3 = str4 != null ? str4 : null;
            } else {
                for (String str5 : zzbUl) {
                    if (str.startsWith(str5)) {
                        if (!zzbUk || zzbUe.isEmpty()) {
                            zzc(contentResolver, zzbUl);
                            if (zzbUe.containsKey(str)) {
                                String str6 = zzbUe.get(str);
                                str3 = str6 != null ? str6 : null;
                            }
                        }
                    }
                }
                Cursor cursorQuery = contentResolver.query(CONTENT_URI, null, null, new String[]{str}, null);
                if (cursorQuery != null) {
                    try {
                        if (cursorQuery.moveToFirst()) {
                            String string = cursorQuery.getString(1);
                            if (string != null && string.equals(null)) {
                                string = null;
                            }
                            zza(obj, str, string);
                            str3 = string != null ? string : null;
                        } else {
                            zza(obj, str, (String) null);
                        }
                    } finally {
                        if (cursorQuery != null) {
                            cursorQuery.close();
                        }
                    }
                } else {
                    zza(obj, str, (String) null);
                }
            }
        }
        return str3;
    }

    private static Map<String, String> zza(ContentResolver contentResolver, String... strArr) {
        Cursor cursorQuery = contentResolver.query(zzbUa, null, null, strArr, null);
        TreeMap treeMap = new TreeMap();
        if (cursorQuery != null) {
            while (cursorQuery.moveToNext()) {
                try {
                    treeMap.put(cursorQuery.getString(0), cursorQuery.getString(1));
                } catch (Throwable th) {
                    cursorQuery.close();
                    throw th;
                }
            }
            cursorQuery.close();
        }
        return treeMap;
    }

    private static void zza(ContentResolver contentResolver) {
        if (zzbUe == null) {
            zzbUd.set(false);
            zzbUe = new HashMap<>();
            zzbUj = new Object();
            zzbUk = false;
            contentResolver.registerContentObserver(CONTENT_URI, true, new hj(null));
            return;
        }
        if (zzbUd.getAndSet(false)) {
            zzbUe.clear();
            zzbUf.clear();
            zzbUg.clear();
            zzbUh.clear();
            zzbUi.clear();
            zzbUj = new Object();
            zzbUk = false;
        }
    }

    private static void zza(Object obj, String str, String str2) {
        synchronized (hi.class) {
            if (obj == zzbUj) {
                zzbUe.put(str, str2);
            }
        }
    }

    private static Object zzb(ContentResolver contentResolver) {
        Object obj;
        synchronized (hi.class) {
            zza(contentResolver);
            obj = zzbUj;
        }
        return obj;
    }

    public static void zzb(ContentResolver contentResolver, String... strArr) {
        String[] strArr2;
        if (strArr.length == 0) {
            return;
        }
        synchronized (hi.class) {
            zza(contentResolver);
            HashSet hashSet = new HashSet((((zzbUl.length + strArr.length) << 2) / 3) + 1);
            hashSet.addAll(Arrays.asList(zzbUl));
            ArrayList arrayList = new ArrayList();
            for (String str : strArr) {
                if (hashSet.add(str)) {
                    arrayList.add(str);
                }
            }
            if (arrayList.isEmpty()) {
                strArr2 = new String[0];
            } else {
                zzbUl = (String[]) hashSet.toArray(new String[hashSet.size()]);
                strArr2 = (String[]) arrayList.toArray(new String[arrayList.size()]);
            }
            if (!zzbUk || zzbUe.isEmpty()) {
                zzc(contentResolver, zzbUl);
            } else if (strArr2.length != 0) {
                zzc(contentResolver, strArr2);
            }
        }
    }

    private static void zzc(ContentResolver contentResolver, String[] strArr) {
        zzbUe.putAll(zza(contentResolver, strArr));
        zzbUk = true;
    }
}
