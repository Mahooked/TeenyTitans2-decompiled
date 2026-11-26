.class final Lcom/adobe/mobile/Messages$3;
.super Ljava/lang/Object;
.source "Messages.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/adobe/mobile/Messages;->checkForPIIRequests(Ljava/util/Map;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$data:Ljava/util/Map;


# direct methods
.method constructor <init>(Ljava/util/Map;)V
    .locals 0

    .prologue
    .line 135
    iput-object p1, p0, Lcom/adobe/mobile/Messages$3;->val$data:Ljava/util/Map;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 10

    .prologue
    .line 138
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v6

    invoke-virtual {v6}, Lcom/adobe/mobile/MobileConfig;->getPiiRequests()Ljava/util/ArrayList;

    move-result-object v5

    .line 141
    .local v5, "piiRequests":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Lcom/adobe/mobile/Message;>;"
    if-eqz v5, :cond_0

    invoke-virtual {v5}, Ljava/util/ArrayList;->size()I

    move-result v6

    if-gtz v6, :cond_1

    .line 168
    :cond_0
    :goto_0
    return-void

    .line 145
    :cond_1
    new-instance v1, Ljava/util/concurrent/FutureTask;

    new-instance v6, Lcom/adobe/mobile/Messages$3$1;

    invoke-direct {v6, p0}, Lcom/adobe/mobile/Messages$3$1;-><init>(Lcom/adobe/mobile/Messages$3;)V

    invoke-direct {v1, v6}, Ljava/util/concurrent/FutureTask;-><init>(Ljava/util/concurrent/Callable;)V

    .line 153
    .local v1, "f":Ljava/util/concurrent/FutureTask;, "Ljava/util/concurrent/FutureTask<Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;>;"
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAnalyticsExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v6

    invoke-interface {v6, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 156
    :try_start_0
    invoke-virtual {v1}, Ljava/util/concurrent/FutureTask;->get()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/Map;

    .line 157
    .local v2, "lifecycleData":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    iget-object v6, p0, Lcom/adobe/mobile/Messages$3;->val$data:Ljava/util/Map;

    invoke-static {v6}, Lcom/adobe/mobile/Messages;->lowercaseKeysForMap(Ljava/util/Map;)Ljava/util/HashMap;

    move-result-object v3

    .line 159
    .local v3, "lowercaseVars":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-virtual {v5}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v6

    :cond_2
    :goto_1
    invoke-interface {v6}, Ljava/util/Iterator;->hasNext()Z

    move-result v7

    if-eqz v7, :cond_0

    invoke-interface {v6}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/adobe/mobile/Message;

    .line 160
    .local v4, "message":Lcom/adobe/mobile/Message;
    const/4 v7, 0x0

    invoke-virtual {v4, v3, v7, v2}, Lcom/adobe/mobile/Message;->shouldShowForVariables(Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;)Z

    move-result v7

    if-eqz v7, :cond_2

    .line 161
    invoke-virtual {v4}, Lcom/adobe/mobile/Message;->show()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    .line 165
    .end local v2    # "lifecycleData":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .end local v3    # "lowercaseVars":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    .end local v4    # "message":Lcom/adobe/mobile/Message;
    :catch_0
    move-exception v0

    .line 166
    .local v0, "e":Ljava/lang/Exception;
    const-string v6, "Lifecycle - Unable to get context data (%s)"

    const/4 v7, 0x1

    new-array v7, v7, [Ljava/lang/Object;

    const/4 v8, 0x0

    invoke-virtual {v0}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object v9

    aput-object v9, v7, v8

    invoke-static {v6, v7}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method
