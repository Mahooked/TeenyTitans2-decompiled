package com.amazon.android.framework.task;

import com.amazon.android.framework.task.pipeline.TaskPipelineId;
import java.util.Date;

/* JADX INFO: loaded from: classes.dex */
public interface TaskManager {
    void enqueue(TaskPipelineId taskPipelineId, Task task);

    void enqueueAfterDelay(TaskPipelineId taskPipelineId, Task task, long j);

    void enqueueAtFront(TaskPipelineId taskPipelineId, Task task);

    void enqueueAtTime(TaskPipelineId taskPipelineId, Task task, Date date);
}
