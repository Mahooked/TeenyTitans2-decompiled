package com.google.android.gms.internal;

import java.net.Inet4Address;
import java.net.InetAddress;
import java.net.UnknownHostException;
import java.nio.ByteBuffer;
import java.util.Arrays;
import java.util.Locale;
import java.util.regex.Pattern;

/* JADX INFO: loaded from: classes.dex */
final class zzcsq {
    private static final Pattern zzbCf = Pattern.compile("[.]");
    private static final Inet4Address zzbCg = (Inet4Address) zzeN("127.0.0.1");
    private static final Inet4Address zzbCh = (Inet4Address) zzeN("0.0.0.0");

    static String zza(InetAddress inetAddress) {
        int i;
        if (inetAddress instanceof Inet4Address) {
            return inetAddress.getHostAddress();
        }
        byte[] address = inetAddress.getAddress();
        int[] iArr = new int[8];
        for (int i2 = 0; i2 < 8; i2++) {
            iArr[i2] = ((address[i2 * 2] & 255) << 8) | 0 | (address[(i2 * 2) + 1] & 255);
        }
        int i3 = -1;
        int i4 = -1;
        int i5 = -1;
        for (int i6 = 0; i6 < 9; i6++) {
            if (i6 >= 8 || iArr[i6] != 0) {
                if (i3 >= 0) {
                    int i7 = i6 - i3;
                    if (i7 > i4) {
                        i = i3;
                    } else {
                        i7 = i4;
                        i = i5;
                    }
                    i3 = -1;
                    i5 = i;
                    i4 = i7;
                }
            } else if (i3 < 0) {
                i3 = i6;
            }
        }
        if (i4 >= 2) {
            Arrays.fill(iArr, i5, i5 + i4, -1);
        }
        StringBuilder sb = new StringBuilder(39);
        int i8 = 0;
        boolean z = false;
        while (i8 < 8) {
            boolean z2 = iArr[i8] >= 0;
            if (z2) {
                if (z) {
                    sb.append(':');
                }
                sb.append(Integer.toHexString(iArr[i8]));
            } else if (i8 == 0 || z) {
                sb.append("::");
            }
            i8++;
            z = z2;
        }
        return sb.toString();
    }

    static boolean zzeI(String str) {
        return zzeJ(str) != null;
    }

    private static byte[] zzeJ(String str) {
        boolean z = false;
        boolean z2 = false;
        for (int i = 0; i < str.length(); i++) {
            char cCharAt = str.charAt(i);
            if (cCharAt == '.') {
                z = true;
            } else if (cCharAt == ':') {
                if (z) {
                    return null;
                }
                z2 = true;
            } else if (Character.digit(cCharAt, 16) == -1) {
                return null;
            }
        }
        if (!z2) {
            if (z) {
                return zzeK(str);
            }
            return null;
        }
        if (z) {
            int iLastIndexOf = str.lastIndexOf(58);
            String strSubstring = str.substring(0, iLastIndexOf + 1);
            byte[] bArrZzeK = zzeK(str.substring(iLastIndexOf + 1));
            if (bArrZzeK == null) {
                str = null;
            } else {
                String hexString = Integer.toHexString(((bArrZzeK[0] & 255) << 8) | (bArrZzeK[1] & 255));
                String hexString2 = Integer.toHexString((bArrZzeK[3] & 255) | ((bArrZzeK[2] & 255) << 8));
                str = new StringBuilder(String.valueOf(strSubstring).length() + 1 + String.valueOf(hexString).length() + String.valueOf(hexString2).length()).append(strSubstring).append(hexString).append(":").append(hexString2).toString();
            }
            if (str == null) {
                return null;
            }
        }
        return zzeL(str);
    }

    private static byte[] zzeK(String str) {
        int i = 0;
        byte[] bArr = new byte[4];
        try {
            String[] strArrSplit = zzbCf.split(str, 4);
            int length = strArrSplit.length;
            int i2 = 0;
            while (i < length) {
                String str2 = strArrSplit[i];
                int i3 = i2 + 1;
                int i4 = Integer.parseInt(str2);
                if (i4 > 255 || (str2.startsWith("0") && str2.length() > 1)) {
                    throw new NumberFormatException();
                }
                bArr[i2] = (byte) i4;
                i++;
                i2 = i3;
            }
            if (i2 != 4) {
                return null;
            }
            return bArr;
        } catch (NumberFormatException e) {
            return null;
        }
    }

    private static byte[] zzeL(String str) {
        int length;
        int i;
        int i2;
        String[] strArrSplit = str.split(":", 10);
        if (strArrSplit.length < 3 || strArrSplit.length > 9) {
            return null;
        }
        int i3 = -1;
        for (int i4 = 1; i4 < strArrSplit.length - 1; i4++) {
            if (strArrSplit[i4].length() == 0) {
                if (i3 >= 0) {
                    return null;
                }
                i3 = i4;
            }
        }
        if (i3 >= 0) {
            int length2 = (strArrSplit.length - i3) - 1;
            if (strArrSplit[0].length() == 0) {
                i2 = i3 - 1;
                if (i2 != 0) {
                    return null;
                }
            } else {
                i2 = i3;
            }
            if (strArrSplit[strArrSplit.length - 1].length() == 0 && (length2 = length2 - 1) != 0) {
                return null;
            }
            int i5 = length2;
            length = i2;
            i = i5;
        } else {
            length = strArrSplit.length;
            i = 0;
        }
        int i6 = 8 - (length + i);
        if (i3 < 0 ? i6 != 0 : i6 <= 0) {
            return null;
        }
        ByteBuffer byteBufferAllocate = ByteBuffer.allocate(16);
        for (int i7 = 0; i7 < length; i7++) {
            try {
                byteBufferAllocate.putShort(zzeM(strArrSplit[i7]));
            } catch (NumberFormatException e) {
                return null;
            }
        }
        for (int i8 = 0; i8 < i6; i8++) {
            byteBufferAllocate.putShort((short) 0);
        }
        while (i > 0) {
            byteBufferAllocate.putShort(zzeM(strArrSplit[strArrSplit.length - i]));
            i--;
        }
        return byteBufferAllocate.array();
    }

    private static short zzeM(String str) {
        int i = Integer.parseInt(str, 16);
        if (i > 65535) {
            throw new NumberFormatException();
        }
        return (short) i;
    }

    static InetAddress zzeN(String str) {
        byte[] bArrZzeJ = zzeJ(str);
        if (bArrZzeJ == null) {
            throw new IllegalArgumentException(String.format(Locale.ROOT, "'%s' is not an IP string literal.", str));
        }
        return zzq(bArrZzeJ);
    }

    private static InetAddress zzq(byte[] bArr) {
        try {
            return InetAddress.getByAddress(bArr);
        } catch (UnknownHostException e) {
            throw new AssertionError(e);
        }
    }
}
