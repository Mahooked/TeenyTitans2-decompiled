package com.google.android.gms.internal;

import android.net.Uri;
import android.text.TextUtils;
import java.io.ByteArrayOutputStream;
import java.io.UnsupportedEncodingException;
import java.math.BigInteger;
import java.net.Inet4Address;
import java.net.Inet6Address;
import java.net.URI;
import java.net.URISyntaxException;
import java.net.UnknownHostException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.LinkedList;
import java.util.List;
import java.util.Locale;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import org.apache.http.HttpHost;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
public class zzcss {
    private static final String TAG = zzcss.class.getSimpleName();
    private static final char[] zzbCj = "0123456789ABCDEF".toCharArray();
    private static final Pattern zzbCk = Pattern.compile("/\\.\\.");
    private static final Pattern zzbCl = Pattern.compile("0[1-7][0-7]*");
    private static final Pattern zzbCm = Pattern.compile("0x[0-9a-f]*", 2);
    private static final Pattern zzbCn = Pattern.compile("^((?:0x[0-9a-f]+|[0-9\\\\.])+)$", 2);
    private final String mPath;
    private final String zzD;
    private final String zzbCo;
    private final String zzbCp;
    private final int zzbCq;
    private final String zzvi;

    static class zza {
        private static final String[] zzbCr = {HttpHost.DEFAULT_SCHEME_NAME, "https", "ftp"};
        private final String zzajd;
        private final String zzbCo;
        private final Uri zzbCs;
        private final URI zzbCt;
        private final Boolean zzbCu;
        private final Integer zzbCv;

        private zza(String str) {
            int port;
            this.zzajd = str;
            this.zzbCs = Uri.parse(this.zzajd);
            try {
                new URI(this.zzajd);
            } catch (URISyntaxException e) {
            } finally {
                this.zzbCt = null;
            }
            this.zzbCo = getScheme();
            this.zzbCu = Boolean.valueOf(zzAn());
            if (this.zzbCv != null) {
                port = this.zzbCv.intValue();
            } else if (this.zzbCs == null || (port = this.zzbCs.getPort()) == -1) {
                port = -1;
            }
            this.zzbCv = Integer.valueOf(port);
        }

        /* JADX INFO: Access modifiers changed from: private */
        public final int getPort() {
            return this.zzbCv.intValue();
        }

        /* JADX INFO: Access modifiers changed from: private */
        public final String getScheme() {
            if (this.zzbCo != null) {
                return this.zzbCo;
            }
            String scheme = this.zzbCs != null ? this.zzbCs.getScheme() : null;
            TextUtils.isEmpty(scheme);
            if (TextUtils.isEmpty(scheme) && !TextUtils.isEmpty(this.zzajd)) {
                int iIndexOf = this.zzajd.indexOf(":");
                if (iIndexOf != -1) {
                    String lowerCase = this.zzajd.substring(0, iIndexOf).toLowerCase(Locale.US);
                    if (zzeU(lowerCase)) {
                        scheme = lowerCase;
                    }
                }
                if (TextUtils.isEmpty(scheme)) {
                    this.zzajd.startsWith("www.");
                    scheme = HttpHost.DEFAULT_SCHEME_NAME;
                }
            }
            if (scheme != null) {
                return scheme.toLowerCase(Locale.US);
            }
            return null;
        }

        /* JADX INFO: Access modifiers changed from: private */
        public final boolean zzAn() {
            return this.zzbCu != null ? this.zzbCu.booleanValue() : zzeU(this.zzbCo);
        }

        private static boolean zzeU(String str) {
            if (TextUtils.isEmpty(str)) {
                return false;
            }
            for (int i = 0; i < zzbCr.length; i++) {
                if (zzbCr[i].equals(str)) {
                    return true;
                }
            }
            return false;
        }
    }

