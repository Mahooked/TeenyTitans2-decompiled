.class Lcom/kayac/unitywebview/WebViewPlugin$4;
.super Ljava/lang/Object;
.source "WebViewPlugin.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/kayac/unitywebview/WebViewPlugin;->EvaluateJS(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/kayac/unitywebview/WebViewPlugin;

.field final synthetic val$js:Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/kayac/unitywebview/WebViewPlugin;Ljava/lang/String;)V
    .locals 0

    .prologue
    .line 203
    iput-object p1, p0, Lcom/kayac/unitywebview/WebViewPlugin$4;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    iput-object p2, p0, Lcom/kayac/unitywebview/WebViewPlugin$4;->val$js:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .prologue
    .line 205
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin$4;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lcom/kayac/unitywebview/WebViewPlugin;->access$400(Lcom/kayac/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "javascript:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget-object v2, p0, Lcom/kayac/unitywebview/WebViewPlugin$4;->val$js:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/webkit/WebView;->loadUrl(Ljava/lang/String;)V

    .line 207
    return-void
.end method
