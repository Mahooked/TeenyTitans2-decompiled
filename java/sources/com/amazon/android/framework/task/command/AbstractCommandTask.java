package com.amazon.android.framework.task.command;

import android.app.Application;
import android.content.Intent;
import android.os.RemoteException;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.android.framework.prompt.PromptManager;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.util.KiwiLogger;
import com.amazon.venezia.command.FailureResult;
import com.amazon.venezia.command.SuccessResult;
import com.amazon.venezia.command.r;
import com.amazon.venezia.command.w;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public abstract class AbstractCommandTask extends com.amazon.android.k.c {
    private static final KiwiLogger LOGGER = new KiwiLogger("AbstractCommandTask");

    @Resource
    private Application application;

    @Resource
    private f authTokenVerifier;

    @Resource
    private c client;

    @Resource
    private com.amazon.android.p.d metricsManager;

    @Resource
    private PromptManager promptManager;

    @Resource
    protected com.amazon.android.e.b resultManager;

    private void expire(r rVar, b bVar) throws RemoteException, KiwiException {
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Expiring Decision: " + rVar + ", reason: " + bVar);
        }
        handleCommandResult(this.client.a(rVar, bVar));
    }

    private com.amazon.android.p.b extractMetric(KiwiException kiwiException) {
        com.amazon.android.p.b bVar = new com.amazon.android.p.b(getFailureMetricName());
        bVar.a("subType", kiwiException.getType()).a("reason", kiwiException.getReason()).a("context", kiwiException.getContext());
        return bVar;
    }

    private w getCommand() {
        return new h(this);
    }

    private String getFailureMetricName() {
        return getCommandName() + "_failure";
    }

    private void handleChoice(r rVar, com.amazon.venezia.command.n nVar) throws RemoteException, KiwiException {
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Handling customer choice: " + nVar);
        }
        Intent intentB = nVar.b();
        if (intentB == null) {
            if (KiwiLogger.TRACE_ON) {
                LOGGER.trace("No intent given, choosing now");
            }
            handleCommandResult(this.client.a(nVar));
            return;
        }
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Choice has intent, scheduling it to be fired!!");
        }
        com.amazon.android.e.f fVarA = this.resultManager.a(intentB);
        if (fVarA == null) {
            if (KiwiLogger.TRACE_ON) {
                LOGGER.trace("No result recived, expiring decision");
            }
            expire(rVar, b.EXPIRATION_DURATION_ELAPSED);
        } else if (fVarA.b == 0) {
            if (KiwiLogger.TRACE_ON) {
                LOGGER.trace("Result canceled, expiring decision");
            }
            expire(rVar, b.ACTION_CANCELED);
        } else {
            if (KiwiLogger.TRACE_ON) {
                LOGGER.trace("Result received!!!, notifying service");
            }
            handleCommandResult(this.client.a(nVar));
        }
    }

    private void handleCommandResult(m mVar) throws RemoteException, KiwiException {
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Received result from CommandService: " + mVar);
        }
        if (mVar == null) {
            if (KiwiLogger.TRACE_ON) {
                LOGGER.trace("Received null result from command service, exiting task");
            }
        } else {
            if (mVar.f != null) {
                handleException(mVar.f);
                return;
            }
            if (mVar.b != null) {
                throw mVar.b;
            }
            this.authTokenVerifier.a(mVar.a, this.client.a());
            if (mVar.c != null) {
                handleSuccess(mVar.c);
            } else if (mVar.d != null) {
                handleFailure(mVar.d);
            } else {
                handleDecision(mVar.e);
            }
        }
    }

    private void handleDecision(r rVar) throws RemoteException, KiwiException {
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Handling Decision");
        }
        try {
            d dVar = new d(rVar);
            this.promptManager.present(dVar);
            com.amazon.venezia.command.n nVarA = dVar.a();
            if (nVarA != null) {
                handleChoice(rVar, nVarA);
                return;
            }
            if (KiwiLogger.TRACE_ON) {
                LOGGER.trace("DecisionChooser returned null!!, expiring");
            }
            expire(rVar, b.EXPIRATION_DURATION_ELAPSED);
        } catch (com.amazon.android.b.c e) {
            expire(rVar, e.a);
        }
    }

    private void handleException(com.amazon.venezia.command.j jVar) throws RemoteException, KiwiException {
        throw new com.amazon.android.b.e(jVar);
    }

    private void handleExecutionException(Throwable th) {
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Exception occurred while processing task: " + th, th);
        }
        KiwiException kiwiExceptionTranslate = translate(th);
        onException(kiwiExceptionTranslate);
        this.metricsManager.a(extractMetric(kiwiExceptionTranslate));
    }

    private void handleFailure(FailureResult failureResult) throws RemoteException, KiwiException {
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Command failed execution: " + failureResult.getDisplayableName());
        }
        onFailure(failureResult);
    }

    private void handleSuccess(SuccessResult successResult) throws RemoteException, KiwiException {
        if (KiwiLogger.TRACE_ON) {
            LOGGER.trace("Command executed successfully");
        }
        onSuccess(successResult);
    }

    private void postExecution() {
        if (isWorkflowChild()) {
            return;
        }
        this.client.b();
    }

    private KiwiException translate(Throwable th) {
        if (th instanceof KiwiException) {
            return (KiwiException) th;
        }
        return th instanceof RemoteException ? new com.amazon.android.b.a((RemoteException) th) : new com.amazon.android.b.b(th);
    }

    @Override // com.amazon.android.framework.task.Task
    public final void execute() {
        try {
            if (KiwiLogger.TRACE_ON) {
                LOGGER.trace("----------------------------------------------");
                LOGGER.trace("Executing: " + getCommandName());
                LOGGER.trace("----------------------------------------------");
            }
            preExecution();
            if (!isExecutionNeeded()) {
                if (KiwiLogger.TRACE_ON) {
                    LOGGER.trace("Execution not needed, quitting");
                }
            } else {
                if (KiwiLogger.TRACE_ON) {
                    LOGGER.trace("Executing Command: " + getCommandName());
                }
                handleCommandResult(this.client.a(getCommand()));
            }
        } catch (Throwable th) {
            handleExecutionException(th);
        } finally {
            if (KiwiLogger.TRACE_ON) {
                LOGGER.trace("Task finished");
            }
            postExecution();
        }
    }

    protected abstract Map getCommandData();

    protected abstract String getCommandName();

    protected abstract String getCommandVersion();

    protected abstract boolean isExecutionNeeded();

    protected void onException(KiwiException kiwiException) {
        LOGGER.error("On Exception!!!!: " + kiwiException);
    }

    protected abstract void onFailure(FailureResult failureResult) throws RemoteException, KiwiException;

    protected abstract void onSuccess(SuccessResult successResult) throws RemoteException, KiwiException;

    protected void preExecution() throws KiwiException {
    }
}
