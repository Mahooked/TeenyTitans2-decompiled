.class public Lcom/unity/purchasing/amazon/AmazonPurchasing;
.super Lcom/unity/purchasing/common/StoreDeserializer;
.source "AmazonPurchasing.java"

# interfaces
.implements Lcom/amazon/device/iap/PurchasingListener;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/unity/purchasing/amazon/AmazonPurchasing$LocaleNotFoundException;
    }
.end annotation


# static fields
.field private static final LOG_PREFIX:Ljava/lang/String; = "Unity Purchasing Amazon"

.field private static instance:Lcom/unity/purchasing/amazon/AmazonPurchasing;


# instance fields
.field private ISO3266CountryCode:Ljava/lang/String;

.field private PRODUCT_UNDER_PURCHASE:Ljava/lang/String;

.field private amazonPurchasing:Lcom/unity/purchasing/amazon/IPurchasingService;

.field private dataResponses:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Lcom/amazon/device/iap/model/Product;",
            ">;"
        }
    .end annotation
.end field

.field private delayInMillis:J

.field private handler:Lcom/unity/purchasing/common/IDelayedExecutor;

.field private itemGroups:Ljava/util/LinkedList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/LinkedList",
            "<",
            "Ljava/util/Set",
            "<",
            "Ljava/lang/String;",
            ">;>;"
        }
    .end annotation
.end field

.field private products:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List",
            "<",
            "Lcom/unity/purchasing/common/ProductDefinition;",
            ">;"
        }
    .end annotation
.end field

.field private receipts:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Lcom/amazon/device/iap/model/Receipt;",
            ">;"
        }
    .end annotation
.end field

.field private unityPurchasing:Lcom/unity/purchasing/common/IStoreCallback;

.field private userId:Ljava/lang/String;


# direct methods
.method public constructor <init>(Lcom/unity/purchasing/common/IStoreCallback;Lcom/unity/purchasing/amazon/IPurchasingService;Lcom/unity/purchasing/common/IDelayedExecutor;)V
    .locals 2
    .param p1, "callback"    # Lcom/unity/purchasing/common/IStoreCallback;
    .param p2, "amazonPurchasing"    # Lcom/unity/purchasing/amazon/IPurchasingService;
    .param p3, "handler"    # Lcom/unity/purchasing/common/IDelayedExecutor;

    .prologue
    .line 72
    invoke-direct {p0}, Lcom/unity/purchasing/common/StoreDeserializer;-><init>()V

    .line 379
    const-wide/16 v0, 0x1388

    iput-wide v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->delayInMillis:J

    .line 73
    iput-object p1, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->unityPurchasing:Lcom/unity/purchasing/common/IStoreCallback;

    .line 74
    iput-object p2, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->amazonPurchasing:Lcom/unity/purchasing/amazon/IPurchasingService;

    .line 75
    iput-object p3, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->handler:Lcom/unity/purchasing/common/IDelayedExecutor;

    .line 76
    invoke-interface {p2, p0}, Lcom/unity/purchasing/amazon/IPurchasingService;->registerListener(Lcom/amazon/device/iap/PurchasingListener;)V

    .line 77
    return-void
.end method

