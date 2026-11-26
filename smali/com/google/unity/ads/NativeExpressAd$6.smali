.class Lcom/google/unity/ads/NativeExpressAd$6;
.super Ljava/lang/Object;
.source "NativeExpressAd.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/google/unity/ads/NativeExpressAd;->show()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/google/unity/ads/NativeExpressAd;


# direct methods
.method constructor <init>(Lcom/google/unity/ads/NativeExpressAd;)V
    .locals 0
    .param p1, "this$0"    # Lcom/google/unity/ads/NativeExpressAd;

    .prologue
    .line 245
    iput-object p1, p0, Lcom/google/unity/ads/NativeExpressAd$6;->this$0:Lcom/google/unity/ads/NativeExpressAd;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .prologue
    const/4 v2, 0x0

    .line 248
    const-string v0, "AdsUnity"

    const-string v1, "Calling show() on NativeExpressAdView"

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 249
    iget-object v0, p0, Lcom/google/unity/ads/NativeExpressAd$6;->this$0:Lcom/google/unity/ads/NativeExpressAd;

    invoke-static {v0, v2}, Lcom/google/unity/ads/NativeExpressAd;->access$302(Lcom/google/unity/ads/NativeExpressAd;Z)Z

    .line 250
    iget-object v0, p0, Lcom/google/unity/ads/NativeExpressAd$6;->this$0:Lcom/google/unity/ads/NativeExpressAd;

    invoke-static {v0}, Lcom/google/unity/ads/NativeExpressAd;->access$700(Lcom/google/unity/ads/NativeExpressAd;)Lcom/google/android/gms/ads/NativeExpressAdView;

    move-result-object v0

    invoke-virtual {v0, v2}, Lcom/google/android/gms/ads/NativeExpressAdView;->setVisibility(I)V

    .line 251
    iget-object v0, p0, Lcom/google/unity/ads/NativeExpressAd$6;->this$0:Lcom/google/unity/ads/NativeExpressAd;

    invoke-static {v0}, Lcom/google/unity/ads/NativeExpressAd;->access$500(Lcom/google/unity/ads/NativeExpressAd;)Landroid/widget/PopupWindow;

    move-result-object v0

    invoke-virtual {v0}, Landroid/widget/PopupWindow;->isShowing()Z

    move-result v0

    if-nez v0, :cond_0

    .line 252
    iget-object v0, p0, Lcom/google/unity/ads/NativeExpressAd$6;->this$0:Lcom/google/unity/ads/NativeExpressAd;

    invoke-static {v0}, Lcom/google/unity/ads/NativeExpressAd;->access$600(Lcom/google/unity/ads/NativeExpressAd;)V

    .line 254
    :cond_0
    iget-object v0, p0, Lcom/google/unity/ads/NativeExpressAd$6;->this$0:Lcom/google/unity/ads/NativeExpressAd;

    invoke-static {v0}, Lcom/google/unity/ads/NativeExpressAd;->access$700(Lcom/google/unity/ads/NativeExpressAd;)Lcom/google/android/gms/ads/NativeExpressAdView;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/gms/ads/NativeExpressAdView;->resume()V

    .line 255
    return-void
.end method
