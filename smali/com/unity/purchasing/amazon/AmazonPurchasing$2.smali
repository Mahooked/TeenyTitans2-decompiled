.class Lcom/unity/purchasing/amazon/AmazonPurchasing$2;
.super Ljava/lang/Object;
.source "AmazonPurchasing.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/unity/purchasing/amazon/AmazonPurchasing;->onPurchaseUpdatesResponse(Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/unity/purchasing/amazon/AmazonPurchasing;


# direct methods
.method constructor <init>(Lcom/unity/purchasing/amazon/AmazonPurchasing;)V
    .locals 0
    .param p1, "this$0"    # Lcom/unity/purchasing/amazon/AmazonPurchasing;

    .prologue
    .line 327
    iput-object p1, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing$2;->this$0:Lcom/unity/purchasing/amazon/AmazonPurchasing;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .prologue
    .line 330
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing$2;->this$0:Lcom/unity/purchasing/amazon/AmazonPurchasing;

    invoke-static {v0}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->access$100(Lcom/unity/purchasing/amazon/AmazonPurchasing;)Lcom/unity/purchasing/amazon/IPurchasingService;

    move-result-object v0

    const/4 v1, 0x0

    invoke-interface {v0, v1}, Lcom/unity/purchasing/amazon/IPurchasingService;->getPurchaseUpdates(Z)Lcom/amazon/device/iap/model/RequestId;

    .line 331
    return-void
.end method
