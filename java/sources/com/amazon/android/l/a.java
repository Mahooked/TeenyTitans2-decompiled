package com.amazon.android.l;

import com.amazon.android.framework.util.KiwiLogger;
import com.amazon.mas.kiwi.util.Base64;
import java.io.IOException;
import java.security.GeneralSecurityException;
import java.security.PublicKey;
import java.security.Signature;

/* JADX INFO: loaded from: classes.dex */
public final class a {
    private static final KiwiLogger a = new KiwiLogger("SignatureVerifier");

    public static boolean a(String str, String str2, PublicKey publicKey) {
        if (KiwiLogger.TRACE_ON) {
            a.trace("Verifying signature of data: '" + str + "', signature: '" + str2 + "', with key: '" + publicKey.toString());
        }
        try {
            byte[] bArrDecode = Base64.decode(str2.getBytes());
            Signature signature = Signature.getInstance("SHA1withRSA");
            signature.initVerify(publicKey);
            signature.update(str.getBytes());
            return signature.verify(bArrDecode);
        } catch (IOException e) {
            return false;
        } catch (GeneralSecurityException e2) {
            return false;
        }
    }
}
