.class Lcom/kayac/unitywebview/WebViewPluginInterface;
.super Ljava/lang/Object;
.source "WebViewPlugin.java"


# instance fields
.field private mGameObject:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;)V
    .locals 0
    .param p1, "gameObject"    # Ljava/lang/String;

    .prologue
    .line 52
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 53
    iput-object p1, p0, Lcom/kayac/unitywebview/WebViewPluginInterface;->mGameObject:Ljava/lang/String;

    .line 54
    return-void
.end method


# virtual methods
.method public call(Ljava/lang/String;)V
    .locals 2
    .param p1, "message"    # Ljava/lang/String;

    .prologue
    .line 58
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPluginInterface;->mGameObject:Ljava/lang/String;

    const-string v1, "CallFromJS"

    invoke-static {v0, v1, p1}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 59
    return-void
.end method
