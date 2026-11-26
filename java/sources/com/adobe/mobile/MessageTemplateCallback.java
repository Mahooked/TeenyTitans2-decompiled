package com.adobe.mobile;

import android.text.TextUtils;
import android.util.Base64;
import java.io.UnsupportedEncodingException;
import java.security.SecureRandom;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import org.apache.http.protocol.HTTP;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
class MessageTemplateCallback extends Message {
    private static final String ADB_TEMPLATE_CALLBACK_BODY = "templatebody";
    private static final String ADB_TEMPLATE_CALLBACK_CONTENT_TYPE = "contenttype";
    private static final String ADB_TEMPLATE_CALLBACK_LOG_PREFIX = "Postbacks";
    private static final String ADB_TEMPLATE_CALLBACK_TIMEOUT = "timeout";
    private static final String ADB_TEMPLATE_CALLBACK_TOKEN_ADVERTISING_IDENTIFIER = "%adid%";
    private static final String ADB_TEMPLATE_CALLBACK_TOKEN_ALL_JSON_ENCODED = "%all_json%";
    private static final String ADB_TEMPLATE_CALLBACK_TOKEN_ALL_URL_ENCODED = "%all_url%";
    private static final String ADB_TEMPLATE_CALLBACK_TOKEN_ISO8601_TIMESTAMP = "%timestampz%";
    private static final String ADB_TEMPLATE_CALLBACK_TOKEN_MARKETING_CLOUD_ID = "%mcid%";
    private static final String ADB_TEMPLATE_CALLBACK_TOKEN_PUSH_IDENTIFIER = "%pushid%";
    private static final String ADB_TEMPLATE_CALLBACK_TOKEN_SDK_CACHEBUST = "%cachebust%";
    private static final String ADB_TEMPLATE_CALLBACK_TOKEN_SDK_VERSION = "%sdkver%";
    private static final String ADB_TEMPLATE_CALLBACK_TOKEN_UNIX_TIMESTAMP = "%timestampu%";
    private static final String ADB_TEMPLATE_CALLBACK_URL = "templateurl";
    private static final int ADB_TEMPLATE_TIMEOUT_DEFAULT = 2;
    private static final char ADB_TEMPLATE_TOKEN_END = '}';
    private static final char ADB_TEMPLATE_TOKEN_START = '{';
    private static final boolean[] tokenDataMask = {false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, true, false, true, true, true, true, true, true, true, true, true, true, false, false, false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, false, false, false, false, true, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false};
    private HashMap<String, Object> _combinedVariablesCopy;
    protected String contentType;
    private final SecureRandom randomGen = new SecureRandom();
    protected String templateBody;
    protected String templateUrl;
    protected int timeout;

    MessageTemplateCallback() {
    }

    @Override // com.adobe.mobile.Message
    protected boolean initWithPayloadObject(JSONObject dictionary) {
        byte[] decodedBytes;
        if (dictionary == null || dictionary.length() <= 0 || !super.initWithPayloadObject(dictionary)) {
            return false;
        }
        String logPrefix = logPrefix();
        try {
            JSONObject jsonPayload = dictionary.getJSONObject("payload");
            if (jsonPayload.length() <= 0) {
                StaticMethods.logDebugFormat("%s - Unable to create data callback %s, \"payload\" is empty", logPrefix, this.messageId);
                return false;
            }
            try {
                this.templateUrl = jsonPayload.getString(ADB_TEMPLATE_CALLBACK_URL);
                if (this.templateUrl.length() <= 0) {
                    StaticMethods.logDebugFormat("%s - Unable to create data callback %s, \"templateurl\" is empty", logPrefix, this.messageId);
                    return false;
                }
                try {
                    this.timeout = jsonPayload.getInt(ADB_TEMPLATE_CALLBACK_TIMEOUT);
                } catch (JSONException e) {
                    StaticMethods.logDebugFormat("%s - Tried to read \"timeout\" for data callback, but found none.  Using default value of two (2) seconds", logPrefix);
                    this.timeout = 2;
                }
                try {
                    String tempBody = jsonPayload.getString(ADB_TEMPLATE_CALLBACK_BODY);
                    if (tempBody != null && tempBody.length() > 0 && (decodedBytes = Base64.decode(tempBody, 0)) != null) {
                        String decodedBody = new String(decodedBytes, HTTP.UTF_8);
                        if (decodedBody.length() > 0) {
                            this.templateBody = decodedBody;
                        }
                    }
                } catch (UnsupportedEncodingException ex) {
                    StaticMethods.logDebugFormat("%s - Failed to decode \"templatebody\" for data callback (%s).  This is not a required field", logPrefix, ex.getLocalizedMessage());
                } catch (IllegalArgumentException ex2) {
                    StaticMethods.logDebugFormat("%s - Failed to decode \"templatebody\" for data callback (%s).  This is not a required field", logPrefix, ex2.getLocalizedMessage());
                } catch (JSONException e2) {
                    StaticMethods.logDebugFormat("%s - Tried to read \"templatebody\" for data callback, but found none.  This is not a required field", logPrefix);
                }
                if (this.templateBody != null && this.templateBody.length() > 0) {
                    try {
                        this.contentType = jsonPayload.getString(ADB_TEMPLATE_CALLBACK_CONTENT_TYPE);
                    } catch (JSONException e3) {
                        StaticMethods.logDebugFormat("%s - Tried to read \"contenttype\" for data callback, but found none.  This is not a required field", logPrefix);
                    }
                }
                return true;
            } catch (JSONException e4) {
                StaticMethods.logDebugFormat("%s - Unable to create data callback %s, \"templateurl\" is required", logPrefix, this.messageId);
                return false;
            }
        } catch (JSONException e5) {
            StaticMethods.logDebugFormat("%s - Unable to create create data callback %s, \"payload\" is required", logPrefix, this.messageId);
            return false;
        }
    }

