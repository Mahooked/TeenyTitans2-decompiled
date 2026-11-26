.class Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$1;
.super Landroid/content/BroadcastReceiver;
.source "UnityBluetoothLE.java"


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
    .line 115
    iput-object p1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$1;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-direct {p0}, Landroid/content/BroadcastReceiver;-><init>()V

    return-void
.end method


# virtual methods
.method public onReceive(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 11
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "intent"    # Landroid/content/Intent;

    .prologue
    const/4 v6, 0x0

    .line 120
    invoke-virtual {p2}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object v0

    .line 122
    .local v0, "action":Ljava/lang/String;
    const-string v5, "android.bluetooth.device.action.UUID"

    invoke-virtual {v5, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_2

    .line 124
    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$1;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    const-string v7, "got action_uuid"

    invoke-virtual {v5, v7}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 126
    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$1;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v5}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$000(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Ljava/util/ArrayList;

    move-result-object v5

    if-eqz v5, :cond_2

    .line 128
    const-string v5, "android.bluetooth.device.extra.DEVICE"

    invoke-virtual {p2, v5}, Landroid/content/Intent;->getParcelableExtra(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object v1

    check-cast v1, Landroid/bluetooth/BluetoothDevice;

    .line 129
    .local v1, "device":Landroid/bluetooth/BluetoothDevice;
    const-string v5, "android.bluetooth.device.extra.UUID"

    invoke-virtual {p2, v5}, Landroid/content/Intent;->getParcelableArrayExtra(Ljava/lang/String;)[Landroid/os/Parcelable;

    move-result-object v3

    .line 131
    .local v3, "uuidExtra":[Landroid/os/Parcelable;
    if-eqz v1, :cond_1

    if-eqz v3, :cond_1

    .line 132
    array-length v7, v3

    move v5, v6

    :goto_0
    if-ge v5, v7, :cond_2

    aget-object v4, v3, v5

    .line 134
    .local v4, "uuidParcel":Landroid/os/Parcelable;
    invoke-virtual {v4}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    .line 136
    .local v2, "uuid":Ljava/lang/String;
    iget-object v8, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$1;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    new-instance v9, Ljava/lang/StringBuilder;

    invoke-direct {v9}, Ljava/lang/StringBuilder;-><init>()V

    const-string v10, "checking uuid "

    invoke-virtual {v9, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v9

    invoke-virtual {v9, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v9

    invoke-virtual {v9}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v9

    invoke-virtual {v8, v9}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 138
    iget-object v8, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$1;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v8}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$000(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Ljava/util/ArrayList;

    move-result-object v8

    iget-object v9, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$1;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v9, v2}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$100(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v9

    invoke-virtual {v8, v9}, Ljava/util/ArrayList;->contains(Ljava/lang/Object;)Z

    move-result v8

    if-eqz v8, :cond_0

    .line 139
    iget-object v8, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$1;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    const/4 v9, 0x0

    invoke-static {v8, v1, v6, v9}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$200(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;Landroid/bluetooth/BluetoothDevice;I[B)V

    .line 132
    :cond_0
    add-int/lit8 v5, v5, 0x1

    goto :goto_0

    .line 144
    .end local v2    # "uuid":Ljava/lang/String;
    .end local v4    # "uuidParcel":Landroid/os/Parcelable;
    :cond_1
    if-eqz v1, :cond_3

    .line 145
    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$1;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, "device: "

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v6

    invoke-virtual {v1}, Landroid/bluetooth/BluetoothDevice;->getAddress()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v6

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v5, v6}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 149
    :goto_1
    if-eqz v3, :cond_4

    .line 150
    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$1;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, "uuid count: "

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v6

    array-length v7, v3

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v6

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v5, v6}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 156
    .end local v1    # "device":Landroid/bluetooth/BluetoothDevice;
    .end local v3    # "uuidExtra":[Landroid/os/Parcelable;
    :cond_2
    :goto_2
    return-void

    .line 147
    .restart local v1    # "device":Landroid/bluetooth/BluetoothDevice;
    .restart local v3    # "uuidExtra":[Landroid/os/Parcelable;
    :cond_3
    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$1;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    const-string v6, "device is null"

    invoke-virtual {v5, v6}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    goto :goto_1

    .line 152
    :cond_4
    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$1;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    const-string v6, "uuidExtra is null"

    invoke-virtual {v5, v6}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    goto :goto_2
.end method
