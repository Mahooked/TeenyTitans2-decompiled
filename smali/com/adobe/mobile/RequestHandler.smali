.class final Lcom/adobe/mobile/RequestHandler;
.super Ljava/lang/Object;
.source "RequestHandler.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/adobe/mobile/RequestHandler$HeaderCallback;
    }
.end annotation


# static fields
.field private static final BUF_SIZE:I = 0x400

.field private static final CONNECTION_TIMEOUT:I = 0x7d0

.field private static final MAX_REDIRECT_COUNT:I = 0x15

.field private static final TO_MILLI:I = 0x3e8


# direct methods
.method constructor <init>()V
    .locals 0

    .prologue
    .line 33
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 39
    return-void
.end method

.method protected static requestConnect(Ljava/lang/String;)Ljava/net/HttpURLConnection;
    .locals 5
    .param p0, "url"    # Ljava/lang/String;

    .prologue
    .line 392
    :try_start_0
    new-instance v1, Ljava/net/URL;

    invoke-direct {v1, p0}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    invoke-virtual {v1}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v1

    check-cast v1, Ljava/net/HttpURLConnection;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 396
    :goto_0
    return-object v1

    .line 393
    :catch_0
    move-exception v0

    .line 394
    .local v0, "e":Ljava/lang/Exception;
    const-string v1, "Adobe Mobile - Exception opening URL(%s)"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    invoke-virtual {v0}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v4

    aput-object v4, v2, v3

    invoke-static {v1, v2}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 396
    const/4 v1, 0x0

    goto :goto_0
.end method

