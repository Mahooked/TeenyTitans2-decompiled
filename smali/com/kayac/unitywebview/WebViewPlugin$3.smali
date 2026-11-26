.class Lcom/kayac/unitywebview/WebViewPlugin$3;
.super Ljava/lang/Object;
.source "WebViewPlugin.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/kayac/unitywebview/WebViewPlugin;->LoadURL(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/kayac/unitywebview/WebViewPlugin;

.field final synthetic val$url:Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/kayac/unitywebview/WebViewPlugin;Ljava/lang/String;)V
    .locals 0

    .prologue
    .line 193
    iput-object p1, p0, Lcom/kayac/unitywebview/WebViewPlugin$3;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    iput-object p2, p0, Lcom/kayac/unitywebview/WebViewPlugin$3;->val$url:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .prologue
    .line 195
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin$3;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lcom/kayac/unitywebview/WebViewPlugin;->access$400(Lcom/kayac/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    iget-object v1, p0, Lcom/kayac/unitywebview/WebViewPlugin$3;->val$url:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/webkit/WebView;->loadUrl(Ljava/lang/String;)V

    .line 197
    return-void
.end method
