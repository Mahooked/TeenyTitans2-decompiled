.class Lcom/adobe/mobile/MobileConfig$12;
.super Ljava/lang/Object;
.source "MobileConfig.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/adobe/mobile/MobileConfig;->loadMessageImages()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/adobe/mobile/MobileConfig;


# direct methods
.method constructor <init>(Lcom/adobe/mobile/MobileConfig;)V
    .locals 0
    .param p1, "this$0"    # Lcom/adobe/mobile/MobileConfig;

    .prologue
    .line 1101
    iput-object p1, p0, Lcom/adobe/mobile/MobileConfig$12;->this$0:Lcom/adobe/mobile/MobileConfig;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 10

    .prologue
    const/16 v9, 0x2710

    .line 1105
    iget-object v4, p0, Lcom/adobe/mobile/MobileConfig$12;->this$0:Lcom/adobe/mobile/MobileConfig;

    invoke-static {v4}, Lcom/adobe/mobile/MobileConfig;->access$300(Lcom/adobe/mobile/MobileConfig;)Ljava/util/ArrayList;

    move-result-object v4

    if-eqz v4, :cond_0

    iget-object v4, p0, Lcom/adobe/mobile/MobileConfig$12;->this$0:Lcom/adobe/mobile/MobileConfig;

    invoke-static {v4}, Lcom/adobe/mobile/MobileConfig;->access$300(Lcom/adobe/mobile/MobileConfig;)Ljava/util/ArrayList;

    move-result-object v4

    invoke-virtual {v4}, Ljava/util/ArrayList;->size()I

    move-result v4

    if-gtz v4, :cond_1

    .line 1106
    :cond_0
    const-string v4, "messageImages"

    invoke-static {v4}, Lcom/adobe/mobile/RemoteDownload;->deleteFilesInDirectory(Ljava/lang/String;)V

    .line 1140
    :goto_0
    return-void

    .line 1110
    :cond_1
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 1113
    .local v0, "assetUrls":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/String;>;"
    iget-object v4, p0, Lcom/adobe/mobile/MobileConfig$12;->this$0:Lcom/adobe/mobile/MobileConfig;

    invoke-static {v4}, Lcom/adobe/mobile/MobileConfig;->access$300(Lcom/adobe/mobile/MobileConfig;)Ljava/util/ArrayList;

    move-result-object v4

    invoke-virtual {v4}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :cond_2
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_4

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/adobe/mobile/Message;

    .line 1115
    .local v3, "message":Lcom/adobe/mobile/Message;
    iget-object v5, v3, Lcom/adobe/mobile/Message;->assets:Ljava/util/ArrayList;

    if-eqz v5, :cond_2

    iget-object v5, v3, Lcom/adobe/mobile/Message;->assets:Ljava/util/ArrayList;

    invoke-virtual {v5}, Ljava/util/ArrayList;->size()I

    move-result v5

    if-lez v5, :cond_2

    .line 1120
    iget-object v5, v3, Lcom/adobe/mobile/Message;->assets:Ljava/util/ArrayList;

    invoke-virtual {v5}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v5

    :cond_3
    invoke-interface {v5}, Ljava/util/Iterator;->hasNext()Z

    move-result v6

    if-eqz v6, :cond_2

    invoke-interface {v5}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/ArrayList;

    .line 1121
    .local v2, "currentAssetArray":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/String;>;"
    invoke-virtual {v2}, Ljava/util/ArrayList;->size()I

    move-result v6

    if-lez v6, :cond_3

    .line 1126
    invoke-virtual {v2}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v6

    :goto_1
    invoke-interface {v6}, Ljava/util/Iterator;->hasNext()Z

    move-result v7

    if-eqz v7, :cond_3

    invoke-interface {v6}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 1127
    .local v1, "currentAsset":Ljava/lang/String;
    invoke-virtual {v0, v1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 1128
    const/4 v7, 0x0

    const-string v8, "messageImages"

    invoke-static {v1, v9, v9, v7, v8}, Lcom/adobe/mobile/RemoteDownload;->remoteDownloadSync(Ljava/lang/String;IILcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;Ljava/lang/String;)V

    goto :goto_1

    .line 1134
    .end local v1    # "currentAsset":Ljava/lang/String;
    .end local v2    # "currentAssetArray":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/String;>;"
    .end local v3    # "message":Lcom/adobe/mobile/Message;
    :cond_4
    invoke-virtual {v0}, Ljava/util/ArrayList;->size()I

    move-result v4

    if-lez v4, :cond_5

    .line 1135
    const-string v4, "messageImages"

    invoke-static {v4, v0}, Lcom/adobe/mobile/RemoteDownload;->deleteFilesForDirectoryNotInList(Ljava/lang/String;Ljava/util/List;)V

    goto :goto_0

    .line 1138
    :cond_5
    const-string v4, "messageImages"

    invoke-static {v4}, Lcom/adobe/mobile/RemoteDownload;->deleteFilesInDirectory(Ljava/lang/String;)V

    goto :goto_0
.end method