.method protected static retrieveAnalyticsRequestData(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;ILjava/lang/String;)[B
    .locals 17
    .param p0, "url"    # Ljava/lang/String;
    .param p1, "postBody"    # Ljava/lang/String;
    .param p3, "timeout"    # I
    .param p4, "logPrefix"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;I",
            "Ljava/lang/String;",
            ")[B"
        }
    .end annotation

    .prologue
    .line 240
    .local p2, "headers":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    if-nez p0, :cond_1

    .line 241
    const/4 v13, 0x0

    .line 314
    :cond_0
    :goto_0
    return-object v13

    .line 244
    :cond_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->isWearableApp()Z

    move-result v13

    if-eqz v13, :cond_2

    .line 245
    move-object/from16 v0, p0

    move-object/from16 v1, p1

    move/from16 v2, p3

    move-object/from16 v3, p4

    invoke-static {v0, v1, v2, v3}, Lcom/adobe/mobile/WearableFunctionBridge;->retrieveAnalyticsRequestData(Ljava/lang/String;Ljava/lang/String;ILjava/lang/String;)[B

    move-result-object v13

    goto :goto_0

    .line 248
    :cond_2
    invoke-static/range {p0 .. p0}, Lcom/adobe/mobile/RequestHandler;->requestConnect(Ljava/lang/String;)Ljava/net/HttpURLConnection;

    move-result-object v6

    .line 250
    .local v6, "connection":Ljava/net/HttpURLConnection;
    if-nez v6, :cond_3

    .line 251
    const/4 v13, 0x0

    goto :goto_0

    .line 256
    :cond_3
    :try_start_0
    move/from16 v0, p3

    invoke-virtual {v6, v0}, Ljava/net/HttpURLConnection;->setConnectTimeout(I)V

    .line 257
    move/from16 v0, p3

    invoke-virtual {v6, v0}, Ljava/net/HttpURLConnection;->setReadTimeout(I)V

    .line 258
    const-string v13, "POST"

    invoke-virtual {v6, v13}, Ljava/net/HttpURLConnection;->setRequestMethod(Ljava/lang/String;)V

    .line 259
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v13

    invoke-virtual {v13}, Lcom/adobe/mobile/MobileConfig;->getSSL()Z

    move-result v13

    if-nez v13, :cond_4

    .line 260
    const-string v13, "connection"

    const-string v14, "close"

    invoke-virtual {v6, v13, v14}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 263
    :cond_4
    const-string v13, "UTF-8"

    move-object/from16 v0, p1

    invoke-virtual {v0, v13}, Ljava/lang/String;->getBytes(Ljava/lang/String;)[B

    move-result-object v11

    .line 264
    .local v11, "outputBytes":[B
    array-length v13, v11

    invoke-virtual {v6, v13}, Ljava/net/HttpURLConnection;->setFixedLengthStreamingMode(I)V

    .line 265
    const-string v13, "Content-Type"

    const-string v14, "application/x-www-form-urlencoded"

    invoke-virtual {v6, v13, v14}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 267
    if-eqz p2, :cond_5

    .line 269
    invoke-interface/range {p2 .. p2}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v13

    invoke-interface {v13}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v15

    :goto_1
    invoke-interface {v15}, Ljava/util/Iterator;->hasNext()Z

    move-result v13

    if-eqz v13, :cond_5

    invoke-interface {v15}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v8

    check-cast v8, Ljava/util/Map$Entry;

    .line 270
    .local v8, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/String;>;"
    invoke-interface {v8}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v13

    check-cast v13, Ljava/lang/String;

    invoke-interface {v8}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v14

    check-cast v14, Ljava/lang/String;

    invoke-virtual {v6, v13, v14}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/net/SocketTimeoutException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_2
    .catch Ljava/lang/Error; {:try_start_0 .. :try_end_0} :catch_3
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_1

    .line 300
    .end local v8    # "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/String;>;"
    .end local v11    # "outputBytes":[B
    :catch_0
    move-exception v7

    .line 301
    .local v7, "e":Ljava/net/SocketTimeoutException;
    :try_start_1
    const-string v13, "%s - Timed out sending request(%s)"

    const/4 v14, 0x2

    new-array v14, v14, [Ljava/lang/Object;

    const/4 v15, 0x0

    aput-object p4, v14, v15

    const/4 v15, 0x1

    aput-object p1, v14, v15

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 302
    const/4 v13, 0x0

    .line 313
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v14

    invoke-virtual {v14}, Lcom/adobe/mobile/MobileConfig;->getSSL()Z

    move-result v14

    if-nez v14, :cond_0

    .line 314
    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->disconnect()V

    goto/16 :goto_0

    .line 275
    .end local v7    # "e":Ljava/net/SocketTimeoutException;
    .restart local v11    # "outputBytes":[B
    :cond_5
    :try_start_2
    new-instance v12, Ljava/io/BufferedOutputStream;

    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->getOutputStream()Ljava/io/OutputStream;

    move-result-object v13

    invoke-direct {v12, v13}, Ljava/io/BufferedOutputStream;-><init>(Ljava/io/OutputStream;)V

    .line 276
    .local v12, "postBodyStream":Ljava/io/OutputStream;
    invoke-virtual {v12, v11}, Ljava/io/OutputStream;->write([B)V

    .line 277
    invoke-virtual {v12}, Ljava/io/OutputStream;->close()V

    .line 279
    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object v9

    .line 280
    .local v9, "inputStream":Ljava/io/InputStream;
    const/16 v13, 0x400

    new-array v5, v13, [B

    .line 281
    .local v5, "buffer":[B
    new-instance v4, Ljava/io/ByteArrayOutputStream;

    invoke-direct {v4}, Ljava/io/ByteArrayOutputStream;-><init>()V

    .line 283
    .local v4, "baos":Ljava/io/ByteArrayOutputStream;
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v13

    invoke-virtual {v13}, Lcom/adobe/mobile/MobileConfig;->getSSL()Z

    move-result v13

    if-nez v13, :cond_6

    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result v13

    const/16 v14, 0xc8

    if-ne v13, v14, :cond_7

    .line 286
    :cond_6
    :goto_2
    invoke-virtual {v9, v5}, Ljava/io/InputStream;->read([B)I

    move-result v10

    .line 287
    .local v10, "len":I
    const/4 v13, -0x1

    if-ne v10, v13, :cond_8

    .line 294
    .end local v10    # "len":I
    :cond_7
    invoke-virtual {v9}, Ljava/io/InputStream;->close()V

    .line 296
    const-string v13, "%s - Request Sent(%s)"

    const/4 v14, 0x2

    new-array v14, v14, [Ljava/lang/Object;

    const/4 v15, 0x0

    aput-object p4, v14, v15

    const/4 v15, 0x1

    aput-object p1, v14, v15

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 299
    invoke-virtual {v4}, Ljava/io/ByteArrayOutputStream;->toByteArray()[B
    :try_end_2
    .catch Ljava/net/SocketTimeoutException; {:try_start_2 .. :try_end_2} :catch_0
    .catch Ljava/io/IOException; {:try_start_2 .. :try_end_2} :catch_1
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_2
    .catch Ljava/lang/Error; {:try_start_2 .. :try_end_2} :catch_3
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    move-result-object v13

    .line 313
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v14

    invoke-virtual {v14}, Lcom/adobe/mobile/MobileConfig;->getSSL()Z

    move-result v14

    if-nez v14, :cond_0

    .line 314
    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->disconnect()V

    goto/16 :goto_0

    .line 290
    .restart local v10    # "len":I
    :cond_8
    const/4 v13, 0x0

    :try_start_3
    invoke-virtual {v4, v5, v13, v10}, Ljava/io/ByteArrayOutputStream;->write([BII)V
    :try_end_3
    .catch Ljava/net/SocketTimeoutException; {:try_start_3 .. :try_end_3} :catch_0
    .catch Ljava/io/IOException; {:try_start_3 .. :try_end_3} :catch_1
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_2
    .catch Ljava/lang/Error; {:try_start_3 .. :try_end_3} :catch_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    goto :goto_2

    .line 303
    .end local v4    # "baos":Ljava/io/ByteArrayOutputStream;
    .end local v5    # "buffer":[B
    .end local v9    # "inputStream":Ljava/io/InputStream;
    .end local v10    # "len":I
    .end local v11    # "outputBytes":[B
    .end local v12    # "postBodyStream":Ljava/io/OutputStream;
    :catch_1
    move-exception v7

    .line 304
    .local v7, "e":Ljava/io/IOException;
    :try_start_4
    const-string v13, "%s - IOException while sending request, may retry(%s)"

    const/4 v14, 0x2

    new-array v14, v14, [Ljava/lang/Object;

    const/4 v15, 0x0

    aput-object p4, v14, v15

    const/4 v15, 0x1

    invoke-virtual {v7}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v16

    aput-object v16, v14, v15

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    .line 305
    const/4 v13, 0x0

    .line 313
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v14

    invoke-virtual {v14}, Lcom/adobe/mobile/MobileConfig;->getSSL()Z

    move-result v14

    if-nez v14, :cond_0

    .line 314
    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->disconnect()V

    goto/16 :goto_0

    .line 306
    .end local v7    # "e":Ljava/io/IOException;
    :catch_2
    move-exception v7

    .line 307
    .local v7, "e":Ljava/lang/Exception;
    :try_start_5
    const-string v13, "%s - Exception while attempting to send hit, will not retry(%s)"

    const/4 v14, 0x2

    new-array v14, v14, [Ljava/lang/Object;

    const/4 v15, 0x0

    aput-object p4, v14, v15

    const/4 v15, 0x1

    invoke-virtual {v7}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v16

    aput-object v16, v14, v15

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 308
    const/4 v13, 0x0

    new-array v13, v13, [B
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_0

    .line 313
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v14

    invoke-virtual {v14}, Lcom/adobe/mobile/MobileConfig;->getSSL()Z

    move-result v14

    if-nez v14, :cond_0

    .line 314
    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->disconnect()V

    goto/16 :goto_0

    .line 309
    .end local v7    # "e":Ljava/lang/Exception;
    :catch_3
    move-exception v7

    .line 310
    .local v7, "e":Ljava/lang/Error;
    :try_start_6
    const-string v13, "%s - Exception while attempting to send hit, will not retry(%s)"

    const/4 v14, 0x2

    new-array v14, v14, [Ljava/lang/Object;

    const/4 v15, 0x0

    aput-object p4, v14, v15

    const/4 v15, 0x1

    invoke-virtual {v7}, Ljava/lang/Error;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v16

    aput-object v16, v14, v15

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 311
    const/4 v13, 0x0

    new-array v13, v13, [B
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_0

    .line 313
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v14

    invoke-virtual {v14}, Lcom/adobe/mobile/MobileConfig;->getSSL()Z

    move-result v14

    if-nez v14, :cond_0

    .line 314
    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->disconnect()V

    goto/16 :goto_0

    .line 313
    .end local v7    # "e":Ljava/lang/Error;
    :catchall_0
    move-exception v13

    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v14

    invoke-virtual {v14}, Lcom/adobe/mobile/MobileConfig;->getSSL()Z

    move-result v14

    if-nez v14, :cond_9

    .line 314
    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->disconnect()V

    :cond_9
    throw v13
.end method

.method protected static retrieveData(Ljava/lang/String;ILjava/lang/String;Ljava/util/concurrent/Callable;Lcom/adobe/mobile/RequestHandler$HeaderCallback;)[B
    .locals 22
    .param p0, "url"    # Ljava/lang/String;
    .param p1, "readTimeout"    # I
    .param p2, "source"    # Ljava/lang/String;
    .param p4, "responseHeaderReceiver"    # Lcom/adobe/mobile/RequestHandler$HeaderCallback;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "I",
            "Ljava/lang/String;",
            "Ljava/util/concurrent/Callable",
            "<",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;>;",
            "Lcom/adobe/mobile/RequestHandler$HeaderCallback;",
            ")[B"
        }
    .end annotation

    .prologue
    .line 79
    .local p3, "requestHeaderProvider":Ljava/util/concurrent/Callable;, "Ljava/util/concurrent/Callable<Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;>;"
    const/4 v6, 0x0

    .line 80
    .local v6, "connection":Ljava/net/HttpURLConnection;
    const/4 v10, 0x0

    .line 84
    .local v10, "inStream":Ljava/io/InputStream;
    move-object/from16 v2, p0

    .line 85
    .local v2, "_url":Ljava/lang/String;
    const/16 v16, 0x0

    .line 86
    .local v16, "responseCode":I
    const/4 v14, 0x0

    .line 91
    .local v14, "redirectCount":I
    :goto_0
    const/16 v17, 0x15

    move/from16 v0, v17

    if-le v14, v0, :cond_2

    .line 92
    :try_start_0
    const-string v17, "%s - Too many redirects for (%s) - %d"

    const/16 v18, 0x3

    move/from16 v0, v18

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v18, v0

    const/16 v19, 0x0

    aput-object p2, v18, v19

    const/16 v19, 0x1

    aput-object p0, v18, v19

    const/16 v19, 0x2

    invoke-static {v14}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v20

    aput-object v20, v18, v19

    invoke-static/range {v17 .. v18}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 142
    :goto_1
    const/16 v17, 0xc8

    move/from16 v0, v16

    move/from16 v1, v17

    if-ne v0, v1, :cond_8

    .line 143
    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object v10

    .line 145
    const/16 v17, 0x400

    move/from16 v0, v17

    new-array v5, v0, [B

    .line 147
    .local v5, "buffer":[B
    new-instance v3, Ljava/io/ByteArrayOutputStream;

    invoke-direct {v3}, Ljava/io/ByteArrayOutputStream;-><init>()V

    .line 151
    .local v3, "baos":Ljava/io/ByteArrayOutputStream;
    :goto_2
    invoke-virtual {v10, v5}, Ljava/io/InputStream;->read([B)I

    move-result v11

    .line 152
    .local v11, "len":I
    const/16 v17, -0x1

    move/from16 v0, v17

    if-ne v11, v0, :cond_6

    .line 159
    invoke-virtual {v10}, Ljava/io/InputStream;->close()V

    .line 162
    invoke-virtual {v3}, Ljava/io/ByteArrayOutputStream;->toByteArray()[B
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_2
    .catch Ljava/lang/Error; {:try_start_0 .. :try_end_0} :catch_6
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    move-result-object v17

    .line 174
    if-eqz v6, :cond_0

    .line 175
    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->disconnect()V

    .line 178
    :cond_0
    if-eqz v10, :cond_1

    .line 180
    :try_start_1
    invoke-virtual {v10}, Ljava/io/InputStream;->close()V
    :try_end_1
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_4

    .line 187
    .end local v3    # "baos":Ljava/io/ByteArrayOutputStream;
    .end local v5    # "buffer":[B
    .end local v11    # "len":I
    :cond_1
    :goto_3
    return-object v17

    .line 97
    :cond_2
    :try_start_2
    new-instance v17, Ljava/net/URL;

    move-object/from16 v0, v17

    invoke-direct {v0, v2}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    invoke-virtual/range {v17 .. v17}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v17

    move-object/from16 v0, v17

    check-cast v0, Ljava/net/HttpURLConnection;

    move-object v6, v0

    .line 98
    const/16 v17, 0x7d0

    move/from16 v0, v17

    invoke-virtual {v6, v0}, Ljava/net/HttpURLConnection;->setConnectTimeout(I)V

    .line 99
    move/from16 v0, p1

    invoke-virtual {v6, v0}, Ljava/net/HttpURLConnection;->setReadTimeout(I)V

    .line 100
    const/16 v17, 0x0

    move/from16 v0, v17

    invoke-virtual {v6, v0}, Ljava/net/HttpURLConnection;->setInstanceFollowRedirects(Z)V

    .line 103
    const-string v17, "Accept-Language"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getDefaultAcceptLanguage()Ljava/lang/String;

    move-result-object v18

    move-object/from16 v0, v17

    move-object/from16 v1, v18

    invoke-virtual {v6, v0, v1}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 104
    const-string v17, "User-Agent"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getDefaultUserAgent()Ljava/lang/String;

    move-result-object v18

    move-object/from16 v0, v17

    move-object/from16 v1, v18

    invoke-virtual {v6, v0, v1}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 107
    if-eqz p3, :cond_4

    .line 108
    invoke-interface/range {p3 .. p3}, Ljava/util/concurrent/Callable;->call()Ljava/lang/Object;

    move-result-object v15

    check-cast v15, Ljava/util/Map;

    .line 110
    .local v15, "requestHeaders":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    if-eqz v15, :cond_4

    .line 111
    invoke-interface {v15}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v17

    invoke-interface/range {v17 .. v17}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v19

    :goto_4
    invoke-interface/range {v19 .. v19}, Ljava/util/Iterator;->hasNext()Z

    move-result v17

    if-eqz v17, :cond_4

    invoke-interface/range {v19 .. v19}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Ljava/util/Map$Entry;

    .line 112
    .local v7, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/String;>;"
    invoke-interface {v7}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v17

    check-cast v17, Ljava/lang/String;

    invoke-interface {v7}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v18

    check-cast v18, Ljava/lang/String;

    move-object/from16 v0, v17

    move-object/from16 v1, v18

    invoke-virtual {v6, v0, v1}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_2
    .catch Ljava/io/IOException; {:try_start_2 .. :try_end_2} :catch_0
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_2
    .catch Ljava/lang/Error; {:try_start_2 .. :try_end_2} :catch_6
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    goto :goto_4

    .line 164
    .end local v7    # "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/String;>;"
    .end local v15    # "requestHeaders":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    :catch_0
    move-exception v9

    .line 165
    .local v9, "ex":Ljava/io/IOException;
    :try_start_3
    const-string v17, "%s - IOException while sending request (%s)"

    const/16 v18, 0x2

    move/from16 v0, v18

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v18, v0

    const/16 v19, 0x0

    aput-object p2, v18, v19

    const/16 v19, 0x1

    invoke-virtual {v9}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v20

    aput-object v20, v18, v19

    invoke-static/range {v17 .. v18}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    .line 166
    const/16 v17, 0x0

    .line 174
    if-eqz v6, :cond_3

    .line 175
    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->disconnect()V

    .line 178
    :cond_3
    if-eqz v10, :cond_1

    .line 180
    :try_start_4
    invoke-virtual {v10}, Ljava/io/InputStream;->close()V
    :try_end_4
    .catch Ljava/io/IOException; {:try_start_4 .. :try_end_4} :catch_1

    goto/16 :goto_3

    .line 181
    :catch_1
    move-exception v9

    .line 182
    const-string v18, "%s - Unable to close stream (%s)"

    const/16 v19, 0x2

    move/from16 v0, v19

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v19, v0

    const/16 v20, 0x0

    aput-object p2, v19, v20

    const/16 v20, 0x1

    invoke-virtual {v9}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v21

    aput-object v21, v19, v20

    invoke-static/range {v18 .. v19}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_3

    .line 118
    .end local v9    # "ex":Ljava/io/IOException;
    :cond_4
    :try_start_5
    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result v16

    .line 121
    if-eqz p4, :cond_5

    .line 122
    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->getHeaderFields()Ljava/util/Map;

    move-result-object v17

    move-object/from16 v0, p4

    move-object/from16 v1, v17

    invoke-interface {v0, v1}, Lcom/adobe/mobile/RequestHandler$HeaderCallback;->call(Ljava/util/Map;)V

    .line 126
    :cond_5
    packed-switch v16, :pswitch_data_0

    goto/16 :goto_1

    .line 129
    :pswitch_0
    add-int/lit8 v14, v14, 0x1

    .line 131
    const-string v17, "Location"

    move-object/from16 v0, v17

    invoke-virtual {v6, v0}, Ljava/net/HttpURLConnection;->getHeaderField(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v12

    .line 132
    .local v12, "location":Ljava/lang/String;
    new-instance v4, Ljava/net/URL;

    invoke-direct {v4, v2}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    .line 133
    .local v4, "base":Ljava/net/URL;
    new-instance v13, Ljava/net/URL;

    invoke-direct {v13, v4, v12}, Ljava/net/URL;-><init>(Ljava/net/URL;Ljava/lang/String;)V

    .line 134
    .local v13, "next":Ljava/net/URL;
    invoke-virtual {v13}, Ljava/net/URL;->toExternalForm()Ljava/lang/String;

    move-result-object v2

    .line 135
    goto/16 :goto_0

    .line 155
    .end local v4    # "base":Ljava/net/URL;
    .end local v12    # "location":Ljava/lang/String;
    .end local v13    # "next":Ljava/net/URL;
    .restart local v3    # "baos":Ljava/io/ByteArrayOutputStream;
    .restart local v5    # "buffer":[B
    .restart local v11    # "len":I
    :cond_6
    const/16 v17, 0x0

    move/from16 v0, v17

    invoke-virtual {v3, v5, v0, v11}, Ljava/io/ByteArrayOutputStream;->write([BII)V
    :try_end_5
    .catch Ljava/io/IOException; {:try_start_5 .. :try_end_5} :catch_0
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_2
    .catch Ljava/lang/Error; {:try_start_5 .. :try_end_5} :catch_6
    .catchall {:try_start_5 .. :try_end_5} :catchall_0

    goto/16 :goto_2

    .line 167
    .end local v3    # "baos":Ljava/io/ByteArrayOutputStream;
    .end local v5    # "buffer":[B
    .end local v11    # "len":I
    :catch_2
    move-exception v9

    .line 168
    .local v9, "ex":Ljava/lang/Exception;
    :try_start_6
    const-string v17, "%s - Exception while sending request (%s)"

    const/16 v18, 0x2

    move/from16 v0, v18

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v18, v0

    const/16 v19, 0x0

    aput-object p2, v18, v19

    const/16 v19, 0x1

    invoke-virtual {v9}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v20

    aput-object v20, v18, v19

    invoke-static/range {v17 .. v18}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_0

    .line 169
    const/16 v17, 0x0

    .line 174
    if-eqz v6, :cond_7

    .line 175
    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->disconnect()V

    .line 178
    :cond_7
    if-eqz v10, :cond_1

    .line 180
    :try_start_7
    invoke-virtual {v10}, Ljava/io/InputStream;->close()V
    :try_end_7
    .catch Ljava/io/IOException; {:try_start_7 .. :try_end_7} :catch_3

    goto/16 :goto_3

    .line 181
    :catch_3
    move-exception v9

    .line 182
    .local v9, "ex":Ljava/io/IOException;
    const-string v18, "%s - Unable to close stream (%s)"

    const/16 v19, 0x2

    move/from16 v0, v19

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v19, v0

    const/16 v20, 0x0

    aput-object p2, v19, v20

    const/16 v20, 0x1

    invoke-virtual {v9}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v21

    aput-object v21, v19, v20

    invoke-static/range {v18 .. v19}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_3

    .line 181
    .end local v9    # "ex":Ljava/io/IOException;
    .restart local v3    # "baos":Ljava/io/ByteArrayOutputStream;
    .restart local v5    # "buffer":[B
    .restart local v11    # "len":I
    :catch_4
    move-exception v9

    .line 182
    .restart local v9    # "ex":Ljava/io/IOException;
    const-string v18, "%s - Unable to close stream (%s)"

    const/16 v19, 0x2

    move/from16 v0, v19

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v19, v0

    const/16 v20, 0x0

    aput-object p2, v19, v20

    const/16 v20, 0x1

    invoke-virtual {v9}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v21

    aput-object v21, v19, v20

    invoke-static/range {v18 .. v19}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_3

    .line 174
    .end local v3    # "baos":Ljava/io/ByteArrayOutputStream;
    .end local v5    # "buffer":[B
    .end local v9    # "ex":Ljava/io/IOException;
    .end local v11    # "len":I
    :cond_8
    if-eqz v6, :cond_9

    .line 175
    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->disconnect()V

    .line 178
    :cond_9
    if-eqz v10, :cond_a

    .line 180
    :try_start_8
    invoke-virtual {v10}, Ljava/io/InputStream;->close()V
    :try_end_8
    .catch Ljava/io/IOException; {:try_start_8 .. :try_end_8} :catch_5

    .line 187
    :cond_a
    :goto_5
    const/16 v17, 0x0

    goto/16 :goto_3

    .line 181
    :catch_5
    move-exception v9

    .line 182
    .restart local v9    # "ex":Ljava/io/IOException;
    const-string v17, "%s - Unable to close stream (%s)"

    const/16 v18, 0x2

    move/from16 v0, v18

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v18, v0

    const/16 v19, 0x0

    aput-object p2, v18, v19

    const/16 v19, 0x1

    invoke-virtual {v9}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v20

    aput-object v20, v18, v19

    invoke-static/range {v17 .. v18}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_5

    .line 170
    .end local v9    # "ex":Ljava/io/IOException;
    :catch_6
    move-exception v8

    .line 171
    .local v8, "er":Ljava/lang/Error;
    :try_start_9
    const-string v17, "%s - Unexpected error while sending request (%s)"

    const/16 v18, 0x2

    move/from16 v0, v18

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v18, v0

    const/16 v19, 0x0

    aput-object p2, v18, v19

    const/16 v19, 0x1

    invoke-virtual {v8}, Ljava/lang/Error;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v20

    aput-object v20, v18, v19

    invoke-static/range {v17 .. v18}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_9
    .catchall {:try_start_9 .. :try_end_9} :catchall_0

    .line 172
    const/16 v17, 0x0

    .line 174
    if-eqz v6, :cond_b

    .line 175
    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->disconnect()V

    .line 178
    :cond_b
    if-eqz v10, :cond_1

    .line 180
    :try_start_a
    invoke-virtual {v10}, Ljava/io/InputStream;->close()V
    :try_end_a
    .catch Ljava/io/IOException; {:try_start_a .. :try_end_a} :catch_7

    goto/16 :goto_3

    .line 181
    :catch_7
    move-exception v9

    .line 182
    .restart local v9    # "ex":Ljava/io/IOException;
    const-string v18, "%s - Unable to close stream (%s)"

    const/16 v19, 0x2

    move/from16 v0, v19

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v19, v0

    const/16 v20, 0x0

    aput-object p2, v19, v20

    const/16 v20, 0x1

    invoke-virtual {v9}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v21

    aput-object v21, v19, v20

    invoke-static/range {v18 .. v19}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_3

    .line 174
    .end local v8    # "er":Ljava/lang/Error;
    .end local v9    # "ex":Ljava/io/IOException;
    :catchall_0
    move-exception v17

    if-eqz v6, :cond_c

    .line 175
    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->disconnect()V

    .line 178
    :cond_c
    if-eqz v10, :cond_d

    .line 180
    :try_start_b
    invoke-virtual {v10}, Ljava/io/InputStream;->close()V
    :try_end_b
    .catch Ljava/io/IOException; {:try_start_b .. :try_end_b} :catch_8

    .line 183
    :cond_d
    :goto_6
    throw v17

    .line 181
    :catch_8
    move-exception v9

    .line 182
    .restart local v9    # "ex":Ljava/io/IOException;
    const-string v18, "%s - Unable to close stream (%s)"

    const/16 v19, 0x2

    move/from16 v0, v19

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v19, v0

    const/16 v20, 0x0

    aput-object p2, v19, v20

    const/16 v20, 0x1

    invoke-virtual {v9}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v21

    aput-object v21, v19, v20

    invoke-static/range {v18 .. v19}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_6

    .line 126
    nop

    :pswitch_data_0
    .packed-switch 0x12d
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method

.method protected static retrieveData(Ljava/lang/String;Ljava/util/Map;ILjava/lang/String;)[B
    .locals 3
    .param p0, "url"    # Ljava/lang/String;
    .param p2, "readTimeout"    # I
    .param p3, "source"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;I",
            "Ljava/lang/String;",
            ")[B"
        }
    .end annotation

    .prologue
    .local p1, "headers":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    const/4 v1, 0x0

    .line 54
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->isWearableApp()Z

    move-result v2

    if-eqz v2, :cond_0

    .line 55
    invoke-static {p0, p2}, Lcom/adobe/mobile/WearableFunctionBridge;->retrieveData(Ljava/lang/String;I)[B

    move-result-object v1

    .line 64
    :goto_0
    return-object v1

    .line 58
    :cond_0
    new-instance v0, Lcom/adobe/mobile/RequestHandler$1;

    invoke-direct {v0, p1}, Lcom/adobe/mobile/RequestHandler$1;-><init>(Ljava/util/Map;)V

    .line 64
    .local v0, "headersCallback":Ljava/util/concurrent/Callable;, "Ljava/util/concurrent/Callable<Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;>;"
    if-eqz p1, :cond_1

    .end local v0    # "headersCallback":Ljava/util/concurrent/Callable;, "Ljava/util/concurrent/Callable<Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;>;"
    :goto_1
    invoke-static {p0, p2, p3, v0, v1}, Lcom/adobe/mobile/RequestHandler;->retrieveData(Ljava/lang/String;ILjava/lang/String;Ljava/util/concurrent/Callable;Lcom/adobe/mobile/RequestHandler$HeaderCallback;)[B

    move-result-object v1

    goto :goto_0

    .restart local v0    # "headersCallback":Ljava/util/concurrent/Callable;, "Ljava/util/concurrent/Callable<Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;>;"
    :cond_1
    move-object v0, v1

    goto :goto_1
.end method

.method protected static retrieveNetworkObject(Ljava/lang/String;ILjava/lang/String;)Lcom/adobe/mobile/NetworkObject;
    .locals 6
    .param p0, "url"    # Ljava/lang/String;
    .param p1, "timeout"    # I
    .param p2, "source"    # Ljava/lang/String;

    .prologue
    const/4 v2, 0x0

    .line 400
    const-string v1, "GET"

    move-object v0, p0

    move v3, p1

    move-object v4, v2

    move-object v5, p2

    invoke-static/range {v0 .. v5}, Lcom/adobe/mobile/RequestHandler;->retrieveNetworkObject(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ILjava/lang/String;Ljava/lang/String;)Lcom/adobe/mobile/NetworkObject;

    move-result-object v0

    return-object v0
.end method

.method protected static retrieveNetworkObject(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ILjava/lang/String;Ljava/lang/String;)Lcom/adobe/mobile/NetworkObject;
    .locals 16
    .param p0, "url"    # Ljava/lang/String;
    .param p1, "requestType"    # Ljava/lang/String;
    .param p2, "postBodyString"    # Ljava/lang/String;
    .param p3, "timeout"    # I
    .param p4, "contentType"    # Ljava/lang/String;
    .param p5, "source"    # Ljava/lang/String;

    .prologue
    .line 405
    invoke-static/range {p0 .. p0}, Lcom/adobe/mobile/RequestHandler;->requestConnect(Ljava/lang/String;)Ljava/net/HttpURLConnection;

    move-result-object v1

    .line 406
    .local v1, "connection":Ljava/net/HttpURLConnection;
    if-nez v1, :cond_1

    .line 407
    const/4 v6, 0x0

    .line 480
    :cond_0
    :goto_0
    return-object v6

    .line 410
    :cond_1
    const/4 v4, 0x0

    .line 411
    .local v4, "inStream":Ljava/io/InputStream;
    new-instance v6, Lcom/adobe/mobile/NetworkObject;

    invoke-direct {v6}, Lcom/adobe/mobile/NetworkObject;-><init>()V

    .line 414
    .local v6, "networkObject":Lcom/adobe/mobile/NetworkObject;
    :try_start_0
    move-object/from16 v0, p1

    invoke-virtual {v1, v0}, Ljava/net/HttpURLConnection;->setRequestMethod(Ljava/lang/String;)V

    .line 415
    move/from16 v0, p3

    mul-int/lit16 v11, v0, 0x3e8

    invoke-virtual {v1, v11}, Ljava/net/HttpURLConnection;->setReadTimeout(I)V

    .line 416
    move/from16 v0, p3

    mul-int/lit16 v11, v0, 0x3e8

    invoke-virtual {v1, v11}, Ljava/net/HttpURLConnection;->setConnectTimeout(I)V

    .line 417
    if-eqz p4, :cond_2

    invoke-virtual/range {p4 .. p4}, Ljava/lang/String;->isEmpty()Z

    move-result v11

    if-nez v11, :cond_2

    .line 418
    const-string v11, "Content-Type"

    move-object/from16 v0, p4

    invoke-virtual {v1, v11, v0}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 420
    :cond_2
    const-string v11, "Accept-Encoding"

    const-string v12, "identity"

    invoke-virtual {v1, v11, v12}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 421
    const-string v11, "Accept-Language"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getDefaultAcceptLanguage()Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v1, v11, v12}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 422
    const-string v11, "User-Agent"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getDefaultUserAgent()Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v1, v11, v12}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 424
    const/4 v11, 0x1

    invoke-virtual {v1, v11}, Ljava/net/HttpURLConnection;->setDoOutput(Z)V

    .line 426
    if-eqz p2, :cond_3

    invoke-virtual/range {p2 .. p2}, Ljava/lang/String;->isEmpty()Z

    move-result v11

    if-nez v11, :cond_3

    .line 427
    const-string v11, "UTF-8"

    move-object/from16 v0, p2

    invoke-virtual {v0, v11}, Ljava/lang/String;->getBytes(Ljava/lang/String;)[B

    move-result-object v7

    .line 428
    .local v7, "outputBytes":[B
    array-length v11, v7

    invoke-virtual {v1, v11}, Ljava/net/HttpURLConnection;->setFixedLengthStreamingMode(I)V

    .line 431
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->getOutputStream()Ljava/io/OutputStream;

    move-result-object v2

    .line 432
    .local v2, "connectionOutputStream":Ljava/io/OutputStream;
    new-instance v8, Ljava/io/BufferedOutputStream;

    invoke-direct {v8, v2}, Ljava/io/BufferedOutputStream;-><init>(Ljava/io/OutputStream;)V

    .line 433
    .local v8, "postBodyStream":Ljava/io/OutputStream;
    invoke-virtual {v8, v7}, Ljava/io/OutputStream;->write([B)V

    .line 434
    invoke-virtual {v8}, Ljava/io/OutputStream;->close()V

    .line 438
    .end local v2    # "connectionOutputStream":Ljava/io/OutputStream;
    .end local v7    # "outputBytes":[B
    .end local v8    # "postBodyStream":Ljava/io/OutputStream;
    :cond_3
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result v11

    iput v11, v6, Lcom/adobe/mobile/NetworkObject;->responseCode:I

    .line 441
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object v4

    .line 443
    new-instance v9, Ljava/io/BufferedReader;

    new-instance v11, Ljava/io/InputStreamReader;

    const-string v12, "UTF-8"

    invoke-direct {v11, v4, v12}, Ljava/io/InputStreamReader;-><init>(Ljava/io/InputStream;Ljava/lang/String;)V

    invoke-direct {v9, v11}, Ljava/io/BufferedReader;-><init>(Ljava/io/Reader;)V

    .line 445
    .local v9, "reader":Ljava/io/BufferedReader;
    new-instance v10, Ljava/lang/StringBuilder;

    invoke-direct {v10}, Ljava/lang/StringBuilder;-><init>()V

    .line 447
    .local v10, "sb":Ljava/lang/StringBuilder;
    :goto_1
    invoke-virtual {v9}, Ljava/io/BufferedReader;->readLine()Ljava/lang/String;

    move-result-object v5

    .local v5, "line":Ljava/lang/String;
    if-eqz v5, :cond_4

    .line 448
    invoke-virtual {v10, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;
    :try_end_0
    .catch Ljava/net/ProtocolException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/lang/NullPointerException; {:try_start_0 .. :try_end_0} :catch_3
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_5
    .catch Ljava/lang/Error; {:try_start_0 .. :try_end_0} :catch_7
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_1

    .line 456
    .end local v5    # "line":Ljava/lang/String;
    .end local v9    # "reader":Ljava/io/BufferedReader;
    .end local v10    # "sb":Ljava/lang/StringBuilder;
    :catch_0
    move-exception v3

    .line 457
    .local v3, "ex":Ljava/net/ProtocolException;
    :try_start_1
    const-string v11, "%s - ProtocolException while trying to get content (%s)"

    const/4 v12, 0x2

    new-array v12, v12, [Ljava/lang/Object;

    const/4 v13, 0x0

    aput-object p5, v12, v13

    const/4 v13, 0x1

    invoke-virtual {v3}, Ljava/net/ProtocolException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v14

    aput-object v14, v12, v13

    invoke-static {v11, v12}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 469
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->disconnect()V

    .line 471
    if-eqz v4, :cond_0

    .line 473
    :try_start_2
    invoke-virtual {v4}, Ljava/io/InputStream;->close()V
    :try_end_2
    .catch Ljava/io/IOException; {:try_start_2 .. :try_end_2} :catch_1

    goto/16 :goto_0

    .line 474
    :catch_1
    move-exception v3

    .line 475
    .local v3, "ex":Ljava/io/IOException;
    const-string v11, "%s - Unable to close stream (%s)"

    const/4 v12, 0x2

    new-array v12, v12, [Ljava/lang/Object;

    const/4 v13, 0x0

    aput-object p5, v12, v13

    const/4 v13, 0x1

    invoke-virtual {v3}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v14

    aput-object v14, v12, v13

    invoke-static {v11, v12}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 451
    .end local v3    # "ex":Ljava/io/IOException;
    .restart local v5    # "line":Ljava/lang/String;
    .restart local v9    # "reader":Ljava/io/BufferedReader;
    .restart local v10    # "sb":Ljava/lang/StringBuilder;
    :cond_4
    :try_start_3
    invoke-virtual {v10}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v11

    iput-object v11, v6, Lcom/adobe/mobile/NetworkObject;->response:Ljava/lang/String;

    .line 454
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->getHeaderFields()Ljava/util/Map;

    move-result-object v11

    iput-object v11, v6, Lcom/adobe/mobile/NetworkObject;->responseHeaders:Ljava/util/Map;
    :try_end_3
    .catch Ljava/net/ProtocolException; {:try_start_3 .. :try_end_3} :catch_0
    .catch Ljava/lang/NullPointerException; {:try_start_3 .. :try_end_3} :catch_3
    .catch Ljava/io/IOException; {:try_start_3 .. :try_end_3} :catch_5
    .catch Ljava/lang/Error; {:try_start_3 .. :try_end_3} :catch_7
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    .line 469
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->disconnect()V

    .line 471
    if-eqz v4, :cond_0

    .line 473
    :try_start_4
    invoke-virtual {v4}, Ljava/io/InputStream;->close()V
    :try_end_4
    .catch Ljava/io/IOException; {:try_start_4 .. :try_end_4} :catch_2

    goto/16 :goto_0

    .line 474
    :catch_2
    move-exception v3

    .line 475
    .restart local v3    # "ex":Ljava/io/IOException;
    const-string v11, "%s - Unable to close stream (%s)"

    const/4 v12, 0x2

    new-array v12, v12, [Ljava/lang/Object;

    const/4 v13, 0x0

    aput-object p5, v12, v13

    const/4 v13, 0x1

    invoke-virtual {v3}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v14

    aput-object v14, v12, v13

    invoke-static {v11, v12}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 459
    .end local v3    # "ex":Ljava/io/IOException;
    .end local v5    # "line":Ljava/lang/String;
    .end local v9    # "reader":Ljava/io/BufferedReader;
    .end local v10    # "sb":Ljava/lang/StringBuilder;
    :catch_3
    move-exception v3

    .line 460
    .local v3, "ex":Ljava/lang/NullPointerException;
    :try_start_5
    const-string v11, "%s - NullPointerException while trying to get content (%s)"

    const/4 v12, 0x2

    new-array v12, v12, [Ljava/lang/Object;

    const/4 v13, 0x0

    aput-object p5, v12, v13

    const/4 v13, 0x1

    invoke-virtual {v3}, Ljava/lang/NullPointerException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v14

    aput-object v14, v12, v13

    invoke-static {v11, v12}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_0

    .line 469
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->disconnect()V

    .line 471
    if-eqz v4, :cond_0

    .line 473
    :try_start_6
    invoke-virtual {v4}, Ljava/io/InputStream;->close()V
    :try_end_6
    .catch Ljava/io/IOException; {:try_start_6 .. :try_end_6} :catch_4

    goto/16 :goto_0

    .line 474
    :catch_4
    move-exception v3

    .line 475
    .local v3, "ex":Ljava/io/IOException;
    const-string v11, "%s - Unable to close stream (%s)"

    const/4 v12, 0x2

    new-array v12, v12, [Ljava/lang/Object;

    const/4 v13, 0x0

    aput-object p5, v12, v13

    const/4 v13, 0x1

    invoke-virtual {v3}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v14

    aput-object v14, v12, v13

    invoke-static {v11, v12}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 462
    .end local v3    # "ex":Ljava/io/IOException;
    :catch_5
    move-exception v3

    .line 463
    .restart local v3    # "ex":Ljava/io/IOException;
    :try_start_7
    const-string v11, "%s - IOException while trying to get content (%s)"

    const/4 v12, 0x2

    new-array v12, v12, [Ljava/lang/Object;

    const/4 v13, 0x0

    aput-object p5, v12, v13

    const/4 v13, 0x1

    invoke-virtual {v3}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v14

    aput-object v14, v12, v13

    invoke-static {v11, v12}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_7
    .catchall {:try_start_7 .. :try_end_7} :catchall_0

    .line 469
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->disconnect()V

    .line 471
    if-eqz v4, :cond_0

    .line 473
    :try_start_8
    invoke-virtual {v4}, Ljava/io/InputStream;->close()V
    :try_end_8
    .catch Ljava/io/IOException; {:try_start_8 .. :try_end_8} :catch_6

    goto/16 :goto_0

    .line 474
    :catch_6
    move-exception v3

    .line 475
    const-string v11, "%s - Unable to close stream (%s)"

    const/4 v12, 0x2

    new-array v12, v12, [Ljava/lang/Object;

    const/4 v13, 0x0

    aput-object p5, v12, v13

    const/4 v13, 0x1

    invoke-virtual {v3}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v14

    aput-object v14, v12, v13

    invoke-static {v11, v12}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 465
    .end local v3    # "ex":Ljava/io/IOException;
    :catch_7
    move-exception v3

    .line 466
    .local v3, "ex":Ljava/lang/Error;
    :try_start_9
    const-string v11, "%s - Exception while trying to get content (%s)"

    const/4 v12, 0x2

    new-array v12, v12, [Ljava/lang/Object;

    const/4 v13, 0x0

    aput-object p5, v12, v13

    const/4 v13, 0x1

    invoke-virtual {v3}, Ljava/lang/Error;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v14

    aput-object v14, v12, v13

    invoke-static {v11, v12}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_9
    .catchall {:try_start_9 .. :try_end_9} :catchall_0

    .line 469
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->disconnect()V

    .line 471
    if-eqz v4, :cond_0

    .line 473
    :try_start_a
    invoke-virtual {v4}, Ljava/io/InputStream;->close()V
    :try_end_a
    .catch Ljava/io/IOException; {:try_start_a .. :try_end_a} :catch_8

    goto/16 :goto_0

    .line 474
    :catch_8
    move-exception v3

    .line 475
    .local v3, "ex":Ljava/io/IOException;
    const-string v11, "%s - Unable to close stream (%s)"

    const/4 v12, 0x2

    new-array v12, v12, [Ljava/lang/Object;

    const/4 v13, 0x0

    aput-object p5, v12, v13

    const/4 v13, 0x1

    invoke-virtual {v3}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v14

    aput-object v14, v12, v13

    invoke-static {v11, v12}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 469
    .end local v3    # "ex":Ljava/io/IOException;
    :catchall_0
    move-exception v11

    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->disconnect()V

    .line 471
    if-eqz v4, :cond_5

    .line 473
    :try_start_b
    invoke-virtual {v4}, Ljava/io/InputStream;->close()V
    :try_end_b
    .catch Ljava/io/IOException; {:try_start_b .. :try_end_b} :catch_9

    .line 476
    :cond_5
    :goto_2
    throw v11

    .line 474
    :catch_9
    move-exception v3

    .line 475
    .restart local v3    # "ex":Ljava/io/IOException;
    const-string v12, "%s - Unable to close stream (%s)"

    const/4 v13, 0x2

    new-array v13, v13, [Ljava/lang/Object;

    const/4 v14, 0x0

    aput-object p5, v13, v14

    const/4 v14, 0x1

    invoke-virtual {v3}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v15

    aput-object v15, v13, v14

    invoke-static {v12, v13}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_2
.end method

.method protected static sendGenericRequest(Ljava/lang/String;Ljava/util/Map;ILjava/lang/String;)V
    .locals 11
    .param p0, "url"    # Ljava/lang/String;
    .param p2, "timeout"    # I
    .param p3, "source"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;I",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation

    .prologue
    .local p1, "headers":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    const/4 v10, 0x2

    const/4 v9, 0x1

    const/4 v8, 0x0

    .line 192
    if-nez p0, :cond_1

    .line 235
    :cond_0
    :goto_0
    return-void

    .line 196
    :cond_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->isWearableApp()Z

    move-result v5

    if-eqz v5, :cond_2

    .line 197
    invoke-static {p0, p2, p3}, Lcom/adobe/mobile/WearableFunctionBridge;->sendGenericRequest(Ljava/lang/String;ILjava/lang/String;)V

    goto :goto_0

    .line 203
    :cond_2
    :try_start_0
    invoke-static {p0}, Lcom/adobe/mobile/RequestHandler;->requestConnect(Ljava/lang/String;)Ljava/net/HttpURLConnection;

    move-result-object v0

    .line 204
    .local v0, "connection":Ljava/net/HttpURLConnection;
    if-eqz v0, :cond_0

    .line 205
    invoke-virtual {v0, p2}, Ljava/net/HttpURLConnection;->setConnectTimeout(I)V

    .line 206
    invoke-virtual {v0, p2}, Ljava/net/HttpURLConnection;->setReadTimeout(I)V

    .line 208
    if-eqz p1, :cond_4

    .line 210
    invoke-interface {p1}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v5

    invoke-interface {v5}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v6

    :cond_3
    :goto_1
    invoke-interface {v6}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_4

    invoke-interface {v6}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/Map$Entry;

    .line 211
    .local v2, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/String;>;"
    invoke-interface {v2}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    .line 212
    .local v4, "value":Ljava/lang/String;
    invoke-virtual {v4}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/String;->length()I

    move-result v5

    if-lez v5, :cond_3

    .line 213
    invoke-interface {v2}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    invoke-virtual {v0, v5, v4}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/net/SocketTimeoutException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_2
    .catch Ljava/lang/Error; {:try_start_0 .. :try_end_0} :catch_3

    goto :goto_1

    .line 226
    .end local v0    # "connection":Ljava/net/HttpURLConnection;
    .end local v2    # "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/String;>;"
    .end local v4    # "value":Ljava/lang/String;
    :catch_0
    move-exception v1

    .line 227
    .local v1, "e":Ljava/net/SocketTimeoutException;
    const-string v5, "%s - Timed out sending request(%s)"

    new-array v6, v10, [Ljava/lang/Object;

    aput-object p3, v6, v8

    aput-object p0, v6, v9

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 218
    .end local v1    # "e":Ljava/net/SocketTimeoutException;
    .restart local v0    # "connection":Ljava/net/HttpURLConnection;
    :cond_4
    :try_start_1
    const-string v5, "%s - Request Sent(%s)"

    const/4 v6, 0x2

    new-array v6, v6, [Ljava/lang/Object;

    const/4 v7, 0x0

    aput-object p3, v6, v7

    const/4 v7, 0x1

    aput-object p0, v6, v7

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 220
    invoke-virtual {v0}, Ljava/net/HttpURLConnection;->getResponseCode()I

    .line 222
    invoke-virtual {v0}, Ljava/net/HttpURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object v3

    .line 223
    .local v3, "stream":Ljava/io/InputStream;
    invoke-virtual {v3}, Ljava/io/InputStream;->close()V

    .line 224
    invoke-virtual {v0}, Ljava/net/HttpURLConnection;->disconnect()V
    :try_end_1
    .catch Ljava/net/SocketTimeoutException; {:try_start_1 .. :try_end_1} :catch_0
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_2
    .catch Ljava/lang/Error; {:try_start_1 .. :try_end_1} :catch_3

    goto :goto_0

    .line 228
    .end local v0    # "connection":Ljava/net/HttpURLConnection;
    .end local v3    # "stream":Ljava/io/InputStream;
    :catch_1
    move-exception v1

    .line 229
    .local v1, "e":Ljava/io/IOException;
    const-string v5, "%s - IOException while sending request, may retry(%s)"

    new-array v6, v10, [Ljava/lang/Object;

    aput-object p3, v6, v8

    invoke-virtual {v1}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v6, v9

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 230
    .end local v1    # "e":Ljava/io/IOException;
    :catch_2
    move-exception v1

    .line 231
    .local v1, "e":Ljava/lang/Exception;
    const-string v5, "%s - Exception while attempting to send hit, will not retry(%s)"

    new-array v6, v10, [Ljava/lang/Object;

    aput-object p3, v6, v8

    invoke-virtual {v1}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v6, v9

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 232
    .end local v1    # "e":Ljava/lang/Exception;
    :catch_3
    move-exception v1

    .line 233
    .local v1, "e":Ljava/lang/Error;
    const-string v5, "%s - Exception while attempting to send hit, will not retry(%s)"

    new-array v6, v10, [Ljava/lang/Object;

    aput-object p3, v6, v8

    invoke-virtual {v1}, Ljava/lang/Error;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v6, v9

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0
.end method

.method protected static sendThirdPartyRequest(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;ILjava/lang/String;Ljava/lang/String;)Z
    .locals 18
    .param p0, "url"    # Ljava/lang/String;
    .param p1, "postBody"    # Ljava/lang/String;
    .param p3, "timeout"    # I
    .param p4, "postType"    # Ljava/lang/String;
    .param p5, "logPrefix"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;I",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ")Z"
        }
    .end annotation

    .prologue
    .line 321
    .local p2, "headers":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    if-nez p0, :cond_0

    .line 322
    const/4 v14, 0x0

    .line 387
    :goto_0
    return v14

    .line 325
    :cond_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->isWearableApp()Z

    move-result v14

    if-eqz v14, :cond_1

    .line 326
    move-object/from16 v0, p0

    move-object/from16 v1, p1

    move/from16 v2, p3

    move-object/from16 v3, p4

    move-object/from16 v4, p5

    invoke-static {v0, v1, v2, v3, v4}, Lcom/adobe/mobile/WearableFunctionBridge;->sendThirdPartyRequest(Ljava/lang/String;Ljava/lang/String;ILjava/lang/String;Ljava/lang/String;)Z

    move-result v14

    goto :goto_0

    .line 329
    :cond_1
    invoke-static/range {p0 .. p0}, Lcom/adobe/mobile/RequestHandler;->requestConnect(Ljava/lang/String;)Ljava/net/HttpURLConnection;

    move-result-object v6

    .line 331
    .local v6, "connection":Ljava/net/HttpURLConnection;
    if-nez v6, :cond_2

    .line 332
    const/4 v14, 0x0

    goto :goto_0

    .line 337
    :cond_2
    :try_start_0
    move/from16 v0, p3

    invoke-virtual {v6, v0}, Ljava/net/HttpURLConnection;->setConnectTimeout(I)V

    .line 338
    move/from16 v0, p3

    invoke-virtual {v6, v0}, Ljava/net/HttpURLConnection;->setReadTimeout(I)V

    .line 339
    const-string v14, "GET"

    invoke-virtual {v6, v14}, Ljava/net/HttpURLConnection;->setRequestMethod(Ljava/lang/String;)V

    .line 342
    if-eqz p2, :cond_3

    .line 343
    invoke-interface/range {p2 .. p2}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v14

    invoke-interface {v14}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v16

    :goto_1
    invoke-interface/range {v16 .. v16}, Ljava/util/Iterator;->hasNext()Z

    move-result v14

    if-eqz v14, :cond_3

    invoke-interface/range {v16 .. v16}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v10

    check-cast v10, Ljava/util/Map$Entry;

    .line 344
    .local v10, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/String;>;"
    invoke-interface {v10}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v14

    check-cast v14, Ljava/lang/String;

    invoke-interface {v10}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v15

    check-cast v15, Ljava/lang/String;

    invoke-virtual {v6, v14, v15}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/net/SocketTimeoutException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_2
    .catch Ljava/lang/Error; {:try_start_0 .. :try_end_0} :catch_3

    goto :goto_1

    .line 373
    .end local v10    # "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/String;>;"
    :catch_0
    move-exception v9

    .line 374
    .local v9, "e":Ljava/net/SocketTimeoutException;
    const-string v14, "%s - Timed out sending request (%s)"

    const/4 v15, 0x2

    new-array v15, v15, [Ljava/lang/Object;

    const/16 v16, 0x0

    aput-object p5, v15, v16

    const/16 v16, 0x1

    aput-object p1, v15, v16

    invoke-static {v14, v15}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 375
    const/4 v14, 0x0

    goto :goto_0

    .line 349
    .end local v9    # "e":Ljava/net/SocketTimeoutException;
    :cond_3
    if-eqz p1, :cond_4

    :try_start_1
    invoke-virtual/range {p1 .. p1}, Ljava/lang/String;->length()I

    move-result v14

    if-lez v14, :cond_4

    .line 350
    const-string v14, "POST"

    invoke-virtual {v6, v14}, Ljava/net/HttpURLConnection;->setRequestMethod(Ljava/lang/String;)V

    .line 352
    if-eqz p4, :cond_6

    invoke-virtual/range {p4 .. p4}, Ljava/lang/String;->length()I

    move-result v14

    if-lez v14, :cond_6

    move-object/from16 v8, p4

    .line 354
    .local v8, "contentType":Ljava/lang/String;
    :goto_2
    const-string v14, "UTF-8"

    move-object/from16 v0, p1

    invoke-virtual {v0, v14}, Ljava/lang/String;->getBytes(Ljava/lang/String;)[B

    move-result-object v12

    .line 355
    .local v12, "outputBytes":[B
    array-length v14, v12

    invoke-virtual {v6, v14}, Ljava/net/HttpURLConnection;->setFixedLengthStreamingMode(I)V

    .line 356
    const-string v14, "Content-Type"

    invoke-virtual {v6, v14, v8}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 359
    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->getOutputStream()Ljava/io/OutputStream;

    move-result-object v7

    .line 360
    .local v7, "connectionOutputStream":Ljava/io/OutputStream;
    new-instance v13, Ljava/io/BufferedOutputStream;

    invoke-direct {v13, v7}, Ljava/io/BufferedOutputStream;-><init>(Ljava/io/OutputStream;)V

    .line 361
    .local v13, "postBodyStream":Ljava/io/OutputStream;
    invoke-virtual {v13, v12}, Ljava/io/OutputStream;->write([B)V

    .line 362
    invoke-virtual {v13}, Ljava/io/OutputStream;->close()V

    .line 366
    .end local v7    # "connectionOutputStream":Ljava/io/OutputStream;
    .end local v8    # "contentType":Ljava/lang/String;
    .end local v12    # "outputBytes":[B
    .end local v13    # "postBodyStream":Ljava/io/OutputStream;
    :cond_4
    invoke-virtual {v6}, Ljava/net/HttpURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object v11

    .line 367
    .local v11, "inputStream":Ljava/io/InputStream;
    const/16 v14, 0xa

    new-array v5, v14, [B

    .line 368
    .local v5, "b":[B
    :cond_5
    invoke-virtual {v11, v5}, Ljava/io/InputStream;->read([B)I

    move-result v14

    if-gtz v14, :cond_5

    .line 369
    invoke-virtual {v11}, Ljava/io/InputStream;->close()V

    .line 371
    const-string v14, "%s - Successfully forwarded hit (%s body: %s type: %s)"

    const/4 v15, 0x4

    new-array v15, v15, [Ljava/lang/Object;

    const/16 v16, 0x0

    aput-object p5, v15, v16

    const/16 v16, 0x1

    aput-object p0, v15, v16

    const/16 v16, 0x2

    aput-object p1, v15, v16

    const/16 v16, 0x3

    aput-object p4, v15, v16

    invoke-static {v14, v15}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 387
    .end local v5    # "b":[B
    .end local v11    # "inputStream":Ljava/io/InputStream;
    :goto_3
    const/4 v14, 0x1

    goto/16 :goto_0

    .line 352
    :cond_6
    const-string v8, "application/x-www-form-urlencoded"
    :try_end_1
    .catch Ljava/net/SocketTimeoutException; {:try_start_1 .. :try_end_1} :catch_0
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_2
    .catch Ljava/lang/Error; {:try_start_1 .. :try_end_1} :catch_3

    goto :goto_2

    .line 377
    :catch_1
    move-exception v9

    .line 378
    .local v9, "e":Ljava/io/IOException;
    const-string v14, "%s - IOException while sending request, will not retry (%s)"

    const/4 v15, 0x2

    new-array v15, v15, [Ljava/lang/Object;

    const/16 v16, 0x0

    aput-object p5, v15, v16

    const/16 v16, 0x1

    invoke-virtual {v9}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v17

    aput-object v17, v15, v16

    invoke-static {v14, v15}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_3

    .line 380
    .end local v9    # "e":Ljava/io/IOException;
    :catch_2
    move-exception v9

    .line 381
    .local v9, "e":Ljava/lang/Exception;
    const-string v14, "%s - Exception while attempting to send hit, will not retry (%s)"

    const/4 v15, 0x2

    new-array v15, v15, [Ljava/lang/Object;

    const/16 v16, 0x0

    aput-object p5, v15, v16

    const/16 v16, 0x1

    invoke-virtual {v9}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v17

    aput-object v17, v15, v16

    invoke-static {v14, v15}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_3

    .line 383
    .end local v9    # "e":Ljava/lang/Exception;
    :catch_3
    move-exception v9

    .line 384
    .local v9, "e":Ljava/lang/Error;
    const-string v14, "%s - Exception while attempting to send hit, will not retry (%s)"

    const/4 v15, 0x2

    new-array v15, v15, [Ljava/lang/Object;

    const/16 v16, 0x0

    aput-object p5, v15, v16

    const/16 v16, 0x1

    invoke-virtual {v9}, Ljava/lang/Error;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v17

    aput-object v17, v15, v16

    invoke-static {v14, v15}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_3
.end method
