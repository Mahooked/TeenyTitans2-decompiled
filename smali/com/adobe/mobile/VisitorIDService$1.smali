.class Lcom/adobe/mobile/VisitorIDService$1;
.super Ljava/lang/Object;
.source "VisitorIDService.java"

# interfaces
.implements Ljava/util/concurrent/Callable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/adobe/mobile/VisitorIDService;->resetVariablesFromSharedPreferences()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Ljava/util/concurrent/Callable",
        "<",
        "Ljava/lang/Void;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic this$0:Lcom/adobe/mobile/VisitorIDService;


# direct methods
.method constructor <init>(Lcom/adobe/mobile/VisitorIDService;)V
    .locals 0
    .param p1, "this$0"    # Lcom/adobe/mobile/VisitorIDService;

    .prologue
    .line 90
    iput-object p1, p0, Lcom/adobe/mobile/VisitorIDService$1;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public bridge synthetic call()Ljava/lang/Object;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .prologue
    .line 90
    invoke-virtual {p0}, Lcom/adobe/mobile/VisitorIDService$1;->call()Ljava/lang/Void;

    move-result-object v0

    return-object v0
.end method

.method public call()Ljava/lang/Void;
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .prologue
    const/4 v6, 0x0

    .line 94
    :try_start_0
    iget-object v1, p0, Lcom/adobe/mobile/VisitorIDService$1;->this$0:Lcom/adobe/mobile/VisitorIDService;

    iget-object v2, p0, Lcom/adobe/mobile/VisitorIDService$1;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v3

    const-string v4, "ADBMOBILE_VISITORID_IDS"

    const/4 v5, 0x0

    invoke-interface {v3, v4, v5}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-static {v2, v3}, Lcom/adobe/mobile/VisitorIDService;->access$100(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/util/List;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/adobe/mobile/VisitorIDService;->access$002(Lcom/adobe/mobile/VisitorIDService;Ljava/util/List;)Ljava/util/List;

    .line 95
    iget-object v1, p0, Lcom/adobe/mobile/VisitorIDService$1;->this$0:Lcom/adobe/mobile/VisitorIDService;

    iget-object v2, p0, Lcom/adobe/mobile/VisitorIDService$1;->this$0:Lcom/adobe/mobile/VisitorIDService;

    iget-object v3, p0, Lcom/adobe/mobile/VisitorIDService$1;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v3}, Lcom/adobe/mobile/VisitorIDService;->access$000(Lcom/adobe/mobile/VisitorIDService;)Ljava/util/List;

    move-result-object v3

    invoke-static {v2, v3}, Lcom/adobe/mobile/VisitorIDService;->access$300(Lcom/adobe/mobile/VisitorIDService;Ljava/util/List;)Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/adobe/mobile/VisitorIDService;->access$202(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;

    .line 96
    iget-object v1, p0, Lcom/adobe/mobile/VisitorIDService$1;->this$0:Lcom/adobe/mobile/VisitorIDService;

    iget-object v2, p0, Lcom/adobe/mobile/VisitorIDService$1;->this$0:Lcom/adobe/mobile/VisitorIDService;

    iget-object v3, p0, Lcom/adobe/mobile/VisitorIDService$1;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v3}, Lcom/adobe/mobile/VisitorIDService;->access$000(Lcom/adobe/mobile/VisitorIDService;)Ljava/util/List;

    move-result-object v3

    invoke-static {v2, v3}, Lcom/adobe/mobile/VisitorIDService;->access$500(Lcom/adobe/mobile/VisitorIDService;Ljava/util/List;)Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/adobe/mobile/VisitorIDService;->access$402(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;

    .line 97
    iget-object v1, p0, Lcom/adobe/mobile/VisitorIDService$1;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v2

    const-string v3, "ADBMOBILE_PERSISTED_MID"

    const/4 v4, 0x0

    invoke-interface {v2, v3, v4}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/adobe/mobile/VisitorIDService;->access$602(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;

    .line 98
    iget-object v1, p0, Lcom/adobe/mobile/VisitorIDService$1;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v2

    const-string v3, "ADBMOBILE_PERSISTED_MID_HINT"

    const/4 v4, 0x0

    invoke-interface {v2, v3, v4}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/adobe/mobile/VisitorIDService;->access$702(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;

    .line 99
    iget-object v1, p0, Lcom/adobe/mobile/VisitorIDService$1;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v2

    const-string v3, "ADBMOBILE_PERSISTED_MID_BLOB"

    const/4 v4, 0x0

    invoke-interface {v2, v3, v4}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/adobe/mobile/VisitorIDService;->access$802(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;

    .line 100
    iget-object v1, p0, Lcom/adobe/mobile/VisitorIDService$1;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v2

    const-string v3, "ADBMOBILE_VISITORID_TTL"

    const-wide/16 v4, 0x0

    invoke-interface {v2, v3, v4, v5}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v2

    invoke-static {v1, v2, v3}, Lcom/adobe/mobile/VisitorIDService;->access$902(Lcom/adobe/mobile/VisitorIDService;J)J

    .line 101
    iget-object v1, p0, Lcom/adobe/mobile/VisitorIDService$1;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v2

    const-string v3, "ADBMOBILE_VISITORID_SYNC"

    const-wide/16 v4, 0x0

    invoke-interface {v2, v3, v4, v5}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v2

    invoke-static {v1, v2, v3}, Lcom/adobe/mobile/VisitorIDService;->access$1002(Lcom/adobe/mobile/VisitorIDService;J)J
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    .line 109
    :goto_0
    return-object v6

    .line 102
    :catch_0
    move-exception v0

    .line 103
    .local v0, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    iget-object v1, p0, Lcom/adobe/mobile/VisitorIDService$1;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v1, v6}, Lcom/adobe/mobile/VisitorIDService;->access$602(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;

    .line 104
    iget-object v1, p0, Lcom/adobe/mobile/VisitorIDService$1;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v1, v6}, Lcom/adobe/mobile/VisitorIDService;->access$702(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;

    .line 105
    iget-object v1, p0, Lcom/adobe/mobile/VisitorIDService$1;->this$0:Lcom/adobe/mobile/VisitorIDService;

    invoke-static {v1, v6}, Lcom/adobe/mobile/VisitorIDService;->access$802(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;

    .line 107
    const-string v1, "Visitor - Unable to check for stored visitor ID due to context error (%s)"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v4

    aput-object v4, v2, v3

    invoke-static {v1, v2}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method
