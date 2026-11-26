package com.comscore.streaming.plugin;

import android.util.Log;
import com.comscore.streaming.StreamSense;
import com.comscore.streaming.StreamSenseClip;
import com.comscore.streaming.StreamSenseEventType;
import com.fasterxml.jackson.core.util.MinimalPrettyPrinter;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import org.apache.http.HttpStatus;

/* JADX INFO: loaded from: classes.dex */
public class StreamSensePlugin extends StreamSense {
    private static final float[][] e = {new float[]{-1.0f, 1.0f}, new float[]{-0.5f, 0.0f, 0.5f}, new float[]{-0.3f, -0.1f, 0.1f, 0.3f}, new float[]{-0.2f, -0.1f, 0.0f, 0.1f, 0.2f}, new float[]{-0.14286f, -0.08571f, -0.02857f, 0.02857f, 0.08571f, 0.14286f}, new float[]{-0.10714f, -0.07143f, -0.03571f, 0.0f, 0.03571f, 0.07143f, 0.10714f}, new float[]{-0.08333f, -0.05952f, -0.03571f, -0.0119f, 0.0119f, 0.03571f, 0.05952f, 0.08333f}, new float[]{-0.06667f, -0.05f, -0.03333f, -0.01667f, 0.0f, 0.01667f, 0.03333f, 0.05f, 0.06667f}, new float[]{-0.05455f, -0.04242f, -0.0303f, -0.01818f, -0.00606f, 0.00606f, 0.01818f, 0.0303f, 0.04242f, 0.05455f}, new float[]{-0.04545f, -0.03636f, -0.02727f, -0.01818f, -0.00909f, 0.0f, 0.00909f, 0.01818f, 0.02727f, 0.03636f, 0.04545f}, new float[]{-0.03846f, -0.03147f, -0.02448f, -0.01748f, -0.01049f, -0.0035f, 0.0035f, 0.01049f, 0.01748f, 0.02448f, 0.03147f, 0.03846f}, new float[]{-0.03297f, -0.02747f, -0.02198f, -0.01648f, -0.01099f, -0.00549f, 0.0f, 0.00549f, 0.01099f, 0.01648f, 0.02198f, 0.02747f, 0.03297f}};
    private Runnable j;
    private StreamSensePlayer u;
    private String b = "1.0.1";
    private int c = 10;
    private int d = 500;
    private float f = 1.25f;
    private int g = 2;
    private int h = HttpStatus.SC_MULTIPLE_CHOICES;
    private int i = 6;
    private List<Long> k = new ArrayList();
    private List<Long> l = new ArrayList();
    private long m = 0;
    private boolean n = false;
    private List<StreamSensePluginListener> o = new ArrayList();
    private boolean p = false;
    private boolean q = false;
    private boolean r = false;
    private boolean s = false;
    private boolean t = false;
    private boolean v = false;
    private boolean w = false;
    private boolean x = false;
    private HashMap<String, String> y = new HashMap<>();
    private HashMap<String, String> z = new HashMap<>();
    private HashMap<String, String> A = new HashMap<>();
    private boolean B = false;
    private boolean C = false;
    private boolean D = false;
    private final String E = "_all_";
    private String[] F = null;
    private int G = -1;
    private String[] H = null;
    private int I = -1;
    private HashMap<String, String> J = new HashMap<>();

