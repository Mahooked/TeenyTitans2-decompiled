package com.comscore.utils;

/* JADX INFO: loaded from: classes.dex */
public class XMLBuilder {
    private static StringBuilder a;
    private static int b = -1;
    private static int c = -1;
    private static final String[] d = {"c12", "c1", "ns_ap_an", "ns_ap_pn", "ns_ap_device", "ns_ak"};
    private static final String[] e = {"c12", "c1", "ns_ap_an", "ns_ap_pn", "ns_ap_device", "ns_ts", "ns_ak"};
    private static final long f = (1 << e.length) - 1;

    private static void a(String str, String str2) {
        c = -1;
        b = -1;
        int iIndexOf = 0;
        do {
            iIndexOf = str.indexOf(str2, iIndexOf);
            if (iIndexOf >= 0) {
                int length = str2.length() + iIndexOf;
                if ((iIndexOf == 0 || str.charAt(iIndexOf - 1) == '&') && length < str.length() && str.charAt(length) == '=') {
                    b = length + 1;
                    c = str.indexOf(38, b);
                    if (c == -1) {
                        c = str.length();
                        return;
                    }
                    return;
                }
                iIndexOf = length + 1;
            }
            if (iIndexOf < 0) {
                return;
            }
        } while (iIndexOf < str.length());
    }

    private static void a(String str, StringBuilder sb) {
        boolean z;
        long j = 0;
        a(str, "ns_ts");
        if (b == -1 || c <= b) {
            return;
        }
        sb.append("<event t=\"").append((CharSequence) str, b, c).append("\">");
        int i = 0;
        int i2 = 0;
        while (i < str.length()) {
            int iIndexOf = str.indexOf(38, i);
            int length = iIndexOf == -1 ? str.length() : iIndexOf;
            if (length > i && str.indexOf(61, i) > i) {
                if (j == f) {
                    z = true;
                    break;
                }
                int i3 = 0;
                while (true) {
                    if (i3 >= e.length) {
                        z = true;
                        break;
                    }
                    long j2 = 1 << i3;
                    if ((j & j2) == 0 && e[i3].regionMatches(0, str, i, e[i3].length())) {
                        z = false;
                        j |= j2;
                        break;
                    }
                    i3++;
                }
                if (z) {
                    if (i2 > 0) {
                        a.append('&');
                        sb.append('&');
                    }
                    a.append((CharSequence) str, i, length);
                    sb.append((CharSequence) str, i, length);
                    i2++;
                }
            }
            i = length + 1;
        }
        sb.append("</event>");
    }

    private static void a(StringBuilder sb) {
        sb.insert("md5=\"".length() + sb.indexOf("md5=\""), Utils.md5(a.toString()).toLowerCase());
    }

    private static void a(StringBuilder sb, String str, String str2) {
        sb.append("<events t=\"").append(String.valueOf(Date.unixTime())).append("\" ");
        for (int i = 0; i < d.length; i++) {
            b(sb, str, d[i]);
        }
        sb.append("dropped=\"").append(str2).append("\" md5=\"\">");
    }

    private static void a(String[] strArr, StringBuilder sb) {
        a = new StringBuilder();
        for (int i = 0; i < strArr.length; i++) {
            if (Utils.isNotEmpty(strArr[i])) {
                a(strArr[i], sb);
            }
        }
    }

    private static void b(StringBuilder sb, String str, String str2) {
        a(str, str2);
        if (b == -1 || c <= b) {
            return;
        }
        sb.append(str2).append("=\"").append((CharSequence) str, b, c).append("\" ");
    }

    public static synchronized String generateXMLRequestString(String[] strArr, String str) {
        StringBuilder sb;
        sb = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
        a(sb, strArr[0], str);
        a(strArr, sb);
        a(sb);
        sb.append("</events>");
        return sb.toString();
    }

    public static synchronized String getLabelFromEvent(String str, String str2) {
        a(str, str2);
        return (b == -1 || c <= b) ? null : str.substring(b, c);
    }
}
