.class Lcom/unity/purchasing/amazon/AmazonPurchasing$1;
.super Ljava/lang/Object;
.source "AmazonPurchasing.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/unity/purchasing/amazon/AmazonPurchasing;->onProductDataResponse(Lcom/amazon/device/iap/model/ProductDataResponse;)V
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
    .line 139
    iput-object p1, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing$1;->this$0:Lcom/unity/purchasing/amazon/AmazonPurchasing;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .prologue
    .line 142
    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing$1;->this$0:Lcom/unity/purchasing/amazon/AmazonPurchasing;

    invoke-static {v0}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->access$100(Lcom/unity/purchasing/amazon/AmazonPurchasing;)Lcom/unity/purchasing/amazon/IPurchasingService;

    move-result-object v1

    iget-object v0, p0, Lcom/unity/purchasing/amazon/AmazonPurchasing$1;->this$0:Lcom/unity/purchasing/amazon/AmazonPurchasing;

    invoke-static {v0}, Lcom/unity/purchasing/amazon/AmazonPurchasing;->access$000(Lcom/unity/purchasing/amazon/AmazonPurchasing;)Ljava/util/LinkedList;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/LinkedList;->peek()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/Set;

    invoke-interface {v1, v0}, Lcom/unity/purchasing/amazon/IPurchasingService;->getProductData(Ljava/util/Set;)Lcom/amazon/device/iap/model/RequestId;

    .line 143
    return-void
.end method
