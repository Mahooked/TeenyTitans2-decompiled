.class Lcom/adobe/mobile/MessageTemplateCallback;
.super Lcom/adobe/mobile/Message;
.source "MessageTemplateCallback.java"


# static fields
.field private static final ADB_TEMPLATE_CALLBACK_BODY:Ljava/lang/String; = "templatebody"

.field private static final ADB_TEMPLATE_CALLBACK_CONTENT_TYPE:Ljava/lang/String; = "contenttype"

.field private static final ADB_TEMPLATE_CALLBACK_LOG_PREFIX:Ljava/lang/String; = "Postbacks"

.field private static final ADB_TEMPLATE_CALLBACK_TIMEOUT:Ljava/lang/String; = "timeout"

.field private static final ADB_TEMPLATE_CALLBACK_TOKEN_ADVERTISING_IDENTIFIER:Ljava/lang/String; = "%adid%"

.field private static final ADB_TEMPLATE_CALLBACK_TOKEN_ALL_JSON_ENCODED:Ljava/lang/String; = "%all_json%"

.field private static final ADB_TEMPLATE_CALLBACK_TOKEN_ALL_URL_ENCODED:Ljava/lang/String; = "%all_url%"

.field private static final ADB_TEMPLATE_CALLBACK_TOKEN_ISO8601_TIMESTAMP:Ljava/lang/String; = "%timestampz%"

.field private static final ADB_TEMPLATE_CALLBACK_TOKEN_MARKETING_CLOUD_ID:Ljava/lang/String; = "%mcid%"

.field private static final ADB_TEMPLATE_CALLBACK_TOKEN_PUSH_IDENTIFIER:Ljava/lang/String; = "%pushid%"

.field private static final ADB_TEMPLATE_CALLBACK_TOKEN_SDK_CACHEBUST:Ljava/lang/String; = "%cachebust%"

.field private static final ADB_TEMPLATE_CALLBACK_TOKEN_SDK_VERSION:Ljava/lang/String; = "%sdkver%"

.field private static final ADB_TEMPLATE_CALLBACK_TOKEN_UNIX_TIMESTAMP:Ljava/lang/String; = "%timestampu%"

.field private static final ADB_TEMPLATE_CALLBACK_URL:Ljava/lang/String; = "templateurl"

.field private static final ADB_TEMPLATE_TIMEOUT_DEFAULT:I = 0x2

.field private static final ADB_TEMPLATE_TOKEN_END:C = '}'

.field private static final ADB_TEMPLATE_TOKEN_START:C = '{'

