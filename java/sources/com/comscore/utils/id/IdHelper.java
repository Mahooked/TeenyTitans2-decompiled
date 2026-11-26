package com.comscore.utils.id;

import android.annotation.SuppressLint;
import android.content.Context;
import com.comscore.analytics.Core;
import com.comscore.android.id.DeviceId;
import com.comscore.android.id.IdHelperAndroid;
import com.comscore.applications.EventType;
import com.comscore.utils.Constants;
import com.comscore.utils.Storage;
import com.comscore.utils.Utils;
import java.util.HashMap;

/* JADX INFO: loaded from: classes.dex */
@SuppressLint({"NewApi"})
public class IdHelper {
    private Core a;
    private String b;
    private String c;
    private Storage d;
    private Context e;
    private String f;
    private String g;
    private String h;
    private boolean i = false;
    private boolean j = false;
    private boolean k = false;
    private Boolean l;
    private IdChecker m;

    public IdHelper(Context context, Storage storage, Core core) {
        this.e = context;
        this.d = storage;
        this.a = core;
        this.m = new IdChecker(this.e, storage);
    }

    private void a() {
        DeviceId advertisingDeviceId = IdHelperAndroid.getAdvertisingDeviceId(this.e);
        if (advertisingDeviceId == null) {
            this.i = true;
            a(null, false);
            return;
        }
        if (advertisingDeviceId.getCommonness() == 0 && advertisingDeviceId.getPersistency() == 0) {
            a(advertisingDeviceId.getId(), true);
            return;
        }
        if (!this.m.checkCrossPublisherId(this.d.get(Constants.MD5_RAW_CROSSPUBLISHER_ID_KEY))) {
            this.d.remove(Constants.MD5_RAW_CROSSPUBLISHER_ID_KEY);
            this.d.remove(Constants.CROSS_PUBLISHER_ID_KEY);
            this.b = null;
            this.h = null;
        }
        a(advertisingDeviceId.getId(), false);
    }

    private void a(String str, boolean z) {
        if (str == null) {
            this.b = null;
            this.h = null;
            return;
        }
        this.i = z;
        String str2 = this.d.get(Constants.MD5_RAW_CROSSPUBLISHER_ID_KEY);
        boolean z2 = this.h == null;
        this.h = Utils.md5(str);
        if (a(this.h)) {
            this.b = this.d.get(Constants.CROSS_PUBLISHER_ID_KEY);
            return;
        }
        if (this.i && str2 != null && !str2.isEmpty() && str2 != this.h) {
            this.j = true;
            this.k = z2;
        }
        this.b = b(str);
        this.d.set(Constants.CROSS_PUBLISHER_ID_KEY, this.b);
        this.d.set(Constants.MD5_RAW_CROSSPUBLISHER_ID_KEY, this.h);
    }

    private boolean a(String str) {
        String str2 = this.d.get(Constants.MD5_RAW_CROSSPUBLISHER_ID_KEY);
        return str2 != null && str2.equals(str);
    }

    private String b(String str) {
        if (str == null || str.length() <= 0) {
            return null;
        }
        try {
            return Utils.encrypt(str);
        } catch (Exception e) {
            return null;
        }
    }

    private void b() {
        if (this.d.has(Constants.VID_KEY).booleanValue()) {
            this.c = this.d.get(Constants.VID_KEY);
            if (this.m.checkVisitorId()) {
                return;
            }
            HashMap<String, String> map = new HashMap<>();
            map.put("cs_c12u", this.d.get(Constants.VID_KEY));
            this.a.notify(EventType.AGGREGATE, map, true);
            this.d.remove(Constants.VID_KEY);
            this.c = null;
        }
        DeviceId deviceId = IdHelperAndroid.getDeviceId(this.e);
        String id = deviceId.getId();
        String str = "-cs" + deviceId.getSuffix();
        if (this.c == null) {
            this.c = Utils.md5(id + getPublisherSecret()) + str;
            this.d.set(Constants.VID_KEY, this.c);
        }
    }

    private boolean c() {
        boolean zIsAdvertisingIdEnabled = IdHelperAndroid.isAdvertisingIdEnabled(this.e);
        if (this.l == null) {
            this.l = Boolean.valueOf(zIsAdvertisingIdEnabled);
        } else if (this.l.booleanValue() != zIsAdvertisingIdEnabled) {
            a();
        }
        return zIsAdvertisingIdEnabled;
    }

    public void generateIds() {
        if (isPublisherSecretEmpty()) {
            return;
        }
        this.g = IdHelperAndroid.getDeviceId(this.e).getId();
        b();
        a();
    }

    public String getAndroidId() {
        return this.g;
    }

    public String getCrossPublisherId() {
        if (!this.i) {
            return this.b;
        }
        if (!c()) {
            return IdHelperAndroid.NO_ID_AVAILABLE;
        }
        a();
        return (!this.j || this.k) ? this.b : IdHelperAndroid.NO_ID_AVAILABLE;
    }

    public String getMD5AdvertisingId() {
        if (this.i && c()) {
            return this.h;
        }
        return null;
    }

    public String getPublisherSecret() {
        if (this.f == null) {
            this.f = "";
        }
        return this.f;
    }

    public String getVisitorId() {
        return this.c;
    }

    public boolean isIdChanged() {
        return this.j;
    }

    public boolean isPublisherSecretEmpty() {
        return this.f == null || this.f.length() == 0;
    }

    public void resetVisitorId() {
        this.c = null;
        if (this.d.has(Constants.VID_KEY).booleanValue()) {
            this.d.remove(Constants.VID_KEY);
            generateIds();
        }
    }

    public void setPublisherSecret(String str) {
        this.f = str;
    }
}
