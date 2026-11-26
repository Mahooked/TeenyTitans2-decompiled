.class public final Lcom/adobe/mobile/Target;
.super Ljava/lang/Object;
.source "Target.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/adobe/mobile/Target$TargetCallback;
    }
.end annotation


# static fields
.field private static final NO_TARGET_MESSAGE:Ljava/lang/String; = "Target - ADBMobile is not configured correctly to use Target."


# direct methods
.method public constructor <init>()V
    .locals 0

    .prologue
    .line 25
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 30
    return-void
.end method

.method public static clearCookies()V
    .locals 2

    .prologue
    .line 138
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAnalyticsExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    new-instance v1, Lcom/adobe/mobile/Target$5;

    invoke-direct {v1}, Lcom/adobe/mobile/Target$5;-><init>()V

    invoke-interface {v0, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 149
    return-void
.end method

.method public static createOrderConfirmRequest(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)Lcom/adobe/mobile/TargetLocationRequest;
    .locals 1
    .param p0, "name"    # Ljava/lang/String;
    .param p1, "orderId"    # Ljava/lang/String;
    .param p2, "orderTotal"    # Ljava/lang/String;
    .param p3, "productPurchasedId"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/adobe/mobile/TargetLocationRequest;"
        }
    .end annotation

    .prologue
    .line 61
    .local p4, "parameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-static {p0, p1, p2, p3, p4}, Lcom/adobe/mobile/TargetLocationRequest;->createRequestWithOrderConfirm(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)Lcom/adobe/mobile/TargetLocationRequest;

    move-result-object v0

    return-object v0
.end method

.method public static createRequest(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)Lcom/adobe/mobile/TargetLocationRequest;
    .locals 1
    .param p0, "name"    # Ljava/lang/String;
    .param p1, "defaultContent"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/adobe/mobile/TargetLocationRequest;"
        }
    .end annotation

    .prologue
    .line 56
    .local p2, "parameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    new-instance v0, Lcom/adobe/mobile/TargetLocationRequest;

    invoke-direct {v0, p0, p1, p2}, Lcom/adobe/mobile/TargetLocationRequest;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

    return-object v0
.end method

