package com.unity.purchasing.amazon;

import android.os.Environment;
import android.util.Log;
import com.amazon.device.iap.PurchasingListener;
import com.amazon.device.iap.PurchasingService;
import com.amazon.device.iap.model.FulfillmentResult;
import com.amazon.device.iap.model.Product;
import com.amazon.device.iap.model.ProductDataResponse;
import com.amazon.device.iap.model.PurchaseResponse;
import com.amazon.device.iap.model.PurchaseUpdatesResponse;
import com.amazon.device.iap.model.Receipt;
import com.amazon.device.iap.model.UserDataResponse;
import com.comscore.utils.Constants;
import com.unity.purchasing.common.DelayedHandler;
import com.unity.purchasing.common.IDelayedExecutor;
import com.unity.purchasing.common.IStoreCallback;
import com.unity.purchasing.common.IUnityCallback;
import com.unity.purchasing.common.InitializationFailureReason;
import com.unity.purchasing.common.ProductDefinition;
import com.unity.purchasing.common.ProductDescription;
import com.unity.purchasing.common.ProductMetadata;
import com.unity.purchasing.common.ProductType;
import com.unity.purchasing.common.PurchaseFailureDescription;
import com.unity.purchasing.common.PurchaseFailureReason;
import com.unity.purchasing.common.SaneJSONObject;
import com.unity.purchasing.common.StoreDeserializer;
import com.unity.purchasing.common.UnityPurchasing;
import com.unity3d.player.UnityPlayer;
import java.io.File;
import java.io.FileOutputStream;
import java.math.BigDecimal;
import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
public class AmazonPurchasing extends StoreDeserializer implements PurchasingListener {
    private static final String LOG_PREFIX = "Unity Purchasing Amazon";
    private static AmazonPurchasing instance;
    private String ISO3266CountryCode;
    private String PRODUCT_UNDER_PURCHASE;
    private IPurchasingService amazonPurchasing;
    private Map<String, Product> dataResponses;
    private long delayInMillis = 5000;
    private IDelayedExecutor handler;
    private LinkedList<Set<String>> itemGroups;
    private List<ProductDefinition> products;
    private Map<String, Receipt> receipts;
    private IStoreCallback unityPurchasing;
    private String userId;

    public static AmazonPurchasing instance(IUnityCallback bridge) {
        if (instance == null) {
            AmazonPurchasingService amazon = new AmazonPurchasingService(UnityPlayer.currentActivity);
            instance = new AmazonPurchasing(new UnityPurchasing(bridge), amazon, new DelayedHandler());
        }
        return instance;
    }

    public AmazonPurchasing(IStoreCallback callback, IPurchasingService amazonPurchasing, IDelayedExecutor handler) {
        this.unityPurchasing = callback;
        this.amazonPurchasing = amazonPurchasing;
        this.handler = handler;
        amazonPurchasing.registerListener(this);
    }

    public String getAmazonUserId() {
        return this.userId;
    }

    public void writeSandboxJSON(String productJSON) {
        String sandboxJSON = buildSandboxJSON(StoreDeserializer.DeserializeProducts(productJSON));
        try {
            File file = new File(Environment.getExternalStorageDirectory(), "amazon.sdktester.json");
            FileOutputStream outputStream = new FileOutputStream(file);
            outputStream.write(sandboxJSON.getBytes());
            outputStream.close();
        } catch (Exception e) {
            e.printStackTrace();
            Log.d(LOG_PREFIX, "Unable to write sandbox JSON! Ensure your manifest has WRITE_EXTERNAL permission");
        }
    }

    public void notifyUnableToFulfillUnavailableProduct(String transactionID) {
        this.amazonPurchasing.notifyFulfillment(transactionID, FulfillmentResult.UNAVAILABLE);
    }

    public static String buildSandboxJSON(List<ProductDefinition> products) {
        String str;
        SaneJSONObject result = new SaneJSONObject();
        for (ProductDefinition product : products) {
            SaneJSONObject hash = new SaneJSONObject();
            result.put(product.storeSpecificId, hash);
            if (product.type == ProductType.Consumable) {
                str = "CONSUMABLE";
            } else {
                str = product.type == ProductType.NonConsumable ? "ENTITLED" : "SUBSCRIPTION";
            }
            hash.put("itemType", str);
            hash.put("title", "Fake title for " + product.id);
            hash.put("description", "Fake description for " + product.id);
            hash.put("price", "0.99");
            hash.put("smallIconUrl", "http://example.com");
            if (ProductType.Subscription == product.type) {
                hash.put("subscriptionParent", "does.not.exist");
            }
        }
        return result.toString();
    }

