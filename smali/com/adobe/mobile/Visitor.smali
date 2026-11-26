.class public final Lcom/adobe/mobile/Visitor;
.super Ljava/lang/Object;
.source "Visitor.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .prologue
    .line 25
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static getIdentifiers()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List",
            "<",
            "Lcom/adobe/mobile/VisitorID;",
            ">;"
        }
    .end annotation

    .prologue
    .line 54
    invoke-static {}, Lcom/adobe/mobile/VisitorIDService;->sharedInstance()Lcom/adobe/mobile/VisitorIDService;

    move-result-object v0

    invoke-virtual {v0}, Lcom/adobe/mobile/VisitorIDService;->getIdentifiers()Ljava/util/List;

    move-result-object v0

    return-object v0
.end method

.method public static getMarketingCloudId()Ljava/lang/String;
    .locals 1

    .prologue
    .line 28
    invoke-static {}, Lcom/adobe/mobile/VisitorIDService;->sharedInstance()Lcom/adobe/mobile/VisitorIDService;

    move-result-object v0

    invoke-virtual {v0}, Lcom/adobe/mobile/VisitorIDService;->getMarketingCloudID()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public static syncIdentifier(Ljava/lang/String;Ljava/lang/String;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;)V
    .locals 4
    .param p0, "identifierType"    # Ljava/lang/String;
    .param p1, "identifier"    # Ljava/lang/String;
    .param p2, "authenticationState"    # Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;

    .prologue
    .line 33
    if-eqz p0, :cond_0

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v1

    if-nez v1, :cond_1

    .line 34
    :cond_0
    const-string v1, "ID Service - Unable to sync VisitorID with id:%s, idType was nil/empty."

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-static {v1, v2}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 40
    :goto_0
    return-void

    .line 37
    :cond_1
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 38
    .local v0, "identifiers":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/String;>;"
    invoke-virtual {v0, p0, p1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 39
    invoke-static {}, Lcom/adobe/mobile/VisitorIDService;->sharedInstance()Lcom/adobe/mobile/VisitorIDService;

    move-result-object v1

    invoke-virtual {v1, v0, p2}, Lcom/adobe/mobile/VisitorIDService;->idSync(Ljava/util/Map;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;)V

    goto :goto_0
.end method

.method public static syncIdentifiers(Ljava/util/Map;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .prologue
    .line 44
    .local p0, "identifiers":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    invoke-static {}, Lcom/adobe/mobile/VisitorIDService;->sharedInstance()Lcom/adobe/mobile/VisitorIDService;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/adobe/mobile/VisitorIDService;->idSync(Ljava/util/Map;)V

    .line 45
    return-void
.end method

.method public static syncIdentifiers(Ljava/util/Map;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;)V
    .locals 1
    .param p1, "authenticationState"    # Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;",
            "Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;",
            ")V"
        }
    .end annotation

    .prologue
    .line 49
    .local p0, "identifiers":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    invoke-static {}, Lcom/adobe/mobile/VisitorIDService;->sharedInstance()Lcom/adobe/mobile/VisitorIDService;

    move-result-object v0

    invoke-virtual {v0, p0, p1}, Lcom/adobe/mobile/VisitorIDService;->idSync(Ljava/util/Map;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;)V

    .line 50
    return-void
.end method