    public zzcss(String str) {
        String strSubstring;
        String lowerCase;
        if (!(!TextUtils.isEmpty(str))) {
            this.zzD = null;
            this.zzbCo = null;
            this.zzbCp = null;
            this.zzbCq = -1;
            this.mPath = null;
            this.zzvi = null;
            return;
        }
        String strReplaceAll = str.replaceAll("^\\s+", "").replaceAll("\\s+$", "").replaceAll("[\\t\\n\\r]", "");
        int iIndexOf = strReplaceAll.indexOf(35);
        strReplaceAll = iIndexOf != -1 ? strReplaceAll.substring(0, iIndexOf) : strReplaceAll;
        zza zzaVar = new zza(strReplaceAll);
        if (!zzaVar.zzAn()) {
            this.zzD = null;
            this.zzbCo = null;
            this.zzbCp = null;
            this.zzbCq = -1;
            this.mPath = null;
            this.zzvi = null;
            return;
        }
        this.zzbCo = zzaVar.getScheme();
        this.zzbCq = zzaVar.getPort();
        if (this.zzbCo != null) {
            String str2 = this.zzbCo;
            strReplaceAll = strReplaceAll.replaceAll(new StringBuilder(String.valueOf(str2).length() + 2).append("^").append(str2).append(":").toString(), "");
        }
        String strZzeS = zzeS(strReplaceAll.replaceAll("^/+", ""));
        int iIndexOf2 = strZzeS.indexOf(63);
        if (iIndexOf2 != -1) {
            int i = iIndexOf2 + 1;
            strSubstring = i < strZzeS.length() ? strZzeS.substring(i) : "";
            strZzeS = strZzeS.substring(0, iIndexOf2);
        } else {
            strSubstring = null;
        }
        if (TextUtils.isEmpty(strZzeS)) {
            lowerCase = null;
        } else {
            int iIndexOf3 = strZzeS.indexOf(47);
            String strSubstring2 = iIndexOf3 != -1 ? strZzeS.substring(0, iIndexOf3) : strZzeS;
            int iIndexOf4 = strSubstring2.indexOf(64);
            strSubstring2 = iIndexOf4 != -1 ? strSubstring2.substring(iIndexOf4 + 1) : strSubstring2;
            String strReplaceAll2 = (this.zzbCq != -1 ? strSubstring2.replaceAll(new StringBuilder(13).append(":").append(this.zzbCq).append("$").toString(), "") : strSubstring2).replaceAll("^\\.*", "").replaceAll("\\.*$", "").replaceAll("\\.+", ".");
            String strZzeO = zzeO(strReplaceAll2);
            lowerCase = (strZzeO == null ? strReplaceAll2 : strZzeO).toLowerCase(Locale.getDefault());
        }
        if (TextUtils.isEmpty(lowerCase)) {
            this.zzD = null;
            this.zzbCp = null;
            this.mPath = null;
            this.zzvi = null;
            return;
        }
        String strZzeQ = zzeQ(strZzeS);
        this.zzbCp = zzeR(lowerCase);
        this.mPath = zzeR(strZzeQ);
        this.zzvi = TextUtils.isEmpty(strSubstring) ? strSubstring : zzeR(strSubstring);
        this.zzD = strZzeS;
    }

