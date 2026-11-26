.class Lcom/adobe/mobile/VisitorIDService$2;
.super Ljava/lang/Object;
.source "VisitorIDService.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/adobe/mobile/VisitorIDService;->idSync(Ljava/util/Map;Ljava/util/Map;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;Z)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/adobe/mobile/VisitorIDService;

.field final synthetic val$authenticationState:Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;

.field final synthetic val$dpidsCopy:Ljava/util/HashMap;

.field final synthetic val$forceResync:Z

.field final synthetic val$identifiersCopy:Ljava/util/HashMap;


# direct methods
.method constructor <init>(Lcom/adobe/mobile/VisitorIDService;ZLjava/util/HashMap;Ljava/util/HashMap;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;)V
    .locals 0
    .param p1, "this$0"    # Lcom/adobe/mobile/VisitorIDService;

    .prologue
    .line 150
    iput-object p1, p0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    iput-boolean p2, p0, Lcom/adobe/mobile/VisitorIDService$2;->val$forceResync:Z

    iput-object p3, p0, Lcom/adobe/mobile/VisitorIDService$2;->val$identifiersCopy:Ljava/util/HashMap;

    iput-object p4, p0, Lcom/adobe/mobile/VisitorIDService$2;->val$dpidsCopy:Ljava/util/HashMap;

    iput-object p5, p0, Lcom/adobe/mobile/VisitorIDService$2;->val$authenticationState:Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 25

    .prologue
    .line 153
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v3

    invoke-virtual {v3}, Lcom/adobe/mobile/MobileConfig;->getVisitorIdServiceEnabled()Z

    move-result v3

    if-nez v3, :cond_1

    .line 287
    :cond_0
    :goto_0
    return-void

    .line 155
    :cond_1
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v3

    invoke-virtual {v3}, Lcom/adobe/mobile/MobileConfig;->getPrivacyStatus()Lcom/adobe/mobile/MobilePrivacyStatus;

    move-result-object v3

    sget-object v4, Lcom/adobe/mobile/MobilePrivacyStatus;->MOBILE_PRIVACY_STATUS_OPT_OUT:Lcom/adobe/mobile/MobilePrivacyStatus;

    if-ne v3, v4, :cond_2

    .line 156
    const-string v3, "ID Service - Ignoring ID Sync due to privacy status being opted out"

    const/4 v4, 0x0

    new-array v4, v4, [Ljava/lang/Object;

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 160
    :cond_2
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v3

    invoke-virtual {v3}, Lcom/adobe/mobile/MobileConfig;->getMarketingCloudOrganizationId()Ljava/lang/String;

    move-result-object v20

    .line 161
    .local v20, "orgId":Ljava/lang/String;
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getTimeSince1970()J

    move-result-wide v4

    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v3}, Lcom/adobe/mobile/VisitorIDService;->access$1000(Lcom/adobe/mobile/VisitorIDService;)J

    move-result-wide v6

    sub-long/2addr v4, v6

    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v3}, Lcom/adobe/mobile/VisitorIDService;->access$900(Lcom/adobe/mobile/VisitorIDService;)J

    move-result-wide v6

    cmp-long v3, v4, v6

    if-gtz v3, :cond_3

    move-object/from16 v0, p0

    iget-boolean v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->val$forceResync:Z

    if-eqz v3, :cond_f

    :cond_3
    const/16 v16, 0x1

    .line 162
    .local v16, "needResync":Z
    :goto_1
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->val$identifiersCopy:Ljava/util/HashMap;

    if-eqz v3, :cond_10

    const/4 v14, 0x1

    .line 163
    .local v14, "hasIdentifiers":Z
    :goto_2
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->val$dpidsCopy:Ljava/util/HashMap;

    if-eqz v3, :cond_11

    const/4 v13, 0x1

    .line 165
    .local v13, "hasDpids":Z
    :goto_3
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v3}, Lcom/adobe/mobile/VisitorIDService;->access$600(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_4

    if-nez v14, :cond_4

    if-nez v13, :cond_4

    if-eqz v16, :cond_0

    .line 169
    :cond_4
    new-instance v23, Ljava/lang/StringBuilder;

    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v3

    invoke-virtual {v3}, Lcom/adobe/mobile/MobileConfig;->getSSL()Z

    move-result v3

    if-eqz v3, :cond_12

    const-string v3, "https"

    :goto_4
    move-object/from16 v0, v23

    invoke-direct {v0, v3}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    .line 170
    .local v23, "url":Ljava/lang/StringBuilder;
    const-string v3, "://"

    move-object/from16 v0, v23

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 171
    sget-object v3, Lcom/adobe/mobile/VisitorIDService;->SERVER:Ljava/lang/String;

    move-object/from16 v0, v23

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 172
    const-string v3, "/id?d_ver=2&d_orgid="

    move-object/from16 v0, v23

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 173
    move-object/from16 v0, v23

    move-object/from16 v1, v20

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 175
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v3}, Lcom/adobe/mobile/VisitorIDService;->access$600(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_5

    .line 176
    const-string v3, "&"

    move-object/from16 v0, v23

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 177
    const-string v3, "d_mid"

    move-object/from16 v0, v23

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 178
    const-string v3, "="

    move-object/from16 v0, v23

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 179
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v3}, Lcom/adobe/mobile/VisitorIDService;->access$600(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v3

    move-object/from16 v0, v23

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 182
    :cond_5
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v3}, Lcom/adobe/mobile/VisitorIDService;->access$800(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_6

    .line 183
    const-string v3, "&"

    move-object/from16 v0, v23

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 184
    const-string v3, "d_blob"

    move-object/from16 v0, v23

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 185
    const-string v3, "="

    move-object/from16 v0, v23

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 186
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v3}, Lcom/adobe/mobile/VisitorIDService;->access$800(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v3

    move-object/from16 v0, v23

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 189
    :cond_6
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v3}, Lcom/adobe/mobile/VisitorIDService;->access$700(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_7

    .line 190
    const-string v3, "&"

    move-object/from16 v0, v23

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 191
    const-string v3, "dcs_region"

    move-object/from16 v0, v23

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 192
    const-string v3, "="

    move-object/from16 v0, v23

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 193
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v3}, Lcom/adobe/mobile/VisitorIDService;->access$700(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v3

    move-object/from16 v0, v23

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 196
    :cond_7
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    move-object/from16 v0, p0

    iget-object v4, v0, Lcom/adobe/mobile/VisitorIDService$2;->val$identifiersCopy:Ljava/util/HashMap;

    move-object/from16 v0, p0

    iget-object v5, v0, Lcom/adobe/mobile/VisitorIDService$2;->val$authenticationState:Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;

    invoke-static {v3, v4, v5}, Lcom/adobe/mobile/VisitorIDService;->access$1100(Lcom/adobe/mobile/VisitorIDService;Ljava/util/Map;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;)Ljava/util/List;

    move-result-object v17

    .line 197
    .local v17, "newCustomerIDs":Ljava/util/List;, "Ljava/util/List<Lcom/adobe/mobile/VisitorID;>;"
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    move-object/from16 v0, v17

    invoke-static {v3, v0}, Lcom/adobe/mobile/VisitorIDService;->access$500(Lcom/adobe/mobile/VisitorIDService;Ljava/util/List;)Ljava/lang/String;

    move-result-object v11

    .line 199
    .local v11, "encodedIdString":Ljava/lang/String;
    if-eqz v11, :cond_8

    .line 200
    move-object/from16 v0, v23

    invoke-virtual {v0, v11}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 203
    :cond_8
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    move-object/from16 v0, p0

    iget-object v4, v0, Lcom/adobe/mobile/VisitorIDService$2;->val$dpidsCopy:Ljava/util/HashMap;

    invoke-static {v3, v4}, Lcom/adobe/mobile/VisitorIDService;->access$1200(Lcom/adobe/mobile/VisitorIDService;Ljava/util/Map;)Ljava/lang/String;

    move-result-object v15

    .line 204
    .local v15, "internalIdString":Ljava/lang/String;
    if-eqz v15, :cond_9

    .line 205
    move-object/from16 v0, v23

    invoke-virtual {v0, v15}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 208
    :cond_9
    invoke-virtual/range {v23 .. v23}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v24

    .line 209
    .local v24, "urlString":Ljava/lang/String;
    const-string v3, "ID Service - Sending id sync call (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    aput-object v24, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 211
    const/4 v3, 0x0

    const/16 v4, 0x7d0

    const-string v5, "ID Service"

    move-object/from16 v0, v24

    invoke-static {v0, v3, v4, v5}, Lcom/adobe/mobile/RequestHandler;->retrieveData(Ljava/lang/String;Ljava/util/Map;ILjava/lang/String;)[B

    move-result-object v21

    .line 213
    .local v21, "response":[B
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    move-object/from16 v0, v21

    invoke-virtual {v3, v0}, Lcom/adobe/mobile/VisitorIDService;->parseResponse([B)Lorg/json/JSONObject;

    move-result-object v22

    .line 215
    .local v22, "responseObject":Lorg/json/JSONObject;
    if-eqz v22, :cond_13

    const-string v3, "d_mid"

    move-object/from16 v0, v22

    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_13

    const-string v3, "error_msg"

    move-object/from16 v0, v22

    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_13

    .line 217
    :try_start_0
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    const-string v4, "d_mid"

    move-object/from16 v0, v22

    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4}, Lcom/adobe/mobile/VisitorIDService;->access$602(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;

    .line 218
    const-string v3, "d_blob"

    move-object/from16 v0, v22

    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_a

    .line 219
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    const-string v4, "d_blob"

    move-object/from16 v0, v22

    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4}, Lcom/adobe/mobile/VisitorIDService;->access$802(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;

    .line 222
    :cond_a
    const-string v3, "dcs_region"

    move-object/from16 v0, v22

    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_b

    .line 223
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    const-string v4, "dcs_region"

    move-object/from16 v0, v22

    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4}, Lcom/adobe/mobile/VisitorIDService;->access$702(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;

    .line 226
    :cond_b
    const-string v3, "id_sync_ttl"

    move-object/from16 v0, v22

    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_c

    .line 227
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    const-string v4, "id_sync_ttl"

    move-object/from16 v0, v22

    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v4

    int-to-long v4, v4

    invoke-static {v3, v4, v5}, Lcom/adobe/mobile/VisitorIDService;->access$902(Lcom/adobe/mobile/VisitorIDService;J)J

    .line 230
    :cond_c
    const-string v19, ""

    .line 231
    .local v19, "optStatusDebug":Ljava/lang/String;
    const-string v3, "d_optout"

    move-object/from16 v0, v22

    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_d

    .line 232
    const-string v3, "d_optout"

    move-object/from16 v0, v22

    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v18

    .line 233
    .local v18, "optOuts":Lorg/json/JSONArray;
    invoke-virtual/range {v18 .. v18}, Lorg/json/JSONArray;->length()I

    move-result v3

    if-lez v3, :cond_d

    .line 234
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v3

    sget-object v4, Lcom/adobe/mobile/MobilePrivacyStatus;->MOBILE_PRIVACY_STATUS_OPT_OUT:Lcom/adobe/mobile/MobilePrivacyStatus;

    invoke-virtual {v3, v4}, Lcom/adobe/mobile/MobileConfig;->setPrivacyStatus(Lcom/adobe/mobile/MobilePrivacyStatus;)V

    .line 235
    const-string v19, ", global privacy status: opted out"

    .line 239
    .end local v18    # "optOuts":Lorg/json/JSONArray;
    :cond_d
    const-string v3, "ID Service - Got ID Response (mid: %s, blob: %s, hint: %s, ttl: %d%s)"

    const/4 v4, 0x5

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    move-object/from16 v0, p0

    iget-object v6, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v6}, Lcom/adobe/mobile/VisitorIDService;->access$600(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    const/4 v5, 0x1

    move-object/from16 v0, p0

    iget-object v6, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v6}, Lcom/adobe/mobile/VisitorIDService;->access$800(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    const/4 v5, 0x2

    move-object/from16 v0, p0

    iget-object v6, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v6}, Lcom/adobe/mobile/VisitorIDService;->access$700(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    const/4 v5, 0x3

    move-object/from16 v0, p0

    iget-object v6, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v6}, Lcom/adobe/mobile/VisitorIDService;->access$900(Lcom/adobe/mobile/VisitorIDService;)J

    move-result-wide v6

    invoke-static {v6, v7}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v6

    aput-object v6, v4, v5

    const/4 v5, 0x4

    aput-object v19, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1

    .line 263
    .end local v19    # "optStatusDebug":Ljava/lang/String;
    :cond_e
    :goto_5
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getTimeSince1970()J

    move-result-wide v4

    invoke-static {v3, v4, v5}, Lcom/adobe/mobile/VisitorIDService;->access$1002(Lcom/adobe/mobile/VisitorIDService;J)J

    .line 266
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    move-object/from16 v0, p0

    iget-object v4, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    move-object/from16 v0, v17

    invoke-static {v4, v0}, Lcom/adobe/mobile/VisitorIDService;->access$1400(Lcom/adobe/mobile/VisitorIDService;Ljava/util/List;)Ljava/util/List;

    move-result-object v4

    invoke-static {v3, v4}, Lcom/adobe/mobile/VisitorIDService;->access$002(Lcom/adobe/mobile/VisitorIDService;Ljava/util/List;)Ljava/util/List;

    .line 267
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    move-object/from16 v0, p0

    iget-object v4, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    move-object/from16 v0, p0

    iget-object v5, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v5}, Lcom/adobe/mobile/VisitorIDService;->access$000(Lcom/adobe/mobile/VisitorIDService;)Ljava/util/List;

    move-result-object v5

    invoke-static {v4, v5}, Lcom/adobe/mobile/VisitorIDService;->access$300(Lcom/adobe/mobile/VisitorIDService;Ljava/util/List;)Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4}, Lcom/adobe/mobile/VisitorIDService;->access$202(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;

    .line 268
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    move-object/from16 v0, p0

    iget-object v4, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    move-object/from16 v0, p0

    iget-object v5, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v5}, Lcom/adobe/mobile/VisitorIDService;->access$000(Lcom/adobe/mobile/VisitorIDService;)Ljava/util/List;

    move-result-object v5

    invoke-static {v4, v5}, Lcom/adobe/mobile/VisitorIDService;->access$500(Lcom/adobe/mobile/VisitorIDService;Ljava/util/List;)Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4}, Lcom/adobe/mobile/VisitorIDService;->access$402(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;

    .line 269
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    move-object/from16 v0, p0

    iget-object v4, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v4}, Lcom/adobe/mobile/VisitorIDService;->access$000(Lcom/adobe/mobile/VisitorIDService;)Ljava/util/List;

    move-result-object v4

    invoke-static {v3, v4}, Lcom/adobe/mobile/VisitorIDService;->access$1500(Lcom/adobe/mobile/VisitorIDService;Ljava/util/List;)Ljava/lang/String;

    move-result-object v10

    .line 271
    .local v10, "idStringToStore":Ljava/lang/String;
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v3}, Lcom/adobe/mobile/VisitorIDService;->access$600(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v3

    move-object/from16 v0, p0

    iget-object v4, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v4}, Lcom/adobe/mobile/VisitorIDService;->access$700(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v4

    move-object/from16 v0, p0

    iget-object v5, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v5}, Lcom/adobe/mobile/VisitorIDService;->access$800(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v5

    move-object/from16 v0, p0

    iget-object v6, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v6}, Lcom/adobe/mobile/VisitorIDService;->access$900(Lcom/adobe/mobile/VisitorIDService;)J

    move-result-wide v6

    move-object/from16 v0, p0

    iget-object v8, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v8}, Lcom/adobe/mobile/VisitorIDService;->access$1000(Lcom/adobe/mobile/VisitorIDService;)J

    move-result-wide v8

    invoke-static/range {v3 .. v10}, Lcom/adobe/mobile/WearableFunctionBridge;->syncVidServiceToWearable(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JJLjava/lang/String;)V

    .line 274
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v2

    .line 275
    .local v2, "e":Landroid/content/SharedPreferences$Editor;
    const-string v3, "ADBMOBILE_VISITORID_IDS"

    invoke-interface {v2, v3, v10}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 276
    const-string v3, "ADBMOBILE_PERSISTED_MID"

    move-object/from16 v0, p0

    iget-object v4, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v4}, Lcom/adobe/mobile/VisitorIDService;->access$600(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v4

    invoke-interface {v2, v3, v4}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 277
    const-string v3, "ADBMOBILE_PERSISTED_MID_HINT"

    move-object/from16 v0, p0

    iget-object v4, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v4}, Lcom/adobe/mobile/VisitorIDService;->access$700(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v4

    invoke-interface {v2, v3, v4}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 278
    const-string v3, "ADBMOBILE_PERSISTED_MID_BLOB"

    move-object/from16 v0, p0

    iget-object v4, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v4}, Lcom/adobe/mobile/VisitorIDService;->access$800(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v4

    invoke-interface {v2, v3, v4}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 279
    const-string v3, "ADBMOBILE_VISITORID_TTL"

    move-object/from16 v0, p0

    iget-object v4, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v4}, Lcom/adobe/mobile/VisitorIDService;->access$900(Lcom/adobe/mobile/VisitorIDService;)J

    move-result-wide v4

    invoke-interface {v2, v3, v4, v5}, Landroid/content/SharedPreferences$Editor;->putLong(Ljava/lang/String;J)Landroid/content/SharedPreferences$Editor;

    .line 280
    const-string v3, "ADBMOBILE_VISITORID_SYNC"

    move-object/from16 v0, p0

    iget-object v4, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v4}, Lcom/adobe/mobile/VisitorIDService;->access$1000(Lcom/adobe/mobile/VisitorIDService;)J

    move-result-wide v4

    invoke-interface {v2, v3, v4, v5}, Landroid/content/SharedPreferences$Editor;->putLong(Ljava/lang/String;J)Landroid/content/SharedPreferences$Editor;

    .line 281
    invoke-interface {v2}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_0

    goto/16 :goto_0

    .line 284
    .end local v2    # "e":Landroid/content/SharedPreferences$Editor;
    :catch_0
    move-exception v12

    .line 285
    .local v12, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v3, "ID Service - Unable to persist identifiers to shared preferences(%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v12}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 161
    .end local v10    # "idStringToStore":Ljava/lang/String;
    .end local v11    # "encodedIdString":Ljava/lang/String;
    .end local v12    # "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    .end local v13    # "hasDpids":Z
    .end local v14    # "hasIdentifiers":Z
    .end local v15    # "internalIdString":Ljava/lang/String;
    .end local v16    # "needResync":Z
    .end local v17    # "newCustomerIDs":Ljava/util/List;, "Ljava/util/List<Lcom/adobe/mobile/VisitorID;>;"
    .end local v21    # "response":[B
    .end local v22    # "responseObject":Lorg/json/JSONObject;
    .end local v23    # "url":Ljava/lang/StringBuilder;
    .end local v24    # "urlString":Ljava/lang/String;
    :cond_f
    const/16 v16, 0x0

    goto/16 :goto_1

    .line 162
    .restart local v16    # "needResync":Z
    :cond_10
    const/4 v14, 0x0

    goto/16 :goto_2

    .line 163
    .restart local v14    # "hasIdentifiers":Z
    :cond_11
    const/4 v13, 0x0

    goto/16 :goto_3

    .line 169
    .restart local v13    # "hasDpids":Z
    :cond_12
    const-string v3, "http"

    goto/16 :goto_4

    .line 240
    .restart local v11    # "encodedIdString":Ljava/lang/String;
    .restart local v15    # "internalIdString":Ljava/lang/String;
    .restart local v17    # "newCustomerIDs":Ljava/util/List;, "Ljava/util/List<Lcom/adobe/mobile/VisitorID;>;"
    .restart local v21    # "response":[B
    .restart local v22    # "responseObject":Lorg/json/JSONObject;
    .restart local v23    # "url":Ljava/lang/StringBuilder;
    .restart local v24    # "urlString":Ljava/lang/String;
    :catch_1
    move-exception v12

    .line 241
    .local v12, "ex":Lorg/json/JSONException;
    const-string v3, "ID Service - Error parsing response (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v12}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_5

    .line 245
    .end local v12    # "ex":Lorg/json/JSONException;
    :cond_13
    if-eqz v22, :cond_14

    const-string v3, "error_msg"

    move-object/from16 v0, v22

    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_14

    .line 247
    :try_start_2
    const-string v3, "ID Service - Service returned error (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    const-string v6, "error_msg"

    move-object/from16 v0, v22

    invoke-virtual {v0, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_2
    .catch Lorg/json/JSONException; {:try_start_2 .. :try_end_2} :catch_2

    .line 252
    :cond_14
    :goto_6
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v3}, Lcom/adobe/mobile/VisitorIDService;->access$600(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v3

    if-nez v3, :cond_e

    .line 254
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    move-object/from16 v0, p0

    iget-object v4, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v4}, Lcom/adobe/mobile/VisitorIDService;->access$1300(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4}, Lcom/adobe/mobile/VisitorIDService;->access$602(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;

    .line 255
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    const/4 v4, 0x0

    invoke-static {v3, v4}, Lcom/adobe/mobile/VisitorIDService;->access$802(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;

    .line 256
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    const/4 v4, 0x0

    invoke-static {v3, v4}, Lcom/adobe/mobile/VisitorIDService;->access$702(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;

    .line 257
    move-object/from16 v0, p0

    iget-object v3, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    const-wide/16 v4, 0x258

    invoke-static {v3, v4, v5}, Lcom/adobe/mobile/VisitorIDService;->access$902(Lcom/adobe/mobile/VisitorIDService;J)J

    .line 259
    const-string v3, "ID Service - Did not return an ID, generating one locally (mid: %s, ttl: %d)"

    const/4 v4, 0x2

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    move-object/from16 v0, p0

    iget-object v6, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v6}, Lcom/adobe/mobile/VisitorIDService;->access$600(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    const/4 v5, 0x1

    move-object/from16 v0, p0

    iget-object v6, v0, Lcom/adobe/mobile/VisitorIDService$2;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v6}, Lcom/adobe/mobile/VisitorIDService;->access$900(Lcom/adobe/mobile/VisitorIDService;)J

    move-result-wide v6

    invoke-static {v6, v7}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_5

    .line 248
    :catch_2
    move-exception v12

    .line 249
    .restart local v12    # "ex":Lorg/json/JSONException;
    const-string v3, "ID Service - Unable to read error condition(%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v12}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_6
.end method
