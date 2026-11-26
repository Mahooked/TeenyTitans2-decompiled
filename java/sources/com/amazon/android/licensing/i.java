package com.amazon.android.licensing;

import com.amazon.android.framework.prompt.PromptContent;

/* JADX INFO: loaded from: classes.dex */
public final class i {
    public static final PromptContent a = new PromptContent(new String[]{"Amazon Appstore required"}, "Amazon Appstore is not installed on your device. Please install Amazon Appstore and sign in to Amazon to use this app.", new String[]{"Quit", "Help"}, new com.amazon.android.framework.prompt.a[]{com.amazon.android.framework.prompt.a.DEFAULT, com.amazon.android.framework.prompt.a.HELP}, true, true, 1, 1);
    public static final PromptContent b = new PromptContent("Amazon Appstore connection failure", "An error occurred connecting to Amazon Appstore. Please try opening this app again", "Quit", true, false);
    public static final PromptContent c = new PromptContent(new String[]{"New Appstore version required", "Amazon Appstore Update Required"}, "Amazon Appstore is out of date.  Please visit the Amazon website to install the latest version of the Appstore. ", new String[]{"Quit", "Help"}, new com.amazon.android.framework.prompt.a[]{com.amazon.android.framework.prompt.a.DEFAULT, com.amazon.android.framework.prompt.a.HELP}, true, true, 1, 2);
    public static final PromptContent d = new PromptContent("Internet connection required", "An internet connection is required to open this app. Please connect to the internet and reopen this app.", "Quit", true, false);
    public static final PromptContent e = new PromptContent(new String[]{"Connection error"}, "An unknown error occurred connecting to Amazon Appstore.", new String[]{"Quit", "Help"}, new com.amazon.android.framework.prompt.a[]{com.amazon.android.framework.prompt.a.DEFAULT, com.amazon.android.framework.prompt.a.HELP}, true, true, 1, 3);
    public static final PromptContent f = new PromptContent("Internal error", "An internal error occurred, please try opening this app again", "Quit", true, false);
    public static final PromptContent g = new PromptContent("App update required", "Please update this app from the Amazon Appstore.", new String[]{"Quit", "Update"}, new com.amazon.android.framework.prompt.a[]{com.amazon.android.framework.prompt.a.DEFAULT, com.amazon.android.framework.prompt.a.DEEPLINK}, true, false, 0);

    private i() {
    }
}
