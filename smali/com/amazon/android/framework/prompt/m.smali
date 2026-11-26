.class final Lcom/amazon/android/framework/prompt/m;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# instance fields
.field private synthetic a:Lcom/amazon/android/framework/prompt/SimplePrompt;


# direct methods
.method constructor <init>(Lcom/amazon/android/framework/prompt/SimplePrompt;)V
    .locals 0

    iput-object p1, p0, Lcom/amazon/android/framework/prompt/m;->a:Lcom/amazon/android/framework/prompt/SimplePrompt;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onClick(Landroid/content/DialogInterface;I)V
    .locals 2

    iget-object v0, p0, Lcom/amazon/android/framework/prompt/m;->a:Lcom/amazon/android/framework/prompt/SimplePrompt;

    invoke-virtual {v0}, Lcom/amazon/android/framework/prompt/SimplePrompt;->dismiss()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/amazon/android/framework/prompt/m;->a:Lcom/amazon/android/framework/prompt/SimplePrompt;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/prompt/SimplePrompt;->doAction(I)V

    :cond_0
    return-void
.end method
