.class Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$4;
.super Ljava/lang/Object;
.source "UnityBluetoothLE.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothRetrieveListOfPeripheralsWithServices(Ljava/lang/String;)V
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
    .line 599
    iput-object p1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$4;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 6

    .prologue
    .line 604
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$4;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v2}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$400(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Landroid/bluetooth/BluetoothAdapter;

    move-result-object v2

    invoke-virtual {v2}, Landroid/bluetooth/BluetoothAdapter;->getBondedDevices()Ljava/util/Set;

    move-result-object v1

    .line 605
    .local v1, "deviceSet":Ljava/util/Set;, "Ljava/util/Set<Landroid/bluetooth/BluetoothDevice;>;"
    if-eqz v1, :cond_2

    invoke-interface {v1}, Ljava/util/Set;->size()I

    move-result v2

    if-lez v2, :cond_2

    .line 607
    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :cond_0
    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_2

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/bluetooth/BluetoothDevice;

    .line 609
    .local v0, "device":Landroid/bluetooth/BluetoothDevice;
    if-eqz v0, :cond_0

    .line 611
    iget-object v3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$4;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "got device "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v0}, Landroid/bluetooth/BluetoothDevice;->getAddress()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 613
    iget-object v3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$4;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v3}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$000(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Ljava/util/ArrayList;

    move-result-object v3

    invoke-virtual {v3}, Ljava/util/ArrayList;->size()I

    move-result v3

    if-lez v3, :cond_1

    .line 614
    invoke-virtual {v0}, Landroid/bluetooth/BluetoothDevice;->fetchUuidsWithSdp()Z

    goto :goto_0

    .line 616
    :cond_1
    iget-object v3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$4;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    const/4 v4, 0x0

    const/4 v5, 0x0

    invoke-static {v3, v0, v4, v5}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$200(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;Landroid/bluetooth/BluetoothDevice;I[B)V

    goto :goto_0

    .line 620
    .end local v0    # "device":Landroid/bluetooth/BluetoothDevice;
    :cond_2
    return-void
.end method
