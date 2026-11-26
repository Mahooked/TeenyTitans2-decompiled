.class public Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;
.super Ljava/lang/Object;
.source "AudienceManagerWorker.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/adobe/mobile/AudienceManagerWorker;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "SubmitSignalTask"
.end annotation


# instance fields
.field public final callback:Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback",
            "<",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;>;"
        }
    .end annotation
.end field

.field public final data:Ljava/util/Map;
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
.method public constructor <init>(Ljava/util/Map;Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;",
            "Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback",
            "<",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;>;)V"
        }
    .end annotation

    .prologue
    .line 142
    .local p1, "initData":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p2, "initCallback":Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback;, "Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback<Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;>;"
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 144
    iput-object p1, p0, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;->data:Ljava/util/Map;

    .line 145
    iput-object p2, p0, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;->callback:Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback;

    .line 146
    return-void
.end method


# virtual methods
.method public run()V
    .locals 10

    .prologue
    const/4 v8, 0x1

    .line 152
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 156
    .local v0, "callbackData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v6

    invoke-virtual {v6}, Lcom/adobe/mobile/MobileConfig;->mobileUsingAudienceManager()Z
    :try_end_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_2
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    move-result v6

    if-nez v6, :cond_1

    .line 199
    iget-object v6, p0, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;->callback:Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback;

    if-eqz v6, :cond_0

    .line 200
    new-instance v6, Ljava/lang/Thread;

    new-instance v7, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask$1;

    invoke-direct {v7, p0, v0}, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask$1;-><init>(Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;Ljava/util/HashMap;)V

    invoke-direct {v6, v7}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    .line 205
    invoke-virtual {v6}, Ljava/lang/Thread;->start()V

    .line 208
    :cond_0
    :goto_0
    return-void

    .line 161
    :cond_1
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v6

    invoke-virtual {v6}, Lcom/adobe/mobile/MobileConfig;->getPrivacyStatus()Lcom/adobe/mobile/MobilePrivacyStatus;

    move-result-object v6

    sget-object v7, Lcom/adobe/mobile/MobilePrivacyStatus;->MOBILE_PRIVACY_STATUS_OPT_OUT:Lcom/adobe/mobile/MobilePrivacyStatus;

    if-ne v6, v7, :cond_2

    .line 162
    const-string v6, "Audience Manager - Privacy status is set to opt out, no signals will be submitted."

    const/4 v7, 0x0

    new-array v7, v7, [Ljava/lang/Object;

    invoke-static {v6, v7}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_1
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_1 .. :try_end_1} :catch_0
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_2
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 199
    iget-object v6, p0, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;->callback:Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback;

    if-eqz v6, :cond_0

    .line 200
    new-instance v6, Ljava/lang/Thread;

    new-instance v7, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask$1;

    invoke-direct {v7, p0, v0}, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask$1;-><init>(Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;Ljava/util/HashMap;)V

    invoke-direct {v6, v7}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    .line 205
    invoke-virtual {v6}, Ljava/lang/Thread;->start()V

    goto :goto_0

    .line 166
    :cond_2
    :try_start_2
    iget-object v6, p0, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;->data:Ljava/util/Map;

    invoke-static {v6}, Lcom/adobe/mobile/AudienceManagerWorker;->access$400(Ljava/util/Map;)Ljava/lang/String;

    move-result-object v3

    .line 168
    .local v3, "requestUrl":Ljava/lang/String;
    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v6

    if-gt v6, v8, :cond_3

    .line 169
    const-string v6, "Audience Manager - Unable to create URL object"

    const/4 v7, 0x0

    new-array v7, v7, [Ljava/lang/Object;

    invoke-static {v6, v7}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_2
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_2 .. :try_end_2} :catch_0
    .catch Lorg/json/JSONException; {:try_start_2 .. :try_end_2} :catch_1
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 199
    iget-object v6, p0, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;->callback:Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback;

    if-eqz v6, :cond_0

    .line 200
    new-instance v6, Ljava/lang/Thread;

    new-instance v7, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask$1;

    invoke-direct {v7, p0, v0}, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask$1;-><init>(Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;Ljava/util/HashMap;)V

    invoke-direct {v6, v7}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    .line 205
    invoke-virtual {v6}, Ljava/lang/Thread;->start()V

    goto :goto_0

    .line 173
    :cond_3
    :try_start_3
    const-string v6, "Audience Manager - request (%s)"

    const/4 v7, 0x1

    new-array v7, v7, [Ljava/lang/Object;

    const/4 v8, 0x0

    aput-object v3, v7, v8

    invoke-static {v6, v7}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 175
    const/4 v6, 0x0

    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v7

    invoke-virtual {v7}, Lcom/adobe/mobile/MobileConfig;->getAamTimeout()I

    move-result v7

    mul-int/lit16 v7, v7, 0x3e8

    const-string v8, "Audience Manager"

    invoke-static {v3, v6, v7, v8}, Lcom/adobe/mobile/RequestHandler;->retrieveData(Ljava/lang/String;Ljava/util/Map;ILjava/lang/String;)[B

    move-result-object v5

    .line 176
    .local v5, "responseBytes":[B
    const-string v4, ""

    .line 178
    .local v4, "response":Ljava/lang/String;
    if-eqz v5, :cond_4

    array-length v6, v5

    if-lez v6, :cond_4

    .line 179
    new-instance v4, Ljava/lang/String;

    .end local v4    # "response":Ljava/lang/String;
    const-string v6, "UTF-8"

    invoke-direct {v4, v5, v6}, Ljava/lang/String;-><init>([BLjava/lang/String;)V

    .line 183
    .restart local v4    # "response":Ljava/lang/String;
    :cond_4
    new-instance v2, Lorg/json/JSONObject;

    invoke-direct {v2, v4}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 186
    .local v2, "jsonResponse":Lorg/json/JSONObject;
    invoke-static {v2}, Lcom/adobe/mobile/AudienceManagerWorker;->processJsonResponse(Lorg/json/JSONObject;)Ljava/util/HashMap;

    move-result-object v6

    invoke-virtual {v0, v6}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V
    :try_end_3
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_3 .. :try_end_3} :catch_0
    .catch Lorg/json/JSONException; {:try_start_3 .. :try_end_3} :catch_1
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_2
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    .line 199
    iget-object v6, p0, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;->callback:Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback;

    if-eqz v6, :cond_0

    .line 200
    new-instance v6, Ljava/lang/Thread;

    new-instance v7, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask$1;

    invoke-direct {v7, p0, v0}, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask$1;-><init>(Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;Ljava/util/HashMap;)V

    invoke-direct {v6, v7}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    .line 205
    invoke-virtual {v6}, Ljava/lang/Thread;->start()V

    goto/16 :goto_0

    .line 188
    .end local v2    # "jsonResponse":Lorg/json/JSONObject;
    .end local v3    # "requestUrl":Ljava/lang/String;
    .end local v4    # "response":Ljava/lang/String;
    .end local v5    # "responseBytes":[B
    :catch_0
    move-exception v1

    .line 189
    .local v1, "e":Ljava/io/UnsupportedEncodingException;
    :try_start_4
    const-string v6, "Audience Manager - Unable to decode server response (%s)"

    const/4 v7, 0x1

    new-array v7, v7, [Ljava/lang/Object;

    const/4 v8, 0x0

    invoke-virtual {v1}, Ljava/io/UnsupportedEncodingException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v9

    aput-object v9, v7, v8

    invoke-static {v6, v7}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    .line 199
    iget-object v6, p0, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;->callback:Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback;

    if-eqz v6, :cond_0

    .line 200
    new-instance v6, Ljava/lang/Thread;

    new-instance v7, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask$1;

    invoke-direct {v7, p0, v0}, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask$1;-><init>(Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;Ljava/util/HashMap;)V

    invoke-direct {v6, v7}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    .line 205
    invoke-virtual {v6}, Ljava/lang/Thread;->start()V

    goto/16 :goto_0

    .line 191
    .end local v1    # "e":Ljava/io/UnsupportedEncodingException;
    :catch_1
    move-exception v1

    .line 192
    .local v1, "e":Lorg/json/JSONException;
    :try_start_5
    const-string v6, "Audience Manager - Unable to parse JSON data (%s)"

    const/4 v7, 0x1

    new-array v7, v7, [Ljava/lang/Object;

    const/4 v8, 0x0

    invoke-virtual {v1}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v9

    aput-object v9, v7, v8

    invoke-static {v6, v7}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_0

    .line 199
    iget-object v6, p0, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;->callback:Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback;

    if-eqz v6, :cond_0

    .line 200
    new-instance v6, Ljava/lang/Thread;

    new-instance v7, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask$1;

    invoke-direct {v7, p0, v0}, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask$1;-><init>(Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;Ljava/util/HashMap;)V

    invoke-direct {v6, v7}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    .line 205
    invoke-virtual {v6}, Ljava/lang/Thread;->start()V

    goto/16 :goto_0

    .line 194
    .end local v1    # "e":Lorg/json/JSONException;
    :catch_2
    move-exception v1

    .line 195
    .local v1, "e":Ljava/lang/Exception;
    :try_start_6
    const-string v6, "Audience Manager - Unexpected error parsing result (%s)"

    const/4 v7, 0x1

    new-array v7, v7, [Ljava/lang/Object;

    const/4 v8, 0x0

    invoke-virtual {v1}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v9

    aput-object v9, v7, v8

    invoke-static {v6, v7}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_0

    .line 199
    iget-object v6, p0, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;->callback:Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback;

    if-eqz v6, :cond_0

    .line 200
    new-instance v6, Ljava/lang/Thread;

    new-instance v7, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask$1;

    invoke-direct {v7, p0, v0}, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask$1;-><init>(Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;Ljava/util/HashMap;)V

    invoke-direct {v6, v7}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    .line 205
    invoke-virtual {v6}, Ljava/lang/Thread;->start()V

    goto/16 :goto_0

    .line 199
    .end local v1    # "e":Ljava/lang/Exception;
    :catchall_0
    move-exception v6

    iget-object v7, p0, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;->callback:Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback;

    if-eqz v7, :cond_5

    .line 200
    new-instance v7, Ljava/lang/Thread;

    new-instance v8, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask$1;

    invoke-direct {v8, p0, v0}, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask$1;-><init>(Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;Ljava/util/HashMap;)V

    invoke-direct {v7, v8}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    .line 205
    invoke-virtual {v7}, Ljava/lang/Thread;->start()V

    :cond_5
    throw v6
.end method
