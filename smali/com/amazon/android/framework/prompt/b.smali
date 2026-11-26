.class public final enum Lcom/amazon/android/framework/prompt/b;
.super Ljava/lang/Enum;


# static fields
.field public static final enum a:Lcom/amazon/android/framework/prompt/b;

.field public static final enum b:Lcom/amazon/android/framework/prompt/b;

.field private static final synthetic c:[Lcom/amazon/android/framework/prompt/b;


# direct methods
.method static constructor <clinit>()V
    .locals 4

    const/4 v3, 0x1

    const/4 v2, 0x0

    new-instance v0, Lcom/amazon/android/framework/prompt/b;

    const-string v1, "LEGACY"

    invoke-direct {v0, v1, v2}, Lcom/amazon/android/framework/prompt/b;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/amazon/android/framework/prompt/b;->a:Lcom/amazon/android/framework/prompt/b;

    new-instance v0, Lcom/amazon/android/framework/prompt/b;

    const-string v1, "EXTENDED"

    invoke-direct {v0, v1, v3}, Lcom/amazon/android/framework/prompt/b;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/amazon/android/framework/prompt/b;->b:Lcom/amazon/android/framework/prompt/b;

    const/4 v0, 0x2

    new-array v0, v0, [Lcom/amazon/android/framework/prompt/b;

    sget-object v1, Lcom/amazon/android/framework/prompt/b;->a:Lcom/amazon/android/framework/prompt/b;

    aput-object v1, v0, v2

    sget-object v1, Lcom/amazon/android/framework/prompt/b;->b:Lcom/amazon/android/framework/prompt/b;

    aput-object v1, v0, v3

    sput-object v0, Lcom/amazon/android/framework/prompt/b;->c:[Lcom/amazon/android/framework/prompt/b;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0

    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/amazon/android/framework/prompt/b;
    .locals 1

    const-class v0, Lcom/amazon/android/framework/prompt/b;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/amazon/android/framework/prompt/b;

    return-object p0
.end method

.method public static values()[Lcom/amazon/android/framework/prompt/b;
    .locals 1

    sget-object v0, Lcom/amazon/android/framework/prompt/b;->c:[Lcom/amazon/android/framework/prompt/b;

    invoke-virtual {v0}, [Lcom/amazon/android/framework/prompt/b;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/amazon/android/framework/prompt/b;

    return-object v0
.end method
