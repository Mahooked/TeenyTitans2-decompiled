.class Lcom/adobe/mobile/AnalyticsWorker$1;
.super Ljava/lang/Object;
.source "AnalyticsWorker.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/adobe/mobile/AnalyticsWorker;->workerThread()Ljava/lang/Runnable;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/adobe/mobile/AnalyticsWorker;


# direct methods
.method constructor <init>(Lcom/adobe/mobile/AnalyticsWorker;)V
    .locals 0
    .param p1, "this$0"    # Lcom/adobe/mobile/AnalyticsWorker;

    .prologue
    .line 224
    iput-object p1, p0, Lcom/adobe/mobile/AnalyticsWorker$1;->this$0:Lcom/adobe/mobile/AnalyticsWorker;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 28

    .prologue
    .line 227
    invoke-static {}, Lcom/adobe/mobile/AnalyticsWorker;->sharedInstance()Lcom/adobe/mobile/AnalyticsWorker;

    move-result-object v21

    .line 230
    .local v21, "worker":Lcom/adobe/mobile/AnalyticsWorker;
    const/16 v22, 0xa

    invoke-static/range {v22 .. v22}, Landroid/os/Process;->setThreadPriority(I)V

    .line 233
    new-instance v10, Ljava/util/HashMap;

    invoke-direct {v10}, Ljava/util/HashMap;-><init>()V

    .line 235
    .local v10, "headers":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    const-string v22, "Accept-Language"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getDefaultAcceptLanguage()Ljava/lang/String;

    move-result-object v23

    move-object/from16 v0, v22

    move-object/from16 v1, v23

    invoke-interface {v10, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 236
    const-string v22, "User-Agent"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getDefaultUserAgent()Ljava/lang/String;

    move-result-object v23

    move-object/from16 v0, v22

    move-object/from16 v1, v23

    invoke-interface {v10, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 239
    :cond_0
    :goto_0
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v22

    invoke-virtual/range {v22 .. v22}, Lcom/adobe/mobile/MobileConfig;->getPrivacyStatus()Lcom/adobe/mobile/MobilePrivacyStatus;

    move-result-object v22

    sget-object v23, Lcom/adobe/mobile/MobilePrivacyStatus;->MOBILE_PRIVACY_STATUS_OPT_IN:Lcom/adobe/mobile/MobilePrivacyStatus;

    move-object/from16 v0, v22

    move-object/from16 v1, v23

    if-ne v0, v1, :cond_1

    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v22

    invoke-virtual/range {v22 .. v22}, Lcom/adobe/mobile/MobileConfig;->networkConnectivity()Z

    move-result v22

    if-eqz v22, :cond_1

    .line 241
    invoke-virtual/range {v21 .. v21}, Lcom/adobe/mobile/AnalyticsWorker;->selectOldestHit()Lcom/adobe/mobile/AbstractHitDatabase$Hit;

    move-result-object v11

    .line 243
    .local v11, "hit":Lcom/adobe/mobile/AbstractHitDatabase$Hit;
    if-nez v11, :cond_2

    .line 346
    .end local v11    # "hit":Lcom/adobe/mobile/AbstractHitDatabase$Hit;
    :cond_1
    :goto_1
    const/16 v22, 0x0

    move/from16 v0, v22

    move-object/from16 v1, v21

    iput-boolean v0, v1, Lcom/adobe/mobile/AnalyticsWorker;->bgThreadActive:Z

    .line 347
    return-void

    .line 248
    .restart local v11    # "hit":Lcom/adobe/mobile/AbstractHitDatabase$Hit;
    :cond_2
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v22

    invoke-virtual/range {v22 .. v22}, Lcom/adobe/mobile/MobileConfig;->getOfflineTrackingEnabled()Z

    move-result v22

    if-eqz v22, :cond_4

    .line 250
    iget-wide v0, v11, Lcom/adobe/mobile/AbstractHitDatabase$Hit;->timestamp:J

    move-wide/from16 v22, v0

    move-object/from16 v0, v21

    iget-wide v0, v0, Lcom/adobe/mobile/AnalyticsWorker;->lastHitTimestamp:J

    move-wide/from16 v24, v0

    sub-long v6, v22, v24

    .line 253
    .local v6, "delta":J
    const-wide/16 v22, 0x0

    cmp-long v22, v6, v22

    if-gez v22, :cond_3

    .line 255
    move-object/from16 v0, v21

    iget-wide v0, v0, Lcom/adobe/mobile/AnalyticsWorker;->lastHitTimestamp:J

    move-wide/from16 v22, v0

    const-wide/16 v24, 0x1

    add-long v16, v22, v24

    .line 256
    .local v16, "newTimestamp":J
    new-instance v22, Ljava/lang/StringBuilder;

    invoke-direct/range {v22 .. v22}, Ljava/lang/StringBuilder;-><init>()V

    const-string v23, "&ts="

    invoke-virtual/range {v22 .. v23}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v22

    iget-wide v0, v11, Lcom/adobe/mobile/AbstractHitDatabase$Hit;->timestamp:J

    move-wide/from16 v24, v0

    invoke-static/range {v24 .. v25}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object v23

    invoke-virtual/range {v22 .. v23}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v22

    invoke-virtual/range {v22 .. v22}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v15

    .line 257
    .local v15, "oldTSString":Ljava/lang/String;
    new-instance v22, Ljava/lang/StringBuilder;

    invoke-direct/range {v22 .. v22}, Ljava/lang/StringBuilder;-><init>()V

    const-string v23, "&ts="

    invoke-virtual/range {v22 .. v23}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v22

    invoke-static/range {v16 .. v17}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object v23

    invoke-virtual/range {v22 .. v23}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v22

    invoke-virtual/range {v22 .. v22}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v14

    .line 259
    .local v14, "newTSString":Ljava/lang/String;
    iget-object v0, v11, Lcom/adobe/mobile/AbstractHitDatabase$Hit;->urlFragment:Ljava/lang/String;

    move-object/from16 v22, v0

    move-object/from16 v0, v22

    invoke-virtual {v0, v15, v14}, Ljava/lang/String;->replaceFirst(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v22

    move-object/from16 v0, v22

    iput-object v0, v11, Lcom/adobe/mobile/AbstractHitDatabase$Hit;->urlFragment:Ljava/lang/String;

    .line 261
    const-string v22, "Analytics - Adjusting out of order hit timestamp(%d->%d)"

    const/16 v23, 0x2

    move/from16 v0, v23

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v23, v0

    const/16 v24, 0x0

    iget-wide v0, v11, Lcom/adobe/mobile/AbstractHitDatabase$Hit;->timestamp:J

    move-wide/from16 v26, v0

    invoke-static/range {v26 .. v27}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v25

    aput-object v25, v23, v24

    const/16 v24, 0x1

    invoke-static/range {v16 .. v17}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v25

    aput-object v25, v23, v24

    invoke-static/range {v22 .. v23}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 263
    move-wide/from16 v0, v16

    iput-wide v0, v11, Lcom/adobe/mobile/AbstractHitDatabase$Hit;->timestamp:J

    .line 288
    .end local v6    # "delta":J
    .end local v14    # "newTSString":Ljava/lang/String;
    .end local v15    # "oldTSString":Ljava/lang/String;
    .end local v16    # "newTimestamp":J
    :cond_3
    iget-object v0, v11, Lcom/adobe/mobile/AbstractHitDatabase$Hit;->urlFragment:Ljava/lang/String;

    move-object/from16 v22, v0

    const-string v23, "ndh"

    invoke-virtual/range {v22 .. v23}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v22

    if-eqz v22, :cond_5

    iget-object v0, v11, Lcom/adobe/mobile/AbstractHitDatabase$Hit;->urlFragment:Ljava/lang/String;

    move-object/from16 v18, v0

    .line 291
    .local v18, "postBody":Ljava/lang/String;
    :goto_2
    new-instance v22, Ljava/lang/StringBuilder;

    invoke-direct/range {v22 .. v22}, Ljava/lang/StringBuilder;-><init>()V

    invoke-static {}, Lcom/adobe/mobile/AnalyticsWorker;->access$000()Ljava/lang/String;

    move-result-object v23

    invoke-virtual/range {v22 .. v23}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v22

    invoke-static {}, Lcom/adobe/mobile/AnalyticsWorker;->access$100()Ljava/security/SecureRandom;

    move-result-object v23

    const v24, 0x5f5e100

    invoke-virtual/range {v23 .. v24}, Ljava/security/SecureRandom;->nextInt(I)I

    move-result v23

    invoke-virtual/range {v22 .. v23}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v22

    invoke-virtual/range {v22 .. v22}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v22

    const/16 v23, 0x1388

    move-object/from16 v0, p0

    iget-object v0, v0, Lcom/adobe/mobile/AnalyticsWorker$1;->this$0:Lcom/adobe/mobile/AnalyticsWorker;

    move-object/from16 v24, v0

    move-object/from16 v0, v24

    iget-object v0, v0, Lcom/adobe/mobile/AnalyticsWorker;->logPrefix:Ljava/lang/String;

    move-object/from16 v24, v0

    move-object/from16 v0, v22

    move-object/from16 v1, v18

    move/from16 v2, v23

    move-object/from16 v3, v24

    invoke-static {v0, v1, v10, v2, v3}, Lcom/adobe/mobile/RequestHandler;->retrieveAnalyticsRequestData(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;ILjava/lang/String;)[B

    move-result-object v19

    .line 293
    .local v19, "response":[B
    if-nez v19, :cond_6

    .line 295
    const-wide/16 v4, 0x1e

    .line 336
    .local v4, "delay":J
    const/4 v12, 0x0

    .local v12, "i":I
    :goto_3
    int-to-long v0, v12

    move-wide/from16 v22, v0

    cmp-long v22, v22, v4

    if-gez v22, :cond_0

    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v22

    invoke-virtual/range {v22 .. v22}, Lcom/adobe/mobile/MobileConfig;->networkConnectivity()Z

    move-result v22

    if-eqz v22, :cond_0

    .line 337
    const-wide/16 v22, 0x3e8

    invoke-static/range {v22 .. v23}, Ljava/lang/Thread;->sleep(J)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_5

    .line 336
    add-int/lit8 v12, v12, 0x1

    goto :goto_3

    .line 267
    .end local v4    # "delay":J
    .end local v12    # "i":I
    .end local v18    # "postBody":Ljava/lang/String;
    .end local v19    # "response":[B
    :cond_4
    iget-wide v0, v11, Lcom/adobe/mobile/AbstractHitDatabase$Hit;->timestamp:J

    move-wide/from16 v22, v0

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getTimeSince1970()J

    move-result-wide v24

    const-wide/16 v26, 0x3c

    sub-long v24, v24, v26

    cmp-long v22, v22, v24

    if-gez v22, :cond_3

    .line 273
    :try_start_1
    iget-object v0, v11, Lcom/adobe/mobile/AbstractHitDatabase$Hit;->identifier:Ljava/lang/String;

    move-object/from16 v22, v0

    invoke-virtual/range {v21 .. v22}, Lcom/adobe/mobile/AnalyticsWorker;->deleteHit(Ljava/lang/String;)V
    :try_end_1
    .catch Lcom/adobe/mobile/AbstractDatabaseBacking$CorruptedDatabaseException; {:try_start_1 .. :try_end_1} :catch_0

    goto/16 :goto_0

    .line 275
    :catch_0
    move-exception v9

    .line 276
    .local v9, "ex":Lcom/adobe/mobile/AbstractDatabaseBacking$CorruptedDatabaseException;
    invoke-static {}, Lcom/adobe/mobile/AnalyticsWorker;->sharedInstance()Lcom/adobe/mobile/AnalyticsWorker;

    move-result-object v22

    move-object/from16 v0, v22

    invoke-virtual {v0, v9}, Lcom/adobe/mobile/AnalyticsWorker;->resetDatabase(Ljava/lang/Exception;)V

    goto/16 :goto_1

    .line 288
    .end local v9    # "ex":Lcom/adobe/mobile/AbstractDatabaseBacking$CorruptedDatabaseException;
    :cond_5
    iget-object v0, v11, Lcom/adobe/mobile/AbstractHitDatabase$Hit;->urlFragment:Ljava/lang/String;

    move-object/from16 v22, v0

    iget-object v0, v11, Lcom/adobe/mobile/AbstractHitDatabase$Hit;->urlFragment:Ljava/lang/String;

    move-object/from16 v23, v0

    const/16 v24, 0x3f

    invoke-virtual/range {v23 .. v24}, Ljava/lang/String;->indexOf(I)I

    move-result v23

    add-int/lit8 v23, v23, 0x1

    invoke-virtual/range {v22 .. v23}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v18

    goto/16 :goto_2

    .line 296
    .restart local v18    # "postBody":Ljava/lang/String;
    .restart local v19    # "response":[B
    :cond_6
    move-object/from16 v0, v19

    array-length v0, v0

    move/from16 v22, v0

    const/16 v23, 0x1

    move/from16 v0, v22

    move/from16 v1, v23

    if-le v0, v1, :cond_7

    .line 299
    :try_start_2
    iget-object v0, v11, Lcom/adobe/mobile/AbstractHitDatabase$Hit;->identifier:Ljava/lang/String;

    move-object/from16 v22, v0

    invoke-virtual/range {v21 .. v22}, Lcom/adobe/mobile/AnalyticsWorker;->deleteHit(Ljava/lang/String;)V

    .line 300
    iget-wide v0, v11, Lcom/adobe/mobile/AbstractHitDatabase$Hit;->timestamp:J

    move-wide/from16 v22, v0

    move-wide/from16 v0, v22

    move-object/from16 v2, v21

    iput-wide v0, v2, Lcom/adobe/mobile/AnalyticsWorker;->lastHitTimestamp:J

    .line 302
    new-instance v20, Ljava/lang/String;

    const-string v22, "UTF-8"

    move-object/from16 v0, v20

    move-object/from16 v1, v19

    move-object/from16 v2, v22

    invoke-direct {v0, v1, v2}, Ljava/lang/String;-><init>([BLjava/lang/String;)V

    .line 303
    .local v20, "responseString":Ljava/lang/String;
    new-instance v13, Lorg/json/JSONObject;

    move-object/from16 v0, v20

    invoke-direct {v13, v0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 306
    .local v13, "jsonResponse":Lorg/json/JSONObject;
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAudienceExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v22

    new-instance v23, Lcom/adobe/mobile/AnalyticsWorker$1$1;

    move-object/from16 v0, v23

    move-object/from16 v1, p0

    invoke-direct {v0, v1, v13}, Lcom/adobe/mobile/AnalyticsWorker$1$1;-><init>(Lcom/adobe/mobile/AnalyticsWorker$1;Lorg/json/JSONObject;)V

    invoke-interface/range {v22 .. v23}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V
    :try_end_2
    .catch Lcom/adobe/mobile/AbstractDatabaseBacking$CorruptedDatabaseException; {:try_start_2 .. :try_end_2} :catch_1
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_2 .. :try_end_2} :catch_2
    .catch Lorg/json/JSONException; {:try_start_2 .. :try_end_2} :catch_3

    goto/16 :goto_0

    .line 312
    .end local v13    # "jsonResponse":Lorg/json/JSONObject;
    .end local v20    # "responseString":Ljava/lang/String;
    :catch_1
    move-exception v9

    .line 313
    .restart local v9    # "ex":Lcom/adobe/mobile/AbstractDatabaseBacking$CorruptedDatabaseException;
    invoke-static {}, Lcom/adobe/mobile/AnalyticsWorker;->sharedInstance()Lcom/adobe/mobile/AnalyticsWorker;

    move-result-object v22

    move-object/from16 v0, v22

    invoke-virtual {v0, v9}, Lcom/adobe/mobile/AnalyticsWorker;->resetDatabase(Ljava/lang/Exception;)V

    goto/16 :goto_1

    .line 315
    .end local v9    # "ex":Lcom/adobe/mobile/AbstractDatabaseBacking$CorruptedDatabaseException;
    :catch_2
    move-exception v8

    .line 316
    .local v8, "e":Ljava/io/UnsupportedEncodingException;
    const-string v22, "Audience Manager - Unable to decode server response (%s)"

    const/16 v23, 0x1

    move/from16 v0, v23

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v23, v0

    const/16 v24, 0x0

    invoke-virtual {v8}, Ljava/io/UnsupportedEncodingException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v25

    aput-object v25, v23, v24

    invoke-static/range {v22 .. v23}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 317
    .end local v8    # "e":Ljava/io/UnsupportedEncodingException;
    :catch_3
    move-exception v8

    .line 318
    .local v8, "e":Lorg/json/JSONException;
    const-string v22, "Audience Manager - Unable to parse JSON data (%s)"

    const/16 v23, 0x1

    move/from16 v0, v23

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v23, v0

    const/16 v24, 0x0

    invoke-virtual {v8}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v25

    aput-object v25, v23, v24

    invoke-static/range {v22 .. v23}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 324
    .end local v8    # "e":Lorg/json/JSONException;
    :cond_7
    :try_start_3
    iget-object v0, v11, Lcom/adobe/mobile/AbstractHitDatabase$Hit;->identifier:Ljava/lang/String;

    move-object/from16 v22, v0

    invoke-virtual/range {v21 .. v22}, Lcom/adobe/mobile/AnalyticsWorker;->deleteHit(Ljava/lang/String;)V

    .line 325
    iget-wide v0, v11, Lcom/adobe/mobile/AbstractHitDatabase$Hit;->timestamp:J

    move-wide/from16 v22, v0

    move-wide/from16 v0, v22

    move-object/from16 v2, v21

    iput-wide v0, v2, Lcom/adobe/mobile/AnalyticsWorker;->lastHitTimestamp:J
    :try_end_3
    .catch Lcom/adobe/mobile/AbstractDatabaseBacking$CorruptedDatabaseException; {:try_start_3 .. :try_end_3} :catch_4

    goto/16 :goto_0

    .line 327
    :catch_4
    move-exception v9

    .line 328
    .restart local v9    # "ex":Lcom/adobe/mobile/AbstractDatabaseBacking$CorruptedDatabaseException;
    invoke-static {}, Lcom/adobe/mobile/AnalyticsWorker;->sharedInstance()Lcom/adobe/mobile/AnalyticsWorker;

    move-result-object v22

    move-object/from16 v0, v22

    invoke-virtual {v0, v9}, Lcom/adobe/mobile/AnalyticsWorker;->resetDatabase(Ljava/lang/Exception;)V

    goto/16 :goto_1

    .line 340
    .end local v9    # "ex":Lcom/adobe/mobile/AbstractDatabaseBacking$CorruptedDatabaseException;
    .restart local v4    # "delay":J
    .restart local v12    # "i":I
    :catch_5
    move-exception v8

    .line 341
    .local v8, "e":Ljava/lang/Exception;
    const-string v22, "Analytics - Background Thread Interrupted(%s)"

    const/16 v23, 0x1

    move/from16 v0, v23

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v23, v0

    const/16 v24, 0x0

    invoke-virtual {v8}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object v25

    aput-object v25, v23, v24

    invoke-static/range {v22 .. v23}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0
.end method
