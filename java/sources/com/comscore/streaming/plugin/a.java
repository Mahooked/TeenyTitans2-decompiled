package com.comscore.streaming.plugin;

import com.comscore.streaming.StreamSenseEventType;
import com.comscore.streaming.StreamSenseState;
import java.util.HashMap;
import java.util.Iterator;

/* JADX INFO: loaded from: classes.dex */
class a implements Runnable {
    final /* synthetic */ StreamSensePlugin a;

    a(StreamSensePlugin streamSensePlugin) {
        this.a = streamSensePlugin;
    }

    /* JADX WARN: Code duplicated, block: B:24:0x00de  */
    @Override // java.lang.Runnable
    public void run() {
        boolean zF;
        if (!this.a.t || this.a.u == null) {
            this.a.c();
            return;
        }
        if (this.a.w) {
            this.a.w = false;
            return;
        }
        long position = this.a.u.getPosition();
        StreamSenseState state = this.a.getState();
        long jCurrentTimeMillis = System.currentTimeMillis();
        if (this.a.k.isEmpty() || position != this.a.a(0)) {
            this.a.k.add(Long.valueOf(Math.abs(position)));
            this.a.l.add(Long.valueOf(jCurrentTimeMillis));
            if (this.a.k.size() > 1 && this.a.a(0) < this.a.a(1)) {
                long jA = this.a.a(0);
                long jB = this.a.b(0);
                this.a.k.clear();
                this.a.l.clear();
                this.a.k.add(Long.valueOf(jA));
                this.a.l.add(Long.valueOf(jB));
                zF = this.a.p;
                if (this.a.k.size() > this.a.i) {
                    this.a.k.remove(0);
                    this.a.l.remove(0);
                }
                if (this.a.p && !zF) {
                    zF = this.a.f();
                }
            } else if (this.a.k.size() < this.a.g) {
                return;
            }
            if (this.a.k.size() > this.a.i) {
                this.a.k.remove(0);
                this.a.l.remove(0);
            }
            if (this.a.p) {
                zF = this.a.f();
            }
        } else {
            this.a.l.set(this.a.l.size() - 1, Long.valueOf(jCurrentTimeMillis));
            zF = false;
        }
        switch (this.a.getState()) {
            case BUFFERING:
            case IDLE:
            case PAUSED:
                if (this.a.r && position > this.a.m && !zF && !this.a.a(position)) {
                    Iterator it = this.a.o.iterator();
                    while (it.hasNext() && ((StreamSensePluginListener) it.next()).onPreStateChange(this.a.getState(), StreamSenseEventType.PLAY, this.a.n)) {
                    }
                    if (!this.a.n || this.a.v) {
                        this.a.notify(StreamSenseEventType.PLAY, null, ((Long) this.a.k.get(0)).longValue());
                    } else {
                        HashMap<String, String> map = new HashMap<>();
                        map.put("ns_st_ui", "seek");
                        this.a.notify(StreamSenseEventType.PLAY, map, position);
                    }
                    this.a.n = false;
                    this.a.v = false;
                } else if (this.a.p && zF) {
                    this.a.n = true;
                }
                break;
            case PLAYING:
                if (this.a.p && zF) {
                    Iterator it2 = this.a.o.iterator();
                    while (it2.hasNext() && ((StreamSensePluginListener) it2.next()).onPreStateChange(this.a.getState(), StreamSenseEventType.PAUSE, false)) {
                    }
                    this.a.notify(StreamSenseEventType.PAUSE, null, this.a.m);
                    this.a.n = true;
                } else if (this.a.s && this.a.a(position)) {
                    Iterator it3 = this.a.o.iterator();
                    while (it3.hasNext() && ((StreamSensePluginListener) it3.next()).onPreStateChange(this.a.getState(), StreamSenseEventType.END, false)) {
                    }
                    this.a.notify(StreamSenseEventType.END, null, this.a.u.getDuration());
                    this.a.n = false;
                } else if (this.a.q && Math.abs(position - this.a.m) <= this.a.c) {
                    Iterator it4 = this.a.o.iterator();
                    while (it4.hasNext() && ((StreamSensePluginListener) it4.next()).onPreStateChange(this.a.getState(), StreamSenseEventType.PAUSE, false)) {
                    }
                    this.a.notify(StreamSenseEventType.PAUSE, null, this.a.m);
                }
                break;
        }
        if (state != this.a.getState()) {
            Iterator it5 = this.a.o.iterator();
            while (it5.hasNext()) {
                ((StreamSensePluginListener) it5.next()).onPostStateChange(this.a.getState());
            }
            if (this.a.getState() == StreamSenseState.PAUSED) {
                this.a.k.clear();
                this.a.l.clear();
            }
        }
        this.a.m = position;
    }
}