.method private OnPurchaseSucceeded(Lcom/amazon/device/iap/model/Receipt;)V
    .locals 4
    .param p1, "receipt"    # Lcom/amazon/device/iap/model/Receipt;

    .prologue
    .line 221
    if-nez p1, :cond_0

    .line 222
    const-string v0, "Unity Purchasing Amazon"

    const-string v1, "OnPurchaseSucceeded: receipt is null"

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 226
    :goto_0
    return-void

    .line 224
    :cond_0
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->unityPurchasing:Lcom/unity/purchasing/common/IStoreCallback;

    invoke-virtual {p1}, Lcom/amazon/device/iap/model/Receipt;->getSku()Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, p1}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->encodeReceipt(Lcom/amazon/device/iap/model/Receipt;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1}, Lcom/amazon/device/iap/model/Receipt;->getReceiptId()Ljava/lang/String;

    move-result-object v3

    invoke-interface {v0, v1, v2, v3}, Lcom/unity/purchasing/common/IStoreCallback;->OnPurchaseSucceeded(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0
.end method

.method private RefreshProducts()V
    .locals 11

    .prologue
    .line 394
    new-instance v6, Ljava/util/HashMap;

    invoke-direct {v6}, Ljava/util/HashMap;-><init>()V

    iput-object v6, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->dataResponses:Ljava/util/Map;

    .line 397
    new-instance v6, Ljava/util/LinkedList;

    invoke-direct {v6}, Ljava/util/LinkedList;-><init>()V

    iput-object v6, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->itemGroups:Ljava/util/LinkedList;

    .line 398
    iget-object v6, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->products:Ljava/util/List;

    invoke-interface {v6}, Ljava/util/List;->size()I

    move-result v4

    .line 399
    .local v4, "sizeOfList":I
    const/16 v0, 0x64

    .line 401
    .local v0, "breakApart":I
    const/4 v2, 0x0

    .local v2, "i":I
    :goto_0
    if-ge v2, v4, :cond_1

    .line 402
    iget-object v6, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->products:Ljava/util/List;

    add-int/lit8 v7, v2, 0x64

    invoke-static {v4, v7}, Ljava/lang/Math;->min(II)I

    move-result v7

    invoke-interface {v6, v2, v7}, Ljava/util/List;->subList(II)Ljava/util/List;

    move-result-object v5

    .line 403
    .local v5, "sublist":Ljava/util/List;, "Ljava/util/List<Lcom/unity/purchasing/common/ProductDefinition;>;"
    new-instance v3, Ljava/util/HashSet;

    invoke-direct {v3}, Ljava/util/HashSet;-><init>()V

    .line 404
    .local v3, "set":Ljava/util/HashSet;, "Ljava/util/HashSet<Ljava/lang/String;>;"
    invoke-interface {v5}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v6

    :goto_1
    invoke-interface {v6}, Ljava/util/Iterator;->hasNext()Z

    move-result v7

    if-eqz v7, :cond_0

    invoke-interface {v6}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/unity/purchasing/common/ProductDefinition;

    .line 405
    .local v1, "def":Lcom/unity/purchasing/common/ProductDefinition;
    iget-object v7, v1, Lcom/unity/purchasing/common/ProductDefinition;->storeSpecificId:Ljava/lang/String;

    invoke-virtual {v3, v7}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 407
    .end local v1    # "def":Lcom/unity/purchasing/common/ProductDefinition;
    :cond_0
    iget-object v6, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->itemGroups:Ljava/util/LinkedList;

    invoke-virtual {v6, v3}, Ljava/util/LinkedList;->add(Ljava/lang/Object;)Z

    .line 401
    add-int/lit8 v2, v2, 0x64

    goto :goto_0

    .line 409
    .end local v3    # "set":Ljava/util/HashSet;, "Ljava/util/HashSet<Ljava/lang/String;>;"
    .end local v5    # "sublist":Ljava/util/List;, "Ljava/util/List<Lcom/unity/purchasing/common/ProductDefinition;>;"
    :cond_1
    const-string v6, "Unity Purchasing Amazon"

    const-string v7, "Requesting %d groups of skus"

    const/4 v8, 0x1

    new-array v8, v8, [Ljava/lang/Object;

    const/4 v9, 0x0

    iget-object v10, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->itemGroups:Ljava/util/LinkedList;

    invoke-virtual {v10}, Ljava/util/LinkedList;->size()I

    move-result v10

    invoke-static {v10}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v10

    aput-object v10, v8, v9

    invoke-static {v7, v8}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v7

    invoke-static {v6, v7}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 410
    iget-object v7, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->amazonPurchasing:Lcom/unity/purchasing/amazon/IPurchasingService;

    iget-object v6, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->itemGroups:Ljava/util/LinkedList;

    invoke-virtual {v6}, Ljava/util/LinkedList;->peek()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/util/Set;

    invoke-interface {v7, v6}, Lcom/unity/purchasing/amazon/IPurchasingService;->getProductData(Ljava/util/Set;)Lcom/amazon/device/iap/model/RequestId;

    .line 411
    return-void
.end method

.method private ResetDelay()V
    .locals 2

    .prologue
    .line 382
    const-wide/16 v0, 0x1388

    iput-wide v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->delayInMillis:J

    .line 383
    return-void
.end method

.method private Retry(Ljava/lang/Runnable;)V
    .locals 4
    .param p1, "r"    # Ljava/lang/Runnable;

    .prologue
    .line 386
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->handler:Lcom/unity/purchasing/common/IDelayedExecutor;

    iget-wide v2, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->delayInMillis:J

    invoke-interface {v0, p1, v2, v3}, Lcom/unity/purchasing/common/IDelayedExecutor;->ExecuteDelayed(Ljava/lang/Runnable;J)V

    .line 387
    iget-wide v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->delayInMillis:J

    const-wide/16 v2, 0x2

    mul-long/2addr v0, v2

    iput-wide v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->delayInMillis:J

    .line 389
    iget-wide v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->delayInMillis:J

    const-wide/32 v2, 0x493e0

    invoke-static {v0, v1, v2, v3}, Ljava/lang/Math;->min(JJ)J

    move-result-wide v0

    iput-wide v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->delayInMillis:J

    .line 390
    return-void
.end method

.method static synthetic access$000(Lcom/unity/purchasing/amazon/AmazonPurchasing;)Ljava/util/LinkedList;
    .locals 1
    .param p0, "x0"    # Lcom/unity/purchasing/amazon/AmazonPurchasing;

    .prologue
    .line 50
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->itemGroups:Ljava/util/LinkedList;

    return-object v0
.end method

.method static synthetic access$100(Lcom/unity/purchasing/amazon/AmazonPurchasing;)Lcom/unity/purchasing/amazon/IPurchasingService;
    .locals 1
    .param p0, "x0"    # Lcom/unity/purchasing/amazon/AmazonPurchasing;

    .prologue
    .line 50
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->amazonPurchasing:Lcom/unity/purchasing/amazon/IPurchasingService;

    return-object v0
.end method

.method public static buildSandboxJSON(Ljava/util/List;)Ljava/lang/String;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List",
            "<",
            "Lcom/unity/purchasing/common/ProductDefinition;",
            ">;)",
            "Ljava/lang/String;"
        }
    .end annotation

    .prologue
    .line 111
    .local p0, "products":Ljava/util/List;, "Ljava/util/List<Lcom/unity/purchasing/common/ProductDefinition;>;"
    new-instance v2, Lcom/unity/purchasing/common/SaneJSONObject;

    invoke-direct {v2}, Lcom/unity/purchasing/common/SaneJSONObject;-><init>()V

    .line 112
    .local v2, "result":Lcom/unity/purchasing/common/SaneJSONObject;
    invoke-interface {p0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :cond_0
    :goto_0
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_3

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/unity/purchasing/common/ProductDefinition;

    .line 113
    .local v1, "product":Lcom/unity/purchasing/common/ProductDefinition;
    new-instance v0, Lcom/unity/purchasing/common/SaneJSONObject;

    invoke-direct {v0}, Lcom/unity/purchasing/common/SaneJSONObject;-><init>()V

    .line 114
    .local v0, "hash":Lcom/unity/purchasing/common/SaneJSONObject;
    iget-object v3, v1, Lcom/unity/purchasing/common/ProductDefinition;->storeSpecificId:Ljava/lang/String;

    invoke-virtual {v2, v3, v0}, Lcom/unity/purchasing/common/SaneJSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 115
    const-string v5, "itemType"

    iget-object v3, v1, Lcom/unity/purchasing/common/ProductDefinition;->type:Lcom/unity/purchasing/common/ProductType;

    sget-object v6, Lcom/unity/purchasing/common/ProductType;->Consumable:Lcom/unity/purchasing/common/ProductType;

    if-ne v3, v6, :cond_1

    const-string v3, "CONSUMABLE"

    :goto_1
    invoke-virtual {v0, v5, v3}, Lcom/unity/purchasing/common/SaneJSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 118
    const-string v3, "title"

    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "Fake title for "

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    iget-object v6, v1, Lcom/unity/purchasing/common/ProductDefinition;->id:Ljava/lang/String;

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v0, v3, v5}, Lcom/unity/purchasing/common/SaneJSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 119
    const-string v3, "description"

    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "Fake description for "

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    iget-object v6, v1, Lcom/unity/purchasing/common/ProductDefinition;->id:Ljava/lang/String;

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v0, v3, v5}, Lcom/unity/purchasing/common/SaneJSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 120
    const-string v3, "price"

    const-string v5, "0.99"

    invoke-virtual {v0, v3, v5}, Lcom/unity/purchasing/common/SaneJSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 121
    const-string v3, "smallIconUrl"

    const-string v5, "http://example.com"

    invoke-virtual {v0, v3, v5}, Lcom/unity/purchasing/common/SaneJSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 122
    sget-object v3, Lcom/unity/purchasing/common/ProductType;->Subscription:Lcom/unity/purchasing/common/ProductType;

    iget-object v5, v1, Lcom/unity/purchasing/common/ProductDefinition;->type:Lcom/unity/purchasing/common/ProductType;

    if-ne v3, v5, :cond_0

    .line 125
    const-string v3, "subscriptionParent"

    const-string v5, "does.not.exist"

    invoke-virtual {v0, v3, v5}, Lcom/unity/purchasing/common/SaneJSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    goto :goto_0

    .line 115
    :cond_1
    iget-object v3, v1, Lcom/unity/purchasing/common/ProductDefinition;->type:Lcom/unity/purchasing/common/ProductType;

    sget-object v6, Lcom/unity/purchasing/common/ProductType;->NonConsumable:Lcom/unity/purchasing/common/ProductType;

    if-ne v3, v6, :cond_2

    const-string v3, "ENTITLED"

    goto :goto_1

    :cond_2
    const-string v3, "SUBSCRIPTION"

    goto :goto_1

    .line 128
    .end local v0    # "hash":Lcom/unity/purchasing/common/SaneJSONObject;
    .end local v1    # "product":Lcom/unity/purchasing/common/ProductDefinition;
    :cond_3
    invoke-virtual {v2}, Lcom/unity/purchasing/common/SaneJSONObject;->toString()Ljava/lang/String;

    move-result-object v3

    return-object v3
