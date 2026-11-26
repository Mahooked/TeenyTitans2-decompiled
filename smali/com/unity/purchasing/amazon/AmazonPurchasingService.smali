.class public Lcom/unity/purchasing/amazon/AmazonPurchasingService;
.super Ljava/lang/Object;
.source "AmazonPurchasingService.java"

# interfaces
.implements Lcom/unity/purchasing/amazon/IPurchasingService;


# instance fields
.field private context:Landroid/content/Context;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0
    .param p1, "context"    # Landroid/content/Context;

    .prologue
    .line 16
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 17
    iput-object p1, p0, Lcom/unity/purchasing/amazon/AmazonPurchasingService;->context:Landroid/content/Context;

    .line 18
    return-void
.end method


# virtual methods
.method public getProductData(Ljava/util/Set;)Lcom/amazon/device/iap/model/RequestId;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Set",
            "<",
            "Ljava/lang/String;",
            ">;)",
            "Lcom/amazon/device/iap/model/RequestId;"
        }
    .end annotation

    .prologue
    .line 33
    .local p1, "skus":Ljava/util/Set;, "Ljava/util/Set<Ljava/lang/String;>;"
    invoke-static {p1}, Lcom/amazon/device/iap/PurchasingService;->getProductData(Ljava/util/Set;)Lcom/amazon/device/iap/model/RequestId;

    move-result-object v0

    return-object v0
.end method

.method public getPurchaseUpdates(Z)Lcom/amazon/device/iap/model/RequestId;
    .locals 1
    .param p1, "restoreAll"    # Z

    .prologue
    .line 37
    invoke-static {p1}, Lcom/amazon/device/iap/PurchasingService;->getPurchaseUpdates(Z)Lcom/amazon/device/iap/model/RequestId;

    move-result-object v0

    return-object v0
.end method

.method public getUserData()Lcom/amazon/device/iap/model/RequestId;
    .locals 1

    .prologue
    .line 25
    invoke-static {}, Lcom/amazon/device/iap/PurchasingService;->getUserData()Lcom/amazon/device/iap/model/RequestId;

    move-result-object v0

    return-object v0
.end method

.method public notifyFulfillment(Ljava/lang/String;Lcom/amazon/device/iap/model/FulfillmentResult;)V
    .locals 0
    .param p1, "transactionID"    # Ljava/lang/String;
    .param p2, "result"    # Lcom/amazon/device/iap/model/FulfillmentResult;

    .prologue
    .line 41
    invoke-static {p1, p2}, Lcom/amazon/device/iap/PurchasingService;->notifyFulfillment(Ljava/lang/String;Lcom/amazon/device/iap/model/FulfillmentResult;)V

    .line 42
    return-void
.end method

.method public purchase(Ljava/lang/String;)Lcom/amazon/device/iap/model/RequestId;
    .locals 1
    .param p1, "sku"    # Ljava/lang/String;

    .prologue
    .line 29
    invoke-static {p1}, Lcom/amazon/device/iap/PurchasingService;->purchase(Ljava/lang/String;)Lcom/amazon/device/iap/model/RequestId;

    move-result-object v0

    return-object v0
.end method

.method public registerListener(Lcom/amazon/device/iap/PurchasingListener;)V
    .locals 1
    .param p1, "listener"    # Lcom/amazon/device/iap/PurchasingListener;

    .prologue
    .line 21
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasingService;->context:Landroid/content/Context;

    invoke-static {v0, p1}, Lcom/amazon/device/iap/PurchasingService;->registerListener(Landroid/content/Context;Lcom/amazon/device/iap/PurchasingListener;)V

    .line 22
    return-void
.end method
