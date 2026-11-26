package com.amazon.android.t;

import com.amazon.android.framework.util.KiwiLogger;
import com.amazon.mas.kiwi.util.Base64;
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.io.Serializable;

/* JADX INFO: loaded from: classes.dex */
public final class a {
    private static final KiwiLogger a = new KiwiLogger("Serializer");

    public static Object a(String str) throws Throwable {
        ObjectInputStream objectInputStream;
        if (str == null || str.length() == 0) {
            return null;
        }
        try {
            try {
                objectInputStream = new ObjectInputStream(new ByteArrayInputStream(Base64.decode(str.getBytes())));
                try {
                    try {
                        Object object = objectInputStream.readObject();
                        com.amazon.android.framework.util.a.a(objectInputStream);
                        return object;
                    } catch (Exception e) {
                        e = e;
                        if (KiwiLogger.ERROR_ON) {
                            a.error("Could not read object from string: " + str, e);
                        }
                        com.amazon.android.framework.util.a.a(objectInputStream);
                        return null;
                    }
                } catch (Throwable th) {
                    th = th;
                    com.amazon.android.framework.util.a.a(objectInputStream);
                    throw th;
                }
            } catch (Exception e2) {
                e = e2;
                objectInputStream = null;
            } catch (Throwable th2) {
                th = th2;
                objectInputStream = null;
                com.amazon.android.framework.util.a.a(objectInputStream);
                throw th;
            }
        } catch (IOException e3) {
            if (KiwiLogger.ERROR_ON) {
                a.error("Could not decode string", e3);
            }
            return null;
        }
    }

    public static String a(Serializable serializable) throws Throwable {
        ObjectOutputStream objectOutputStream;
        if (serializable == null) {
            return null;
        }
        ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
        try {
            objectOutputStream = new ObjectOutputStream(byteArrayOutputStream);
            try {
                try {
                    objectOutputStream.writeObject(serializable);
                    String strEncodeBytes = Base64.encodeBytes(byteArrayOutputStream.toByteArray());
                    com.amazon.android.framework.util.a.a(objectOutputStream);
                    return strEncodeBytes;
                } catch (IOException e) {
                    e = e;
                    if (KiwiLogger.ERROR_ON) {
                        a.error("Could not serialize object: " + serializable, e);
                    }
                    com.amazon.android.framework.util.a.a(objectOutputStream);
                    return null;
                }
            } catch (Throwable th) {
                th = th;
                com.amazon.android.framework.util.a.a(objectOutputStream);
                throw th;
            }
        } catch (IOException e2) {
            e = e2;
            objectOutputStream = null;
        } catch (Throwable th2) {
            th = th2;
            objectOutputStream = null;
            com.amazon.android.framework.util.a.a(objectOutputStream);
            throw th;
        }
    }
}