    @Override // com.adobe.mobile.Message
    protected boolean shouldShowForVariables(Map<String, Object> vars, Map<String, Object> cdata, Map<String, Object> lifecycleData) {
        HashMap<String, Object> combinedVars = cdata != null ? new HashMap<>(cdata) : new HashMap<>();
        if (vars != null) {
            combinedVars.putAll(vars);
        }
        combinedVars.putAll(getExpansionTokensForVariables(combinedVars));
        if (lifecycleData != null) {
            combinedVars.putAll(lifecycleData);
        }
        this._combinedVariablesCopy = new HashMap<>(combinedVars);
        return super.shouldShowForVariables(vars, cdata, lifecycleData);
    }

    @Override // com.adobe.mobile.Message
    protected void show() {
        String expandedUrl = getExpandedUrl();
        String expandedBody = getExpandedBody();
        String logPrefix = logPrefix();
        StaticMethods.logDebugFormat("%s - Request Queued (url:%s body:%s contentType:%s)", logPrefix, expandedUrl, expandedBody, this.contentType);
        ThirdPartyQueue currQueue = getQueue();
        currQueue.queue(expandedUrl, expandedBody, this.contentType, StaticMethods.getTimeSince1970(), this.timeout);
    }

    private String getExpandedUrl() {
        if (this.templateUrl == null || this.templateUrl.length() <= 0) {
            return null;
        }
        ArrayList<String> nonEncodedParamsList = new ArrayList<>();
        nonEncodedParamsList.add("{%all_url%}");
        HashMap<String, String> urlExpansions = buildExpansionsForTokens(findTokensForExpansion(this.templateUrl), true);
        HashMap<String, String> nonEncodedURLExpansions = buildExpansionsForTokens(nonEncodedParamsList, false);
        urlExpansions.putAll(nonEncodedURLExpansions);
        String expandedUrl = StaticMethods.expandTokens(this.templateUrl, urlExpansions);
        return expandedUrl;
    }

    private String getExpandedBody() {
        if (this.templateBody == null || this.templateBody.length() <= 0) {
            return null;
        }
        HashMap<String, String> bodyExpansions = buildExpansionsForTokens(findTokensForExpansion(this.templateBody), true);
        ArrayList<String> nonEncodedParamsList = new ArrayList<>();
        nonEncodedParamsList.add("{%all_url%}");
        nonEncodedParamsList.add("{%all_json%}");
        HashMap<String, String> nonEncodedURLExpansions = buildExpansionsForTokens(nonEncodedParamsList, false);
        bodyExpansions.putAll(nonEncodedURLExpansions);
        String expandedBody = StaticMethods.expandTokens(this.templateBody, bodyExpansions);
        return expandedBody;
    }

    protected ThirdPartyQueue getQueue() {
        return ThirdPartyQueue.sharedInstance();
    }

