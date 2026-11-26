package com.adobe.mobile;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
class MessageMatcherExists extends MessageMatcher {
    MessageMatcherExists() {
    }

    @Override // com.adobe.mobile.MessageMatcher
    protected boolean matchesInMaps(Map<String, Object>... maps) {
        Object value = null;
        if (maps == null || maps.length <= 0) {
            return false;
        }
        for (Map<String, Object> map : maps) {
            if (map != null && map.containsKey(this.key)) {
                value = map.get(this.key);
                break;
            }
        }
        return value != null;
    }
}
