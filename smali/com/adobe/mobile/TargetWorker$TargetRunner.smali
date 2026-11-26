.class Lcom/adobe/mobile/TargetWorker$TargetRunner;
.super Ljava/lang/Object;
.source "TargetWorker.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/adobe/mobile/TargetWorker;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "TargetRunner"
.end annotation


# instance fields
.field private callback:Lcom/adobe/mobile/Target$TargetCallback;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/adobe/mobile/Target$TargetCallback",
            "<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private defaultContent:Ljava/lang/String;

.field private mboxName:Ljava/lang/String;

.field private mboxParameters:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field

.field private orderParameters:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field

.field private profileParameters:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method private constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;Lcom/adobe/mobile/Target$TargetCallback;)V
    .locals 0
    .param p1, "newMboxName"    # Ljava/lang/String;
    .param p2, "newDefaultContent"    # Ljava/lang/String;
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
    .line 363
    .local p3, "newProfileParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p4, "newOrderParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p5, "newMboxParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p6, "newCallback":Lcom/adobe/mobile/Target$TargetCallback;, "Lcom/adobe/mobile/Target$TargetCallback<Ljava/lang/String;>;"
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 365
    iput-object p1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->mboxName:Ljava/lang/String;

    .line 366
    iput-object p2, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->defaultContent:Ljava/lang/String;

    .line 367
    iput-object p3, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->profileParameters:Ljava/util/Map;

    .line 368
    iput-object p4, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->orderParameters:Ljava/util/Map;

    .line 369
    iput-object p5, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->mboxParameters:Ljava/util/Map;

    .line 370
    iput-object p6, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    .line 371
    return-void
.end method

