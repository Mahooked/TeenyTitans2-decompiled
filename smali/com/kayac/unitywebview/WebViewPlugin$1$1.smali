.class Lcom/kayac/unitywebview/WebViewPlugin$1$1;
.super Landroid/webkit/WebViewClient;
.source "WebViewPlugin.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/kayac/unitywebview/WebViewPlugin$1;->run()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lcom/kayac/unitywebview/WebViewPlugin$1;


# direct methods
.method constructor <init>(Lcom/kayac/unitywebview/WebViewPlugin$1;)V
    .locals 0

    .prologue
    .line 106
    iput-object p1, p0, Lcom/kayac/unitywebview/WebViewPlugin$1$1;->this$1:Lcom/kayac/unitywebview/WebViewPlugin$1;

    invoke-direct {p0}, Landroid/webkit/WebViewClient;-><init>()V

    return-void
.end method


# virtual methods
.method public onPageFinished(Landroid/webkit/WebView;Ljava/lang/String;)V
    .locals 3
    .param p1, "view"    # Landroid/webkit/WebView;
    .param p2, "url"    # Ljava/lang/String;

    .prologue
    .line 109
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin$1$1;->this$1:Lcom/kayac/unitywebview/WebViewPlugin$1;

    iget-object v0, v0, Lcom/kayac/unitywebview/WebViewPlugin$1;->val$gameObject:Ljava/lang/String;

    const-string v1, "LoadComplete"

    const-string v2, "1"

    invoke-static {v0, v1, v2}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 110
    return-void
.end method

.method public onReceivedError(Landroid/webkit/WebView;ILjava/lang/String;Ljava/lang/String;)V
    .locals 3
    .param p1, "view"    # Landroid/webkit/WebView;
    .param p2, "errorCode"    # I
    .param p3, "description"    # Ljava/lang/String;
    .param p4, "failingUrl"    # Ljava/lang/String;

    .prologue
    .line 114
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin$1$1;->this$1:Lcom/kayac/unitywebview/WebViewPlugin$1;

    iget-object v0, v0, Lcom/kayac/unitywebview/WebViewPlugin$1;->val$gameObject:Ljava/lang/String;

    const-string v1, "LoadComplete"

    const-string v2, "0"

    invoke-static {v0, v1, v2}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 115
    return-void
.end method
