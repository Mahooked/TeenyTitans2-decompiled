.class public Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;
.super Ljava/lang/Object;
.source "UnityBluetoothLE.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1
    name = "AdRecord"
.end annotation


# instance fields
.field public Data:[B

.field public Length:I

.field public Type:I

.field final synthetic this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;


# direct methods
.method public constructor <init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;II[B)V
    .locals 0
    .param p1, "this$0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;
    .param p2, "length"    # I
    .param p3, "type"    # I
    .param p4, "data"    # [B

    .prologue
    .line 303
    iput-object p1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 305
    iput p2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;->Length:I

    .line 306
    iput p3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;->Type:I

    .line 307
    iput-object p4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;->Data:[B

    .line 308
    return-void
.end method


# virtual methods
.method public decodeRecord()Ljava/lang/String;
    .locals 5

    .prologue
    .line 312
    const-string v0, ""

    .line 315
    .local v0, "decodedRecord":Ljava/lang/String;
    :try_start_0
    new-instance v1, Ljava/lang/String;

    iget-object v3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;->Data:[B

    const-string v4, "UTF-8"

    invoke-direct {v1, v3, v4}, Ljava/lang/String;-><init>([BLjava/lang/String;)V
    :try_end_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_0

    .end local v0    # "decodedRecord":Ljava/lang/String;
    .local v1, "decodedRecord":Ljava/lang/String;
    move-object v0, v1

    .line 321
    .end local v1    # "decodedRecord":Ljava/lang/String;
    .restart local v0    # "decodedRecord":Ljava/lang/String;
    :goto_0
    return-object v0

    .line 317
    :catch_0
    move-exception v2

    .line 318
    .local v2, "e":Ljava/io/UnsupportedEncodingException;
    invoke-virtual {v2}, Ljava/io/UnsupportedEncodingException;->printStackTrace()V

    goto :goto_0
.end method
