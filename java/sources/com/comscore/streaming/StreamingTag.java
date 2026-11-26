package com.comscore.streaming;

import com.comscore.utils.Date;
import com.comscore.utils.Utils;
import java.util.HashMap;

/* JADX INFO: loaded from: classes.dex */
public class StreamingTag {
    private String[] a = {"ns_st_ci", "c3", "c4", "c6", "ns_st_st", "ns_st_pu", "ns_st_pr", "ns_st_ep", "ns_st_sn", "ns_st_en", "ns_st_ct"};
    private int e = 0;
    private long c = 0;
    private long d = 0;
    private HashMap<String, String> f = null;
    private boolean h = false;
    private p g = p.None;
    private StreamSense b = new StreamSense();

    public StreamingTag() {
        this.b.setLabel("ns_st_it", "r");
    }

    private HashMap<String, String> a(HashMap<String, String> map) {
        HashMap<String, String> mapMapOfStrings = Utils.mapOfStrings(map);
        for (String str : this.a) {
            if (!mapMapOfStrings.containsKey(str)) {
                if (str == "ns_st_ci") {
                    mapMapOfStrings.put(str, "0");
                } else {
                    mapMapOfStrings.put(str, "*null");
                }
            }
        }
        return mapMapOfStrings;
    }

    private void a(long j) {
        if (this.b.getState() != StreamSenseState.IDLE && this.b.getState() != StreamSenseState.PAUSED) {
            this.b.notify(StreamSenseEventType.END, b(j));
        } else if (this.b.getState() == StreamSenseState.PAUSED) {
            this.b.notify(StreamSenseEventType.END, this.d);
        }
    }

    private void a(long j, HashMap<String, String> map) {
        a(j);
        this.e++;
        if (!map.containsKey("ns_st_cn")) {
            map.put("ns_st_cn", String.valueOf(this.e));
        }
        if (!map.containsKey("ns_st_pn")) {
            map.put("ns_st_pn", "1");
        }
        if (!map.containsKey("ns_st_tp")) {
            map.put("ns_st_tp", "0");
        }
        this.b.setClip(map);
        this.f = map;
        this.c = j;
        this.d = 0L;
        this.b.notify(StreamSenseEventType.PLAY, this.d);
    }

    private void a(HashMap<String, String> map, p pVar) {
        long jUnixTime = Date.unixTime();
        HashMap<String, String> mapA = a(Utils.mapOfStrings(map));
        if (this.g == p.None) {
            this.g = pVar;
        }
        if (this.h && this.g == pVar && c(mapA)) {
            this.b.getClip().setLabels(mapA);
            if (this.b.getState() != StreamSenseState.PLAYING) {
                this.c = jUnixTime;
                this.b.notify(StreamSenseEventType.PLAY, this.d);
            }
        } else {
            a(jUnixTime, mapA);
        }
        this.h = true;
        this.g = pVar;
    }

    private boolean a(String str, HashMap<String, String> map, HashMap<String, String> map2) {
        if (str != null) {
            if ((map2 != null) & (map != null)) {
                String str2 = map.get(str);
                String str3 = map2.get(str);
                if (str2 != null && str3 != null) {
                    return str2.equals(str3);
                }
            }
        }
        return false;
    }

    private long b(long j) {
        if (this.c <= 0 || j < this.c) {
            this.d = 0L;
        } else {
            this.d += j - this.c;
        }
        return this.d;
    }

    private void b(HashMap<String, String> map) {
        long jUnixTime = Date.unixTime();
        a(jUnixTime);
        this.e++;
        HashMap<String, String> mapA = a(Utils.mapOfStrings(map));
        if (!mapA.containsKey("ns_st_cn")) {
            mapA.put("ns_st_cn", String.valueOf(this.e));
        }
        if (!mapA.containsKey("ns_st_pn")) {
            mapA.put("ns_st_pn", "1");
        }
        if (!mapA.containsKey("ns_st_tp")) {
            mapA.put("ns_st_tp", "1");
        }
        if (!mapA.containsKey("ns_st_ad")) {
            mapA.put("ns_st_ad", "1");
        }
        this.b.setClip(mapA);
        this.d = 0L;
        this.b.notify(StreamSenseEventType.PLAY, this.d);
        this.c = jUnixTime;
        this.h = false;
    }

    private boolean c(HashMap<String, String> map) {
        HashMap<String, String> mapA = a(Utils.mapOfStrings(map));
        for (String str : this.a) {
            if (!a(str, this.f, mapA)) {
                return false;
            }
        }
        return true;
    }

    @Deprecated
    public void playAdvertisement() {
        HashMap<String, String> map = new HashMap<>();
        map.put("ns_st_ct", "va");
        b(map);
    }

    @Deprecated
    public void playAudioAdvertisement() {
        playAudioAdvertisement(new HashMap<>());
    }

    @Deprecated
    public void playAudioAdvertisement(HashMap<String, String> map) {
        playAudioAdvertisement(map, null);
    }

    public void playAudioAdvertisement(HashMap<String, String> map, AdType adType) {
        HashMap<String, String> map2 = new HashMap<>();
        if (map != null) {
            map2.putAll(map);
        }
        if (!map2.containsKey("ns_st_ct")) {
            map2.put("ns_st_ct", adType == null ? "aa" : "a" + adType.a());
        }
        b(map2);
    }

    @Deprecated
    public void playAudioContentPart(HashMap<String, String> map) {
        playAudioContentPart(map, null);
    }

    public void playAudioContentPart(HashMap<String, String> map, ContentType contentType) {
        HashMap<String, String> map2 = new HashMap<>();
        if (map != null) {
            map2.putAll(map);
        }
        if (!map2.containsKey("ns_st_ct")) {
            map2.put("ns_st_ct", contentType == null ? "ac" : "a" + contentType.a());
        }
        a(map2, p.AudioContent);
    }

    @Deprecated
    public void playContentPart(HashMap<String, String> map) {
        HashMap<String, String> map2 = new HashMap<>();
        if (map != null) {
            map2.putAll(map);
        }
        if (!map2.containsKey("ns_st_ct")) {
            map2.put("ns_st_ct", "vc");
        }
        a(map2, p.VideoContent);
    }

    @Deprecated
    public void playVideoAdvertisement() {
        playVideoAdvertisement(new HashMap<>());
    }

    @Deprecated
    public void playVideoAdvertisement(HashMap<String, String> map) {
        playVideoAdvertisement(map, null);
    }

    public void playVideoAdvertisement(HashMap<String, String> map, AdType adType) {
        HashMap<String, String> map2 = new HashMap<>();
        if (map != null) {
            map2.putAll(map);
        }
        if (!map2.containsKey("ns_st_ct")) {
            map2.put("ns_st_ct", adType == null ? "va" : "v" + adType.a());
        }
        b(map2);
    }

    @Deprecated
    public void playVideoContentPart(HashMap<String, String> map) {
        playVideoContentPart(map, null);
    }

    public void playVideoContentPart(HashMap<String, String> map, ContentType contentType) {
        HashMap<String, String> map2 = new HashMap<>();
        if (map != null) {
            map2.putAll(map);
        }
        if (!map2.containsKey("ns_st_ct")) {
            map2.put("ns_st_ct", contentType == null ? "vc" : "v" + contentType.a());
        }
        a(map2, p.VideoContent);
    }

    public void stop() {
        this.b.notify(StreamSenseEventType.PAUSE, b(Date.unixTime()));
    }
}
