package com.amazon.android.framework.prompt;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import com.amazon.android.framework.util.KiwiLogger;

/* JADX INFO: loaded from: classes.dex */
public abstract class SimplePrompt extends Prompt {
    private static final String LEGACY_BUTTON_LABEL = "OK";
    private static final KiwiLogger LOGGER = new KiwiLogger("SimplePrompt");
    protected final PromptContent content;
    protected final b mode;

    public SimplePrompt(PromptContent promptContent) {
        this(promptContent, b.LEGACY);
    }

    public SimplePrompt(PromptContent promptContent, b bVar) {
        com.amazon.android.d.a.a(promptContent, "content");
        this.content = promptContent;
        this.mode = bVar;
        LOGGER.trace("created SimplePrompt with mode " + bVar);
    }

    private final Dialog createExtendedDialog(Activity activity) {
        LOGGER.trace("doCreate() called to create extended dialog");
        AlertDialog.Builder builder = new AlertDialog.Builder(activity);
        builder.setTitle(this.content.getExtendedTitle()).setMessage(this.content.getMessage()).setCancelable(false);
        setUpButtons(builder);
        LOGGER.trace("doCreate() returning");
        return builder.create();
    }

    private final Dialog createLegacyDialog(Activity activity) {
        LOGGER.trace("Legacy mode dialog, legacy title = " + this.content.getTitle() + ", extended title = " + this.content.getExtendedTitle());
        AlertDialog.Builder builder = new AlertDialog.Builder(activity);
        builder.setTitle(this.content.getExtendedTitle()).setMessage(this.content.getMessage()).setCancelable(false).setNeutralButton("OK", new l(this));
        return builder.create();
    }

    private boolean[] filterActions(a[] aVarArr) {
        if (aVarArr == null) {
            return null;
        }
        boolean[] zArr = new boolean[aVarArr.length];
        for (int i = 0; i < aVarArr.length; i++) {
            zArr[i] = canHandleAction(aVarArr[i]);
            if (!zArr[i]) {
                LOGGER.trace("filterActions() filtering item " + i);
            }
        }
        return zArr;
    }

    private void setNeutralButton(AlertDialog.Builder builder, String str, int i) {
        builder.setNeutralButton(str, new j(this, i));
    }

    private void setUpButtons(AlertDialog.Builder builder) {
        String[] buttonLabels = this.content.getButtonLabels();
        if (buttonLabels.length <= 0 || buttonLabels.length > 2) {
            LOGGER.error("Unexpected button count: " + buttonLabels.length);
            return;
        }
        boolean[] zArrFilterActions = filterActions(this.content.getButtonActions());
        int i = 0;
        int i2 = -1;
        for (int i3 = 0; i3 < zArrFilterActions.length; i3++) {
            if (zArrFilterActions[i3]) {
                i++;
                i2 = i3;
            }
        }
        if (i == 1) {
            LOGGER.trace("single button dialog");
            setNeutralButton(builder, buttonLabels[i2], i2);
        } else {
            LOGGER.trace("two button dialog");
            builder.setNegativeButton(buttonLabels[0], new m(this));
            builder.setPositiveButton(buttonLabels[1], new h(this));
        }
    }

    protected boolean canHandleAction(a aVar) {
        return aVar == a.DEFAULT;
    }

    protected abstract void doAction();

    protected void doAction(int i) {
        LOGGER.error("extended doAction from base class called, this should never happen.");
        doAction();
    }

    @Override // com.amazon.android.framework.prompt.Prompt
    protected boolean doCompatibilityCheck(Activity activity) {
        return this.content.isVisible();
    }

    @Override // com.amazon.android.framework.prompt.Prompt
    public final Dialog doCreate(Activity activity) {
        return this.mode == b.LEGACY ? createLegacyDialog(activity) : createExtendedDialog(activity);
    }

    @Override // com.amazon.android.framework.prompt.Prompt
    protected void doExpiration(f fVar) {
        doAction();
    }
}
