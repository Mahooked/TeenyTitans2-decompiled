.class public Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;
.super Ljava/lang/Object;
.source "UnityBluetoothLE.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1
    name = "AdRecordList"
.end annotation


# instance fields
.field Records:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List",
            "<",
            "Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;",
            ">;"
        }
    .end annotation
.end field

.field final synthetic this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;


# direct methods
.method public constructor <init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)V
    .locals 1
    .param p1, "this$0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    .prologue
    .line 325
    iput-object p1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 327
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;->Records:Ljava/util/List;

    return-void
.end method


# virtual methods
.method public getRecord(I)Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;
    .locals 3
    .param p1, "type"    # I

    .prologue
    .line 355
    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;->Records:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;

    .line 357
    .local v0, "record":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;
    iget v2, v0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;->Type:I

    if-ne v2, p1, :cond_0

    .line 361
    .end local v0    # "record":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;
    :goto_0
    return-object v0

    :cond_1
    const/4 v0, 0x0

    goto :goto_0
.end method

.method public parseScanRecord([B)Ljava/util/List;
    .locals 8
    .param p1, "scanRecord"    # [B
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "([B)",
            "Ljava/util/List",
            "<",
            "Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;",
            ">;"
        }
    .end annotation

    .prologue
    .line 331
    const/4 v1, 0x0

    .line 332
    .local v1, "index":I
    :goto_0
    array-length v5, p1

    if-ge v1, v5, :cond_0

    .line 334
    add-int/lit8 v2, v1, 0x1

    .end local v1    # "index":I
    .local v2, "index":I
    aget-byte v3, p1, v1

    .line 336
    .local v3, "length":I
    if-nez v3, :cond_1

    move v1, v2

    .line 350
    .end local v2    # "index":I
    .end local v3    # "length":I
    .restart local v1    # "index":I
    :cond_0
    :goto_1
    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;->Records:Ljava/util/List;

    return-object v5

    .line 338
    .end local v1    # "index":I
    .restart local v2    # "index":I
    .restart local v3    # "length":I
    :cond_1
    aget-byte v4, p1, v2

    .line 340
    .local v4, "type":I
    if-nez v4, :cond_2

    move v1, v2

    .end local v2    # "index":I
    .restart local v1    # "index":I
    goto :goto_1

    .line 342
    .end local v1    # "index":I
    .restart local v2    # "index":I
    :cond_2
    add-int/lit8 v5, v2, 0x1

    add-int v6, v2, v3

    invoke-static {p1, v5, v6}, Ljava/util/Arrays;->copyOfRange([BII)[B

    move-result-object v0

    .line 344
    .local v0, "data":[B
    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;->Records:Ljava/util/List;

    new-instance v6, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;

    iget-object v7, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-direct {v6, v7, v3, v4, v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;-><init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;II[B)V

    invoke-interface {v5, v6}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 347
    add-int v1, v2, v3

    .line 348
    .end local v2    # "index":I
    .restart local v1    # "index":I
    goto :goto_0
.end method
