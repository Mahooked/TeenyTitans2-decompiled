package com.unity.purchasing.common;

import java.util.List;
import org.json.JSONArray;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
public class UnityPurchasing implements IStoreCallback {
    private IUnityCallback bridge;

    public UnityPurchasing(IUnityCallback bridge) {
        this.bridge = bridge;
    }

    @Override // com.unity.purchasing.common.IStoreCallback
    public void OnSetupFailed(InitializationFailureReason i) {
        this.bridge.OnSetupFailed(i.toString());
    }

    @Override // com.unity.purchasing.common.IStoreCallback
    public void OnProductsRetrieved(List<ProductDescription> products) {
        this.bridge.OnProductsRetrieved(SerialiseProducts(products));
    }

    @Override // com.unity.purchasing.common.IStoreCallback
    public void OnPurchaseSucceeded(String storeSpecificId, String receipt, String transactionIdentifier) {
        this.bridge.OnPurchaseSucceeded(storeSpecificId, receipt, transactionIdentifier);
    }

    @Override // com.unity.purchasing.common.IStoreCallback
    public void OnPurchaseFailed(PurchaseFailureDescription p) {
        this.bridge.OnPurchaseFailed(SerialisePurchaseFailure(p));
    }

    public static String SerialisePurchaseFailure(PurchaseFailureDescription p) {
        SaneJSONObject result = new SaneJSONObject();
        result.put("message", p.message);
        result.put("productId", p.productId);
        result.put("reason", p.reason.toString());
        return result.toString();
    }

    public static String SerialiseProducts(List<ProductDescription> products) {
        JSONArray result = new JSONArray();
        for (ProductDescription product : products) {
            result.put(SerialiseProduct(product));
        }
        return result.toString();
    }

    private static JSONObject SerialiseProduct(ProductDescription product) {
        SaneJSONObject result = new SaneJSONObject();
        result.put("metadata", SerialiseMetadata(product.metadata));
        result.put("receipt", product.receipt);
        result.put("storeSpecificId", product.storeSpecificId);
        result.put("transactionId", product.transactionId);
        return result;
    }

    private static JSONObject SerialiseMetadata(ProductMetadata metadata) {
        SaneJSONObject result = new SaneJSONObject();
        result.put("isoCurrencyCode", metadata.isoCurrencyCode);
        result.put("localizedDescription", metadata.localizedDescription);
        result.put("localizedPriceString", metadata.localizedPriceString);
        result.put("localizedPrice", metadata.localizedPrice == null ? 0.0d : metadata.localizedPrice.doubleValue());
        result.put("localizedTitle", metadata.localizedTitle);
        return result;
    }
}
