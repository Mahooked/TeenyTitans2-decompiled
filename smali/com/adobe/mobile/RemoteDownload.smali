.class Lcom/adobe/mobile/RemoteDownload;
.super Ljava/lang/Object;
.source "RemoteDownload.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/adobe/mobile/RemoteDownload$1;,
        Lcom/adobe/mobile/RemoteDownload$DownloadFileTask;,
        Lcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;
    }
.end annotation


# static fields
.field private static final DEFAULT_CONNECTION_TIMEOUT:I = 0x2710

.field private static final DEFAULT_READ_TIMEOUT:I = 0x2710


# direct methods
.method constructor <init>()V
    .locals 0

    .prologue
    .line 13
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 320
    return-void
.end method

.method static synthetic access$100()Ljava/text/SimpleDateFormat;
    .locals 1

    .prologue
    .line 13
    invoke-static {}, Lcom/adobe/mobile/RemoteDownload;->createRFC2822Formatter()Ljava/text/SimpleDateFormat;

    move-result-object v0

    return-object v0
.end method

.method static synthetic access$200(Ljava/lang/String;)Ljava/lang/String;
    .locals 1
    .param p0, "x0"    # Ljava/lang/String;

    .prologue
    .line 13
    invoke-static {p0}, Lcom/adobe/mobile/RemoteDownload;->getEtagOfFile(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method static synthetic access$300(Ljava/lang/String;)J
    .locals 2
    .param p0, "x0"    # Ljava/lang/String;

    .prologue
    .line 13
    invoke-static {p0}, Lcom/adobe/mobile/RemoteDownload;->getLastModifiedOfFile(Ljava/lang/String;)J

    move-result-wide v0

    return-wide v0
.end method

.method static synthetic access$400(Ljava/lang/String;Ljava/util/Date;Ljava/lang/String;Ljava/lang/String;)Ljava/io/File;
    .locals 1
    .param p0, "x0"    # Ljava/lang/String;
    .param p1, "x1"    # Ljava/util/Date;
    .param p2, "x2"    # Ljava/lang/String;
    .param p3, "x3"    # Ljava/lang/String;

    .prologue
    .line 13
    invoke-static {p0, p1, p2, p3}, Lcom/adobe/mobile/RemoteDownload;->getNewCachedFile(Ljava/lang/String;Ljava/util/Date;Ljava/lang/String;Ljava/lang/String;)Ljava/io/File;

    move-result-object v0

    return-object v0
.end method

.method private static createRFC2822Formatter()Ljava/text/SimpleDateFormat;
    .locals 4

    .prologue
    .line 283
    const-string v0, "EEE, dd MMM yyyy HH:mm:ss Z"

    .line 284
    .local v0, "pattern":Ljava/lang/String;
    new-instance v1, Ljava/text/SimpleDateFormat;

    const-string v2, "EEE, dd MMM yyyy HH:mm:ss Z"

    sget-object v3, Ljava/util/Locale;->US:Ljava/util/Locale;

    invoke-direct {v1, v2, v3}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    .line 285
    .local v1, "rfc2822formatter":Ljava/text/SimpleDateFormat;
    const-string v2, "GMT"

    invoke-static {v2}, Ljava/util/TimeZone;->getTimeZone(Ljava/lang/String;)Ljava/util/TimeZone;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/text/SimpleDateFormat;->setTimeZone(Ljava/util/TimeZone;)V

    .line 287
    return-object v1
.end method

.method protected static deleteCachedDataForURL(Ljava/lang/String;Ljava/lang/String;)Z
    .locals 4
    .param p0, "url"    # Ljava/lang/String;
    .param p1, "directory"    # Ljava/lang/String;

    .prologue
    const/4 v1, 0x1

    const/4 v2, 0x0

    .line 214
    if-eqz p0, :cond_0

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v3

    if-ge v3, v1, :cond_2

    .line 215
    :cond_0
    const-string v1, "Cached File - tried to delete cached file, but file path was empty"

    new-array v3, v2, [Ljava/lang/Object;

    invoke-static {v1, v3}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    move v1, v2

    .line 221
    :cond_1
    :goto_0
    return v1

    .line 219
    :cond_2
    invoke-static {p0, p1}, Lcom/adobe/mobile/RemoteDownload;->getFileForCachedURL(Ljava/lang/String;Ljava/lang/String;)Ljava/io/File;

    move-result-object v0

    .line 221
    .local v0, "cachedFile":Ljava/io/File;
    if-eqz v0, :cond_3

    invoke-virtual {v0}, Ljava/io/File;->delete()Z

    move-result v3

    if-nez v3, :cond_1

    :cond_3
    move v1, v2

    goto :goto_0
.end method

.method protected static deleteFilesForDirectoryNotInList(Ljava/lang/String;Ljava/util/List;)V
    .locals 12
    .param p0, "directory"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/List",
            "<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .prologue
    .local p1, "urls":Ljava/util/List;, "Ljava/util/List<Ljava/lang/String;>;"
    const/4 v8, 0x0

    .line 113
    if-eqz p1, :cond_0

    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v7

    if-gtz v7, :cond_2

    .line 115
    :cond_0
    invoke-static {p0}, Lcom/adobe/mobile/RemoteDownload;->deleteFilesInDirectory(Ljava/lang/String;)V

    .line 149
    :cond_1
    return-void

    .line 119
    :cond_2
    invoke-static {p0}, Lcom/adobe/mobile/RemoteDownload;->getDownloadCacheDirectory(Ljava/lang/String;)Ljava/io/File;

    move-result-object v0

    .line 120
    .local v0, "cacheDir":Ljava/io/File;
    if-eqz v0, :cond_1

    .line 125
    invoke-virtual {v0}, Ljava/io/File;->listFiles()[Ljava/io/File;

    move-result-object v1

    .line 126
    .local v1, "cachedFiles":[Ljava/io/File;
    if-eqz v1, :cond_1

    array-length v7, v1

    if-lez v7, :cond_1

    .line 132
    new-instance v5, Ljava/util/ArrayList;

    invoke-direct {v5}, Ljava/util/ArrayList;-><init>()V

    .line 133
    .local v5, "hashedUrls":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/String;>;"
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v7

    :goto_0
    invoke-interface {v7}, Ljava/util/Iterator;->hasNext()Z

    move-result v9

    if-eqz v9, :cond_3

    invoke-interface {v7}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/lang/String;

    .line 134
    .local v6, "url":Ljava/lang/String;
    invoke-static {v6}, Lcom/adobe/mobile/RemoteDownload;->md5hash(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v9

    invoke-virtual {v5, v9}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 137
    .end local v6    # "url":Ljava/lang/String;
    :cond_3
    array-length v9, v1

    move v7, v8

    :goto_1
    if-ge v7, v9, :cond_1

    aget-object v2, v1, v7

    .line 138
    .local v2, "file":Ljava/io/File;
    invoke-virtual {v2}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v4

    .line 139
    .local v4, "fileName":Ljava/lang/String;
    const-string v10, "."

    invoke-virtual {v4, v10}, Ljava/lang/String;->indexOf(Ljava/lang/String;)I

    move-result v10

    invoke-virtual {v4, v8, v10}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    .line 140
    .local v3, "fileHash":Ljava/lang/String;
    invoke-virtual {v5, v3}, Ljava/util/ArrayList;->contains(Ljava/lang/Object;)Z

    move-result v10

    if-nez v10, :cond_4

    .line 141
    invoke-virtual {v2}, Ljava/io/File;->delete()Z

    move-result v10

    if-eqz v10, :cond_5

    .line 142
    const-string v10, "Cached File - Removed unused cache file"

    new-array v11, v8, [Ljava/lang/Object;

    invoke-static {v10, v11}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 137
    :cond_4
    :goto_2
    add-int/lit8 v7, v7, 0x1

    goto :goto_1

    .line 145
    :cond_5
    const-string v10, "Cached File - Failed to remove unused cache file"

    new-array v11, v8, [Ljava/lang/Object;

    invoke-static {v10, v11}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_2
.end method

.method protected static deleteFilesInDirectory(Ljava/lang/String;)V
    .locals 8
    .param p0, "directory"    # Ljava/lang/String;

    .prologue
    const/4 v4, 0x0

    .line 152
    invoke-static {p0}, Lcom/adobe/mobile/RemoteDownload;->getDownloadCacheDirectory(Ljava/lang/String;)Ljava/io/File;

    move-result-object v0

    .line 153
    .local v0, "cacheDir":Ljava/io/File;
    if-nez v0, :cond_1

    .line 172
    :cond_0
    return-void

    .line 158
    :cond_1
    invoke-virtual {v0}, Ljava/io/File;->listFiles()[Ljava/io/File;

    move-result-object v1

    .line 159
    .local v1, "cachedFiles":[Ljava/io/File;
    if-eqz v1, :cond_0

    array-length v3, v1

    if-lez v3, :cond_0

    .line 164
    array-length v5, v1

    move v3, v4

    :goto_0
    if-ge v3, v5, :cond_0

    aget-object v2, v1, v3

    .line 165
    .local v2, "file":Ljava/io/File;
    invoke-virtual {v2}, Ljava/io/File;->delete()Z

    move-result v6

    if-eqz v6, :cond_2

    .line 166
    const-string v6, "Cached File - Removed unused cache file"

    new-array v7, v4, [Ljava/lang/Object;

    invoke-static {v6, v7}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 164
    :goto_1
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .line 169
    :cond_2
    const-string v6, "Cached File - Failed to remove unused cache file"

    new-array v7, v4, [Ljava/lang/Object;

    invoke-static {v6, v7}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1
.end method

.method protected static getDownloadCacheDirectory(Ljava/lang/String;)Ljava/io/File;
    .locals 3
    .param p0, "directory"    # Ljava/lang/String;

    .prologue
    .line 204
    new-instance v0, Ljava/io/File;

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getCacheDirectory()Ljava/io/File;

    move-result-object v1

    invoke-direct {v0, v1, p0}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 205
    .local v0, "downloadCacheDirectory":Ljava/io/File;
    invoke-virtual {v0}, Ljava/io/File;->exists()Z

    move-result v1

    if-nez v1, :cond_0

    invoke-virtual {v0}, Ljava/io/File;->mkdir()Z

    move-result v1

    if-nez v1, :cond_0

    .line 206
    const-string v1, "Cached File - unable to open/make download cache directory"

    const/4 v2, 0x0

    new-array v2, v2, [Ljava/lang/Object;

    invoke-static {v1, v2}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 207
    const/4 v0, 0x0

    .line 210
    .end local v0    # "downloadCacheDirectory":Ljava/io/File;
    :cond_0
    return-object v0
.end method

.method private static getEtagOfFile(Ljava/lang/String;)Ljava/lang/String;
    .locals 6
    .param p0, "path"    # Ljava/lang/String;

    .prologue
    const/4 v1, 0x0

    const/4 v5, 0x1

    const/4 v4, 0x0

    .line 242
    if-eqz p0, :cond_0

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v2

    if-ge v2, v5, :cond_1

    .line 243
    :cond_0
    const-string v2, "Cached File - Path was null or empty for Cache File"

    new-array v3, v4, [Ljava/lang/Object;

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 254
    :goto_0
    return-object v1

    .line 247
    :cond_1
    invoke-static {p0}, Lcom/adobe/mobile/RemoteDownload;->getPathExtension(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lcom/adobe/mobile/RemoteDownload;->splitPathExtension(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v0

    .line 249
    .local v0, "splitExtension":[Ljava/lang/String;
    if-eqz v0, :cond_2

    array-length v2, v0

    const/4 v3, 0x2

    if-ge v2, v3, :cond_3

    .line 250
    :cond_2
    const-string v2, "Cached File - No etag for file. Extension had no second value after split."

    new-array v3, v4, [Ljava/lang/Object;

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 254
    :cond_3
    aget-object v1, v0, v5

    goto :goto_0
.end method

.method protected static getFileForCachedURL(Ljava/lang/String;)Ljava/io/File;
    .locals 1
    .param p0, "url"    # Ljava/lang/String;

    .prologue
    .line 80
    const-string v0, "adbdownloadcache"

    invoke-static {p0, v0}, Lcom/adobe/mobile/RemoteDownload;->getFileForCachedURL(Ljava/lang/String;Ljava/lang/String;)Ljava/io/File;

    move-result-object v0

    return-object v0
.end method

.method protected static getFileForCachedURL(Ljava/lang/String;Ljava/lang/String;)Ljava/io/File;
    .locals 10
    .param p0, "url"    # Ljava/lang/String;
    .param p1, "directory"    # Ljava/lang/String;

    .prologue
    const/4 v4, 0x0

    const/4 v9, 0x1

    const/4 v6, 0x0

    .line 85
    if-eqz p0, :cond_0

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v5

    if-ge v5, v9, :cond_1

    .line 109
    :cond_0
    :goto_0
    return-object v4

    .line 89
    :cond_1
    invoke-static {p1}, Lcom/adobe/mobile/RemoteDownload;->getDownloadCacheDirectory(Ljava/lang/String;)Ljava/io/File;

    move-result-object v0

    .line 90
    .local v0, "cacheDirectory":Ljava/io/File;
    if-eqz v0, :cond_0

    .line 94
    invoke-virtual {v0}, Ljava/io/File;->list()[Ljava/lang/String;

    move-result-object v1

    .line 95
    .local v1, "cachedFiles":[Ljava/lang/String;
    if-eqz v1, :cond_2

    array-length v5, v1

    if-ge v5, v9, :cond_3

    .line 96
    :cond_2
    const-string v5, "Cached Files - Directory is empty (%s)."

    new-array v7, v9, [Ljava/lang/Object;

    invoke-virtual {v0}, Ljava/io/File;->getAbsolutePath()Ljava/lang/String;

    move-result-object v8

    aput-object v8, v7, v6

    invoke-static {v5, v7}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 100
    :cond_3
    invoke-static {p0}, Lcom/adobe/mobile/RemoteDownload;->md5hash(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 102
    .local v3, "hashedName":Ljava/lang/String;
    array-length v7, v1

    move v5, v6

    :goto_1
    if-ge v5, v7, :cond_5

    aget-object v2, v1, v5

    .line 103
    .local v2, "fileName":Ljava/lang/String;
    const/16 v8, 0x2e

    invoke-virtual {v2, v8}, Ljava/lang/String;->lastIndexOf(I)I

    move-result v8

    invoke-virtual {v2, v6, v8}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v8, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v8

    if-eqz v8, :cond_4

    .line 104
    new-instance v4, Ljava/io/File;

    invoke-direct {v4, v0, v2}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    goto :goto_0

    .line 102
    :cond_4
    add-int/lit8 v5, v5, 0x1

    goto :goto_1

    .line 108
    .end local v2    # "fileName":Ljava/lang/String;
    :cond_5
    const-string v5, "Cached Files - This file has not previously been cached (%s)."

    new-array v7, v9, [Ljava/lang/Object;

    aput-object p0, v7, v6

    invoke-static {v5, v7}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method private static getLastModifiedOfFile(Ljava/lang/String;)J
    .locals 6
    .param p0, "path"    # Ljava/lang/String;

    .prologue
    const-wide/16 v2, 0x0

    const/4 v5, 0x1

    const/4 v4, 0x0

    .line 226
    if-eqz p0, :cond_0

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v1

    if-ge v1, v5, :cond_1

    .line 227
    :cond_0
    const-string v1, "Cached File - Path was null or empty for Cache File. Could not get Last Modified Date."

    new-array v4, v4, [Ljava/lang/Object;

    invoke-static {v1, v4}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 237
    :goto_0
    return-wide v2

    .line 231
    :cond_1
    invoke-static {p0}, Lcom/adobe/mobile/RemoteDownload;->getPathExtension(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lcom/adobe/mobile/RemoteDownload;->splitPathExtension(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v0

    .line 232
    .local v0, "splitExtension":[Ljava/lang/String;
    if-eqz v0, :cond_2

    array-length v1, v0

    if-ge v1, v5, :cond_3

    .line 233
    :cond_2
    const-string v1, "Cached File - No last modified date for file. Extension had no values after split."

    new-array v4, v4, [Ljava/lang/Object;

    invoke-static {v1, v4}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 237
    :cond_3
    aget-object v1, v0, v4

    invoke-static {v1}, Ljava/lang/Long;->parseLong(Ljava/lang/String;)J

    move-result-wide v2

    goto :goto_0
.end method

.method private static getNewCachedFile(Ljava/lang/String;Ljava/util/Date;Ljava/lang/String;Ljava/lang/String;)Ljava/io/File;
    .locals 6
    .param p0, "url"    # Ljava/lang/String;
    .param p1, "lastModified"    # Ljava/util/Date;
    .param p2, "etag"    # Ljava/lang/String;
    .param p3, "directory"    # Ljava/lang/String;

    .prologue
    const/4 v5, 0x1

    const/4 v4, 0x0

    const/4 v2, 0x0

    .line 175
    if-eqz p0, :cond_0

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v3

    if-ge v3, v5, :cond_2

    .line 176
    :cond_0
    const-string v3, "Cached File - Invalid url parameter while attempting to create cache file. Could not save data."

    new-array v4, v4, [Ljava/lang/Object;

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 200
    :cond_1
    :goto_0
    return-object v2

    .line 180
    :cond_2
    if-nez p1, :cond_3

    .line 181
    const-string v3, "Cached File - Invalid lastModified parameter while attempting to create cache file. Could not save data."

    new-array v4, v4, [Ljava/lang/Object;

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 185
    :cond_3
    if-eqz p2, :cond_4

    invoke-virtual {p2}, Ljava/lang/String;->length()I

    move-result v3

    if-ge v3, v5, :cond_5

    .line 186
    :cond_4
    const-string v3, "Cached File - Invalid etag parameter while attempting to create cache file. Could not save data."

    new-array v4, v4, [Ljava/lang/Object;

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 191
    :cond_5
    invoke-static {p3}, Lcom/adobe/mobile/RemoteDownload;->getDownloadCacheDirectory(Ljava/lang/String;)Ljava/io/File;

    move-result-object v0

    .line 192
    .local v0, "cacheDirectory":Ljava/io/File;
    if-eqz v0, :cond_1

    .line 196
    invoke-static {p0}, Lcom/adobe/mobile/RemoteDownload;->md5hash(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    .line 197
    .local v1, "md5Hash":Ljava/lang/String;
    if-eqz v1, :cond_1

    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v3

    if-lt v3, v5, :cond_1

    .line 200
    new-instance v2, Ljava/io/File;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v0}, Ljava/io/File;->getPath()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    sget-object v4, Ljava/io/File;->separator:Ljava/lang/String;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-static {p0}, Lcom/adobe/mobile/RemoteDownload;->md5hash(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    const-string v4, "."

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {p1}, Ljava/util/Date;->getTime()J

    move-result-wide v4

    invoke-virtual {v3, v4, v5}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    move-result-object v3

    const-string v4, "_"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v2, v3}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    goto :goto_0
.end method

.method private static getPathExtension(Ljava/lang/String;)Ljava/lang/String;
    .locals 2
    .param p0, "path"    # Ljava/lang/String;

    .prologue
    .line 259
    if-eqz p0, :cond_0

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v0

    const/4 v1, 0x1

    if-ge v0, v1, :cond_1

    .line 260
    :cond_0
    const-string v0, "Cached File - Path was null or empty for Cache File"

    const/4 v1, 0x0

    new-array v1, v1, [Ljava/lang/Object;

    invoke-static {v0, v1}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 261
    const/4 v0, 0x0

    .line 264
    :goto_0
    return-object v0

    :cond_1
    const-string v0, "."

    invoke-virtual {p0, v0}, Ljava/lang/String;->lastIndexOf(Ljava/lang/String;)I

    move-result v0

    add-int/lit8 v0, v0, 0x1

    invoke-virtual {p0, v0}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v0

    goto :goto_0
.end method

.method private static md5hash(Ljava/lang/String;)Ljava/lang/String;
    .locals 13
    .param p0, "input"    # Ljava/lang/String;

    .prologue
    const/4 v6, 0x0

    const/4 v12, 0x1

    const/4 v8, 0x0

    .line 293
    if-eqz p0, :cond_0

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v7

    if-ge v7, v12, :cond_1

    .line 317
    :cond_0
    :goto_0
    return-object v6

    .line 298
    :cond_1
    :try_start_0
    const-string v7, "MD5"

    invoke-static {v7}, Ljava/security/MessageDigest;->getInstance(Ljava/lang/String;)Ljava/security/MessageDigest;

    move-result-object v5

    .line 299
    .local v5, "messagedigest":Ljava/security/MessageDigest;
    const-string v7, "UTF-8"

    invoke-virtual {p0, v7}, Ljava/lang/String;->getBytes(Ljava/lang/String;)[B

    move-result-object v7

    invoke-virtual {v5, v7}, Ljava/security/MessageDigest;->update([B)V

    .line 300
    invoke-virtual {v5}, Ljava/security/MessageDigest;->digest()[B

    move-result-object v4

    .line 302
    .local v4, "messageDigest":[B
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    .line 303
    .local v3, "md5HexBuilder":Ljava/lang/StringBuilder;
    array-length v9, v4

    move v7, v8

    :goto_1
    if-ge v7, v9, :cond_3

    aget-byte v0, v4, v7

    .line 304
    .local v0, "aMessageDigest":B
    and-int/lit16 v10, v0, 0xff

    invoke-static {v10}, Ljava/lang/Integer;->toHexString(I)Ljava/lang/String;

    move-result-object v2

    .line 305
    .local v2, "hexString":Ljava/lang/String;
    :goto_2
    invoke-virtual {v2}, Ljava/lang/String;->length()I

    move-result v10

    const/4 v11, 0x2

    if-ge v10, v11, :cond_2

    .line 306
    new-instance v10, Ljava/lang/StringBuilder;

    invoke-direct {v10}, Ljava/lang/StringBuilder;-><init>()V

    const-string v11, "0"

    invoke-virtual {v10, v11}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v10

    invoke-virtual {v10, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v10

    invoke-virtual {v10}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    goto :goto_2

    .line 308
    :cond_2
    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 303
    add-int/lit8 v7, v7, 0x1

    goto :goto_1

    .line 310
    .end local v0    # "aMessageDigest":B
    .end local v2    # "hexString":Ljava/lang/String;
    :cond_3
    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;
    :try_end_0
    .catch Ljava/security/NoSuchAlgorithmException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_1

    move-result-object v6

    goto :goto_0

    .line 312
    .end local v3    # "md5HexBuilder":Ljava/lang/StringBuilder;
    .end local v4    # "messageDigest":[B
    .end local v5    # "messagedigest":Ljava/security/MessageDigest;
    :catch_0
    move-exception v1

    .line 313
    .local v1, "e":Ljava/security/NoSuchAlgorithmException;
    const-string v7, "Cached Files - unable to get md5 hash (%s)"

    new-array v9, v12, [Ljava/lang/Object;

    invoke-virtual {v1}, Ljava/security/NoSuchAlgorithmException;->getMessage()Ljava/lang/String;

    move-result-object v10

    aput-object v10, v9, v8

    invoke-static {v7, v9}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 314
    .end local v1    # "e":Ljava/security/NoSuchAlgorithmException;
    :catch_1
    move-exception v1

    .line 315
    .local v1, "e":Ljava/io/UnsupportedEncodingException;
    const-string v7, "Cached Files - Unsupported Encoding: UTF-8 (%s)"

    new-array v9, v12, [Ljava/lang/Object;

    invoke-virtual {v1}, Ljava/io/UnsupportedEncodingException;->getMessage()Ljava/lang/String;

    move-result-object v10

    aput-object v10, v9, v8

    invoke-static {v7, v9}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method protected static remoteDownloadAsync(Ljava/lang/String;IILcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;)V
    .locals 1
    .param p0, "url"    # Ljava/lang/String;
    .param p1, "connectionTimeout"    # I
    .param p2, "readTimeout"    # I
    .param p3, "block"    # Lcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;

    .prologue
    .line 45
    const-string v0, "adbdownloadcache"

    invoke-static {p0, p1, p2, p3, v0}, Lcom/adobe/mobile/RemoteDownload;->remoteDownloadAsync(Ljava/lang/String;IILcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;Ljava/lang/String;)V

    .line 46
    return-void
.end method

.method protected static remoteDownloadAsync(Ljava/lang/String;IILcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;Ljava/lang/String;)V
    .locals 8
    .param p0, "url"    # Ljava/lang/String;
    .param p1, "connectionTimeout"    # I
    .param p2, "readTimeout"    # I
    .param p3, "block"    # Lcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;
    .param p4, "directory"    # Ljava/lang/String;

    .prologue
    .line 39
    new-instance v7, Ljava/lang/Thread;

    new-instance v0, Lcom/adobe/mobile/RemoteDownload$DownloadFileTask;

    const/4 v6, 0x0

    move-object v1, p0

    move-object v2, p3

    move v3, p1

    move v4, p2

    move-object v5, p4

    invoke-direct/range {v0 .. v6}, Lcom/adobe/mobile/RemoteDownload$DownloadFileTask;-><init>(Ljava/lang/String;Lcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;IILjava/lang/String;Lcom/adobe/mobile/RemoteDownload$1;)V

    invoke-direct {v7, v0}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    .line 40
    .local v7, "thread":Ljava/lang/Thread;
    invoke-virtual {v7}, Ljava/lang/Thread;->start()V

    .line 41
    return-void
.end method

.method protected static remoteDownloadAsync(Ljava/lang/String;Lcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;)V
    .locals 2
    .param p0, "url"    # Ljava/lang/String;
    .param p1, "block"    # Lcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;

    .prologue
    const/16 v1, 0x2710

    .line 50
    const-string v0, "adbdownloadcache"

    invoke-static {p0, v1, v1, p1, v0}, Lcom/adobe/mobile/RemoteDownload;->remoteDownloadAsync(Ljava/lang/String;IILcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;Ljava/lang/String;)V

    .line 51
    return-void
.end method

.method protected static remoteDownloadAsync(Ljava/lang/String;Ljava/lang/String;Lcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;)V
    .locals 1
    .param p0, "url"    # Ljava/lang/String;
    .param p1, "directory"    # Ljava/lang/String;
    .param p2, "block"    # Lcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;

    .prologue
    const/16 v0, 0x2710

    .line 55
    invoke-static {p0, v0, v0, p2, p1}, Lcom/adobe/mobile/RemoteDownload;->remoteDownloadAsync(Ljava/lang/String;IILcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;Ljava/lang/String;)V

    .line 56
    return-void
.end method

.method protected static remoteDownloadSync(Ljava/lang/String;IILcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;)V
    .locals 1
    .param p0, "url"    # Ljava/lang/String;
    .param p1, "connectionTimeout"    # I
    .param p2, "readTimeout"    # I
    .param p3, "block"    # Lcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;

    .prologue
    .line 66
    const-string v0, "adbdownloadcache"

    invoke-static {p0, p1, p2, p3, v0}, Lcom/adobe/mobile/RemoteDownload;->remoteDownloadSync(Ljava/lang/String;IILcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;Ljava/lang/String;)V

    .line 67
    return-void
.end method

.method protected static remoteDownloadSync(Ljava/lang/String;IILcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;Ljava/lang/String;)V
    .locals 7
    .param p0, "url"    # Ljava/lang/String;
    .param p1, "connectionTimeout"    # I
    .param p2, "readTimeout"    # I
    .param p3, "block"    # Lcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;
    .param p4, "directory"    # Ljava/lang/String;

    .prologue
    .line 60
    new-instance v0, Lcom/adobe/mobile/RemoteDownload$DownloadFileTask;

    const/4 v6, 0x0

    move-object v1, p0

    move-object v2, p3

    move v3, p1

    move v4, p2

    move-object v5, p4

    invoke-direct/range {v0 .. v6}, Lcom/adobe/mobile/RemoteDownload$DownloadFileTask;-><init>(Ljava/lang/String;Lcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;IILjava/lang/String;Lcom/adobe/mobile/RemoteDownload$1;)V

    .line 61
    .local v0, "r":Ljava/lang/Runnable;
    invoke-interface {v0}, Ljava/lang/Runnable;->run()V

    .line 62
    return-void
.end method

.method protected static remoteDownloadSync(Ljava/lang/String;Lcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;)V
    .locals 2
    .param p0, "url"    # Ljava/lang/String;
    .param p1, "block"    # Lcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;

    .prologue
    const/16 v1, 0x2710

    .line 71
    const-string v0, "adbdownloadcache"

    invoke-static {p0, v1, v1, p1, v0}, Lcom/adobe/mobile/RemoteDownload;->remoteDownloadSync(Ljava/lang/String;IILcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;Ljava/lang/String;)V

    .line 72
    return-void
.end method

.method protected static remoteDownloadSync(Ljava/lang/String;Ljava/lang/String;Lcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;)V
    .locals 1
    .param p0, "url"    # Ljava/lang/String;
    .param p1, "directory"    # Ljava/lang/String;
    .param p2, "block"    # Lcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;

    .prologue
    const/16 v0, 0x2710

    .line 76
    invoke-static {p0, v0, v0, p2, p1}, Lcom/adobe/mobile/RemoteDownload;->remoteDownloadSync(Ljava/lang/String;IILcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;Ljava/lang/String;)V

    .line 77
    return-void
.end method

.method private static splitPathExtension(Ljava/lang/String;)[Ljava/lang/String;
    .locals 6
    .param p0, "extension"    # Ljava/lang/String;

    .prologue
    const/4 v1, 0x0

    const/4 v5, 0x1

    const/4 v4, 0x0

    .line 269
    if-eqz p0, :cond_0

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v2

    if-ge v2, v5, :cond_2

    .line 270
    :cond_0
    const-string v2, "Cached File - Extension was null or empty on Cache File"

    new-array v3, v4, [Ljava/lang/Object;

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    move-object v0, v1

    .line 279
    :cond_1
    :goto_0
    return-object v0

    .line 274
    :cond_2
    const-string v2, "_"

    invoke-virtual {p0, v2}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v0

    .line 275
    .local v0, "separated":[Ljava/lang/String;
    array-length v2, v0

    const/4 v3, 0x2

    if-eq v2, v3, :cond_1

    .line 276
    const-string v2, "Cached File - Invalid Extension on Cache File (%s)"

    new-array v3, v5, [Ljava/lang/Object;

    aput-object p0, v3, v4

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    move-object v0, v1

    .line 277
    goto :goto_0
.end method

.method protected static stringIsUrl(Ljava/lang/String;)Z
    .locals 3
    .param p0, "stringUrl"    # Ljava/lang/String;

    .prologue
    const/4 v1, 0x0

    .line 24
    if-eqz p0, :cond_0

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v2

    if-gtz v2, :cond_1

    .line 33
    :cond_0
    :goto_0
    return v1

    .line 29
    :cond_1
    :try_start_0
    new-instance v2, Ljava/net/URL;

    invoke-direct {v2, p0}, Ljava/net/URL;-><init>(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/net/MalformedURLException; {:try_start_0 .. :try_end_0} :catch_0

    .line 30
    const/4 v1, 0x1

    goto :goto_0

    .line 32
    :catch_0
    move-exception v0

    .line 33
    .local v0, "ex":Ljava/net/MalformedURLException;
    goto :goto_0
.end method
