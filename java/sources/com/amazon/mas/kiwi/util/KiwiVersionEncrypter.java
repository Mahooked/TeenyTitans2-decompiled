package com.amazon.mas.kiwi.util;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
public class KiwiVersionEncrypter {
    private static final String SECRET_KEY = "Kiwi__Version__Obfuscator";

    private KiwiVersionEncrypter() {
    }

    public static String encrypt(String text) {
        checkInput(text, "text");
        return Base64.encodeBytes(performXOR(text.getBytes()));
    }

    public static void encryptToFile(String text, String file) throws Throwable {
        checkInput(text, "text");
        checkInput(file, "file");
        String encrypted = Base64.encodeBytes(performXOR(text.getBytes()));
        BufferedWriter bw = null;
        try {
            BufferedWriter bw2 = new BufferedWriter(new FileWriter(file));
            try {
                bw2.write(encrypted);
                bw2.flush();
                if (bw2 != null) {
                    try {
                        bw2.close();
                    } catch (IOException e) {
                    }
                }
            } catch (Throwable th) {
                th = th;
                bw = bw2;
                if (bw != null) {
                    try {
                        bw.close();
                    } catch (IOException e2) {
                    }
                }
                throw th;
            }
        } catch (Throwable th2) {
            th = th2;
        }
    }

    public static String decrypt(String text) throws IOException {
        checkInput(text, "text");
        return new String(performXOR(Base64.decode(text.getBytes())), HTTP.UTF_8);
    }

    public static String decryptFromFile(String file) throws Throwable {
        checkInput(file, "file");
        BufferedReader br = null;
        try {
            BufferedReader br2 = new BufferedReader(new FileReader(file));
            try {
                String encrypted = br2.readLine();
                if (br2 != null) {
                    try {
                        br2.close();
                    } catch (IOException e) {
                    }
                }
                return new String(performXOR(Base64.decode(encrypted.getBytes())), HTTP.UTF_8);
            } catch (Throwable th) {
                th = th;
                br = br2;
                if (br != null) {
                    try {
                        br.close();
                    } catch (IOException e2) {
                    }
                }
                throw th;
            }
        } catch (Throwable th2) {
            th = th2;
        }
    }

    private static void checkInput(String input, String inputName) {
        if (input == null || input.isEmpty()) {
            throw new IllegalArgumentException("input '" + inputName + "' cannot be null or empty");
        }
    }

    private static byte[] performXOR(byte[] input) {
        byte[] output = new byte[input.length];
        byte[] secret = SECRET_KEY.getBytes();
        int spos = 0;
        for (int pos = 0; pos < input.length; pos++) {
            output[pos] = (byte) (input[pos] ^ secret[spos]);
            spos++;
            if (spos >= secret.length) {
                spos = 0;
            }
        }
        return output;
    }

    public static void main(String[] args) throws Exception {
        if (args == null || args.length == 0) {
            System.out.println("Usage: com.amazon.mas.kiwi.util.KiwiVersionEncrypter <textToBeEncrypted> [<encryptToFileName>]");
        } else if (args.length > 1) {
            encryptToFile(args[0], args[1]);
        } else {
            System.out.println(encrypt(args[0]));
        }
    }
}
