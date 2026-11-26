.class final Lcom/amazon/android/framework/task/command/i;
.super Lcom/amazon/venezia/command/p;


# instance fields
.field private synthetic a:Lcom/amazon/android/framework/task/command/b;

.field private synthetic b:Lcom/amazon/android/framework/task/command/c;


# direct methods
.method constructor <init>(Lcom/amazon/android/framework/task/command/c;Lcom/amazon/android/framework/task/command/b;)V
    .locals 0

    iput-object p1, p0, Lcom/amazon/android/framework/task/command/i;->b:Lcom/amazon/android/framework/task/command/c;

    iput-object p2, p0, Lcom/amazon/android/framework/task/command/i;->a:Lcom/amazon/android/framework/task/command/b;

    invoke-direct {p0}, Lcom/amazon/venezia/command/p;-><init>()V

    return-void
.end method


# virtual methods
.method public final a()Ljava/lang/String;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    iget-object v0, p0, Lcom/amazon/android/framework/task/command/i;->a:Lcom/amazon/android/framework/task/command/b;

    invoke-virtual {v0}, Lcom/amazon/android/framework/task/command/b;->name()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public final b()Ljava/util/Map;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    const/4 v0, 0x0

    return-object v0
.end method
