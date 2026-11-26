.class Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$2;
.super Ljava/lang/Object;
.source "UnityBluetoothLE.java"

# interfaces
.implements Landroid/bluetooth/BluetoothAdapter$LeScanCallback;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;


# direct methods
.method constructor <init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)V
    .locals 0
    .param p1, "this$0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    .prologue
    .line 423
    iput-object p1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$2;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onLeScan(Landroid/bluetooth/BluetoothDevice;I[B)V
    .locals 1
    .param p1, "device"    # Landroid/bluetooth/BluetoothDevice;
    .param p2, "rssi"    # I
    .param p3, "scanRecord"    # [B

    .prologue
    .line 427
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$2;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v0, p1, p2, p3}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$200(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;Landroid/bluetooth/BluetoothDevice;I[B)V

    .line 428
    return-void
.end method
