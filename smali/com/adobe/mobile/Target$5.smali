.class final Lcom/adobe/mobile/Target$5;
.super Ljava/lang/Object;
.source "Target.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/adobe/mobile/Target;->clearCookies()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# direct methods
.method constructor <init>()V
    .locals 0

    .prologue
    .line 138
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 1

    .prologue
    const/4 v0, 0x0

    .line 141
    invoke-static {}, Lcom/adobe/mobile/TargetWorker;->setTntIdAndSessionFromOldCookieValues()V

    .line 143
    invoke-static {v0}, Lcom/adobe/mobile/TargetWorker;->setTntId(Ljava/lang/String;)V

    .line 144
    invoke-static {v0}, Lcom/adobe/mobile/TargetWorker;->setThirdPartyId(Ljava/lang/String;)V

    .line 145
    invoke-static {v0}, Lcom/adobe/mobile/TargetWorker;->setSessionId(Ljava/lang/String;)V

    .line 146
    invoke-static {v0}, Lcom/adobe/mobile/TargetWorker;->setEdgeHost(Ljava/lang/String;)V

    .line 147
    return-void
.end method
