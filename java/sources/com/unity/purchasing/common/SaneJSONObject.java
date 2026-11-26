package com.unity.purchasing.common;

import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
public class SaneJSONObject extends JSONObject {
    @Override // org.json.JSONObject
    public JSONObject put(String name, double value) {
        try {
            return super.put(name, value);
        } catch (JSONException j) {
            throw new RuntimeException(j);
        }
    }

    @Override // org.json.JSONObject
    public JSONObject put(String name, Object value) {
        try {
            return super.put(name, value);
        } catch (JSONException j) {
            throw new RuntimeException(j);
        }
    }

    @Override // org.json.JSONObject
    public JSONObject put(String name, boolean value) {
        try {
            return super.put(name, value);
        } catch (JSONException j) {
            throw new RuntimeException(j);
        }
    }
}