    public StreamSensePlugin(HashMap<String, String> map, String str, String str2, String str3) {
        if (map != null && map.size() > 0) {
            b(map);
        }
        setLabel("ns_st_mp", str, true);
        setLabel("ns_st_pv", str2, true);
        setLabel("ns_st_mv", str3, true);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public long a(int i) {
        return this.k.get((this.k.size() - 1) - i).longValue();
    }

    private HashMap<String, String> a(HashMap<String, HashMap<String, String>> map, HashMap<String, String> map2) {
        HashMap<String, String> mapC;
        HashMap<String, String> map3 = (map2 == null || map2.size() <= 0) ? new HashMap<>() : new HashMap<>(map2);
        boolean z = this.H != null && this.H.length == 1 && this.H[0].equals("_all_");
        if (map.size() <= 0) {
            return map3;
        }
        if (z) {
            mapC = map3;
        } else if (map3.size() > 0) {
            map3.putAll(c(map.get("")));
            mapC = map3;
        } else {
            mapC = c(map.get(""));
        }
        for (String str : map.get("").keySet()) {
            boolean zFind = Pattern.compile("^([Cc][A-Da-d]_)?ns_st_.+").matcher(str).find();
            boolean zFind2 = Pattern.compile("^[Cc][A-Da-d]?([1-9]|1[0-9]|20)$").matcher(str).find();
            if (zFind || zFind2) {
                mapC.put(str, map.get("").get(str));
            }
        }
        return mapC;
    }

    private void a(String str) {
        String strReplace;
        if (str == null || str.length() == 0) {
            return;
        }
        Matcher matcher = Pattern.compile("([^=, ]+)\\s*=(\\s*('([^']+?)'|[a-z0-9\\._-]+)\\s*\\+?)+\\s*", 2).matcher(str);
        while (matcher.find()) {
            String[] strArrSplit = str.substring(matcher.start(), matcher.end()).split("=", 2);
            if (strArrSplit != null && strArrSplit.length == 2 && (strReplace = strArrSplit[0].replace(MinimalPrettyPrinter.DEFAULT_ROOT_VALUE_SEPARATOR, "")) != null && strReplace.length() > 0) {
                this.J.put(strReplace, strArrSplit[1]);
            }
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public boolean a(long j) {
        return Long.parseLong(getClip().getLabel("ns_st_cl")) > 0 && (j > Long.parseLong(getClip().getLabel("ns_st_cl")) || Math.abs(j - Long.parseLong(getClip().getLabel("ns_st_cl"))) < ((long) this.d));
    }

    /* JADX INFO: Access modifiers changed from: private */
    public long b(int i) {
        return this.l.get((this.l.size() - 1) - i).longValue();
    }

    private HashMap<String, String> b(HashMap<String, HashMap<String, String>> map, HashMap<String, String> map2) {
        String strReplaceFirst;
        String str;
        String str2;
        HashMap<String, String> map3 = (map2 == null || map2.size() <= 0) ? new HashMap<>() : new HashMap<>(map2);
        for (String str3 : this.J.keySet()) {
            String str4 = "";
            Matcher matcher = Pattern.compile("'([^']+?)'|[a-z0-9\\._-]+\\s*", 2).matcher(this.J.get(str3));
            while (matcher.find()) {
                String strReplace = this.J.get(str3).substring(matcher.start(), matcher.end()).replace(MinimalPrettyPrinter.DEFAULT_ROOT_VALUE_SEPARATOR, "");
                if (Pattern.compile("^'([^']+)'$", 2).matcher(strReplace).find()) {
                    strReplaceFirst = strReplace.replaceFirst("^'+", "").replaceFirst("'+$", "");
                } else {
                    try {
                        int iLastIndexOf = strReplace.lastIndexOf(46);
                        if (iLastIndexOf < 1 || iLastIndexOf >= strReplace.length() - 1) {
                            str = "";
                            str2 = strReplace;
                        } else {
                            String strSubstring = strReplace.substring(0, iLastIndexOf);
                            String strSubstring2 = strReplace.substring(iLastIndexOf + 1, strReplace.length());
                            str = strSubstring;
                            str2 = strSubstring2;
                        }
                        Iterator<String> it = map.keySet().iterator();
                        while (true) {
                            if (it.hasNext()) {
                                String next = it.next();
                                HashMap<String, String> map4 = map.get(next);
                                if (next.equals(str)) {
                                    if (map4 != null && map4.containsKey(str2)) {
                                        strReplaceFirst = map4.get(str2);
                                        break;
                                    }
                                }
                            }
                            strReplaceFirst = str4;
                            break;
                        }
                    } catch (Exception e2) {
                        log("Exception occurred while processing mapped labels");
                    }
                }
                map3.put(str3, strReplaceFirst);
                str4 = strReplaceFirst;
            }
        }
        return map3;
    }

    private void b(String str) {
        String strReplace;
        if (str == null || str.length() == 0) {
            return;
        }
        for (String str2 : str.split(",")) {
            String[] strArrSplit = str2.split("=", 2);
            if (strArrSplit.length == 2 && (strReplace = strArrSplit[0].replace(MinimalPrettyPrinter.DEFAULT_ROOT_VALUE_SEPARATOR, "")) != null && strReplace.length() > 0) {
                String strReplaceFirst = strArrSplit[1].replaceFirst("^'+", "").replaceFirst("'+$", "");
                setLabel(strReplace, strReplaceFirst);
                this.A.put(strReplace, strReplaceFirst);
            }
        }
    }

    private void b(HashMap<String, String> map) {
        String str;
        this.D = Boolean.valueOf(map.get("debug")).booleanValue();
        if (map.containsKey("labelmapping")) {
            a(map.get("labelmapping"));
        }
        if (map.containsKey("persistentlabels")) {
            b(map.get("persistentlabels"));
        }
        if (map.containsKey("throttling") && Boolean.valueOf(map.get("throttling")).booleanValue()) {
            setPausePlaySwitchDelayEnabled(true);
        } else {
            setPausePlaySwitchDelayEnabled(false);
        }
        if (map.containsKey("include")) {
            String str2 = map.get("include");
            if (str2 == "_all_") {
                this.F = new String[1];
                this.F[0] = "_all_";
            } else if (str2.length() > 0) {
                this.F = str2.split(",");
            }
        }
        if ((this.F == null || this.F.length == 0 || !this.F[0].equals("_all_")) && map.containsKey("include_prefixes") && (str = map.get("include_prefixes")) != null && str.length() > 0) {
            if (str.equals("_all_")) {
                this.F = new String[1];
                this.F[0] = "_all_";
            } else {
                if (this.F == null) {
                    this.F = new String[0];
                }
                this.G = this.F.length;
                ArrayList arrayList = new ArrayList(Arrays.asList(this.F));
                arrayList.addAll(Arrays.asList(str.split(",")));
                this.F = new String[arrayList.size()];
                this.F = (String[]) arrayList.toArray(this.F);
            }
        }
        if (this.F == null) {
            this.H = new String[1];
            this.H[0] = "_all_";
            return;
        }
        if (map.containsKey("exclude")) {
            String str3 = map.get("exclude");
            if (str3.equals("_all_")) {
                this.H = new String[1];
                this.H[0] = "_all_";
            } else if (str3.length() > 0) {
                this.H = str3.split(",");
            }
        }
        if ((this.H == null || this.H.length == 0 || !this.H[0].equals("_all_")) && map.containsKey("exclude_prefixes")) {
            String str4 = map.get("exclude_prefixes");
            if (str4.equals("_all_")) {
                this.H = new String[1];
                this.H[0] = "_all_";
                return;
            }
            if (this.H == null) {
                this.H = new String[0];
            }
            this.I = this.H.length;
            ArrayList arrayList2 = new ArrayList(Arrays.asList(this.H));
            arrayList2.addAll(Arrays.asList(str4.split(",")));
            this.H = new String[arrayList2.size()];
            this.H = (String[]) arrayList2.toArray(this.H);
        }
    }

    /* JADX WARN: Code restructure failed: missing block: B:41:0x0090, code lost:
    
        r5 = true;
     */
    /*
        Code decompiled incorrectly, please refer to instructions dump.
        To view partially-correct add '--show-bad-code' argument
    */
    private java.util.HashMap<java.lang.String, java.lang.String> c(java.util.HashMap<java.lang.String, java.lang.String> r12) {
        /*
            Method dump skipped, instruction units count: 316
            To view this dump add '--comments-level debug' option
        */
        throw new UnsupportedOperationException("Method not decompiled: com.comscore.streaming.plugin.StreamSensePlugin.c(java.util.HashMap):java.util.HashMap");
    }

    /* JADX INFO: Access modifiers changed from: private */
    public void c() {
        if (this.j != null) {
            this.a.getTaskExecutor().removeEnqueuedTask(this.j);
            this.j = null;
        }
    }

    private void d() {
        c();
        if (!this.t || this.u == null) {
            return;
        }
        this.j = e();
        this.a.getTaskExecutor().execute(this.j, 0L, true, this.h);
    }

    private Runnable e() {
        return new a(this);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public boolean f() {
        if (a(0) < a(1)) {
            return true;
        }
        int i = this.h;
        float fLongValue = 0.0f;
        for (int i2 = 0; i2 < this.k.size(); i2++) {
            fLongValue += this.k.get(i2).longValue() * e[this.k.size() - 2][i2];
        }
        return fLongValue / ((float) i) > this.f;
    }

    public void addListener(StreamSensePluginListener streamSensePluginListener) {
        this.o.add(streamSensePluginListener);
    }

    public void clearAllListeners() {
        this.o.clear();
    }

    public void clearListener(StreamSensePluginListener streamSensePluginListener) {
        this.o.remove(streamSensePluginListener);
    }

    public String getGenericPluginVersion() {
        return this.b;
    }

    public StreamSensePlayer getPlayer() {
        return this.u;
    }

    public void log(String str) {
        if (this.D) {
            Log.d(getClass().getSimpleName(), str);
        }
    }

    @Override // com.comscore.streaming.StreamSense
    public void notify(StreamSenseEventType streamSenseEventType, HashMap<String, String> map, long j) {
        long position;
        if (this.B || this.C) {
            return;
        }
        if (map == null) {
            map = new HashMap<>();
        }
        Iterator<StreamSensePluginListener> it = this.o.iterator();
        while (it.hasNext()) {
            it.next().onGetLabels(streamSenseEventType, map);
        }
        if (j > 0) {
            position = j;
        } else {
            position = this.u != null ? this.u.getPosition() : 0L;
        }
        if (position < 0) {
            position = 0;
        }
        if (streamSenseEventType == StreamSenseEventType.END && this.t) {
            this.k = new ArrayList();
            this.l = new ArrayList();
            this.n = false;
            this.v = true;
            this.w = true;
        } else if (streamSenseEventType == StreamSenseEventType.PLAY) {
            this.x = true;
        }
        super.notify(streamSenseEventType, map, position);
    }

    public void setBitRate(long j) {
        setLabel("ns_st_br", j > 0 ? String.valueOf(j) : "0");
    }

    public Boolean setClip(HashMap<String, String> map, boolean z, HashMap<String, HashMap<String, String>> map2) {
        return setClip(map, z, map2, false);
    }

    public Boolean setClip(HashMap<String, String> map, boolean z, HashMap<String, HashMap<String, String>> map2, boolean z2) {
        if (this.x && map != null && map.get("ns_st_ci") != getClip().getLabel("ns_st_ci")) {
            notify(StreamSenseEventType.END, this.u.getPosition());
            this.x = false;
        }
        if (map2 != null && map2.size() > 0) {
            map = b(map2, new HashMap<>(a(map2, new HashMap<>(map))));
        }
        if (!z2) {
            this.z = new HashMap<>();
            if (map != null && map.size() > 0) {
                for (String str : map.keySet()) {
                    this.z.put(str, map.get(str));
                }
            }
        } else if (this.z != null && this.z.size() > 0) {
            for (String str2 : this.z.keySet()) {
                map.put(str2, this.z.get(str2));
            }
        }
        if (map == null || map.get("ns_st_skip") == null || !Boolean.parseBoolean(map.get("ns_st_skip"))) {
            this.B = false;
        } else {
            this.B = true;
        }
        return super.setClip(map, z);
    }

    public void setClipLabel(String str, String str2) {
        setClipLabel(str, str2, false);
    }

    public void setClipLabel(String str, String str2, boolean z) {
        if (!z) {
            this.z.put(str, str2);
            getClip().setLabel(str, str2);
        } else if (this.z.get(str) == null && this.A.get(str) == null) {
            getClip().setLabel(str, str2);
        }
    }

    public void setDetectEnd(boolean z) {
        this.s = z;
    }

    public void setDetectPause(boolean z) {
        this.q = z;
    }

    public void setDetectPlay(boolean z) {
        this.r = z;
    }

    public void setDetectSeek(boolean z) {
        this.p = z;
    }

    public void setDuration(long j) {
        getClip().setLabel("ns_st_cl", j >= 0 ? String.valueOf(j) : "0");
    }

    public void setEndDetectionErrorMargin(int i) {
        this.d = i;
    }

    public void setIsFullScreen(boolean z) {
        setLabel("ns_st_ws", z ? "full" : "norm");
    }

    @Override // com.comscore.streaming.StreamSense
    public void setLabel(String str, String str2) {
        setLabel(str, str2, false);
    }

    public void setLabel(String str, String str2, boolean z) {
        HashMap<String, String> map = new HashMap<>();
        map.put(str, str2);
        setLabels(map, z);
    }

    @Override // com.comscore.streaming.StreamSense
    public void setLabels(HashMap<String, String> map) {
        setLabels(map, false);
    }

    public void setLabels(HashMap<String, String> map, boolean z) {
        if (!z) {
            this.A = new HashMap<>();
            if (map != null && map.size() > 0) {
                for (String str : map.keySet()) {
                    this.A.put(str, map.get(str));
                }
            }
        } else if (this.A != null && this.A.size() > 0) {
            for (String str2 : this.A.keySet()) {
                map.put(str2, this.A.get(str2));
            }
        }
        super.setLabels(map);
    }

    public void setMaximumNumberOfEntriesInHistory(int i) {
        if (i < 2 || i > 13) {
            return;
        }
        this.i = i;
    }

    public void setMinimumNumberOfTimeUpdateEventsBeforeSensingAnything(int i) {
        if (i < 2 || i > 13) {
            return;
        }
        this.g = i;
    }

    public void setPauseDetectionErrorMargin(int i) {
        this.c = i;
    }

    public void setPlayer(StreamSensePlayer streamSensePlayer) {
        this.u = streamSensePlayer;
    }

    @Override // com.comscore.streaming.StreamSense
    public Boolean setPlaylist(HashMap<String, String> map) {
        return setPlaylist(map, false);
    }

    public Boolean setPlaylist(HashMap<String, String> map, boolean z) {
        if (!z) {
            this.y = new HashMap<>();
            if (map != null && map.size() > 0) {
                for (String str : map.keySet()) {
                    this.y.put(str, map.get(str));
                }
            }
        } else if (this.y != null && this.y.size() > 0) {
            for (String str2 : this.y.keySet()) {
                map.put(str2, this.y.get(str2));
            }
        }
        if (map == null || map.get("ns_st_skip") == null || !Boolean.parseBoolean(map.get("ns_st_skip"))) {
            this.C = false;
        } else {
            this.C = true;
        }
        return super.setPlaylist(map);
    }

    public void setPlaylistLabel(String str, String str2) {
        setPlaylistLabel(str, str2, false);
    }

    public void setPlaylistLabel(String str, String str2, boolean z) {
        if (!z || !z) {
            this.y.put(str, str2);
            getPlaylist().setLabel(str, str2);
        } else if (this.y.get(str) == null && this.A.get(str) == null) {
            getPlaylist().setLabel(str, str2);
        }
    }

    public void setPulseSamplingInterval(int i) {
        if (i > 0) {
            this.h = i;
        }
    }

    public void setSeekDetectionMinQuotient(float f) {
        if (f > 1.0f) {
            this.f = f;
        }
    }

    public void setSmartStateDetection(boolean z) {
        boolean z2 = this.t;
        this.t = z;
        if (!z2 && z) {
            d();
        } else {
            if (!z2 || z) {
                return;
            }
            c();
        }
    }

    public void setVideoSize(String str) {
        StreamSenseClip clip = getClip();
        if (str == null || str.length() <= 0) {
            str = "0";
        }
        clip.setLabel("ns_st_cs", str);
    }

    public void setVolume(int i) {
        setLabel("ns_st_vo", (i < 0 || i > 100) ? "100" : String.valueOf(i));
    }
}