    @Override // com.amazon.device.iap.PurchasingListener
    public void onProductDataResponse(ProductDataResponse response) {
        Log.d(LOG_PREFIX, "onItemDataResponse");
        switch (response.getRequestStatus()) {
            case NOT_SUPPORTED:
                this.unityPurchasing.OnSetupFailed(InitializationFailureReason.PurchasingUnavailable);
                break;
            case FAILED:
                Retry(new Runnable() { // from class: com.unity.purchasing.amazon.AmazonPurchasing.1
                    @Override // java.lang.Runnable
                    public void run() {
                        AmazonPurchasing.this.amazonPurchasing.getProductData((Set) AmazonPurchasing.this.itemGroups.peek());
                    }
                });
                break;
            case SUCCESSFUL:
                ResetDelay();
                this.dataResponses.putAll(response.getProductData());
                this.itemGroups.remove();
                if (this.itemGroups.isEmpty()) {
                    initiatePurchaseUpdate(true);
                } else {
                    this.amazonPurchasing.getProductData(this.itemGroups.peek());
                }
                break;
        }
    }

    @Override // com.amazon.device.iap.PurchasingListener
    public void onPurchaseResponse(PurchaseResponse purchaseResponse) {
        Log.d(LOG_PREFIX, "onPurchaseResponse");
        String sku = this.PRODUCT_UNDER_PURCHASE;
        if (purchaseResponse.getReceipt() != null) {
            sku = purchaseResponse.getReceipt().getSku();
        }
        Log.i(LOG_PREFIX, purchaseResponse.getRequestStatus().toString());
        switch (purchaseResponse.getRequestStatus()) {
            case ALREADY_PURCHASED:
            case SUCCESSFUL:
                Receipt r = purchaseResponse.getReceipt();
                if (r == null && this.receipts.containsKey(sku)) {
                    r = this.receipts.get(sku);
                }
                if (r != null) {
                    OnPurchaseSucceeded(r);
                } else {
                    this.unityPurchasing.OnPurchaseSucceeded(sku, encodeReceipt(null), "");
                }
                break;
            default:
                PurchaseFailureReason reason = convertAmazonRequestStatustoFailureReason(purchaseResponse.getRequestStatus());
                PurchaseFailureDescription description = new PurchaseFailureDescription(sku, reason, null);
                this.unityPurchasing.OnPurchaseFailed(description);
                break;
        }
    }

    private PurchaseFailureReason convertAmazonRequestStatustoFailureReason(PurchaseResponse.RequestStatus status) {
        switch (status) {
            case INVALID_SKU:
                return PurchaseFailureReason.ItemUnavailable;
            case NOT_SUPPORTED:
                return PurchaseFailureReason.BillingUnavailable;
            default:
                return PurchaseFailureReason.Unknown;
        }
    }

    private String encodeReceipt(Receipt receipt) {
        SaneJSONObject receiptBundle = new SaneJSONObject();
        String receiptId = receipt == null ? "" : receipt.getReceiptId();
        receiptBundle.put("receiptId", receiptId);
        receiptBundle.put("userId", this.userId);
        receiptBundle.put("isSandbox", PurchasingService.IS_SANDBOX_MODE);
        return receiptBundle.toString();
    }

    private void OnPurchaseSucceeded(Receipt receipt) {
        if (receipt == null) {
            Log.d(LOG_PREFIX, "OnPurchaseSucceeded: receipt is null");
        } else {
            this.unityPurchasing.OnPurchaseSucceeded(receipt.getSku(), encodeReceipt(receipt), receipt.getReceiptId());
        }
    }

    private void initiatePurchaseUpdate(boolean restoreAll) {
        this.receipts = new HashMap();
        this.amazonPurchasing.getPurchaseUpdates(restoreAll);
    }

    private BigDecimal parsePrice(String priceFormattedWithCurrency) {
        if (this.ISO3266CountryCode != null && this.ISO3266CountryCode.length() > 0) {
            try {
                Locale locale = findLocale(this.ISO3266CountryCode);
                try {
                    NumberFormat format = NumberFormat.getCurrencyInstance(locale);
                    Number n = format.parse(priceFormattedWithCurrency);
                    return new BigDecimal(n.toString());
                } catch (Exception e) {
                    Log.w("Unity Purchasing", "Unable to parse currency: " + priceFormattedWithCurrency);
                }
            } catch (LocaleNotFoundException e2) {
                Log.w("Unity Purchasing", "Locale not found for country: " + this.ISO3266CountryCode);
            }
        }
        return BigDecimal.ZERO;
    }

    @Override // com.unity.purchasing.common.IStore
    public void RetrieveProducts(List<ProductDefinition> products) {
        Log.d(LOG_PREFIX, "RetrieveProducts " + products.size());
        this.products = products;
        if (this.userId == null) {
            this.amazonPurchasing.getUserData();
        } else {
            RefreshProducts();
        }
    }

    @Override // com.unity.purchasing.common.IStore
    public void Purchase(ProductDefinition product) {
        Purchase(product, (String) null);
    }

    @Override // com.unity.purchasing.common.IStore
    public void Purchase(ProductDefinition product, String developerPayload) {
        Log.d(LOG_PREFIX, "initiatePurchaseRequest");
        this.PRODUCT_UNDER_PURCHASE = product.storeSpecificId;
        this.amazonPurchasing.purchase(product.storeSpecificId);
    }

