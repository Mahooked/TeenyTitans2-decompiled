.class final Lcom/amazon/android/e/d;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/amazon/android/framework/task/Task;


# instance fields
.field private synthetic a:Lcom/amazon/android/e/a;

.field private synthetic b:Lcom/amazon/android/e/c;


# direct methods
.method constructor <init>(Lcom/amazon/android/e/c;Lcom/amazon/android/e/a;)V
    .locals 0

    iput-object p1, p0, Lcom/amazon/android/e/d;->b:Lcom/amazon/android/e/c;

    iput-object p2, p0, Lcom/amazon/android/e/d;->a:Lcom/amazon/android/e/a;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final execute()V
    .locals 2

    iget-object v0, p0, Lcom/amazon/android/e/d;->b:Lcom/amazon/android/e/c;

    invoke-static {v0}, Lcom/amazon/android/e/c;->b(Lcom/amazon/android/e/c;)Lcom/amazon/android/framework/context/ContextManager;

    move-result-object v0

    invoke-interface {v0}, Lcom/amazon/android/framework/context/ContextManager;->getVisible()Landroid/app/Activity;

    move-result-object v0

    if-nez v0, :cond_0

    invoke-static {}, Lcom/amazon/android/e/c;->a()Lcom/amazon/android/framework/util/KiwiLogger;

    move-result-object v0

    const-string v1, "No activity to call startActivityForResult on. startActivityForResult when an activity becomes visible"

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    :goto_0
    return-void

    :cond_0
    iget-object v1, p0, Lcom/amazon/android/e/d;->a:Lcom/amazon/android/e/a;

    invoke-virtual {v1, v0}, Lcom/amazon/android/e/a;->a(Landroid/app/Activity;)V

    goto :goto_0
.end method
