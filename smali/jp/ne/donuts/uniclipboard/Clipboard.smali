.class public Ljp/ne/donuts/uniclipboard/Clipboard;
.super Ljava/lang/Object;
.source "Clipboard.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .prologue
    .line 14
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static getText()Ljava/lang/String;
    .locals 4

    .prologue
    .line 32
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    .line 33
    .local v0, "activity":Landroid/app/Activity;
    new-instance v2, Ljava/util/concurrent/FutureTask;

    new-instance v3, Ljp/ne/donuts/uniclipboard/Clipboard$2;

    invoke-direct {v3, v0}, Ljp/ne/donuts/uniclipboard/Clipboard$2;-><init>(Landroid/app/Activity;)V

    invoke-direct {v2, v3}, Ljava/util/concurrent/FutureTask;-><init>(Ljava/util/concurrent/Callable;)V

    .line 45
    .local v2, "futureResult":Ljava/util/concurrent/FutureTask;, "Ljava/util/concurrent/FutureTask<Ljava/lang/String;>;"
    invoke-virtual {v0, v2}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    .line 48
    :try_start_0
    invoke-virtual {v2}, Ljava/util/concurrent/FutureTask;->get()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 50
    :goto_0
    return-object v3

    .line 49
    :catch_0
    move-exception v1

    .line 50
    .local v1, "e":Ljava/lang/Exception;
    const-string v3, ""

    goto :goto_0
.end method

.method public static setText(Ljava/lang/String;)V
    .locals 2
    .param p0, "text"    # Ljava/lang/String;

    .prologue
    .line 17
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    .line 18
    .local v0, "activity":Landroid/app/Activity;
    new-instance v1, Ljp/ne/donuts/uniclipboard/Clipboard$1;

    invoke-direct {v1, p0, v0}, Ljp/ne/donuts/uniclipboard/Clipboard$1;-><init>(Ljava/lang/String;Landroid/app/Activity;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    .line 29
    return-void
.end method