    @Override // com.unity.purchasing.common.IStore
    public void FinishTransaction(ProductDefinition product, String transactionID) {
        Log.d(LOG_PREFIX, "Finishing transaction " + transactionID);
        this.amazonPurchasing.notifyFulfillment(transactionID, FulfillmentResult.FULFILLED);
    }

    private static class LocaleNotFoundException extends Exception {
        private LocaleNotFoundException() {
        }
    }

    private static Locale findLocale(String isoCountryCode) throws LocaleNotFoundException {
        for (Locale locale : Locale.getAvailableLocales()) {
            if (locale.getCountry().equals(isoCountryCode)) {
                return locale;
            }
        }
        throw new LocaleNotFoundException();
    }

    private List<ProductDescription> getProductMetadata() {
        List<ProductDescription> products = new ArrayList<>();
        for (Map.Entry<String, Product> entry : this.dataResponses.entrySet()) {
            Product product = entry.getValue();
            String priceString = product.getPrice() == null ? "0" : product.getPrice();
            ProductMetadata metadata = new ProductMetadata(priceString, product.getTitle(), product.getDescription(), CurrencyMapper.MapISO3166Alpha2ToISO4217OrEmpty(this.ISO3266CountryCode), parsePrice(priceString));
            String receipt = "";
            String tranID = "";
            if (this.receipts.containsKey(entry.getKey())) {
                Receipt r = this.receipts.get(entry.getKey());
                receipt = encodeReceipt(r).toString();
                tranID = r.getReceiptId();
            }
            products.add(new ProductDescription(entry.getKey(), metadata, receipt, tranID));
        }
        return products;
    }

    @Override // com.amazon.device.iap.PurchasingListener
    public void onPurchaseUpdatesResponse(PurchaseUpdatesResponse response) {
        Log.d(LOG_PREFIX, "onPurchaseUpdatesResponse");
        switch (response.getRequestStatus()) {
            case NOT_SUPPORTED:
                this.unityPurchasing.OnSetupFailed(InitializationFailureReason.PurchasingUnavailable);
                break;
            case FAILED:
                Retry(new Runnable() { // from class: com.unity.purchasing.amazon.AmazonPurchasing.2
                    @Override // java.lang.Runnable
                    public void run() {
                        AmazonPurchasing.this.amazonPurchasing.getPurchaseUpdates(false);
                    }
                });
                break;
            case SUCCESSFUL:
                ResetDelay();
                for (Receipt r : response.getReceipts()) {
                    Log.d(r.getSku(), String.valueOf(r.isCanceled()));
                    if (!r.isCanceled()) {
                        this.receipts.put(r.getSku(), r);
                    }
                }
                if (response.hasMore()) {
                    PurchasingService.getPurchaseUpdates(false);
                } else {
                    this.unityPurchasing.OnProductsRetrieved(getProductMetadata());
                }
                break;
        }
    }

    @Override // com.amazon.device.iap.PurchasingListener
    public void onUserDataResponse(UserDataResponse response) {
        Log.d(LOG_PREFIX, String.format("onGetUserIdResponse:%s", response.getUserData().getUserId()));
        switch (response.getRequestStatus()) {
            case SUCCESSFUL:
                ResetDelay();
                this.userId = response.getUserData().getUserId();
                this.ISO3266CountryCode = response.getUserData().getMarketplace();
                RefreshProducts();
                break;
            case NOT_SUPPORTED:
                this.unityPurchasing.OnSetupFailed(InitializationFailureReason.PurchasingUnavailable);
                break;
            case FAILED:
                Retry(new Runnable() { // from class: com.unity.purchasing.amazon.AmazonPurchasing.3
                    @Override // java.lang.Runnable
                    public void run() {
                        AmazonPurchasing.this.amazonPurchasing.getUserData();
                    }
                });
                break;
        }
    }

    private void ResetDelay() {
        this.delayInMillis = 5000L;
    }

    private void Retry(Runnable r) {
        this.handler.ExecuteDelayed(r, this.delayInMillis);
        this.delayInMillis *= 2;
        this.delayInMillis = Math.min(this.delayInMillis, Constants.USER_SESSION_INACTIVE_PERIOD);
    }

    private void RefreshProducts() {
        this.dataResponses = new HashMap();
        this.itemGroups = new LinkedList<>();
        int sizeOfList = this.products.size();
        for (int i = 0; i < sizeOfList; i += 100) {
            List<ProductDefinition> sublist = this.products.subList(i, Math.min(sizeOfList, i + 100));
            HashSet<String> set = new HashSet<>();
            for (ProductDefinition def : sublist) {
                set.add(def.storeSpecificId);
            }
            this.itemGroups.add(set);
        }
        Log.d(LOG_PREFIX, String.format("Requesting %d groups of skus", Integer.valueOf(this.itemGroups.size())));
        this.amazonPurchasing.getProductData(this.itemGroups.peek());
    }
}