.method synthetic constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;Lcom/adobe/mobile/Target$TargetCallback;Lcom/adobe/mobile/TargetWorker$1;)V
    .locals 0
    .param p1, "x0"    # Ljava/lang/String;
    .param p2, "x1"    # Ljava/lang/String;
    .param p3, "x2"    # Ljava/util/Map;
    .param p4, "x3"    # Ljava/util/Map;
    .param p5, "x4"    # Ljava/util/Map;
    .param p6, "x5"    # Lcom/adobe/mobile/Target$TargetCallback;
    .param p7, "x6"    # Lcom/adobe/mobile/TargetWorker$1;

    .prologue
    .line 349
    invoke-direct/range {p0 .. p6}, Lcom/adobe/mobile/TargetWorker$TargetRunner;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;Lcom/adobe/mobile/Target$TargetCallback;)V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 15

    .prologue
    const/4 v5, 0x2

    const/4 v14, 0x1

    const/4 v13, 0x0

    .line 377
    invoke-static {}, Lcom/adobe/mobile/TargetWorker;->setTntIdAndSessionFromOldCookieValues()V

    .line 379
    invoke-static {}, Lcom/adobe/mobile/TargetWorker;->access$100()Ljava/lang/String;

    move-result-object v0

    .line 381
    .local v0, "requestUrl":Ljava/lang/String;
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->mboxName:Ljava/lang/String;

    iget-object v2, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->profileParameters:Ljava/util/Map;

    iget-object v3, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->orderParameters:Ljava/util/Map;

    iget-object v4, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->mboxParameters:Ljava/util/Map;

    invoke-static {v1, v2, v3, v4}, Lcom/adobe/mobile/TargetWorker;->access$200(Ljava/lang/String;Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;)Lorg/json/JSONObject;

    move-result-object v11

    .line 382
    .local v11, "parametersJson":Lorg/json/JSONObject;
    if-nez v11, :cond_1

    .line 383
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    if-eqz v1, :cond_0

    .line 384
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    iget-object v2, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->defaultContent:Ljava/lang/String;

    invoke-interface {v1, v2}, Lcom/adobe/mobile/Target$TargetCallback;->call(Ljava/lang/Object;)V

    .line 480
    :cond_0
    :goto_0
    return-void

    .line 389
    :cond_1
    const-string v1, "Target - requesting content from url \"%s\" with parameters: %s"

    new-array v2, v5, [Ljava/lang/Object;

    aput-object v0, v2, v13

    invoke-virtual {v11}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v3

    aput-object v3, v2, v14

    invoke-static {v1, v2}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 392
    const-string v1, "POST"

    invoke-virtual {v11}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v3

    invoke-virtual {v3}, Lcom/adobe/mobile/MobileConfig;->getDefaultLocationTimeout()I

    move-result v3

    const-string v4, "application/json"

    const-string v5, "Target"

    invoke-static/range {v0 .. v5}, Lcom/adobe/mobile/RequestHandler;->retrieveNetworkObject(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ILjava/lang/String;Ljava/lang/String;)Lcom/adobe/mobile/NetworkObject;

    move-result-object v10

    .line 395
    .local v10, "networkObject":Lcom/adobe/mobile/NetworkObject;
    if-nez v10, :cond_2

    .line 396
    const-string v1, "Target - unable to open connection (%s)"

    new-array v2, v14, [Ljava/lang/Object;

    aput-object v0, v2, v13

    invoke-static {v1, v2}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 397
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    if-eqz v1, :cond_0

    .line 398
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    iget-object v2, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->defaultContent:Ljava/lang/String;

    invoke-interface {v1, v2}, Lcom/adobe/mobile/Target$TargetCallback;->call(Ljava/lang/Object;)V

    goto :goto_0

    .line 404
    :cond_2
    iget-object v1, v10, Lcom/adobe/mobile/NetworkObject;->response:Ljava/lang/String;

    if-eqz v1, :cond_3

    iget-object v1, v10, Lcom/adobe/mobile/NetworkObject;->response:Ljava/lang/String;

    invoke-virtual {v1}, Ljava/lang/String;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_4

    .line 405
    :cond_3
    const-string v1, "Target - Response was empty"

    new-array v2, v13, [Ljava/lang/Object;

    invoke-static {v1, v2}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 406
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    if-eqz v1, :cond_0

    .line 407
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    iget-object v2, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->defaultContent:Ljava/lang/String;

    invoke-interface {v1, v2}, Lcom/adobe/mobile/Target$TargetCallback;->call(Ljava/lang/Object;)V

    goto :goto_0

    .line 413
    :cond_4
    :try_start_0
    iget v1, v10, Lcom/adobe/mobile/NetworkObject;->responseCode:I

    const/16 v2, 0xc8

    if-ne v1, v2, :cond_8

    .line 416
    new-instance v12, Lorg/json/JSONObject;

    iget-object v1, v10, Lcom/adobe/mobile/NetworkObject;->response:Ljava/lang/String;

    invoke-direct {v12, v1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 419
    .local v12, "responseJson":Lorg/json/JSONObject;
    invoke-static {v12}, Lcom/adobe/mobile/TargetWorker;->access$300(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v8

    .line 420
    .local v8, "errorMessage":Ljava/lang/String;
    if-eqz v8, :cond_5

    invoke-virtual {v8}, Ljava/lang/String;->isEmpty()Z

    move-result v1

    if-nez v1, :cond_5

    .line 421
    const-string v1, "Target - An error was reported by the server (%s)"

    const/4 v2, 0x0

    new-array v2, v2, [Ljava/lang/Object;

    invoke-static {v1, v2}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 422
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    if-eqz v1, :cond_0

    .line 423
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    iget-object v2, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->defaultContent:Ljava/lang/String;

    invoke-interface {v1, v2}, Lcom/adobe/mobile/Target$TargetCallback;->call(Ljava/lang/Object;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/lang/NullPointerException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_2

    goto/16 :goto_0

    .line 465
    .end local v8    # "errorMessage":Ljava/lang/String;
    .end local v12    # "responseJson":Lorg/json/JSONObject;
    :catch_0
    move-exception v9

    .line 466
    .local v9, "ex":Lorg/json/JSONException;
    const-string v1, "Target - JSONException while trying to get content (%s)"

    new-array v2, v14, [Ljava/lang/Object;

    invoke-virtual {v9}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v3

    aput-object v3, v2, v13

    invoke-static {v1, v2}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 467
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    if-eqz v1, :cond_0

    .line 468
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    iget-object v2, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->defaultContent:Ljava/lang/String;

    invoke-interface {v1, v2}, Lcom/adobe/mobile/Target$TargetCallback;->call(Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 429
    .end local v9    # "ex":Lorg/json/JSONException;
    .restart local v8    # "errorMessage":Ljava/lang/String;
    .restart local v12    # "responseJson":Lorg/json/JSONObject;
    :cond_5
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v7

    .line 430
    .local v7, "editor":Landroid/content/SharedPreferences$Editor;
    const-string v1, "ADBMOBILE_TARGET_LAST_TIMESTAMP"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getTimeSince1970()J

    move-result-wide v2

    invoke-interface {v7, v1, v2, v3}, Landroid/content/SharedPreferences$Editor;->putLong(Ljava/lang/String;J)Landroid/content/SharedPreferences$Editor;

    .line 431
    invoke-interface {v7}, Landroid/content/SharedPreferences$Editor;->commit()Z

    .line 434
    invoke-static {v12}, Lcom/adobe/mobile/TargetWorker;->access$400(Lorg/json/JSONObject;)V

    .line 437
    invoke-static {v12}, Lcom/adobe/mobile/TargetWorker;->access$500(Lorg/json/JSONObject;)V

    .line 440
    const-string v1, "content"

    invoke-virtual {v12, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    .line 443
    .local v6, "content":Ljava/lang/String;
    if-eqz v6, :cond_6

    invoke-virtual {v6}, Ljava/lang/String;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_7

    .line 444
    :cond_6
    const-string v1, "Target - Content was empty"

    const/4 v2, 0x0

    new-array v2, v2, [Ljava/lang/Object;

    invoke-static {v1, v2}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 445
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    if-eqz v1, :cond_0

    .line 446
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    iget-object v2, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->defaultContent:Ljava/lang/String;

    invoke-interface {v1, v2}, Lcom/adobe/mobile/Target$TargetCallback;->call(Ljava/lang/Object;)V
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0
    .catch Ljava/lang/NullPointerException; {:try_start_1 .. :try_end_1} :catch_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_2

    goto/16 :goto_0

    .line 471
    .end local v6    # "content":Ljava/lang/String;
    .end local v7    # "editor":Landroid/content/SharedPreferences$Editor;
    .end local v8    # "errorMessage":Ljava/lang/String;
    .end local v12    # "responseJson":Lorg/json/JSONObject;
    :catch_1
    move-exception v9

    .line 472
    .local v9, "ex":Ljava/lang/NullPointerException;
    const-string v1, "Target - NullPointerException while trying to get content (%s)"

    new-array v2, v14, [Ljava/lang/Object;

    invoke-virtual {v9}, Ljava/lang/NullPointerException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v3

    aput-object v3, v2, v13

    invoke-static {v1, v2}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 473
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    if-eqz v1, :cond_0

    .line 474
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    iget-object v2, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->defaultContent:Ljava/lang/String;

    invoke-interface {v1, v2}, Lcom/adobe/mobile/Target$TargetCallback;->call(Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 452
    .end local v9    # "ex":Ljava/lang/NullPointerException;
    .restart local v6    # "content":Ljava/lang/String;
    .restart local v7    # "editor":Landroid/content/SharedPreferences$Editor;
    .restart local v8    # "errorMessage":Ljava/lang/String;
    .restart local v12    # "responseJson":Lorg/json/JSONObject;
    :cond_7
    :try_start_2
    const-string v1, "Target - Response received for location \"%s\" - \"%s\""

    const/4 v2, 0x2

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    iget-object v4, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->mboxName:Ljava/lang/String;

    aput-object v4, v2, v3

    const/4 v3, 0x1

    aput-object v6, v2, v3

    invoke-static {v1, v2}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 453
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    if-eqz v1, :cond_0

    .line 454
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    invoke-interface {v1, v6}, Lcom/adobe/mobile/Target$TargetCallback;->call(Ljava/lang/Object;)V
    :try_end_2
    .catch Lorg/json/JSONException; {:try_start_2 .. :try_end_2} :catch_0
    .catch Ljava/lang/NullPointerException; {:try_start_2 .. :try_end_2} :catch_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_2 .. :try_end_2} :catch_2

    goto/16 :goto_0

    .line 477
    .end local v6    # "content":Ljava/lang/String;
    .end local v7    # "editor":Landroid/content/SharedPreferences$Editor;
    .end local v8    # "errorMessage":Ljava/lang/String;
    .end local v12    # "responseJson":Lorg/json/JSONObject;
    :catch_2
    move-exception v9

    .line 478
    .local v9, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v1, "Target - Error retrieving shared preferences - application context is null"

    new-array v2, v13, [Ljava/lang/Object;

    invoke-static {v1, v2}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 459
    .end local v9    # "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :cond_8
    :try_start_3
    const-string v1, "Target - Response code from Target server (%d) was invalid, returning default content"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    iget v4, v10, Lcom/adobe/mobile/NetworkObject;->responseCode:I

    invoke-static {v4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    aput-object v4, v2, v3

    invoke-static {v1, v2}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 460
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    if-eqz v1, :cond_0

    .line 461
    iget-object v1, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->callback:Lcom/adobe/mobile/Target$TargetCallback;

    iget-object v2, p0, Lcom/adobe/mobile/TargetWorker$TargetRunner;->defaultContent:Ljava/lang/String;

    invoke-interface {v1, v2}, Lcom/adobe/mobile/Target$TargetCallback;->call(Ljava/lang/Object;)V
    :try_end_3
    .catch Lorg/json/JSONException; {:try_start_3 .. :try_end_3} :catch_0
    .catch Ljava/lang/NullPointerException; {:try_start_3 .. :try_end_3} :catch_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_3 .. :try_end_3} :catch_2

    goto/16 :goto_0
.end method