.end method

.method private convertAmazonRequestStatustoFailureReason(Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;)Lcom/unity/purchasing/common/PurchaseFailureReason;
    .locals 2
    .param p1, "status"    # Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;

    .prologue
    .line 201
    sget-object v0, Lcom/unity/purchasing/amazon/AmazonPurchasing$4;->$SwitchMap$com$amazon$device$iap$model$PurchaseResponse$RequestStatus:[I

    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->ordinal()I

    move-result v1

    aget v0, v0, v1

    packed-switch v0, :pswitch_data_0

    .line 208
    sget-object v0, Lcom/unity/purchasing/common/PurchaseFailureReason;->Unknown:Lcom/unity/purchasing/common/PurchaseFailureReason;

    :goto_0
    return-object v0

    .line 203
    :pswitch_0
    sget-object v0, Lcom/unity/purchasing/common/PurchaseFailureReason;->ItemUnavailable:Lcom/unity/purchasing/common/PurchaseFailureReason;

    goto :goto_0

    .line 205
    :pswitch_1
    sget-object v0, Lcom/unity/purchasing/common/PurchaseFailureReason;->BillingUnavailable:Lcom/unity/purchasing/common/PurchaseFailureReason;

    goto :goto_0

    .line 201
    :pswitch_data_0
    .packed-switch 0x3
        :pswitch_0
        :pswitch_1
    .end packed-switch
.end method

.method private encodeReceipt(Lcom/amazon/device/iap/model/Receipt;)Ljava/lang/String;
    .locals 4
    .param p1, "receipt"    # Lcom/amazon/device/iap/model/Receipt;

    .prologue
    .line 212
    new-instance v0, Lcom/unity/purchasing/common/SaneJSONObject;

    invoke-direct {v0}, Lcom/unity/purchasing/common/SaneJSONObject;-><init>()V

    .line 213
    .local v0, "receiptBundle":Lcom/unity/purchasing/common/SaneJSONObject;
    if-nez p1, :cond_0

    const-string v1, ""

    .line 214
    .local v1, "receiptId":Ljava/lang/String;
    :goto_0
    const-string v2, "receiptId"

    invoke-virtual {v0, v2, v1}, Lcom/unity/purchasing/common/SaneJSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 215
    const-string v2, "userId"

    iget-object v3, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->userId:Ljava/lang/String;

    invoke-virtual {v0, v2, v3}, Lcom/unity/purchasing/common/SaneJSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 216
    const-string v2, "isSandbox"

    sget-boolean v3, Lcom/amazon/device/iap/PurchasingService;->IS_SANDBOX_MODE:Z

    invoke-virtual {v0, v2, v3}, Lcom/unity/purchasing/common/SaneJSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    .line 217
    invoke-virtual {v0}, Lcom/unity/purchasing/common/SaneJSONObject;->toString()Ljava/lang/String;

    move-result-object v2

    return-object v2

    .line 213
    .end local v1    # "receiptId":Ljava/lang/String;
    :cond_0
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/Receipt;->getReceiptId()Ljava/lang/String;

    move-result-object v1

    goto :goto_0
.end method

.method private static findLocale(Ljava/lang/String;)Ljava/util/Locale;
    .locals 5
    .param p0, "isoCountryCode"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/unity/purchasing/amazon/AmazonPurchasing$LocaleNotFoundException;
        }
    .end annotation

    .prologue
    .line 284
    invoke-static {}, Ljava/util/Locale;->getAvailableLocales()[Ljava/util/Locale;

    move-result-object v2

    array-length v3, v2

    const/4 v1, 0x0

    :goto_0
    if-ge v1, v3, :cond_1

    aget-object v0, v2, v1

    .line 285
    .local v0, "locale":Ljava/util/Locale;
    invoke-virtual {v0}, Ljava/util/Locale;->getCountry()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v4, p0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_0

    .line 286
    return-object v0

    .line 284
    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 290
    .end local v0    # "locale":Ljava/util/Locale;
    :cond_1
    new-instance v1, Lcom/unity/purchasing/amazon/AmazonPurchasing$LocaleNotFoundException;

    const/4 v2, 0x0

    invoke-direct {v1, v2}, Lcom/unity/purchasing/amazon/AmazonPurchasing$LocaleNotFoundException;-><init>(Lcom/unity/purchasing/amazon/AmazonPurchasing$1;)V

    throw v1
.end method

.method private getProductMetadata()Ljava/util/List;
    .locals 13
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List",
            "<",
            "Lcom/unity/purchasing/common/ProductDescription;",
            ">;"
        }
    .end annotation

    .prologue
    .line 294
    new-instance v8, Ljava/util/ArrayList;

    invoke-direct {v8}, Ljava/util/ArrayList;-><init>()V

    .line 295
    .local v8, "products":Ljava/util/List;, "Ljava/util/List<Lcom/unity/purchasing/common/ProductDescription;>;"
    iget-object v2, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->dataResponses:Ljava/util/Map;

    invoke-interface {v2}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v2

    invoke-interface {v2}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v12

    :goto_0
    invoke-interface {v12}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    invoke-interface {v12}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/util/Map$Entry;

    .line 296
    .local v6, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Lcom/amazon/device/iap/model/Product;>;"
    invoke-interface {v6}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Lcom/amazon/device/iap/model/Product;

    .line 298
    .local v7, "product":Lcom/amazon/device/iap/model/Product;
    invoke-virtual {v7}, Lcom/amazon/device/iap/model/Product;->getPrice()Ljava/lang/String;

    move-result-object v2

    if-nez v2, :cond_1

    const-string v1, "0"

    .line 299
    .local v1, "priceString":Ljava/lang/String;
    :goto_1
    new-instance v0, Lcom/unity/purchasing/common/ProductMetadata;

    .line 300
    invoke-virtual {v7}, Lcom/amazon/device/iap/model/Product;->getTitle()Ljava/lang/String;

    move-result-object v2

    .line 301
    invoke-virtual {v7}, Lcom/amazon/device/iap/model/Product;->getDescription()Ljava/lang/String;

    move-result-object v3

    iget-object v4, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->ISO3266CountryCode:Ljava/lang/String;

    .line 302
    invoke-static {v4}, Lcom/unity/purchasing/amazon/CurrencyMapper;->MapISO3166Alpha2ToISO4217OrEmpty(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    .line 303
    invoke-direct {p0, v1}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->parsePrice(Ljava/lang/String;)Ljava/math/BigDecimal;

    move-result-object v5

    invoke-direct/range {v0 .. v5}, Lcom/unity/purchasing/common/ProductMetadata;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/math/BigDecimal;)V

    .line 305
    .local v0, "metadata":Lcom/unity/purchasing/common/ProductMetadata;
    const-string v10, ""

    .line 306
    .local v10, "receipt":Ljava/lang/String;
    const-string v11, ""

    .line 308
    .local v11, "tranID":Ljava/lang/String;
    iget-object v2, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->receipts:Ljava/util/Map;

    invoke-interface {v6}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    invoke-interface {v2, v3}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_0

    .line 309
    iget-object v2, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->receipts:Ljava/util/Map;

    invoke-interface {v6}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    invoke-interface {v2, v3}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v9

    check-cast v9, Lcom/amazon/device/iap/model/Receipt;

    .line 310
    .local v9, "r":Lcom/amazon/device/iap/model/Receipt;
    invoke-direct {p0, v9}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->encodeReceipt(Lcom/amazon/device/iap/model/Receipt;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->toString()Ljava/lang/String;

    move-result-object v10

    .line 311
    invoke-virtual {v9}, Lcom/amazon/device/iap/model/Receipt;->getReceiptId()Ljava/lang/String;

    move-result-object v11

    .line 314
    .end local v9    # "r":Lcom/amazon/device/iap/model/Receipt;
    :cond_0
    new-instance v3, Lcom/unity/purchasing/common/ProductDescription;

    invoke-interface {v6}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    invoke-direct {v3, v2, v0, v10, v11}, Lcom/unity/purchasing/common/ProductDescription;-><init>(Ljava/lang/String;Lcom/unity/purchasing/common/ProductMetadata;Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v8, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 298
    .end local v0    # "metadata":Lcom/unity/purchasing/common/ProductMetadata;
    .end local v1    # "priceString":Ljava/lang/String;
    .end local v10    # "receipt":Ljava/lang/String;
    .end local v11    # "tranID":Ljava/lang/String;
    :cond_1
    invoke-virtual {v7}, Lcom/amazon/device/iap/model/Product;->getPrice()Ljava/lang/String;

    move-result-object v1

    goto :goto_1

    .line 316
    .end local v6    # "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Lcom/amazon/device/iap/model/Product;>;"
    .end local v7    # "product":Lcom/amazon/device/iap/model/Product;
    :cond_2
    return-object v8
.end method

.method private initiatePurchaseUpdate(Z)V
    .locals 1
    .param p1, "restoreAll"    # Z

    .prologue
    .line 229
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->receipts:Ljava/util/Map;

    .line 230
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->amazonPurchasing:Lcom/unity/purchasing/amazon/IPurchasingService;

    invoke-interface {v0, p1}, Lcom/unity/purchasing/amazon/IPurchasingService;->getPurchaseUpdates(Z)Lcom/amazon/device/iap/model/RequestId;

    .line 231
    return-void
.end method

.method public static instance(Lcom/unity/purchasing/common/IUnityCallback;)Lcom/unity/purchasing/amazon/AmazonPurchasing;
    .locals 4
    .param p0, "bridge"    # Lcom/unity/purchasing/common/IUnityCallback;

    .prologue
    .line 65
    sget-object v1, Lcom/unity/purchasing/amazon/AmazonPurchasing;->instance:Lcom/unity/purchasing/amazon/AmazonPurchasing;

    if-nez v1, :cond_0

    .line 66
    new-instance v0, Lcom/unity/purchasing/amazon/AmazonPurchasingService;

    sget-object v1, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    invoke-direct {v0, v1}, Lcom/unity/purchasing/amazon/AmazonPurchasingService;-><init>(Landroid/content/Context;)V

    .line 67
    .local v0, "amazon":Lcom/unity/purchasing/amazon/AmazonPurchasingService;
    new-instance v1, Lcom/unity/purchasing/amazon/AmazonPurchasing;

    new-instance v2, Lcom/unity/purchasing/common/UnityPurchasing;

    invoke-direct {v2, p0}, Lcom/unity/purchasing/common/UnityPurchasing;-><init>(Lcom/unity/purchasing/common/IUnityCallback;)V

    new-instance v3, Lcom/unity/purchasing/common/DelayedHandler;

    invoke-direct {v3}, Lcom/unity/purchasing/common/DelayedHandler;-><init>()V

    invoke-direct {v1, v2, v0, v3}, Lcom/unity/purchasing/amazon/AmazonPurchasing;-><init>(Lcom/unity/purchasing/common/IStoreCallback;Lcom/unity/purchasing/amazon/IPurchasingService;Lcom/unity/purchasing/common/IDelayedExecutor;)V

    sput-object v1, Lcom/unity/purchasing/amazon/AmazonPurchasing;->instance:Lcom/unity/purchasing/amazon/AmazonPurchasing;

    .line 69
    .end local v0    # "amazon":Lcom/unity/purchasing/amazon/AmazonPurchasingService;
    :cond_0
    sget-object v1, Lcom/unity/purchasing/amazon/AmazonPurchasing;->instance:Lcom/unity/purchasing/amazon/AmazonPurchasing;

    return-object v1
.end method

.method private parsePrice(Ljava/lang/String;)Ljava/math/BigDecimal;
    .locals 7
    .param p1, "priceFormattedWithCurrency"    # Ljava/lang/String;

    .prologue
    .line 235
    iget-object v4, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->ISO3266CountryCode:Ljava/lang/String;

    if-eqz v4, :cond_0

    iget-object v4, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->ISO3266CountryCode:Ljava/lang/String;

    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-lez v4, :cond_0

    .line 237
    :try_start_0
    iget-object v4, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->ISO3266CountryCode:Ljava/lang/String;

    invoke-static {v4}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->findLocale(Ljava/lang/String;)Ljava/util/Locale;
    :try_end_0
    .catch Lcom/unity/purchasing/amazon/AmazonPurchasing$LocaleNotFoundException; {:try_start_0 .. :try_end_0} :catch_1

    move-result-object v2

    .line 239
    .local v2, "locale":Ljava/util/Locale;
    :try_start_1
    invoke-static {v2}, Ljava/text/NumberFormat;->getCurrencyInstance(Ljava/util/Locale;)Ljava/text/NumberFormat;

    move-result-object v1

    .line 240
    .local v1, "format":Ljava/text/NumberFormat;
    invoke-virtual {v1, p1}, Ljava/text/NumberFormat;->parse(Ljava/lang/String;)Ljava/lang/Number;

    move-result-object v3

    .line 241
    .local v3, "n":Ljava/lang/Number;
    new-instance v4, Ljava/math/BigDecimal;

    invoke-virtual {v3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-direct {v4, v5}, Ljava/math/BigDecimal;-><init>(Ljava/lang/String;)V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catch Lcom/unity/purchasing/amazon/AmazonPurchasing$LocaleNotFoundException; {:try_start_1 .. :try_end_1} :catch_1

    .line 251
    .end local v1    # "format":Ljava/text/NumberFormat;
    .end local v2    # "locale":Ljava/util/Locale;
    .end local v3    # "n":Ljava/lang/Number;
    :goto_0
    return-object v4

    .line 242
    .restart local v2    # "locale":Ljava/util/Locale;
    :catch_0
    move-exception v0

    .line 243
    .local v0, "e":Ljava/lang/Exception;
    :try_start_2
    const-string v4, "Unity Purchasing"

    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "Unable to parse currency: "

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    invoke-virtual {v5, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-static {v4, v5}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_2
    .catch Lcom/unity/purchasing/amazon/AmazonPurchasing$LocaleNotFoundException; {:try_start_2 .. :try_end_2} :catch_1

    .line 251
    .end local v0    # "e":Ljava/lang/Exception;
    .end local v2    # "locale":Ljava/util/Locale;
    :cond_0
    :goto_1
    sget-object v4, Ljava/math/BigDecimal;->ZERO:Ljava/math/BigDecimal;

    goto :goto_0

    .line 245
    :catch_1
    move-exception v0

    .line 246
    .local v0, "e":Lcom/unity/purchasing/amazon/AmazonPurchasing$LocaleNotFoundException;
    const-string v4, "Unity Purchasing"

    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "Locale not found for country: "

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    iget-object v6, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->ISO3266CountryCode:Ljava/lang/String;

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-static {v4, v5}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_1
.end method


# virtual methods
.method public FinishTransaction(Lcom/unity/purchasing/common/ProductDefinition;Ljava/lang/String;)V
    .locals 3
    .param p1, "product"    # Lcom/unity/purchasing/common/ProductDefinition;
    .param p2, "transactionID"    # Ljava/lang/String;

    .prologue
    .line 277
    const-string v0, "Unity Purchasing Amazon"

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Finishing transaction "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 278
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->amazonPurchasing:Lcom/unity/purchasing/amazon/IPurchasingService;

    sget-object v1, Lcom/amazon/device/iap/model/FulfillmentResult;->FULFILLED:Lcom/amazon/device/iap/model/FulfillmentResult;

    invoke-interface {v0, p2, v1}, Lcom/unity/purchasing/amazon/IPurchasingService;->notifyFulfillment(Ljava/lang/String;Lcom/amazon/device/iap/model/FulfillmentResult;)V

    .line 279
    return-void
.end method

.method public Purchase(Lcom/unity/purchasing/common/ProductDefinition;)V
    .locals 1
    .param p1, "product"    # Lcom/unity/purchasing/common/ProductDefinition;

    .prologue
    .line 267
    const/4 v0, 0x0

    invoke-virtual {p0, p1, v0}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->Purchase(Lcom/unity/purchasing/common/ProductDefinition;Ljava/lang/String;)V

    .line 268
    return-void
.end method

.method public Purchase(Lcom/unity/purchasing/common/ProductDefinition;Ljava/lang/String;)V
    .locals 2
    .param p1, "product"    # Lcom/unity/purchasing/common/ProductDefinition;
    .param p2, "developerPayload"    # Ljava/lang/String;

    .prologue
    .line 271
    const-string v0, "Unity Purchasing Amazon"

    const-string v1, "initiatePurchaseRequest"

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 272
    iget-object v0, p1, Lcom/unity/purchasing/common/ProductDefinition;->storeSpecificId:Ljava/lang/String;

    iput-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->PRODUCT_UNDER_PURCHASE:Ljava/lang/String;

    .line 273
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->amazonPurchasing:Lcom/unity/purchasing/amazon/IPurchasingService;

    iget-object v1, p1, Lcom/unity/purchasing/common/ProductDefinition;->storeSpecificId:Ljava/lang/String;

    invoke-interface {v0, v1}, Lcom/unity/purchasing/amazon/IPurchasingService;->purchase(Ljava/lang/String;)Lcom/amazon/device/iap/model/RequestId;

    .line 274
    return-void
.end method

.method public RetrieveProducts(Ljava/util/List;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List",
            "<",
            "Lcom/unity/purchasing/common/ProductDefinition;",
            ">;)V"
        }
    .end annotation

    .prologue
    .line 256
    .local p1, "products":Ljava/util/List;, "Ljava/util/List<Lcom/unity/purchasing/common/ProductDefinition;>;"
    const-string v0, "Unity Purchasing Amazon"

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "RetrieveProducts "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 258
    iput-object p1, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->products:Ljava/util/List;

    .line 259
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->userId:Ljava/lang/String;

    if-nez v0, :cond_0

    .line 260
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->amazonPurchasing:Lcom/unity/purchasing/amazon/IPurchasingService;

    invoke-interface {v0}, Lcom/unity/purchasing/amazon/IPurchasingService;->getUserData()Lcom/amazon/device/iap/model/RequestId;

    .line 264
    :goto_0
    return-void

    .line 262
    :cond_0
    invoke-direct {p0}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->RefreshProducts()V

    goto :goto_0
.end method

.method public getAmazonUserId()Ljava/lang/String;
    .locals 1

    .prologue
    .line 89
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->userId:Ljava/lang/String;

    return-object v0
.end method

.method public notifyUnableToFulfillUnavailableProduct(Ljava/lang/String;)V
    .locals 2
    .param p1, "transactionID"    # Ljava/lang/String;

    .prologue
    .line 107
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->amazonPurchasing:Lcom/unity/purchasing/amazon/IPurchasingService;

    sget-object v1, Lcom/amazon/device/iap/model/FulfillmentResult;->UNAVAILABLE:Lcom/amazon/device/iap/model/FulfillmentResult;

    invoke-interface {v0, p1, v1}, Lcom/unity/purchasing/amazon/IPurchasingService;->notifyFulfillment(Ljava/lang/String;Lcom/amazon/device/iap/model/FulfillmentResult;)V

    .line 108
    return-void
.end method

.method public onProductDataResponse(Lcom/amazon/device/iap/model/ProductDataResponse;)V
    .locals 2
    .param p1, "response"    # Lcom/amazon/device/iap/model/ProductDataResponse;

    .prologue
    .line 132
    const-string v0, "Unity Purchasing Amazon"

    const-string v1, "onItemDataResponse"

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 134
    sget-object v0, Lcom/unity/purchasing/amazon/AmazonPurchasing$4;->$SwitchMap$com$amazon$device$iap$model$ProductDataResponse$RequestStatus:[I

    invoke-virtual {p1}, Lcom/amazon/device/iap/model/ProductDataResponse;->getRequestStatus()Lcom/amazon/device/iap/model/ProductDataResponse$RequestStatus;

    move-result-object v1

    invoke-virtual {v1}, Lcom/amazon/device/iap/model/ProductDataResponse$RequestStatus;->ordinal()I

    move-result v1

    aget v0, v0, v1

    packed-switch v0, :pswitch_data_0

    .line 161
    :goto_0
    return-void

    .line 136
    :pswitch_0
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->unityPurchasing:Lcom/unity/purchasing/common/IStoreCallback;

    sget-object v1, Lcom/unity/purchasing/common/InitializationFailureReason;->PurchasingUnavailable:Lcom/unity/purchasing/common/InitializationFailureReason;

    invoke-interface {v0, v1}, Lcom/unity/purchasing/common/IStoreCallback;->OnSetupFailed(Lcom/unity/purchasing/common/InitializationFailureReason;)V

    goto :goto_0

    .line 139
    :pswitch_1
    new-instance v0, Lcom/unity/purchasing/amazon/AmazonPurchasing$1;

    invoke-direct {v0, p0}, Lcom/unity/purchasing/amazon/AmazonPurchasing$1;-><init>(Lcom/unity/purchasing/amazon/AmazonPurchasing;)V

    invoke-direct {p0, v0}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->Retry(Ljava/lang/Runnable;)V

    goto :goto_0

    .line 147
    :pswitch_2
    invoke-direct {p0}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->ResetDelay()V

    .line 148
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->dataResponses:Ljava/util/Map;

    invoke-virtual {p1}, Lcom/amazon/device/iap/model/ProductDataResponse;->getProductData()Ljava/util/Map;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/Map;->putAll(Ljava/util/Map;)V

    .line 150
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->itemGroups:Ljava/util/LinkedList;

    invoke-virtual {v0}, Ljava/util/LinkedList;->remove()Ljava/lang/Object;

    .line 151
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->itemGroups:Ljava/util/LinkedList;

    invoke-virtual {v0}, Ljava/util/LinkedList;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 153
    const/4 v0, 0x1

    invoke-direct {p0, v0}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->initiatePurchaseUpdate(Z)V

    goto :goto_0

    .line 156
    :cond_0
    iget-object v1, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->amazonPurchasing:Lcom/unity/purchasing/amazon/IPurchasingService;

    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->itemGroups:Ljava/util/LinkedList;

    invoke-virtual {v0}, Ljava/util/LinkedList;->peek()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/Set;

    invoke-interface {v1, v0}, Lcom/unity/purchasing/amazon/IPurchasingService;->getProductData(Ljava/util/Set;)Lcom/amazon/device/iap/model/RequestId;

    goto :goto_0

    .line 134
    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_0
        :pswitch_1
        :pswitch_2
    .end packed-switch
.end method

.method public onPurchaseResponse(Lcom/amazon/device/iap/model/PurchaseResponse;)V
    .locals 7
    .param p1, "purchaseResponse"    # Lcom/amazon/device/iap/model/PurchaseResponse;

    .prologue
    const/4 v6, 0x0

    .line 165
    const-string v4, "Unity Purchasing Amazon"

    const-string v5, "onPurchaseResponse"

    invoke-static {v4, v5}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 166
    iget-object v3, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->PRODUCT_UNDER_PURCHASE:Ljava/lang/String;

    .line 169
    .local v3, "sku":Ljava/lang/String;
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseResponse;->getReceipt()Lcom/amazon/device/iap/model/Receipt;

    move-result-object v4

    if-eqz v4, :cond_0

    .line 170
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseResponse;->getReceipt()Lcom/amazon/device/iap/model/Receipt;

    move-result-object v4

    invoke-virtual {v4}, Lcom/amazon/device/iap/model/Receipt;->getSku()Ljava/lang/String;

    move-result-object v3

    .line 173
    :cond_0
    const-string v4, "Unity Purchasing Amazon"

    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseResponse;->getRequestStatus()Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;

    move-result-object v5

    invoke-virtual {v5}, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-static {v4, v5}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    .line 174
    sget-object v4, Lcom/unity/purchasing/amazon/AmazonPurchasing$4;->$SwitchMap$com$amazon$device$iap$model$PurchaseResponse$RequestStatus:[I

    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseResponse;->getRequestStatus()Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;

    move-result-object v5

    invoke-virtual {v5}, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->ordinal()I

    move-result v5

    aget v4, v4, v5

    packed-switch v4, :pswitch_data_0

    .line 193
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseResponse;->getRequestStatus()Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;

    move-result-object v4

    invoke-direct {p0, v4}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->convertAmazonRequestStatustoFailureReason(Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;)Lcom/unity/purchasing/common/PurchaseFailureReason;

    move-result-object v2

    .line 194
    .local v2, "reason":Lcom/unity/purchasing/common/PurchaseFailureReason;
    new-instance v0, Lcom/unity/purchasing/common/PurchaseFailureDescription;

    invoke-direct {v0, v3, v2, v6}, Lcom/unity/purchasing/common/PurchaseFailureDescription;-><init>(Ljava/lang/String;Lcom/unity/purchasing/common/PurchaseFailureReason;Ljava/lang/String;)V

    .line 195
    .local v0, "description":Lcom/unity/purchasing/common/PurchaseFailureDescription;
    iget-object v4, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->unityPurchasing:Lcom/unity/purchasing/common/IStoreCallback;

    invoke-interface {v4, v0}, Lcom/unity/purchasing/common/IStoreCallback;->OnPurchaseFailed(Lcom/unity/purchasing/common/PurchaseFailureDescription;)V

    .line 198
    .end local v0    # "description":Lcom/unity/purchasing/common/PurchaseFailureDescription;
    .end local v2    # "reason":Lcom/unity/purchasing/common/PurchaseFailureReason;
    :goto_0
    return-void

    .line 178
    :pswitch_0
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseResponse;->getReceipt()Lcom/amazon/device/iap/model/Receipt;

    move-result-object v1

    .line 181
    .local v1, "r":Lcom/amazon/device/iap/model/Receipt;
    if-nez v1, :cond_1

    iget-object v4, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->receipts:Ljava/util/Map;

    invoke-interface {v4, v3}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_1

    .line 182
    iget-object v4, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->receipts:Ljava/util/Map;

    invoke-interface {v4, v3}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    .end local v1    # "r":Lcom/amazon/device/iap/model/Receipt;
    check-cast v1, Lcom/amazon/device/iap/model/Receipt;

    .line 185
    .restart local v1    # "r":Lcom/amazon/device/iap/model/Receipt;
    :cond_1
    if-eqz v1, :cond_2

    .line 186
    invoke-direct {p0, v1}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->OnPurchaseSucceeded(Lcom/amazon/device/iap/model/Receipt;)V

    goto :goto_0

    .line 189
    :cond_2
    iget-object v4, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->unityPurchasing:Lcom/unity/purchasing/common/IStoreCallback;

    invoke-direct {p0, v6}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->encodeReceipt(Lcom/amazon/device/iap/model/Receipt;)Ljava/lang/String;

    move-result-object v5

    const-string v6, ""

    invoke-interface {v4, v3, v5, v6}, Lcom/unity/purchasing/common/IStoreCallback;->OnPurchaseSucceeded(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    .line 174
    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method

.method public onPurchaseUpdatesResponse(Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;)V
    .locals 4
    .param p1, "response"    # Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;

    .prologue
    .line 321
    const-string v1, "Unity Purchasing Amazon"

    const-string v2, "onPurchaseUpdatesResponse"

    invoke-static {v1, v2}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 322
    sget-object v1, Lcom/unity/purchasing/amazon/AmazonPurchasing$4;->$SwitchMap$com$amazon$device$iap$model$PurchaseUpdatesResponse$RequestStatus:[I

    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->getRequestStatus()Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;

    move-result-object v2

    invoke-virtual {v2}, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;->ordinal()I

    move-result v2

    aget v1, v1, v2

    packed-switch v1, :pswitch_data_0

    .line 350
    :goto_0
    return-void

    .line 324
    :pswitch_0
    iget-object v1, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->unityPurchasing:Lcom/unity/purchasing/common/IStoreCallback;

    sget-object v2, Lcom/unity/purchasing/common/InitializationFailureReason;->PurchasingUnavailable:Lcom/unity/purchasing/common/InitializationFailureReason;

    invoke-interface {v1, v2}, Lcom/unity/purchasing/common/IStoreCallback;->OnSetupFailed(Lcom/unity/purchasing/common/InitializationFailureReason;)V

    goto :goto_0

    .line 327
    :pswitch_1
    new-instance v1, Lcom/unity/purchasing/amazon/AmazonPurchasing$2;

    invoke-direct {v1, p0}, Lcom/unity/purchasing/amazon/AmazonPurchasing$2;-><init>(Lcom/unity/purchasing/amazon/AmazonPurchasing;)V

    invoke-direct {p0, v1}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->Retry(Ljava/lang/Runnable;)V

    goto :goto_0

    .line 335
    :pswitch_2
    invoke-direct {p0}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->ResetDelay()V

    .line 336
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->getReceipts()Ljava/util/List;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_0
    :goto_1
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/amazon/device/iap/model/Receipt;

    .line 337
    .local v0, "r":Lcom/amazon/device/iap/model/Receipt;
    invoke-virtual {v0}, Lcom/amazon/device/iap/model/Receipt;->getSku()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0}, Lcom/amazon/device/iap/model/Receipt;->isCanceled()Z

    move-result v3

    invoke-static {v3}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object v3

    invoke-static {v2, v3}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 338
    invoke-virtual {v0}, Lcom/amazon/device/iap/model/Receipt;->isCanceled()Z

    move-result v2

    if-nez v2, :cond_0

    .line 339
    iget-object v2, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->receipts:Ljava/util/Map;

    invoke-virtual {v0}, Lcom/amazon/device/iap/model/Receipt;->getSku()Ljava/lang/String;

    move-result-object v3

    invoke-interface {v2, v3, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_1

    .line 343
    .end local v0    # "r":Lcom/amazon/device/iap/model/Receipt;
    :cond_1
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->hasMore()Z

    move-result v1

    if-eqz v1, :cond_2

    .line 344
    const/4 v1, 0x0

    invoke-static {v1}, Lcom/amazon/device/iap/PurchasingService;->getPurchaseUpdates(Z)Lcom/amazon/device/iap/model/RequestId;

    goto :goto_0

    .line 346
    :cond_2
    iget-object v1, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->unityPurchasing:Lcom/unity/purchasing/common/IStoreCallback;

    invoke-direct {p0}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->getProductMetadata()Ljava/util/List;

    move-result-object v2

    invoke-interface {v1, v2}, Lcom/unity/purchasing/common/IStoreCallback;->OnProductsRetrieved(Ljava/util/List;)V

    goto :goto_0

    .line 322
    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_0
        :pswitch_1
        :pswitch_2
    .end packed-switch
.end method

.method public onUserDataResponse(Lcom/amazon/device/iap/model/UserDataResponse;)V
    .locals 5
    .param p1, "response"    # Lcom/amazon/device/iap/model/UserDataResponse;

    .prologue
    .line 354
    const-string v0, "Unity Purchasing Amazon"

    const-string v1, "onGetUserIdResponse:%s"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    invoke-virtual {p1}, Lcom/amazon/device/iap/model/UserDataResponse;->getUserData()Lcom/amazon/device/iap/model/UserData;

    move-result-object v4

    invoke-virtual {v4}, Lcom/amazon/device/iap/model/UserData;->getUserId()Ljava/lang/String;

    move-result-object v4

    aput-object v4, v2, v3

    invoke-static {v1, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 355
    sget-object v0, Lcom/unity/purchasing/amazon/AmazonPurchasing$4;->$SwitchMap$com$amazon$device$iap$model$UserDataResponse$RequestStatus:[I

    invoke-virtual {p1}, Lcom/amazon/device/iap/model/UserDataResponse;->getRequestStatus()Lcom/amazon/device/iap/model/UserDataResponse$RequestStatus;

    move-result-object v1

    invoke-virtual {v1}, Lcom/amazon/device/iap/model/UserDataResponse$RequestStatus;->ordinal()I

    move-result v1

    aget v0, v0, v1

    packed-switch v0, :pswitch_data_0

    .line 377
    :goto_0
    return-void

    .line 357
    :pswitch_0
    invoke-direct {p0}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->ResetDelay()V

    .line 358
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/UserDataResponse;->getUserData()Lcom/amazon/device/iap/model/UserData;

    move-result-object v0

    invoke-virtual {v0}, Lcom/amazon/device/iap/model/UserData;->getUserId()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->userId:Ljava/lang/String;

    .line 361
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/UserDataResponse;->getUserData()Lcom/amazon/device/iap/model/UserData;

    move-result-object v0

    invoke-virtual {v0}, Lcom/amazon/device/iap/model/UserData;->getMarketplace()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->ISO3266CountryCode:Ljava/lang/String;

    .line 363
    invoke-direct {p0}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->RefreshProducts()V

    goto :goto_0

    .line 366
    :pswitch_1
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing;->unityPurchasing:Lcom/unity/purchasing/common/IStoreCallback;

    sget-object v1, Lcom/unity/purchasing/common/InitializationFailureReason;->PurchasingUnavailable:Lcom/unity/purchasing/common/InitializationFailureReason;

    invoke-interface {v0, v1}, Lcom/unity/purchasing/common/IStoreCallback;->OnSetupFailed(Lcom/unity/purchasing/common/InitializationFailureReason;)V

    goto :goto_0

    .line 369
    :pswitch_2
    new-instance v0, Lcom/unity/purchasing/amazon/AmazonPurchasing$3;

    invoke-direct {v0, p0}, Lcom/unity/purchasing/amazon/AmazonPurchasing$3;-><init>(Lcom/unity/purchasing/amazon/AmazonPurchasing;)V

    invoke-direct {p0, v0}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->Retry(Ljava/lang/Runnable;)V

    goto :goto_0

    .line 355
    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_0
        :pswitch_1
        :pswitch_2
    .end packed-switch
.end method

.method public writeSandboxJSON(Ljava/lang/String;)V
    .locals 6
    .param p1, "productJSON"    # Ljava/lang/String;

    .prologue
    .line 94
    invoke-static {p1}, Lcom/unity/purchasing/common/StoreDeserializer;->DeserializeProducts(Ljava/lang/String;)Ljava/util/List;

    move-result-object v4

    invoke-static {v4}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->buildSandboxJSON(Ljava/util/List;)Ljava/lang/String;

    move-result-object v3

    .line 96
    .local v3, "sandboxJSON":Ljava/lang/String;
    :try_start_0
    new-instance v1, Ljava/io/File;

    invoke-static {}, Landroid/os/Environment;->getExternalStorageDirectory()Ljava/io/File;

    move-result-object v4

    const-string v5, "amazon.sdktester.json"

    invoke-direct {v1, v4, v5}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 97
    .local v1, "file":Ljava/io/File;
    new-instance v2, Ljava/io/FileOutputStream;

    invoke-direct {v2, v1}, Ljava/io/FileOutputStream;-><init>(Ljava/io/File;)V

    .line 98
    .local v2, "outputStream":Ljava/io/FileOutputStream;
    invoke-virtual {v3}, Ljava/lang/String;->getBytes()[B

    move-result-object v4

    invoke-virtual {v2, v4}, Ljava/io/FileOutputStream;->write([B)V

    .line 99
    invoke-virtual {v2}, Ljava/io/FileOutputStream;->close()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 104
    .end local v1    # "file":Ljava/io/File;
    .end local v2    # "outputStream":Ljava/io/FileOutputStream;
    :goto_0
    return-void

    .line 100
    :catch_0
    move-exception v0

    .line 101
    .local v0, "e":Ljava/lang/Exception;
    invoke-virtual {v0}, Ljava/lang/Exception;->printStackTrace()V

    .line 102
    const-string v4, "Unity Purchasing Amazon"

    const-string v5, "Unable to write sandbox JSON! Ensure your manifest has WRITE_EXTERNAL permission"

    invoke-static {v4, v5}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0
.end method
