.class Lcom/kayac/unitywebview/WebViewPlugin$6;
.super Ljava/lang/Object;
.source "WebViewPlugin.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/kayac/unitywebview/WebViewPlugin;->SetVisibility(Z)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/kayac/unitywebview/WebViewPlugin;

.field final synthetic val$visibility:Z


# direct methods
.method constructor <init>(Lcom/kayac/unitywebview/WebViewPlugin;Z)V
    .locals 0

    .prologue
    .line 228
    iput-object p1, p0, Lcom/kayac/unitywebview/WebViewPlugin$6;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    iput-boolean p2, p0, Lcom/kayac/unitywebview/WebViewPlugin$6;->val$visibility:Z

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .prologue
    .line 230
    iget-boolean v0, p0, Lcom/kayac/unitywebview/WebViewPlugin$6;->val$visibility:Z

    if-eqz v0, :cond_0

    .line 231
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin$6;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lcom/kayac/unitywebview/WebViewPlugin;->access$400(Lcom/kayac/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/webkit/WebView;->setVisibility(I)V

    .line 232
    invoke-static {}, Lcom/kayac/unitywebview/WebViewPlugin;->access$500()Landroid/widget/FrameLayout;

    move-result-object v0

    invoke-virtual {v0}, Landroid/widget/FrameLayout;->requestFocus()Z

    .line 233
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin$6;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lcom/kayac/unitywebview/WebViewPlugin;->access$400(Lcom/kayac/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    invoke-virtual {v0}, Landroid/webkit/WebView;->requestFocus()Z

    .line 238
    :goto_0
    return-void

    .line 235
    :cond_0
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin$6;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lcom/kayac/unitywebview/WebViewPlugin;->access$400(Lcom/kayac/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/webkit/WebView;->setVisibility(I)V

    goto :goto_0
.end method