.field private static final tokenDataMask:[Z


# instance fields
.field private _combinedVariablesCopy:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field

.field protected contentType:Ljava/lang/String;

.field private final randomGen:Ljava/security/SecureRandom;

.field protected templateBody:Ljava/lang/String;

.field protected templateUrl:Ljava/lang/String;

.field protected timeout:I


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .prologue
    .line 334
    const/16 v0, 0x100

    new-array v0, v0, [Z

    fill-array-data v0, :array_0

    sput-object v0, Lcom/adobe/mobile/MessageTemplateCallback;->tokenDataMask:[Z

    return-void

    :array_0
    .array-data 1
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x1t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x1t
        0x0t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x0t
        0x0t
        0x0t
        0x0t
        0x1t
        0x0t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
    .end array-data
.end method

.method constructor <init>()V
    .locals 1

    .prologue
    .line 29
    invoke-direct {p0}, Lcom/adobe/mobile/Message;-><init>()V

    .line 38
    new-instance v0, Ljava/security/SecureRandom;

    invoke-direct {v0}, Ljava/security/SecureRandom;-><init>()V

    iput-object v0, p0, Lcom/adobe/mobile/MessageTemplateCallback;->randomGen:Ljava/security/SecureRandom;

    return-void
.end method

.method private buildExpansionsForTokens(Ljava/util/ArrayList;Z)Ljava/util/HashMap;
    .locals 9
    .param p2, "urlEncodeExpansions"    # Z
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/ArrayList",
            "<",
            "Ljava/lang/String;",
            ">;Z)",
            "Ljava/util/HashMap",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .prologue
    .line 271
    .local p1, "tokens":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/String;>;"
    new-instance v1, Ljava/util/HashMap;

    invoke-virtual {p1}, Ljava/util/ArrayList;->size()I

    move-result v6

    invoke-direct {v1, v6}, Ljava/util/HashMap;-><init>(I)V

    .line 273
    .local v1, "expansions":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/String;>;"
    invoke-virtual {p1}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v6

    :goto_0
    invoke-interface {v6}, Ljava/util/Iterator;->hasNext()Z

    move-result v7

    if-eqz v7, :cond_2

    invoke-interface {v6}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 274
    .local v2, "token":Ljava/lang/String;
    const/4 v7, 0x1

    invoke-virtual {v2}, Ljava/lang/String;->length()I

    move-result v8

    add-int/lit8 v8, v8, -0x1

    invoke-virtual {v2, v7, v8}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v7}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v0

    .line 275
    .local v0, "cleanToken":Ljava/lang/String;
    iget-object v7, p0, Lcom/adobe/mobile/MessageTemplateCallback;->_combinedVariablesCopy:Ljava/util/HashMap;

    invoke-virtual {v7, v0}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    .line 276
    .local v3, "tokenObject":Ljava/lang/Object;
    if-nez v3, :cond_0

    const-string v4, ""

    .line 278
    .local v4, "tokenString":Ljava/lang/String;
    :goto_1
    if-eqz p2, :cond_1

    invoke-static {v4}, Lcom/adobe/mobile/StaticMethods;->URLEncode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    .line 279
    .local v5, "tokenValue":Ljava/lang/String;
    :goto_2
    invoke-virtual {v1, v2, v5}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 276
    .end local v4    # "tokenString":Ljava/lang/String;
    .end local v5    # "tokenValue":Ljava/lang/String;
    :cond_0
    invoke-virtual {v3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v4

    goto :goto_1

    .restart local v4    # "tokenString":Ljava/lang/String;
    :cond_1
    move-object v5, v4

    .line 278
    goto :goto_2

    .line 282
    .end local v0    # "cleanToken":Ljava/lang/String;
    .end local v2    # "token":Ljava/lang/String;
    .end local v3    # "tokenObject":Ljava/lang/Object;
    .end local v4    # "tokenString":Ljava/lang/String;
    :cond_2
    return-object v1
.end method

.method private findTokensForExpansion(Ljava/lang/String;)Ljava/util/ArrayList;
    .locals 7
    .param p1, "input"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/ArrayList",
            "<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .prologue
    .line 286
    if-eqz p1, :cond_0

    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v5

    if-gtz v5, :cond_2

    .line 287
    :cond_0
    const/4 v0, 0x0

    .line 328
    :cond_1
    return-object v0

    .line 291
    :cond_2
    new-instance v0, Ljava/util/ArrayList;

    const/16 v5, 0x20

    invoke-direct {v0, v5}, Ljava/util/ArrayList;-><init>(I)V

    .line 293
    .local v0, "foundList":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/String;>;"
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v2

    .line 296
    .local v2, "inputLength":I
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    if-ge v1, v2, :cond_1

    .line 298
    invoke-virtual {p1, v1}, Ljava/lang/String;->charAt(I)C

    move-result v5

    const/16 v6, 0x7b

    if-ne v5, v6, :cond_4

    .line 301
    add-int/lit8 v3, v1, 0x1

    .local v3, "j":I
    :goto_1
    if-ge v3, v2, :cond_3

    .line 302
    invoke-virtual {p1, v3}, Ljava/lang/String;->charAt(I)C

    move-result v5

    const/16 v6, 0x7d

    if-ne v5, v6, :cond_5

    .line 308
    :cond_3
    if-eq v3, v2, :cond_1

    .line 313
    add-int/lit8 v5, v3, 0x1

    invoke-virtual {p1, v1, v5}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v4

    .line 316
    .local v4, "token":Ljava/lang/String;
    const/4 v5, 0x1

    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v6

    add-int/lit8 v6, v6, -0x1

    invoke-virtual {v4, v5, v6}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v5

    invoke-direct {p0, v5}, Lcom/adobe/mobile/MessageTemplateCallback;->tokenIsValid(Ljava/lang/String;)Z

    move-result v5

    if-nez v5, :cond_6

    .line 296
    .end local v3    # "j":I
    .end local v4    # "token":Ljava/lang/String;
    :cond_4
    :goto_2
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 301
    .restart local v3    # "j":I
    :cond_5
    add-int/lit8 v3, v3, 0x1

    goto :goto_1

    .line 321
    .restart local v4    # "token":Ljava/lang/String;
    :cond_6
    invoke-virtual {v0, v4}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 324
    move v1, v3

    goto :goto_2
.end method

.method private getExpandedBody()Ljava/lang/String;
    .locals 6

    .prologue
    .line 204
    const/4 v1, 0x0

    .line 205
    .local v1, "expandedBody":Ljava/lang/String;
    iget-object v4, p0, Lcom/adobe/mobile/MessageTemplateCallback;->templateBody:Ljava/lang/String;

    if-eqz v4, :cond_0

    iget-object v4, p0, Lcom/adobe/mobile/MessageTemplateCallback;->templateBody:Ljava/lang/String;

    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-lez v4, :cond_0

    .line 206
    iget-object v4, p0, Lcom/adobe/mobile/MessageTemplateCallback;->templateBody:Ljava/lang/String;

    invoke-direct {p0, v4}, Lcom/adobe/mobile/MessageTemplateCallback;->findTokensForExpansion(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v4

    const/4 v5, 0x1

    invoke-direct {p0, v4, v5}, Lcom/adobe/mobile/MessageTemplateCallback;->buildExpansionsForTokens(Ljava/util/ArrayList;Z)Ljava/util/HashMap;

    move-result-object v0

    .line 208
    .local v0, "bodyExpansions":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/String;>;"
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    .line 209
    .local v2, "nonEncodedParamsList":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/String;>;"
    const-string v4, "{%all_url%}"

    invoke-virtual {v2, v4}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 210
    const-string v4, "{%all_json%}"

    invoke-virtual {v2, v4}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 212
    const/4 v4, 0x0

    invoke-direct {p0, v2, v4}, Lcom/adobe/mobile/MessageTemplateCallback;->buildExpansionsForTokens(Ljava/util/ArrayList;Z)Ljava/util/HashMap;

    move-result-object v3

    .line 213
    .local v3, "nonEncodedURLExpansions":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/String;>;"
    invoke-virtual {v0, v3}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 215
    iget-object v4, p0, Lcom/adobe/mobile/MessageTemplateCallback;->templateBody:Ljava/lang/String;

    invoke-static {v4, v0}, Lcom/adobe/mobile/StaticMethods;->expandTokens(Ljava/lang/String;Ljava/util/Map;)Ljava/lang/String;

    move-result-object v1

    .line 218
    .end local v0    # "bodyExpansions":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/String;>;"
    .end local v2    # "nonEncodedParamsList":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/String;>;"
    .end local v3    # "nonEncodedURLExpansions":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/String;>;"
    :cond_0
    return-object v1
.end method

.method private getExpandedUrl()Ljava/lang/String;
    .locals 6

    .prologue
    .line 189
    const/4 v0, 0x0

    .line 191
    .local v0, "expandedUrl":Ljava/lang/String;
    iget-object v4, p0, Lcom/adobe/mobile/MessageTemplateCallback;->templateUrl:Ljava/lang/String;

    if-eqz v4, :cond_0

    iget-object v4, p0, Lcom/adobe/mobile/MessageTemplateCallback;->templateUrl:Ljava/lang/String;

    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-lez v4, :cond_0

    .line 192
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 193
    .local v1, "nonEncodedParamsList":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/String;>;"
    const-string v4, "{%all_url%}"

    invoke-virtual {v1, v4}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 194
    iget-object v4, p0, Lcom/adobe/mobile/MessageTemplateCallback;->templateUrl:Ljava/lang/String;

    invoke-direct {p0, v4}, Lcom/adobe/mobile/MessageTemplateCallback;->findTokensForExpansion(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v4

    const/4 v5, 0x1

    invoke-direct {p0, v4, v5}, Lcom/adobe/mobile/MessageTemplateCallback;->buildExpansionsForTokens(Ljava/util/ArrayList;Z)Ljava/util/HashMap;

    move-result-object v3

    .line 195
    .local v3, "urlExpansions":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/String;>;"
    const/4 v4, 0x0

    invoke-direct {p0, v1, v4}, Lcom/adobe/mobile/MessageTemplateCallback;->buildExpansionsForTokens(Ljava/util/ArrayList;Z)Ljava/util/HashMap;

    move-result-object v2

    .line 196
    .local v2, "nonEncodedURLExpansions":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/String;>;"
    invoke-virtual {v3, v2}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 197
    iget-object v4, p0, Lcom/adobe/mobile/MessageTemplateCallback;->templateUrl:Ljava/lang/String;

    invoke-static {v4, v3}, Lcom/adobe/mobile/StaticMethods;->expandTokens(Ljava/lang/String;Ljava/util/Map;)Ljava/lang/String;

    move-result-object v0

    .line 199
    .end local v1    # "nonEncodedParamsList":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/String;>;"
    .end local v2    # "nonEncodedURLExpansions":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/String;>;"
    .end local v3    # "urlExpansions":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/String;>;"
    :cond_0
    return-object v0
.end method

.method private getExpansionTokensForVariables(Ljava/util/HashMap;)Ljava/util/HashMap;
    .locals 11
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/HashMap",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Ljava/util/HashMap",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .prologue
    .line 233
    .local p1, "vars":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    new-instance v1, Ljava/util/HashMap;

    const/4 v7, 0x5

    invoke-direct {v1, v7}, Ljava/util/HashMap;-><init>(I)V

    .line 235
    .local v1, "expansionTokens":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    const-string v7, "%sdkver%"

    const-string v8, "4.10.0-AN"

    invoke-virtual {v1, v7, v8}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 236
    const-string v7, "%cachebust%"

    iget-object v8, p0, Lcom/adobe/mobile/MessageTemplateCallback;->randomGen:Ljava/security/SecureRandom;

    const v9, 0x5f5e100

    invoke-virtual {v8, v9}, Ljava/security/SecureRandom;->nextInt(I)I

    move-result v8

    invoke-static {v8}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v1, v7, v8}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 237
    const-string v7, "%adid%"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAdvertisingIdentifier()Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v1, v7, v8}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 238
    const-string v7, "%timestampu%"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getTimeSince1970()J

    move-result-wide v8

    invoke-static {v8, v9}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v1, v7, v8}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 239
    const-string v7, "%timestampz%"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getIso8601Date()Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v1, v7, v8}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 240
    const-string v7, "%pushid%"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getPushIdentifier()Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v1, v7, v8}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 241
    const-string v8, "%mcid%"

    invoke-static {}, Lcom/adobe/mobile/VisitorIDService;->sharedInstance()Lcom/adobe/mobile/VisitorIDService;

    move-result-object v7

    invoke-virtual {v7}, Lcom/adobe/mobile/VisitorIDService;->getMarketingCloudID()Ljava/lang/String;

    move-result-object v7

    if-eqz v7, :cond_0

    invoke-static {}, Lcom/adobe/mobile/VisitorIDService;->sharedInstance()Lcom/adobe/mobile/VisitorIDService;

    move-result-object v7

    invoke-virtual {v7}, Lcom/adobe/mobile/VisitorIDService;->getMarketingCloudID()Ljava/lang/String;

    move-result-object v7

    :goto_0
    invoke-virtual {v1, v8, v7}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 243
    new-instance v6, Ljava/util/ArrayList;

    invoke-direct {v6}, Ljava/util/ArrayList;-><init>()V

    .line 244
    .local v6, "queryParameters":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/String;>;"
    invoke-virtual {p1}, Ljava/util/HashMap;->keySet()Ljava/util/Set;

    move-result-object v7

    invoke-interface {v7}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v7

    :goto_1
    invoke-interface {v7}, Ljava/util/Iterator;->hasNext()Z

    move-result v8

    if-eqz v8, :cond_1

    invoke-interface {v7}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    .line 246
    .local v4, "key":Ljava/lang/Object;
    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    .line 247
    .local v5, "queryParameterString":Ljava/lang/StringBuilder;
    invoke-virtual {v4}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v8

    invoke-static {v8}, Lcom/adobe/mobile/StaticMethods;->URLEncode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v5, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 248
    const-string v8, "="

    invoke-virtual {v5, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 249
    invoke-virtual {p1, v4}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v8

    invoke-virtual {v8}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v8

    invoke-static {v8}, Lcom/adobe/mobile/StaticMethods;->URLEncode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v5, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 251
    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v6, v8}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 241
    .end local v4    # "key":Ljava/lang/Object;
    .end local v5    # "queryParameterString":Ljava/lang/StringBuilder;
    .end local v6    # "queryParameters":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/String;>;"
    :cond_0
    const-string v7, ""

    goto :goto_0

    .line 254
    .restart local v6    # "queryParameters":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/String;>;"
    :cond_1
    const-string v7, "%all_url%"

    const-string v8, "&"

    invoke-static {v8, v6}, Landroid/text/TextUtils;->join(Ljava/lang/CharSequence;Ljava/lang/Iterable;)Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v1, v7, v8}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 257
    :try_start_0
    new-instance v2, Lorg/json/JSONObject;

    invoke-direct {v2, p1}, Lorg/json/JSONObject;-><init>(Ljava/util/Map;)V

    .line 258
    .local v2, "jsonObject":Lorg/json/JSONObject;
    invoke-virtual {v2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v3

    .line 260
    .local v3, "jsonString":Ljava/lang/String;
    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v7

    if-lez v7, :cond_2

    .line 261
    const-string v7, "%all_json%"

    invoke-virtual {v1, v7, v3}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catch Ljava/lang/NullPointerException; {:try_start_0 .. :try_end_0} :catch_0

    .line 267
    .end local v2    # "jsonObject":Lorg/json/JSONObject;
    .end local v3    # "jsonString":Ljava/lang/String;
    :cond_2
    :goto_2
    return-object v1

    .line 263
    :catch_0
    move-exception v0

    .line 264
    .local v0, "ex":Ljava/lang/NullPointerException;
    const-string v7, "Data Callback - unable to create json string for vars:  (%s)"

    const/4 v8, 0x1

    new-array v8, v8, [Ljava/lang/Object;

    const/4 v9, 0x0

    invoke-virtual {v0}, Ljava/lang/NullPointerException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v10

    aput-object v10, v8, v9

    invoke-static {v7, v8}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_2
.end method

.method private tokenIsValid(Ljava/lang/String;)Z
    .locals 9
    .param p1, "token"    # Ljava/lang/String;

    .prologue
    const/4 v4, 0x1

    const/4 v3, 0x0

    .line 355
    :try_start_0
    const-string v5, "UTF-8"

    invoke-virtual {p1, v5}, Ljava/lang/String;->getBytes(Ljava/lang/String;)[B

    move-result-object v2

    .line 358
    .local v2, "utf8Token":[B
    array-length v6, v2

    move v5, v3

    :goto_0
    if-ge v5, v6, :cond_1

    aget-byte v0, v2, v5

    .line 360
    .local v0, "currentByte":B
    sget-object v7, Lcom/adobe/mobile/MessageTemplateCallback;->tokenDataMask:[Z

    and-int/lit16 v8, v0, 0xff

    aget-boolean v7, v7, v8
    :try_end_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_0

    if-nez v7, :cond_0

    .line 370
    .end local v0    # "currentByte":B
    .end local v2    # "utf8Token":[B
    :goto_1
    return v3

    .line 358
    .restart local v0    # "currentByte":B
    .restart local v2    # "utf8Token":[B
    :cond_0
    add-int/lit8 v5, v5, 0x1

    goto :goto_0

    .line 365
    .end local v0    # "currentByte":B
    .end local v2    # "utf8Token":[B
    :catch_0
    move-exception v1

    .line 366
    .local v1, "ex":Ljava/io/UnsupportedEncodingException;
    const-string v5, "Data Callback - Unable to validate token (%s)"

    new-array v4, v4, [Ljava/lang/Object;

    invoke-virtual {v1}, Ljava/io/UnsupportedEncodingException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v3

    invoke-static {v5, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1

    .end local v1    # "ex":Ljava/io/UnsupportedEncodingException;
    .restart local v2    # "utf8Token":[B
    :cond_1
    move v3, v4

    .line 370
    goto :goto_1
.end method


# virtual methods
.method protected getQueue()Lcom/adobe/mobile/ThirdPartyQueue;
    .locals 1

    .prologue
    .line 224
    invoke-static {}, Lcom/adobe/mobile/ThirdPartyQueue;->sharedInstance()Lcom/adobe/mobile/ThirdPartyQueue;

    move-result-object v0

    return-object v0
.end method

.method protected initWithPayloadObject(Lorg/json/JSONObject;)Z
    .locals 13
    .param p1, "dictionary"    # Lorg/json/JSONObject;

    .prologue
    const/4 v12, 0x2

    const/4 v7, 0x1

    const/4 v6, 0x0

    .line 70
    if-eqz p1, :cond_0

    invoke-virtual {p1}, Lorg/json/JSONObject;->length()I

    move-result v8

    if-gtz v8, :cond_1

    .line 153
    :cond_0
    :goto_0
    return v6

    .line 75
    :cond_1
    invoke-super {p0, p1}, Lcom/adobe/mobile/Message;->initWithPayloadObject(Lorg/json/JSONObject;)Z

    move-result v8

    if-eqz v8, :cond_0

    .line 81
    invoke-virtual {p0}, Lcom/adobe/mobile/MessageTemplateCallback;->logPrefix()Ljava/lang/String;

    move-result-object v4

    .line 84
    .local v4, "logPrefix":Ljava/lang/String;
    :try_start_0
    const-string v8, "payload"

    invoke-virtual {p1, v8}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v3

    .line 85
    .local v3, "jsonPayload":Lorg/json/JSONObject;
    invoke-virtual {v3}, Lorg/json/JSONObject;->length()I

    move-result v8

    if-gtz v8, :cond_2

    .line 86
    const-string v8, "%s - Unable to create data callback %s, \"payload\" is empty"

    const/4 v9, 0x2

    new-array v9, v9, [Ljava/lang/Object;

    const/4 v10, 0x0

    aput-object v4, v9, v10

    const/4 v10, 0x1

    iget-object v11, p0, Lcom/adobe/mobile/MessageTemplateCallback;->messageId:Ljava/lang/String;

    aput-object v11, v9, v10

    invoke-static {v8, v9}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 90
    .end local v3    # "jsonPayload":Lorg/json/JSONObject;
    :catch_0
    move-exception v2

    .line 91
    .local v2, "ex":Lorg/json/JSONException;
    const-string v8, "%s - Unable to create create data callback %s, \"payload\" is required"

    new-array v9, v12, [Ljava/lang/Object;

    aput-object v4, v9, v6

    iget-object v10, p0, Lcom/adobe/mobile/MessageTemplateCallback;->messageId:Ljava/lang/String;

    aput-object v10, v9, v7

    invoke-static {v8, v9}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 97
    .end local v2    # "ex":Lorg/json/JSONException;
    .restart local v3    # "jsonPayload":Lorg/json/JSONObject;
    :cond_2
    :try_start_1
    const-string v8, "templateurl"

    invoke-virtual {v3, v8}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    iput-object v8, p0, Lcom/adobe/mobile/MessageTemplateCallback;->templateUrl:Ljava/lang/String;

    .line 98
    iget-object v8, p0, Lcom/adobe/mobile/MessageTemplateCallback;->templateUrl:Ljava/lang/String;

    invoke-virtual {v8}, Ljava/lang/String;->length()I

    move-result v8

    if-gtz v8, :cond_3

    .line 99
    const-string v8, "%s - Unable to create data callback %s, \"templateurl\" is empty"

    const/4 v9, 0x2

    new-array v9, v9, [Ljava/lang/Object;

    const/4 v10, 0x0

    aput-object v4, v9, v10

    const/4 v10, 0x1

    iget-object v11, p0, Lcom/adobe/mobile/MessageTemplateCallback;->messageId:Ljava/lang/String;

    aput-object v11, v9, v10

    invoke-static {v8, v9}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_0

    .line 103
    :catch_1
    move-exception v2

    .line 104
    .restart local v2    # "ex":Lorg/json/JSONException;
    const-string v8, "%s - Unable to create data callback %s, \"templateurl\" is required"

    new-array v9, v12, [Ljava/lang/Object;

    aput-object v4, v9, v6

    iget-object v10, p0, Lcom/adobe/mobile/MessageTemplateCallback;->messageId:Ljava/lang/String;

    aput-object v10, v9, v7

    invoke-static {v8, v9}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 110
    .end local v2    # "ex":Lorg/json/JSONException;
    :cond_3
    :try_start_2
    const-string v8, "timeout"

    invoke-virtual {v3, v8}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v8

    iput v8, p0, Lcom/adobe/mobile/MessageTemplateCallback;->timeout:I
    :try_end_2
    .catch Lorg/json/JSONException; {:try_start_2 .. :try_end_2} :catch_2

    .line 120
    :goto_1
    :try_start_3
    const-string v8, "templatebody"

    invoke-virtual {v3, v8}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    .line 121
    .local v5, "tempBody":Ljava/lang/String;
    if-eqz v5, :cond_4

    invoke-virtual {v5}, Ljava/lang/String;->length()I

    move-result v8

    if-lez v8, :cond_4

    .line 123
    const/4 v8, 0x0

    invoke-static {v5, v8}, Landroid/util/Base64;->decode(Ljava/lang/String;I)[B

    move-result-object v1

    .line 125
    .local v1, "decodedBytes":[B
    if-eqz v1, :cond_4

    .line 126
    new-instance v0, Ljava/lang/String;

    const-string v8, "UTF-8"

    invoke-direct {v0, v1, v8}, Ljava/lang/String;-><init>([BLjava/lang/String;)V

    .line 127
    .local v0, "decodedBody":Ljava/lang/String;
    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v8

    if-lez v8, :cond_4

    .line 128
    iput-object v0, p0, Lcom/adobe/mobile/MessageTemplateCallback;->templateBody:Ljava/lang/String;
    :try_end_3
    .catch Lorg/json/JSONException; {:try_start_3 .. :try_end_3} :catch_3
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_3 .. :try_end_3} :catch_4
    .catch Ljava/lang/IllegalArgumentException; {:try_start_3 .. :try_end_3} :catch_5

    .line 144
    .end local v0    # "decodedBody":Ljava/lang/String;
    .end local v1    # "decodedBytes":[B
    .end local v5    # "tempBody":Ljava/lang/String;
    :cond_4
    :goto_2
    iget-object v8, p0, Lcom/adobe/mobile/MessageTemplateCallback;->templateBody:Ljava/lang/String;

    if-eqz v8, :cond_5

    iget-object v8, p0, Lcom/adobe/mobile/MessageTemplateCallback;->templateBody:Ljava/lang/String;

    invoke-virtual {v8}, Ljava/lang/String;->length()I

    move-result v8

    if-lez v8, :cond_5

    .line 146
    :try_start_4
    const-string v8, "contenttype"

    invoke-virtual {v3, v8}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    iput-object v8, p0, Lcom/adobe/mobile/MessageTemplateCallback;->contentType:Ljava/lang/String;
    :try_end_4
    .catch Lorg/json/JSONException; {:try_start_4 .. :try_end_4} :catch_6

    :cond_5
    :goto_3
    move v6, v7

    .line 153
    goto/16 :goto_0

    .line 112
    :catch_2
    move-exception v2

    .line 113
    .restart local v2    # "ex":Lorg/json/JSONException;
    const-string v8, "%s - Tried to read \"timeout\" for data callback, but found none.  Using default value of two (2) seconds"

    new-array v9, v7, [Ljava/lang/Object;

    aput-object v4, v9, v6

    invoke-static {v8, v9}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 114
    iput v12, p0, Lcom/adobe/mobile/MessageTemplateCallback;->timeout:I

    goto :goto_1

    .line 133
    .end local v2    # "ex":Lorg/json/JSONException;
    :catch_3
    move-exception v2

    .line 134
    .restart local v2    # "ex":Lorg/json/JSONException;
    const-string v8, "%s - Tried to read \"templatebody\" for data callback, but found none.  This is not a required field"

    new-array v9, v7, [Ljava/lang/Object;

    aput-object v4, v9, v6

    invoke-static {v8, v9}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_2

    .line 136
    .end local v2    # "ex":Lorg/json/JSONException;
    :catch_4
    move-exception v2

    .line 137
    .local v2, "ex":Ljava/io/UnsupportedEncodingException;
    const-string v8, "%s - Failed to decode \"templatebody\" for data callback (%s).  This is not a required field"

    new-array v9, v12, [Ljava/lang/Object;

    aput-object v4, v9, v6

    invoke-virtual {v2}, Ljava/io/UnsupportedEncodingException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v10

    aput-object v10, v9, v7

    invoke-static {v8, v9}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_2

    .line 139
    .end local v2    # "ex":Ljava/io/UnsupportedEncodingException;
    :catch_5
    move-exception v2

    .line 140
    .local v2, "ex":Ljava/lang/IllegalArgumentException;
    const-string v8, "%s - Failed to decode \"templatebody\" for data callback (%s).  This is not a required field"

    new-array v9, v12, [Ljava/lang/Object;

    aput-object v4, v9, v6

    invoke-virtual {v2}, Ljava/lang/IllegalArgumentException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v10

    aput-object v10, v9, v7

    invoke-static {v8, v9}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_2

    .line 148
    .end local v2    # "ex":Ljava/lang/IllegalArgumentException;
    :catch_6
    move-exception v2

    .line 149
    .local v2, "ex":Lorg/json/JSONException;
    const-string v8, "%s - Tried to read \"contenttype\" for data callback, but found none.  This is not a required field"

    new-array v9, v7, [Ljava/lang/Object;

    aput-object v4, v9, v6

    invoke-static {v8, v9}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_3
.end method

.method protected logPrefix()Ljava/lang/String;
    .locals 1

    .prologue
    .line 229
    const-string v0, "Postbacks"

    return-object v0
.end method

.method protected shouldShowForVariables(Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;)Z
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
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
            ">;)Z"
        }
    .end annotation

    .prologue
    .line 158
    .local p1, "vars":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p2, "cdata":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p3, "lifecycleData":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    if-eqz p2, :cond_2

    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0, p2}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    .line 160
    .local v0, "combinedVars":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :goto_0
    if-eqz p1, :cond_0

    .line 161
    invoke-virtual {v0, p1}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 164
    :cond_0
    invoke-direct {p0, v0}, Lcom/adobe/mobile/MessageTemplateCallback;->getExpansionTokensForVariables(Ljava/util/HashMap;)Ljava/util/HashMap;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 166
    if-eqz p3, :cond_1

    .line 167
    invoke-virtual {v0, p3}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 170
    :cond_1
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1, v0}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    iput-object v1, p0, Lcom/adobe/mobile/MessageTemplateCallback;->_combinedVariablesCopy:Ljava/util/HashMap;

    .line 172
    invoke-super {p0, p1, p2, p3}, Lcom/adobe/mobile/Message;->shouldShowForVariables(Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;)Z

    move-result v1

    return v1

    .line 158
    .end local v0    # "combinedVars":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :cond_2
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    goto :goto_0
.end method

.method protected show()V
    .locals 9

    .prologue
    .line 178
    invoke-direct {p0}, Lcom/adobe/mobile/MessageTemplateCallback;->getExpandedUrl()Ljava/lang/String;

    move-result-object v1

    .line 179
    .local v1, "expandedUrl":Ljava/lang/String;
    invoke-direct {p0}, Lcom/adobe/mobile/MessageTemplateCallback;->getExpandedBody()Ljava/lang/String;

    move-result-object v2

    .line 180
    .local v2, "expandedBody":Ljava/lang/String;
    invoke-virtual {p0}, Lcom/adobe/mobile/MessageTemplateCallback;->logPrefix()Ljava/lang/String;

    move-result-object v8

    .line 182
    .local v8, "logPrefix":Ljava/lang/String;
    const-string v3, "%s - Request Queued (url:%s body:%s contentType:%s)"

    const/4 v4, 0x4

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    aput-object v8, v4, v5

    const/4 v5, 0x1

    aput-object v1, v4, v5

    const/4 v5, 0x2

    aput-object v2, v4, v5

    const/4 v5, 0x3

    iget-object v6, p0, Lcom/adobe/mobile/MessageTemplateCallback;->contentType:Ljava/lang/String;

    aput-object v6, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 183
    invoke-virtual {p0}, Lcom/adobe/mobile/MessageTemplateCallback;->getQueue()Lcom/adobe/mobile/ThirdPartyQueue;

    move-result-object v0

    .line 184
    .local v0, "currQueue":Lcom/adobe/mobile/ThirdPartyQueue;
    iget-object v3, p0, Lcom/adobe/mobile/MessageTemplateCallback;->contentType:Ljava/lang/String;

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getTimeSince1970()J

    move-result-wide v4

    iget v6, p0, Lcom/adobe/mobile/MessageTemplateCallback;->timeout:I

    int-to-long v6, v6

    invoke-virtual/range {v0 .. v7}, Lcom/adobe/mobile/ThirdPartyQueue;->queue(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JJ)V

    .line 185
    return-void
.end method
