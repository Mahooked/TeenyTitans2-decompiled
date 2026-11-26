.class public abstract Lcom/amazon/android/k/c;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/amazon/android/k/a;


# instance fields
.field private workflow:Lcom/amazon/android/k/b;


# direct methods
.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method protected final isWorkflowChild()Z
    .locals 1

    iget-object v0, p0, Lcom/amazon/android/k/c;->workflow:Lcom/amazon/android/k/b;

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    :goto_0
    return v0

    :cond_0
    const/4 v0, 0x0

    goto :goto_0
.end method

.method protected final quitParentWorkflow()V
    .locals 2

    invoke-virtual {p0}, Lcom/amazon/android/k/c;->isWorkflowChild()Z

    move-result v0

    const-string v1, "task is no a workflow child"

    invoke-static {v0, v1}, Lcom/amazon/android/d/a;->a(ZLjava/lang/String;)V

    iget-object v0, p0, Lcom/amazon/android/k/c;->workflow:Lcom/amazon/android/k/b;

    invoke-virtual {v0}, Lcom/amazon/android/k/b;->c()V

    return-void
.end method

.method public final setWorkflow(Lcom/amazon/android/k/b;)V
    .locals 2

    const-string v0, "workflow"

    invoke-static {p1, v0}, Lcom/amazon/android/d/a;->a(Ljava/lang/Object;Ljava/lang/String;)V

    iget-object v0, p0, Lcom/amazon/android/k/c;->workflow:Lcom/amazon/android/k/b;

    const-string v1, "workflow instance can only be set once"

    if-nez v0, :cond_0

    const/4 v0, 0x1

    :goto_0
    invoke-static {v0, v1}, Lcom/amazon/android/d/a;->a(ZLjava/lang/String;)V

    iput-object p1, p0, Lcom/amazon/android/k/c;->workflow:Lcom/amazon/android/k/b;

    return-void

    :cond_0
    const/4 v0, 0x0

    goto :goto_0
.end method
