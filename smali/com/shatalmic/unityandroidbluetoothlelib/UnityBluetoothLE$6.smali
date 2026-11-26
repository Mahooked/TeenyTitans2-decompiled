.class Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$6;
.super Ljava/lang/Object;
.source "UnityBluetoothLE.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothConnectToPeripheral(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

.field final synthetic val$device:Landroid/bluetooth/BluetoothDevice;


# direct methods
.method constructor <init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;Landroid/bluetooth/BluetoothDevice;)V
    .locals 0
    .param p1, "this$0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    .prologue
    .line 746
    iput-object p1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$6;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    iput-object p2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$6;->val$device:Landroid/bluetooth/BluetoothDevice;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    .prologue
    .line 751
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$6;->val$device:Landroid/bluetooth/BluetoothDevice;

    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$6;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v1}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$600(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Landroid/content/Context;

    move-result-object v1

    const/4 v2, 0x0

    iget-object v3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$6;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v3}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$700(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Landroid/bluetooth/BluetoothGattCallback;

    move-result-object v3

    invoke-virtual {v0, v1, v2, v3}, Landroid/bluetooth/BluetoothDevice;->connectGatt(Landroid/content/Context;ZLandroid/bluetooth/BluetoothGattCallback;)Landroid/bluetooth/BluetoothGatt;

    .line 752
    return-void
.end method
