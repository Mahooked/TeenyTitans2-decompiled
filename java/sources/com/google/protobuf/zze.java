package com.google.protobuf;

import java.lang.reflect.Field;
import java.nio.Buffer;
import java.nio.ByteOrder;
import java.security.AccessController;
import java.util.logging.Level;
import java.util.logging.Logger;
import sun.misc.Unsafe;

/* JADX INFO: loaded from: classes.dex */
final class zze {
    private static final boolean zzcrM;
    private static final long zzcrN;
    private static final boolean zzcrV;
    private static final boolean zzcrW;
    private static final boolean zzcrX;
    private static final zzd zzcrY;
    private static final boolean zzcrZ;
    private static final boolean zzcsa;
    private static final long zzcsb;
    private static final boolean zzcsc;
    private static final Logger logger = Logger.getLogger(zze.class.getName());
    private static final Unsafe zzcrT = zzLv();
    private static final Class<?> zzcrU = zzhP("libcore.io.Memory");

    static final class zza extends zzd {
        zza(Unsafe unsafe) {
            super(unsafe);
        }
    }

    static final class zzb extends zzd {
        zzb(Unsafe unsafe) {
            super(unsafe);
        }
    }

    static final class zzc extends zzd {
        zzc(Unsafe unsafe) {
            super(unsafe);
        }
    }

    static abstract class zzd {
        Unsafe zzcsd;

        zzd(Unsafe unsafe) {
            this.zzcsd = unsafe;
        }
    }

    static {
        zzd zzcVar;
        Field fieldZza;
        zzcrV = zzhP("org.robolectric.Robolectric") != null;
        zzcrW = zzg(Long.TYPE);
        zzcrX = zzg(Integer.TYPE);
        if (zzcrT == null) {
            zzcVar = null;
        } else if (!zzLz()) {
            zzcVar = new zzc(zzcrT);
        } else if (zzcrW) {
            zzcVar = new zzb(zzcrT);
        } else {
            zzcVar = zzcrX ? new zza(zzcrT) : null;
        }
        zzcrY = zzcVar;
        zzcrZ = zzLy();
        zzcrM = zzLw();
        zzcsa = zzLx();
        zzcrN = zzcrM ? zzcrY.zzcsd.arrayBaseOffset(byte[].class) : -1;
        if (!zzLz() || (fieldZza = zza(Buffer.class, "effectiveDirectAddress")) == null) {
            fieldZza = zza(Buffer.class, "address");
        }
        zzcsb = (fieldZza == null || zzcrY == null) ? -1L : zzcrY.zzcsd.objectFieldOffset(fieldZza);
        zzcsc = ByteOrder.nativeOrder() == ByteOrder.BIG_ENDIAN;
    }

    private zze() {
    }

    static boolean zzLt() {
        return zzcrM;
    }

    static long zzLu() {
        return zzcrN;
    }

    private static Unsafe zzLv() {
        try {
            return (Unsafe) AccessController.doPrivileged(new zzf());
        } catch (Throwable th) {
            return null;
        }
    }

    private static boolean zzLw() {
        if (zzcrT == null) {
            return false;
        }
        try {
            Class<?> cls = zzcrT.getClass();
            cls.getMethod("objectFieldOffset", Field.class);
            cls.getMethod("arrayBaseOffset", Class.class);
            cls.getMethod("getInt", Object.class, Long.TYPE);
            cls.getMethod("putInt", Object.class, Long.TYPE, Integer.TYPE);
            cls.getMethod("getLong", Object.class, Long.TYPE);
            cls.getMethod("putLong", Object.class, Long.TYPE, Long.TYPE);
            cls.getMethod("getObject", Object.class, Long.TYPE);
            cls.getMethod("putObject", Object.class, Long.TYPE, Object.class);
            if (zzLz()) {
                return true;
            }
            cls.getMethod("getByte", Object.class, Long.TYPE);
            cls.getMethod("putByte", Object.class, Long.TYPE, Byte.TYPE);
            cls.getMethod("getBoolean", Object.class, Long.TYPE);
            cls.getMethod("putBoolean", Object.class, Long.TYPE, Boolean.TYPE);
            cls.getMethod("getFloat", Object.class, Long.TYPE);
            cls.getMethod("putFloat", Object.class, Long.TYPE, Float.TYPE);
            cls.getMethod("getDouble", Object.class, Long.TYPE);
            cls.getMethod("putDouble", Object.class, Long.TYPE, Double.TYPE);
            return true;
        } catch (Throwable th) {
            Logger logger2 = logger;
            Level level = Level.WARNING;
            String strValueOf = String.valueOf(th);
            logger2.logp(level, "com.google.protobuf.UnsafeUtil", "supportsUnsafeArrayOperations", new StringBuilder(String.valueOf(strValueOf).length() + 71).append("platform method missing - proto runtime falling back to safer methods: ").append(strValueOf).toString());
            return false;
        }
    }

