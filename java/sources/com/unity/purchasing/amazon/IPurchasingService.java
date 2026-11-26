package com.unity.purchasing.amazon;

import com.amazon.device.iap.PurchasingListener;
import com.amazon.device.iap.model.FulfillmentResult;
import com.amazon.device.iap.model.RequestId;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
public interface IPurchasingService {
    RequestId getProductData(Set<String> set);

    RequestId getPurchaseUpdates(boolean z);

    RequestId getUserData();

    void notifyFulfillment(String str, FulfillmentResult fulfillmentResult);

    RequestId purchase(String str);

    void registerListener(PurchasingListener purchasingListener);
}
