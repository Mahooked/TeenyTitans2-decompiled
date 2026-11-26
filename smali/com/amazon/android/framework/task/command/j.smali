.class final Lcom/amazon/android/framework/task/command/j;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/content/ServiceConnection;


# instance fields
.field private synthetic a:Lcom/amazon/android/framework/task/command/c;


# direct methods
.method constructor <init>(Lcom/amazon/android/framework/task/command/c;)V
    .locals 0

    iput-object p1, p0, Lcom/amazon/android/framework/task/command/j;->a:Lcom/amazon/android/framework/task/command/c;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onServiceConnected(Landroid/content/ComponentName;Landroid/os/IBinder;)V
    .locals 2

    invoke-static {}, Lcom/amazon/android/framework/task/command/c;->c()Lcom/amazon/android/framework/util/KiwiLogger;

    move-result-object v0

    const-string v1, "onServiceConnected"

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    iget-object v0, p0, Lcom/amazon/android/framework/task/command/j;->a:Lcom/amazon/android/framework/task/command/c;

    invoke-static {v0}, Lcom/amazon/android/framework/task/command/c;->a(Lcom/amazon/android/framework/task/command/c;)Ljava/util/concurrent/BlockingQueue;

    move-result-object v0

    if-nez p2, :cond_0

    const/4 v1, 0x0

    :goto_0
    invoke-interface {v0, v1}, Ljava/util/concurrent/BlockingQueue;->add(Ljava/lang/Object;)Z

    return-void

    :cond_0
    const-string v1, "com.amazon.venezia.command.CommandService"

    invoke-interface {p2, v1}, Landroid/os/IBinder;->queryLocalInterface(Ljava/lang/String;)Landroid/os/IInterface;

    move-result-object p0

    if-eqz p0, :cond_1

    instance-of v1, p0, Lcom/amazon/venezia/command/h;

    if-eqz v1, :cond_1

    check-cast p0, Lcom/amazon/venezia/command/h;

    move-object v1, p0

    goto :goto_0

    :cond_1
    new-instance v1, Lcom/amazon/venezia/command/l;

    invoke-direct {v1, p2}, Lcom/amazon/venezia/command/l;-><init>(Landroid/os/IBinder;)V

    goto :goto_0
.end method

.method public final onServiceDisconnected(Landroid/content/ComponentName;)V
    .locals 2

    invoke-static {}, Lcom/amazon/android/framework/task/command/c;->c()Lcom/amazon/android/framework/util/KiwiLogger;

    move-result-object v0

    const-string v1, "onServiceDisconnected!!!"

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    return-void
.end method
