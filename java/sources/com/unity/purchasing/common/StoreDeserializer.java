package com.unity.purchasing.common;

import com.adobe.mobile.TargetLocationRequest;
import java.util.ArrayList;
import java.util.List;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
public abstract class StoreDeserializer implements INativeStore, IStore {
    @Override // com.unity.purchasing.common.INativeStore
    public void RetrieveProducts(String json) {
        RetrieveProducts(DeserializeProducts(json));
    }

    @Override // com.unity.purchasing.common.INativeStore
    public void Purchase(String productJSON, String developerPayload) {
        Purchase(GetProductDefinition(productJSON), developerPayload);
    }

    @Override // com.unity.purchasing.common.INativeStore
    public void FinishTransaction(String productJSON, String transactionID) {
        FinishTransaction(GetProductDefinition(productJSON), transactionID);
    }

    public static List<ProductDefinition> DeserializeProducts(String json) {
        try {
            JSONArray array = new JSONArray(json);
            List<ProductDefinition> result = new ArrayList<>();
            for (int t = 0; t < array.length(); t++) {
                JSONObject o = array.getJSONObject(t);
                result.add(GetProductDefinition(o));
            }
            return result;
        } catch (JSONException j) {
            throw new RuntimeException(j);
        }
    }

    public static ProductDefinition GetProductDefinition(String json) {
        if (json == null) {
            return null;
        }
        try {
            JSONObject j = new JSONObject(json);
            return GetProductDefinition(j);
        } catch (JSONException r) {
            throw new RuntimeException(r);
        }
    }

    private static ProductDefinition GetProductDefinition(JSONObject j) {
        try {
            return new ProductDefinition(j.getString(TargetLocationRequest.TARGET_PARAMETER_ORDER_ID), j.getString("storeSpecificId"), ProductType.valueOf(j.getString("type")));
        } catch (JSONException e) {
            throw new RuntimeException(e);
        }
    }
}