.method public static getPcID()Ljava/lang/String;
    .locals 6

    .prologue
    .line 66
    new-instance v1, Ljava/util/concurrent/FutureTask;

    new-instance v2, Lcom/adobe/mobile/Target$1;

    invoke-direct {v2}, Lcom/adobe/mobile/Target$1;-><init>()V

    invoke-direct {v1, v2}, Ljava/util/concurrent/FutureTask;-><init>(Ljava/util/concurrent/Callable;)V

    .line 73
    .local v1, "f":Ljava/util/concurrent/FutureTask;, "Ljava/util/concurrent/FutureTask<Ljava/lang/String;>;"
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAnalyticsExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v2

    invoke-interface {v2, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 76
    :try_start_0
    invoke-virtual {v1}, Ljava/util/concurrent/FutureTask;->get()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 80
    :goto_0
    return-object v2

    .line 78
    :catch_0
    move-exception v0

    .line 79
    .local v0, "e":Ljava/lang/Exception;
    const-string v2, "Target - Unable to get PcID (%s)"

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    invoke-virtual {v0}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v3, v4

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 80
    const/4 v2, 0x0

    goto :goto_0
.end method

.method public static getSessionID()Ljava/lang/String;
    .locals 6

    .prologue
    .line 86
    new-instance v1, Ljava/util/concurrent/FutureTask;

    new-instance v2, Lcom/adobe/mobile/Target$2;

    invoke-direct {v2}, Lcom/adobe/mobile/Target$2;-><init>()V

    invoke-direct {v1, v2}, Ljava/util/concurrent/FutureTask;-><init>(Ljava/util/concurrent/Callable;)V

    .line 93
    .local v1, "f":Ljava/util/concurrent/FutureTask;, "Ljava/util/concurrent/FutureTask<Ljava/lang/String;>;"
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAnalyticsExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v2

    invoke-interface {v2, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 96
    :try_start_0
    invoke-virtual {v1}, Ljava/util/concurrent/FutureTask;->get()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 100
    :goto_0
    return-object v2

    .line 98
    :catch_0
    move-exception v0

    .line 99
    .local v0, "e":Ljava/lang/Exception;
    const-string v2, "Target - Unable to get SessionID (%s)"

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    invoke-virtual {v0}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v3, v4

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 100
    const/4 v2, 0x0

    goto :goto_0
.end method

.method public static getThirdPartyID()Ljava/lang/String;
    .locals 6

    .prologue
    .line 106
    new-instance v1, Ljava/util/concurrent/FutureTask;

    new-instance v2, Lcom/adobe/mobile/Target$3;

    invoke-direct {v2}, Lcom/adobe/mobile/Target$3;-><init>()V

    invoke-direct {v1, v2}, Ljava/util/concurrent/FutureTask;-><init>(Ljava/util/concurrent/Callable;)V

    .line 113
    .local v1, "f":Ljava/util/concurrent/FutureTask;, "Ljava/util/concurrent/FutureTask<Ljava/lang/String;>;"
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAnalyticsExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v2

    invoke-interface {v2, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 116
    :try_start_0
    invoke-virtual {v1}, Ljava/util/concurrent/FutureTask;->get()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 120
    :goto_0
    return-object v2

    .line 118
    :catch_0
    move-exception v0

    .line 119
    .local v0, "e":Ljava/lang/Exception;
    const-string v2, "Target - Unable to get ThirdPartyID (%s)"

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    invoke-virtual {v0}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v3, v4

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 120
    const/4 v2, 0x0

    goto :goto_0
.end method

.method public static loadRequest(Lcom/adobe/mobile/TargetLocationRequest;Lcom/adobe/mobile/Target$TargetCallback;)V
    .locals 2
    .param p0, "request"    # Lcom/adobe/mobile/TargetLocationRequest;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/adobe/mobile/TargetLocationRequest;",
            "Lcom/adobe/mobile/Target$TargetCallback",
            "<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .prologue
    .line 36
    .local p1, "callback":Lcom/adobe/mobile/Target$TargetCallback;, "Lcom/adobe/mobile/Target$TargetCallback<Ljava/lang/String;>;"
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->isWearableApp()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 37
    const-string v0, "Target - Method loadRequest is not available for Wearable"

    const/4 v1, 0x0

    new-array v1, v1, [Ljava/lang/Object;

    invoke-static {v0, v1}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 42
    :goto_0
    return-void

    .line 41
    :cond_0
    invoke-static {p0, p1}, Lcom/adobe/mobile/TargetWorker;->loadRequest(Lcom/adobe/mobile/TargetLocationRequest;Lcom/adobe/mobile/Target$TargetCallback;)V

    goto :goto_0
.end method

.method public static loadRequest(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;Lcom/adobe/mobile/Target$TargetCallback;)V
    .locals 2
    .param p0, "name"    # Ljava/lang/String;
    .param p1, "defaultContent"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;",
            "Lcom/adobe/mobile/Target$TargetCallback",
            "<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .prologue
    .line 47
    .local p2, "profileParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p3, "orderParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p4, "mboxParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p5, "callback":Lcom/adobe/mobile/Target$TargetCallback;, "Lcom/adobe/mobile/Target$TargetCallback<Ljava/lang/String;>;"
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->isWearableApp()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 48
    const-string v0, "Target - Method loadRequest is not available for Wearable"

    const/4 v1, 0x0

    new-array v1, v1, [Ljava/lang/Object;

    invoke-static {v0, v1}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 52
    :goto_0
    return-void

    .line 51
    :cond_0
    invoke-static/range {p0 .. p5}, Lcom/adobe/mobile/TargetWorker;->loadRequest(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;Lcom/adobe/mobile/Target$TargetCallback;)V

    goto :goto_0
.end method

.method public static setThirdPartyID(Ljava/lang/String;)V
    .locals 2
    .param p0, "thirdPartyId"    # Ljava/lang/String;

    .prologue
    .line 126
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAnalyticsExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    new-instance v1, Lcom/adobe/mobile/Target$4;

    invoke-direct {v1, p0}, Lcom/adobe/mobile/Target$4;-><init>(Ljava/lang/String;)V

    invoke-interface {v0, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 132
    return-void
.end method
