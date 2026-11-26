package com.comscore.utils;

import android.content.Context;
import com.comscore.analytics.Core;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.LinkedList;

/* JADX INFO: loaded from: classes.dex */
public class FileUtils {
    public static boolean deleteFile(Core core, String str) {
        Context appContext = core.getAppContext();
        Storage storage = core.getStorage();
        boolean zDeleteFile = appContext.deleteFile(str);
        if (zDeleteFile) {
            CSLog.d((Class<? extends Object>) FileUtils.class, "File" + str + " was removed");
            storage.remove(str);
        }
        return zDeleteFile;
    }

    public static ArrayList<String> getFileList(Context context) {
        String[] list = null;
        File filesDir = context.getFilesDir();
        if (filesDir != null && filesDir.isDirectory()) {
            list = filesDir.list(new b());
            if (list != null) {
                Arrays.sort(list);
            } else {
                list = new String[0];
            }
        }
        return new ArrayList<>(Arrays.asList(list));
    }

    /* JADX WARN: Code duplicated, block: B:35:0x0044 A[EXC_TOP_SPLITTER, SYNTHETIC] */
    public static String[] readCachedEvents(Context context, String str) {
        BufferedReader bufferedReader;
        LinkedList linkedList = new LinkedList();
        BufferedReader bufferedReader2 = null;
        try {
            bufferedReader = new BufferedReader(new InputStreamReader(context.openFileInput(str)));
            while (true) {
                try {
                    try {
                        String line = bufferedReader.readLine();
                        if (line == null) {
                            break;
                        }
                        linkedList.add(line);
                    } catch (Exception e) {
                        e = e;
                        CSLog.printStackTrace(e);
                        if (bufferedReader != null) {
                            try {
                                bufferedReader.close();
                            } catch (IOException e2) {
                                CSLog.printStackTrace(e2);
                            }
                        }
                    }
                } catch (Throwable th) {
                    th = th;
                    bufferedReader2 = bufferedReader;
                    if (bufferedReader2 != null) {
                        try {
                            bufferedReader2.close();
                        } catch (IOException e3) {
                            CSLog.printStackTrace(e3);
                        }
                    }
                    throw th;
                }
            }
            if (bufferedReader != null) {
                try {
                    bufferedReader.close();
                } catch (IOException e4) {
                    CSLog.printStackTrace(e4);
                }
            }
        } catch (Exception e5) {
            e = e5;
            bufferedReader = null;
        } catch (Throwable th2) {
            th = th2;
            if (bufferedReader2 != null) {
                bufferedReader2.close();
            }
            throw th;
        }
        String[] strArr = new String[linkedList.size()];
        linkedList.toArray(strArr);
        return strArr;
    }

    public static void writeEvent(Core core, String str, int i, String str2) {
        Context appContext = core.getAppContext();
        Storage storage = core.getStorage();
        FileOutputStream fileOutputStreamOpenFileOutput = null;
        int integer = Utils.getInteger(storage.get(str), 0);
        try {
            fileOutputStreamOpenFileOutput = appContext.openFileOutput(str, i);
            fileOutputStreamOpenFileOutput.write(str2.getBytes());
            storage.set(str, String.valueOf(integer + 1));
        } catch (Exception e) {
            CSLog.e((Class<? extends Object>) FileUtils.class, "Exception in writeEvent:" + e.getLocalizedMessage());
            CSLog.printStackTrace(e);
        } finally {
            if (fileOutputStreamOpenFileOutput != null) {
                try {
                    fileOutputStreamOpenFileOutput.close();
                } catch (IOException e2) {
                    CSLog.printStackTrace(e2);
                }
            }
        }
    }
}