    protected String logPrefix() {
        return ADB_TEMPLATE_CALLBACK_LOG_PREFIX;
    }

    private HashMap<String, Object> getExpansionTokensForVariables(HashMap<String, Object> vars) {
        HashMap<String, Object> expansionTokens = new HashMap<>(5);
        expansionTokens.put(ADB_TEMPLATE_CALLBACK_TOKEN_SDK_VERSION, "4.10.0-AN");
        expansionTokens.put(ADB_TEMPLATE_CALLBACK_TOKEN_SDK_CACHEBUST, String.valueOf(this.randomGen.nextInt(100000000)));
        expansionTokens.put(ADB_TEMPLATE_CALLBACK_TOKEN_ADVERTISING_IDENTIFIER, StaticMethods.getAdvertisingIdentifier());
        expansionTokens.put(ADB_TEMPLATE_CALLBACK_TOKEN_UNIX_TIMESTAMP, String.valueOf(StaticMethods.getTimeSince1970()));
        expansionTokens.put(ADB_TEMPLATE_CALLBACK_TOKEN_ISO8601_TIMESTAMP, StaticMethods.getIso8601Date());
        expansionTokens.put(ADB_TEMPLATE_CALLBACK_TOKEN_PUSH_IDENTIFIER, StaticMethods.getPushIdentifier());
        expansionTokens.put(ADB_TEMPLATE_CALLBACK_TOKEN_MARKETING_CLOUD_ID, VisitorIDService.sharedInstance().getMarketingCloudID() != null ? VisitorIDService.sharedInstance().getMarketingCloudID() : "");
        ArrayList<String> queryParameters = new ArrayList<>();
        for (Object key : vars.keySet()) {
            queryParameters.add(StaticMethods.URLEncode(key.toString()) + "=" + StaticMethods.URLEncode(vars.get(key).toString()));
        }
        expansionTokens.put(ADB_TEMPLATE_CALLBACK_TOKEN_ALL_URL_ENCODED, TextUtils.join("&", queryParameters));
        try {
            JSONObject jsonObject = new JSONObject(vars);
            String jsonString = jsonObject.toString();
            if (jsonString.length() > 0) {
                expansionTokens.put(ADB_TEMPLATE_CALLBACK_TOKEN_ALL_JSON_ENCODED, jsonString);
            }
        } catch (NullPointerException ex) {
            StaticMethods.logDebugFormat("Data Callback - unable to create json string for vars:  (%s)", ex.getLocalizedMessage());
        }
        return expansionTokens;
    }

    private HashMap<String, String> buildExpansionsForTokens(ArrayList<String> tokens, boolean urlEncodeExpansions) {
        HashMap<String, String> expansions = new HashMap<>(tokens.size());
        for (String token : tokens) {
            String cleanToken = token.substring(1, token.length() - 1).toLowerCase();
            Object tokenObject = this._combinedVariablesCopy.get(cleanToken);
            String tokenString = tokenObject == null ? "" : tokenObject.toString();
            String tokenValue = urlEncodeExpansions ? StaticMethods.URLEncode(tokenString) : tokenString;
            expansions.put(token, tokenValue);
        }
        return expansions;
    }

    private ArrayList<String> findTokensForExpansion(String input) {
        if (input == null || input.length() <= 0) {
            return null;
        }
        ArrayList<String> foundList = new ArrayList<>(32);
        int inputLength = input.length();
        int i = 0;
        while (i < inputLength) {
            if (input.charAt(i) == '{') {
                int j = i + 1;
                while (j < inputLength && input.charAt(j) != '}') {
                    j++;
                }
                if (j != inputLength) {
                    String token = input.substring(i, j + 1);
                    if (tokenIsValid(token.substring(1, token.length() - 1))) {
                        foundList.add(token);
                        i = j;
                    }
                } else {
                    return foundList;
                }
            }
            i++;
        }
        return foundList;
    }

    private boolean tokenIsValid(String token) {
        try {
            byte[] utf8Token = token.getBytes(HTTP.UTF_8);
            for (byte currentByte : utf8Token) {
                if (!tokenDataMask[currentByte & 255]) {
                    return false;
                }
            }
            return true;
        } catch (UnsupportedEncodingException ex) {
            StaticMethods.logErrorFormat("Data Callback - Unable to validate token (%s)", ex.getLocalizedMessage());
            return false;
        }
    }
}
