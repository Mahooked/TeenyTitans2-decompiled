.class Lcom/unity/purchasing/amazon/AmazonPurchasing$LocaleNotFoundException;
.super Ljava/lang/Exception;
.source "AmazonPurchasing.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/unity/purchasing/amazon/AmazonPurchasing;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "LocaleNotFoundException"
.end annotation


# direct methods
.method private constructor <init>()V
    .locals 0

    .prologue
    .line 282
    invoke-direct {p0}, Ljava/lang/Exception;-><init>()V

    return-void
.end method

.method synthetic constructor <init>(Lcom/unity/purchasing/amazon/AmazonPurchasing$1;)V
    .locals 0
    .param p1, "x0"    # Lcom/unity/purchasing/amazon/AmazonPurchasing$1;

    .prologue
    .line 282
    invoke-direct {p0}, Lcom/unity/purchasing/amazon/AmazonPurchasing$LocaleNotFoundException;-><init>()V

    return-void
.end method