    private static boolean isHexDigit(char c) {
        return (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f');
    }

    private final List<String> zzAl() {
        if (TextUtils.isEmpty(this.zzbCp)) {
            return null;
        }
        ArrayList arrayList = new ArrayList();
        char[] charArray = this.zzbCp.toCharArray();
        boolean z = false;
        for (int length = charArray.length - 2; length > 0 && arrayList.size() < 4; length--) {
            if (charArray[length] == '.') {
                if (z) {
                    arrayList.add(this.zzbCp.substring(length + 1));
                } else {
                    z = true;
                }
            }
        }
        arrayList.add(this.zzbCp);
        return arrayList;
    }

    private final List<String> zzAm() {
        if (TextUtils.isEmpty(this.mPath)) {
            return null;
        }
        ArrayList arrayList = new ArrayList();
        char[] charArray = this.mPath.toCharArray();
        for (int i = 0; i < charArray.length && arrayList.size() < 4; i++) {
            if (charArray[i] == '/') {
                arrayList.add(this.mPath.substring(0, i + 1));
            }
        }
        if (!arrayList.isEmpty() && !((String) arrayList.get(arrayList.size() - 1)).equals(this.mPath)) {
            arrayList.add(this.mPath);
        }
        if (!TextUtils.isEmpty(this.zzvi)) {
            String str = this.mPath;
            String str2 = this.zzvi;
            arrayList.add(new StringBuilder(String.valueOf(str).length() + 1 + String.valueOf(str2).length()).append(str).append("?").append(str2).toString());
        }
        return arrayList;
    }

    private static String zzeO(String str) {
        if (TextUtils.isEmpty(str)) {
            return null;
        }
        String strReplaceAll = str.replaceAll("^\\[", "").replaceAll("\\]$", "");
        if (zzcsq.zzeI(strReplaceAll)) {
            String strZza = zzcsq.zza(zzcsq.zzeN(strReplaceAll));
            return strZza.contains(":") ? String.format("[%s]", strZza) : strZza;
        }
        if (TextUtils.isDigitsOnly(str)) {
            String strZzeP = zzeP(str);
            if (TextUtils.isEmpty(strZzeP)) {
                return null;
            }
            return strZzeP;
        }
        if (!zzbCn.matcher(strReplaceAll).find()) {
            return null;
        }
        Matcher matcher = zzbCl.matcher(strReplaceAll);
        StringBuffer stringBuffer = new StringBuffer();
        while (matcher.find()) {
            matcher.appendReplacement(stringBuffer, new StringBuilder(11).append(Integer.parseInt(matcher.group(), 8)).toString());
        }
        matcher.appendTail(stringBuffer);
        Matcher matcher2 = zzbCm.matcher(stringBuffer.toString());
        StringBuffer stringBuffer2 = new StringBuffer();
        while (matcher2.find()) {
            matcher2.appendReplacement(stringBuffer2, new StringBuilder(11).append(Integer.parseInt(matcher2.group().substring(2), 16)).toString());
        }
        matcher2.appendTail(stringBuffer2);
        String string = stringBuffer2.toString();
        return string.contains(":") ? String.format("[%s]", string) : string;
    }

    private static String zzeP(String str) {
        byte[] bArrCopyOfRange;
        int i = 0;
        if (TextUtils.isEmpty(str)) {
            return null;
        }
        try {
            BigInteger bigInteger = new BigInteger(str);
            byte[] byteArray = bigInteger.toByteArray();
            if (byteArray.length < 4) {
                return null;
            }
            byte[] bArrCopyOfRange2 = Arrays.copyOfRange(byteArray, byteArray.length - 4, byteArray.length);
            if (bigInteger.equals(new BigInteger(new byte[]{0, bArrCopyOfRange2[0], bArrCopyOfRange2[1], bArrCopyOfRange2[2], bArrCopyOfRange2[3]}))) {
                return Inet4Address.getByAddress(bArrCopyOfRange2).getHostAddress();
            }
            if (byteArray.length >= 16) {
                bArrCopyOfRange = Arrays.copyOfRange(byteArray, byteArray.length - 16, byteArray.length);
            } else {
                bArrCopyOfRange = new byte[16];
                int length = 16 - byteArray.length;
                int i2 = 1;
                int i3 = 0;
                while (i2 <= length) {
                    bArrCopyOfRange[i3] = 0;
                    i2++;
                    i3++;
                }
                while (i < byteArray.length) {
                    int i4 = i3 + 1;
                    bArrCopyOfRange[i3] = byteArray[i];
                    i++;
                    i3 = i4;
                }
            }
            return String.format("[%s]", Inet6Address.getByAddress(bArrCopyOfRange).getHostAddress());
        } catch (ArrayIndexOutOfBoundsException e) {
            return null;
        } catch (NumberFormatException e2) {
            return null;
        } catch (UnknownHostException e3) {
            return null;
        }
    }

    private final String zzeQ(String str) {
        if (TextUtils.isEmpty(str)) {
            return null;
        }
        int iIndexOf = str.indexOf(47);
        String strReplaceAll = (iIndexOf != -1 ? str.substring(iIndexOf) : "/").replaceAll("/\\./", "/").replaceAll("/\\.$", "/");
        if (zzbCk.matcher(strReplaceAll).find()) {
            try {
                strReplaceAll = new URI(this.zzbCo, "host", strReplaceAll, null).normalize().getRawPath();
            } catch (URISyntaxException e) {
            }
        }
        return strReplaceAll.replaceAll("/+", "/");
    }

    private static String zzeR(String str) {
        if (TextUtils.isEmpty(str)) {
            return null;
        }
        try {
            byte[] bytes = str.getBytes(HTTP.UTF_8);
            StringBuilder sb = new StringBuilder();
            for (byte b : bytes) {
                char c = (char) (b & 255);
                if (c <= ' ' || c > '~' || c == '#' || c == '%') {
                    sb.append("%");
                    sb.append(zzbCj[c >>> 4]);
                    sb.append(zzbCj[c & 15]);
                } else {
                    sb.append(c);
                }
            }
            return sb.toString();
        } catch (UnsupportedEncodingException e) {
            return null;
        }
    }

    private static String zzeS(String str) {
        String str2;
        Object obj = null;
        if (TextUtils.isEmpty(str)) {
            return null;
        }
        int i = 0;
        while (true) {
            str2 = str;
            if (str2.equals(obj) || i >= 1024) {
                break;
            }
            str = zzeT(str2);
            i++;
            obj = str2;
        }
        return str2;
    }

    /* JADX WARN: Code duplicated, block: B:24:0x0088  */
    /* JADX WARN: Code duplicated, block: B:25:0x008a  */
    private static String zzeT(String str) {
        Byte bValueOf;
        try {
            byte[] bytes = str.replace("\\x", "%").getBytes(HTTP.UTF_8);
            ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream(bytes.length);
            int i = 0;
            while (i < bytes.length) {
                byte b = bytes[i];
                if (((char) (b & 255)) >= 128) {
                    byteArrayOutputStream.write(b);
                } else {
                    if (i + 2 >= bytes.length || ((char) (bytes[i] & 255)) != '%') {
                        bValueOf = null;
                    } else {
                        char c = (char) (bytes[i + 1] & 255);
                        char c2 = (char) (bytes[i + 2] & 255);
                        if (isHexDigit(c) && isHexDigit(c2)) {
                            bValueOf = Byte.valueOf((byte) ((Integer.parseInt(new StringBuilder(1).append(c).toString(), 16) << 4) + Integer.parseInt(new StringBuilder(1).append(c2).toString(), 16)));
                        } else {
                            bValueOf = null;
                        }
                    }
                    if (bValueOf != null) {
                        byteArrayOutputStream.write(bValueOf.byteValue());
                        i += 2;
                    } else {
                        byteArrayOutputStream.write(b);
                    }
                }
                i++;
            }
            try {
                return new String(byteArrayOutputStream.toByteArray(), HTTP.UTF_8);
            } catch (UnsupportedEncodingException e) {
                return null;
            }
        } catch (UnsupportedEncodingException e2) {
            return null;
        }
    }

    /* JADX WARN: Code duplicated, block: B:36:0x0088  */
    public final List<zzcsp> zzAk() throws NoSuchAlgorithmException {
        LinkedList<String> linkedList;
        MessageDigest messageDigest;
        List<String> listZzAl;
        List<String> listZzAm;
        if (TextUtils.isEmpty(this.zzD)) {
            linkedList = null;
        } else {
            String strZzeO = zzeO(this.zzbCp);
            if (strZzeO != null) {
                listZzAl = new ArrayList<>();
                listZzAl.add(strZzeO);
            } else {
                listZzAl = zzAl();
            }
            if (listZzAl == null || listZzAl.isEmpty() || (listZzAm = zzAm()) == null || listZzAm.isEmpty()) {
                linkedList = null;
            } else {
                LinkedList linkedList2 = new LinkedList();
                for (String str : listZzAl) {
                    for (String str2 : listZzAm) {
                        String strValueOf = String.valueOf(str);
                        String strValueOf2 = String.valueOf(str2);
                        linkedList2.add(strValueOf2.length() != 0 ? strValueOf.concat(strValueOf2) : new String(strValueOf));
                    }
                }
                if (linkedList2.isEmpty()) {
                    linkedList = null;
                } else {
                    linkedList = linkedList2;
                }
            }
        }
        if (linkedList == null || linkedList.isEmpty()) {
            return null;
        }
        try {
            messageDigest = MessageDigest.getInstance("SHA-256");
        } catch (NoSuchAlgorithmException e) {
            messageDigest = null;
        }
        if (messageDigest == null) {
            return null;
        }
        ArrayList arrayList = new ArrayList(linkedList.size());
        for (String str3 : linkedList) {
            if (!TextUtils.isEmpty(str3)) {
                try {
                    arrayList.add(new zzcsp(messageDigest.digest(str3.getBytes(HTTP.UTF_8))));
                } catch (UnsupportedEncodingException e2) {
                }
                messageDigest.reset();
            }
        }
        if (arrayList.isEmpty()) {
            return null;
        }
        return arrayList;
    }
}
