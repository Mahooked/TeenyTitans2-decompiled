package com.adobe.mobile;

import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class MessageTemplatePii extends MessageTemplateCallback {
    private static final String ADB_TEMPLATE_PII_LOG_PREFIX = "PII";

    MessageTemplatePii() {
    }

    @Override // com.adobe.mobile.MessageTemplateCallback, com.adobe.mobile.Message
    protected boolean initWithPayloadObject(JSONObject dictionary) {
        if (!super.initWithPayloadObject(dictionary)) {
            return false;
        }
        if (this.templateUrl.length() > 0 && this.templateUrl.toLowerCase().trim().startsWith("https")) {
            return true;
        }
        StaticMethods.logDebugFormat("Data Callback - Unable to create data callback %s, templateurl is empty or does not use https for request", this.messageId);
        return false;
    }

    @Override // com.adobe.mobile.MessageTemplateCallback
    protected String logPrefix() {
        return ADB_TEMPLATE_PII_LOG_PREFIX;
    }

    @Override // com.adobe.mobile.MessageTemplateCallback
    protected ThirdPartyQueue getQueue() {
        return PiiQueue.sharedInstance();
    }
}