    private static boolean zzLx() {
        if (zzcrT == null) {
            return false;
        }
        try {
            zzcrT.getClass().getMethod("copyMemory", Object.class, Long.TYPE, Object.class, Long.TYPE, Long.TYPE);
            return true;
        } catch (Throwable th) {
            logger.logp(Level.WARNING, "com.google.protobuf.UnsafeUtil", "supportsUnsafeCopyMemory", "copyMemory is missing from platform - proto runtime falling back to safer methods.");
            return false;
        }
    }

    private static boolean zzLy() {
        if (zzcrT == null) {
            return false;
        }
        try {
            Class<?> cls = zzcrT.getClass();
            cls.getMethod("objectFieldOffset", Field.class);
            cls.getMethod("getLong", Object.class, Long.TYPE);
            if (zzLz()) {
                return true;
            }
            cls.getMethod("getByte", Long.TYPE);
            cls.getMethod("putByte", Long.TYPE, Byte.TYPE);
            cls.getMethod("getInt", Long.TYPE);
            cls.getMethod("putInt", Long.TYPE, Integer.TYPE);
            cls.getMethod("getLong", Long.TYPE);
            cls.getMethod("putLong", Long.TYPE, Long.TYPE);
            cls.getMethod("copyMemory", Long.TYPE, Long.TYPE, Long.TYPE);
            return true;
        } catch (Throwable th) {
            Logger logger2 = logger;
            Level level = Level.WARNING;
            String strValueOf = String.valueOf(th);
            logger2.logp(level, "com.google.protobuf.UnsafeUtil", "supportsUnsafeByteBufferOperations", new StringBuilder(String.valueOf(strValueOf).length() + 71).append("platform method missing - proto runtime falling back to safer methods: ").append(strValueOf).toString());
            return false;
        }
    }

    private static boolean zzLz() {
        return (zzcrU == null || zzcrV) ? false : true;
    }

    private static Field zza(Class<?> cls, String str) {
        try {
            Field declaredField = cls.getDeclaredField(str);
            declaredField.setAccessible(true);
            return declaredField;
        } catch (Throwable th) {
            return null;
        }
    }

    private static boolean zzg(Class<?> cls) {
        if (!zzLz()) {
            return false;
        }
        try {
            Class<?> cls2 = zzcrU;
            cls2.getMethod("peekLong", cls, Boolean.TYPE);
            cls2.getMethod("pokeLong", cls, Long.TYPE, Boolean.TYPE);
            cls2.getMethod("pokeInt", cls, Integer.TYPE, Boolean.TYPE);
            cls2.getMethod("peekInt", cls, Boolean.TYPE);
            cls2.getMethod("pokeByte", cls, Byte.TYPE);
            cls2.getMethod("peekByte", cls);
            cls2.getMethod("pokeByteArray", cls, byte[].class, Integer.TYPE, Integer.TYPE);
            cls2.getMethod("peekByteArray", cls, byte[].class, Integer.TYPE, Integer.TYPE);
            return true;
        } catch (Throwable th) {
            return false;
        }
    }

    private static <T> Class<T> zzhP(String str) {
        try {
            return (Class<T>) Class.forName(str);
        } catch (Throwable th) {
            return null;
        }
    }
}
