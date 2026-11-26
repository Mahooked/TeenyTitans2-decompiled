package com.unity.purchasing.amazon;

import android.content.Context;
import com.amazon.device.iap.PurchasingListener;
import com.amazon.device.iap.PurchasingService;
import com.amazon.device.iap.model.FulfillmentResult;
import com.amazon.device.iap.model.RequestId;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
public class AmazonPurchasingService implements IPurchasingService {
    private Context context;

    public AmazonPurchasingService(Context context) {
        this.context = context;
    }

    @Override // com.unity.purchasing.amazon.IPurchasingService
    public void registerListener(PurchasingListener listener) {
        PurchasingService.registerListener(this.context, listener);
    }

    @Override // com.unity.purchasing.amazon.IPurchasingService
    public RequestId getUserData() {
        return PurchasingService.getUserData();
    }

    @Override // com.unity.purchasing.amazon.IPurchasingService
    public RequestId purchase(String sku) {
        return PurchasingService.purchase(sku);
    }

    @Override // com.unity.purchasing.amazon.IPurchasingService
    public RequestId getProductData(Set<String> skus) {
        return PurchasingService.getProductData(skus);
    }

    @Override // com.unity.purchasing.amazon.IPurchasingService
    public RequestId getPurchaseUpdates(boolean restoreAll) {
        return PurchasingService.getPurchaseUpdates(restoreAll);
    }

    @Override // com.unity.purchasing.amazon.IPurchasingService
    public void notifyFulfillment(String transactionID, FulfillmentResult result) {
        PurchasingService.notifyFulfillment(transactionID, result);
    }
}
