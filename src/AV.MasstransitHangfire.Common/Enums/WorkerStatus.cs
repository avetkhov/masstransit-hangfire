namespace AV.MasstransitHangfire.Common.Enums;

public enum WorkerStatus
{
    Initialized = 0,
    Starting,
    Running,
    Paused,
    Faulted,
    Stopping,
    Stopped,
    ShuttingDown
}